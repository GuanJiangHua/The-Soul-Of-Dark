using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class ItemPropertiesWindow : MonoBehaviour
    {
        [Header("��Ʒ����:")]
        public Text itemName;
        [Header("��Ʒͼ��:")]
        public Image icon;
        [Header("�����ı�:")]
        public Text introduction;
        [Header("���ܽ����ı�:")]
        public Text functionIntroduction;

        //��Ʒ����id: 1[����Ʒ],2[����],3[ͷ��],4[�ؼ�],5[�ȼ�],6[�ۼ�],7[��ҩ],8[��ָ],9[����]
        public void UpdateItemPropertiesWindow(Item item , int itemTypeID)
        {
            if(item != null)
            {
                if(itemTypeID == 1)
                {
                    UpdateConsumableItemToWindow(item);
                }
                else if(itemTypeID == 2)
                {
                    UpdateWeaponItemToWindow(item);
                }
                else if(itemTypeID == 3)
                {
                    UpdateHelmetItemToWindow(item);
                }
                else if (itemTypeID == 4)
                {
                    UpdateTorsoItemToWindow(item);
                }
                else if(itemTypeID == 5)
                {
                    UpdateLegItemToWindow(item);
                }
                else if (itemTypeID == 6)
                {
                    UpdateHandItemToWindow(item);
                }
                else if (itemTypeID == 7)
                {
                    UpdateAmmoItemToWindow(item);
                }
                else if (itemTypeID == 8)
                {
                    UpdateRingItemToWindow(item);
                }
                else if(itemTypeID == 9)
                {
                    UpdateSpelltemToWindow(item);
                }
            }
            else
            {
                itemName.text = "��Ʒ����";
                icon.sprite = null;
                introduction.text = "��Ʒ����--";
                functionIntroduction.text = "���ܽ���--";

                icon.enabled = false;
            }
        }

        private void UpdateConsumableItemToWindow(Item item)
        {
            ConsumableItem consumable = (ConsumableItem)item;
            itemName.text = consumable.itemName;
            icon.sprite = consumable.itemIcon;
            introduction.text = consumable.consumableDescription;
            functionIntroduction.text = consumable.functionDescription;

            icon.enabled = true;
        }

        private void UpdateWeaponItemToWindow(Item item)
        {
            WeaponItem weapon = (WeaponItem)item;
            itemName.text = weapon.itemName;
            icon.sprite = weapon.itemIcon;
            introduction.text = weapon.weaponDescription;
            functionIntroduction.text = weapon.functionDescription;

            icon.enabled = true;
        }

        private void UpdateHelmetItemToWindow(Item item)
        {
            HelmetEquipment helmet = (HelmetEquipment)item;
            itemName.text = helmet.itemName;
            icon.sprite = helmet.itemIcon;
            introduction.text = helmet.equipmentIDescription;
            functionIntroduction.text = helmet.functionDescription;

            icon.enabled = true;
        }

        private void UpdateTorsoItemToWindow(Item item)
        {
            TorsoEquipment torso = (TorsoEquipment)item;
            itemName.text = torso.itemName;
            icon.sprite = torso.itemIcon;
            introduction.text = torso.equipmentIDescription;
            functionIntroduction.text = torso.functionDescription;

            icon.enabled = true;
        }

        private void UpdateLegItemToWindow(Item item)
        {
            LegEquipment torso = (LegEquipment)item;
            itemName.text = torso.itemName;
            icon.sprite = torso.itemIcon;
            introduction.text = torso.equipmentIDescription;
            functionIntroduction.text = torso.functionDescription;

            icon.enabled = true;
        }

        private void UpdateHandItemToWindow(Item item)
        {
            HandEquipment torso = (HandEquipment)item;
            itemName.text = torso.itemName;
            icon.sprite = torso.itemIcon;
            introduction.text = torso.equipmentIDescription;
            functionIntroduction.text = torso.functionDescription;

            icon.enabled = true;
        }

        private void UpdateAmmoItemToWindow(Item item)
        {
            if(item != null)
            {
                RangedAmmoItem ring = (RangedAmmoItem)item;
                itemName.text = ring.itemName;
                icon.sprite = ring.itemIcon;
                introduction.text = ring.weaponDescription;
                functionIntroduction.text = ring.functionDescription;

                icon.enabled = true;
            }
        }
    
        private void UpdateRingItemToWindow(Item item)
        {
            if (item != null)
            {
                RingItem ring = (RingItem)item;
                itemName.text = ring.itemName;
                icon.sprite = ring.itemIcon;
                introduction.text = ring.equipmentIDescription;
                functionIntroduction.text = ring.functionDescription;

                icon.enabled = true;
            }
        }

        private void UpdateSpelltemToWindow(Item item)
        {
            if (item != null)
            {
                SpellItem spell = (SpellItem)item;
                itemName.text = spell.itemName;
                icon.sprite = spell.itemIcon;
                introduction.text = spell.spellDescription;
                functionIntroduction.text = spell.functionDescription;

                icon.enabled = true;
            }
        }
    }
} 