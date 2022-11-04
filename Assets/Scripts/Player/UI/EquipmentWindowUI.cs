using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class EquipmentWindowUI : MonoBehaviour
    {
        public bool rightHand01Selected;    //ѡ������01;
        public bool rightHand02Selected;    //ѡ������02;
        public bool rightHand03Selected;    //ѡ������03;

        public bool leftHand01Selected;      //ѡ������01;
        public bool leftHand02Selected;      //ѡ������02;
        public bool leftHand03Selected;      //ѡ������03;

        //������������:
        [Header("����������:")]
        public HandEquipmentSlotUI[] handEquipmentSlotUIs;

        //����Ʒ��:
        [Header("����Ʒ:")]
        public ConsumableSlotUI[] consumableSlotUIs;

        [Header("ͷ��:")]
        public HelmetSlotUI helmetSlotUI;

        [Header("�ؼ�:")]
        public TorsoSlotUI torsoSlotUI;

        [Header("�ȼ�:")]
        public LegSlotUI legSlotUI;

        [Header("�ۼ�:")]
        public HandSlotUI handSlotUI;

        //��ʸװ����:
        [Header("��ʸ:")]
        public AmmoSlotUI[] ammoSlotUIs = new AmmoSlotUI[4];

        [Header("��ָ:")]
        public RingSlotUI[] ringSlotUIs = new RingSlotUI[4];

        public UIManager uiManager;

        private void Start()
        {
            //handEquipmentSlotUIs = FindObjectsOfType<HandEquipmentSlotUI>();
        }

        //����������浽��Ļ:
        public void LoadWeaponsOnEquipmentScreen(PlayerInventoryManager playerInventory)
        {
            for(int i = 0; i < handEquipmentSlotUIs.Length; i++)
            {
                if (handEquipmentSlotUIs[i].rightHand01)
                {
                    handEquipmentSlotUIs[i].AddItem(playerInventory.WeaponRightHandSlots[0]);
                }
                else if (handEquipmentSlotUIs[i].rightHand02)
                {
                    handEquipmentSlotUIs[i].AddItem(playerInventory.WeaponRightHandSlots[1]);
                }
                else if (handEquipmentSlotUIs[i].rightHand03)
                {
                    handEquipmentSlotUIs[i].AddItem(playerInventory.WeaponRightHandSlots[2]);
                }
                else if (handEquipmentSlotUIs[i].leftHand01)
                {
                    handEquipmentSlotUIs[i].AddItem(playerInventory.WeaponLeftHandSlots[0]);
                }
                else if (handEquipmentSlotUIs[i].leftHand02)
                {
                    handEquipmentSlotUIs[i].AddItem(playerInventory.WeaponLeftHandSlots[1]);
                }
                else if (handEquipmentSlotUIs[i].leftHand03)
                {
                    handEquipmentSlotUIs[i].AddItem(playerInventory.WeaponLeftHandSlots[2]);
                }
            }
        }

        //���ظ���������Ʒװ������Ļ:
        public void LoadConsumableOnEquipmentScreen(PlayerInventoryManager playerInventory)
        {
            int index = uiManager.consumableInventorySlotSelected;
            for(int i = 0; i < playerInventory.consumableSlots.Length; i++)
            {
                //����װ�����ڵ�����Ʒ��
                consumableSlotUIs[i].AddItem(playerInventory.consumableSlots[i]);
            }

            ConsumableItem currentConsumable = playerInventory.consumableSlots[playerInventory.currentConsumableIndex];
            //������ʾλ�õ�����Ʒ��
            uiManager.quickSlotUI.UpdateCurrentConsumableQuickSlotUI(currentConsumable);
        }
        //����ͷ��װ������Ļ:
        public void LoadHelmetEquipmentOnEquipmentScreen(PlayerInventoryManager playerInventory)
        {
            helmetSlotUI.AddItem(playerInventory.currentHelmetEquipment);
        }

        //���ص�ǰ�ؼ�װ������Ļ
        public void LoadTorsoEquipmentOnEquipmentScreen(PlayerInventoryManager playerInventory)
        {
            torsoSlotUI.AddItem(playerInventory.currentTorsoEquipment);
        }

        //���ص�ǰ�ȼ׵���Ļ
        public void LoadLegEquipmentOnEquipmentScreen(PlayerInventoryManager playerInventory)
        {
            legSlotUI.AddItem(playerInventory.currentLegEquipment);
        }

        //���ص�ǰ�ۼ׵���Ļ
        public void LoadHandEquipmentOnEquipmentScreen(PlayerInventoryManager playerInventory)
        {
            handSlotUI.AddItem(playerInventory.currentHandEquipment);
        }

        //���ؼ�ʸ����Ļ:
        public void LoadAmmoEquipmentOnEquipmentScreen(PlayerInventoryManager playerInventory)
        {
            ammoSlotUIs[0].AddItem(playerInventory.currentBow);
            ammoSlotUIs[1].AddItem(playerInventory.spareBow);
            ammoSlotUIs[2].AddItem(playerInventory.otherAmmo);
            ammoSlotUIs[3].AddItem(playerInventory.spareOtherAmmo);
        }

        //���ؽ�ָ����Ļ:
        public void LoadRingEquipmentOnEquipmentScreen(PlayerInventoryManager playerInventory)
        {
            for(int i = 0; i < ringSlotUIs.Length; i++)
            {
                ringSlotUIs[i].AddItem(playerInventory.GetRingById(i));
            }
        }

        #region ����װ��
        public void SelectedRightHand01()
        {
            rightHand01Selected = true;
        }
        public void SelectedRightHand02()
        {
            rightHand02Selected = true;
        }
        public void SelectedRightHand03()
        {
            rightHand03Selected = true;
        }
        #endregion

        #region ����װ��
        public void SelectedLeftHand01()
        {
            leftHand01Selected = true;
        }
        public void SelectedLeftHand02()
        {
            leftHand02Selected = true;
        }
        public void SelectedLeftHand03()
        {
            leftHand03Selected = true;
        }
        #endregion
    }
}