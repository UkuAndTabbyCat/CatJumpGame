using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace ToonNumbers
{
    public class FloatingTexts : MonoBehaviour
    {
        public GameObject[] Elems;
        GameObject Obj1;
        float Px;

        void Start()
        {
            Obj1 = Instantiate(Elems[0], transform.position + Vector3.up * (Px + 2f), transform.rotation, transform);
            Px = 0f;
        }

        void Update()
        {
            Obj1.transform.position = new Vector3(0f, Px + 2f, 0f);
        }

        public void Press()
        {
            StopAllCoroutines();
            StartCoroutine(change(Elems[0]));
        }
        public void Guess()
        {
            StopAllCoroutines();
            StartCoroutine(change(Elems[1]));
        }
        public void GuessMore()
        {
            StopAllCoroutines();
            StartCoroutine(change(Elems[2]));
        }
        public void Wrong()
        {
            StopAllCoroutines();
            StartCoroutine(change(Elems[4]));
        }
        public void Correct()
        {
            StopAllCoroutines();
            StartCoroutine(change(Elems[3]));
        }

        IEnumerator change(GameObject newGO)
        {
            while (Px < 6)
            {
                Px += 0.5f;
                yield return null;
            }
            Destroy(transform.GetChild(0).gameObject);
            Obj1 = Instantiate(newGO, transform.position + Vector3.up * (Px + 2f), transform.rotation, transform);
            while (Px > 0.5f)
            {
                Px -= 0.25f;
                yield return null;
            }
        }
    }
}
