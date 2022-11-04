using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class CommodityInventorySlot : MonoBehaviour
    {
        public UIManager uiManager;
        [Header("�������Ʒ:")]
        public Commodity thisSlotCommodity;
        [Header("��Ʒͼ��:")]
        public Image icon;
        public Image notAvailableIcon;
        [Header("��Ʒ��ֵ:")]
        public Text commodityValueText;
        [Header("��Ʒ����:")]
        public Text commodityAmountText;

        Button button;
        private void Awake()
        {
            button = GetComponent<Button>();
            if (uiManager == null)
                uiManager = FindObjectOfType<UIManager>();
        }

        public void SelectThisSlot()
        {
            uiManager.commodityTradingWindow.OpenTradingSelectCommodityWindow(thisSlotCommodity, transform as RectTransform);
        }
        public void AddCommodity(Commodity commodity)
        {
            if(commodity == null)
            {
                RemoveCommodity();
                return;
            }

            thisSlotCommodity = commodity;
            icon.sprite = commodity.item.itemIcon;
            commodityValueText.text = commodity.commodityPrice.ToString();
            //��Ʒ����:
            if (commodity.isInfinite)
            {
                commodityAmountText.text = "--";
            }
            else
            {
                commodityAmountText.text = commodity.commodityAmount.ToString("d2");
            }
            commodityAmountText.enabled = true;
            gameObject.SetActive(true);
        }
        public void RemoveCommodity()
        {
            thisSlotCommodity = null;
            icon.sprite = null;
            icon.enabled = false;
            notAvailableIcon.sprite = null;
            notAvailableIcon.enabled = false;
            commodityAmountText.enabled = false;

            gameObject.SetActive(false);
        }

        //�ɽ����ж�:(�жϸ���Ʒ�Ƿ��ܽ���)
        public void TradableJudgment(PlayerManager player)
        {
            if (thisSlotCommodity == null) return;

            if(thisSlotCommodity.itemType == ItemType.Spell)
            {
                SpellItem spell = thisSlotCommodity.item as SpellItem;
                bool isAvailable = player.playerStateManager.intelligenceLevel >= spell.requiredIntelligenceLevel && player.playerStateManager.faithLevel >= spell.requiredFaithLevel;
                if (isAvailable)
                    notAvailableIcon.enabled = false;
                else
                    notAvailableIcon.enabled = true;
            }
            else
            {
                notAvailableIcon.enabled = false;
            }
        }

        //���ð�ť
        public void DisableButton()
        {
            button.interactable = false;
        }
        //���ð�ť
        public void EnableButton()
        {
            button.interactable = true;
        }
    }
}