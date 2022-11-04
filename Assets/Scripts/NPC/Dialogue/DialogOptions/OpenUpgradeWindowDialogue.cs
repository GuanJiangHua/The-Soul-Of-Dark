using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG {
    //打开升级窗口:
    [CreateAssetMenu(fileName = "打开升级窗口选项", menuName = "游戏对话系统模块/新建新对话选项/打开升级窗口选项")]
    public class OpenUpgradeWindowDialogue : OptionMethodClass
    {
        public override void OptionMethod(PlayerManager playerManger, NpcManager npc)
        {
            playerManger.uiManager.playerLeveUpWindow.SetActive(true);  //启用升级窗口

            //播放人物祈祷动画:
            playerManger.playerAnimatorManager.PlayTargetAnimation("Praying", true);
        }
    }
}