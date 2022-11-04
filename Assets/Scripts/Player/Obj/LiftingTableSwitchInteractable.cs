using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class LiftingTableSwitchInteractable : Interactable
    {
        public Collider interactableCollider;
        [Header("����̨����:")]
        public Elevator elevator;

        private void Awake()
        {
            interactableCollider = GetComponent<Collider>();
        }
        public override void Interact(PlayerManager playerManger)
        {
            //�������ﶯ��:
            playerManger.playerAnimatorManager.PlayTargetAnimation("Open_The_Door", true);
            //��������̨:
            elevator.StartingMechanism(this);
        }
    }
}