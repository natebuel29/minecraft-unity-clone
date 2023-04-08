using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NB
{
    public class Pickaxe : MonoBehaviour
    {
        PlayerManager playerManager;
        private void Awake()
        {
            playerManager = GetComponentInParent<PlayerManager>();
        }
        public void CallMineBlock()
        {
            playerManager.HandleMineBlock();
        }
    }
}