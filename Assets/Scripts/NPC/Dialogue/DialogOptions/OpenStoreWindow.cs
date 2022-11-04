using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "打开商店窗口选项", menuName = "游戏对话系统模块/新建新对话选项/打开商店窗口选项")]
    public class OpenStoreWindow : OptionMethodClass
    {
        public override void OptionMethod(PlayerManager player, NpcManager npc)
        {
            //打开商店窗口:
            player.uiManager.EnableStoreWindow(npc);
        }
    }
}