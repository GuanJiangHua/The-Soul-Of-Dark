using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PassThroughFogWall : Interactable
    {
        public Collider interactableFogWallCollider;

        FogWall fogWall;
        WorldEventManager worldEventManager;
        private void Awake()
        {
            interactableFogWallCollider = GetComponent<Collider>();

            fogWall = GetComponentInParent<FogWall>();
            worldEventManager = FindObjectOfType<WorldEventManager>();
        }

        public override void Interact(PlayerManager playerManger)
        {
            base.Interact(playerManger);
            playerManger.PassThroughFogWallInteraction(transform);
            interactableFogWallCollider.enabled = false;
        }
    }
}
