using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "新建物品管理信息", menuName = "新建物品管理")]
    public class ItemManager : ScriptableObject
    {
        [Header("所有武器:")]
        public List<WeaponItem> allWeaponsList = new List<WeaponItem>();

        [Header("所有头盔:")]
        public List<HelmetEquipment> allHelmetsList = new List<HelmetEquipment>();
        [Header("所有胸甲:")]
        public List<TorsoEquipment> allTorsoEquipmentList = new List<TorsoEquipment>();
        [Header("所有臂甲:")]
        public List<HandEquipment> allHandEquipmentList = new List<HandEquipment>();
        [Header("所有腿甲:")]
        public List<LegEquipment> allLegEquipmentList = new List<LegEquipment>();

        [Header("所有戒指:")]
        public List<RingItem> allRingList = new List<RingItem>();

        [Header("所有法术:")]
        public List<SpellItem> allSpellItemList = new List<SpellItem>();

        [Header("所有消耗品:")]
        public List<ConsumableItem> allConsumableItemList = new List<ConsumableItem>();
        [Header("所有弹丸:")]
        public List<RangedAmmoItem> allRangedAmmoItemList = new List<RangedAmmoItem>();

        #region 获取物品函数:
        //获取到武器:
        public WeaponItem GetWeaponItemByID(int weaponID)
        {
            if(weaponID >= 0 && weaponID < allWeaponsList.Count)
            {
                return allWeaponsList[weaponID];
            }
            return null;
        }
        //获取到头盔:
        public HelmetEquipment GetHelmetEquipmentByID(int helmetID)
        {
            if(helmetID >= 0)
            {
                return allHelmetsList[helmetID];
            }
            return null;
        }
        //获取到胸甲:
        public TorsoEquipment GetTorsoEquipmentByID(int torsoID)
        {
            if(torsoID > -1)
            {
                return allTorsoEquipmentList[torsoID];
            }
            return null;
        }
        //获取臂甲:
        public HandEquipment GetHandEquipmentByID(int handID)
        {
            if(handID > -1)
            {
                return allHandEquipmentList[handID];
            }
            return null;
        }
        //获取腿甲:
        public LegEquipment GetLegEquipmentByID(int legID)
        {
            if(legID > -1)
            {
                return allLegEquipmentList[legID];
            }
            return null;
        }
        //获取到法术:
        public SpellItem GetSpellItemByName(string spellName)
        {
            foreach(SpellItem spell in allSpellItemList)
            {
                if (spell.itemName == spellName)
                    return spell;
            }
            return null;
        }
        //获取到消耗品:
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
        //获取戒指:
        public RingItem GetRingItemByID(int ringID)
        {
            if (ringID < 0)
            {
                return null;
            }
            return allRingList[ringID];
        }
        //获取箭矢:
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

        #region 获取到物品在数组的位置:
        //获取到武器在武器列表中的位置:
        public int IndexInWeaponList(WeaponItem weapon)
        {
            int index = allWeaponsList.IndexOf(weapon);

            return index;
        }
        //获取到头盔在头盔列表中的位置:
        public int IndexInHelmetEquipmentList(HelmetEquipment helmet)
        {
            int index = allHelmetsList.IndexOf(helmet);

            return index;
        }
        //获取到胸甲在胸甲列表中的位置:
        public int IndexInTorsoEquipmentList(TorsoEquipment torso)
        {
            int index = allTorsoEquipmentList.IndexOf(torso);

            return index;
        }
        //获取臂甲在臂甲列表中的位置:
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
        //获取消耗品在列表中的位置:
        public string NameInConsumableList(ConsumableItem consumable)
        {
            foreach(ConsumableItem item in allConsumableItemList)
            {
                if (item.itemName == consumable.itemName)
                    return item.itemName;
            }
            return null;
        }
        //获取戒指在列表中的位置:
        public int IndexInRingItemList(RingItem ring)
        {
            int index = allRingList.IndexOf(ring);
            return index;
        }
        //获取弹丸在列表中的位置:
        public int IndexInAmmoItemList(RangedAmmoItem rangedAmmo)
        {
            int index = allRangedAmmoItemList.IndexOf(rangedAmmo);
            return index;
        }
        #endregion
    }
}