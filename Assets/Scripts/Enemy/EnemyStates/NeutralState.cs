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
        public bool needToTurn = false;        //需要转向:
        public override State Tick(EnemyManager enemyManager, EnemyStatsManager enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {
            if (firstEntry)
            {
                firstEntry = false;
                npcManager = enemyManager.npcManager;
            }

            //第一,收缩
            //收索目标,在范围内的就控制头看向他:
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
                        if (characterStats.teamIDNumber != enemyStats.teamIDNumber)
                        {
                            targetInRange = true;
                            enemyManager.currentTarget = characterStats;
                        }
                    }
                }
            }
            #endregion

            //再视野范围内:
            //如果
            if (enemyManager.currentTarget != null && targetInRange == true)
            {
                Vector3 targetDir = enemyManager.currentTarget.transform.position - transform.position;
                float viewbleAngle = Vector3.Angle(targetDir, transform.forward);                   //视野角度(与对象前方的夹角的大小)
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

            if (needToTurn)                           //需要转向;
            {
                StartCoroutine(AfterDamageRotation(enemyManager, enemyAnimatorManager));
                needToTurn = false;
            }
            //如果被攻击次数小于3,返回本状态：
            //如果被攻击次数大于3,返回收索攻击敌人状态:
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
                    enemyAnimatorManager.SetHeadWeight(0);                           //头部索具设置其权重为0;
                    enemyAnimatorManager.anim.SetBool("isNeutral", false);      //播放抽出武器动作:
                    enemyManager.npcManager.dialogueInteraction.enabled = false;
                    npcManager.favorability = -1;
                    return idleState;               //进入警备状态:
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
            float viewableAngle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);//返回 from 和 to 之间的有符号角度。且选择两个角中较小的进行比较，故结果在（-180,180）之间;从form出发，顺时针旋转x度得到to，就返回正x，逆时针旋转x度得到to就返回-x;

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