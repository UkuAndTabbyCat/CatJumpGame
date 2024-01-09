using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    /*
     每隔10m生成一组随机障碍
     */

    [SerializeField] GameObject normalStep;
    [SerializeField] GameObject moveStep;
    [SerializeField] GameObject onlyOnceStep;
    [SerializeField] GameObject hurtStep;
    [SerializeField] GameObject bouncyStep;
    [SerializeField] GameObject twinkleStep;

    [SerializeField] GameObject m_Bubble;
    private float cameraHeight;
    private float startHeight = 0;
    private float xBound = 30f;

    private float lastStepLoc_X;

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
            if (startStep_y > startHeight)
            {
                break;
            }
        }
        lastStepLoc_X = step.gameObject.transform.position.x;
        StartCoroutine("GenerateBubble");
    }

    // Update is called once per frame
    void Update()
    {
        cameraHeight = Camera.main.transform.position.y;
        if (startHeight - cameraHeight < 8)
        {
            int intervalNum = Random.Range(3, 5);
            for (int i = 0; i < intervalNum; i++)
            {
                startHeight += Random.Range(4f, 9f);
                GenerateRandomStep(lastStepLoc_X, normalStep, startHeight);
            }
            startHeight += Random.Range(4f, 9f);
            GenerateRandomStep(lastStepLoc_X, moveStep, startHeight);
            startHeight += Random.Range(4f, 9f);
            GenerateRandomStep(lastStepLoc_X, onlyOnceStep, startHeight);
            startHeight += Random.Range(4f, 9f);
            GenerateRandomStep(lastStepLoc_X, twinkleStep, startHeight);
            startHeight += Random.Range(4f, 9f);
            GenerateRandomStep(lastStepLoc_X, hurtStep, startHeight);
            startHeight += Random.Range(4f, 9f);
            GenerateRandomStep(lastStepLoc_X, bouncyStep, startHeight);

        }
    }

    private void GenerateRandomStep(float lastStepLoc_X, GameObject step, float loc_y)
    {
        float random_x = lastStepLoc_X + Random.Range(-10f, 10f);
        if (random_x < -xBound)
        {
            random_x = -2 * xBound - random_x;
        }
        else if (random_x > xBound)
        {
            random_x = 2 * xBound - random_x;
        }
        Vector3 loc = new Vector3(random_x, loc_y, 0);
        this.lastStepLoc_X = random_x;

        // generate random step length
        float scale_x = Random.Range(2f, 6f);
        step.transform.localScale = new Vector3(scale_x, step.transform.localScale.y, step.transform.localScale.z);

        Instantiate(step, loc, step.transform.rotation);
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
}
