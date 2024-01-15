using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BackGroundScene
{
    Ocean,
    Ground,
    Sky,
    Space
}

public class PlayerController : MonoBehaviour
{
    // Status PowerUp
    [SerializeField] List<GameObject> m_PowerUpLists;
    [SerializeField] List<AudioClip> m_JumpSound;

    [SerializeField] private ParticleSystem m_JumpUnderWater;

    [SerializeField] private float jumpVelocity;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private float fallMultiplier;
    private float inputHorizontal;
#if UNITY_ANDROID
    private Vector3 inputAcceleration;
# endif
    private float xBound = 30f;

    private Rigidbody playerRb;
    private float m_Velocity_y;
    private Animator playerAnimator;
    private AudioSource playerAudioSource;

    private Coroutine[] m_coroutines = new Coroutine[2];

    // Status Tag
    public bool isProtect { get; private set; }

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnimator = transform.GetComponentInChildren<Animator>();
        playerAudioSource = GetComponent<AudioSource>();

        // Init Status
        isProtect = false;
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        moveLimit();
        m_Velocity_y = playerRb.velocity.y;
        playerAnimator.SetFloat("Velocity_f", Mathf.Abs(m_Velocity_y));
        if (m_Velocity_y > 0)
        {
            transform.GetComponent<BoxCollider>().isTrigger = true;
            playerRb.velocity += Vector3.up * Physics.gravity.y * jumpMultiplier * Time.deltaTime;
        }
        else
        {
            transform.GetComponent<BoxCollider>().isTrigger = false;
            playerRb.velocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.deltaTime;
        }
    }

    private void Move()
    {
#if UNITY_EDITOR
        inputHorizontal = Input.GetAxis("Horizontal");
#elif UNITY_ANDROID
        inputAcceleration = GetAccelerometerValue() * 2.0f;
        inputHorizontal = inputAcceleration.x;
#endif
        if (inputHorizontal < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else if (inputHorizontal > 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.Translate(Vector3.right * Mathf.Abs(inputHorizontal) * moveSpeed * Time.deltaTime);
    }

    private Vector3 GetAccelerometerValue()
    {
        Vector3 getAcc = Vector3.zero;
        float period = 0f;
        foreach (AccelerationEvent evnt in Input.accelerationEvents)
        {
            getAcc += evnt.acceleration * evnt.deltaTime;
            period += evnt.deltaTime;
        }
        if (period > 0)
        {
            getAcc *= 1.0f / period;
        }

        // Add lowPassFilter
        getAcc = Vector3.Lerp(getAcc, Input.acceleration, 1f / 60f);
        return getAcc;
    }

    private void moveLimit()
    {
        // limit x position
        if (transform.position.x > xBound)
        {
            transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -xBound)
        {
            transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);
        }
    }

    public void SetPlayerParameter(BackGroundScene s)
    {
        switch (s)
        {
            case BackGroundScene.Ocean:
                moveSpeed = 25f;
                jumpMultiplier = 4f;
                fallMultiplier = 2.5f;
                playerAnimator.speed = 0.8f;
                break;
            case BackGroundScene.Ground:
                moveSpeed = 30f;
                jumpMultiplier = 6f;
                fallMultiplier = 4f;
                playerAnimator.speed = 1f;
                break;
            case BackGroundScene.Sky:
                break;
            case BackGroundScene:
                break;

        }
    }

    public void PowerUp_Protect()
    {
        m_PowerUpLists[0].SetActive(true);
        isProtect = true;
        if (null != m_coroutines[0])
        {
            StopCoroutine(m_coroutines[0]);
        }
        m_coroutines[0] = StartCoroutine("EnableProtect");
    }

    private IEnumerator EnableProtect()
    {
        yield return new WaitForSeconds(5);
        m_PowerUpLists[0].SetActive(false);
        isProtect = false;
    }

    public void PowerUp_Jump()
    {
        m_PowerUpLists[1].SetActive(true);
        jumpVelocity *= 1.2f;
        if (null != m_coroutines[1])
        {
            StopCoroutine(m_coroutines[1]);
        }
        m_coroutines[1] = StartCoroutine("EnableJumpHigher");
    }

    private IEnumerator EnableJumpHigher()
    {
        yield return new WaitForSeconds(5);
        m_PowerUpLists[1].SetActive(false);
        jumpVelocity /= 1.2f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        playerAnimator.SetTrigger("Jump_t");
        if (collision.gameObject.CompareTag("Bouncy") && !collision.gameObject.GetComponent<BouncyStep>().isTrig)
        {
            playerRb.velocity = Vector3.up * 1.5f * jumpVelocity;
        }
        else
        {
            playerRb.velocity = Vector3.up * jumpVelocity;
            int num = Random.Range(0, m_JumpSound.Count);
            playerAudioSource.PlayOneShot(m_JumpSound[num], 0.6f);
        }
        m_JumpUnderWater.Play();
    }
}
