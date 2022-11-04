using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class LiftingTableSwitchInteractable : Interactable
    {
        public Collider interactableCollider;
        [Header("升降台引用:")]
        public Elevator elevator;

        private void Awake()
        {
            interactableCollider = GetComponent<Collider>();
        }
        public override void Interact(PlayerManager playerManger)
        {
            //播放人物动作:
            playerManger.playerAnimatorManager.PlayTargetAnimation("Open_The_Door", true);
            //启用升降台:
            elevator.StartingMechanism(this);
        }
    }
}