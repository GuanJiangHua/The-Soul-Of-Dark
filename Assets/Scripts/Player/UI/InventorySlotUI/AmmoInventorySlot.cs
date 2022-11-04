using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class AmmoInventorySlot : MonoBehaviour
    {
        public Image icon;
        public RangedAmmoItem item;

        UIManager uiManager;
        PlayerInventoryManager playerInventory;
        InventoryWindowManager inventoryWindowManager;

        private void Awake()
        {
            uiManager = FindObjectOfType<UIManager>();
            playerInventory = FindObjectOfType<PlayerInventoryManager>();
            inventoryWindowManager = GetComponentInParent<InventoryWindowManager>();
        }

        //��Ӵ���Ʒ:
        public void AddItem(RangedAmmoItem newItem)
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

        //װ������Ʒ:
        public void EquipThisItem()
        {
            if (inventoryWindowManager.ammoSlotIndex < 1 || inventoryWindowManager.ammoSlotIndex > 4) return;
            //�����װ������Ϊ��ǰװ���ĵ�ҩ:
            if(inventoryWindowManager.ammoSlotIndex == 1)
            {
                if(playerInventory.currentBow!=null)
                {
                    playerInventory.rangedAmmoInventory.Add(playerInventory.currentBow);
                }
                playerInventory.currentBow = item;
            }
            else if (inventoryWindowManager.ammoSlotIndex == 2)
            {
                if (playerInventory.spareBow != null)
                {
                    playerInventory.rangedAmmoInventory.Add(playerInventory.spareBow);
                }
                playerInventory.spareBow = item;
            }
            else if (inventoryWindowManager.ammoSlotIndex == 3)
            {
                if (playerInventory.otherAmmo != null)
                {
                    playerInventory.rangedAmmoInventory.Add(playerInventory.otherAmmo);
                }
                playerInventory.otherAmmo = item;
            }
            else if (inventoryWindowManager.ammoSlotIndex == 4)
            {
                if (playerInventory.spareOtherAmmo != null)
                {
                    playerInventory.rangedAmmoInventory.Add(playerInventory.spareOtherAmmo);
                }
                playerInventory.spareOtherAmmo = item;
            }

            //���ȼײ�Ϊ��,����ӵ�������ɾ��:
            if (item != null)
            {
                playerInventory.rangedAmmoInventory.Remove(item);
            }

            //���µ�ǰװ���ļ�ʸ:
            uiManager.equipmentWindowUI.LoadAmmoEquipmentOnEquipmentScreen(playerInventory);
            //����ui:��洰��(��������)
            inventoryWindowManager.UpdateInventoryWindowUI();
        }

        //����װ������ҳ��:[��꾭���¼�����:]
        public void UpdateEquipmentProperties()
        {
            Debug.Log("������:");
            uiManager.weapentPropertiesWindowManager.UpdateUIWindow(item, playerInventory , true);
        }
        //������ʱ:[���������е���]
        public void MouseEntry()
        {
            //��ҩ��id��:[6]
            uiManager.itemPropertiesWindowManager.UpdateItemPropertiesWindow(item, 7);
        }
        //����Ƴ�ʱ:[���������е���]
        public void MouseOut()
        {
            //��ҩ��id��:[6]
            uiManager.itemPropertiesWindowManager.UpdateItemPropertiesWindow(null, 7);
        }
    }
}