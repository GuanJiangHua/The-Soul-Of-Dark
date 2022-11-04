using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class HandEquipmentInventorySlotUI : MonoBehaviour
    {
        public Image icon;
        public HandEquipment item;

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
        public void AddItem(HandEquipment newItem)
        {
            item = newItem;
            icon.sprite = item.itemIcon;
            icon.enabled = true;
            gameObject.SetActive(true);
        }

        //删除此物品:
        public void ClearItem()
        {
            item = null;
            icon.sprite = null;
            icon.enabled = false;
            gameObject.SetActive(false);
        }

        //装备此物品:
        public void EquipThisItem()
        {
            //如果当前腿甲不为空:
            if (playerInventory.currentHandEquipment != null)
            {
                playerInventory.handEquipmentInventory.Add(playerInventory.currentHandEquipment);
            }

            //将这个装备设置为当前装备的腿甲:
            playerInventory.currentHandEquipment = item;

            //该腿甲不为空,将其从腿甲库存中删除:
            if (item != null)
            {
                playerInventory.handEquipmentInventory.Remove(item);
            }
            //将其添加进腿甲库存:

            //更新选择装备窗口ui:
            uiManager.equipmentWindowUI.LoadHandEquipmentOnEquipmentScreen(playerInventory);

            //更新ui:库存窗口(除了武器)
            inventoryWindowManager.UpdateInventoryWindowUI();

            //更新当前装备:
            playerInventory.GetComponent<PlayerEquipmentManager>().EquipAllEquipmentModels();
        }

        //更新装备详情页面:[鼠标经过事件调用:]
        public void UpdateEquipmentProperties()
        {
            uiManager.equipmentPropertiesWindowManager.UpdateUIWindow(item, playerInventory);
        }
        //鼠标进入时:[背包窗口中调用]
        public void MouseEntry()
        {
            //腿甲的id是:[6]
            uiManager.itemPropertiesWindowManager.UpdateItemPropertiesWindow(item, 6);
        }
        //鼠标移出时:[背包窗口中调用]
        public void MouseOut()
        {
            //腿甲的id是:[6]
            uiManager.itemPropertiesWindowManager.UpdateItemPropertiesWindow(null, 6);
        }
    }
}