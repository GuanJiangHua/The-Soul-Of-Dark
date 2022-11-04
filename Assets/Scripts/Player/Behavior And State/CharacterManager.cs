using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class CharacterManager : MonoBehaviour
    {
        [Header("锁定位置变换:")]
        public Transform lockOnTransform;                   //锁定变换;(当被玩家锁定时，锁定那个位置)
        [Header("战斗碰撞机:")]
        public CriticalDamageCollider backStabCollider;         //背刺碰撞管理
        public CriticalDamageCollider riposteCollider;            //处决碰撞管理

        [Header("交互标记:")]
        public bool isInteracting;                       //是否在交互状态;

        [Header("运动标记:")]
        public bool isRotatingWithRootMotion;
        public bool canRotate;
        public bool isSprinting;                           //冲刺中
        public bool isInAir = false;                      //是在空中;
        public bool isGrounded = true;              //是再地面;
        public bool isClimbLadder = false;

        [Header("战斗中的标记:")]
        public bool isAiming;                                       //正在瞄准
        public bool isParrying;                                     //正在弹反
        public bool isBlocking;                                    //是否举盾防御
        public bool isFiringSpell;                                 //是否在施放法术
        public bool isInvulnerab;                                 //是否刀枪不入;
        public bool isTwoHandingWeapon;                //是否双持武器;
        public bool isHoldingArrow;                           //是否手持箭矢

        public bool canBeRiposted;                             //可以处决;
        public bool canFire;                                         //可以开火
        public bool canDoCombo;                               //可以连击
        public bool isUsingRightHand;               //使用右手武器攻击
        public bool isUsingLeftHand;                 //使用左手武器攻击
        public bool isDeath = false;
        [Header("是否收取武器:")]
        public bool isPutWeapons = false;

        public CharacterStatsManager characterStatsManager;
        [Header("背刺待处置伤害值:")]
        public int pendingCriticalDamage;                  //背刺伤害;

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
