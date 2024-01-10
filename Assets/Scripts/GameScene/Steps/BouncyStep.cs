using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyStep : MonoBehaviour
{
    [SerializeField] private List<AudioClip> m_AudioClips;
    [SerializeField] private GameObject m_BouncyTextBoing;

    private AudioSource m_AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_BouncyTextBoing = Instantiate(m_BouncyTextBoing, transform.position, m_BouncyTextBoing.transform.rotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        int num = Random.Range(0, m_AudioClips.Count);
        m_AudioSource.PlayOneShot(m_AudioClips[num]);
        m_BouncyTextBoing.GetComponent<ParticleSystem>().Play();
    }
}
