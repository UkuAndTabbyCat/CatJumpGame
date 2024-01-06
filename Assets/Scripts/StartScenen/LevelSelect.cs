using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public GameLevel MyLevel { get; private set; }
    private GameLevel CurLevel { get; set; }

    private void Awake()
    {
        switch (gameObject.name)
        {
            case "Easy":
                MyLevel = GameLevel.Easy;
                break;
            case "Medium":
                MyLevel = GameLevel.Medium;
                break;
            case "Hard":
                MyLevel = GameLevel.Hard;
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CurLevel = StartPlayerData.Instance.Level;

        if (MyLevel == CurLevel)
        {
            gameObject.GetComponent<Toggle>().isOn = true;
        }
    }

}
