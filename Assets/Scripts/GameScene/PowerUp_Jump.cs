using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_Jump : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        other.gameObject.GetComponent<PlayerController>().PowerUp_Jump();
    }
}
