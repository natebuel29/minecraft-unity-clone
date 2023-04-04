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
        public Transform direction;
        public Transform axeRotationPoint;
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

        public void HandleMovement(float delta)
        {
            float vertical = inputHandler.vertical;
            float horizontal = inputHandler.horizontal;
            bool canJump = false;
            Vector3 movementVector = Vector3.zero;
            movementVector += direction.transform.forward * inputHandler.horizontal * delta;
            movementVector += direction.transform.right * inputHandler.vertical * delta;
            movementVector = movementVector.normalized * moveSpeed;

            if (Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.5f, layerMask))
            {
                canJump = true;
            }
            else
            {
                canJump = false;
            }

            if (inputHandler.jump_flag && canJump)
            {
                movementVector += Vector3.up * 5f;
                inputHandler.jump_flag = false;
            }
            else
            {
                inputHandler.jump_flag = false;
                movementVector.y = rb.velocity.y;
            }

            rb.velocity = movementVector;
        }

        public void HandleRotation(float delta)
        {
            float mouseX = inputHandler.mouseInput.x * delta * xSensitivity;
            float mouseY = inputHandler.mouseInput.y * delta * ySensitivity;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -xRotationClamp, xRotationClamp);

            yRotation += mouseX;
            mainCam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            direction.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}
