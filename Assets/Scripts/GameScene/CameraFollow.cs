using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && player.transform.position.y > transform.position.y)
        {
            float len = player.transform.position.y - transform.position.y;
            CameraMove(len);
        }
    }

    private void CameraMove(float len)
    {
        transform.position += Vector3.up * len;
    }
}
