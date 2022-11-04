using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    //׷��״̬:
    public class PursueTargetState : State
    {
        public CombatStanceState combatStanceState;
        public RotateTowadsTargetState rotateTowadsTargetState;
        public override State Tick(EnemyManager enemyManager, EnemyStatsManager enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {
            Vector3 targetDir = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
            float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
            float viewableAngle = Vector3.SignedAngle(targetDir, enemyManager.transform.forward,Vector3.up);

            HandleRotateTowardsTarget(enemyManager);
            if (viewableAngle > 55 || viewableAngle < -55)
                return rotateTowadsTargetState;

            if (enemyStats.isDead) return null;

            //��������ڴ���������Ϊ����ֹͣ�ƶ�:(�����ڹ����򷭹�)
            if (enemyManager.isInteracting)
            {
                enemyAnimatorManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);            //���ƶ������Ĳ�������Ϊ0;
                enemyManager.navMeshAgent.transform.localPosition = Vector3.zero;
                enemyManager.navMeshAgent.transform.localRotation = Quaternion.identity;
                enemyManager.navMeshAgent.enabled = false;
            }
            else
            {
                if (distanceFromTarget > enemyManager.maximumAggroRadius)
                {
                    enemyAnimatorManager.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);        //���ƶ������Ĳ�������Ϊ1;
                    enemyAnimatorManager.anim.SetFloat("Horizontal", 0, 0.1f, Time.deltaTime);
                }
            }

            if(distanceFromTarget <= enemyManager.maximumAggroRadius)
            {
                return combatStanceState; 
            }
            else
            {
                return this;
            }

        }

        //��Ŀ����ת:
        private void HandleRotateTowardsTarget(EnemyManager enemyManager)
        {
            //�ֶ���ת:
            if (enemyManager.isPreformingAction)
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
                enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, targetRotation, enemyManager.rotationSpeed / Time.deltaTime);
            }
            //���ݵ������������ת:
            else
            {
                Vector3 relativeDir = enemyManager.transform.InverseTransformDirection(enemyManager.navMeshAgent.desiredVelocity);        //��ȡ������������ٶȣ������䷽��ת��Ϊ��������;
                Vector3 targetVelocity = enemyManager.enemyRigidbody.velocity;

                enemyManager.navMeshAgent.enabled = true;
                enemyManager.navMeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
                enemyManager.enemyRigidbody.velocity = targetVelocity;

                enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, enemyManager.navMeshAgent.transform.rotation, enemyManager.rotationSpeed / Time.deltaTime);
            }
        }
    }
}
