using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class AttackState : State
    {
        public PursueTargetState pursueTargetState;
        public RotateTowadsTargetState rotateTowadsTargetState;
        public EnemyAttackAction currentAttack;

        public bool hasPerformedAttack = false;                      //已经执行攻击;[当进入combatStanceState状态时会被设置为false（攻击只能于此状态进入）]

        bool willDoComboOnNextAttack = false;           //下次攻击可以连击;
        public override State Tick(EnemyManager enemyManager, EnemyStatsManager enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {
            float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
            RotateTowardsTargetWhilstAttacking(enemyManager);

            //大于距离执行跑动作
            if (distanceFromTarget > enemyManager.maximumAggroRadius)
            {
                return pursueTargetState;
            }

            if(willDoComboOnNextAttack && enemyManager.canDoCombo)
            {
                //使用连击
                AttackTargetWitthCombo(enemyManager , enemyStats ,enemyAnimatorManager);
                return rotateTowadsTargetState;
            }

            //没有执行过攻击
            if (hasPerformedAttack == false && distanceFromTarget<currentAttack.maxDistanceNededToAttack && distanceFromTarget>currentAttack.minDistanceNededToAttack)
            {
                //攻击
                AttackTarget(enemyManager , enemyStats , enemyAnimatorManager);
                //设置是否可以连击标记与攻击动作:
                RollForComboChance(enemyManager);
            }
            else if(distanceFromTarget > currentAttack.maxDistanceNededToAttack)
            {
                return pursueTargetState;
            }

            //可以连击并且执行过攻击
            if(willDoComboOnNextAttack && hasPerformedAttack)
            {
                return this;    //执行过此次攻击了,退回执行连击动作
            }

            //小于距离,并且不连击不攻击时，执行旋转向目标
            return rotateTowadsTargetState;
        }

        //攻击目标:
        private void AttackTarget(EnemyManager enemyManager, EnemyStatsManager enemyStatsManager ,EnemyAnimatorManager enemyAnimatorManager)
        {
            enemyManager.UpdateWhichHandCharacterIsUsing(false);
            enemyManager.enemyInventoryManager.currentItemBeingUsed = enemyManager.enemyInventoryManager.rightWeapon;
            //设置攻击间隔时间
            enemyManager.currentRecoveryTime = currentAttack.recoveryTime;

            enemyAnimatorManager.PlayTargetAnimation(currentAttack.acionAnimation, true);
            enemyAnimatorManager.PlayWeaponTrailFX();
            hasPerformedAttack = true;
        }

        //连击目标:
        private void AttackTargetWitthCombo(EnemyManager enemyManager , EnemyStatsManager enemyStatsManager, EnemyAnimatorManager enemyAnimatorManager)
        {
            enemyManager.UpdateWhichHandCharacterIsUsing(false);
            //设置攻击间隔时间
            enemyManager.currentRecoveryTime = currentAttack.recoveryTime;

            willDoComboOnNextAttack = false;
            enemyAnimatorManager.PlayTargetAnimation(currentAttack.acionAnimation, true);
            hasPerformedAttack = true;
            currentAttack = null;

            enemyAnimatorManager.PlayWeaponTrailFX();
        }

        //翻滚与连击几率计算:
        private void RollForComboChance(EnemyManager enemyManager)
        {
            float comboChance = Random.Range(0,100);   //连击几率;
            if (enemyManager.allowAIToPerformCombos && comboChance<=enemyManager.comboLikelyHood * 100)
            {
                if (currentAttack.comboAttack != null)
                {
                    willDoComboOnNextAttack = true;
                    currentAttack = currentAttack.comboAttack;
                }
                else
                {
                    willDoComboOnNextAttack = false;
                    currentAttack = null;
                }
            }
        }

        //攻击时向目标旋转:
        private void RotateTowardsTargetWhilstAttacking(EnemyManager enemyManager)
        {
            //手动旋转:
            if (enemyManager.canRotate && enemyManager.isInteracting)
            {
                Vector3 dir = Vector3.zero;
                dir = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
                dir.y = 0;
                dir.Normalize();
                if (dir == Vector3.zero)
                {
                    dir = enemyManager.transform.forward;
                }

                Quaternion targetRotation = Quaternion.LookRotation(dir);
                enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, targetRotation, 2*enemyManager.rotationSpeed / Time.deltaTime);
            }
        }
    }
}
