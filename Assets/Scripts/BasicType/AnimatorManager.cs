using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;

namespace SG
{
    [System.Serializable]
    public class NoParameterEvent : UnityEvent { }
    public class AnimatorManager : MonoBehaviour
    {
        public Animator anim;
        [Header("动画事件:")]
        [HideInInspector] public NoParameterEvent weaponCollisionStartEvent = new NoParameterEvent();   //武器碰撞开启
        [HideInInspector] public NoParameterEvent weaponCollisionEntEvent = new NoParameterEvent();     //武器碰撞关闭
        [HideInInspector] public NoParameterEvent enableComboEvent = new NoParameterEvent();              //启用连击
        [HideInInspector] public NoParameterEvent disableCombosEvent = new NoParameterEvent();            //禁用连击
        [HideInInspector] public NoParameterEvent grantPoiseBonusEvent = new NoParameterEvent();          //出手韧性
        [HideInInspector] public NoParameterEvent resetPoiseBonusEvent = new NoParameterEvent();          //扣除出手韧性
        [Header("动画套索:")]
        public RigBuilder rigBuilder;
        public MultiAimConstraint headConstraint;           //头部
        [Header("套索目标:")]
        public float headFollowSpeed = 1;                    //头部跟随速度;
        public Transform headConstraintTrager;          //头部套索目标;

        protected CharacterManager chaeacterManager;
        protected CharacterStatsManager chaeacterStatsManager;
        protected CharacterWeaponSlotManager characterWeaponSlotManager;

        protected virtual void Awake()
        {
            anim = GetComponent<Animator>();
            rigBuilder = GetComponent<RigBuilder>();
            chaeacterManager = GetComponent<CharacterManager>();
            chaeacterStatsManager = GetComponent<CharacterStatsManager>();
            characterWeaponSlotManager = GetComponent<CharacterWeaponSlotManager>();

            if(rigBuilder!= null)
                rigBuilder.enabled = false;
        }
        private void Start()
        {
            //连击事件的启用与禁用
            enableComboEvent.AddListener(EnableCombo);
            disableCombosEvent.AddListener(DisableCombo);

            //可以旋转事件的禁用:
            grantPoiseBonusEvent.AddListener(StopRotate);

            PlayerStatsManager playerState = FindObjectOfType<PlayerStatsManager>();
            SoulCountBar soulCountBar = FindObjectOfType<SoulCountBar>();
            soulCountBar.SetSoulCountText(playerState.currentSoulCount);
        }

        //使用根旋转播放目标动画:
        public virtual void PlayTargetAnimationWitRootRotation(string targetAnim, bool isInteracting)
        {
            anim.applyRootMotion = isInteracting;

            anim.SetBool("isInteracting", isInteracting);
            anim.SetBool("isRotatingWithRootMotion", true); //以根运动旋转;
            anim.CrossFade(targetAnim, 0.2f);
        }
        //播放指定(动画状态名)动画:
        public virtual void PlayTargetAnimation(string targetAnim, bool isInteracting, bool canRotate = false, bool mirrorAnim = false)
        {
            //是否是正在交互状态
            anim.applyRootMotion = isInteracting;

            anim.SetBool("isInteracting", isInteracting);
            anim.SetBool("canRotate", canRotate);
            anim.SetBool("isMirror", mirrorAnim);           //是否镜像动画;
            anim.CrossFade(targetAnim, 0.2f);

        }
        public virtual void PlayTargetAnimation(string targetAnim, bool isInteracting,float over)
        {
            //是否是正在交互状态
            anim.applyRootMotion = isInteracting;
            anim.SetBool("canRotate", false);
            anim.SetBool("isInteracting", isInteracting);
            anim.CrossFadeInFixedTime(targetAnim, over);
        }

        //背刺伤害动画事件:(对目标产生背刺伤害,帧动画调用)
        public virtual void TakeCriticalDamageAnimationEvent()
        {
            chaeacterStatsManager.TakeDamageNoAnimation(chaeacterManager.pendingCriticalDamage , 0 , 0 , 0 , 0);
            chaeacterManager.pendingCriticalDamage = 0;
        }

        //死亡奖励灵魂:
        public virtual void AwardSoulsOnDeath()
        {
            //扫描场景中的一位玩家，给予他们的灵魂
            PlayerStatsManager playerState = FindObjectOfType<PlayerStatsManager>();
            SoulCountBar soulCountBar = FindObjectOfType<SoulCountBar>();

            if (playerState != null)
            {
                playerState.AddSouls(chaeacterStatsManager.soulsAwardedOnDeath);
                if (soulCountBar != null)
                {
                    soulCountBar.SetSoulCountText(playerState.currentSoulCount);
                }
            }
        }

        //开启可以连击标志:
        private void EnableCombo()
        {
            anim.SetBool("canDoCombo", true);
        }
        //关闭可以连击标志:
        private void DisableCombo()
        {
            anim.SetBool("canDoCombo", false);
        }

        //启用弹反:
        public virtual void EnableIsParrying()
        {
            chaeacterManager.isParrying = true;
        }
        //禁用弹反:
        public virtual void DisableIsParrying()
        {
            chaeacterManager.isParrying = false;
        }
    
        //启用可以被处决:
        protected virtual void EnableCanBeRiposted()
        {
            GetComponentInParent<CharacterManager>().canBeRiposted = true;
        }
        //禁用可以被处决:
        protected virtual void DisableCanBeRiposted()
        {
            GetComponentInParent<CharacterManager>().canBeRiposted = false;
        }
    
        //启用碰撞体:
        protected virtual void EnableCollision()
        {

        }
        //禁用碰撞体:
        protected virtual void DisableCollision()
        {

        }

        //开启刀枪不入状态:(帧动画调用)
        public void EnableisInvulnerab()
        {
            anim.SetBool("isInvulnerab", true);
        }
        //关闭刀枪不入状态:(帧动画调用)
        public void DisableisInvulnerab()
        {
            anim.SetBool("isInvulnerab", false);
        }

        //可以旋转:
        public void CanRotate()
        {
            anim.SetBool("canRotate", true);
        }
        private void StopRotate()
        {
            anim.SetBool("canRotate", false);
        }

        private void CanFire()
        {
            anim.SetBool("canFire", true);
        }

        #region 动画器事件的启用方法:
        public void EnableWeaponCollisionEvent()
        {
            weaponCollisionStartEvent.Invoke();
        }
        public void DisableWeaponCollisionEvent()
        {
            weaponCollisionEntEvent.Invoke();
        }

        public void GrantPoiseBonusEvent()
        {
            grantPoiseBonusEvent.Invoke();
        }
        public void ResetPoiseBonusEvent()
        {
            resetPoiseBonusEvent.Invoke();
        }

        public void EnableComboEvent()
        {
            enableComboEvent.Invoke();
        }
        public void DisableComboEvent()
        {
            disableCombosEvent.Invoke();
        }
        #endregion

        #region 动画套索的相关方法:
        //设置权重:Enable
        public void SetHeadWeight(float weightValue)
        {
            headConstraint.weight = weightValue;
        }

        //设置头部目标位置:
        public void SetHeadTargetPosition(Vector3 position)
        {
            headConstraintTrager.position = Vector3.Lerp(headConstraintTrager.position, position, Time.deltaTime * headFollowSpeed);
        }
        #endregion
    }
}