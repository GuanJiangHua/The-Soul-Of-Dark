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
        [Header("�Ѿ�װ��:")]
        public bool isEquipment = false;
        public Image currentEquipmentIcon;
        public Image unqualifiedIcon;

        public UIManager uiManager;
        //����:---------
        private void Awake()
        {
            button = icon.GetComponent<Button>();
            if(uiManager == null)
                uiManager = FindObjectOfType<UIManager>();
        }
        //�����ѡ�������λ:
        public void SelectThisSlot()
        {
            //����װ�����ô���:
            RectTransform rectTransform = icon.GetComponent<RectTransform>();
            uiManager.campfireUIWindowManager.memorySpellWindowManaget.OpenEquipmentDisposalWindow(rectTransform, this);
            //ʹ�����ƶ�������۵�λ��:
            //ʹ�����Ĳ۵İ�ť���ɽ���:
            //���ü��䷨�����ڵ�"��ǰѡ����"Ϊ���۵ķ���:
        }

        //��ӷ���������:
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
        //�Ƴ���ǰ����:
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

        //���ñ�������ѡ��ť:
        public void EnableSelectionButton()
        {
            button.interactable = true;
        }
        //���ñ�������ѡ��ť:
        public void DisableSelectionButton()
        {
            button.interactable = false;
        }

        //������ʱ:[���������е���]
        public void MouseEntry()
        {
            //������id��:[9]
            uiManager.itemPropertiesWindowManager.UpdateItemPropertiesWindow(mySpell, 9);
            uiManager.playerPropertiesWindowManager.UpdatePropertiesText(uiManager.playerManager.playerInventoryManager.GetComponent<PlayerStatsManager>());
        }
        //����Ƴ�ʱ:[���������е���]
        public void MouseOut()
        {
            //������id��:[9]
            uiManager.itemPropertiesWindowManager.UpdateItemPropertiesWindow(null, 9);
            uiManager.playerPropertiesWindowManager.UpdatePropertiesText(uiManager.playerManager.playerInventoryManager.GetComponent<PlayerStatsManager>());
        }
    }
}