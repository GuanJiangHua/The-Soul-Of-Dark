using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class RingInventorySlot : MonoBehaviour
    {
        public Image icon;
        public RingItem item;

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
        public void AddItem(RingItem newItem)
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
            playerInventory.AddRing(uiManager.ringInventorySlotSelected , item);
            //����ѡ��װ������ui:
            uiManager.equipmentWindowUI.LoadRingEquipmentOnEquipmentScreen(playerInventory);
            uiManager.playerPropertiesWindowManager.UpdatePropertiesText(playerInventory.GetComponent<PlayerStatsManager>());
            uiManager.ringInventorySlotSelected = -1;
            //����ui:��洰��(��������)
            inventoryWindowManager.UpdateInventoryWindowUI();
        }

        //����װ������ҳ��:[��꾭���¼�����:]
        public void UpdateEquipmentProperties()
        {
            uiManager.equipmentPropertiesWindowManager.UpdateUIWindow();
        }
        //������ʱ:[���������е���]
        public void MouseEntry()
        {
            //��ָ��id��:[8]
            uiManager.itemPropertiesWindowManager.UpdateItemPropertiesWindow(item, 8);
            uiManager.playerPropertiesWindowManager.UpdatePropertiesText(playerInventory.GetComponent<PlayerStatsManager>(), item);
        }
        //����Ƴ�ʱ:[���������е���]
        public void MouseOut()
        {
            //��ָ��id��:[8]
            uiManager.itemPropertiesWindowManager.UpdateItemPropertiesWindow(null, 8);
            uiManager.playerPropertiesWindowManager.UpdatePropertiesText(playerInventory.GetComponent<PlayerStatsManager>(), null);
        }
    }
}
