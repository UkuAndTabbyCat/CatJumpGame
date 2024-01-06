using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{

    [SerializeField] UI_Manager m_UI_Manager;

    private AudioSource m_AudioSource;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        m_AudioSource.Play();
        Destroy(other.gameObject);
        m_UI_Manager.GameOver();
    }
}
