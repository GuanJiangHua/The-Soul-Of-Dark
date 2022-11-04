using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "背刺动作", menuName = "游戏物品/新建按键动作/(左键按住)背刺动作")]
    public class CriticalAttackAction : ItemAction
    {
        public override void PerformAction(PlayerManager player)
        {
            if (player.isInteracting) return;

            player.playerCombatManager.AttemptBackStabOrRigoste();
        }
    }
}