using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class CommodityTradingWindow : MonoBehaviour
    {
        [Header("商店名称:")]
        public Text shopkeeperNameText;
        [Header("商品名称:")]
        public Text commodityNameText;
        [Header("交易必须物:")]
        public Text exchangeItemNameText;
        public Text currentExchangeItemAmountText;
        public Text needExchangeItemAmountText;
        [Header("商品窗口:")]
        public GameObject commodityInventorySlotPreform;    //交易物品槽位预制体
        public Transform commodityInventorySlotParent;          //所有交易物品槽位的父物体
        CommodityInventorySlot[] commodityInventorySlotArray;
        Vector2 commodityInventoryOriginalSize;
        Vector2 commodityInventoryOriginalPosition;
        [Header("交易窗口:")]
        public Commodity selectedCommodity;
        public GameObject tradingSelectWindow;
        public Text amountVslueText;                        //交易个数
        public Text priceValueText;                            //价格
        public Button dealButton;                              //成交按钮

        int currentAmount = 0;                                  //当前交易个数
        bool isThereTransaction = false;                   //是否进行了交易,商店主不等于上一个时重置;
        [Header("关闭商店窗口:")]
        public GameObject endTransactionWindow;

        NpcManager shopowner;
        UIManager uiManager;
        private void Awake()
        {
            uiManager = FindObjectOfType<UIManager>();
            commodityInventoryOriginalSize = commodityInventorySlotParent.GetComponent<RectTransform>().sizeDelta;
            commodityInventoryOriginalPosition = commodityInventorySlotParent.GetComponent<RectTransform>().anchoredPosition;
        }

        #region 更新商品
        //更新商店的商品:
        public void UpdateCommodityInventory(PlayerManager player,NpcManager npc)
        {
            if (npc != shopowner)
                isThereTransaction = false;

            //设置店主:
            shopowner = npc;
            //更新交易物品列表:
            List<Commodity> commoditieList = npc.storeManager.commoditieList;
            commodityInventorySlotArray = GetAllChildCommodityInventorySlot(commodityInventorySlotParent);
            for (int i = 0; i < commodityInventorySlotArray.Length; i++)
            {
                //所有交易物品槽中,小于商店商品个数的部分:
                if(i < commoditieList.Count)
                {
                    //交易物品槽小于商品个数:
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

            //交易物父物体的大小与位置:(商店窗口最多容纳5*4[列*行]20个商品,如果小于则不用放大)
            int amplifyScale =1 + (commoditieList.Count - 20) / 5;
            if(commoditieList.Count <= 20)
                amplifyScale = 0;
            float heightOccupied = commodityInventoryOriginalSize.y / 4; //计算每行占据窗口的高度;
            Vector2 newSize = new Vector2(commodityInventoryOriginalSize.x, commodityInventoryOriginalSize.y + heightOccupied * amplifyScale);
            Vector2 newPosition = new Vector2(commodityInventoryOriginalPosition.x, commodityInventoryOriginalPosition.y - heightOccupied / 2 * amplifyScale);
            commodityInventorySlotParent.GetComponent<RectTransform>().sizeDelta = newSize;
            commodityInventorySlotParent.GetComponent<RectTransform>().anchoredPosition = newPosition;

            //更新商店名称:
            shopkeeperNameText.text = npc.npcName + "的商店";
            //禁用交易按钮:
            AllDisableButton();
            //启用交易按钮:
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

        #region 交易物品
        //打开交易商品窗口
        public void OpenTradingSelectCommodityWindow(Commodity commodity, RectTransform target)
        {
            if (commodity != null)
            {
                tradingSelectWindow.SetActive(true);                //启用窗口:
                tradingSelectWindow.GetComponent<RectTransform>().position = target.position;   //定位到该目标

                currentAmount = 0;
                selectedCommodity = commodity;
                UpdateTransactionAmountAndPrice(true);       //更新商品交易小窗口:

                //启用人物等级窗口:(并更新属性)
                uiManager.playerPropertiesWindowManager.UpdatePropertiesText(uiManager.playerManager.playerStateManager);
                //启用该商品对应的详情窗口:(并更新属性)
                ProductDetailWindow(commodity);

                AllDisableButton();                                              //禁用所有交易物品的按钮
            }
        }
        //更新交易数量,计算交易价格,计算是否可用交易:
        public void UpdateTransactionAmountAndPrice(bool isUp)
        {
            //交易数量计算--------------
            if (isUp)
            {
                currentAmount++;
                //交易个数不能大于商品个数，除非该商品是无限的:
                if (currentAmount > selectedCommodity.commodityAmount && selectedCommodity.isInfinite == false)
                    currentAmount = selectedCommodity.commodityAmount;
            }
            else
            {
                currentAmount--;
                if (currentAmount < 0)
                    currentAmount = 0;
            }
            //交易价格计算-------------
            int transactionPrice = currentAmount * selectedCommodity.commodityPrice;
            //是否可用交易判断--------
            bool canTransaction = uiManager.playerManager.playerStateManager.currentSoulCount >= transactionPrice;  //持有灵魂大于商品价格
            bool haveNecessaryItem = CheckForHaveNecessaryItem();                                                                                    //拥有交易必须物

            //ui对象属性设置:---------
            amountVslueText.text = currentAmount.ToString();
            priceValueText.text = transactionPrice.ToString();

            //成交按钮
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
        //检查是否有必需物品：
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
        //成交:
        public void Deal()
        {
            //给出装备:
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
                    currentAmount = 1;          //法术只能买一份
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

                    //如果背包中数量+购买数量>消耗品持有最大数量;则购买数=消耗品持有最大数-背包中数量;
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

                    //如果背包中数量+购买数量>消耗品持有最大数量;则购买数=消耗品持有最大数-背包中数量;
                    if (ammoAmountInBackpack + currentAmount > newAmmo.carryLimit)
                    {
                        currentAmount = newAmmo.carryLimit - ammoAmountInBackpack;
                    }

                    newAmmo.currentAmount = currentAmount;
                    playerInventory.AddAmmoItemToInventory(newAmmo);
                    break;
            }

            //计算交易价格:
            int transactionPrice = currentAmount * selectedCommodity.commodityPrice;
            selectedCommodity.commodityAmount -= currentAmount;
            PlayerStatsManager playerStats = uiManager.playerManager.playerStateManager;
            playerStats.currentSoulCount -= transactionPrice;

            if (selectedCommodity.exchangeItem != null)
            {
                DeleteRequiredProps(selectedCommodity.exchangeItem, selectedCommodity.exchangeItemType, playerInventory);
            }
            //进行过交易:
            isThereTransaction = true;

            //更新装备窗口属性:
            uiManager.CloseAllCentralWindow();
            uiManager.itemPropertiesWindow.SetActive(true);
            uiManager.itemPropertiesWindowManager.UpdateItemPropertiesWindow(null, 1);
            uiManager.playerPropertiesWindowManager.UpdatePropertiesText(playerStats);
            //关闭窗口:
            selectedCommodity = null;
            tradingSelectWindow.SetActive(false);
            AllEnableButton();

            //更新商店:
            UpdateCommodityInventory(uiManager.playerManager, shopowner);
        }
        //取消
        public void Cancel()
        {
            //属性:
            currentExchangeItemAmountText.color = Color.white;
            currentExchangeItemAmountText.text = "-";
            needExchangeItemAmountText.text = "-";
            exchangeItemNameText.text = "必需物品:";

            //更新装备窗口属性:
            uiManager.CloseAllCentralWindow();
            uiManager.itemPropertiesWindow.SetActive(true);
            uiManager.itemPropertiesWindowManager.UpdateItemPropertiesWindow(null, 1);
            uiManager.playerPropertiesWindowManager.UpdatePropertiesText(uiManager.playerManager.playerStateManager);
            //关闭窗口:
            selectedCommodity = null;
            tradingSelectWindow.SetActive(false);
            AllEnableButton();

            //更新商店:
            UpdateCommodityInventory(uiManager.playerManager, shopowner);
        }
        //删除必需道具:
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

        //打开商品详情窗口:
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
                    Debug.Log("体力加成:" + ring.staminaLevelAddition + " 灵魂加成:"+ring.soulsRewardLevelAddition);
                    break;
                case ItemType.Ammo:
                    uiManager.CloseAllCentralWindow();
                    uiManager.weaponPropertiesWindow.SetActive(true);
                    uiManager.weapentPropertiesWindowManager.UpdateUIWindow(commodity.item as RangedAmmoItem, playerInventory, true);
                    break;
            }
        }
        //禁用所有交易物按钮：
        private void AllDisableButton()
        {
            commodityInventorySlotArray = GetAllChildCommodityInventorySlot(commodityInventorySlotParent);
            foreach(var commodityInventorySlot in commodityInventorySlotArray)
            {
                commodityInventorySlot.DisableButton();
            }
        }
        //启用所有交易按钮:
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

        #region 结束交易:
        //打开结束交易窗口:
        public void EnableEndTransactionWindow()
        {
            //调用一遍"取消"
            Cancel();
            //禁用所有商品按钮
            AllDisableButton();
            //打开结束交易窗口
            endTransactionWindow.SetActive(true);
        }
        //关闭结束交易窗口:
        public void CloseEndTransactionWindow()
        {
            //启用商品交易按钮:
            AllEnableButton();
            //关闭结束交易窗口:
            endTransactionWindow.SetActive(false);
        }
        //结束交易:
        public void CloseTheTransaction()
        {
            uiManager.CloseAllLeftWindow();
            uiManager.CloseAllCentralWindow();
            uiManager.CloseAllRightWindow();

            uiManager.hudWindow.SetActive(true);

            //播放结束对话:
            uiManager.dialogSystemWindow.SetActive(true);
            if (isThereTransaction)
            {
                uiManager.dialogSystemWindowManager.GiveDialogueContentToUI(shopowner.storeManager.postPurchaseDialogue, true);
            }
            else
            {
                uiManager.dialogSystemWindowManager.GiveDialogueContentToUI(shopowner.storeManager.notPurchasedDialogue, true);
            }
            //玩家交易状态结束:
            uiManager.playerManager.isTrading = false;

            //关闭结束交易窗口:
            endTransactionWindow.SetActive(false);
        }
        #endregion
    }
}