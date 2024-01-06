using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtStep : MonoBehaviour
{
    private UI_Manager m_manager;
    private AudioSource m_audioSource;
    private void Awake()
    {
        m_manager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        m_audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        m_manager.HurtLife(1);
        if (m_manager.isGameOver)
        {
            m_manager.GameOver();
            m_audioSource.Play();
            Destroy(collision.gameObject);
        }
    }
}
