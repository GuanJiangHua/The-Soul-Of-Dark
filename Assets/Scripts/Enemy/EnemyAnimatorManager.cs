using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class EnemyAnimatorManager : AnimatorManager
    {
        EnemyManager enemyManager;
        EnemyBossManager enemyBossManager;
        EnemyEffectsManager enemyEffectsManager;
        EnemyLocomotionManager enemyLocomotionManager;

        protected override void Awake()
        {
            base.Awake();
            enemyManager = GetComponent<EnemyManager>();
            enemyBossManager = GetComponent<EnemyBossManager>();
            enemyEffectsManager = GetComponent<EnemyEffectsManager>();
            enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
        }

        //�������˶���ǰ����
        private void OnAnimatorMove()
        {
            float delta = Time.deltaTime;
            enemyManager.enemyRigidbody.drag = 0;     //��������Ϊ0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;

            enemyManager.enemyRigidbody.velocity = velocity;

            if(enemyManager.isRotatingWithRootMotion == true)
            {
                enemyManager.transform.rotation *= anim.deltaRotation;
            }
        }

        public void PlayWeaponTrailFX()
        {
            enemyEffectsManager.PlayWeaponFX(false);
        }
        public void PlayTargetAnimation(string targetAnim)
        {
            base.PlayTargetAnimation(targetAnim,true,0);
        }
        public override void PlayTargetAnimation(string targetAnim, bool isInteracting, bool canRotate = false , bool mirrorAnim = false)
        {
            base.PlayTargetAnimation(targetAnim, isInteracting, canRotate , mirrorAnim);
        }
        public override void PlayTargetAnimation(string targetAnim, bool isInteracting, float over)
        {
            base.PlayTargetAnimation(targetAnim, isInteracting, over);
        }

        //�����˺�����:
        public override void TakeCriticalDamageAnimationEvent()
        {
            base.TakeCriticalDamageAnimationEvent();
            enemyManager.canBeRiposted = false;
        }

        //�����������:
        public override void AwardSoulsOnDeath()
        {
            base.AwardSoulsOnDeath();
        }

        //����������Ч:(������)
        public void InstantiateBossParticleFX()
        {
            BossFXTransform bossFXTransform = GetComponentInChildren<BossFXTransform>();
            if(bossFXTransform == null)
            {
                GameObject phaseFX = Instantiate(enemyBossManager.particleFX, chaeacterManager.lockOnTransform);
            }
            else
            {
                GameObject phaseFX = Instantiate(enemyBossManager.particleFX, bossFXTransform.transform);
            }
        }

        #region ��ײ������:
        protected override void EnableCollision()
        {
            enemyLocomotionManager.characterCollider.enabled = true;
            enemyLocomotionManager.characterCollidionBlockerCollider.enabled = true;
            enemyLocomotionManager.backStabCollider.enabled = true;
        }
        protected override void DisableCollision()
        {
            enemyLocomotionManager.characterCollider.enabled = false;
            enemyLocomotionManager.characterCollidionBlockerCollider.enabled = false;
            enemyLocomotionManager.backStabCollider.enabled = false;
            enemyLocomotionManager.blockingCollider.enabled = false;
        }
        #endregion
    }
}
