using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "��������", menuName = "��Ϸ��Ʒ/�½���������/(Shift������)��������")]
    public class ParryAction : ItemAction
    {
        public override void PerformAction(PlayerManager player)
        {
            if (player.isInteracting)
                return;
            WeaponItem parryWeapon = player.playerInventoryManager.currentItemBeingUsed as WeaponItem;

            //С��:
            if(parryWeapon.weaponType == WeaponType.SmallShield)
            {
                player.playerAnimatorManager.PlayTargetAnimation("Parry", true, 0);
            }
            //�ж�:
            else if(parryWeapon.weaponType == WeaponType.Shield)
            {
                player.playerAnimatorManager.PlayTargetAnimation("Parry", true, 0);
            }
        }
    }
}