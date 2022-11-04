using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class BossCombatStanceState : CombatStanceState
    {
        [Header("boss阶段变化攻击:")]
        public bool hasPhaseShifted;    //有阶段变化
        public EnemyAttackAction[] secondPhaseAttacks;

        protected override void GetNawAttack(EnemyManager enemyManager)
        {
            if (hasPhaseShifted)
            {
                #region 从第二阶段的攻击方式数组取招式:
                Vector3 targetDir = enemyManager.currentTarget.transform.position - transform.position;
                float viewableAngle = Vector3.Angle(targetDir, transform.forward);              //可视角度
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

                int randomValue = Random.Range(0, maxScore);        //随机数
                int temporaryScore = 0;                                                //临时分数

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
