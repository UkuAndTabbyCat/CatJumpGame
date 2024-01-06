using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] List<AudioClip> m_JumpSound;

    [SerializeField] private float jumpVelocity;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private float fallMultiplier;
    private float inputHorizontal;
    private Vector3 inputAcceleration;
    private float xBound = 4.5f;

    private Rigidbody playerRb;
    private Animator playerAnimator;
    private AudioSource playerAudioSource;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnimator = transform.GetComponentInChildren<Animator>();
        playerAudioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        moveLimit();
        if (playerRb.velocity.y > 0)
        {
            transform.GetComponent<BoxCollider>().isTrigger = true;
            playerRb.velocity += Vector3.up * Physics.gravity.y * jumpMultiplier * Time.deltaTime;
        }
        else
        {
            transform.GetComponent<BoxCollider>().isTrigger = false;
            playerRb.velocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.deltaTime;
        }

        if (playerRb.velocity.y < 0)
        {
            playerAnimator.SetBool("Jump_b", false);
        }
    }

    private void Move()
    {
# if UNITY_EDITOR
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

    private void OnCollisionEnter(Collision collision)
    {
        playerRb.velocity += Vector3.up * jumpVelocity;
        playerAnimator.SetBool("Jump_b", true);
        int num = Random.Range(0, m_JumpSound.Count);
        playerAudioSource.PlayOneShot(m_JumpSound[num], 0.6f);

    }
}
