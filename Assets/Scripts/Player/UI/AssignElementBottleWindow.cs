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

        //重置元素瓶方法:
        public void RestoreElementBottle(PlayerManager player)
        {
            foreach (ConsumableItem consumable in player.playerInventoryManager.consumableInventory)
            {
                if (consumable.itemName == "元素瓶")
                {
                    consumable.maxItemAmount = player.numberElement;
                    consumable.currentItemAmount = consumable.maxItemAmount;
                    break;
                }
            }

            foreach (ConsumableItem consumable in player.playerInventoryManager.consumableInventory)
            {
                if (consumable.itemName == "元素灰瓶")
                {
                    consumable.maxItemAmount = player.totalNumberElementBottle - player.numberElement;
                    consumable.currentItemAmount = consumable.maxItemAmount;
                    break;
                }
            }

            foreach (ConsumableItem consumable in player.playerInventoryManager.consumableSlots)
            {
                if (consumable != null && consumable.itemName == "元素瓶")
                {
                    consumable.maxItemAmount = player.numberElement;
                    consumable.currentItemAmount = consumable.maxItemAmount;
                    break;
                }
            }

            foreach (ConsumableItem consumable in player.playerInventoryManager.consumableSlots)
            {
                if (consumable != null && consumable.itemName == "元素灰瓶")
                {
                    consumable.maxItemAmount = player.totalNumberElementBottle - player.numberElement;
                    consumable.currentItemAmount = consumable.maxItemAmount;
                    break;
                }
            }

            //重置快速装备窗口:
            player.uiManager.quickSlotUI.UpdateCurrentConsumableQuickSlotUI(player.playerInventoryManager.currentConsumable);
        }
        //分配元素瓶方法:
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
        //启用分配元素瓶窗口:
        public void EnableAssignElementBottleWindow()
        {
            gameObject.SetActive(true);
            numberElementBottleText.text = uiManager.playerManager.numberElement.ToString();
            numberElementGrayBottleText.text = (uiManager.playerManager.totalNumberElementBottle - uiManager.playerManager.numberElement).ToString();
        }
        //关闭窗口并保持分配结果:
        public void CloseAssignElementBottleWindow()
        {
            //提示保存分配结果:
            uiManager.textPromptWindow.SetActive(true);
            uiManager.textPromptWindowManager.SetTextPrompt("已保存分配结果", 1);
            //重置元素瓶:
            RestoreElementBottle(uiManager.playerManager);
            //关闭窗口:
            gameObject.SetActive(false);
            //启用篝火窗口:
            uiManager.EnableCampfireUIWindow();                                                               //启用篝火功能窗口;
        }
        public void CloseAssignElementBottleWindow(bool haveInput)
        {
            //关闭窗口:
            gameObject.SetActive(false);
        }
    }
}