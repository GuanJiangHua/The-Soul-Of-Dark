using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class HandSlotUI : MonoBehaviour
    {
        public Image icom;

        HandEquipment hand;
        UIManager uiManager;

        private void Start()
        {
            uiManager = FindObjectOfType<UIManager>();
        }
        public void AddItem(HandEquipment item)
        {
            if (item == null)
            {
                ClearItem();
                return;
            }

            hand = item;
            icom.sprite = hand.itemIcon;
            icom.enabled = true;
            gameObject.SetActive(true);
        }

        public void ClearItem()
        {
            hand = null;
            icom.sprite = null;
            icom.enabled = false;
            //gameObject.SetActive(false);
        }

        //鼠标进入:
        public void MouseOverEvent()
        {
            PlayerInventoryManager playerInventory = uiManager.playerManager.playerInventoryManager;
            uiManager.equipmentPropertiesWindowManager.UpdateUIWindow(hand, playerInventory);

            uiManager.weaponPropertiesWindow.SetActive(false);
            uiManager.equipmentPropertiesWindow.SetActive(true);
        }

        //鼠标离开时:
        public void MouseLeaveEvent()
        {
            uiManager.equipmentPropertiesWindowManager.UpdateUIWindow();
        }
    }
}