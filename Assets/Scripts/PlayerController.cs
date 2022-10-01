using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace ludumdare51
{
    public class PlayerController : MonoBehaviour
    {
        public float maxMoveSpeed = 10;
        public float maxSpinSpeed = 10;
        public float maxFallSpeed = 10;
        public float moveSpeed = 1000;
        public float rotateSpeed = 10;
        public GameObject humanoidChild;
        public TimeManager myTimeManager;


        private float speedScaleFactor = 1;

        private Vector2 direction;
        private Vector2 rotation;
        private Rigidbody rb;
        private Animator humanoidMeshAnimator;
        private bool isGrounded;

        private AudioSource audioSource;

        private bool playerEnabled;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            humanoidMeshAnimator = humanoidChild.GetComponent<Animator>();
            isGrounded = false;
            //humanoidMeshAnimator.SetBool("IsGrounded", false);

            audioSource = GetComponent<AudioSource>();


            myTimeManager.TimerFinished += OnTimerFinishedHandler;

            playerEnabled = true;
        }




        // Update is called once per frame
        void Update()
        {
            if (playerEnabled)
            {

                // Mostly fixed direction 
                Vector3 moveVector = (new Vector3(1, 0, 0) * direction.x * (moveSpeed * Time.deltaTime)) + (new Vector3(0, 0, 1) * direction.y * (moveSpeed * Time.deltaTime));
                //rb.MovePosition(transform.position + moveVector);

                rb.AddForce(moveVector, ForceMode.Acceleration);


                if (isGrounded)
                {
                }


                //if (moveVector != new Vector3(0, 0, 0))
                //{
                //    humanoidMeshAnimator.SetBool("IsMoving", true);
                //}
                //else
                //{
                //    humanoidMeshAnimator.SetBool("IsMoving", false);
                //}


                // Clamp max speeds except on falling axis
                var v = rb.velocity;
                var yd = v.y;
                v.y = 0f;
                v = Vector3.ClampMagnitude(v, maxMoveSpeed);
                v.y = Vector3.ClampMagnitude(new Vector3(0, yd, 0), maxFallSpeed * speedScaleFactor).y;
                rb.velocity = v;


                var va = rb.angularVelocity;
                va = Vector3.ClampMagnitude(va, maxSpinSpeed * speedScaleFactor);
                rb.angularVelocity = va;



            }
            else
            {
                // Play should be stopped now.
                rb.isKinematic = true;
            }
        }



        public void Move(InputAction.CallbackContext context)
        {
            direction = context.ReadValue<Vector2>();
            //humanoidMeshAnimator.SetFloat("direction", -direction.x);
            //Debug.Log("Direction " + direction);
        }
        public void Rotate(InputAction.CallbackContext context)
        {
            rotation = context.ReadValue<Vector2>();
            //Debug.Log("rotation " + rotation);
        }


        private void OnTimerFinishedHandler()
        {
            Debug.Log("Player Heard timer end!");
        }

    }
}