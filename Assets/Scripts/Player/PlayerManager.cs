using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NB
{
    public class PlayerManager : MonoBehaviour
    {
        PlayerLocomotion playerLocomotion;
        InputHandler inputHandler;

        private void Awake()
        {
            playerLocomotion = GetComponent<PlayerLocomotion>();
            inputHandler = GetComponent<InputHandler>();
        }

        void Update()
        {
            float delta = Time.deltaTime;
            playerLocomotion.HandleRotation(delta);
        }

        void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;
            playerLocomotion.HandleMovement(delta);
        }
    }
}