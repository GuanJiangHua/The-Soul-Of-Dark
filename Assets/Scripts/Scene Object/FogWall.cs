using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class FogWall : MonoBehaviour
    {
        PassThroughFogWall fogWallColliderManager;          //��ǽ��ڵĴ�����
        ParticleSystem fogWallFX;
        ParticleSystem circleFX;
        private void Awake()
        {
            fogWallFX = GetComponent<ParticleSystem>();
            circleFX = transform.GetChild(0).GetComponent<ParticleSystem>();

            fogWallColliderManager = GetComponentInChildren<PassThroughFogWall>();
            gameObject.SetActive(false);
        }

        //������ǽ:
        public void ActivateFogWall(bool canEnter)
        {
            gameObject.SetActive(true);
            if (canEnter)
            {
                fogWallColliderManager.interactableFogWallCollider.enabled = true;
            }
            else
            {
                fogWallColliderManager.interactableFogWallCollider.enabled = false;
            }
        }

        //������ǽ:
        public void DeactivateFogWall()
        {
            fogWallFX.Stop();
            circleFX.Stop();
            GetComponent<Collider>().enabled = false;
            fogWallColliderManager.interactableFogWallCollider.enabled = false;
            Invoke("HandleFogWall", 8);
        }

        private void HandleFogWall()
        {
            gameObject.SetActive(false);
        }
    }
}
