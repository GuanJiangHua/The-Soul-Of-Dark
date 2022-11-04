using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class CommodityTradingWindow : MonoBehaviour
    {
        [Header("�̵�����:")]
        public Text shopkeeperNameText;
        [Header("��Ʒ����:")]
        public Text commodityNameText;
        [Header("���ױ�����:")]
        public Text exchangeItemNameText;
        public Text currentExchangeItemAmountText;
        public Text needExchangeItemAmountText;
        [Header("��Ʒ����:")]
        public GameObject commodityInventorySlotPreform;    //������Ʒ��λԤ����
        public Transform commodityInventorySlotParent;          //���н�����Ʒ��λ�ĸ�����
        CommodityInventorySlot[] commodityInventorySlotArray;
        Vector2 commodityInventoryOriginalSize;
        Vector2 commodityInventoryOriginalPosition;
        [Header("���״���:")]
        public Commodity selectedCommodity;
        public GameObject tradingSelectWindow;
        public Text amountVslueText;                        //���׸���
        public Text priceValueText;                            //�۸�
        public Button dealButton;                              //�ɽ���ť

        int currentAmount = 0;                                  //��ǰ���׸���
        bool isThereTransaction = false;                   //�Ƿ�����˽���,�̵�����������һ��ʱ����;
        [Header("�ر��̵괰��:")]
        public GameObject endTransactionWindow;

        NpcManager shopowner;
        UIManager uiManager;
        private void Awake()
        {
            uiManager = FindObjectOfType<UIManager>();
            commodityInventoryOriginalSize = commodityInventorySlotParent.GetComponent<RectTransform>().sizeDelta;
            commodityInventoryOriginalPosition = commodityInventorySlotParent.GetComponent<RectTransform>().anchoredPosition;
        }

        #region ������Ʒ
        //�����̵����Ʒ:
        public void UpdateCommodityInventory(PlayerManager player,NpcManager npc)
        {
            if (npc != shopowner)
                isThereTransaction = false;

            //���õ���:
            shopowner = npc;
            //���½�����Ʒ�б�:
            List<Commodity> commoditieList = npc.storeManager.commoditieList;
            commodityInventorySlotArray = GetAllChildCommodityInventorySlot(commodityInventorySlotParent);
            for (int i = 0; i < commodityInventorySlotArray.Length; i++)
            {
                //���н�����Ʒ����,С���̵���Ʒ�����Ĳ���:
                if(i < commoditieList.Count)
                {
                    //������Ʒ��С����Ʒ����:
                    if(commodityInventorySlotArray.Length < commoditieList.Count)
                    {
                        Instantiate(commodityInventorySlotPreform, commodityInventorySlotParent);
                        commodityInventorySlotArray = GetAllChildCommodityInventorySlot(commodityInventorySlotParent);
                    }
                    commodityInventorySlotArray[i].AddCommodity(commoditieList[i]);
                    commodityInventorySlotArray[i].TradableJudgment(player);
                }
                else
                {
                    commodityInventorySlotArray[i].RemoveCommodity();
                }
            }

            //�����︸����Ĵ�С��λ��:(�̵괰���������5*4[��*��]20����Ʒ,���С�����÷Ŵ�)
            int amplifyScale =1 + (commoditieList.Count - 20) / 5;
            if(commoditieList.Count <= 20)
                amplifyScale = 0;
            float heightOccupied = commodityInventoryOriginalSize.y / 4; //����ÿ��ռ�ݴ��ڵĸ߶�;
            Vector2 newSize = new Vector2(commodityInventoryOriginalSize.x, commodityInventoryOriginalSize.y + heightOccupied * amplifyScale);
            Vector2 newPosition = new Vector2(commodityInventoryOriginalPosition.x, commodityInventoryOriginalPosition.y - heightOccupied / 2 * amplifyScale);
            commodityInventorySlotParent.GetComponent<RectTransform>().sizeDelta = newSize;
            commodityInventorySlotParent.GetComponent<RectTransform>().anchoredPosition = newPosition;

            //�����̵�����:
            shopkeeperNameText.text = npc.npcName + "���̵�";
            //���ý��װ�ť:
            AllDisableButton();
            //���ý��װ�ť:
            AllEnableButton();
        }
        private CommodityInventorySlot[] GetAllChildCommodityInventorySlot(Transform parent)
        {
            CommodityInventorySlot[] inventorySlotArray = new CommodityInventorySlot[parent.childCount];
            for(int i=0; i< inventorySlotArray.Length; i++)
            {
                inventorySlotArray[i] = parent.GetChild(i).GetComponent<CommodityInventorySlot>();
            }

            return inventorySlotArray;
        }
        #endregion

        #region ������Ʒ
        //�򿪽�����Ʒ����
        public void OpenTradingSelectCommodityWindow(Commodity commodity, RectTransform target)
        {
            if (commodity != null)
            {
                tradingSelectWindow.SetActive(true);                //���ô���:
                tradingSelectWindow.GetComponent<RectTransform>().position = target.position;   //��λ����Ŀ��

                currentAmount = 0;
                selectedCommodity = commodity;
                UpdateTransactionAmountAndPrice(true);       //������Ʒ����С����:

                //��������ȼ�����:(����������)
                uiManager.playerPropertiesWindowManager.UpdatePropertiesText(uiManager.playerManager.playerStateManager);
                //���ø���Ʒ��Ӧ�����鴰��:(����������)
                ProductDetailWindow(commodity);

                AllDisableButton();                                              //�������н�����Ʒ�İ�ť
            }
        }
        //���½�������,���㽻�׼۸�,�����Ƿ���ý���:
        public void UpdateTransactionAmountAndPrice(bool isUp)
        {
            //������������--------------
            if (isUp)
            {
                currentAmount++;
                //���׸������ܴ�����Ʒ���������Ǹ���Ʒ�����޵�:
                if (currentAmount > selectedCommodity.commodityAmount && selectedCommodity.isInfinite == false)
                    currentAmount = selectedCommodity.commodityAmount;
            }
            else
            {
                currentAmount--;
                if (currentAmount < 0)
                    currentAmount = 0;
            }
            //���׼۸����-------------
            int transactionPrice = currentAmount * selectedCommodity.commodityPrice;
            //�Ƿ���ý����ж�--------
            bool canTransaction = uiManager.playerManager.playerStateManager.currentSoulCount >= transactionPrice;  //������������Ʒ�۸�
            bool haveNecessaryItem = CheckForHaveNecessaryItem();                                                                                    //ӵ�н��ױ�����

            //ui������������:---------
            amountVslueText.text = currentAmount.ToString();
            priceValueText.text = transactionPrice.ToString();

            //�ɽ���ť
            if (canTransaction && haveNecessaryItem)
                dealButton.interactable = true;
            else
                dealButton.interactable = false;

            if (selectedCommodity.exchangeItem != null)
            {
                exchangeItemNameText.text = selectedCommodity.exchangeItem.itemName;
                needExchangeItemAmountText.text = "1";
                if (haveNecessaryItem)
                {
                    currentExchangeItemAmountText.color = Color.white;
                    currentExchangeItemAmountText.text = "1";
                }
                else
                {
                    currentExchangeItemAmountText.color = Color.red;
                    currentExchangeItemAmountText.text = "0";
                }
            }
            else
            {
                exchangeItemNameText.text = "--";
                needExchangeItemAmountText.text = "-";
                currentExchangeItemAmountText.color = Color.white;
                currentExchangeItemAmountText.text = "-";
            }
        }
        //����Ƿ��б�����Ʒ��
        private bool CheckForHaveNecessaryItem()
        {
            if(selectedCommodity.exchangeItem == null)
            {
                return true;
            }
            else
            {
                PlayerInventoryManager playerInventory = uiManager.playerManager.playerInventoryManager;
                switch (selectedCommodity.exchangeItemType)
                {
                    case ItemType.Weapon:
                        foreach(WeaponItem weapon in playerInventory.weaponInventory)
                        {
                            if(selectedCommodity.exchangeItem.itemName == weapon.itemName)
                            {
                                return true;
                            }
                        }
                        break;
                    case ItemType.Helmet:
                        foreach (HelmetEquipment helmet in playerInventory.headEquipmentInventory)
                        {
                            if (selectedCommodity.exchangeItem.itemName == helmet.itemName)
                            {
                                return true;
                            }
                        }
                        break;
                    case ItemType.Torso:
                        foreach (TorsoEquipment torso in playerInventory.torsoEquipmentInventory)
                        {
                            if (selectedCommodity.exchangeItem.itemName == torso.itemName)
                            {
                                return true;
                            }
                        }
                        break;
                    case ItemType.Leg:
                        foreach (LegEquipment leg in playerInventory.legEquipmentInventory)
                        {
                            if (selectedCommodity.exchangeItem.itemName == leg.itemName)
                            {
                                return true;
                            }
                        }
                        break;
                    case ItemType.Hand:
                        foreach (HandEquipment hand in playerInventory.handEquipmentInventory)
                        {
                            if (selectedCommodity.exchangeItem.itemName == hand.itemName)
                            {
                                return true;
                            }
                        }
                        break;
                    case ItemType.Spell:
                        foreach (SpellItem spell in playerInventory.spellleInventory)
                        {
                            if (selectedCommodity.exchangeItem.itemName == spell.itemName)
                            {
                                return true;
                            }
                        }
                        break;
                    case ItemType.Consumable:
                        foreach (ConsumableItem consumable in playerInventory.consumableInventory)
                        {
                            if (selectedCommodity.exchangeItem.itemName == consumable.itemName)
                            {
                                return true;
                            }
                        }
                        break;
                    case ItemType.Ring:
                        foreach (RingItem ring in playerInventory.ringInventory)
                        {
                            if (selectedCommodity.exchangeItem.itemName == ring.itemName)
                            {
                                return true;
                            }
                        }
                        break;
                    case ItemType.Ammo:
                        foreach (RangedAmmoItem ammo in playerInventory.rangedAmmoInventory)
                        {
                            if (selectedCommodity.exchangeItem.itemName == ammo.itemName)
                            {
                                return true;
                            }
                        }
                        break;
                }

                return false;
            }
        }
        //�ɽ�:
        public void Deal()
        {
            //����װ��:
            PlayerInventoryManager playerInventory = uiManager.playerManager.playerInventoryManager;
            switch (selectedCommodity.itemType)
            {
                case ItemType.Weapon:
                    for(int i = 0; i < currentAmount; i++)
                        playerInventory.weaponInventory.Add(selectedCommodity.item as WeaponItem);
                    break;
                case ItemType.Helmet:
                    for (int i = 0; i < currentAmount; i++)
                        playerInventory.headEquipmentInventory.Add(selectedCommodity.item as HelmetEquipment);
                    break;
                case ItemType.Torso:
                    for (int i = 0; i < currentAmount; i++)
                        playerInventory.torsoEquipmentInventory.Add(selectedCommodity.item as TorsoEquipment);
                    break;
                case ItemType.Leg:
                    for (int i = 0; i < currentAmount; i++)
                        playerInventory.legEquipmentInventory.Add(selectedCommodity.item as LegEquipment);
                    break;
                case ItemType.Hand:
                    for (int i = 0; i < currentAmount; i++)
                        playerInventory.handEquipmentInventory.Add(selectedCommodity.item as HandEquipment);
                    break;
                case ItemType.Spell:
                    foreach(SpellItem spell in playerInventory.spellleInventory)
                    {
                        if (selectedCommodity.item.itemName == spell.itemName)
                            return;
                    }
                    currentAmount = 1;          //����ֻ����һ��
                    playerInventory.spellleInventory.Add(selectedCommodity.item as SpellItem);
                    break;
                case ItemType.Consumable:
                    int amountInBackpack = 0;
                    foreach (ConsumableItem consumable in playerInventory.consumableInventory)
                    {
                        if (consumable.itemName == selectedCommodity.item.itemName)
                            amountInBackpack = consumable.currentItemAmount;
                    }

                    ConsumableItem newConsumable = Instantiate(selectedCommodity.item as ConsumableItem);

                    //�������������+��������>����Ʒ�����������;������=����Ʒ���������-����������;
                    if(amountInBackpack + currentAmount > newConsumable.maxItemAmount)
                    {
                        currentAmount = newConsumable.maxItemAmount - amountInBackpack;
                    }

                    newConsumable.currentItemAmount = currentAmount;
                    playerInventory.AddConsumableItemToInventory(newConsumable);
                    break;
                case ItemType.Ring:
                    for (int i = 0; i < currentAmount; i++)
                        playerInventory.ringInventory.Add(selectedCommodity.item as RingItem);
                    break;
                case ItemType.Ammo:
                    int ammoAmountInBackpack = 0;
                    foreach (RangedAmmoItem ammo in playerInventory.rangedAmmoInventory)
                    {
                        if (ammo.itemName == selectedCommodity.item.itemName)
                            ammoAmountInBackpack = ammo.currentAmount;
                    }

                    RangedAmmoItem newAmmo = Instantiate(selectedCommodity.item as RangedAmmoItem);

                    //�������������+��������>����Ʒ�����������;������=����Ʒ���������-����������;
                    if (ammoAmountInBackpack + currentAmount > newAmmo.carryLimit)
                    {
                        currentAmount = newAmmo.carryLimit - ammoAmountInBackpack;
                    }

                    newAmmo.currentAmount = currentAmount;
                    playerInventory.AddAmmoItemToInventory(newAmmo);
                    break;
            }

            //���㽻�׼۸�:
            int transactionPrice = currentAmount * selectedCommodity.commodityPrice;
            selectedCommodity.commodityAmount -= currentAmount;
            PlayerStatsManager playerStats = uiManager.playerManager.playerStateManager;
            playerStats.currentSoulCount -= transactionPrice;

            if (selectedCommodity.exchangeItem != null)
            {
                DeleteRequiredProps(selectedCommodity.exchangeItem, selectedCommodity.exchangeItemType, playerInventory);
            }
            //���й�����:
            isThereTransaction = true;

            //����װ����������:
            uiManager.CloseAllCentralWindow();
            uiManager.itemPropertiesWindow.SetActive(true);
            uiManager.itemPropertiesWindowManager.UpdateItemPropertiesWindow(null, 1);
            uiManager.playerPropertiesWindowManager.UpdatePropertiesText(playerStats);
            //�رմ���:
            selectedCommodity = null;
            tradingSelectWindow.SetActive(false);
            AllEnableButton();

            //�����̵�:
            UpdateCommodityInventory(uiManager.playerManager, shopowner);
        }
        //ȡ��
        public void Cancel()
        {
            //����:
            currentExchangeItemAmountText.color = Color.white;
            currentExchangeItemAmountText.text = "-";
            needExchangeItemAmountText.text = "-";
            exchangeItemNameText.text = "������Ʒ:";

            //����װ����������:
            uiManager.CloseAllCentralWindow();
            uiManager.itemPropertiesWindow.SetActive(true);
            uiManager.itemPropertiesWindowManager.UpdateItemPropertiesWindow(null, 1);
            uiManager.playerPropertiesWindowManager.UpdatePropertiesText(uiManager.playerManager.playerStateManager);
            //�رմ���:
            selectedCommodity = null;
            tradingSelectWindow.SetActive(false);
            AllEnableButton();

            //�����̵�:
            UpdateCommodityInventory(uiManager.playerManager, shopowner);
        }
        //ɾ���������:
        private void DeleteRequiredProps(Item item, ItemType itemType, PlayerInventoryManager playerInventory)
        {
            switch (itemType)
            {
                case ItemType.Weapon:
                    foreach(WeaponItem weapon in playerInventory.weaponInventory)
                    {
                        if(weapon.itemName == item.itemName)
                        {
                            playerInventory.weaponInventory.Remove(weapon);
                            break;
                        }
                    }
                    break;
                case ItemType.Helmet:
                    foreach (HelmetEquipment helmet in playerInventory.headEquipmentInventory)
                    {
                        if (helmet.itemName == item.itemName)
                        {
                            playerInventory.headEquipmentInventory.Remove(helmet);
                            break;
                        }
                    }
                    break;
                case ItemType.Torso:
                    foreach (TorsoEquipment torso in playerInventory.torsoEquipmentInventory)
                    {
                        if (torso.itemName == item.itemName)
                        {
                            playerInventory.torsoEquipmentInventory.Remove(torso);
                            break;
                        }
                    }
                    break;
                case ItemType.Leg:
                    foreach (LegEquipment leg in playerInventory.legEquipmentInventory)
                    {
                        if (leg.itemName == item.itemName)
                        {
                            playerInventory.legEquipmentInventory.Remove(leg);
                            break;
                        }
                    }
                    break;
                case ItemType.Hand:
                    foreach (HandEquipment hand in playerInventory.handEquipmentInventory)
                    {
                        if (hand.itemName == item.itemName)
                        {
                            playerInventory.handEquipmentInventory.Remove(hand);
                            break;
                        }
                    }
                    break;
                case ItemType.Spell:
                    SpellItem toDestroy = null;
                    foreach (SpellItem spell in playerInventory.spellleInventory)
                    {
                        if (spell.itemName == item.itemName)
                        {
                            toDestroy = spell;
                            break;
                        }
                    }

                    for(int i = 0; i < playerInventory.memorySpellArray.Length; i++)
                    {
                        if(playerInventory.memorySpellArray[i] != null && playerInventory.memorySpellArray[i].itemName == toDestroy.itemName)
                        {
                            playerInventory.memorySpellArray[i] = null;
                        }
                    }
                    break;
                case ItemType.Consumable:
                    foreach(ConsumableItem consumable in playerInventory.consumableInventory)
                    {
                        if (consumable.itemName == item.itemName)
                        {
                            consumable.currentItemAmount--;
                            break;
                        }
                    }
                    break;
                case ItemType.Ring:
                    foreach(RingItem ring in playerInventory.ringInventory)
                    {
                        if (ring.itemName == item.itemName)
                        {
                            playerInventory.ringInventory.Remove(ring);
                            break;
                        }
                    }
                    break;
                case ItemType.Ammo:
                    foreach (RangedAmmoItem ammo in playerInventory.rangedAmmoInventory)
                    {
                        if (ammo.itemName == item.itemName)
                        {
                            ammo.currentAmount--;
                            break;
                        }
                    }
                    break;
            }
        }

        //����Ʒ���鴰��:
        private void ProductDetailWindow(Commodity commodity)
        {
            PlayerManager player = uiManager.playerManager;
            PlayerInventoryManager playerInventory = player.playerInventoryManager;
            switch (commodity.itemType)
            {
                case ItemType.Weapon:
                    uiManager.CloseAllCentralWindow();
                    uiManager.weaponPropertiesWindow.SetActive(true);
                    uiManager.weapentPropertiesWindowManager.UpdateUIWindow(commodity.item as WeaponItem, playerInventory);
                    break;
                case ItemType.Helmet:
                    uiManager.CloseAllCentralWindow();
                    uiManager.equipmentPropertiesWindow.SetActive(true);
                    uiManager.equipmentPropertiesWindowManager.UpdateUIWindow(commodity.item as EquipmentItem, playerInventory);
                    break;
                case ItemType.Torso:
                    uiManager.CloseAllCentralWindow();
                    uiManager.equipmentPropertiesWindow.SetActive(true);
                    uiManager.equipmentPropertiesWindowManager.UpdateUIWindow(commodity.item as EquipmentItem, playerInventory);
                    break;
                case ItemType.Leg:
                    uiManager.CloseAllCentralWindow();
                    uiManager.equipmentPropertiesWindow.SetActive(true);
                    uiManager.equipmentPropertiesWindowManager.UpdateUIWindow(commodity.item as EquipmentItem, playerInventory);
                    break;
                case ItemType.Hand:
                    uiManager.CloseAllCentralWindow();
                    uiManager.equipmentPropertiesWindow.SetActive(true);
                    uiManager.equipmentPropertiesWindowManager.UpdateUIWindow(commodity.item as EquipmentItem, playerInventory);
                    break;
                case ItemType.Spell:
                    uiManager.CloseAllCentralWindow();
                    uiManager.itemPropertiesWindow.SetActive(true);
                    uiManager.itemPropertiesWindowManager.UpdateItemPropertiesWindow(commodity.item, 9);
                    break;
                case ItemType.Consumable:
                    uiManager.CloseAllCentralWindow();
                    uiManager.itemPropertiesWindow.SetActive(true);
                    uiManager.itemPropertiesWindowManager.UpdateItemPropertiesWindow(commodity.item, 1);
                    break;
                case ItemType.Ring:
                    uiManager.CloseAllCentralWindow();
                    uiManager.itemPropertiesWindow.SetActive(true);
                    uiManager.itemPropertiesWindowManager.UpdateItemPropertiesWindow(commodity.item, 8);
                    uiManager.playerPropertiesWindowManager.UpdatePropertiesText(player.playerStateManager, commodity.item as RingItem);
                    RingItem ring = commodity.item as RingItem;
                    Debug.Log("�����ӳ�:" + ring.staminaLevelAddition + " ���ӳ�:"+ring.soulsRewardLevelAddition);
                    break;
                case ItemType.Ammo:
                    uiManager.CloseAllCentralWindow();
                    uiManager.weaponPropertiesWindow.SetActive(true);
                    uiManager.weapentPropertiesWindowManager.UpdateUIWindow(commodity.item as RangedAmmoItem, playerInventory, true);
                    break;
            }
        }
        //�������н����ﰴť��
        private void AllDisableButton()
        {
            commodityInventorySlotArray = GetAllChildCommodityInventorySlot(commodityInventorySlotParent);
            foreach(var commodityInventorySlot in commodityInventorySlotArray)
            {
                commodityInventorySlot.DisableButton();
            }
        }
        //�������н��װ�ť:
        private void AllEnableButton()
        {
            commodityInventorySlotArray = GetAllChildCommodityInventorySlot(commodityInventorySlotParent);
            foreach (var commodityInventorySlot in commodityInventorySlotArray)
            {
                if (commodityInventorySlot.thisSlotCommodity != null)
                {
                    if(commodityInventorySlot.thisSlotCommodity.commodityAmount>0 || commodityInventorySlot.thisSlotCommodity.isInfinite)
                        commodityInventorySlot.EnableButton();
                }
            }
        }
        #endregion

        #region ��������:
        //�򿪽������״���:
        public void EnableEndTransactionWindow()
        {
            //����һ��"ȡ��"
            Cancel();
            //����������Ʒ��ť
            AllDisableButton();
            //�򿪽������״���
            endTransactionWindow.SetActive(true);
        }
        //�رս������״���:
        public void CloseEndTransactionWindow()
        {
            //������Ʒ���װ�ť:
            AllEnableButton();
            //�رս������״���:
            endTransactionWindow.SetActive(false);
        }
        //��������:
        public void CloseTheTransaction()
        {
            uiManager.CloseAllLeftWindow();
            uiManager.CloseAllCentralWindow();
            uiManager.CloseAllRightWindow();

            uiManager.hudWindow.SetActive(true);

            //���Ž����Ի�:
            uiManager.dialogSystemWindow.SetActive(true);
            if (isThereTransaction)
            {
                uiManager.dialogSystemWindowManager.GiveDialogueContentToUI(shopowner.storeManager.postPurchaseDialogue, true);
            }
            else
            {
                uiManager.dialogSystemWindowManager.GiveDialogueContentToUI(shopowner.storeManager.notPurchasedDialogue, true);
            }
            //��ҽ���״̬����:
            uiManager.playerManager.isTrading = false;

            //�رս������״���:
            endTransactionWindow.SetActive(false);
        }
        #endregion
    }
}