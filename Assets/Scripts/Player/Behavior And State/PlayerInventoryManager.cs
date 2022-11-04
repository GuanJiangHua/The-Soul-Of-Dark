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

        [Header("��Ʒ������:")]
        public ItemManager itemManager;
        [Header("������������λ��:")]
        public int currentRightWeaponIndex = -1;
        public int currentLeftWeaponIndex = -1;
        [Header("����Ʒ��λ��:")]
        public int currentConsumableIndex = 0;
        [Header("������λ��:")]
        public int currentSpellIndex = 0;

        [Header("������װ�����:")]
        public List<WeaponItem> weaponInventory;                         //�������;
        public List<HelmetEquipment> headEquipmentInventory;    //ͷ�����;
        public List<TorsoEquipment> torsoEquipmentInventory;      //�ؼ׿��;
        public List<LegEquipment> legEquipmentInventory;             //�ȼ׿��;
        public List<HandEquipment> handEquipmentInventory;       //�ۼ׿��;
        [Header("��ҩ���:")]
        public List<RangedAmmoItem> rangedAmmoInventory;
        [Header("����Ʒ���:")]
        public List<ConsumableItem> consumableInventory = new List<ConsumableItem>();           //����Ʒ���;
        [Header("������:")]
        public List<SpellItem> spellleInventory = new List<SpellItem>();
        [Header("��ָ���:")]
        public List<RingItem> ringInventory = new List<RingItem>();

        [Header("����Ʒװ��:")]
        public ConsumableItem[] consumableSlots = new ConsumableItem[10];
        [Header("���䷨��:")]
        public int enabledMemorySpellSlotNumber = 0;                                                                        //���䷨���ո�;
        public SpellItem[] memorySpellArray = new SpellItem[8];
        [Header("װ����ָ:")]
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

            currentConsumable = consumableSlots[currentConsumableIndex];                  //����Ʒ
            currentSpell = memorySpellArray[currentSpellIndex];                                        //����
            characterWeaponSlotManger.LoadWeaponHolderOfSlot(leftWeapon, true);    //����
            characterWeaponSlotManger.LoadWeaponHolderOfSlot(rightWeapon, false); //����

            uiManager.quickSlotUI.UpdateCurrentConsumableQuickSlotUI(currentConsumable);
            uiManager.quickSlotUI.UpdateCurrentSpellIconQuickSlotUI(currentSpell, playerManager.playerStateManager);

            CountMemorySpellSlotNumber();   //�������ո�:
            //���ؽ�ָ:
            foreach (RingItem ring in currentRings)
            {
                if (ring != null)
                {
                    ring.PutOnTheRing(playerManager);
                }
            }
        }

        //������������:
        public void ChangeRightWeapon()
        {
            currentRightWeaponIndex += 1;
            if(currentRightWeaponIndex == 0 && WeaponRightHandSlots[currentRightWeaponIndex] != null)       //�������������װ��:
            {
                rightWeapon = WeaponRightHandSlots[currentRightWeaponIndex];
                characterWeaponSlotManger.LoadWeaponHolderOfSlot(rightWeapon, false);
            }
            else if(currentRightWeaponIndex == 0 && WeaponRightHandSlots[currentRightWeaponIndex] == null)      //�������������װ��:
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
    
        //������������:
        public void ChangeLeftWeapon()
        {
            if (inputHandler.twoHandFlag) return;

            currentLeftWeaponIndex += 1;
            if (currentLeftWeaponIndex == 0 && WeaponLeftHandSlots[currentLeftWeaponIndex] != null)       //�������������װ��:
            {
                leftWeapon = WeaponLeftHandSlots[currentLeftWeaponIndex];
                characterWeaponSlotManger.LoadWeaponHolderOfSlot(leftWeapon, true);
            }
            else if (currentLeftWeaponIndex == 0 && WeaponLeftHandSlots[currentLeftWeaponIndex] == null)      //�������������װ��:
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
    
        //������ǰ����Ʒ:
        public void ChangeConsumable()
        {
            currentConsumableIndex++;

            for (int i = 0; i < 10; i++)
            {
                //�����λ��װ��������Ʒ��Ϊ��:
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

        //�������Ʒ������Ʒװ����:()
        public void AddConsumableItemToSlots(ConsumableItem item , int indexlocation)
        {
            if (item != null)
            {
                //�����λ������Ʒ,������Ʒ��ӵ�����Ʒ���:
                if(consumableSlots[indexlocation] != null)
                {
                    consumableInventory.Add(consumableSlots[indexlocation]);
                }

                //������Ʒ��ӵ�����Ʒװ��ָ��λ��:
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

        //������ǰ����:
        public void ChangeSpell()
        {
            Debug.Log("�л�������������:");
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

        //��ӷ�������ǰ���䷨����ȥ:
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
        //������䷨���ո�:
        public void CountMemorySpellSlotNumber()
        {
            enabledMemorySpellSlotNumber = playerManager.playerStateManager.intelligenceLevel / 5;      //����ո����,���������ļ���ո����ֻ�ܵ�5;
            if (enabledMemorySpellSlotNumber > 5)
                enabledMemorySpellSlotNumber = 5;
        }

        //װ����ָ:[ringItem����Ϊ��,�ձ�ʾ����λ�õĽ�ָж������]
        public void AddRing(int index , RingItem ringItem)
        {
            if (index >= 0 && index < currentRings.Length)
            {
                if (currentRings[index] != null)
                {
                    //�����ǰ��ָλ��Ϊ��,����ж�ؽ�ָ����,������������:
                    currentRings[index].TakeOffTheRing(playerManager);
                    //����λ�õĽ�ָ��ӻر���:
                    ringInventory.Add(currentRings[index]);
                    //���ø�λ��Ϊ��:
                    currentRings[index] = null;
                }

                if (ringItem != null)
                {
                    //���ô��Ͻ�ָ����,��������:
                    ringItem.PutOnTheRing(playerManager);
                    //���õ�ǰװ���Ľ�ָ:
                    currentRings[index] = ringItem;
                    //�ӱ�����ɾ����ָ:
                    ringInventory.Remove(ringItem);
                }

                //����װ��ui...
            }
        }
        public RingItem GetRingById(int index)
        {
            return currentRings[index];
        }
        //������ʹ��:
        public void SetRingArray(RingItem[] ringItems)
        {
            if (ringItems != null && ringItems.Length == 4)
            {
                currentRings = ringItems;
            }
        }
    }
}
