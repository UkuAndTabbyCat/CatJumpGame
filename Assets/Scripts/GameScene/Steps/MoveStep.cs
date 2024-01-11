using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStep : MonoBehaviour
{
    private float speed;
    private int forward;
    private float cur_pos_x;

    private void Start()
    {
        forward = Random.value < 0.5 ? -1 : 1;
        speed = Random.Range(3f, 8f);
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.right * Time.deltaTime * speed * forward);
        cur_pos_x = Mathf.Abs(transform.position.x);
        if (cur_pos_x > 28)
        {
            transform.position = new Vector3(forward * cur_pos_x, transform.position.y, transform.position.z);
            forward *= -1;
        }
    }
}
