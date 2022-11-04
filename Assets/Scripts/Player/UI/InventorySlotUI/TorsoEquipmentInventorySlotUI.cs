using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class TorsoEquipmentInventorySlotUI : MonoBehaviour
    {
        public Image icon;
        public TorsoEquipment item;

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
        public void AddItem(TorsoEquipment newItem)
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
            //如果当前防具不为空,则将这个躯干防具加入到躯干库存:
            if(playerInventory.currentTorsoEquipment != null)
            {
                playerInventory.torsoEquipmentInventory.Add(playerInventory.currentTorsoEquipment);
            }

            //把这个物品装备到当前躯干防具:
            playerInventory.currentTorsoEquipment = item;

            //将这个物品从躯干防具库存中删除:
            if (item != null)
            {
                playerInventory.torsoEquipmentInventory.Remove(item);
            }

            //加载所有装备:
            playerInventory.GetComponent<PlayerEquipmentManager>().EquipAllEquipmentModels();
            //更新ui:库存窗口(除了武器)
            inventoryWindowManager.UpdateInventoryWindowUI();
            //更新装备选择窗口:
            uiManager.equipmentWindowUI.LoadTorsoEquipmentOnEquipmentScreen(playerInventory);
        }

        //更新装备详情页面:[鼠标经过事件调用:]
        public void UpdateWeapenProperties()
        {
            uiManager.equipmentPropertiesWindowManager.UpdateUIWindow(item, playerInventory);
        }

        //鼠标进入时:[背包窗口中调用]
        public void MouseEntry()
        {
            //胸甲的id是:[4]
            uiManager.itemPropertiesWindowManager.UpdateItemPropertiesWindow(item, 4);
        }
        //鼠标移出时:[背包窗口中调用]
        public void MouseOut()
        {
            //胸甲的id是:[4]
            uiManager.itemPropertiesWindowManager.UpdateItemPropertiesWindow(null, 4);
        }
    }
}