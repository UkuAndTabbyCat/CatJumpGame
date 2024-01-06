using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToonNumbers
{
    public class NLController3Ddirections : MonoBehaviour
    {
        public GameObject Character;
        public float walkspeed;
        public float runspeed;
        public float jumpforce;
        public RuntimeAnimatorController REFanim;
        GameObject Model;
        GameObject Camera;
        GameObject Helper;
        GameObject Target;
        Transform trans;
        Rigidbody rigid;
        Animator anim;
        Vector3 MoveDir;
        float rightmove;
        float forwmove;
        float runing;
        bool jumping;
        bool grounded;
        Vector3 dirforw;
        float angleforward;
        float angleright;
        GameObject currentGroundObject;
        int randomN;


        void Start()
        {
            Camera = GameObject.FindWithTag("MainCamera");
            Helper = new GameObject("Helper");
            Target = new GameObject("Target");
            Model = Instantiate(Character, transform.position, transform.rotation, transform);
            Helper.transform.position = transform.position;
            Helper.transform.parent = transform;
            Target.transform.position = transform.position + new Vector3(0f, 1.25f, 0f);
            Target.transform.parent = transform;
            Camera.transform.position = transform.position + new Vector3(0f, 3f, -5f);
            Camera.transform.parent = Helper.transform;
            Camera.transform.LookAt(Target.transform);
            trans = GetComponent<Transform>();
            rigid = GetComponent<Rigidbody>();
            anim = Model.GetComponent<Animator>();
            anim.applyRootMotion = false;
            anim.runtimeAnimatorController = REFanim;
            InvokeRepeating("RandomN", 2f, 1f);
        }

        void Update()
        {
            GetInput();
            MoveChar();
            Checkground();
            anim.SetInteger("randomN", randomN);
        }

        void Checkground()
        {
            if (!jumping)
            {
                float highttest;
                if (grounded) highttest = 0.35f; else highttest = 0.2f;
                RaycastHit hit0;
                grounded = false;
                if (Physics.Raycast(transform.position + Vector3.up * 0.15f, -Vector3.up, out hit0))
                {
                    if (hit0.distance < highttest) grounded = true;
                }
                if (Physics.Raycast(transform.position + Vector3.up * 0.15f + dirforw * 0.15f, Vector3.down, out hit0))
                {
                    if (hit0.distance < highttest) grounded = true;
                }
                if (Physics.Raycast(transform.position + Vector3.up * 0.15f + dirforw * -0.15f, Vector3.down, out hit0))
                {
                    if (hit0.distance < highttest) grounded = true;
                }
                if (Physics.Raycast(transform.position + Vector3.up * 0.2f , Vector3.down, out hit0))
                {
                    if (hit0.distance < highttest) grounded = true;
                }
                if (Physics.Raycast(transform.position + Vector3.up * 0.2f , Vector3.down, out hit0))
                {
                    if (hit0.distance < highttest) grounded = true;
                }
            }
            else
            {
                grounded = false;
            }
            anim.SetBool("grounded", grounded);
        }

        private bool IsFloor(Vector3 v)
        {
            float angle = Vector3.Angle(Vector3.up, v);
            return angle < 45;
        }

        void GetInput()
        {
            //mouse
            Camera.transform.RotateAround(Target.transform.position, -Camera.transform.right , Input.GetAxis("Mouse Y"));
            Camera.transform.position += Camera.transform.forward * Input.GetAxis("Mouse ScrollWheel");
            Camera.transform.LookAt(Target.transform);
            //movement
            Helper.transform.Rotate(0f, Input.GetAxis("Mouse X"), 0f);
            if (Input.GetButtonDown("Jump") && grounded ) StartCoroutine("Jump");
            if (Input.GetKey("left shift")) runing = Mathf.Lerp(runing, runspeed, 0.05f);
            else runing = Mathf.Lerp(runing, 0, 0.25f);
            MoveDir = (Helper.transform.forward * Input.GetAxis("Vertical") + Helper.transform.right * Input.GetAxis("Horizontal"));
            Setdir();
            Debug.DrawRay(transform.position + new Vector3(0f, 0.1f, 0f), MoveDir, Color.magenta);
            Debug.DrawRay(transform.position + new Vector3(0f, 0.1f, 0f), dirforw, Color.green);
        }

        void MoveChar()
        {
            if (MoveDir.magnitude !=0f) Model.transform.rotation = Quaternion.LookRotation(MoveDir); 
            if (grounded) rigid.velocity = dirforw * walkspeed * MoveDir.magnitude + dirforw * runing * runspeed * MoveDir.magnitude;
            anim.SetFloat("walk", MoveDir.magnitude);
            anim.SetFloat("run", runing);
        }

        void Setdir()
        {
            RaycastHit hit; RaycastHit hit1;
            Physics.Raycast(trans.position + new Vector3(0f, 0.2f, 0f), Vector3.down, out hit);
            Physics.Raycast(trans.position + new Vector3(0f, 0.2f, 0f) + MoveDir * 0.125f , Vector3.down, out hit1);
            dirforw = Vector3.Slerp(dirforw, -Vector3.Cross(hit.normal + hit1.normal, Model.transform.right), 0.25f).normalized;
            angleforward = Vector3.SignedAngle(Model.transform.forward, dirforw, Model.transform.right);
            angleright = Mathf.Lerp(angleright,Vector3.SignedAngle(Model.transform.forward, MoveDir, Vector3.up),0.25f);
            anim.SetFloat("turn", angleright);
            anim.SetFloat("angle", angleforward);
        }

        void RandomN()
        {
            randomN = Random.Range(0, 12);
        }



        IEnumerator Jump()
        {
            jumping = true;
            grounded = false;
            if (runing < 0.25) anim.Play("jump");
            else anim.Play("runjump");
            rigid.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
            yield return new WaitForSeconds(0.3f);
            jumping = false;
        }
    }
}