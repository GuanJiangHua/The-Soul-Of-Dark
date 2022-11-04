using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class WeaponInventorySlot : MonoBehaviour
    {
        public Image icon;
        public WeaponItem unarmedWeapon;
        public WeaponItem item;

        PlayerInventoryManager playerInventory;
        PlayerWeaponSlotManger weaponSlotManger;
        UIManager uiManager;

        private void Awake()
        {
            playerInventory = FindObjectOfType<PlayerInventoryManager>();
            weaponSlotManger = FindObjectOfType<PlayerWeaponSlotManger>();
            uiManager = FindObjectOfType<UIManager>();
        }

        //添加此物品:
        public void AddItem(WeaponItem newItem)
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

        //装备此物品:(按钮调用)
        public void EquipThisItem()
        {

            bool isRightHand = uiManager.rightHandSlot01Selected || uiManager.rightHandSlot02Selected || uiManager.rightHandSlot03Selected;

            if (isRightHand && (item.weaponType == WeaponType.SmallShield || item.weaponType == WeaponType.Shield))
            {
                uiManager.ResetAllSelectedSlots();
                return;
            }

            //右手武器:
            if (uiManager.rightHandSlot01Selected)
            {
                //武器为空，武器为空手不加进背包
                if (playerInventory.WeaponRightHandSlots[0] != null && playerInventory.WeaponRightHandSlots[0] != unarmedWeapon)
                {
                    playerInventory.weaponInventory.Add(playerInventory.WeaponRightHandSlots[0]);
                }

                Debug.Log(item.itemName);
                playerInventory.WeaponRightHandSlots[0] = item;
            }
            else if (uiManager.rightHandSlot02Selected)
            {
                if (playerInventory.WeaponRightHandSlots[1] != null && playerInventory.WeaponRightHandSlots[1] != unarmedWeapon)
                {
                    if (item.weaponType == WeaponType.Shield || item.weaponType == WeaponType.SmallShield) return;
                    playerInventory.weaponInventory.Add(playerInventory.WeaponRightHandSlots[1]);
                }
                playerInventory.WeaponRightHandSlots[1] = item;
            }
            else if (uiManager.rightHandSlot03Selected)
            {

                if (playerInventory.WeaponRightHandSlots[2] != null && playerInventory.WeaponRightHandSlots[2] != unarmedWeapon)
                {
                    if (item.weaponType == WeaponType.Shield || item.weaponType == WeaponType.SmallShield) return;
                    playerInventory.weaponInventory.Add(playerInventory.WeaponRightHandSlots[2]);
                }
                playerInventory.WeaponRightHandSlots[2] = item;
            }
            //左手武器:
            else if (uiManager.leftHandSlot01Selected)
            {
                if (playerInventory.WeaponLeftHandSlots[0] != null && playerInventory.WeaponLeftHandSlots[0] != unarmedWeapon)
                {
                    playerInventory.weaponInventory.Add(playerInventory.WeaponLeftHandSlots[0]);
                }
                playerInventory.WeaponLeftHandSlots[0] = item;
            }
            else if (uiManager.leftHandSlot02Selected)
            {
                if (playerInventory.WeaponLeftHandSlots[1] != null && playerInventory.WeaponLeftHandSlots[1] != unarmedWeapon)
                {
                    playerInventory.weaponInventory.Add(playerInventory.WeaponLeftHandSlots[1]);
                }
                playerInventory.WeaponLeftHandSlots[1] = item;
            }
            else if (uiManager.leftHandSlot03Selected)
            {
                if (playerInventory.WeaponLeftHandSlots[2] != null && playerInventory.WeaponLeftHandSlots[2] != unarmedWeapon)
                {
                    playerInventory.weaponInventory.Add(playerInventory.WeaponLeftHandSlots[2]);
                }
                playerInventory.WeaponLeftHandSlots[2] = item;
            }
            else
            {
                return;
            }

            if(item!=null && item != unarmedWeapon)
            {
                playerInventory.weaponInventory.Remove(item);
            }

            if (playerInventory.currentLeftWeaponIndex != -1)
            {
                playerInventory.leftWeapon = playerInventory.WeaponLeftHandSlots[playerInventory.currentLeftWeaponIndex];
            }
            else
            {
                playerInventory.leftWeapon = playerInventory.WeaponLeftHandSlots[0];
            }
            if (playerInventory.currentRightWeaponIndex != -1)
            {
                playerInventory.rightWeapon = playerInventory.WeaponRightHandSlots[playerInventory.currentRightWeaponIndex];
            }
            else
            {
                playerInventory.rightWeapon = playerInventory.WeaponRightHandSlots[0];
            }
            weaponSlotManger.LoadWeaponHolderOfSlot(playerInventory.leftWeapon, true);
            weaponSlotManger.LoadWeaponHolderOfSlot(playerInventory.rightWeapon, false);

            uiManager.equipmentWindowUI.LoadWeaponsOnEquipmentScreen(playerInventory);
            uiManager.ResetAllSelectedSlots();
        }

        //更新装备详情页面:[鼠标经过事件调用:]
        public void UpdateWeapenProperties()
        {
            uiManager.weapentPropertiesWindowManager.UpdateUIWindow(item , playerInventory);
        }

        //鼠标进入时:[背包窗口中调用]
        public void MouseEntry()
        {
            //武器的id是:[1]
            uiManager.itemPropertiesWindowManager.UpdateItemPropertiesWindow(item, 2);
        }
        //鼠标移出时:[背包窗口中调用]
        public void MouseOut()
        {
            //武器的id是:[1]
            uiManager.itemPropertiesWindowManager.UpdateItemPropertiesWindow(null, 2);
        }
    }
}
