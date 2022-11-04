using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class DoorInteractable : Interactable
    {
        [Header("是大门:")]
        public bool isBigDoor;
        [Header("只能从前面开门:")]
        public bool isOpenDoorFromFront;
        [Header("是否已经开启:")]
        public bool isAlreadyTurnedOn;
        [Header("开门条件:")]
        public ConditionClass openTheDoorCondition;
        Animator animator;
        Collider interactableCollider;
        private void Awake()
        {
            animator = GetComponent<Animator>();
            interactableCollider = GetComponent<Collider>();
        }
        public override void Interact(PlayerManager playerManger)
        {
            if (isOpenDoorFromFront)
            {
                Vector3 characterDir = playerManger.transform.position - transform.position;
                float angle = Vector3.Angle(characterDir, transform.forward);
                if(angle > 90)
                {
                    //提示门只能从另一侧打开:
                    playerManger.uiManager.textPromptWindowManager.SetTextPrompt("不能从这一侧打开", 2);
                    return;
                }
            }

            if (openTheDoorCondition == null || openTheDoorCondition.CheckConditionsMet(playerManger))
            {
                //播放开门动画:
                if (isBigDoor)
                {
                    playerManger.playerAnimatorManager.PlayTargetAnimation("Kick_Open_The_Door", true);
                }
                else
                {
                    playerManger.playerAnimatorManager.PlayTargetAnimation("Open_The_Door", true);
                }
                //播放门的动画:
                animator.CrossFade("DoorOpen", 0.2f);
                //设置门已经开启:
                isAlreadyTurnedOn = true;
                //碰撞器设置为禁用:
                interactableCollider.enabled = false;
            }
            else
            {
                //提示没有满足开门条件:
            }
        }
        public void ThisOpenDoorInitialization(bool isOpen)
        {
            if (isOpen)
            {
                isAlreadyTurnedOn = true;
                interactableCollider.enabled = false;
                animator.CrossFade("DoorOpen", 0.2f);
            }
            else
            {
                isAlreadyTurnedOn = false;
                interactableCollider.enabled = true;
            }
        }
    }
}