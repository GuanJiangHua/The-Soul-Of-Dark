using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "格挡动作", menuName = "游戏物品/新建按键动作/(F键按住)格挡动作")]
    public class BlockAction : ItemAction
    {
        public override void PerformAction(PlayerManager player)
        {
            BlockingAction(player);
        }

        //执行举盾格挡动作: 
        private void BlockingAction(PlayerManager player)
        {
            if (player.isInteracting) return;
            if (player.isBlocking) return;

            player.isBlocking = true;

            WeaponItem rightWeapon = player.playerInventoryManager.rightWeapon;
            WeaponItem leftWeapon = player.playerInventoryManager.leftWeapon;
            bool isOffHandWeapon = leftWeapon != null && leftWeapon.weaponType != WeaponType.PyromancyCaster && leftWeapon.weaponType != WeaponType.SpellCaster && leftWeapon.weaponType != WeaponType.Unarmed;
            //双持状态:(或左手武器为空)
            if (player.inputHandler.twoHandFlag || leftWeapon.isUnarmed)
            {
                string blockAnim = rightWeapon.mainHandBlock; //使用右手武器的主武器格挡动作
                player.playerAnimatorManager.PlayTargetAnimation(blockAnim, false, true);
            }
            //左手武器不为空，且是盾牌武器
            else if (leftWeapon != null && (leftWeapon.weaponType == WeaponType.Shield || leftWeapon.weaponType == WeaponType.SmallShield))
            {
                player.playerAnimatorManager.PlayTargetAnimation("Block_Start", false, true);
            }
            //左手武器不为空，且是近战武器
            else if (isOffHandWeapon)
            {
                string blockAnim = leftWeapon.offHandBlock; //使用左手武器的副武器格挡动作
                player.playerAnimatorManager.PlayTargetAnimation(blockAnim, false, true);
            }
            player.playerEquipmentManager.OpenBlockingCollider();
        }
    }
}