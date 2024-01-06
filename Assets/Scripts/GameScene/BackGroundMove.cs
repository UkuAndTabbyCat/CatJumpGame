using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove : MonoBehaviour
{

    // Update is called once per frame
    void LateUpdate()
    {
        if (Camera.main.transform.position.y - transform.position.y > 20)
        {
            transform.position += Vector3.up * 40;
        }
    }
}
