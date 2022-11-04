 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class QuickSlotUI : MonoBehaviour
    {
        public WeaponItem unarmedWeapon;

        public Image currentSpellIcon;                  //法术图片;
        public Image leftWeaponIcon;                  //左手武器图片;
        public Image rightWeaponIcon;                //右手武器图片;
        [Header("消耗品:")]
        public Image currentConsumableIcon;     //当前消耗品图片;
        public Text currentValueText;
        public Text maxValueText;
        [Header("未满足法术条件图标:")]
        public Image unqualifiedSpell;
        //更新武器槽ui图片:
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

        //更新消耗品槽ui图片:
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

        //更新法术槽ui图片:
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
