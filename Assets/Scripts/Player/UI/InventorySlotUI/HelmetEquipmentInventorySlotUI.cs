using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class HelmetEquipmentInventorySlotUI : MonoBehaviour
    {
        public Image icon;
        public HelmetEquipment item;

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
        public void AddItem(HelmetEquipment newItem)
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
            //如果原本有装备头盔:(将该头盔加入回装备库存:)
            if(playerInventory.currentHelmetEquipment != null)
            {
                playerInventory.headEquipmentInventory.Add(playerInventory.currentHelmetEquipment);
            }
            //当前头盔装备为本槽位指代的头盔:
            playerInventory.currentHelmetEquipment = item;

            //非空,则从背包库存中删除:
            if (item != null)
            {
                playerInventory.headEquipmentInventory.Remove(item);
            }

            //加载所有装备:
            playerInventory.GetComponent<PlayerEquipmentManager>().EquipAllEquipmentModels();
            //更新ui:库存窗口(除了武器)
            inventoryWindowManager.UpdateInventoryWindowUI();
            //更新装备到装备选择窗口ui:
            uiManager.equipmentWindowUI.LoadHelmetEquipmentOnEquipmentScreen(playerInventory);
        }

        //更新装备详情页面:[鼠标经过事件调用:]
        public void UpdateEquipmentProperties()
        {
            uiManager.equipmentPropertiesWindowManager.UpdateUIWindow(item, playerInventory);
        }

        //鼠标进入时:[背包窗口中调用]
        public void MouseEntry()
        {
            //头盔的id是:[3]
            uiManager.itemPropertiesWindowManager.UpdateItemPropertiesWindow(item, 3);
        }
        //鼠标移出时:[背包窗口中调用]
        public void MouseOut()
        {
            //头盔的id是:[3]
            uiManager.itemPropertiesWindowManager.UpdateItemPropertiesWindow(null, 3);
        }
    }
}