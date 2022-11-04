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

        public bool hasPerformedAttack = false;                      //�Ѿ�ִ�й���;[������combatStanceState״̬ʱ�ᱻ����Ϊfalse������ֻ���ڴ�״̬���룩]

        bool willDoComboOnNextAttack = false;           //�´ι�����������;
        public override State Tick(EnemyManager enemyManager, EnemyStatsManager enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {
            float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
            RotateTowardsTargetWhilstAttacking(enemyManager);

            //���ھ���ִ���ܶ���
            if (distanceFromTarget > enemyManager.maximumAggroRadius)
            {
                return pursueTargetState;
            }

            if(willDoComboOnNextAttack && enemyManager.canDoCombo)
            {
                //ʹ������
                AttackTargetWitthCombo(enemyManager , enemyStats ,enemyAnimatorManager);
                return rotateTowadsTargetState;
            }

            //û��ִ�й�����
            if (hasPerformedAttack == false && distanceFromTarget<currentAttack.maxDistanceNededToAttack && distanceFromTarget>currentAttack.minDistanceNededToAttack)
            {
                //����
                AttackTarget(enemyManager , enemyStats , enemyAnimatorManager);
                //�����Ƿ������������빥������:
                RollForComboChance(enemyManager);
            }
            else if(distanceFromTarget > currentAttack.maxDistanceNededToAttack)
            {
                return pursueTargetState;
            }

            //������������ִ�й�����
            if(willDoComboOnNextAttack && hasPerformedAttack)
            {
                return this;    //ִ�й��˴ι�����,�˻�ִ����������
            }

            //С�ھ���,���Ҳ�����������ʱ��ִ����ת��Ŀ��
            return rotateTowadsTargetState;
        }

        //����Ŀ��:
        private void AttackTarget(EnemyManager enemyManager, EnemyStatsManager enemyStatsManager ,EnemyAnimatorManager enemyAnimatorManager)
        {
            enemyManager.UpdateWhichHandCharacterIsUsing(false);
            enemyManager.enemyInventoryManager.currentItemBeingUsed = enemyManager.enemyInventoryManager.rightWeapon;
            //���ù������ʱ��
            enemyManager.currentRecoveryTime = currentAttack.recoveryTime;

            enemyAnimatorManager.PlayTargetAnimation(currentAttack.acionAnimation, true);
            enemyAnimatorManager.PlayWeaponTrailFX();
            hasPerformedAttack = true;
        }

        //����Ŀ��:
        private void AttackTargetWitthCombo(EnemyManager enemyManager , EnemyStatsManager enemyStatsManager, EnemyAnimatorManager enemyAnimatorManager)
        {
            enemyManager.UpdateWhichHandCharacterIsUsing(false);
            //���ù������ʱ��
            enemyManager.currentRecoveryTime = currentAttack.recoveryTime;

            willDoComboOnNextAttack = false;
            enemyAnimatorManager.PlayTargetAnimation(currentAttack.acionAnimation, true);
            hasPerformedAttack = true;
            currentAttack = null;

            enemyAnimatorManager.PlayWeaponTrailFX();
        }

        //�������������ʼ���:
        private void RollForComboChance(EnemyManager enemyManager)
        {
            float comboChance = Random.Range(0,100);   //��������;
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

        //����ʱ��Ŀ����ת:
        private void RotateTowardsTargetWhilstAttacking(EnemyManager enemyManager)
        {
            //�ֶ���ת:
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
