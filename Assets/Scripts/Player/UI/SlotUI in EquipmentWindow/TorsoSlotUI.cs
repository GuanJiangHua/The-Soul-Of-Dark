using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class TorsoSlotUI : MonoBehaviour
    {
        public Image icom;

        TorsoEquipment torso;
        UIManager uiManager;

        private void Start()
        {
            uiManager = FindObjectOfType<UIManager>();
        }
        public void AddItem(TorsoEquipment item)
        {
            if (item == null)
            {
                ClearItem();
                return;
            }

            torso = item;
            icom.sprite = torso.itemIcon;
            icom.enabled = true;
            gameObject.SetActive(true);
        }

        public void ClearItem()
        {
            torso = null;
            icom.sprite = null;
            icom.enabled = false;
            //gameObject.SetActive(false);
        }

        //鼠标进入:
        public void MouseOverEvent()
        {
            PlayerInventoryManager playerInventory = uiManager.playerManager.playerInventoryManager;
            uiManager.equipmentPropertiesWindowManager.UpdateUIWindow(torso, playerInventory);

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