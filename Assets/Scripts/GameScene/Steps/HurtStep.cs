using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtStep : MonoBehaviour
{
    [SerializeField] private List<AudioClip> m_AudioClips;
    [SerializeField] private GameObject m_BrokenHeart;

    private UI_Manager m_manager;
    private AudioSource m_audioSource;
    private void Awake()
    {
        m_manager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        m_audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        m_BrokenHeart = Instantiate(m_BrokenHeart, transform.position, m_BrokenHeart.transform.rotation);
    }
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>().isProtect)
            return;

        m_manager.HurtLife(1);
        m_BrokenHeart.GetComponent<ParticleSystem>().Play();
        m_audioSource.PlayOneShot(m_AudioClips[0]);
        if (m_manager.isGameOver)
        {
            m_manager.GameOver();
            m_audioSource.PlayOneShot(m_AudioClips[1]);
            Destroy(collision.gameObject);
        }
    }
}
