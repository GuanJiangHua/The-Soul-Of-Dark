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
            #region �����˵�Ŀ�����
            Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, detectionLayer);

            for (int i = 0; i < colliders.Length; i++)
            {
                CharacterStatsManager characterStats = colliders[i].transform.GetComponent<CharacterStatsManager>();

                if (characterStats != null)
                {
                    Vector3 targetDir = characterStats.transform.position - transform.position;
                    float viewbleAngle = Vector3.Angle(targetDir, transform.forward);                   //��Ұ�Ƕ�(�����ǰ���ļнǵĴ�С)

                    //����Ұ��Χ��:
                    if (viewbleAngle <= enemyManager.detectionAngle / 2 && viewbleAngle >= -enemyManager.detectionAngle / 2)
                    {
                        //�Ѿ��ж�:
                        if(characterStats.teamIDNumber != enemyStats.teamIDNumber)
                        {
                            enemyManager.currentTarget = characterStats;
                        }
                    }
                }
            }
            #endregion

            #region �л�����һ��״̬
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