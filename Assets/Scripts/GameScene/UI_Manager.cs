using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI m_ScoreText;
    [SerializeField] TextMeshProUGUI m_GameOverScoreText;
    [SerializeField] List<Image> m_lifeImg;
    [SerializeField] List<AudioClip> m_BGM_Lists;
    [SerializeField] GameObject m_CountDownCanvas;
    [SerializeField] GameObject m_StatusPanel;
    [SerializeField] GameObject m_StopPanel;
    [SerializeField] GameObject m_GameOverPanel;
    [SerializeField] List<GameObject> m_CountDownNum;

    private AudioSource m_AudioSource;

    public bool isGameOver = false;
    private int m_PlayerLife;
    private int score;

    private bool startTag = false;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerLife = PlayerData.Instance.Life;
        for (int i = 0; i < m_PlayerLife; i++)
        {
            m_lifeImg[i].enabled = true;
        }
        m_AudioSource = GetComponent<AudioSource>();
        StartCoroutine("CountDown");
        Time.timeScale = 0;
        // Time.timeScale will cause WaitForSconds stop!!!
    }

    // Update is called once per frame
    void Update()
    {
        if (startTag)
        {
            Time.timeScale = 1;
            startTag = false;
        }
        UpdateScore();
    }

    void UpdateScore()
    {
        float pos_y = Camera.main.transform.position.y * 10f;
        score = Mathf.RoundToInt(pos_y);
        m_ScoreText.SetText($"Score : {score}");
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Continue()
    {
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void UpdateGameOverScore()
    {
        m_GameOverScoreText.SetText($"Score : {score}");
    }

    public void HurtLife(int num)
    {
        if (num >= m_PlayerLife)
        {
            num = m_PlayerLife;
        }
        for (int i = m_PlayerLife - num; i < m_PlayerLife; i++)
        {
            m_lifeImg[i].enabled = false;
        }
        m_PlayerLife -= num;
        if (m_PlayerLife == 0)
        {
            isGameOver = true;
        }
    }

    public void GameOver()
    {
        UpdateGameOverScore();
        isGameOver = true;
        m_StatusPanel.SetActive(false);
        m_StopPanel.SetActive(false);
        m_GameOverPanel.SetActive(true);
    }

    private IEnumerator CountDown()
    {
        for (int i = 0; i < m_CountDownNum.Count; i++)
        {
            m_CountDownNum[i].SetActive(true);
            yield return new WaitForSecondsRealtime(1);
            m_CountDownNum[i].SetActive(false);
        }
        m_CountDownCanvas.SetActive(false);
        startTag = true;
        m_StopPanel.SetActive(true);
        StartCoroutine("LoopGameMusic");
    }

    private IEnumerator LoopGameMusic()
    {
        int i = 0;
        m_AudioSource.PlayOneShot(m_BGM_Lists[i], 1.5f);
        new WaitForSeconds(2);
        while (true)
        {
            yield return new WaitForSeconds(2);
            if (m_AudioSource.isPlaying)
                continue;
            else
                i++;

            if (i == m_BGM_Lists.Count)
                i = 0;
            m_AudioSource.PlayOneShot(m_BGM_Lists[i], 1.5f);
        }
    }

}
