using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class SpellInventorySlot : MonoBehaviour
    {
        public SpellItem mySpell;
        public Image icon;

        Button button;
        [Header("已经装备:")]
        public bool isEquipment = false;
        public Image currentEquipmentIcon;
        public Image unqualifiedIcon;

        public UIManager uiManager;
        //方法:---------
        private void Awake()
        {
            button = icon.GetComponent<Button>();
            if(uiManager == null)
                uiManager = FindObjectOfType<UIManager>();
        }
        //鼠标点击选择这个槽位:
        public void SelectThisSlot()
        {
            //启用装备处置窗口:
            RectTransform rectTransform = icon.GetComponent<RectTransform>();
            uiManager.campfireUIWindowManager.memorySpellWindowManaget.OpenEquipmentDisposalWindow(rectTransform, this);
            //使窗口移动到这个槽的位置:
            //使其他的槽的按钮不可交互:
            //设置记忆法术窗口的"当前选择法术"为本槽的法术:
        }

        //添加法术到本槽:
        public void AddSpell(SpellItem spell, bool isEquipmentIcon)
        {
            if(spell == null)
            {
                RemoveSpell();
                return;
            }

            mySpell = spell;
            icon.enabled = true;
            icon.sprite = spell.itemIcon;

            if(button!=null)
                button.enabled = true;

            if (isEquipmentIcon)
            {
                isEquipment = true;
                if(currentEquipmentIcon!=null)
                    currentEquipmentIcon.gameObject.SetActive(true);
            }
            else
            {
                isEquipment = false;
                if (currentEquipmentIcon != null)
                    currentEquipmentIcon.gameObject.SetActive(false);
            }

            bool isUnqualified = 
                uiManager.playerManager.playerStateManager.intelligenceLevel > spell.requiredIntelligenceLevel 
                && uiManager.playerManager.playerStateManager.faithLevel > spell.requiredFaithLevel;

            if (isUnqualified)
            {
                if(unqualifiedIcon!=null)
                    unqualifiedIcon.enabled = false;
                if (currentEquipmentIcon != null)
                    currentEquipmentIcon.color = new Color(1, 1, 1, 1);
            }
        }
        //移除当前法术:
        public void RemoveSpell(bool isActive = true)
        {
            mySpell = null;
            icon.enabled = false;
            icon.sprite = null;

            if (button != null)
                button.enabled = false;

            isEquipment = false;

            if(unqualifiedIcon!=null)
                unqualifiedIcon.enabled = false;
            if (currentEquipmentIcon != null)
                currentEquipmentIcon.gameObject.SetActive(false);

            gameObject.SetActive(isActive);
        }

        //启用本法术的选择按钮:
        public void EnableSelectionButton()
        {
            button.interactable = true;
        }
        //禁用本法术的选择按钮:
        public void DisableSelectionButton()
        {
            button.interactable = false;
        }

        //鼠标进入时:[背包窗口中调用]
        public void MouseEntry()
        {
            //法术的id是:[9]
            uiManager.itemPropertiesWindowManager.UpdateItemPropertiesWindow(mySpell, 9);
            uiManager.playerPropertiesWindowManager.UpdatePropertiesText(uiManager.playerManager.playerInventoryManager.GetComponent<PlayerStatsManager>());
        }
        //鼠标移出时:[背包窗口中调用]
        public void MouseOut()
        {
            //法术的id是:[9]
            uiManager.itemPropertiesWindowManager.UpdateItemPropertiesWindow(null, 9);
            uiManager.playerPropertiesWindowManager.UpdatePropertiesText(uiManager.playerManager.playerInventoryManager.GetComponent<PlayerStatsManager>());
        }
    }
}