using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class FortifiedElementBottleWindow : MonoBehaviour
    {
        [Header("���Ӹ�����ť:")]
        public Button redUpButton;
        public Button blueUpButton;
        [Header("�ᾧ�����ı�:")]
        int currentRedValue;
        public Text redValueText;       //Ԫ��ƿ�������
        int currentBlueValue;
        public Text blueValueText;     //Ԫ��ƿ�ظ���
        [Header("Ԫ�ؽᾧ:")]
        public ConsumableItem redElementCrystal;
        public ConsumableItem blueElementCrystal;
        [HideInInspector] public UIManager uiManager;

        //���ô���ʱ:
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
        //�������Ƿ���ڳ�ɫԪ�ؽᾧ:
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
        //�������Ƿ������ɫԪ�ؽᾧ:
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
        //������ɫ�ᾧ����:
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
        //���ӳ�ɫ�ᾧ����:
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


        //����ǿ��������رմ���:
        public void SaveEnhancementResult()
        {
            PlayerManager player = uiManager.playerManager;
            int inventoryAmount = HaveBlueElementCrystals(player);   //����е�����
            List<string> stringList = new List<string>();
            if (inventoryAmount != currentBlueValue)
                stringList.Add("����Ԫ��ƿ�ظ���");

            inventoryAmount = HaveRedElementCrystals(player);
            if (inventoryAmount != currentRedValue)
                stringList.Add("����Ԫ��ƿ������");

            uiManager.textPromptWindow.SetActive(true);
            uiManager.textPromptWindowManager.SetTextPrompt(stringList.ToArray(), 1.0f);

            StrengthenRedElementCrystals();
            StrengthenBlueElementCrystals();

            uiManager.campfireUIWindow.SetActive(true);
            gameObject.SetActive(false);
        }
        //ע���ɫԪ�ؽᾧ:
        private void StrengthenRedElementCrystals()
        {
            PlayerManager player = uiManager.playerManager;
            int inventoryAmount = HaveRedElementCrystals(player);   //����е�����

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
        //ע����ɫԪ�ؽᾧ:
        private void StrengthenBlueElementCrystals()
        {
            PlayerManager player = uiManager.playerManager;
            int inventoryAmount = HaveBlueElementCrystals(player);    //����е�����

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