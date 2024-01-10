using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanCheck : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    private void OnTriggerEnter(Collider other)
    {
        playerController.SetPlayerParameter(BackGroundScene.Ocean);
    }

}
