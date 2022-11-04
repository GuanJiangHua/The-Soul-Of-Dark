using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class EquipmentWindowUI : MonoBehaviour
    {
        public bool rightHand01Selected;    //选中右手01;
        public bool rightHand02Selected;    //选中右手02;
        public bool rightHand03Selected;    //选中右手03;

        public bool leftHand01Selected;      //选中左手01;
        public bool leftHand02Selected;      //选中左手02;
        public bool leftHand03Selected;      //选中左手03;

        //左右手武器槽:
        [Header("左右手武器:")]
        public HandEquipmentSlotUI[] handEquipmentSlotUIs;

        //消耗品槽:
        [Header("消耗品:")]
        public ConsumableSlotUI[] consumableSlotUIs;

        [Header("头盔:")]
        public HelmetSlotUI helmetSlotUI;

        [Header("胸甲:")]
        public TorsoSlotUI torsoSlotUI;

        [Header("腿甲:")]
        public LegSlotUI legSlotUI;

        [Header("臂甲:")]
        public HandSlotUI handSlotUI;

        //箭矢装备槽:
        [Header("箭矢:")]
        public AmmoSlotUI[] ammoSlotUIs = new AmmoSlotUI[4];

        [Header("戒指:")]
        public RingSlotUI[] ringSlotUIs = new RingSlotUI[4];

        public UIManager uiManager;

        private void Start()
        {
            //handEquipmentSlotUIs = FindObjectsOfType<HandEquipmentSlotUI>();
        }

        //加载武器库存到屏幕:
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

        //加载更换的消耗品装备到屏幕:
        public void LoadConsumableOnEquipmentScreen(PlayerInventoryManager playerInventory)
        {
            int index = uiManager.consumableInventorySlotSelected;
            for(int i = 0; i < playerInventory.consumableSlots.Length; i++)
            {
                //更新装备栏内的消耗品槽
                consumableSlotUIs[i].AddItem(playerInventory.consumableSlots[i]);
            }

            ConsumableItem currentConsumable = playerInventory.consumableSlots[playerInventory.currentConsumableIndex];
            //更新显示位置的消耗品巢
            uiManager.quickSlotUI.UpdateCurrentConsumableQuickSlotUI(currentConsumable);
        }
        //加载头盔装备到屏幕:
        public void LoadHelmetEquipmentOnEquipmentScreen(PlayerInventoryManager playerInventory)
        {
            helmetSlotUI.AddItem(playerInventory.currentHelmetEquipment);
        }

        //加载当前胸甲装备到屏幕
        public void LoadTorsoEquipmentOnEquipmentScreen(PlayerInventoryManager playerInventory)
        {
            torsoSlotUI.AddItem(playerInventory.currentTorsoEquipment);
        }

        //加载当前腿甲到屏幕
        public void LoadLegEquipmentOnEquipmentScreen(PlayerInventoryManager playerInventory)
        {
            legSlotUI.AddItem(playerInventory.currentLegEquipment);
        }

        //加载当前臂甲到屏幕
        public void LoadHandEquipmentOnEquipmentScreen(PlayerInventoryManager playerInventory)
        {
            handSlotUI.AddItem(playerInventory.currentHandEquipment);
        }

        //加载箭矢到屏幕:
        public void LoadAmmoEquipmentOnEquipmentScreen(PlayerInventoryManager playerInventory)
        {
            ammoSlotUIs[0].AddItem(playerInventory.currentBow);
            ammoSlotUIs[1].AddItem(playerInventory.spareBow);
            ammoSlotUIs[2].AddItem(playerInventory.otherAmmo);
            ammoSlotUIs[3].AddItem(playerInventory.spareOtherAmmo);
        }

        //加载戒指到屏幕:
        public void LoadRingEquipmentOnEquipmentScreen(PlayerInventoryManager playerInventory)
        {
            for(int i = 0; i < ringSlotUIs.Length; i++)
            {
                ringSlotUIs[i].AddItem(playerInventory.GetRingById(i));
            }
        }

        #region 右手装备
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

        #region 左手装备
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