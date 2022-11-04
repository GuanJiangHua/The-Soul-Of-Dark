using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerAnimatorManager : AnimatorManager
    {
        InputHandler inputHandler;
        PlayerLocomotionManager playerLocomotionManager;

        int vertical;
        int horizontal;
        protected override void Awake()
        {
            base.Awake();
            inputHandler = GetComponent<InputHandler>();
            playerLocomotionManager = GetComponent<PlayerLocomotionManager>();


            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");
        }

        //更新输入动画控制器的值:
        public void UpdateAnimatorValue(float verticalMovement,float horizontalMovement,bool isSprinting)
        {
            #region Vertical
            float v = 0.0f;
            if(verticalMovement > 0 && verticalMovement < 0.55f)
            {
                v = 0.5f;
            }
            else if(verticalMovement > 0.55f)
            {
                v = 1.0f;
            }
            else if(verticalMovement < 0 && verticalMovement > -0.55f)
            {
                v = -0.5f;
            }
            else if(verticalMovement < -0.55f)
            {
                v = -1.0f;
            }
            #endregion

            #region Horizontal
            float h = 0.0f;
            if (horizontalMovement > 0 && horizontalMovement < 0.55f)
            {
                h = 0.5f;
            }
            else if (horizontalMovement > 0.55f)
            {
                h = 1.0f;
            } 
            else if(horizontalMovement <0 && horizontalMovement > -0.55f)
            {
                h = -0.5f;
            }
            else if(horizontalMovement < -0.55f)
            {
                h = -1.0f;
            }
            #endregion

            if (isSprinting && inputHandler.moveAmount > 0)
            {
                v = 2;
                h = horizontalMovement;
            }
                
            anim.SetFloat(vertical, v, 0.1f , Time.deltaTime);
            anim.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
        }

        //重写父类的播放目标动画函数:
        public override void PlayTargetAnimation(string targetAnim, bool isInteracting,bool canRotate = false , bool mirrorAnim = false)
        {
            if (chaeacterManager.isDeath) return;
            base.PlayTargetAnimation(targetAnim, isInteracting, canRotate , mirrorAnim);
        }
        public override void PlayTargetAnimation(string targetAnim, bool isInteracting, float over)
        {
            if (chaeacterManager.isDeath) return;
            base.PlayTargetAnimation(targetAnim, isInteracting, over);
        }

        //移动方法:
        private void OnAnimatorMove()
        {
            if (chaeacterManager.isInteracting == false) return;

            float delta = Time.deltaTime;
            playerLocomotionManager.rigidbody.drag = 0;    //阻力为0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            playerLocomotionManager.rigidbody.velocity = 80 * velocity;

        }

        #region 碰撞器管理:
        protected override void EnableCollision()
        {
            playerLocomotionManager.characterCollider.enabled = true;
            playerLocomotionManager.characterCollidionBlockerCollider.enabled = true;
            playerLocomotionManager.backStabCollider.enabled = true;
            playerLocomotionManager.blockingCollider.enabled = true;
        }
        protected override void DisableCollision()
        {
            playerLocomotionManager.characterCollider.enabled = false;
            playerLocomotionManager.characterCollidionBlockerCollider.enabled = false;
            playerLocomotionManager.backStabCollider.enabled = false;
            playerLocomotionManager.blockingCollider.enabled = false;
        }
        #endregion

    }
}
