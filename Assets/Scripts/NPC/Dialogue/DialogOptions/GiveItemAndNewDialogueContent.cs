using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "����ĳ��Ʒ", menuName = "��Ϸ�Ի�ϵͳģ��/�½��¶Ի�ѡ��/������Ʒ")]
    public class GiveItemAndNewDialogueContent : OptionMethodClass
    {
        [Header("��Ʒ:")]
        public Item item;
        public ItemType itemType;
        [Header("�¶Ի�����:")]
        [TextArea] public string[] newDialogueContent= {"��������ɡ�" };
        public override void OptionMethod(PlayerManager player, NpcManager npc)
        {
            switch (itemType)
            {
                case ItemType.Weapon:
                    player.playerInventoryManager.weaponInventory.Add((WeaponItem)item);
                    break;
                case ItemType.Helmet:
                    player.playerInventoryManager.headEquipmentInventory.Add((HelmetEquipment)item);
                    break;
                case ItemType.Torso:
                    player.playerInventoryManager.torsoEquipmentInventory.Add((TorsoEquipment)item);
                    break;
                case ItemType.Leg:
                    player.playerInventoryManager.legEquipmentInventory.Add((LegEquipment)item);
                    break;
                case ItemType.Hand:
                    player.playerInventoryManager.handEquipmentInventory.Add((HandEquipment)item);
                    break;
                case ItemType.Spell:
                    player.playerInventoryManager.spellleInventory.Add((SpellItem)item);
                    break;
                case ItemType.Consumable:
                    player.playerInventoryManager.AddConsumableItemToInventory((ConsumableItem)item);
                    break;
                case ItemType.Ring:
                    player.playerInventoryManager.ringInventory.Add((RingItem)item);
                    break;
                case ItemType.Ammo:
                    player.playerInventoryManager.AddAmmoItemToInventory((RangedAmmoItem)item);
                    break;
            }

            npc.uiManager.dialogSystemWindow.SetActive(true);
            npc.uiManager.dialogSystemWindowManager.GiveDialogueContentToUI(newDialogueContent, true);
        }
    }
}