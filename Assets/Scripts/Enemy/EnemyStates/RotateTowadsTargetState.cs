using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class RotateTowadsTargetState : State
    {
        public CombatStanceState combatStanceState;
        public override State Tick(EnemyManager enemyManager, EnemyStatsManager enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {
            enemyAnimatorManager.anim.SetFloat("Vertical", 0);
            enemyAnimatorManager.anim.SetFloat("Horizontal", 0);

            Vector3 targetDir = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
            float viewableAngle = Vector3.SignedAngle(targetDir, enemyManager.transform.forward, Vector3.up);//返回 from 和 to 之间的有符号角度。且选择两个角中较小的进行比较，故结果在（-180,180）之间;从form出发，顺时针旋转x度得到to，就返回正x，逆时针旋转x度得到to就返回-x;
            if (enemyManager.isInteracting)
                return this;

            if(viewableAngle>=100 && viewableAngle <= 180 && enemyManager.isInteracting == false)
            {
                enemyAnimatorManager.PlayTargetAnimationWitRootRotation("Rotate_Left_180",true);
                return combatStanceState;
            }
            else if(viewableAngle >= -180 && viewableAngle <= -101 && enemyManager.isInteracting == false)
            {
                enemyAnimatorManager.PlayTargetAnimationWitRootRotation("Rotate_Left_180", true);
                return combatStanceState;
            }
            else if(viewableAngle >= -100 && viewableAngle <= -45 && enemyManager.isInteracting == false)
            {
                enemyAnimatorManager.PlayTargetAnimationWitRootRotation("Rotate_Right_90", true);
                return combatStanceState;
            }
            else if (viewableAngle >= 45 && viewableAngle <= 100 && enemyManager.isInteracting == false)
            {
                enemyAnimatorManager.PlayTargetAnimationWitRootRotation("Rotate_Left_90", true);
                return combatStanceState;
            }
            else if(enemyManager.canRotate)
            {
                HandleRotateTowardsTarget(enemyManager);
            }
            return combatStanceState;
        }

        private void HandleRotateTowardsTarget(EnemyManager enemyManager)
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
    }
}