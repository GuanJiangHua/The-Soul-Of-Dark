using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class HelmetSlotUI : MonoBehaviour
    {
        public Image icom;

        HelmetEquipment helmet;
        UIManager uiManager;

        private void Start()
        {
            uiManager = FindObjectOfType<UIManager>();
        }
        public void AddItem(HelmetEquipment item)
        {
            if (item == null)
            {
                ClearItem();
                return;
            }

            helmet = item;
            icom.sprite = helmet.itemIcon;
            icom.enabled = true;
            gameObject.SetActive(true);
        }

        public void ClearItem()
        {
            helmet = null;
            icom.sprite = null;
            icom.enabled = false;
            //gameObject.SetActive(false);
        }

        //ѡ���ͷ��װ����:��ҪҲ��
        public void SelectThisSlot()
        {
            //uiManager.UpdateUI();
        }

        //������:
        public void MouseOverEvent()
        {
            PlayerInventoryManager playerInventory = uiManager.playerManager.playerInventoryManager;
            uiManager.equipmentPropertiesWindowManager.UpdateUIWindow(helmet, playerInventory);

            uiManager.weaponPropertiesWindow.SetActive(false);
            uiManager.equipmentPropertiesWindow.SetActive(true);
        }

        //����뿪ʱ:
        public void MouseLeaveEvent()
        {
            uiManager.equipmentPropertiesWindowManager.UpdateUIWindow();
        }
    }
}