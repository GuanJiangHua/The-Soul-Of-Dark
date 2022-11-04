using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class ConsumableInventorySlot : MonoBehaviour
    {
        public Image icon;
        public ConsumableItem item;
        [Header("����Ʒ����:")]
        public Text currentValueText;
        public Text maxValueText;

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
        public void AddItem(ConsumableItem newItem)
        {
            item = newItem;
            icon.sprite = item.itemIcon;
            icon.enabled = true;

            currentValueText.text = newItem.currentItemAmount.ToString();
            if (newItem.currentItemAmount <= 0)
                currentValueText.color = Color.red;
            else
                currentValueText.color = Color.white;
            currentValueText.enabled = true;
            maxValueText.text = newItem.maxItemAmount.ToString();

            gameObject.SetActive(true);
        }

        //ɾ������Ʒ:
        public void ClearItem()
        {
            item = null;
            icon.sprite = null;
            icon.enabled = false;
            maxValueText.text = "-";
            maxValueText.enabled = false;
            currentValueText.text = "-";
            currentValueText.enabled = false;

            gameObject.SetActive(false);
        }

        //װ��������:
        public void EquipThisItem()
        {
            int index = uiManager.consumableInventorySlotSelected;
            ConsumableItem[] consumables = playerInventory.consumableSlots;
            if(index != -1)
            {

                //�����λ����װ����������Ʒ,��������Ʒ�����"����Ʒ���";
                if(consumables[index] != null)
                {
                    playerInventory.consumableInventory.Add(consumables[index]);
                }

                //������Ʒ��ӵ�ѡ��������Ʒװ����:
                consumables[index] = item;
                //�������Ʒ�ǿ�,������Ʒ�����ɾ��:
                if(item != null)
                {
                    playerInventory.consumableInventory.Remove(item);
                }

                //װ����,����ui(װ������):
                uiManager.equipmentWindowUI.LoadConsumableOnEquipmentScreen(playerInventory);
            }
            playerInventory.currentConsumable = consumables[playerInventory.currentConsumableIndex];
            uiManager.consumableInventorySlotSelected = -1;
        }

        //������ʱ:[���������е���]
        public void MouseEntry()
        {
            //����Ʒ��id��:[1]
            uiManager.itemPropertiesWindowManager.UpdateItemPropertiesWindow(item, 1);
        }
        //����Ƴ�ʱ:[���������е���]
        public void MouseOut()
        {
            //����Ʒ��id��:[1]
            uiManager.itemPropertiesWindowManager.UpdateItemPropertiesWindow(null, 1);
        }
    }
}