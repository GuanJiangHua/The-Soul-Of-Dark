using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class HelmetEquipmentInventorySlotUI : MonoBehaviour
    {
        public Image icon;
        public HelmetEquipment item;

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
        public void AddItem(HelmetEquipment newItem)
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
            //���ԭ����װ��ͷ��:(����ͷ�������װ�����:)
            if(playerInventory.currentHelmetEquipment != null)
            {
                playerInventory.headEquipmentInventory.Add(playerInventory.currentHelmetEquipment);
            }
            //��ǰͷ��װ��Ϊ����λָ����ͷ��:
            playerInventory.currentHelmetEquipment = item;

            //�ǿ�,��ӱ��������ɾ��:
            if (item != null)
            {
                playerInventory.headEquipmentInventory.Remove(item);
            }

            //��������װ��:
            playerInventory.GetComponent<PlayerEquipmentManager>().EquipAllEquipmentModels();
            //����ui:��洰��(��������)
            inventoryWindowManager.UpdateInventoryWindowUI();
            //����װ����װ��ѡ�񴰿�ui:
            uiManager.equipmentWindowUI.LoadHelmetEquipmentOnEquipmentScreen(playerInventory);
        }

        //����װ������ҳ��:[��꾭���¼�����:]
        public void UpdateEquipmentProperties()
        {
            uiManager.equipmentPropertiesWindowManager.UpdateUIWindow(item, playerInventory);
        }

        //������ʱ:[���������е���]
        public void MouseEntry()
        {
            //ͷ����id��:[3]
            uiManager.itemPropertiesWindowManager.UpdateItemPropertiesWindow(item, 3);
        }
        //����Ƴ�ʱ:[���������е���]
        public void MouseOut()
        {
            //ͷ����id��:[3]
            uiManager.itemPropertiesWindowManager.UpdateItemPropertiesWindow(null, 3);
        }
    }
}