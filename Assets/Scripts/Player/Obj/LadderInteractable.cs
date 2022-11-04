using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class LadderInteractable : Interactable
    {

        public LadderParts ladderParts;
        [Header("�ײ�������")]
        public Transform bottomTargetPoint;
        [Header("��������������:")]
        bool unfinishedClimbing = true;
        public float horizontalPositionSpeed = 1;
        public float verticalPositionSpeed = 1;
        public Transform[] TopTargetPointArray = new Transform[3];
        public override void Interact(PlayerManager playerManger)
        {
            //��������,������:
            if (playerManger.isClimbLadder)
            {
                playerManger.isClimbLadder = false;
                playerManger.playerAnimatorManager.anim.SetBool("isClimbLadder", false);
            }
            //���������ϣ�������ȥ
            else
            {

                switch (ladderParts)
                {
                    case LadderParts.Bottom:
                        playerManger.transform.position = bottomTargetPoint.position;
                        playerManger.transform.rotation = bottomTargetPoint.rotation;
                        playerManger.playerAnimatorManager.PlayTargetAnimation("Climb-Ladder-On-Bottom", true);
                        break;
                    case LadderParts.Top:
                        playerManger.playerAnimatorManager.PlayTargetAnimation("Climb-Ladder-On-Top", true);
                        StartCoroutine(ClimbLadderFromTop(playerManger));
                        break;
                }


                playerManger.isClimbLadder = true;
                playerManger.playerAnimatorManager.anim.SetBool("isClimbLadder", true);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            PlayerManager player = other.GetComponent<PlayerManager>();
            if (player != null && player.isClimbLadder)
            {
                switch (ladderParts)
                {
                    case LadderParts.Bottom:
                        if (player.inputHandler.vertical < 0)
                        {
                            player.isClimbLadder = false;
                            player.playerAnimatorManager.anim.SetBool("isClimbLadder", false);
                            player.playerAnimatorManager.PlayTargetAnimation("Climb-Ladder-Off-Bottom", true);
                        }
                        break;
                    case LadderParts.Top:
                        if (player.inputHandler.vertical > 0 && unfinishedClimbing)
                        {
                            unfinishedClimbing = false;
                            StartCoroutine(GetOffLadderFromTop(player));
                            player.playerAnimatorManager.PlayTargetAnimation("Climb-Ladder-Off-Top", true);
                        }
                        break;
                }
            }
        }

        //2.667ȫ 0-1.11����,1.11-2.667����
        IEnumerator GetOffLadderFromTop(PlayerManager player)
        {
            player.playerAnimatorManager.anim.SetBool("isClimbLadder", false);
            //1.���ƶ�����ֱλ��
            //2.Ȼ���ƶ���ˮƽλ��
            //3.���ƶ�����ȫλ��
            player.transform.position = TopTargetPointArray[2].position;
            player.transform.rotation = TopTargetPointArray[2].rotation;
            float horizontalPositionTimer = 1.11f;
            while (horizontalPositionTimer >= 0)
            {
                horizontalPositionTimer -= Time.deltaTime * horizontalPositionSpeed;
                player.transform.position = Vector3.Lerp(player.transform.position, TopTargetPointArray[1].position, 1 - horizontalPositionTimer / 1.11f);
                Debug.Log(1 - horizontalPositionTimer / 1.11f);
                yield return null;
            }

            float verticalPositionTimer = 1.557f;
            while (verticalPositionTimer >= 0)
            {
                verticalPositionTimer -= Time.deltaTime * verticalPositionSpeed;
                player.transform.position = Vector3.Lerp(player.transform.position, TopTargetPointArray[0].position, 1 - verticalPositionTimer / 1.557f);
                yield return null;
            }

            unfinishedClimbing = true;
            player.isClimbLadder = false;
        }

        //2.667ȫ 0-1.11��ˮƽλ�� 1.11-2.667��ֱλ��;
        IEnumerator ClimbLadderFromTop(PlayerManager player)
        {
            //1,��ת��-
            //2,�ƶ���ˮƽλ��
            //3,�ƶ�����Ӧ��ֱλ��
            player.transform.position = TopTargetPointArray[0].position;
            player.transform.rotation = TopTargetPointArray[0].rotation;
            float horizontalPositionTimer = 1.11f;
            while(horizontalPositionTimer >= 0)
            {
                horizontalPositionTimer -= Time.deltaTime * horizontalPositionSpeed;
                player.transform.position = Vector3.Lerp(player.transform.position, TopTargetPointArray[1].position, 1 - horizontalPositionTimer / 1.11f);
                yield return null;
            }
            float verticalPositionTimer = 1.557f;
            while (verticalPositionTimer >= 0)
            {
                verticalPositionTimer -= Time.deltaTime * verticalPositionSpeed;
                player.transform.position = Vector3.Lerp(player.transform.position, TopTargetPointArray[2].position, 1 - verticalPositionTimer / 1.557f);
                yield return null;
            }
        }
    }

    public enum LadderParts
    {
        Top,
        Bottom
    }
}