using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class LadderInteractable : Interactable
    {

        public LadderParts ladderParts;
        [Header("底部出发点")]
        public Transform bottomTargetPoint;
        [Header("顶部出发点数组:")]
        bool unfinishedClimbing = true;
        public float horizontalPositionSpeed = 1;
        public float verticalPositionSpeed = 1;
        public Transform[] TopTargetPointArray = new Transform[3];
        public override void Interact(PlayerManager playerManger)
        {
            //在梯子上,就下来:
            if (playerManger.isClimbLadder)
            {
                playerManger.isClimbLadder = false;
                playerManger.playerAnimatorManager.anim.SetBool("isClimbLadder", false);
            }
            //不在梯子上，就爬上去
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

        //2.667全 0-1.11到顶,1.11-2.667到变
        IEnumerator GetOffLadderFromTop(PlayerManager player)
        {
            player.playerAnimatorManager.anim.SetBool("isClimbLadder", false);
            //1.先移动到垂直位置
            //2.然后移动到水平位置
            //3.在移动到安全位置
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

        //2.667全 0-1.11到水平位置 1.11-2.667垂直位置;
        IEnumerator ClimbLadderFromTop(PlayerManager player)
        {
            //1,先转身-
            //2,移动到水平位置
            //3,移动到对应垂直位置
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