using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NB
{
    public class PlayerLocomotion : MonoBehaviour
    {
        Rigidbody rb;
        InputHandler inputHandler;
        public Camera mainCam;

        [SerializeField]
        float moveSpeed = 25;
        [SerializeField]
        float xSensitivity = 20;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            inputHandler = GetComponent<InputHandler>();
        }

        private void Update()
        {
            HandleRotation();

        }

        private void FixedUpdate()
        {
            MovePlayer();
        }

        private void MovePlayer()
        {
            float vertical = inputHandler.vertical;
            float horizontal = inputHandler.horizontal;

            Vector3 movement = new Vector3(vertical, 0, horizontal) * moveSpeed;

            rb.velocity = movement;
        }

        private void HandleRotation()
        {

            //TODO: Calculcate target rotation from mouse input
            // rotate player on y axis
            //rotate camera on x axis
            //Clamp Y rotation imbetween a max and min look angle
            Vector3 targetDirectionX = new Vector3(0, inputHandler.mouseInput.x, 0);

            Quaternion targetRotation = Quaternion.Euler(targetDirectionX);
            transform.Rotate(transform.up, inputHandler.mouseInput.x * xSensitivity * Time.deltaTime);


            // Vector3 targetDirectionY = new Vector3(inputHandler.mousePosition.y, 0, 0);

            // targetRotation = Quaternion.Euler(targetDirectionY);

            // mainCam.transform.rotation = Quaternion.Slerp(mainCam.transform.rotation, targetRotation, Time.deltaTime);

        }
    }
}
