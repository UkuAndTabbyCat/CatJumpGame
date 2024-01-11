using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class U10PS_DissolveOverTime : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private BoxCollider m_BoxCollider;

    private float speed;
    private float m_CurCutOff;

    private void Start()
    {
        speed = Random.Range(0.4f, 1.2f);
        meshRenderer = this.GetComponent<MeshRenderer>();
        m_BoxCollider = GetComponent<BoxCollider>();
    }

    private float t = 0.0f;
    private void Update()
    {
        Material[] mats = meshRenderer.materials;

        m_CurCutOff = Mathf.Sin(t * speed);
        mats[0].SetFloat("_Cutoff", m_CurCutOff);
        t += Time.deltaTime;

        // Unity does not allow meshRenderer.materials[0]...
        meshRenderer.materials = mats;
    }

    private void FixedUpdate()
    {
        if (m_CurCutOff > 0.3f)
        {
            m_BoxCollider.enabled = false;
        }
        else
        {
            m_BoxCollider.enabled = true;
        }
    }
}
