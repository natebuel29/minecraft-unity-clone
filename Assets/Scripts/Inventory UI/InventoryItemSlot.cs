using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NB
{
    public class InventoryItemSlot : MonoBehaviour
    {
        public bool isSlotTaken;
        public Item slotItem;
        public int slotItemCount;
        public Image itemImage;
        public Text itemCountText;


        private void Awake()
        {
            isSlotTaken = false;
            slotItem = null;
            slotItemCount = 0;
            itemCountText.enabled = false;
        }

        public void SetItem(Item item)
        {
            slotItem = item;
            isSlotTaken = true;
            slotItemCount = 1;
            SetItemImage(item);
            itemCountText.enabled = true;
            SetItemCountText();
        }

        public void AddItem()
        {
            slotItemCount++;
            SetItemCountText();
        }

        public void RemoveItem()
        {
            slotItemCount--;

            if (slotItemCount > 0)
            {
                SetItemCountText();
            }
            else if (slotItemCount == 0)
            {
                slotItem = null;
                isSlotTaken = false;
                itemImage.enabled = false;
                SetItemCountText();
                itemCountText.enabled = false;
            }
            else
            {
                Debug.Log("WE SHOULD NEVER BE HERE");
            }
        }

        public void SetItemImage(Item item)
        {
            itemImage.enabled = true;
            itemImage.sprite = item.itemIcon;
        }

        public void SetItemCountText()
        {
            itemCountText.text = this.slotItemCount.ToString();
        }

    }
}