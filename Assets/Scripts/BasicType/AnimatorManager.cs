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
        [Header("�����¼�:")]
        [HideInInspector] public NoParameterEvent weaponCollisionStartEvent = new NoParameterEvent();   //������ײ����
        [HideInInspector] public NoParameterEvent weaponCollisionEntEvent = new NoParameterEvent();     //������ײ�ر�
        [HideInInspector] public NoParameterEvent enableComboEvent = new NoParameterEvent();              //��������
        [HideInInspector] public NoParameterEvent disableCombosEvent = new NoParameterEvent();            //��������
        [HideInInspector] public NoParameterEvent grantPoiseBonusEvent = new NoParameterEvent();          //��������
        [HideInInspector] public NoParameterEvent resetPoiseBonusEvent = new NoParameterEvent();          //�۳���������
        [Header("��������:")]
        public RigBuilder rigBuilder;
        public MultiAimConstraint headConstraint;           //ͷ��
        [Header("����Ŀ��:")]
        public float headFollowSpeed = 1;                    //ͷ�������ٶ�;
        public Transform headConstraintTrager;          //ͷ������Ŀ��;

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
            //�����¼������������
            enableComboEvent.AddListener(EnableCombo);
            disableCombosEvent.AddListener(DisableCombo);

            //������ת�¼��Ľ���:
            grantPoiseBonusEvent.AddListener(StopRotate);

            PlayerStatsManager playerState = FindObjectOfType<PlayerStatsManager>();
            SoulCountBar soulCountBar = FindObjectOfType<SoulCountBar>();
            soulCountBar.SetSoulCountText(playerState.currentSoulCount);
        }

        //ʹ�ø���ת����Ŀ�궯��:
        public virtual void PlayTargetAnimationWitRootRotation(string targetAnim, bool isInteracting)
        {
            anim.applyRootMotion = isInteracting;

            anim.SetBool("isInteracting", isInteracting);
            anim.SetBool("isRotatingWithRootMotion", true); //�Ը��˶���ת;
            anim.CrossFade(targetAnim, 0.2f);
        }
        //����ָ��(����״̬��)����:
        public virtual void PlayTargetAnimation(string targetAnim, bool isInteracting, bool canRotate = false, bool mirrorAnim = false)
        {
            //�Ƿ������ڽ���״̬
            anim.applyRootMotion = isInteracting;

            anim.SetBool("isInteracting", isInteracting);
            anim.SetBool("canRotate", canRotate);
            anim.SetBool("isMirror", mirrorAnim);           //�Ƿ��񶯻�;
            anim.CrossFade(targetAnim, 0.2f);

        }
        public virtual void PlayTargetAnimation(string targetAnim, bool isInteracting,float over)
        {
            //�Ƿ������ڽ���״̬
            anim.applyRootMotion = isInteracting;
            anim.SetBool("canRotate", false);
            anim.SetBool("isInteracting", isInteracting);
            anim.CrossFadeInFixedTime(targetAnim, over);
        }

        //�����˺������¼�:(��Ŀ����������˺�,֡��������)
        public virtual void TakeCriticalDamageAnimationEvent()
        {
            chaeacterStatsManager.TakeDamageNoAnimation(chaeacterManager.pendingCriticalDamage , 0 , 0 , 0 , 0);
            chaeacterManager.pendingCriticalDamage = 0;
        }

        //�����������:
        public virtual void AwardSoulsOnDeath()
        {
            //ɨ�賡���е�һλ��ң��������ǵ����
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

        //��������������־:
        private void EnableCombo()
        {
            anim.SetBool("canDoCombo", true);
        }
        //�رտ���������־:
        private void DisableCombo()
        {
            anim.SetBool("canDoCombo", false);
        }

        //���õ���:
        public virtual void EnableIsParrying()
        {
            chaeacterManager.isParrying = true;
        }
        //���õ���:
        public virtual void DisableIsParrying()
        {
            chaeacterManager.isParrying = false;
        }
    
        //���ÿ��Ա�����:
        protected virtual void EnableCanBeRiposted()
        {
            GetComponentInParent<CharacterManager>().canBeRiposted = true;
        }
        //���ÿ��Ա�����:
        protected virtual void DisableCanBeRiposted()
        {
            GetComponentInParent<CharacterManager>().canBeRiposted = false;
        }
    
        //������ײ��:
        protected virtual void EnableCollision()
        {

        }
        //������ײ��:
        protected virtual void DisableCollision()
        {

        }

        //������ǹ����״̬:(֡��������)
        public void EnableisInvulnerab()
        {
            anim.SetBool("isInvulnerab", true);
        }
        //�رյ�ǹ����״̬:(֡��������)
        public void DisableisInvulnerab()
        {
            anim.SetBool("isInvulnerab", false);
        }

        //������ת:
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

        #region �������¼������÷���:
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

        #region ������������ط���:
        //����Ȩ��:Enable
        public void SetHeadWeight(float weightValue)
        {
            headConstraint.weight = weightValue;
        }

        //����ͷ��Ŀ��λ��:
        public void SetHeadTargetPosition(Vector3 position)
        {
            headConstraintTrager.position = Vector3.Lerp(headConstraintTrager.position, position, Time.deltaTime * headFollowSpeed);
        }
        #endregion
    }
}