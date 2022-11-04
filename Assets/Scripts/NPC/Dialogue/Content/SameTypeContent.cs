using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "有某类物品条件", menuName = "游戏对话系统模块/新建对话条件判断/有某类物品条件")]
    public class SameTypeContent : ConditionClass
    {
        [Header("目标类型物品:")]
        public Item item;
        [Header("物品归属的大类:")]
        public ItemType itemType;
        public override bool CheckConditionsMet(PlayerManager playerManager)
        {
            bool isHave = false;
            PlayerInventoryManager playerInventory = playerManager.playerInventoryManager;
            switch (itemType)
            {
                case ItemType.Weapon:
                    foreach(WeaponItem weapon in playerInventory.weaponInventory)
                    {
                        if(item.GetType() == weapon.GetType())
                        {
                            isHave = true;
                            break;
                        }
                    }
                    break;
                case ItemType.Helmet:
                    foreach (HelmetEquipment helmet in playerInventory.headEquipmentInventory)
                    {
                        if (item.GetType() == helmet.GetType())
                        {
                            isHave = true;
                            break;
                        }
                    }
                    break;
                case ItemType.Torso:
                    foreach (TorsoEquipment torso in playerInventory.torsoEquipmentInventory)
                    {
                        if (item.GetType() == torso.GetType())
                        {
                            isHave = true;
                            break;
                        }
                    }
                    break;
                case ItemType.Leg:
                    foreach (LegEquipment leg in playerInventory.legEquipmentInventory)
                    {
                        if (item.GetType() == leg.GetType())
                        {
                            isHave = true;
                            break;
                        }
                    }
                    break;
                case ItemType.Hand:
                    foreach (HandEquipment hand in playerInventory.handEquipmentInventory)
                    {
                        if (item.GetType() == hand.GetType())
                        {
                            isHave = true;
                            break;
                        }
                    }
                    break;
                case ItemType.Spell:
                    foreach (SpellItem spell in playerInventory.spellleInventory)
                    {
                        if (item.GetType() == spell.GetType())
                        {
                            isHave = true;
                            break;
                        }
                    }
                    break;
                case ItemType.Consumable:
                    foreach (ConsumableItem consumable in playerInventory.consumableInventory)
                    {
                        if (item.GetType() == consumable.GetType())
                        {
                            isHave = true;
                            break;
                        }
                    }
                    break;
                case ItemType.Ring:
                    foreach (RingItem ring in playerInventory.ringInventory)
                    {
                        if (item.GetType() == ring.GetType())
                        {
                            isHave = true;
                            break;
                        }
                    }
                    break;
                case ItemType.Ammo:
                    foreach (RangedAmmoItem ammo in playerInventory.rangedAmmoInventory)
                    {
                        if (item.GetType() == ammo.GetType())
                        {
                            isHave = true;
                            break;
                        }
                    }
                    break;
            }

            return isHave;
        }
    }
}