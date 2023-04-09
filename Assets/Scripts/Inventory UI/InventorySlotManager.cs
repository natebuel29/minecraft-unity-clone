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

            //Check to see if item is currently in toolbar
            foreach (InventoryItemSlot itemSlot in inventoryItemSlots)
            {
                if (itemSlot.isSlotTaken)
                {
                    //if item name in current item slot matches name of new item AND itemCount is less than max item count, then it can be added
                    //to this slot
                    if (itemSlot.slotItem.itemName == item.itemName && itemSlot.slotItemCount < itemSlot.slotItem.stackedInventoryCount)
                    {
                        inToolbar = true;
                        break;
                    }
                }
                i++;
            }

            if (inToolbar)
            {
                //incrememnt item count if in toolbar
                InventoryItemSlot itemSlotToUpdate = inventoryItemSlots[i];
                itemSlotToUpdate.AddItem();
            }
            else
            {
                //find first open slot and SetItem of that slot
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