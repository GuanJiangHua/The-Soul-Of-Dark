using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class DroppedItem : Interactable
    {
        public int lostSoul;
        public GameObject PickUpEffect;
        public override void Interact(PlayerManager playerManger)
        {
            playerManger.playerLocomotion.rigidbody.velocity = Vector3.zero;
            playerManger.playerAnimatorManager.PlayTargetAnimation("Pick Up Item", true);
            //捡回丢失的灵魂:
            playerManger.playerStateManager.currentSoulCount += lostSoul;
            playerManger.playerStateManager.soulCountBar.SetSoulCountText(playerManger.playerStateManager.currentSoulCount);
            lostSoul = 0;
            //世界事件:
            WorldEventManager.single.playerRegenerationData.isLostSoulNotFound = false;
            //开启提示:
            EmergencyPromptInformationWindow.single.EnablePrompt("已拾取遗失的灵魂");

            GameObject fx = Instantiate(PickUpEffect, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
            Destroy(fx, 5);
            Destroy(gameObject);
        }
    }
}