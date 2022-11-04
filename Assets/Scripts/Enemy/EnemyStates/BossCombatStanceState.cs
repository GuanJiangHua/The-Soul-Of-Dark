using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class BossCombatStanceState : CombatStanceState
    {
        [Header("boss�׶α仯����:")]
        public bool hasPhaseShifted;    //�н׶α仯
        public EnemyAttackAction[] secondPhaseAttacks;

        protected override void GetNawAttack(EnemyManager enemyManager)
        {
            if (hasPhaseShifted)
            {
                #region �ӵڶ��׶εĹ�����ʽ����ȡ��ʽ:
                Vector3 targetDir = enemyManager.currentTarget.transform.position - transform.position;
                float viewableAngle = Vector3.Angle(targetDir, transform.forward);              //���ӽǶ�
                float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, transform.position);
                int maxScore = 0;
                for (int i = 0; i < secondPhaseAttacks.Length; i++)
                {
                    EnemyAttackAction enemyAttackAction = secondPhaseAttacks[i];
                    if (distanceFromTarget < enemyAttackAction.maxDistanceNededToAttack && distanceFromTarget > enemyAttackAction.minDistanceNededToAttack)
                    {
                        if (viewableAngle < enemyAttackAction.maxAttackAngle && viewableAngle > enemyAttackAction.minAttackAngle)
                        {
                            maxScore += enemyAttackAction.attackScore;
                        }
                    }
                }

                int randomValue = Random.Range(0, maxScore);        //�����
                int temporaryScore = 0;                                                //��ʱ����

                for (int i = 0; i < secondPhaseAttacks.Length; i++)
                {
                    EnemyAttackAction enemyAttackAction = secondPhaseAttacks[i];
                    if (distanceFromTarget < enemyAttackAction.maxDistanceNededToAttack && distanceFromTarget > enemyAttackAction.minDistanceNededToAttack)
                    {
                        if (viewableAngle < enemyAttackAction.maxAttackAngle && viewableAngle > enemyAttackAction.minAttackAngle)
                        {
                            if (attackState.currentAttack != null) return;
                            temporaryScore += enemyAttackAction.attackScore;

                            if (temporaryScore > randomValue)
                            {
                                attackState.currentAttack = enemyAttackAction;
                            }
                        }
                    }
                }
                #endregion
            }
            else
            {
                base.GetNawAttack(enemyManager);
            }
        }
    }
}
