using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class LevelUpInteractable : Interactable
    {
        public override void Interact(PlayerManager playerManger)
        {
            playerManger.uiManager.playerLeveUpWindow.SetActive(true);  //������������
            //���Ƴ���:
            Vector3 dir =transform.position - playerManger.transform.position;
            dir.y = 0;
            dir.Normalize();
            Quaternion rt = Quaternion.LookRotation(dir);
            playerManger.transform.rotation = Quaternion.Slerp(playerManger.transform.rotation, rt, 500 * Time.deltaTime);

            Vector3 targetDir = playerManger.transform.position - transform.position;
            float viewableAngle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);//���� from �� to ֮����з��ŽǶȡ���ѡ���������н�С�Ľ��бȽϣ��ʽ���ڣ�-180,180��֮��;��form������˳ʱ����תx�ȵõ�to���ͷ�����x����ʱ����תx�ȵõ�to�ͷ���-x;

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

            //��������������:
            playerManger.playerAnimatorManager.PlayTargetAnimation("Praying", true);
        }
    }
}