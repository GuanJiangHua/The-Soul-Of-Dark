using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "�½���Ʒ������Ϣ", menuName = "�½���Ʒ����")]
    public class ItemManager : ScriptableObject
    {
        [Header("��������:")]
        public List<WeaponItem> allWeaponsList = new List<WeaponItem>();

        [Header("����ͷ��:")]
        public List<HelmetEquipment> allHelmetsList = new List<HelmetEquipment>();
        [Header("�����ؼ�:")]
        public List<TorsoEquipment> allTorsoEquipmentList = new List<TorsoEquipment>();
        [Header("���бۼ�:")]
        public List<HandEquipment> allHandEquipmentList = new List<HandEquipment>();
        [Header("�����ȼ�:")]
        public List<LegEquipment> allLegEquipmentList = new List<LegEquipment>();

        [Header("���н�ָ:")]
        public List<RingItem> allRingList = new List<RingItem>();

        [Header("���з���:")]
        public List<SpellItem> allSpellItemList = new List<SpellItem>();

        [Header("��������Ʒ:")]
        public List<ConsumableItem> allConsumableItemList = new List<ConsumableItem>();
        [Header("���е���:")]
        public List<RangedAmmoItem> allRangedAmmoItemList = new List<RangedAmmoItem>();

        #region ��ȡ��Ʒ����:
        //��ȡ������:
        public WeaponItem GetWeaponItemByID(int weaponID)
        {
            if(weaponID >= 0 && weaponID < allWeaponsList.Count)
            {
                return allWeaponsList[weaponID];
            }
            return null;
        }
        //��ȡ��ͷ��:
        public HelmetEquipment GetHelmetEquipmentByID(int helmetID)
        {
            if(helmetID >= 0)
            {
                return allHelmetsList[helmetID];
            }
            return null;
        }
        //��ȡ���ؼ�:
        public TorsoEquipment GetTorsoEquipmentByID(int torsoID)
        {
            if(torsoID > -1)
            {
                return allTorsoEquipmentList[torsoID];
            }
            return null;
        }
        //��ȡ�ۼ�:
        public HandEquipment GetHandEquipmentByID(int handID)
        {
            if(handID > -1)
            {
                return allHandEquipmentList[handID];
            }
            return null;
        }
        //��ȡ�ȼ�:
        public LegEquipment GetLegEquipmentByID(int legID)
        {
            if(legID > -1)
            {
                return allLegEquipmentList[legID];
            }
            return null;
        }
        //��ȡ������:
        public SpellItem GetSpellItemByName(string spellName)
        {
            foreach(SpellItem spell in allSpellItemList)
            {
                if (spell.itemName == spellName)
                    return spell;
            }
            return null;
        }
        //��ȡ������Ʒ:
        public ConsumableItem GetConsumableItem(ConsumableItemData consumableData)
        {
            ConsumableItem consumableItem = null;
            if (consumableData.consumableItemName != null && consumableData.consumableItemName.Equals("") == false)
            {
                //consumableItem = allConsumableItemList[consumableData.consumableItemID];
                foreach(ConsumableItem item in allConsumableItemList)
                {
                    if (item.itemName == consumableData.consumableItemName)
                    {
                        consumableItem = Instantiate(item);
                        consumableItem.maxItemAmount = consumableData.maxItemAmount;
                        consumableItem.currentItemAmount = consumableData.currentItemAmount;
                        return consumableItem;
                    }
                }

            }

            return null;
        }
        //��ȡ��ָ:
        public RingItem GetRingItemByID(int ringID)
        {
            if (ringID < 0)
            {
                return null;
            }
            return allRingList[ringID];
        }
        //��ȡ��ʸ:
        public RangedAmmoItem GetRangedAmmoItem(RangedAmmoItemData ammoItemData)
        {
            RangedAmmoItem ammoItem = null;
            if (ammoItemData.rangedAmmoItemID != -1)
            {
                ammoItem = allRangedAmmoItemList[ammoItemData.rangedAmmoItemID];
                ammoItem.currentAmount = ammoItemData.currentAmmoItemAmount;
            }
            return ammoItem;
        }
        #endregion

        #region ��ȡ����Ʒ�������λ��:
        //��ȡ�������������б��е�λ��:
        public int IndexInWeaponList(WeaponItem weapon)
        {
            int index = allWeaponsList.IndexOf(weapon);

            return index;
        }
        //��ȡ��ͷ����ͷ���б��е�λ��:
        public int IndexInHelmetEquipmentList(HelmetEquipment helmet)
        {
            int index = allHelmetsList.IndexOf(helmet);

            return index;
        }
        //��ȡ���ؼ����ؼ��б��е�λ��:
        public int IndexInTorsoEquipmentList(TorsoEquipment torso)
        {
            int index = allTorsoEquipmentList.IndexOf(torso);

            return index;
        }
        //��ȡ�ۼ��ڱۼ��б��е�λ��:
        public int IndexInHandEquipmentList(HandEquipment hand)
        {
            int index = allHandEquipmentList.IndexOf(hand);

            return index;
        }
        public int IndexInLegEquipmentList(LegEquipment leg)
        {
            int index = allLegEquipmentList.IndexOf(leg);

            return index;
        }
        //��ȡ����Ʒ���б��е�λ��:
        public string NameInConsumableList(ConsumableItem consumable)
        {
            foreach(ConsumableItem item in allConsumableItemList)
            {
                if (item.itemName == consumable.itemName)
                    return item.itemName;
            }
            return null;
        }
        //��ȡ��ָ���б��е�λ��:
        public int IndexInRingItemList(RingItem ring)
        {
            int index = allRingList.IndexOf(ring);
            return index;
        }
        //��ȡ�������б��е�λ��:
        public int IndexInAmmoItemList(RangedAmmoItem rangedAmmo)
        {
            int index = allRangedAmmoItemList.IndexOf(rangedAmmo);
            return index;
        }
        #endregion
    }
}