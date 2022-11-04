using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class CombatStanceState : State
    {
        public AttackState attackState;
        public PursueTargetState pursueTargetState;
        public RotateTowadsTargetState rotateTowadsTargetState;
        [Header("���й�����ʽ:")]
        public EnemyAttackAction[] enemyAttacks;

        protected bool randomDestinationSet = false;
        protected float verticalMovementValue = 0;
        protected float horizontalMovementValue = 0;

        //�ޱ�֮�˵��޸�:
        float changeDirectionMovementTimer = 0;
        //end
        public override State Tick(EnemyManager enemyManager, EnemyStatsManager enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {
            //�ޱ�֮�˵��޸�:
            changeDirectionMovementTimer -= Time.deltaTime;
            Vector3 targetDir = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
            float viewableAngle = Vector3.SignedAngle(targetDir, enemyManager.transform.forward, Vector3.up);
            if (changeDirectionMovementTimer <= 0) 
                randomDestinationSet = false;
            //end

            float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
            enemyAnimatorManager.anim.SetFloat("Vertical", verticalMovementValue, 0.2f, Time.deltaTime);
            enemyAnimatorManager.anim.SetFloat("Horizontal", horizontalMovementValue, 0.2f, Time.deltaTime);
            attackState.hasPerformedAttack = false;

            if(enemyManager.isInteracting == true)
            {
                enemyAnimatorManager.anim.SetFloat("Vertical", 0);
                enemyAnimatorManager.anim.SetFloat("Horizontal", 0);
                return this;
            }
            else if (distanceFromTarget > enemyManager.maximumAggroRadius)
            {
                return pursueTargetState;
            }

            /*�ޱ�֮�˵��޸�:
            if (viewableAngle > 65 || viewableAngle < -65)
            {
                changeDirectionMovementTimer = 0;
                return rotateTowadsTargetState;
            }*/

            //end
            HandleRotateTowardsTarget(enemyManager);

            if (randomDestinationSet == false)
            {
                //�ޱ�֮�˵��޸�:
                changeDirectionMovementTimer = 3;
                //end

                randomDestinationSet = true;
                DecideCirclingAction(enemyAnimatorManager);
            }

            if (enemyManager.currentRecoveryTime <= 0 && attackState.currentAttack !=null)
            {
                return attackState;
            }
            else
            {
                GetNawAttack(enemyManager);
            }

            return this;
        }

        protected void HandleRotateTowardsTarget(EnemyManager enemyManager)
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
        //����תȦ�ƶ�:
        protected void DecideCirclingAction(EnemyAnimatorManager enemyAnimatorManager)
        {
            //����ǰ��ֱ�ƶ���ԲȦ
            //�ܶ���ȦȦ
            //ֻ��·��ԲȦ
            WalkAroundTarget(enemyAnimatorManager);
        }
        //����Ŀ���ƶ�:
        protected void WalkAroundTarget(EnemyAnimatorManager enemyAnimatorManager)
        {
            verticalMovementValue = Random.Range(-1, 1);
            if(verticalMovementValue <= 1 && verticalMovementValue >= -0.5f)
            {
                verticalMovementValue = 1f;
            }
            else if (verticalMovementValue < -0.5f && verticalMovementValue >= -0.75f)
            {
                verticalMovementValue = 0.5f;
            }
            else if (verticalMovementValue < 0.75f && verticalMovementValue >= -1)
            {
                verticalMovementValue = 0;
            }

            horizontalMovementValue = Random.Range(-1, 1);
            if(horizontalMovementValue>=0 && horizontalMovementValue <= 1)
            {
                horizontalMovementValue = 1;
            }
            else if(horizontalMovementValue>=-1 && horizontalMovementValue <= 0)
            {
                horizontalMovementValue = -1f;
            }
        }

        //��ȡ�¹�������:
        protected virtual void GetNawAttack(EnemyManager enemyManager)
        {
            Vector3 targetDir = enemyManager.currentTarget.transform.position - transform.position;
            float viewableAngle = Vector3.Angle(targetDir, transform.forward);              //���ӽǶ�
            float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, transform.position);
            int maxScore = 0;
            for (int i = 0; i < enemyAttacks.Length; i++)
            {
                EnemyAttackAction enemyAttackAction = enemyAttacks[i];
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

            for (int i = 0; i < enemyAttacks.Length; i++)
            {
                EnemyAttackAction enemyAttackAction = enemyAttacks[i];
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
        }
    }
}
