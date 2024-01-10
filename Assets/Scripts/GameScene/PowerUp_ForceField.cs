using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_ForceField : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        other.gameObject.GetComponent<PlayerController>().PowerUp_Protect();
    }
}
