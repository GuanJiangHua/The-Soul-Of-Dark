using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class LegEquipmentInventorySlotUI : MonoBehaviour
    {
        public Image icon;
        public LegEquipment item;

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
        public void AddItem(LegEquipment newItem)
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
            //�����ǰ�ȼײ�Ϊ��:
            if (playerInventory.currentLegEquipment != null)
            {
                playerInventory.legEquipmentInventory.Add(playerInventory.currentLegEquipment);
            }

            //�����װ������Ϊ��ǰװ�����ȼ�:
            playerInventory.currentLegEquipment = item;

            //���ȼײ�Ϊ��,������ȼ׿����ɾ��:
            if (item != null)
            {
                playerInventory.legEquipmentInventory.Remove(item);
            }
            //������ӽ��ȼ׿��:

            //����ѡ��װ������ui:
            uiManager.equipmentWindowUI.LoadLegEquipmentOnEquipmentScreen(playerInventory);

            //����ui:��洰��(��������)
            inventoryWindowManager.UpdateInventoryWindowUI();

            //���µ�ǰװ��:
            playerInventory.GetComponent<PlayerEquipmentManager>().EquipAllEquipmentModels();
        }

        //����װ������ҳ��:[��꾭���¼�����:]
        public void UpdateEquipmentProperties()
        {
            uiManager.equipmentPropertiesWindowManager.UpdateUIWindow(item, playerInventory);
        }

        //������ʱ:[���������е���]
        public void MouseEntry()
        {
            //�ȼ׵�id��:[5]
            uiManager.itemPropertiesWindowManager.UpdateItemPropertiesWindow(item, 5);
        }
        //����Ƴ�ʱ:[���������е���]
        public void MouseOut()
        {
            //�ȼ׵�id��:[5]
            uiManager.itemPropertiesWindowManager.UpdateItemPropertiesWindow(null, 5);
        }
    }
}