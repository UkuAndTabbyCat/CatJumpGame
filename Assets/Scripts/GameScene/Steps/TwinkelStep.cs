using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwinkelStep : MonoBehaviour
{
    private bool switchEnable = false;

    private MeshRenderer m_TwinkleMeshRender;
    private BoxCollider m_BoxCollider;
    // Start is called before the first frame update
    void Start()
    {
        m_TwinkleMeshRender = GetComponent<MeshRenderer>();
        m_BoxCollider = GetComponent<BoxCollider>();
        StartCoroutine("TwinkleTwinkle");
    }

    private IEnumerator TwinkleTwinkle()
    {
        float f = Random.Range(1.5f, 3f);
        while (true)
        {
            m_TwinkleMeshRender.enabled = switchEnable;
            m_BoxCollider.enabled = switchEnable;
            switchEnable = !switchEnable;
            yield return new WaitForSeconds(f);
        }
    }
}
