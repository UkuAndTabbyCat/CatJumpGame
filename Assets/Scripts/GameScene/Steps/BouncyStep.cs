using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyStep : MonoBehaviour
{
    [SerializeField] private List<AudioClip> m_AudioClips;
    [SerializeField] private GameObject m_BouncyTextBoing;

    private AudioSource m_AudioSource;
    private Animator m_Animator;
    public bool isTrig { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_Animator = GetComponent<Animator>();
        m_BouncyTextBoing = Instantiate(m_BouncyTextBoing, -2.6f * Vector3.forward + transform.position, m_BouncyTextBoing.transform.rotation);
        if (transform.localScale.x > 2.5f)
        {
            transform.localScale = new Vector3(2.5f, transform.localScale.y, transform.localScale.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isTrig)
        {
            isTrig = true;
            int num = Random.Range(0, m_AudioClips.Count);
            m_Animator.SetTrigger("BouncyJump_b");
            m_AudioSource.PlayOneShot(m_AudioClips[num]);
            m_BouncyTextBoing.GetComponent<ParticleSystem>().Play();
        }
    }

    private void OnDestroy()
    {
        Destroy(m_BouncyTextBoing);
    }
}
