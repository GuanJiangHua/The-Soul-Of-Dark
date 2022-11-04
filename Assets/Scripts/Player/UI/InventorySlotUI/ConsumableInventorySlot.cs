using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class ConsumableInventorySlot : MonoBehaviour
    {
        public Image icon;
        public ConsumableItem item;
        [Header("消耗品数量:")]
        public Text currentValueText;
        public Text maxValueText;

        UIManager uiManager;
        PlayerInventoryManager playerInventory;
        InventoryWindowManager inventoryWindowManager;

        private void Awake()
        {
            uiManager = FindObjectOfType<UIManager>();
            playerInventory = FindObjectOfType<PlayerInventoryManager>();
            inventoryWindowManager = GetComponentInParent<InventoryWindowManager>();
        }

        //添加此物品:
        public void AddItem(ConsumableItem newItem)
        {
            item = newItem;
            icon.sprite = item.itemIcon;
            icon.enabled = true;

            currentValueText.text = newItem.currentItemAmount.ToString();
            if (newItem.currentItemAmount <= 0)
                currentValueText.color = Color.red;
            else
                currentValueText.color = Color.white;
            currentValueText.enabled = true;
            maxValueText.text = newItem.maxItemAmount.ToString();

            gameObject.SetActive(true);
        }

        //删除此物品:
        public void ClearItem()
        {
            item = null;
            icon.sprite = null;
            icon.enabled = false;
            maxValueText.text = "-";
            maxValueText.enabled = false;
            currentValueText.text = "-";
            currentValueText.enabled = false;

            gameObject.SetActive(false);
        }

        //装备此物体:
        public void EquipThisItem()
        {
            int index = uiManager.consumableInventorySlotSelected;
            ConsumableItem[] consumables = playerInventory.consumableSlots;
            if(index != -1)
            {

                //如果该位置有装备其他消耗品,将该消耗品添加至"消耗品库存";
                if(consumables[index] != null)
                {
                    playerInventory.consumableInventory.Add(consumables[index]);
                }

                //将本物品添加到选定的消耗品装备槽:
                consumables[index] = item;
                //如果本物品非空,从消耗品库存中删除:
                if(item != null)
                {
                    playerInventory.consumableInventory.Remove(item);
                }

                //装备后,更新ui(装备窗口):
                uiManager.equipmentWindowUI.LoadConsumableOnEquipmentScreen(playerInventory);
            }
            playerInventory.currentConsumable = consumables[playerInventory.currentConsumableIndex];
            uiManager.consumableInventorySlotSelected = -1;
        }

        //鼠标进入时:[背包窗口中调用]
        public void MouseEntry()
        {
            //消耗品的id是:[1]
            uiManager.itemPropertiesWindowManager.UpdateItemPropertiesWindow(item, 1);
        }
        //鼠标移出时:[背包窗口中调用]
        public void MouseOut()
        {
            //消耗品的id是:[1]
            uiManager.itemPropertiesWindowManager.UpdateItemPropertiesWindow(null, 1);
        }
    }
}