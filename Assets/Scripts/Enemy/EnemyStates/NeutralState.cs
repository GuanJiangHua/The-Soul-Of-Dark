using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class NeutralState : State
    {
        public IdleState idleState;
        public LayerMask detectionLayer;
        public NpcManager npcManager;

        public int currentNumberAttacks = 0;
        bool firstEntry = true;
        bool targetInRange = false;
        public bool needToTurn = false;        //��Ҫת��:
        public override State Tick(EnemyManager enemyManager, EnemyStatsManager enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {
            if (firstEntry)
            {
                firstEntry = false;
                npcManager = enemyManager.npcManager;
            }

            //��һ,����
            //����Ŀ��,�ڷ�Χ�ڵľͿ���ͷ������:
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
                        if (characterStats.teamIDNumber != enemyStats.teamIDNumber)
                        {
                            targetInRange = true;
                            enemyManager.currentTarget = characterStats;
                        }
                    }
                }
            }
            #endregion

            //����Ұ��Χ��:
            //���
            if (enemyManager.currentTarget != null && targetInRange == true)
            {
                Vector3 targetDir = enemyManager.currentTarget.transform.position - transform.position;
                float viewbleAngle = Vector3.Angle(targetDir, transform.forward);                   //��Ұ�Ƕ�(�����ǰ���ļнǵĴ�С)
                float distance = Vector3.SqrMagnitude(targetDir);

                if (viewbleAngle <= enemyManager.detectionAngle / 2 && viewbleAngle >= -enemyManager.detectionAngle / 2 && distance < 9)
                {
                    enemyAnimatorManager.SetHeadWeight(1);
                    enemyAnimatorManager.SetHeadTargetPosition(transform.position + targetDir.normalized + new Vector3(0, 1.35f, 0));
                }
                else
                {
                    targetInRange = false;
                    enemyAnimatorManager.SetHeadTargetPosition(transform.position + transform.forward + new Vector3(0, 1.35f, 0));
                }
            }
            else
            {
                enemyAnimatorManager.SetHeadTargetPosition(transform.position + transform.forward + new Vector3(0, 1.35f, 0));
            }

            if (needToTurn)                           //��Ҫת��;
            {
                StartCoroutine(AfterDamageRotation(enemyManager, enemyAnimatorManager));
                needToTurn = false;
            }
            //�������������С��3,���ر�״̬��
            //�����������������3,����������������״̬:
            if (npcManager.numberAttacks < 3)
            {
                if(npcManager.numberAttacks != currentNumberAttacks)
                {
                    needToTurn = true;
                    currentNumberAttacks = npcManager.numberAttacks;
                }
                return this;
            }
            else
            {
                if (idleState != null)
                {
                    enemyManager.enemyAnimatorManager.rigBuilder.enabled = true;
                    enemyAnimatorManager.SetHeadWeight(0);                           //ͷ������������Ȩ��Ϊ0;
                    enemyAnimatorManager.anim.SetBool("isNeutral", false);      //���ų����������:
                    enemyManager.npcManager.dialogueInteraction.enabled = false;
                    npcManager.favorability = -1;
                    return idleState;               //���뾯��״̬:
                }
                else
                {
                    return this;
                }
            }
        }

        private void Rotation(EnemyManager enemyManager, EnemyAnimatorManager enemyAnimatorManager)
        {
            if (enemyManager.currentTarget == null) 
            {
                return;
            }
            
            Vector3 targetDir = enemyManager.currentTarget.transform.position - transform.position;
            float viewableAngle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);//���� from �� to ֮����з��ŽǶȡ���ѡ���������н�С�Ľ��бȽϣ��ʽ���ڣ�-180,180��֮��;��form������˳ʱ����תx�ȵõ�to���ͷ�����x����ʱ����תx�ȵõ�to�ͷ���-x;

            if (viewableAngle >= 100 && viewableAngle <= 180)
            {
                enemyAnimatorManager.PlayTargetAnimationWitRootRotation("Rotate_Left_180", true);
            }
            else if (viewableAngle >= -180 && viewableAngle <= -101)
            {
                enemyAnimatorManager.PlayTargetAnimationWitRootRotation("Rotate_Left_180", true);
            }
            else if (viewableAngle >= -100 && viewableAngle <= -45)
            {
                enemyAnimatorManager.PlayTargetAnimationWitRootRotation("Rotate_Right_90", true);
            }
            else if (viewableAngle >= 45 && viewableAngle <= 100)
            {
                enemyAnimatorManager.PlayTargetAnimationWitRootRotation("Rotate_Left_90", true);
            }
        }

        IEnumerator AfterDamageRotation(EnemyManager enemyManager, EnemyAnimatorManager enemyAnimatorManager)
        {
            if (enemyManager.currentTarget != null)
            {
                while (enemyManager.isInteracting)
                {
                    yield return null;
                }
                Rotation(enemyManager, enemyAnimatorManager);
            }
        }
    }
}