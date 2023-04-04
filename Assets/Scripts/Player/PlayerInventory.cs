using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NB
{
    public class PlayerInventory : MonoBehaviour
    {
        int inventorySize = 20;
        public List<Block> playerInventory = new List<Block>(20);

        public void AddItem(Block block)
        {
            playerInventory.Add(block);
        }

        public bool canAddBlock()
        {
            return playerInventory.Count < inventorySize;
        }
    }
}