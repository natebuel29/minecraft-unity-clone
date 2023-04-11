using System;
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

        public InventoryItemSlot[] inventoryItemSlots;
        public int selectedIndex = -99;

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
                        if (IsInventoryEmpty())
                        {
                            itemSlot.SetOutlineColor(Color.cyan);
                            selectedIndex = 0;
                        }
                        itemSlot.SetItem(item);
                        break;
                    }
                }
            }
        }

        public Item RemoveItemFromSelected()
        {
            if (IsInventoryEmpty())
            {
                return null;
            }
            else
            {
                InventoryItemSlot currentSlot = inventoryItemSlots[selectedIndex];
                Item currentItem = currentSlot.slotItem;
                currentSlot.RemoveItem();
                if (!currentSlot.isSlotTaken)
                {
                    SelectFirstTakenSlot();
                    currentSlot.SetOutlineColor(Color.gray);
                }
                return currentItem;
            }
        }

        private void SelectFirstTakenSlot()
        {
            for (int i = 0; i < inventoryItemSlots.Length; i++)
            {
                InventoryItemSlot currentSlot = inventoryItemSlots[i];
                if (currentSlot.isSlotTaken)
                {
                    currentSlot.SetOutlineColor(Color.cyan);
                    selectedIndex = i;
                    return;
                }
            }
        }

        public void SelectInventorySlot(int index)
        {
            InventoryItemSlot desiredSlot = inventoryItemSlots[index];
            if (desiredSlot.isSlotTaken)
            {
                InventoryItemSlot currentSelected = inventoryItemSlots[selectedIndex];
                currentSelected.SetOutlineColor(Color.gray);
                desiredSlot.SetOutlineColor(Color.cyan);
                selectedIndex = index;
            }
        }

        public bool IsInventoryEmpty()
        {
            foreach (InventoryItemSlot itemSlot in inventoryItemSlots)
            {
                if (itemSlot.isSlotTaken == true)
                {
                    return false;
                }
            }

            return true;
        }
    }
}