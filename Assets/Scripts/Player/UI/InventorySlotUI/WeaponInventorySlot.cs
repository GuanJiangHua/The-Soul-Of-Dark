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

        //��Ӵ���Ʒ:
        public void AddItem(WeaponItem newItem)
        {
            item = newItem;
            icon.sprite = item.itemIcon;
            icon.enabled = true;
            gameObject.SetActive(true);
        }

        //ɾ������Ʒ:
        public void ClearItem()
        {
            item = null;
            icon.sprite = null;
            icon.enabled = false;
            gameObject.SetActive(false);
        }

        //װ������Ʒ:(��ť����)
        public void EquipThisItem()
        {

            bool isRightHand = uiManager.rightHandSlot01Selected || uiManager.rightHandSlot02Selected || uiManager.rightHandSlot03Selected;

            if (isRightHand && (item.weaponType == WeaponType.SmallShield || item.weaponType == WeaponType.Shield))
            {
                uiManager.ResetAllSelectedSlots();
                return;
            }

            //��������:
            if (uiManager.rightHandSlot01Selected)
            {
                //����Ϊ�գ�����Ϊ���ֲ��ӽ�����
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
            //��������:
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

        //����װ������ҳ��:[��꾭���¼�����:]
        public void UpdateWeapenProperties()
        {
            uiManager.weapentPropertiesWindowManager.UpdateUIWindow(item , playerInventory);
        }

        //������ʱ:[���������е���]
        public void MouseEntry()
        {
            //������id��:[1]
            uiManager.itemPropertiesWindowManager.UpdateItemPropertiesWindow(item, 2);
        }
        //����Ƴ�ʱ:[���������е���]
        public void MouseOut()
        {
            //������id��:[1]
            uiManager.itemPropertiesWindowManager.UpdateItemPropertiesWindow(null, 2);
        }
    }
}
