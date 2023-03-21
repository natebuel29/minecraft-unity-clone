using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NB
{
    public class PlayerManager : MonoBehaviour
    {
        PlayerLocomotion playerLocomotion;
        InputHandler inputHandler;
        UIManager uiManager;
        public Camera mainCamera;


        private void Awake()
        {
            playerLocomotion = GetComponent<PlayerLocomotion>();
            inputHandler = GetComponent<InputHandler>();
            uiManager = FindObjectOfType<UIManager>();
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            inputHandler.lockMouse_flag = true;
        }

        void Update()
        {
            float delta = Time.deltaTime;
            playerLocomotion.HandleRotation(delta);
            HandleRaycast();
        }

        void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;
            playerLocomotion.HandleMovement(delta);
        }

        void HandleRaycast()
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(ray, out RaycastHit hit, 5))
            {
                uiManager.SetCrossHairColor(Color.red);
            }
            else
            {
                uiManager.SetCrossHairColor(Color.white);
            }
        }

        internal void ShouldLockMouse(bool lockMouse_flag)
        {
            if (lockMouse_flag)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}