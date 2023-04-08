using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NB
{
    public class InventorySlotManager : MonoBehaviour
    {
        //TODO:
        // List of all inventory item slots
        // Add item to inventory:
        // Check if item is in already in toolbar
        //if it is, add item to that slot (AddItem)
        // if not, find first open slot.
        // set item to inventory slot (SetItem)
        //Remove item
        //remove item from highlighted inventory slot 
        // must contain an item
        //
        public InventoryItemSlot[] inventoryItemSlots;

        private void Awake()
        {
            inventoryItemSlots = GetComponentsInChildren<InventoryItemSlot>();
        }

        public void AddItemToInventorySlot(Item item)
        {
            bool inToolbar = false;
            int i = 0;

            while (!inToolbar && i < inventoryItemSlots.Length)
            {
                InventoryItemSlot tempSlot = inventoryItemSlots[i];

                if (tempSlot.isSlotTaken)
                {
                    if (tempSlot.slotItem.itemName == item.itemName) // add this in broke it -- why?!?!? && tempSlot.slotItemCount < tempSlot.slotItem.stackedInventoryCount)
                    {
                        inToolbar = true;
                    }
                }
                else
                {
                    i++;
                }
            }

            if (inToolbar)
            {
                InventoryItemSlot itemSlotToUpdate = inventoryItemSlots[i];
                itemSlotToUpdate.AddItem();
            }
            else
            {
                foreach (InventoryItemSlot itemSlot in inventoryItemSlots)
                {
                    if (itemSlot.isSlotTaken == false)
                    {
                        itemSlot.SetItem(item);
                        break;
                    }
                }
            }
        }
    }
}