using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    //追赶状态:
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

            //如果对象在处理其他行为，则停止移动:(如正在攻击或翻滚)
            if (enemyManager.isInteracting)
            {
                enemyAnimatorManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);            //将移动动画的参数设置为0;
                enemyManager.navMeshAgent.transform.localPosition = Vector3.zero;
                enemyManager.navMeshAgent.transform.localRotation = Quaternion.identity;
                enemyManager.navMeshAgent.enabled = false;
            }
            else
            {
                if (distanceFromTarget > enemyManager.maximumAggroRadius)
                {
                    enemyAnimatorManager.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);        //将移动动画的参数设置为1;
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

        //向目标旋转:
        private void HandleRotateTowardsTarget(EnemyManager enemyManager)
        {
            //手动旋转:
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
            //根据导航网格代理旋转:
            else
            {
                Vector3 relativeDir = enemyManager.transform.InverseTransformDirection(enemyManager.navMeshAgent.desiredVelocity);        //获取到导航网格的速度，并将其方向转化为本地坐标;
                Vector3 targetVelocity = enemyManager.enemyRigidbody.velocity;

                enemyManager.navMeshAgent.enabled = true;
                enemyManager.navMeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
                enemyManager.enemyRigidbody.velocity = targetVelocity;

                enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, enemyManager.navMeshAgent.transform.rotation, enemyManager.rotationSpeed / Time.deltaTime);
            }
        }
    }
}
