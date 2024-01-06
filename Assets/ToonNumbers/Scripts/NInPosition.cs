using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToonNumbers
{
    [SelectionBase]

    public class NInPosition : MonoBehaviour
    {

        public GameObject[] characters;
        public int number;
        public float Startdelay;
        public float distance;
        public bool Arms;
        public bool Legs;
        public bool staticidle;

        GameObject Char;
        Animator anim;
        Camera Cam;
        int randomN;
        int Nfase;
        float angle;


        void Start()
        {
            Nfase = 0;
            InvokeRepeating("RandomN", 0f, 2f);
            if (transform.childCount > 0) Destroy(transform.GetChild(0).gameObject);

            Vector3 auxV3 = (transform.right - transform.forward * 0.5f).normalized;
            if (number > 9) number = Random.Range(0, 10);
            auxV3 = Quaternion.AngleAxis(Random.Range(0f, -220f), Vector3.up) * auxV3;
            Char = Instantiate(characters[number + (10 * (Random.Range(0, 3)))], transform.position, transform.rotation, transform);
            Char.transform.position = transform.position + auxV3 * (distance + 5f);
            Char.transform.LookAt(transform.position);
            anim = Char.GetComponent<Animator>();
            anim.SetInteger("randomN", Random.Range(0, 9));
            anim.Play("idle");
            StartCoroutine(DelayedStart(Startdelay));
            if (!Arms) Char.transform.Find("ARMS").gameObject.SetActive(false);
            if (!Legs) Char.transform.Find("LEGS").gameObject.SetActive(false);
            Cam = Camera.main;
            if (staticidle) anim.SetBool("static", true);

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
                StartCoroutine("Orientate");
                Nfase++;
                if (staticidle) StartCoroutine("DelayedHide", true);

            }
            if (Nfase == 4) //waiting
            {
                anim.SetInteger("randomN", randomN);
            }
        }






        void RandomN()
        {
            randomN = Random.Range(1, 10);
        }



        IEnumerator DelayedHide(bool active)
        {
            yield return new WaitForSeconds(0.5f);
            Char.transform.Find("ARMS").gameObject.SetActive(!active);
            Char.transform.Find("LEGS").gameObject.SetActive(!active);
        }

        IEnumerator Delayed(float delay)
        {
            yield return new WaitForSeconds(delay);
            Nfase++;
        }
        IEnumerator DelayedStart(float delay)
        {
            yield return new WaitForSeconds(delay);
            anim.Play("start");
            Nfase++;
        }
        IEnumerator Orientate()
        {
            yield return new WaitForSeconds(0.05f);
            float thisangle = Vector3.SignedAngle(Char.transform.forward, Vector3.ProjectOnPlane(-Cam.transform.forward, Vector3.up), Vector3.up);
            while (thisangle > 5 || thisangle < -5)
            {
                Char.transform.Rotate(Vector3.up * (thisangle * 0.25f) * Time.deltaTime * 30f);
                Char.transform.position = Vector3.Lerp(Char.transform.position, transform.position, 0.15f);
                thisangle = Vector3.SignedAngle(Char.transform.forward, Vector3.ProjectOnPlane(-Cam.transform.forward, Vector3.up), Vector3.up);
                yield return null;
            }
        }
    }
}

