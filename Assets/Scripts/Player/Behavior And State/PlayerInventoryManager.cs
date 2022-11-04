using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerInventoryManager : CharacterInventoryManager
    {
        UIManager uiManager;
        InputHandler inputHandler;
        PlayerManager playerManager;

        [Header("物品管理器:")]
        public ItemManager itemManager;
        [Header("左右手武器槽位置:")]
        public int currentRightWeaponIndex = -1;
        public int currentLeftWeaponIndex = -1;
        [Header("消耗品槽位置:")]
        public int currentConsumableIndex = 0;
        [Header("法术槽位置:")]
        public int currentSpellIndex = 0;

        [Header("武器与装备库存:")]
        public List<WeaponItem> weaponInventory;                         //武器库存;
        public List<HelmetEquipment> headEquipmentInventory;    //头盔库存;
        public List<TorsoEquipment> torsoEquipmentInventory;      //胸甲库存;
        public List<LegEquipment> legEquipmentInventory;             //腿甲库存;
        public List<HandEquipment> handEquipmentInventory;       //臂甲库存;
        [Header("弹药库存:")]
        public List<RangedAmmoItem> rangedAmmoInventory;
        [Header("消耗品库存:")]
        public List<ConsumableItem> consumableInventory = new List<ConsumableItem>();           //消耗品库存;
        [Header("咒语库存:")]
        public List<SpellItem> spellleInventory = new List<SpellItem>();
        [Header("戒指库存:")]
        public List<RingItem> ringInventory = new List<RingItem>();

        [Header("消耗品装备:")]
        public ConsumableItem[] consumableSlots = new ConsumableItem[10];
        [Header("记忆法术:")]
        public int enabledMemorySpellSlotNumber = 0;                                                                        //记忆法术空格;
        public SpellItem[] memorySpellArray = new SpellItem[8];
        [Header("装备戒指:")]
        [SerializeField] private RingItem[] currentRings = new RingItem[4];

        protected override void Awake()
        {
            base.Awake();

            uiManager = FindObjectOfType<UIManager>();
            inputHandler = GetComponent<InputHandler>();
            playerManager = GetComponent<PlayerManager>();
        }

        private void Start()
        {
            rightWeapon = WeaponRightHandSlots[0];
            leftWeapon = WeaponLeftHandSlots[0];

            currentConsumable = consumableSlots[currentConsumableIndex];                  //消耗品
            currentSpell = memorySpellArray[currentSpellIndex];                                        //法术
            characterWeaponSlotManger.LoadWeaponHolderOfSlot(leftWeapon, true);    //武器
            characterWeaponSlotManger.LoadWeaponHolderOfSlot(rightWeapon, false); //武器

            uiManager.quickSlotUI.UpdateCurrentConsumableQuickSlotUI(currentConsumable);
            uiManager.quickSlotUI.UpdateCurrentSpellIconQuickSlotUI(currentSpell, playerManager.playerStateManager);

            CountMemorySpellSlotNumber();   //计算记忆空格:
            //加载戒指:
            foreach (RingItem ring in currentRings)
            {
                if (ring != null)
                {
                    ring.PutOnTheRing(playerManager);
                }
            }
        }

        //更换右手武器:
        public void ChangeRightWeapon()
        {
            currentRightWeaponIndex += 1;
            if(currentRightWeaponIndex == 0 && WeaponRightHandSlots[currentRightWeaponIndex] != null)       //如果武器槽中有装备:
            {
                rightWeapon = WeaponRightHandSlots[currentRightWeaponIndex];
                characterWeaponSlotManger.LoadWeaponHolderOfSlot(rightWeapon, false);
            }
            else if(currentRightWeaponIndex == 0 && WeaponRightHandSlots[currentRightWeaponIndex] == null)      //如果武器槽中无装备:
            {
                //currentRightWeaponIndex += 1;
                rightWeapon = unarmedWeapon;
                characterWeaponSlotManger.LoadWeaponHolderOfSlot(unarmedWeapon, false);
            }
            else if (currentRightWeaponIndex == 1 && WeaponRightHandSlots[currentRightWeaponIndex] != null)
            {
                rightWeapon = WeaponRightHandSlots[currentRightWeaponIndex];
                characterWeaponSlotManger.LoadWeaponHolderOfSlot(rightWeapon, false);
            }
            else if (currentRightWeaponIndex == 1 && WeaponRightHandSlots[currentRightWeaponIndex] == null)
            {
                //currentRightWeaponIndex += 1;
                rightWeapon = unarmedWeapon;
                characterWeaponSlotManger.LoadWeaponHolderOfSlot(unarmedWeapon, false);
            }
            else if (currentRightWeaponIndex == 2 && WeaponRightHandSlots[currentRightWeaponIndex] != null)
            {
                rightWeapon = WeaponRightHandSlots[currentRightWeaponIndex];
                characterWeaponSlotManger.LoadWeaponHolderOfSlot(rightWeapon, false);
            }
            else if (currentRightWeaponIndex == 2 && WeaponRightHandSlots[currentRightWeaponIndex] == null)
            {
                //currentRightWeaponIndex += 1;
                rightWeapon = unarmedWeapon;
                characterWeaponSlotManger.LoadWeaponHolderOfSlot(unarmedWeapon, false);
            }
            else
            {
                currentRightWeaponIndex += 1;
            }

            if(currentRightWeaponIndex > WeaponRightHandSlots.Length - 1)
            {
                currentRightWeaponIndex = -1;
                rightWeapon = unarmedWeapon;
                characterWeaponSlotManger.LoadWeaponHolderOfSlot(unarmedWeapon, false);
            }
        }
    
        //更换左手武器:
        public void ChangeLeftWeapon()
        {
            if (inputHandler.twoHandFlag) return;

            currentLeftWeaponIndex += 1;
            if (currentLeftWeaponIndex == 0 && WeaponLeftHandSlots[currentLeftWeaponIndex] != null)       //如果武器槽中有装备:
            {
                leftWeapon = WeaponLeftHandSlots[currentLeftWeaponIndex];
                characterWeaponSlotManger.LoadWeaponHolderOfSlot(leftWeapon, true);
            }
            else if (currentLeftWeaponIndex == 0 && WeaponLeftHandSlots[currentLeftWeaponIndex] == null)      //如果武器槽中无装备:
            {
                //currentLeftWeaponIndex += 1;
                leftWeapon = unarmedWeapon;
                characterWeaponSlotManger.LoadWeaponHolderOfSlot(unarmedWeapon, true);
            }
            else if (currentLeftWeaponIndex == 1 && WeaponLeftHandSlots[currentLeftWeaponIndex] != null)
            {
                leftWeapon = WeaponLeftHandSlots[currentLeftWeaponIndex];
                characterWeaponSlotManger.LoadWeaponHolderOfSlot(leftWeapon, true);
            }
            else if (currentLeftWeaponIndex == 1 && WeaponLeftHandSlots[currentLeftWeaponIndex] == null)
            {
                //currentLeftWeaponIndex += 1;
                leftWeapon = unarmedWeapon;
                characterWeaponSlotManger.LoadWeaponHolderOfSlot(unarmedWeapon, true);
            }
            else if (currentLeftWeaponIndex == 2 && WeaponLeftHandSlots[currentLeftWeaponIndex] != null)
            {
                leftWeapon = WeaponLeftHandSlots[currentLeftWeaponIndex];
                characterWeaponSlotManger.LoadWeaponHolderOfSlot(leftWeapon, true);
            }
            else if (currentLeftWeaponIndex == 2 && WeaponLeftHandSlots[currentLeftWeaponIndex] == null)
            {
                //currentLeftWeaponIndex += 1;
                leftWeapon = unarmedWeapon;
                characterWeaponSlotManger.LoadWeaponHolderOfSlot(unarmedWeapon, true);
            }
            else
            {
                currentLeftWeaponIndex += 1;
            }

            if (currentLeftWeaponIndex > WeaponLeftHandSlots.Length - 1)
            {
                currentLeftWeaponIndex = -1;
                leftWeapon = unarmedWeapon;
                characterWeaponSlotManger.LoadWeaponHolderOfSlot(unarmedWeapon, true);
            }
        }
    
        //更换当前消耗品:
        public void ChangeConsumable()
        {
            currentConsumableIndex++;

            for (int i = 0; i < 10; i++)
            {
                //如果该位置装备的消耗品不为空:
                int index = (currentConsumableIndex + i )% consumableSlots.Length;

                if (consumableSlots[index] != null)
                {
                    currentConsumableIndex = index;

                    currentConsumable = consumableSlots[currentConsumableIndex];
                    uiManager.quickSlotUI.UpdateCurrentConsumableQuickSlotUI(currentConsumable);
                    return;
                }
            }

            currentConsumableIndex = 0;
        }

        //添加消耗品到消耗品装备中:()
        public void AddConsumableItemToSlots(ConsumableItem item , int indexlocation)
        {
            if (item != null)
            {
                //如果该位置有物品,将该物品添加到消耗品库存:
                if(consumableSlots[indexlocation] != null)
                {
                    consumableInventory.Add(consumableSlots[indexlocation]);
                }

                //将该物品添加到消耗品装备指定位置:
                consumableSlots[indexlocation] = item;
            }
            else
            {
                if (memorySpellArray[indexlocation] != null)
                {
                    spellleInventory.Add(memorySpellArray[indexlocation]);
                }

                consumableSlots[indexlocation] = null;
            }
        }
        public void AddConsumableItemToInventory(ConsumableItem item)
        {
            for(int i = 0; i < consumableInventory.Count; i++)
            {
                if (item.itemName == consumableInventory[i].itemName)
                {
                    consumableInventory[i].currentItemAmount += item.currentItemAmount;
                    if(consumableInventory[i].currentItemAmount > consumableInventory[i].maxItemAmount)
                    {
                        consumableInventory[i].currentItemAmount = consumableInventory[i].maxItemAmount;
                    }

                    return;
                }
            }

            consumableInventory.Add(item);
        }
        public void AddAmmoItemToInventory(RangedAmmoItem item)
        {
            for (int i = 0; i < rangedAmmoInventory.Count; i++)
            {
                if (item.itemName == rangedAmmoInventory[i].itemName)
                {
                    rangedAmmoInventory[i].currentAmount += item.currentAmount;
                    if (rangedAmmoInventory[i].currentAmount > rangedAmmoInventory[i].carryLimit)
                    {
                        rangedAmmoInventory[i].currentAmount = rangedAmmoInventory[i].carryLimit;
                    }

                    return;
                }
            }

            rangedAmmoInventory.Add(item);
        }

        //更换当前法术:
        public void ChangeSpell()
        {
            Debug.Log("切换法术方法调用:");
            currentSpellIndex++;
            for(int i = 0; i < 5; i++)
            {
                int index = (currentSpellIndex + i) % 5;

                if (memorySpellArray[index] != null)
                {
                    currentSpellIndex = index;

                    currentSpell = memorySpellArray[currentSpellIndex];

                    uiManager.quickSlotUI.UpdateCurrentSpellIconQuickSlotUI(currentSpell, playerManager.playerStateManager);
                    return;
                }
            }
        }

        //添加法术到当前记忆法术中去:
        public void AddSpellItemToSlots(SpellItem spell,int indexlocation)
        {
            if (spell != null)
            {
                if(memorySpellArray[indexlocation] != null)
                {
                    spellleInventory.Add(memorySpellArray[indexlocation]);
                }

                memorySpellArray[indexlocation] = spell;
            }
            else
            {
                memorySpellArray[indexlocation] = null;
            }
        }
        //计算记忆法术空格:
        public void CountMemorySpellSlotNumber()
        {
            enabledMemorySpellSlotNumber = playerManager.playerStateManager.intelligenceLevel / 5;      //记忆空格计算,智力带来的记忆空格最多只能到5;
            if (enabledMemorySpellSlotNumber > 5)
                enabledMemorySpellSlotNumber = 5;
        }

        //装备戒指:[ringItem可以为空,空表示将该位置的戒指卸载下来]
        public void AddRing(int index , RingItem ringItem)
        {
            if (index >= 0 && index < currentRings.Length)
            {
                if (currentRings[index] != null)
                {
                    //如果当前戒指位不为空,调用卸载戒指方法,更新人物属性:
                    currentRings[index].TakeOffTheRing(playerManager);
                    //将该位置的戒指添加回背包:
                    ringInventory.Add(currentRings[index]);
                    //设置该位置为空:
                    currentRings[index] = null;
                }

                if (ringItem != null)
                {
                    //调用带上戒指方法,更新属性:
                    ringItem.PutOnTheRing(playerManager);
                    //设置当前装备的戒指:
                    currentRings[index] = ringItem;
                    //从背包中删除戒指:
                    ringInventory.Remove(ringItem);
                }

                //更新装备ui...
            }
        }
        public RingItem GetRingById(int index)
        {
            return currentRings[index];
        }
        //读档是使用:
        public void SetRingArray(RingItem[] ringItems)
        {
            if (ringItems != null && ringItems.Length == 4)
            {
                currentRings = ringItems;
            }
        }
    }
}
