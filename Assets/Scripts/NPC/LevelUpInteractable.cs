using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class LevelUpInteractable : Interactable
    {
        public override void Interact(PlayerManager playerManger)
        {
            playerManger.uiManager.playerLeveUpWindow.SetActive(true);  //启用升级窗口
            //控制朝向:
            Vector3 dir =transform.position - playerManger.transform.position;
            dir.y = 0;
            dir.Normalize();
            Quaternion rt = Quaternion.LookRotation(dir);
            playerManger.transform.rotation = Quaternion.Slerp(playerManger.transform.rotation, rt, 500 * Time.deltaTime);

            Vector3 targetDir = playerManger.transform.position - transform.position;
            float viewableAngle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);//返回 from 和 to 之间的有符号角度。且选择两个角中较小的进行比较，故结果在（-180,180）之间;从form出发，顺时针旋转x度得到to，就返回正x，逆时针旋转x度得到to就返回-x;

            if (viewableAngle >= 100 && viewableAngle <= 180)
            {
                GetComponent<EnemyAnimatorManager>().PlayTargetAnimationWitRootRotation("Rotate_Left_180", true);
            }
            else if (viewableAngle >= -180 && viewableAngle <= -101)
            {
                GetComponent<EnemyAnimatorManager>().PlayTargetAnimationWitRootRotation("Rotate_Left_180", true);
            }
            else if (viewableAngle >= -100 && viewableAngle <= -45)
            {
                GetComponent<EnemyAnimatorManager>().PlayTargetAnimationWitRootRotation("Rotate_Right_90", true);
            }
            else if (viewableAngle >= 45 && viewableAngle <= 100)
            {
                GetComponent<EnemyAnimatorManager>().PlayTargetAnimationWitRootRotation("Rotate_Left_90", true);
            }

            //播放人物祈祷动画:
            playerManger.playerAnimatorManager.PlayTargetAnimation("Praying", true);
        }
    }
}