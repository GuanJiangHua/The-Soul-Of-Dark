using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class RingSlotUI : MonoBehaviour
    {
        public Image icom;

        RingItem ring;
        UIManager uiManager;

        private void Start()
        {
            uiManager = FindObjectOfType<UIManager>();
        }
        public void AddItem(RingItem item)
        {
            if (item == null)
            {
                ClearItem();
                return;
            }

            ring = item;
            icom.sprite = ring.itemIcon;
            icom.enabled = true;
            gameObject.SetActive(true);
        }

        public void ClearItem()
        {
            ring = null;
            icom.sprite = null;
            icom.enabled = false;
            //gameObject.SetActive(false);
        }

        //选择此消耗品槽:[按钮调用]
        public void SelectThisSlot(int index)
        {
            uiManager.UpdateUI();

            uiManager.ringInventorySlotSelected = index;
        }
    }
}