using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class DoorInteractable : Interactable
    {
        [Header("�Ǵ���:")]
        public bool isBigDoor;
        [Header("ֻ�ܴ�ǰ�濪��:")]
        public bool isOpenDoorFromFront;
        [Header("�Ƿ��Ѿ�����:")]
        public bool isAlreadyTurnedOn;
        [Header("��������:")]
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
                    //��ʾ��ֻ�ܴ���һ���:
                    playerManger.uiManager.textPromptWindowManager.SetTextPrompt("���ܴ���һ���", 2);
                    return;
                }
            }

            if (openTheDoorCondition == null || openTheDoorCondition.CheckConditionsMet(playerManger))
            {
                //���ſ��Ŷ���:
                if (isBigDoor)
                {
                    playerManger.playerAnimatorManager.PlayTargetAnimation("Kick_Open_The_Door", true);
                }
                else
                {
                    playerManger.playerAnimatorManager.PlayTargetAnimation("Open_The_Door", true);
                }
                //�����ŵĶ���:
                animator.CrossFade("DoorOpen", 0.2f);
                //�������Ѿ�����:
                isAlreadyTurnedOn = true;
                //��ײ������Ϊ����:
                interactableCollider.enabled = false;
            }
            else
            {
                //��ʾû�����㿪������:
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