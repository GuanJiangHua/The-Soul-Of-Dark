using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "弹反动作", menuName = "游戏物品/新建按键动作/(Shift键按下)弹反动作")]
    public class ParryAction : ItemAction
    {
        public override void PerformAction(PlayerManager player)
        {
            if (player.isInteracting)
                return;
            WeaponItem parryWeapon = player.playerInventoryManager.currentItemBeingUsed as WeaponItem;

            //小盾:
            if(parryWeapon.weaponType == WeaponType.SmallShield)
            {
                player.playerAnimatorManager.PlayTargetAnimation("Parry", true, 0);
            }
            //中盾:
            else if(parryWeapon.weaponType == WeaponType.Shield)
            {
                player.playerAnimatorManager.PlayTargetAnimation("Parry", true, 0);
            }
        }
    }
}