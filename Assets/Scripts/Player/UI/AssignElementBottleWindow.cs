using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class AssignElementBottleWindow : MonoBehaviour
    {
        public UIManager uiManager;
        public Text numberElementBottleText;
        public Text numberElementGrayBottleText;

        //����Ԫ��ƿ����:
        public void RestoreElementBottle(PlayerManager player)
        {
            foreach (ConsumableItem consumable in player.playerInventoryManager.consumableInventory)
            {
                if (consumable.itemName == "Ԫ��ƿ")
                {
                    consumable.maxItemAmount = player.numberElement;
                    consumable.currentItemAmount = consumable.maxItemAmount;
                    break;
                }
            }

            foreach (ConsumableItem consumable in player.playerInventoryManager.consumableInventory)
            {
                if (consumable.itemName == "Ԫ�ػ�ƿ")
                {
                    consumable.maxItemAmount = player.totalNumberElementBottle - player.numberElement;
                    consumable.currentItemAmount = consumable.maxItemAmount;
                    break;
                }
            }

            foreach (ConsumableItem consumable in player.playerInventoryManager.consumableSlots)
            {
                if (consumable != null && consumable.itemName == "Ԫ��ƿ")
                {
                    consumable.maxItemAmount = player.numberElement;
                    consumable.currentItemAmount = consumable.maxItemAmount;
                    break;
                }
            }

            foreach (ConsumableItem consumable in player.playerInventoryManager.consumableSlots)
            {
                if (consumable != null && consumable.itemName == "Ԫ�ػ�ƿ")
                {
                    consumable.maxItemAmount = player.totalNumberElementBottle - player.numberElement;
                    consumable.currentItemAmount = consumable.maxItemAmount;
                    break;
                }
            }

            //���ÿ���װ������:
            player.uiManager.quickSlotUI.UpdateCurrentConsumableQuickSlotUI(player.playerInventoryManager.currentConsumable);
        }
        //����Ԫ��ƿ����:
        public void DistributionElementBottle(bool isUp)
        {
            PlayerManager player = uiManager.playerManager;
            if (isUp)
            {
                player.numberElement++;
                if (player.numberElement > player.totalNumberElementBottle)
                    player.numberElement = player.totalNumberElementBottle;
            }
            else
            {
                player.numberElement--;
                if (player.numberElement < 0)
                    player.numberElement = 0;
            }

            numberElementBottleText.text = player.numberElement.ToString();
            numberElementGrayBottleText.text = (player.totalNumberElementBottle - player.numberElement).ToString();
        }
        //���÷���Ԫ��ƿ����:
        public void EnableAssignElementBottleWindow()
        {
            gameObject.SetActive(true);
            numberElementBottleText.text = uiManager.playerManager.numberElement.ToString();
            numberElementGrayBottleText.text = (uiManager.playerManager.totalNumberElementBottle - uiManager.playerManager.numberElement).ToString();
        }
        //�رմ��ڲ����ַ�����:
        public void CloseAssignElementBottleWindow()
        {
            //��ʾ���������:
            uiManager.textPromptWindow.SetActive(true);
            uiManager.textPromptWindowManager.SetTextPrompt("�ѱ��������", 1);
            //����Ԫ��ƿ:
            RestoreElementBottle(uiManager.playerManager);
            //�رմ���:
            gameObject.SetActive(false);
            //�������𴰿�:
            uiManager.EnableCampfireUIWindow();                                                               //���������ܴ���;
        }
        public void CloseAssignElementBottleWindow(bool haveInput)
        {
            //�رմ���:
            gameObject.SetActive(false);
        }
    }
}