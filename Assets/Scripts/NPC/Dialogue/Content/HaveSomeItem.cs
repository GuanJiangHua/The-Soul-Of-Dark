using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "有某物品", menuName = "游戏对话系统模块/新建对话条件判断/有某物品")]
    public class HaveSomeItem : ConditionClass
    {
        public Item item;
        public ItemType inspectionItemType;
        public override bool CheckConditionsMet(PlayerManager playerManager)
        {
            bool isHeave = false;
            switch (inspectionItemType)
            {
                case ItemType.Weapon:
                    isHeave = ItemExistsInList<WeaponItem>((WeaponItem)item , playerManager.playerInventoryManager.weaponInventory);
                    break;
                case ItemType.Helmet:
                    isHeave = ItemExistsInList<HelmetEquipment>((HelmetEquipment)item, playerManager.playerInventoryManager.headEquipmentInventory);
                    break;
                case ItemType.Torso:
                    isHeave = ItemExistsInList<TorsoEquipment>((TorsoEquipment)item, playerManager.playerInventoryManager.torsoEquipmentInventory);
                    break;
                case ItemType.Leg:
                    isHeave = ItemExistsInList<TorsoEquipment>((TorsoEquipment)item, playerManager.playerInventoryManager.torsoEquipmentInventory);
                    break;
                case ItemType.Hand:
                    isHeave = ItemExistsInList<HandEquipment>((HandEquipment)item, playerManager.playerInventoryManager.handEquipmentInventory);
                    break;
                case ItemType.Spell:
                    isHeave = ItemExistsInList<SpellItem>((SpellItem)item, playerManager.playerInventoryManager.spellleInventory);
                    break;
                case ItemType.Consumable:
                    isHeave = ItemExistsInList<ConsumableItem>((ConsumableItem)item, playerManager.playerInventoryManager.consumableInventory);
                    break;
                case ItemType.Ring:
                    isHeave = ItemExistsInList<RingItem>((RingItem)item, playerManager.playerInventoryManager.ringInventory);
                    break;
                case ItemType.Ammo:
                    isHeave = ItemExistsInList<RangedAmmoItem>((RangedAmmoItem)item, playerManager.playerInventoryManager.rangedAmmoInventory);
                    break;
            }

            return isHeave;
        }

        private bool ItemExistsInList<T>(T item,List<T> inventory)
        {
            for(int i=0;i < inventory.Count; i++)
            {
                if (item.Equals(inventory[i]))
                {
                    return true;
                }
            }

            return false;
        }
    }
}