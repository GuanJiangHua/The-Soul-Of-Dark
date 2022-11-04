using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "���̶���", menuName = "��Ϸ��Ʒ/�½���������/(�����ס)���̶���")]
    public class CriticalAttackAction : ItemAction
    {
        public override void PerformAction(PlayerManager player)
        {
            if (player.isInteracting) return;

            player.playerCombatManager.AttemptBackStabOrRigoste();
        }
    }
}