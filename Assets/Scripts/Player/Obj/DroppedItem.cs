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
            //��ض�ʧ�����:
            playerManger.playerStateManager.currentSoulCount += lostSoul;
            playerManger.playerStateManager.soulCountBar.SetSoulCountText(playerManger.playerStateManager.currentSoulCount);
            lostSoul = 0;
            //�����¼�:
            WorldEventManager.single.playerRegenerationData.isLostSoulNotFound = false;
            //������ʾ:
            EmergencyPromptInformationWindow.single.EnablePrompt("��ʰȡ��ʧ�����");

            GameObject fx = Instantiate(PickUpEffect, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
            Destroy(fx, 5);
            Destroy(gameObject);
        }
    }
}