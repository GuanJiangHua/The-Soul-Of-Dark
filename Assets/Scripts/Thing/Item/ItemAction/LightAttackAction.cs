using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "�������" , menuName = "��Ϸ��Ʒ/�½���������/(�������)�������")]
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
                //��̹������:
                if (player.isSprinting)
                {
                    HandleRuningAttack( player);
                    return;
                }
            }
            //�������:
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
                //�ṥ�����:
                HandleLightAttack(player);
            }

            //����������β��Ч:
            player.playerEffectsManager.PlayWeaponFX(player.isUsingLeftHand);
        }

        //����ṥ������:(��̹���)
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

        //���������������:
        public void HandleWeaponCombo(PlayerManager player)
        {
            if (player.inputHandler.comboFlag)
            {
                player.playerAnimatorManager.anim.SetBool("canDoCombo", false);
                if (player.playerStateManager.currentStamina <= 10) return;
                //˫�ֳ�������:
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
                //���ֳ�������:
                else
                {
                    bool sameType = player.playerInventoryManager.rightWeapon.weaponType == player.playerInventoryManager.leftWeapon.weaponType;
                    //������������Ҫ����������������ͬ:
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

        //�����ṥ��:
        public void HandleLightAttack(PlayerManager player)
        {
            if (player.inputHandler.twoHandFlag == true)
            {
                player.UpdateWhichHandCharacterIsUsing(false);
                player.playerAnimatorManager.PlayTargetAnimation(player.playerCombatManager.Th_Light_Attack_1, true);            //�����������
                player.playerCombatManager.lastAttack = player.playerCombatManager.Th_Light_Attack_1;
                return;
            }
            //����������������ͬ,�������ǿ���
            bool sameType = player.playerInventoryManager.rightWeapon.weaponType == player.playerInventoryManager.leftWeapon.weaponType || player.playerInventoryManager.rightWeapon.weaponType == WeaponType.Unarmed;
            //��������������Ҫ��ͬ:(ì�ͽ��Ķ�������̫��)
            if (player.isUsingLeftHand && sameType)
            {
                player.playerAnimatorManager.PlayTargetAnimation(player.playerCombatManager.OH_Light_Attack_1, true , false , true);            //�����������
                player.playerCombatManager.lastAttack = player.playerCombatManager.OH_Light_Attack_1;
            }
            else if(player.isUsingRightHand)
            {
                player.playerAnimatorManager.PlayTargetAnimation(player.playerCombatManager.OH_Light_Attack_1, true);            //�����������
                player.playerCombatManager.lastAttack = player.playerCombatManager.OH_Light_Attack_1;
            }
        }
    }
}