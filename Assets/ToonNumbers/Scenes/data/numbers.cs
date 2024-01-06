using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToonNumbers
{
    [SelectionBase]

    public class numbers : MonoBehaviour
    {

        public GameObject[] characters;
        GameObject Char;
        Animator anim;
        Camera Cam;
        int randomN;
        int Nfase;
        float angle;

        void Start()
        {
            Nfase = 0;
            Vector3 auxV3 = transform.position.normalized;
            InvokeRepeating("RandomN", 0f, 2f);
        }

        void Update()
        {
            if (Nfase == 1) //runing
            {
                anim.SetInteger("randomN", randomN);
                if (((Char.transform.position - transform.position).magnitude) < 1.5f * transform.localScale.magnitude)
                {
                    if (randomN < 3) anim.CrossFade("arrive1", 0.1f);
                    if (randomN > 2 && randomN < 5) anim.CrossFade("arrive2", 0.1f);
                    if (randomN > 4 && randomN < 7) anim.CrossFade("arrive3", 0.1f);
                    if (randomN > 6) anim.CrossFade("arrive4", 0.1f);
                    Nfase++;
                    StartCoroutine(Delayed(1f));
                    angle = Vector3.SignedAngle(Char.transform.forward, -Cam.transform.forward, Vector3.up);
                    if (angle < 10f && angle > -10f) anim.SetInteger("turnN", 0);
                    if (angle > 10f) anim.SetInteger("turnN", 1);
                    if (angle < -10f) anim.SetInteger("turnN", 2);
                }
            }
            if (Nfase == 3) //turning
            {
                Nfase++;
                StartCoroutine("Orientate");
            }

            if (Nfase == 4) //waiting
            {
                anim.SetInteger("randomN", randomN);
            }
        }

        public void letsgo(int number)
        {
            if (transform.childCount > 0) for (int forAUX = 0; forAUX < transform.childCount; forAUX++) Destroy(transform.GetChild(forAUX).gameObject);
            Nfase = 1;
            Vector3 auxV3 = transform.right - transform.forward;
            auxV3 = Quaternion.AngleAxis(Random.Range(0f, -270f), Vector3.up) * auxV3;
            Char = Instantiate(characters[number + (10 * (Random.Range(0, 3)))], transform.position, transform.rotation, transform);
            Char.transform.position = transform.position + auxV3 * Random.Range(7f, 8f);
            Char.transform.LookAt(transform.position);
            anim = Char.GetComponent<Animator>();
            anim.SetInteger("randomN", Random.Range(0, 9));
            Cam = Camera.main;
        }

        void RandomN()
        {
            randomN = Random.Range(1, 10);
        }

        public void resetN()
        {
            StopAllCoroutines();
            if (transform.childCount > 0) for (int forAUX = 0; forAUX < transform.childCount; forAUX++) Destroy(transform.GetChild(forAUX).gameObject);
            Nfase = 0;
        }

        public void Tension()
        {
            anim.CrossFade("tension", 0.1f);
        }
        public void Victory()
        {
            StartCoroutine("DelayAnim", "victory");
        }
        public void Fail()
        {
            StartCoroutine("DelayAnim", "defeat");
        }



        IEnumerator DelayAnim(string animation)
        {
            //yield return new WaitForSeconds(0.5f);
            yield return new WaitForSeconds(Random.Range(0.4f, 0.6f));
            anim.Play(animation);

        }
        IEnumerator Delayed(float delay)
        {
            yield return new WaitForSeconds(delay);
            Nfase++;
        }

        IEnumerator Orientate()
        {
            yield return new WaitForSeconds(0.05f);

            float angle = Vector3.SignedAngle(Char.transform.forward, Vector3.ProjectOnPlane(-Cam.transform.forward, Vector3.up), Vector3.up);
            while (angle > 5 || angle < -5)
            {
                //Char.transform.Rotate(Vector3.up * (angle * 0.125f));
                Char.transform.Rotate(Vector3.up * (angle * 0.25f) * Time.deltaTime * 30f);

                Char.transform.position = Vector3.Lerp(Char.transform.position, transform.position, 0.15f);
                angle = Vector3.SignedAngle(Char.transform.forward, Vector3.ProjectOnPlane(-Cam.transform.forward, Vector3.up), Vector3.up);
                yield return null;
            }
        }
    }
}
