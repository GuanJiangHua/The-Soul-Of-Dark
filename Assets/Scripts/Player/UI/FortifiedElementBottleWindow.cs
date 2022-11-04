using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class FortifiedElementBottleWindow : MonoBehaviour
    {
        [Header("增加个数按钮:")]
        public Button redUpButton;
        public Button blueUpButton;
        [Header("结晶数量文本:")]
        int currentRedValue;
        public Text redValueText;       //元素瓶最大数量
        int currentBlueValue;
        public Text blueValueText;     //元素瓶回复量
        [Header("元素结晶:")]
        public ConsumableItem redElementCrystal;
        public ConsumableItem blueElementCrystal;
        [HideInInspector] public UIManager uiManager;

        //启用窗口时:
        public void EnableFortifiedElementBottleWindow(PlayerManager player, UIManager uiManager)
        {
            this.uiManager = uiManager;
            currentRedValue = HaveRedElementCrystals(player);
            redValueText.text = player.totalNumberElementBottle.ToString();
            currentBlueValue = HaveBlueElementCrystals(player);
            blueValueText.text = player.restoreHealthLevel.ToString();
            if (currentRedValue > 0)
                redUpButton.interactable = true;
            else
                redUpButton.interactable = false;

            if (currentBlueValue > 0)
                blueUpButton.interactable = true;
            else
                blueUpButton.interactable = false;
        }
        //背包内是否存在赤色元素结晶:
        public int HaveRedElementCrystals(PlayerManager player)
        {
            List<ConsumableItem> consumableList = player.playerInventoryManager.consumableInventory;
            foreach(ConsumableItem item in consumableList)
            {
                if(item.itemName == redElementCrystal.itemName)
                {
                    return item.currentItemAmount;
                }
            }
            return 0;
        }
        //背包内是否存在蓝色元素结晶:
        public int HaveBlueElementCrystals(PlayerManager player)
        {
            List<ConsumableItem> consumableList = player.playerInventoryManager.consumableInventory;
            foreach (ConsumableItem item in consumableList)
            {
                if (item.itemName == blueElementCrystal.itemName)
                {
                    return item.currentItemAmount;
                }
            }
            return 0;
        }
        //增加蓝色结晶数量:
        public void AddBlueElementCrystalsValue()
        {
            int currentBlueText = int.Parse(blueValueText.text);
            if (currentBlueValue > 0)
            {
                currentBlueText++;
                blueValueText.text = currentBlueText.ToString();

                currentBlueValue--;
            }

            if (currentBlueValue <= 0)
            {
                blueUpButton.interactable = false;
            }
        }
        //增加赤色结晶数量:
        public void AddRedElementCrystalsValue()
        {
            int currentRedText = int.Parse(redValueText.text);
            if(currentRedValue > 0)
            {
                currentRedText++;
                redValueText.text = currentRedText.ToString();

                currentRedValue--;
            }

            if (currentRedValue <= 0)
            {
                redUpButton.interactable = false;
            }
        }


        //保存强化结果并关闭窗口:
        public void SaveEnhancementResult()
        {
            PlayerManager player = uiManager.playerManager;
            int inventoryAmount = HaveBlueElementCrystals(player);   //库存中的数量
            List<string> stringList = new List<string>();
            if (inventoryAmount != currentBlueValue)
                stringList.Add("增加元素瓶回复量");

            inventoryAmount = HaveRedElementCrystals(player);
            if (inventoryAmount != currentRedValue)
                stringList.Add("增加元素瓶最大个数");

            uiManager.textPromptWindow.SetActive(true);
            uiManager.textPromptWindowManager.SetTextPrompt(stringList.ToArray(), 1.0f);

            StrengthenRedElementCrystals();
            StrengthenBlueElementCrystals();

            uiManager.campfireUIWindow.SetActive(true);
            gameObject.SetActive(false);
        }
        //注入赤色元素结晶:
        private void StrengthenRedElementCrystals()
        {
            PlayerManager player = uiManager.playerManager;
            int inventoryAmount = HaveRedElementCrystals(player);   //库存中的数量

            player.totalNumberElementBottle += inventoryAmount - currentRedValue;

            if (uiManager.playerManager.totalNumberElementBottle > 15)
                uiManager.playerManager.totalNumberElementBottle = 15;

            foreach(ConsumableItem item in player.playerInventoryManager.consumableInventory)
            {
                if(item.itemName == redElementCrystal.itemName)
                {
                    item.currentItemAmount = currentRedValue;
                }
            }
        }
        //注入蓝色元素结晶:
        private void StrengthenBlueElementCrystals()
        {
            PlayerManager player = uiManager.playerManager;
            int inventoryAmount = HaveBlueElementCrystals(player);    //库存中的数量

            if (uiManager.playerManager.restoreHealthLevel > 15)
                uiManager.playerManager.restoreHealthLevel = 15;

            player.restoreHealthLevel += inventoryAmount - currentBlueValue;
            Debug.Log(inventoryAmount);
            foreach (ConsumableItem item in uiManager.playerManager.playerInventoryManager.consumableInventory)
            {
                if (item.itemName == blueElementCrystal.itemName)
                {
                    item.currentItemAmount = currentBlueValue;
                }
            }
        }
    }
}