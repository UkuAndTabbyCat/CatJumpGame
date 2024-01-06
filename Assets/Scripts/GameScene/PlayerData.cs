using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;
    public int Life { get; private set; }

    private void Awake()
    {
        Instance = this;

        if (!StartPlayerData.Instance)
        {
            Life = 3;
            return;
        }
        Life = StartPlayerData.Instance.PlayerLife;
    }

}
