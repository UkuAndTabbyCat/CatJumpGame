using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyOnceStep : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
