using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "轻击动作" , menuName = "游戏物品/新建按键动作/(左键按下)轻击动作")]
    public class LightAttackAction : ItemAction
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

            WeaponType rightWeaponType = player.playerInventoryManager.rightWeapon.weaponType;
            bool isCanRuningAttack = rightWeaponType == WeaponType.StraightSword 
                || rightWeaponType == WeaponType.Spear 
                || rightWeaponType == WeaponType.BluntInstrument 
                || rightWeaponType == WeaponType.Dagger
                || rightWeaponType == WeaponType.BigSword;

            if (isCanRuningAttack)
            {
                //冲刺攻击检测:
                if (player.isSprinting)
                {
                    HandleRuningAttack( player);
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
                //轻攻击检测:
                HandleLightAttack(player);
            }

            //启用右手拖尾特效:
            player.playerEffectsManager.PlayWeaponFX(player.isUsingLeftHand);
        }

        //冲刺轻攻击动作:(冲刺攻击)
        private void HandleRuningAttack(PlayerManager player)
        {
            if (player.isInteracting)
                return;

            if (player.isTwoHandingWeapon)
            {
                player.UpdateWhichHandCharacterIsUsing(false);
                player.playerAnimatorManager.PlayTargetAnimation(player.playerCombatManager.TH_Runing_Attack_1, true);
                player.playerCombatManager.lastAttack = player.playerCombatManager.TH_Runing_Attack_1;
            }
            else
            {
                if (player.isUsingLeftHand)
                {
                    player.playerAnimatorManager.PlayTargetAnimation(player.playerCombatManager.OH_Runing_Attack_1, true , false , true);
                    player.playerCombatManager.lastAttack = player.playerCombatManager.OH_Runing_Attack_1;
                }
                else if(player.isUsingRightHand)
                {
                    player.playerAnimatorManager.PlayTargetAnimation(player.playerCombatManager.OH_Runing_Attack_1, true);
                    player.playerCombatManager.lastAttack = player.playerCombatManager.OH_Runing_Attack_1;
                }

            }
        }

        //武器轻击连击动作:
        public void HandleWeaponCombo(PlayerManager player)
        {
            if (player.inputHandler.comboFlag)
            {
                player.playerAnimatorManager.anim.SetBool("canDoCombo", false);
                if (player.playerStateManager.currentStamina <= 10) return;
                //双手持握连击:
                if (player.inputHandler.twoHandFlag == true)
                {
                    player.UpdateWhichHandCharacterIsUsing(false);
                    if (player.playerCombatManager.lastAttack == player.playerCombatManager.Th_Light_Attack_1)
                    {
                        player.playerAnimatorManager.PlayTargetAnimation(player.playerCombatManager.Th_Light_Attack_2, true);
                        player.playerCombatManager.lastAttack = player.playerCombatManager.Th_Light_Attack_2;
                    }
                    else if (player.playerCombatManager.lastAttack == player.playerCombatManager.Th_Light_Attack_2)
                    {
                        player.playerAnimatorManager.PlayTargetAnimation(player.playerCombatManager.Th_Light_Attack_1, true);
                        player.playerCombatManager.lastAttack = player.playerCombatManager.Th_Light_Attack_1;
                    }
                }
                //单手持握连击:
                else
                {
                    bool sameType = player.playerInventoryManager.rightWeapon.weaponType == player.playerInventoryManager.leftWeapon.weaponType;
                    //左手武器类型要与右手武器类型相同:
                    if (player.isUsingLeftHand && sameType)
                    {
                        if (player.playerCombatManager.lastAttack == player.playerCombatManager.OH_Light_Attack_1)
                        {
                            player.playerAnimatorManager.PlayTargetAnimation(player.playerCombatManager.OH_Light_Attack_2, true , false , true);
                            player.playerCombatManager.lastAttack = player.playerCombatManager.OH_Light_Attack_2;
                        }
                        else if (player.playerCombatManager.lastAttack == player.playerCombatManager.OH_Light_Attack_2)
                        {
                            player.playerAnimatorManager.PlayTargetAnimation(player.playerCombatManager.OH_Light_Attack_3, true , false, true);
                            player.playerCombatManager.lastAttack = player.playerCombatManager.OH_Light_Attack_3;
                        }
                        else if (player.playerCombatManager.lastAttack == player.playerCombatManager.OH_Light_Attack_3)
                        {
                            player.playerAnimatorManager.PlayTargetAnimation(player.playerCombatManager.OH_Light_Attack_1, true, false, true);
                            player.playerCombatManager.lastAttack = player.playerCombatManager.OH_Light_Attack_1;
                        }
                    }
                    else if(player.isUsingRightHand)
                    {
                        if (player.playerCombatManager.lastAttack == player.playerCombatManager.OH_Light_Attack_1)
                        {
                            player.playerAnimatorManager.PlayTargetAnimation(player.playerCombatManager.OH_Light_Attack_2, true);
                            player.playerCombatManager.lastAttack = player.playerCombatManager.OH_Light_Attack_2;
                        }
                        else if (player.playerCombatManager.lastAttack == player.playerCombatManager.OH_Light_Attack_2)
                        {
                            player.playerAnimatorManager.PlayTargetAnimation(player.playerCombatManager.OH_Light_Attack_3, true);
                            player.playerCombatManager.lastAttack = player.playerCombatManager.OH_Light_Attack_3;
                        }
                        else if (player.playerCombatManager.lastAttack == player.playerCombatManager.OH_Light_Attack_3)
                        {
                            player.playerAnimatorManager.PlayTargetAnimation(player.playerCombatManager.OH_Light_Attack_1, true);
                            player.playerCombatManager.lastAttack = player.playerCombatManager.OH_Light_Attack_1;
                        }
                    }
                }
            }
        }

        //武器轻攻击:
        public void HandleLightAttack(PlayerManager player)
        {
            if (player.inputHandler.twoHandFlag == true)
            {
                player.UpdateWhichHandCharacterIsUsing(false);
                player.playerAnimatorManager.PlayTargetAnimation(player.playerCombatManager.Th_Light_Attack_1, true);            //播放轻击动画
                player.playerCombatManager.lastAttack = player.playerCombatManager.Th_Light_Attack_1;
                return;
            }
            //左右手武器类型相同,或右手是空手
            bool sameType = player.playerInventoryManager.rightWeapon.weaponType == player.playerInventoryManager.leftWeapon.weaponType || player.playerInventoryManager.rightWeapon.weaponType == WeaponType.Unarmed;
            //左右手武器类型要相同:(矛和剑的动作差异太大)
            if (player.isUsingLeftHand && sameType)
            {
                player.playerAnimatorManager.PlayTargetAnimation(player.playerCombatManager.OH_Light_Attack_1, true , false , true);            //播放轻击动画
                player.playerCombatManager.lastAttack = player.playerCombatManager.OH_Light_Attack_1;
            }
            else if(player.isUsingRightHand)
            {
                player.playerAnimatorManager.PlayTargetAnimation(player.playerCombatManager.OH_Light_Attack_1, true);            //播放轻击动画
                player.playerCombatManager.lastAttack = player.playerCombatManager.OH_Light_Attack_1;
            }
        }
    }
}