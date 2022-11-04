using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG {
    public class AmbushState : State
    {
        public bool isSleeping;
        public string sleepAnimation;                            //˯�߶���
        public string wakeAnimation;                            //���Ѷ���
        public float detectionRadius = 2;                       //���뾶
        public PursueTargetState pursueTargetState;     //׷��Ŀ��״̬

        public LayerMask detectionLayer;
        public override State Tick(EnemyManager enemyManager, EnemyStatsManager enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {
            if(isSleeping && enemyManager.isInteracting == false)
            {
                enemyAnimatorManager.PlayTargetAnimation(sleepAnimation, true);
            }

            #region ������Ŀ��
            Collider[] colliders = Physics.OverlapSphere(enemyManager.transform.position, detectionRadius, detectionLayer);

            if (colliders != null)
            {
                for(int i = 0; i < colliders.Length; i++)
                {
                    CharacterStatsManager characterStats = colliders[i].GetComponent<CharacterStatsManager>();
                    if (characterStats != null)
                    {
                        Vector3 targetsDir = characterStats.transform.position - enemyManager.transform.position;
                        float viewableAngle = Vector3.Angle(targetsDir, enemyManager.transform.forward);

                        if(viewableAngle < enemyManager.detectionAngle/2 && viewableAngle > -enemyManager.detectionAngle / 2)
                        {
                            enemyManager.currentTarget = characterStats;
                            isSleeping = false;
                            enemyAnimatorManager.PlayTargetAnimation(wakeAnimation, true);
                        }
                    }
                }
            }

            #endregion

            #region ����״̬�л�

            if(enemyManager.currentTarget != null)
            {
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
