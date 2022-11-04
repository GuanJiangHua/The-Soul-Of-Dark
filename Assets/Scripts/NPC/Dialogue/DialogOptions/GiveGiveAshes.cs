using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "给出骨灰选项", menuName = "游戏对话系统模块/新建新对话选项/给出骨灰选项")]
    public class GiveGiveAshes : OptionMethodClass
    {
        [Header("骨灰类型物品:")]
        public AshesItem ashesItem;
        [Header("给出后对话:")]
        public string[] afterSelectionDialogue = { "哦？又是一位可怜人。", "也罢，来看看他还有什么价值吧。" };
        public override void OptionMethod(PlayerManager player, NpcManager npc)
        {
            AshesItem ashes = null;
            //删除骨灰:
            foreach(ConsumableItem consumable in player.playerInventoryManager.consumableInventory)
            {
                if(consumable.GetType() == ashesItem.GetType())
                {
                    ashes = consumable as AshesItem;
                    player.playerInventoryManager.consumableInventory.Remove(consumable);
                    break;
                }
            }

            //读取存档:
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