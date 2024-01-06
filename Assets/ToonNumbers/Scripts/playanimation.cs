using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToonNumbers
{

    public class playanimation : MonoBehaviour
    {
        public string anim;
        public bool delayed;

        void Start()
        {
            GetComponent<Animator>().Play(anim);
            if (delayed)
            {
                StartCoroutine("playanim", anim);
            }
        }

        IEnumerator playanim(string anim)
        {
            GetComponent<Animator>().speed = 0.65f;
            yield return new WaitForSeconds(Random.Range(0f, 2f));
            GetComponent<Animator>().speed = 1f;
            GetComponent<Animator>().Play(anim);
        }

        public void playtheanimation(string newanim)
        {
            anim = newanim;
            StartCoroutine("playanim", anim);
        }
    }
}

