using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class RingInventorySlot : MonoBehaviour
    {
        public Image icon;
        public RingItem item;

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
        public void AddItem(RingItem newItem)
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
            playerInventory.AddRing(uiManager.ringInventorySlotSelected , item);
            //更新选择装备窗口ui:
            uiManager.equipmentWindowUI.LoadRingEquipmentOnEquipmentScreen(playerInventory);
            uiManager.playerPropertiesWindowManager.UpdatePropertiesText(playerInventory.GetComponent<PlayerStatsManager>());
            uiManager.ringInventorySlotSelected = -1;
            //更新ui:库存窗口(除了武器)
            inventoryWindowManager.UpdateInventoryWindowUI();
        }

        //更新装备详情页面:[鼠标经过事件调用:]
        public void UpdateEquipmentProperties()
        {
            uiManager.equipmentPropertiesWindowManager.UpdateUIWindow();
        }
        //鼠标进入时:[背包窗口中调用]
        public void MouseEntry()
        {
            //戒指的id是:[8]
            uiManager.itemPropertiesWindowManager.UpdateItemPropertiesWindow(item, 8);
            uiManager.playerPropertiesWindowManager.UpdatePropertiesText(playerInventory.GetComponent<PlayerStatsManager>(), item);
        }
        //鼠标移出时:[背包窗口中调用]
        public void MouseOut()
        {
            //戒指的id是:[8]
            uiManager.itemPropertiesWindowManager.UpdateItemPropertiesWindow(null, 8);
            uiManager.playerPropertiesWindowManager.UpdatePropertiesText(playerInventory.GetComponent<PlayerStatsManager>(), null);
        }
    }
}
