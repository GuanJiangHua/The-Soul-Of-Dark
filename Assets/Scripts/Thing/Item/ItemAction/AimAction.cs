using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "瞄准动作", menuName = "游戏物品/新建按键动作/(F键按住)瞄准动作")]
    public class AimAction : ItemAction
    {
        public override void PerformAction(PlayerManager player)
        {
            if (player.isAiming)
                return;

            player.inputHandler.uiManager.crossHair.SetActive(true);
            player.isAiming = true;
        }
    }
}