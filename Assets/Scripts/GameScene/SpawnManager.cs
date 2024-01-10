using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    /*
     每隔10m生成一组随机障碍
     */

    [SerializeField] GameObject normalStep;
    [SerializeField] List<GameObject> m_SpecialStep;
    [SerializeField] List<GameObject> m_PowerUp;
    [SerializeField] GameObject m_Bubble;
    [SerializeField] UI_Manager m_UI_Manager;
    private float cameraHeight;
    private float startHeight_Left = 0;
    private float startHeight_Right = 0;

    // Double Axies Spawn Steps x = -5, Range(-25, 15); x = 5, Range(-15, 25)
    private float xAxies = 5f;

    // PowerUp CountDown
    private bool m_GenPowerUp = false;

    // Start is called before the first frame update
    void Start()
    {
        // 生成初始step
        cameraHeight = Camera.main.transform.position.y;
        float startStep_y = -10;
        GameObject step = null;

        for (int i = 0; i < 6; i++)
        {
            startStep_y += Random.Range(4f, 9f);
            step = Instantiate(normalStep, new Vector3(Random.Range(-10, 10), startStep_y, 0), normalStep.transform.rotation);
            if (startStep_y > 10)
            {
                break;
            }
        }
        StartCoroutine("GenerateBubble");
        StartCoroutine("PowerUpCountDown");
        StartCoroutine("SpawnLeftStep");
        StartCoroutine("SpawnRightStep");
    }

    // Update is called once per frame
    void Update()
    {
        cameraHeight = Camera.main.transform.position.y;
    }

    private void GenerateRandomStep(bool isRight, GameObject step, float loc_y)
    {
        int forward = isRight ? 1 : -1;
        float random_x = Random.Range(forward * xAxies - 20, forward * xAxies + 20);

        // generate random step length
        float scale_x = Random.Range(2f, 6f);
        step.transform.localScale = new Vector3(scale_x, step.transform.localScale.y, step.transform.localScale.z);
        Vector3 loc = new Vector3(random_x, loc_y, 0);

        Instantiate(step, loc, step.transform.rotation);

        // Generate Special PowerUp
        if (m_GenPowerUp)
        {
            GenerateRandomPowerUp(loc);
            m_GenPowerUp = false;
        }
    }

    private IEnumerator GenerateBubble()
    {
        while (true)
        {
            float x = Random.Range(-30f, 30f);
            float y = Camera.main.transform.position.y - 25f;
            float z = Random.Range(-15f, 15f);
            Instantiate(m_Bubble, new Vector3(x, y, z), m_Bubble.transform.rotation);
            yield return new WaitForSeconds(Random.Range(2f, 5f));
        }
    }

    private void GenerateRandomPowerUp(Vector3 loc)
    {
        loc = loc + Vector3.up * 3.5f;
        int num = Random.Range(0, m_PowerUp.Count);
        Instantiate(m_PowerUp[num], loc, m_PowerUp[num].transform.rotation);
    }

    private IEnumerator PowerUpCountDown()
    {
        while (!m_UI_Manager.isGameOver)
        {
            yield return new WaitForSeconds(Random.Range(3f, 8f));
            m_GenPowerUp = true;
            yield return new WaitForSeconds(2);
            m_GenPowerUp = false;
        }

    }

    private IEnumerator SpawnLeftStep()
    {
        int intervalNum;
        int special_num;
        while (!m_UI_Manager.isGameOver)
        {
            // Spwan Lfet Step
            if (startHeight_Left - cameraHeight < 8)
            {
                intervalNum = Random.Range(2, 5);
                for (int i = 0; i < intervalNum; i++)
                {
                    startHeight_Left += Random.Range(4f, 9f);
                    GenerateRandomStep(false, normalStep, startHeight_Left);
                }

                // Spwan Special Step 

                for (int i = 0; i < Random.Range(0, 3); i++)
                {
                    special_num = Random.Range(0, m_SpecialStep.Count);
                    startHeight_Left += Random.Range(4f, 9f);
                    GenerateRandomStep(false, m_SpecialStep[special_num], startHeight_Left);
                }
            }
            yield return null;
        }
    }

    private IEnumerator SpawnRightStep()
    {
        int intervalNum;
        int special_num;
        while (!m_UI_Manager.isGameOver)
        {
            // Spawn Right Step
            if (startHeight_Right - cameraHeight < 8)
            {
                intervalNum = Random.Range(2, 5);
                for (int i = 0; i < intervalNum; i++)
                {
                    startHeight_Right += Random.Range(4f, 9f);
                    GenerateRandomStep(true, normalStep, startHeight_Right);
                }

                // Spwan Special Step 
                for (int i = 0; i < Random.Range(0, 3); i++)
                {
                    special_num = Random.Range(0, m_SpecialStep.Count);
                    startHeight_Right += Random.Range(4f, 9f);
                    GenerateRandomStep(true, m_SpecialStep[special_num], startHeight_Right);
                }
            }
            yield return null;
        }
    }
}
