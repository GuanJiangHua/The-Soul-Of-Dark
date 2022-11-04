using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class CharacterManager : MonoBehaviour
    {
        [Header("����λ�ñ任:")]
        public Transform lockOnTransform;                   //�����任;(�����������ʱ�������Ǹ�λ��)
        [Header("ս����ײ��:")]
        public CriticalDamageCollider backStabCollider;         //������ײ����
        public CriticalDamageCollider riposteCollider;            //������ײ����

        [Header("�������:")]
        public bool isInteracting;                       //�Ƿ��ڽ���״̬;

        [Header("�˶����:")]
        public bool isRotatingWithRootMotion;
        public bool canRotate;
        public bool isSprinting;                           //�����
        public bool isInAir = false;                      //���ڿ���;
        public bool isGrounded = true;              //���ٵ���;
        public bool isClimbLadder = false;

        [Header("ս���еı��:")]
        public bool isAiming;                                       //������׼
        public bool isParrying;                                     //���ڵ���
        public bool isBlocking;                                    //�Ƿ�ٶܷ���
        public bool isFiringSpell;                                 //�Ƿ���ʩ�ŷ���
        public bool isInvulnerab;                                 //�Ƿ�ǹ����;
        public bool isTwoHandingWeapon;                //�Ƿ�˫������;
        public bool isHoldingArrow;                           //�Ƿ��ֳּ�ʸ

        public bool canBeRiposted;                             //���Դ���;
        public bool canFire;                                         //���Կ���
        public bool canDoCombo;                               //��������
        public bool isUsingRightHand;               //ʹ��������������
        public bool isUsingLeftHand;                 //ʹ��������������
        public bool isDeath = false;
        [Header("�Ƿ���ȡ����:")]
        public bool isPutWeapons = false;

        public CharacterStatsManager characterStatsManager;
        [Header("���̴������˺�ֵ:")]
        public int pendingCriticalDamage;                  //�����˺�;

        protected virtual void Awake()
        {
            characterStatsManager = GetComponent<CharacterStatsManager>();
        }
        public virtual void UpdateWhichHandCharacterIsUsing(bool usingLeftHand)
        {
            if (usingLeftHand)
            {
                isUsingLeftHand = true;
                isUsingRightHand = false;
            }
            else
            {
                isUsingLeftHand = false;
                isUsingRightHand = true;
            }
        }
    }
}
