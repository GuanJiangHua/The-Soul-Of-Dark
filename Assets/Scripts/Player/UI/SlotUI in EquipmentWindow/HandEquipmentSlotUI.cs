using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class HandEquipmentSlotUI : MonoBehaviour
    {
        public Image icom;

        WeaponItem weapon;
        UIManager uiManager;

        public bool rightHand01;    
        public bool rightHand02;    
        public bool rightHand03;    

        public bool leftHand01;      
        public bool leftHand02;      
        public bool leftHand03;

        private void Start()
        {
            uiManager = FindObjectOfType<UIManager>();
        }
        public void AddItem(WeaponItem item)
        {
            if (item == null) 
            { 
                ClearItem();
                return;
            } 

            weapon = item;
            icom.sprite = weapon.itemIcon;
            icom.enabled = true;
            gameObject.SetActive(true);
        }

        public void ClearItem()
        {
            weapon = null;
            icom.sprite = null;
            icom.enabled = false;
            //gameObject.SetActive(false);
        }
    
        //选择此武器槽:
        public void SelectThisSlot()
        {
            uiManager.UpdateUI();

            if (rightHand01)
            {
                uiManager.rightHandSlot01Selected = true;
            }
            else if (rightHand02)
            {
                uiManager.rightHandSlot02Selected = true;
            }
            else if (rightHand03)
            {
                uiManager.rightHandSlot03Selected = true;
            }
            else if (leftHand01)
            {
                uiManager.leftHandSlot01Selected = true;
            }
            else if (leftHand02)
            {
                uiManager.leftHandSlot02Selected = true;
            }
            else if (leftHand03)
            {
                uiManager.leftHandSlot03Selected = true;
            }
        }

        //鼠标进入其上时:
        public void MouseOverEvent()
        {
            PlayerInventoryManager playerInventory = uiManager.playerManager.playerInventoryManager;
            WeaponItem[] rightWeapons = uiManager.playerManager.playerInventoryManager.WeaponRightHandSlots;
            WeaponItem[] leftWeapons = uiManager.playerManager.playerInventoryManager.WeaponLeftHandSlots;
            if (rightHand01)
            {
                uiManager.weapentPropertiesWindowManager.UpdateUIWindow(rightWeapons[0], playerInventory);
            }
            else if (rightHand02)
            {
                uiManager.weapentPropertiesWindowManager.UpdateUIWindow(rightWeapons[1], playerInventory);
            }
            else if (rightHand03)
            {
                uiManager.weapentPropertiesWindowManager.UpdateUIWindow(rightWeapons[2], playerInventory);
            }
            else if (leftHand01)
            {
                uiManager.weapentPropertiesWindowManager.UpdateUIWindow(leftWeapons[0], playerInventory);
            }
            else if (leftHand02)
            {
                uiManager.weapentPropertiesWindowManager.UpdateUIWindow(leftWeapons[1], playerInventory);
            }
            else if (leftHand03)
            {
                uiManager.weapentPropertiesWindowManager.UpdateUIWindow(leftWeapons[2], playerInventory);
            }

            uiManager.weaponPropertiesWindow.SetActive(true);
            uiManager.equipmentPropertiesWindow.SetActive(false);
        }
        //鼠标离开时:
        public void MouseLeaveEvent()
        {
            uiManager.weapentPropertiesWindowManager.UpdateUIWindow();
        }
    }
}
