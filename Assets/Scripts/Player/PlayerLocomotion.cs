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
        PlayerManager playerManager;
        public Camera mainCam;

        [SerializeField]
        float moveSpeed = 25;
        [SerializeField]
        float xSensitivity = 20;
        [SerializeField]
        float ySensitivity = 20;
        [SerializeField]
        float yRotation;
        [SerializeField]
        float xRotation;
        [SerializeField]
        float xRotationClamp = 40;


        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            inputHandler = GetComponent<InputHandler>();
            playerManager = GetComponent<PlayerManager>();
        }

        private void Update()
        {
            // HandleRotation();
        }

        private void FixedUpdate()
        {
            MovePlayer();
        }

        private void MovePlayer()
        {
            float vertical = inputHandler.vertical;
            float horizontal = inputHandler.horizontal;
            Vector3 movementVector = Vector3.zero;
            movementVector += transform.forward * inputHandler.horizontal;
            movementVector += transform.right * inputHandler.vertical;
            movementVector = movementVector.normalized * moveSpeed;
            Debug.DrawLine(transform.position, transform.position + Vector3.down * 2, Color.red, 0.1f);
            if (playerManager.jump_flag)
            {
                movementVector += Vector3.up * 5f;
                playerManager.jump_flag = false;
            }
            else
            {
                movementVector.y = rb.velocity.y;
            }

            rb.velocity = movementVector;
        }

        private void HandleRotation()
        {
            float mouseX = inputHandler.mouseInput.x * Time.deltaTime * xSensitivity;
            float mouseY = inputHandler.mouseInput.y * Time.deltaTime * ySensitivity;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -xRotationClamp, xRotationClamp);

            yRotation += mouseX;

            transform.rotation = Quaternion.Euler(0, yRotation, 0);
            mainCam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        }
    }
}
