using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "�����Ի�", menuName = "��Ϸ�Ի�ϵͳģ��/�½��¶Ի�ѡ��/�����Ի�")]
    public class QuitDialogue : OptionMethodClass
    {
        public override void OptionMethod(PlayerManager player, NpcManager npc)
        {
            player.playerAnimatorManager.PlayTargetAnimation("PullOutRightHandWeapon", true);
        }
    }
}