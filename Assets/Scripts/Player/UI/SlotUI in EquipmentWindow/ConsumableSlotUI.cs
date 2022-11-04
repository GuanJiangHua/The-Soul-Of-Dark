using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class ConsumableSlotUI : MonoBehaviour
    {
        public Image icom;

        ConsumableItem consumable;
        UIManager uiManager;

        private void Start()
        {
            uiManager = FindObjectOfType<UIManager>();
        }
        public void AddItem(ConsumableItem item)
        {
            if (item == null)
            {
                ClearItem();
                return;
            }

            consumable = item;
            icom.sprite = consumable.itemIcon;
            icom.enabled = true;
            gameObject.SetActive(true);
        }

        public void ClearItem()
        {
            consumable = null;
            icom.sprite = null;
            icom.enabled = false;
            //gameObject.SetActive(false);
        }

        //选择此消耗品槽:[按钮调用]
        public void SelectThisSlot(int index)
        {
            uiManager.UpdateUI();

            uiManager.consumableInventorySlotSelected = index;
        }
    }
}