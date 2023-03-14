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

        [Header("Player Inputs")]
        public Vector2 moveVector;
        public Vector2 mouseInput;
        public float horizontal;
        public float vertical;


        private void Awake()
        {
            //find the Movement action and keep reference to it for use in update
            movementAction = actions.FindActionMap("PlayerMovement").FindAction("Movement");
            mouseX = actions.FindActionMap("PlayerMovement").FindAction("MouseX");
            mouseY = actions.FindActionMap("PlayerMovement").FindAction("MouseY");


            // for the "jump" action, we add a callback method for when it is performed
            // actions.FindActionMap("gameplay").FindAction("jump").performed += OnJump;
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            HandleMovementInput();

            mouseInput.x = mouseX.ReadValue<float>();
            mouseInput.y = mouseY.ReadValue<float>();

        }

        private void HandleMovementInput()
        {
            // our update loop polls the "move" action value each frame
            moveVector = movementAction.ReadValue<Vector2>();
            vertical = moveVector.x;
            horizontal = moveVector.y;
        }



        private void OnJump(InputAction.CallbackContext context) { }

        void OnEnable()
        {
            actions.FindActionMap("PlayerMovement").Enable();
        }
        void OnDisable()
        {
            actions.FindActionMap("PlayerMovement").Disable();
        }
    }
}