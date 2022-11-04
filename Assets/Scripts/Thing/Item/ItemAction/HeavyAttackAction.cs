using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "重击动作", menuName = "游戏物品/新建按键动作/(右键按下)重击动作")]
    public class HeavyAttackAction : ItemAction
    {
        public override void PerformAction(PlayerManager player)
        {
            if (player.isUsingLeftHand)
            {
                if (player.playerStateManager.currentStamina <= player.playerInventoryManager.leftWeapon.baseStamina)
                    return;

                //player.playerWeaponSlotManger.attackingWeapon = player.playerInventoryManager.leftWeapon;
            }
            else if (player.isUsingRightHand)
            {
                if (player.playerStateManager.currentStamina <= player.playerInventoryManager.rightWeapon.baseStamina)
                    return;

                //player.playerWeaponSlotManger.attackingWeapon = player.playerInventoryManager.rightWeapon;
            }

            WeaponType currentWeapon = player.playerInventoryManager.rightWeapon.weaponType;
            bool canJumpAttack = currentWeapon != WeaponType.SpellCaster 
                && currentWeapon != WeaponType.PyromancyCaster 
                && currentWeapon != WeaponType.FaithCaster 
                && currentWeapon != WeaponType.Unarmed;
            if (canJumpAttack)
            {
                //跳跃攻击检测:
                if (player.isSprinting)
                {
                    HandleJumpAttack(player);
                    return;
                }
            }
            //连击检测:
            if (player.canDoCombo)
            {
                player.inputHandler.comboFlag = true;
                HandleWeaponCombo(player);
                player.inputHandler.comboFlag = false;
            }
            else
            {
                if (player.canDoCombo)
                    return;
                if (player.isInteracting)
                    return;
                //重攻击检测:
                HandleHeavyAttack(player);
            }

            //启用右手拖尾特效:
            player.playerEffectsManager.PlayWeaponFX(player.isUsingLeftHand);
        }

        //武器重攻击:
        private void HandleHeavyAttack(PlayerManager player)
        {
            if (player.playerStateManager.currentStamina <= 10) return;

            player.playerAnimatorManager.PlayTargetAnimation(player.playerCombatManager.OH_Heavy_Attack_1, true);
            player.playerCombatManager.lastAttack = player.playerCombatManager.OH_Heavy_Attack_1;
        }

        //武器轻击连击动作:
        private void HandleWeaponCombo(PlayerManager player)
        {
            if (player.inputHandler.comboFlag)
            {
                player.playerAnimatorManager.anim.SetBool("canDoCombo", false);
                if (player.playerStateManager.currentStamina <= 10) return;
                //双手持握连击:
                if (player.inputHandler.twoHandFlag == true)
                {
                    player.UpdateWhichHandCharacterIsUsing(false);
                    if (player.playerCombatManager.lastAttack == player.playerCombatManager.OH_Heavy_Attack_1)
                    {
                        player.playerAnimatorManager.PlayTargetAnimation(player.playerCombatManager.OH_Heavy_Attack_2, true);
                        player.playerCombatManager.lastAttack = player.playerCombatManager.OH_Heavy_Attack_2;
                    }
                }
                //单手持握连击:
                else
                {
                    if (player.isUsingLeftHand)
                    {
                        if (player.playerCombatManager.lastAttack == player.playerCombatManager.OH_Heavy_Attack_1)
                        {
                            player.playerAnimatorManager.PlayTargetAnimation(player.playerCombatManager.OH_Heavy_Attack_2, true, false, true);
                            player.playerCombatManager.lastAttack = player.playerCombatManager.OH_Heavy_Attack_2;
                        }
                    }
                    else if (player.isUsingRightHand)
                    {
                        if (player.playerCombatManager.lastAttack == player.playerCombatManager.OH_Heavy_Attack_1)
                        {
                            player.playerAnimatorManager.PlayTargetAnimation(player.playerCombatManager.OH_Heavy_Attack_2, true);
                            player.playerCombatManager.lastAttack = player.playerCombatManager.OH_Heavy_Attack_2;
                        }
                    }
                }
            }
        }

        //冲刺重攻击:(跳劈)
        private void HandleJumpAttack(PlayerManager player)
        {
            if (player.isInteracting)
                return;
            if (player.playerStateManager.currentStamina <= 10)
                return;

            //playerWeaponSlotManger.attackingWeapon = weapon;

            if (player.isTwoHandingWeapon)
            {
                player.playerAnimatorManager.PlayTargetAnimation(player.playerCombatManager.TH_Jump_Attack_1, true);

                player.playerCombatManager.lastAttack = player.playerCombatManager.TH_Jump_Attack_1;
            }
            else
            {
                player.playerAnimatorManager.PlayTargetAnimation(player.playerCombatManager.OH_Jump_Attack_1, true);
                player.playerCombatManager.lastAttack = player.playerCombatManager.OH_Jump_Attack_1;
            }
        }
    }
}