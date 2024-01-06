using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public enum GameLevel
{
    Easy,
    Medium,
    Hard

}

public class StartPlayerData : MonoBehaviour
{
    public static StartPlayerData Instance;
    [SerializeField] private Toggle[] m_LevelToggle;
    public GameLevel Level { get; private set; }

    public int PlayerLife { get; private set; }

    private bool isMenu;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        m_LevelToggle = new Toggle[3];

        isMenu = InitializeLevel();
        Level = GameLevel.Medium;
        SelectLevel(true, m_LevelToggle[1]);
    }

    private void Start()
    {

        UpdateSelectLevel();
        SceneManager.sceneLoaded += LoadSelectLevel;
    }

    private void LoadSelectLevel(Scene scene, LoadSceneMode mode)
    {
        // todo tuning!!!
        isMenu = InitializeLevel();
        if (!isMenu)
        {
            return;
        }
        UpdateSelectLevel();
        SetLevel(Level);
        SetPlayerLife(Level);
    }

    private bool InitializeLevel()
    {
        GameObject[] m_obj_levels = GameObject.FindGameObjectsWithTag("Level");
        if (m_obj_levels.Length != 3)
        {
            return false;
        }
        for (int i = 0; i < m_obj_levels.Length; i++)
        {

            if (m_obj_levels[i].name == "Easy")
            {
                m_LevelToggle[0] = m_obj_levels[i].GetComponent<Toggle>();
            }
            if (m_obj_levels[i].name == "Medium")
            {
                m_LevelToggle[1] = m_obj_levels[i].GetComponent<Toggle>();
            }
            if (m_obj_levels[i].name == "Hard")
            {
                m_LevelToggle[2] = m_obj_levels[i].GetComponent<Toggle>();
            }
        }
        return true;
    }

    private void SetLevel(GameLevel Level)
    {
        switch (Level)
        {
            case GameLevel.Easy:
                m_LevelToggle[0].isOn = true;
                break;
            case GameLevel.Medium:
                m_LevelToggle[1].isOn = true;
                break;
            case GameLevel.Hard:
                m_LevelToggle[2].isOn = true;
                break;
        }
    }

    public void UpdateSelectLevel()
    {
        for (int i = 0; i < m_LevelToggle.Length; i++)
        {
            // why must creat new object?
            Toggle t = m_LevelToggle[i];
            t.onValueChanged.AddListener((b) => SelectLevel(b, t));
        }
    }

    public void SelectLevel(bool isSet, Toggle toggle)
    {
        if (!isSet)
        {
            return;
        }
        Level = toggle.GetComponent<LevelSelect>().MyLevel;
        SetPlayerLife(Level);
    }

    private void SetPlayerLife(GameLevel Level)
    {
        switch (Level)
        {
            case GameLevel.Easy:
                PlayerLife = 5;
                break;
            case GameLevel.Medium:
                PlayerLife = 3;
                break;
            case GameLevel.Hard:
                PlayerLife = 1;
                break;
        }
    }
}
