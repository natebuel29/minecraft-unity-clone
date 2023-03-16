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
        public LayerMask layerMask;
        public Camera mainCam;

        public Transform rayCastPointTransform;

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
        [SerializeField]
        float playerHeight = 2f;


        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            inputHandler = GetComponent<InputHandler>();
            playerManager = GetComponent<PlayerManager>();
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
            bool canJump = false;
            Vector3 movementVector = Vector3.zero;
            movementVector += transform.forward * inputHandler.horizontal * Time.fixedDeltaTime;
            movementVector += transform.right * inputHandler.vertical * Time.fixedDeltaTime;
            movementVector = movementVector.normalized * moveSpeed;

            if (Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.5f, layerMask))
            {
                Debug.Log("can jump");
                canJump = true;
            }
            else
            {
                Debug.Log("cant jump");
                canJump = false;
            }

            if (playerManager.jump_flag && canJump)
            {
                movementVector += Vector3.up * 5f;
                playerManager.jump_flag = false;
            }
            else
            {
                playerManager.jump_flag = false;
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
