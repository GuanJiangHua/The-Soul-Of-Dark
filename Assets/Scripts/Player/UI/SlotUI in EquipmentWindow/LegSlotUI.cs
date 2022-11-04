using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class LegSlotUI : MonoBehaviour
    {
        public Image icom;

        LegEquipment leg;
        UIManager uiManager;

        private void Start()
        {
            uiManager = FindObjectOfType<UIManager>();
        }
        public void AddItem(LegEquipment item)
        {
            if (item == null)
            {
                ClearItem();
                return;
            }

            leg = item;
            icom.sprite = leg.itemIcon;
            icom.enabled = true;
            gameObject.SetActive(true);
        }

        public void ClearItem()
        {
            leg = null;
            icom.sprite = null;
            icom.enabled = false;
            //gameObject.SetActive(false);
        }

        //鼠标进入:
        public void MouseOverEvent()
        {
            PlayerInventoryManager playerInventory = uiManager.playerManager.playerInventoryManager;
            uiManager.equipmentPropertiesWindowManager.UpdateUIWindow(leg, playerInventory);

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