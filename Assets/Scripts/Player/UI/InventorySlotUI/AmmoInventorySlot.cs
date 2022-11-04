using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class AmmoInventorySlot : MonoBehaviour
    {
        public Image icon;
        public RangedAmmoItem item;

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
        public void AddItem(RangedAmmoItem newItem)
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
            if (inventoryWindowManager.ammoSlotIndex < 1 || inventoryWindowManager.ammoSlotIndex > 4) return;
            //将这个装备设置为当前装备的弹药:
            if(inventoryWindowManager.ammoSlotIndex == 1)
            {
                if(playerInventory.currentBow!=null)
                {
                    playerInventory.rangedAmmoInventory.Add(playerInventory.currentBow);
                }
                playerInventory.currentBow = item;
            }
            else if (inventoryWindowManager.ammoSlotIndex == 2)
            {
                if (playerInventory.spareBow != null)
                {
                    playerInventory.rangedAmmoInventory.Add(playerInventory.spareBow);
                }
                playerInventory.spareBow = item;
            }
            else if (inventoryWindowManager.ammoSlotIndex == 3)
            {
                if (playerInventory.otherAmmo != null)
                {
                    playerInventory.rangedAmmoInventory.Add(playerInventory.otherAmmo);
                }
                playerInventory.otherAmmo = item;
            }
            else if (inventoryWindowManager.ammoSlotIndex == 4)
            {
                if (playerInventory.spareOtherAmmo != null)
                {
                    playerInventory.rangedAmmoInventory.Add(playerInventory.spareOtherAmmo);
                }
                playerInventory.spareOtherAmmo = item;
            }

            //该腿甲不为空,将其从弹丸库存中删除:
            if (item != null)
            {
                playerInventory.rangedAmmoInventory.Remove(item);
            }

            //更新当前装备的箭矢:
            uiManager.equipmentWindowUI.LoadAmmoEquipmentOnEquipmentScreen(playerInventory);
            //更新ui:库存窗口(除了武器)
            inventoryWindowManager.UpdateInventoryWindowUI();
        }

        //更新装备详情页面:[鼠标经过事件调用:]
        public void UpdateEquipmentProperties()
        {
            Debug.Log("鼠标进入:");
            uiManager.weapentPropertiesWindowManager.UpdateUIWindow(item, playerInventory , true);
        }
        //鼠标进入时:[背包窗口中调用]
        public void MouseEntry()
        {
            //弹药的id是:[6]
            uiManager.itemPropertiesWindowManager.UpdateItemPropertiesWindow(item, 7);
        }
        //鼠标移出时:[背包窗口中调用]
        public void MouseOut()
        {
            //弹药的id是:[6]
            uiManager.itemPropertiesWindowManager.UpdateItemPropertiesWindow(null, 7);
        }
    }
}