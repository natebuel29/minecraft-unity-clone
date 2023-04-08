using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NB
{
    public class InputHandler : MonoBehaviour
    {
        public InputActionAsset actions;

        private InputAction movementAction;
        private InputAction mouseX;
        private InputAction mouseY;
        private InputAction mineButton;
        PlayerManager playerManager;

        [Header("Player Inputs")]
        public Vector2 moveVector;
        public Vector2 mouseInput;
        public float horizontal;
        public float vertical;

        public bool jump_flag;
        public bool mine_flag;
        public bool lockMouse_flag;

        private void Awake()
        {
            //find the Movement action and keep reference to it for use in update
            movementAction = actions.FindActionMap("PlayerMovement").FindAction("Movement");
            mouseX = actions.FindActionMap("PlayerMovement").FindAction("MouseX");
            mouseY = actions.FindActionMap("PlayerMovement").FindAction("MouseY");


            mineButton = actions.FindActionMap("PlayerActions").FindAction("Mine");

            // for the "jump" action, we add a callback method for when it is performed
            actions.FindActionMap("PlayerActions").FindAction("Jump").performed += OnJump;
            actions.FindActionMap("PlayerActions").FindAction("LockOrUnlockMouse").performed += OnLockUnlockMouse;
            actions.FindActionMap("PlayerActions").FindAction("PlaceBlock").performed += OnPlaceBlock;
            playerManager = GetComponent<PlayerManager>();
        }        // Update is called once per frame
        void Update()
        {
            HandleMovementInput();
            mouseInput.x = mouseX.ReadValue<float>();
            mouseInput.y = mouseY.ReadValue<float>();
            mine_flag = Mouse.current.leftButton.isPressed;
        }

        private void HandleMovementInput()
        {
            // our update loop polls the "move" action value each frame
            moveVector = movementAction.ReadValue<Vector2>();
            vertical = moveVector.x;
            horizontal = moveVector.y;
        }

        private void OnJump(InputAction.CallbackContext context)
        {
            jump_flag = true;
        }

        private void OnLockUnlockMouse(InputAction.CallbackContext context)
        {
            lockMouse_flag = !lockMouse_flag;
            playerManager.ShouldLockMouse(lockMouse_flag);
        }

        private void OnPlaceBlock(InputAction.CallbackContext context)
        {
            playerManager.HandlePlaceBlock();
        }

        void OnEnable()
        {
            actions.FindActionMap("PlayerMovement").Enable();
            actions.FindActionMap("PlayerActions").Enable();

        }
        void OnDisable()
        {
            actions.FindActionMap("PlayerMovement").Disable();
            actions.FindActionMap("PlayerActions").Disable();
        }
    }
}