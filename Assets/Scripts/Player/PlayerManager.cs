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
        AnimationHandler animationHandler;
        PlayerInventory playerInventory;

        public GameObject block;


        private void Awake()
        {
            playerLocomotion = GetComponent<PlayerLocomotion>();
            inputHandler = GetComponent<InputHandler>();
            uiManager = FindObjectOfType<UIManager>();
            animationHandler = GetComponent<AnimationHandler>();
            playerInventory = GetComponent<PlayerInventory>();
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
            HandleMineBlock(delta);
        }

        public void PlaceBlock()
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(ray, out RaycastHit hit, 5))
            {
                Vector3 pos = hit.point;
                pos += hit.normal * 0.1f;
                Vector3 gridPosition = new Vector3(
                    Mathf.RoundToInt(pos.x),
                    Mathf.RoundToInt(pos.y),
                    Mathf.RoundToInt(pos.z)
                );
                gridPosition.y += 0.3f;
                Instantiate(block, gridPosition, Quaternion.identity);
            }
        }

        public void HandleMineBlock(float delta)
        {
            animationHandler.PlayMineAnimation(inputHandler.mine_flag);
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(ray, out RaycastHit hit, 5) && inputHandler.mine_flag)
            {
                if (hit.collider.tag == "Block")
                {
                    if (playerInventory.canAddBlock())
                    {
                        GameObject hitGameObject = hit.collider.gameObject;
                        Block hitBlock = hitGameObject.GetComponent<BlockManager>().block;
                        playerInventory.AddItem(hitBlock);
                        Destroy(hitGameObject);
                    }
                }
            }
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

        public void ShouldLockMouse(bool lockMouse_flag)
        {
            if (lockMouse_flag)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}