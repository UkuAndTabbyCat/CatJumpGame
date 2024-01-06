using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ToonNumbers
{
    [SelectionBase]

    public class signs : MonoBehaviour
    {
        public GameObject[] GOsigns;
        public int sign;
        GameObject Char;
        Transform trans;
        public float height;
        public float amplitude;
        public float speed;
        float now;
        float land;
        bool running;

        void Start()
        {
            running = false;
        }

        void Update()
        {
            if (running)
            {
                now += Time.deltaTime;
                trans.localScale = new Vector3(1f, 1f, 1f) * ((Mathf.Sin(now * speed * 0.25f) * 0.25f) + 0.9f);
                trans.position = transform.position + Vector3.up * (height + amplitude * Mathf.Sin(now * speed) + land);
            }
        }

        public void resetN()
        {
            if (transform.childCount > 0) for (int forAUX = 0; forAUX < transform.childCount; forAUX++) Destroy(transform.GetChild(forAUX).gameObject);
            running = false;
        }

        public void OUT()
        {
            StartCoroutine("OUTq");
        }

        public void letsgo(int sign)
        {
            if (transform.childCount > 0) for (int forAUX = 0; forAUX < transform.childCount; forAUX++) Destroy(transform.GetChild(forAUX).gameObject);
            Char = Instantiate(GOsigns[sign], transform.position, transform.rotation, transform);
            trans = Char.GetComponent<Transform>();
            land = 8f + Random.Range(-6f, 6f);
            trans.position += Vector3.up * land;
            now = 0f + Random.Range(0f, 4f);
            StartCoroutine("Landing");
            running = true;
        }

        IEnumerator Landing()
        {
            while (land > 0f)
            {
                land = Mathf.Lerp(land, 0f, 0.125f);
                yield return null;
            }
        }

        IEnumerator OUTq()
        {
            yield return new WaitForSeconds(1.25f);
            while (height < 100f)
            {
                height = Mathf.Lerp(height, 101f, 0.0025f);
                yield return null;
            }
        }
    }
}