using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class IdleState : State
    {
        public PursueTargetState pursueTargetState;
        public LayerMask detectionLayer;
        public override State Tick(EnemyManager enemyManager, EnemyStatsManager enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {
            #region 检测敌人的目标对象
            Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, detectionLayer);

            for (int i = 0; i < colliders.Length; i++)
            {
                CharacterStatsManager characterStats = colliders[i].transform.GetComponent<CharacterStatsManager>();

                if (characterStats != null)
                {
                    Vector3 targetDir = characterStats.transform.position - transform.position;
                    float viewbleAngle = Vector3.Angle(targetDir, transform.forward);                   //视野角度(与对象前方的夹角的大小)

                    //再视野范围内:
                    if (viewbleAngle <= enemyManager.detectionAngle / 2 && viewbleAngle >= -enemyManager.detectionAngle / 2)
                    {
                        //友军判断:
                        if(characterStats.teamIDNumber != enemyStats.teamIDNumber)
                        {
                            enemyManager.currentTarget = characterStats;
                        }
                    }
                }
            }
            #endregion

            #region 切换到下一个状态
            if (enemyManager.currentTarget != null)
            {
                if (enemyManager.isPutWeapons)
                {
                    enemyAnimatorManager.PlayTargetAnimation("RightHand_GetWeapon", true);
                }

                return pursueTargetState;
            }
            else
            {
                return this;
            }
            #endregion
        }
    }
}