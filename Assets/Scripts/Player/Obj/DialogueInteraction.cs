using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class DialogueInteraction : Interactable
    {
        NpcManager npcManager;
        private void Awake()
        {
            npcManager = GetComponentInParent<NpcManager>();
        }
        public override void Interact(PlayerManager playerManger)
        {
            Rotation(playerManger);
            playerManger.playerAnimatorManager.PlayTargetAnimation("PutAwayRightHandWeapon", true);
            //对话交互:
            npcManager.currentDialogContent = npcManager.currentDialogueAndPlot.GiveDialogue(playerManger , npcManager);            //1,获取对话内容:
            //对话选项:
            OptionMethodClass[] optionMethods = npcManager.currentDialogueAndPlot.GetOptions();
            playerManger.uiManager.dialogSystemWindow.SetActive(true);
            playerManger.uiManager.dialogSystemWindowManager.GiveDialogueContentToUI(npcManager.currentDialogContent, true , optionMethods, playerManger, npcManager);
        }

        private void Rotation(PlayerManager playerManger)
        {
            //控制朝向:
            Vector3 dir = transform.position - playerManger.transform.position;
            dir.y = 0;
            dir.Normalize();
            Quaternion rt = Quaternion.LookRotation(dir);
            playerManger.transform.rotation = Quaternion.Slerp(playerManger.transform.rotation, rt, 500 * Time.deltaTime);

            Vector3 targetDir = playerManger.transform.position - transform.position;
            float viewableAngle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);//返回 from 和 to 之间的有符号角度。且选择两个角中较小的进行比较，故结果在（-180,180）之间;从form出发，顺时针旋转x度得到to，就返回正x，逆时针旋转x度得到to就返回-x;

            if (viewableAngle >= 100 && viewableAngle <= 180)
            {
                npcManager.GetComponent<EnemyAnimatorManager>().PlayTargetAnimationWitRootRotation("Rotate_Left_180", true);
            }
            else if (viewableAngle >= -180 && viewableAngle <= -101)
            {
                npcManager.GetComponent<EnemyAnimatorManager>().PlayTargetAnimationWitRootRotation("Rotate_Left_180", true);
            }
            else if (viewableAngle >= -100 && viewableAngle <= -45)
            {
                npcManager.GetComponent<EnemyAnimatorManager>().PlayTargetAnimationWitRootRotation("Rotate_Right_90", true);
            }
            else if (viewableAngle >= 45 && viewableAngle <= 100)
            {
                npcManager.GetComponent<EnemyAnimatorManager>().PlayTargetAnimationWitRootRotation("Rotate_Left_90", true);
            }
        }
    }
}