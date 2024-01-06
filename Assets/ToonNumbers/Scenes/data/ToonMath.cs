using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToonNumbers
{
    public class ToonMath : MonoBehaviour
    {
        public GameObject Numbers;
        public GameObject Signs;
        GameObject[] operation;
        int fase;
        int[] values;
        int[] operators;
        float result;
        int guess;
        public GameObject Texts;

        void Start()
        {
            result = 1000f;
            fase = 0;
            Texts = Instantiate(Texts, transform.position, transform.rotation, transform);
            operation = new GameObject[8];
            for (int forAUX = 0; forAUX < 8; forAUX += 2) operation[forAUX] = Instantiate(Numbers, transform.position - Vector3.right * ((forAUX - 4f)), transform.rotation, transform);
            operation[7] = Instantiate(Numbers, transform.position + Vector3.right * (-3f), transform.rotation, transform);
            for (int forAUX = 1; forAUX < 7; forAUX += 2) operation[forAUX] = Instantiate(Signs, transform.position - Vector3.right * ((forAUX - 4f)) + transform.forward * -0.25f, transform.rotation, transform);
        }


        void Update()
        {
            if (Input.anyKey && fase == 0)
            {
                while (result < 0 || result > 99 || result != (int)Mathf.Floor(result))
                {
                    result = 0;
                    values = new int[3] { Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10) };
                    operators = new int[3] { Random.Range(0, 4), Random.Range(0, 4), 4 };
                    if (operators[0] == 3 && values[1] == 0) values[1]++;
                    if (operators[0] == 3 && values[0] == 0) values[0]++;
                    if (operators[1] == 3 && values[2] == 0) values[2]++;
                    if (operators[1] == 3 && values[1] == 0) values[1]++;

                    if (operators[0] == 0) result = values[0] + values[1];
                    if (operators[0] == 1) result = values[0] - values[1];
                    if (operators[0] == 2) result = values[0] * values[1];
                    if (operators[0] == 3) result = (float)values[0] / (float)values[1];

                    if (operators[1] == 0) result = result + values[2];
                    if (operators[1] == 1) result = result - values[2];
                    if (operators[1] == 2) result = result * values[2];
                    if (operators[1] == 3) result = result / (float)values[2];
                }
                operation[0].GetComponent<numbers>().letsgo(values[0]);
                operation[1].GetComponent<signs>().letsgo(operators[0]);
                operation[2].GetComponent<numbers>().letsgo(values[1]);
                operation[3].GetComponent<signs>().letsgo(operators[1]);
                operation[4].GetComponent<numbers>().letsgo(values[2]);
                operation[5].GetComponent<signs>().letsgo(operators[2]);
                fase++;
                StartCoroutine("Delayed", 2f);
                Texts.GetComponent<FloatingTexts>().Guess();
            }

            if (fase == 2)
            {
                if (result < 10)
                {
                    if (Input.GetKeyDown("0")) fase3(0);
                    if (Input.GetKeyDown("1")) fase3(1);
                    if (Input.GetKeyDown("2")) fase3(2);
                    if (Input.GetKeyDown("3")) fase3(3);
                    if (Input.GetKeyDown("4")) fase3(4);
                    if (Input.GetKeyDown("5")) fase3(5);
                    if (Input.GetKeyDown("6")) fase3(6);
                    if (Input.GetKeyDown("7")) fase3(7);
                    if (Input.GetKeyDown("8")) fase3(8);
                    if (Input.GetKeyDown("9")) fase3(9);

                }
                if (result > 9)
                {
                    if (Input.GetKeyDown("0")) fase3b(0);
                    if (Input.GetKeyDown("1")) fase3b(1);
                    if (Input.GetKeyDown("2")) fase3b(2);
                    if (Input.GetKeyDown("3")) fase3b(3);
                    if (Input.GetKeyDown("4")) fase3b(4);
                    if (Input.GetKeyDown("5")) fase3b(5);
                    if (Input.GetKeyDown("6")) fase3b(6);
                    if (Input.GetKeyDown("7")) fase3b(7);
                    if (Input.GetKeyDown("8")) fase3b(8);
                    if (Input.GetKeyDown("9")) fase3b(9);
                }
            }

            if (fase == 4)
            {
                if (Input.GetKeyDown("0")) { operation[7].GetComponent<numbers>().letsgo(0); fase++; StartCoroutine("Delayed", 2f); guess += 0; }
                if (Input.GetKeyDown("1")) { operation[7].GetComponent<numbers>().letsgo(1); fase++; StartCoroutine("Delayed", 2f); guess += 1; }
                if (Input.GetKeyDown("2")) { operation[7].GetComponent<numbers>().letsgo(2); fase++; StartCoroutine("Delayed", 2f); guess += 2; }
                if (Input.GetKeyDown("3")) { operation[7].GetComponent<numbers>().letsgo(3); fase++; StartCoroutine("Delayed", 2f); guess += 3; }
                if (Input.GetKeyDown("4")) { operation[7].GetComponent<numbers>().letsgo(4); fase++; StartCoroutine("Delayed", 2f); guess += 4; }
                if (Input.GetKeyDown("5")) { operation[7].GetComponent<numbers>().letsgo(5); fase++; StartCoroutine("Delayed", 2f); guess += 5; }
                if (Input.GetKeyDown("6")) { operation[7].GetComponent<numbers>().letsgo(6); fase++; StartCoroutine("Delayed", 2f); guess += 6; }
                if (Input.GetKeyDown("7")) { operation[7].GetComponent<numbers>().letsgo(7); fase++; StartCoroutine("Delayed", 2f); guess += 7; }
                if (Input.GetKeyDown("8")) { operation[7].GetComponent<numbers>().letsgo(8); fase++; StartCoroutine("Delayed", 2f); guess += 8; }
                if (Input.GetKeyDown("9")) { operation[7].GetComponent<numbers>().letsgo(9); fase++; StartCoroutine("Delayed", 2f); guess += 9; }
            }


            if (fase == 6)
            {
                if (guess == result)
                {
                    for (int forAUX = 0; forAUX < 7; forAUX += 2) operation[forAUX].GetComponent<numbers>().Victory();
                    Texts.GetComponent<FloatingTexts>().Correct();
                    if (result > 10) operation[7].GetComponent<numbers>().Victory();
                }
                else
                {
                    for (int forAUX = 0; forAUX < 7; forAUX += 2) operation[forAUX].GetComponent<numbers>().Fail();
                    Texts.GetComponent<FloatingTexts>().Wrong();
                    if (result > 10) operation[7].GetComponent<numbers>().Fail();
                }
                fase++;
                StartCoroutine("Delayed", 1f);
            }

            if (fase == 8)
            {
                if (Input.anyKey)
                {
                    result = 01000f;
                    fase = 0;
                    for (int forAUX = 0; forAUX < 5; forAUX += 2) operation[forAUX].GetComponent<numbers>().resetN();
                    operation[6].GetComponent<numbers>().resetN();
                    operation[7].GetComponent<numbers>().resetN();
                    Texts.GetComponent<FloatingTexts>().Guess();
                }
            }

            if (Input.GetKeyDown("x"))
            {
                StopAllCoroutines();
                result = 01000f;
                fase = 0;
                for (int forAUX = 0; forAUX < 7; forAUX += 2) operation[forAUX].GetComponent<numbers>().resetN();
                operation[6].GetComponent<numbers>().resetN();
                operation[7].GetComponent<numbers>().resetN();
                Texts.GetComponent<FloatingTexts>().Guess();
            }

        }





        void fase3(int temp)
        {
            guess = temp;
            operation[6].GetComponent<numbers>().letsgo(temp);
            for (int forAUX = 0; forAUX < 6; forAUX += 2) operation[forAUX].GetComponent<numbers>().Tension();
            fase = 5;
            StartCoroutine("Delayed", 2f);
        }
        void fase3b(int temp)
        {
            Texts.GetComponent<FloatingTexts>().GuessMore();
            guess = temp * 10;
            operation[6].GetComponent<numbers>().letsgo(temp);
            for (int forAUX = 0; forAUX < 6; forAUX += 2) operation[forAUX].GetComponent<numbers>().Tension();
            fase = 3;
            StartCoroutine("Delayed", 0.125f);
        }






        IEnumerator Delayed(float time)
        {
            yield return new WaitForSeconds(time);
            fase++;
        }
    }
}