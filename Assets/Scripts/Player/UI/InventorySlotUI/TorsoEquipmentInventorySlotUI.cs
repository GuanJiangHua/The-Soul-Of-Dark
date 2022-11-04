using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class TorsoEquipmentInventorySlotUI : MonoBehaviour
    {
        public Image icon;
        public TorsoEquipment item;

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
        public void AddItem(TorsoEquipment newItem)
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
            //�����ǰ���߲�Ϊ��,��������ɷ��߼��뵽���ɿ��:
            if(playerInventory.currentTorsoEquipment != null)
            {
                playerInventory.torsoEquipmentInventory.Add(playerInventory.currentTorsoEquipment);
            }

            //�������Ʒװ������ǰ���ɷ���:
            playerInventory.currentTorsoEquipment = item;

            //�������Ʒ�����ɷ��߿����ɾ��:
            if (item != null)
            {
                playerInventory.torsoEquipmentInventory.Remove(item);
            }

            //��������װ��:
            playerInventory.GetComponent<PlayerEquipmentManager>().EquipAllEquipmentModels();
            //����ui:��洰��(��������)
            inventoryWindowManager.UpdateInventoryWindowUI();
            //����װ��ѡ�񴰿�:
            uiManager.equipmentWindowUI.LoadTorsoEquipmentOnEquipmentScreen(playerInventory);
        }

        //����װ������ҳ��:[��꾭���¼�����:]
        public void UpdateWeapenProperties()
        {
            uiManager.equipmentPropertiesWindowManager.UpdateUIWindow(item, playerInventory);
        }

        //������ʱ:[���������е���]
        public void MouseEntry()
        {
            //�ؼ׵�id��:[4]
            uiManager.itemPropertiesWindowManager.UpdateItemPropertiesWindow(item, 4);
        }
        //����Ƴ�ʱ:[���������е���]
        public void MouseOut()
        {
            //�ؼ׵�id��:[4]
            uiManager.itemPropertiesWindowManager.UpdateItemPropertiesWindow(null, 4);
        }
    }
}