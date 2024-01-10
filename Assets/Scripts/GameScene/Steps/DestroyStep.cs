using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyStep : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.transform.position.y - transform.position.y > 22)
        {
            Destroy(gameObject);
        }
    }
}
