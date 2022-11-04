 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class QuickSlotUI : MonoBehaviour
    {
        public WeaponItem unarmedWeapon;

        public Image currentSpellIcon;                  //����ͼƬ;
        public Image leftWeaponIcon;                  //��������ͼƬ;
        public Image rightWeaponIcon;                //��������ͼƬ;
        [Header("����Ʒ:")]
        public Image currentConsumableIcon;     //��ǰ����ƷͼƬ;
        public Text currentValueText;
        public Text maxValueText;
        [Header("δ���㷨������ͼ��:")]
        public Image unqualifiedSpell;
        //����������uiͼƬ:
        public void UpdateWeapomQuickSlotUI(WeaponItem weapon,bool isLeft)
        {
            if (weapon == null) weapon = unarmedWeapon;

            if (isLeft == false)
            {
                if(weapon.itemIcon != null)
                {
                    rightWeaponIcon.sprite = weapon.itemIcon;
                    rightWeaponIcon.enabled = true;
                }
                else
                {
                    rightWeaponIcon.sprite = null;
                    rightWeaponIcon.enabled = false;
                }
            }
            else
            {
                if(weapon.itemIcon != null)
                {
                    leftWeaponIcon.sprite = weapon.itemIcon;
                    leftWeaponIcon.enabled = true;
                }
                else
                {
                    leftWeaponIcon.sprite = null;
                    leftWeaponIcon.enabled = false;
                }
            }
        }

        //��������Ʒ��uiͼƬ:
        public void UpdateCurrentConsumableQuickSlotUI(ConsumableItem consumableItem)
        {
            if(consumableItem != null)
            {
                currentConsumableIcon.sprite = consumableItem.itemIcon;
                currentConsumableIcon.enabled = true;
                currentValueText.text = consumableItem.currentItemAmount.ToString();
                maxValueText.text = consumableItem.maxItemAmount.ToString();

                if (consumableItem.currentItemAmount <= 0)
                    currentValueText.color = Color.red;
                else
                    currentValueText.color = Color.white;
            }
            else
            {
                currentConsumableIcon.sprite = null;
                currentConsumableIcon.enabled = false;

                maxValueText.text = "-";
                currentValueText.text = "-";
                currentValueText.color = Color.white;
            }
        }

        //���·�����uiͼƬ:
        public void UpdateCurrentSpellIconQuickSlotUI(SpellItem spellIcon , PlayerStatsManager playerStats)
        {
            if (spellIcon != null)
            {
                currentSpellIcon.sprite = spellIcon.itemIcon;
                currentSpellIcon.enabled = true;
                bool isMeetCondition = playerStats.intelligenceLevel >= spellIcon.requiredIntelligenceLevel && playerStats.faithLevel >= spellIcon.requiredFaithLevel;
                if(isMeetCondition == true)
                {
                    unqualifiedSpell.enabled = false;
                    currentSpellIcon.color = new Color(1, 1, 1, 1);
                }
                else
                {
                    unqualifiedSpell.enabled = true;
                    currentSpellIcon.color = new Color(0.5f, 0.5f, 0.5f, 1);
                }
            }
            else
            {
                currentSpellIcon.sprite = null;
                currentSpellIcon.enabled = false;
                unqualifiedSpell.enabled = false;
                currentSpellIcon.color = new Color(1, 1, 1, 1);
            }
        }
    }
}
