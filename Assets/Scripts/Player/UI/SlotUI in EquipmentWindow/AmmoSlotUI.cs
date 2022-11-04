using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class AmmoSlotUI : MonoBehaviour
    {
        public Image icom;

        RangedAmmoItem ammo;
        UIManager uiManager;

        private void Start()
        {
            uiManager = FindObjectOfType<UIManager>();
        }
        public void AddItem(RangedAmmoItem item)
        {
            if (item == null)
            {
                ClearItem();
                return;
            }

            ammo = item;
            icom.sprite = ammo.itemIcon;
            icom.enabled = true;
            gameObject.SetActive(true);
        }

        public void ClearItem()
        {
            ammo = null;
            icom.sprite = null;
            icom.enabled = false;
            //gameObject.SetActive(false);
        }

        //选择此消耗品槽:[按钮调用]
        public void SelectThisSlot(int index)
        {
            uiManager.UpdateUI();

            uiManager.inventoryWindonManager.ammoSlotIndex = index;
        }
    }
}