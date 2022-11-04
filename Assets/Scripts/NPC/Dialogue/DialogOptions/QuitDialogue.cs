using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "结束对话", menuName = "游戏对话系统模块/新建新对话选项/结束对话")]
    public class QuitDialogue : OptionMethodClass
    {
        public override void OptionMethod(PlayerManager player, NpcManager npc)
        {
            player.playerAnimatorManager.PlayTargetAnimation("PullOutRightHandWeapon", true);
        }
    }
}