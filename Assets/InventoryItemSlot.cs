using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NB
{
    public class InventoryItemSlot : MonoBehaviour
    {
        //bool isSlotOpen;
        //Item slotItem
        //int slotItemCount
        //set UI image 'Item Icon' to slotItem png and enable
        //

        public bool isSlotTaken;
        public Item slotItem;
        public int slotItemCount;
        public Image itemImage;


        private void Awake()
        {
            isSlotTaken = false;
            slotItem = null;
            slotItemCount = 0;
        }

        public void SetItem(Item item)
        {
            slotItem = item;
            isSlotTaken = true;
            slotItemCount = 1;
            SetItemImage(item);
        }

        public void SetItemImage(Item item)
        {
            itemImage.enabled = true;
            itemImage.sprite = item.itemIcon;
        }
    }
}