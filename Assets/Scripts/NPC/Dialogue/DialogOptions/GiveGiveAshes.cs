using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "�����ǻ�ѡ��", menuName = "��Ϸ�Ի�ϵͳģ��/�½��¶Ի�ѡ��/�����ǻ�ѡ��")]
    public class GiveGiveAshes : OptionMethodClass
    {
        [Header("�ǻ�������Ʒ:")]
        public AshesItem ashesItem;
        [Header("������Ի�:")]
        public string[] afterSelectionDialogue = { "Ŷ������һλ�����ˡ�", "Ҳ�գ�������������ʲô��ֵ�ɡ�" };
        public override void OptionMethod(PlayerManager player, NpcManager npc)
        {
            AshesItem ashes = null;
            //ɾ���ǻ�:
            foreach(ConsumableItem consumable in player.playerInventoryManager.consumableInventory)
            {
                if(consumable.GetType() == ashesItem.GetType())
                {
                    ashes = consumable as AshesItem;
                    player.playerInventoryManager.consumableInventory.Remove(consumable);
                    break;
                }
            }

            //��ȡ�浵:
            if (ashes != null)
            {
                List<Commodity> commoditieList = PlotProgressManager.single.GetStoreCommoditieList(ashes.shopownerName);
                foreach(Commodity commodity in commoditieList)
                {
                    StoreManager store = npc.storeManager;
                    store.AddCommodity(commodity);
                    Debug.Log(commodity.item.itemName);
                }
            }

            player.uiManager.dialogSystemWindow.SetActive(true);
            player.uiManager.dialogSystemWindowManager.GiveDialogueContentToUI(afterSelectionDialogue, true);
        }
    }
}