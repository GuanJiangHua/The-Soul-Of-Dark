using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class CharacterWeaponSlotManager : MonoBehaviour
    {
        [Header("空手武器:")]
        public WeaponItem unarmedWeapon;

        [Header("武器槽:")]
        public WeaponHolderSlot rightHandSlot;
        public WeaponHolderSlot leftHandSlot;
        public WeaponHolderSlot backSlot;                        //背后武器槽
        public WeaponHolderSlot backShieldSlot;              //背后盾牌槽
        public WeaponHolderSlot backBowSlot;                 //背后弓箭
        public WeaponHolderSlot waistWeaponSlot;         //腰部武器槽

        [Header("武器碰撞器:")]
        public DamageCollider rightHandDamageCollider;
        public DamageCollider leftHandDamageCollider;
        [Header("当前左手套索目标:")]
        public Transform currentLeftHandConstraintTrager;

        protected CharacterManager characterManager;
        protected CharacterStatsManager characterStatsManager;
        protected AnimatorManager characterAnimatorManager;
        protected CharacterInventoryManager characterInventoryManager;
        protected CharacterEffectsManager characterEffectsManager;

        protected virtual void Awake()
        {
            characterManager = GetComponent<CharacterManager>();
            characterAnimatorManager = GetComponent<AnimatorManager>();
            characterInventoryManager = GetComponent<CharacterInventoryManager>();
            characterStatsManager = GetComponent<CharacterStatsManager>();
            characterEffectsManager = GetComponent<CharacterEffectsManager>();

            LoadWeaponHolderSlots();
        }

        protected virtual void Start()
        {

        }

        //加载武器槽位:
        protected virtual void LoadWeaponHolderSlots()
        {
            WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
            foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
            {
                if (weaponSlot.isLeftHandSlot)
                {
                    leftHandSlot = weaponSlot;
                }
                else if (weaponSlot.isRightHandSlot)
                {
                    rightHandSlot = weaponSlot;
                }
                else if (weaponSlot.backSlot)
                {
                    backSlot = weaponSlot;
                }
                else if (weaponSlot.backShield)
                {
                    backShieldSlot = weaponSlot;
                }
                else if (weaponSlot.backBow)
                {
                    backBowSlot = weaponSlot;
                }
                else if (weaponSlot.waistSlot)
                {
                    waistWeaponSlot = weaponSlot;
                }
            }
        }

        //加载武器：
        public virtual void LoadWeaponHolderOfSlot()
        {
            if (characterManager.isTwoHandingWeapon == false)
            {
                LoadWeaponHolderOfSlot(characterInventoryManager.leftWeapon, true);
                LoadWeaponHolderOfSlot(characterInventoryManager.rightWeapon, false);
            }
            else
            {
                LoadWeaponHolderOfSlot(characterInventoryManager.rightWeapon, false);
                //加载当前左手套索目标,从右手武器获取:
            }
        }

        public virtual void LoadWeaponHolderOfSlot(WeaponItem weaponItem, bool isLeft)
        {
            if (weaponItem != null)
            {
                //加载武器:
                if (isLeft)
                {
                    leftHandSlot.LoadWeaponModel(weaponItem);

                    leftHandSlot.currentWeapon = weaponItem;
                    //加载武器碰撞控制器:
                    LoadLeftWeaponHolderDamageCollider();
                }
                else
                {
                    //是咒术施法武器或奇迹施法武器就用空手的动画控制器
                    if (weaponItem.weaponType == WeaponType.PyromancyCaster
                        || weaponItem.weaponType == WeaponType.SpellCaster)
                    {
                        characterAnimatorManager.anim.runtimeAnimatorController = unarmedWeapon.weaponController;
                    }
                    else
                    {
                        //武器闲置状态，但是双持输入要另外播放双持的闲置动画;
                        characterAnimatorManager.anim.runtimeAnimatorController = weaponItem.weaponController;
                    }

                    if (characterInventoryManager.leftWeapon != null)
                    {
                        #region -------武器类型判断:------
                        //如果是背后槽位类型，就在背后武器槽加载武器:
                        bool rearSlotType = characterInventoryManager.leftWeapon.weaponType == WeaponType.FaithCaster
                            || characterInventoryManager.leftWeapon.weaponType == WeaponType.Spear
                            || characterInventoryManager.leftWeapon.weaponType == WeaponType.BigSword;
                        //腰部槽:
                        bool waistSlot = characterInventoryManager.leftWeapon.weaponType == WeaponType.StraightSword  // 直剑
                            || characterInventoryManager.leftWeapon.weaponType == WeaponType.BluntInstrument                    //钝器
                            || characterInventoryManager.leftWeapon.weaponType == WeaponType.Dagger                                  //匕首
                            || characterInventoryManager.leftWeapon.weaponType == WeaponType.SpellCaster;                           //奇迹触媒

                        //如果是盾牌:
                        bool isShieldSlotType = characterInventoryManager.leftWeapon.weaponType == WeaponType.Shield
                            || characterInventoryManager.leftWeapon.weaponType == WeaponType.SmallShield;
                        #endregion

                        //左手不是盾牌(双持主手武器:)
                        if (characterManager.isTwoHandingWeapon && rearSlotType)
                        {
                            backSlot.LoadWeaponModel(characterInventoryManager.leftWeapon);
                            leftHandSlot.UnloadWeaponAndDestroy();

                            characterAnimatorManager.PlayTargetAnimation("Left Arm Empty", false, true);  //播放副手动画;
                        }
                        else if (characterManager.isTwoHandingWeapon && waistSlot)
                        {
                            waistWeaponSlot.LoadWeaponModel(characterInventoryManager.leftWeapon);
                            leftHandSlot.UnloadWeaponAndDestroy();

                            characterAnimatorManager.PlayTargetAnimation("Left Arm Empty", false, true);  //播放副手动画;
                        }
                        //左手是盾牌(双持主手武器)
                        else if (characterManager.isTwoHandingWeapon && isShieldSlotType)
                        {
                            backShieldSlot.LoadWeaponModel(characterInventoryManager.leftWeapon);
                            leftHandSlot.UnloadWeaponAndDestroy();

                            characterAnimatorManager.PlayTargetAnimation("Left Arm Empty", false, true);  //播放副手动画;
                        }
                        //左手武器是弓箭:
                        else if (characterManager.isTwoHandingWeapon && characterInventoryManager.leftWeapon.weaponType == WeaponType.Bow)
                        {
                            backBowSlot.LoadWeaponModel(characterInventoryManager.leftWeapon);
                            leftHandSlot.UnloadWeaponAndDestroy();

                            characterAnimatorManager.PlayTargetAnimation("Left Arm Empty", false, true);  //播放副手动画;
                        }
                        else if (characterManager.isTwoHandingWeapon == false)
                        {
                            backBowSlot.UnloadWeaponAndDestroy();
                            backSlot.UnloadWeaponAndDestroy();                               //删除背后武器模型;
                            backShieldSlot.UnloadWeaponAndDestroy();
                            waistWeaponSlot.UnloadWeaponAndDestroy();
                        }
                    }

                    rightHandSlot.LoadWeaponModel(weaponItem);               //加载右手武器碰撞器:
                    rightHandSlot.currentWeapon = weaponItem;
                    LoadRightWeaponHandDamageCollider();                          //加载右手武器碰撞

                    if (weaponItem.isUnarmed)
                    {
                        if (isLeft)
                        {
                            characterAnimatorManager.anim.CrossFade("Left Arm Empty", 0.2f);
                        }
                    }
                }
            }
            else
            {
                //改变闲置动画:
                if (isLeft)
                {
                    characterInventoryManager.leftWeapon = unarmedWeapon;

                    characterAnimatorManager.anim.CrossFade("Left Arm Empty", 0.2f);             //取消副手武器动画;
                    leftHandSlot.currentWeapon = unarmedWeapon;
                    leftHandSlot.LoadWeaponModel(unarmedWeapon);
                    LoadLeftWeaponHolderDamageCollider();
                }
                else
                {
                    characterInventoryManager.rightWeapon = unarmedWeapon;

                    rightHandSlot.currentWeapon = unarmedWeapon;
                    rightHandSlot.LoadWeaponModel(unarmedWeapon);
                    LoadRightWeaponHandDamageCollider();

                    characterAnimatorManager.anim.runtimeAnimatorController = unarmedWeapon.weaponController;
                }
            }

        }
        //收起右手武器:
        protected virtual void PutAwayRightHandWeapon()
        {
            //如果是背后槽位类型，就在背后武器槽加载武器:
            bool rearSlotType = characterInventoryManager.rightWeapon.weaponType == WeaponType.FaithCaster
                || characterInventoryManager.rightWeapon.weaponType == WeaponType.Spear
                || characterInventoryManager.rightWeapon.weaponType == WeaponType.BigSword;
            //腰部槽:
            bool waistSlot = characterInventoryManager.rightWeapon.weaponType == WeaponType.StraightSword  // 直剑
                || characterInventoryManager.rightWeapon.weaponType == WeaponType.BluntInstrument                    //钝器
                || characterInventoryManager.rightWeapon.weaponType == WeaponType.Dagger                                  //匕首
                || characterInventoryManager.rightWeapon.weaponType == WeaponType.SpellCaster;                           //奇迹触媒
            //是弓箭:
            bool isBow = characterInventoryManager.rightWeapon.weaponType == WeaponType.Bow;

            if (rearSlotType)
            {
                backSlot.LoadWeaponModel(characterInventoryManager.rightWeapon);
            }
            else if (waistSlot)
            {
                waistWeaponSlot.LoadWeaponModel(characterInventoryManager.rightWeapon);
            }
            else if (isBow)
            {
                backBowSlot.LoadWeaponModel(characterInventoryManager.rightWeapon);
            }

            rightHandSlot.UnloadWeapon();
        } 
        //拔出右手武器:
        protected virtual void PullOutRightHandWeapon()
        {
            //如果是背后槽位类型，就在背后武器槽加载武器:
            bool rearSlotType = characterInventoryManager.rightWeapon.weaponType == WeaponType.FaithCaster
                || characterInventoryManager.rightWeapon.weaponType == WeaponType.Spear
                || characterInventoryManager.rightWeapon.weaponType == WeaponType.BigSword;
            //腰部槽:
            bool waistSlot = characterInventoryManager.rightWeapon.weaponType == WeaponType.StraightSword  // 直剑
                || characterInventoryManager.rightWeapon.weaponType == WeaponType.BluntInstrument                    //钝器
                || characterInventoryManager.rightWeapon.weaponType == WeaponType.Dagger                                  //匕首
                || characterInventoryManager.rightWeapon.weaponType == WeaponType.SpellCaster;                           //奇迹触媒
            //是弓箭:
            bool isBow = characterInventoryManager.rightWeapon.weaponType == WeaponType.Bow;

            if (rearSlotType)
            {
                backSlot.UnloadWeapon();
            }
            else if (waistSlot)
            {
                waistWeaponSlot.UnloadWeapon();
            }
            else if (isBow)
            {
                backBowSlot.UnloadWeapon();
            }
            LoadWeaponHolderOfSlot(characterInventoryManager.rightWeapon , false);
        }

        //加载左手武器的伤害碰撞器控制:
        protected virtual void LoadLeftWeaponHolderDamageCollider()
        {
            leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
            if (leftHandDamageCollider != null)
            {
                //给予友军id到碰撞器:
                leftHandDamageCollider.teamIDNumber = characterStatsManager.teamIDNumber;

                //给予伤害属性:
                leftHandDamageCollider.physicalDamage = characterInventoryManager.leftWeapon.physicalDamage;
                leftHandDamageCollider.fireDamage = characterInventoryManager.leftWeapon.fierDamage;
                leftHandDamageCollider.magicDamage = characterInventoryManager.leftWeapon.magicDamage;
                leftHandDamageCollider.lightningDamage = characterInventoryManager.leftWeapon.lightningDamage;
                leftHandDamageCollider.darkDamage = characterInventoryManager.leftWeapon.darkDamage;
                //给予特殊效果属性:
                leftHandDamageCollider.frost = characterInventoryManager.leftWeapon.frost;
                leftHandDamageCollider.frostDamage = characterInventoryManager.leftWeapon.frostDamage;
                leftHandDamageCollider.poison = characterInventoryManager.leftWeapon.poison;
                leftHandDamageCollider.poisonDamage = characterInventoryManager.leftWeapon.poisonDamage;
                leftHandDamageCollider.hemorrhage = characterInventoryManager.leftWeapon.hemorrhage;
                leftHandDamageCollider.hemorrhageDamage = characterInventoryManager.leftWeapon.hemorrhageDamage;
                //给予伤害加成属性:
                leftHandDamageCollider.strengthAddition = characterInventoryManager.leftWeapon.strengthAddition;
                leftHandDamageCollider.dexterityAddition = characterInventoryManager.leftWeapon.dexterityAddition;
                leftHandDamageCollider.intelligenceAddition = characterInventoryManager.leftWeapon.intelligenceAddition;
                leftHandDamageCollider.faithAddition = characterInventoryManager.leftWeapon.faithAddition;


                leftHandDamageCollider.poiseBreak = characterInventoryManager.leftWeapon.poiseBreak;
                leftHandDamageCollider.chaeacterManager = characterManager;     //背刺与处决时用到playerManger属性，不好获取，故在加载时给定

                //获取到武器的拖尾特效管理脚本:
                characterEffectsManager.leftWeaponFX = leftHandSlot.currentWeaponModel.GetComponentInChildren<WeaponFX>();
            }
        }
        //加载右手武器的伤害碰撞器控制:
        protected virtual void LoadRightWeaponHandDamageCollider()
        {
            rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();

            if (rightHandDamageCollider != null)
            {
                //给予友军id到碰撞器:
                rightHandDamageCollider.teamIDNumber = characterStatsManager.teamIDNumber;

                //给予伤害属性:
                rightHandDamageCollider.physicalDamage = characterInventoryManager.rightWeapon.physicalDamage;
                rightHandDamageCollider.fireDamage = characterInventoryManager.rightWeapon.fierDamage;
                rightHandDamageCollider.magicDamage = characterInventoryManager.rightWeapon.magicDamage;
                rightHandDamageCollider.lightningDamage = characterInventoryManager.rightWeapon.lightningDamage;
                rightHandDamageCollider.darkDamage = characterInventoryManager.rightWeapon.darkDamage;

                rightHandDamageCollider.poiseBreak = characterInventoryManager.rightWeapon.poiseBreak;
                rightHandDamageCollider.chaeacterManager = characterManager;

                //给予特殊效果属性:
                rightHandDamageCollider.frost = characterInventoryManager.rightWeapon.frost;
                rightHandDamageCollider.frostDamage = characterInventoryManager.rightWeapon.frostDamage;
                rightHandDamageCollider.poison = characterInventoryManager.rightWeapon.poison;
                rightHandDamageCollider.poisonDamage = characterInventoryManager.rightWeapon.poisonDamage;
                rightHandDamageCollider.hemorrhage = characterInventoryManager.rightWeapon.hemorrhage;
                rightHandDamageCollider.hemorrhageDamage = characterInventoryManager.rightWeapon.hemorrhageDamage;
                //给予伤害加成属性:
                rightHandDamageCollider.strengthAddition = characterInventoryManager.rightWeapon.strengthAddition;
                rightHandDamageCollider.dexterityAddition = characterInventoryManager.rightWeapon.dexterityAddition;
                rightHandDamageCollider.intelligenceAddition = characterInventoryManager.rightWeapon.intelligenceAddition;
                rightHandDamageCollider.faithAddition = characterInventoryManager.rightWeapon.faithAddition;

                //获取到武器的拖尾特效管理脚本:
                characterEffectsManager.rightWeaponFX = rightHandSlot.currentWeaponModel.GetComponentInChildren<WeaponFX>();
            }
        }

        //启用武器的碰撞器(帧动画调用);
        protected virtual void OpenDamageCollider()
        {
            if (characterManager.isUsingLeftHand)
            {
                if (leftHandDamageCollider != null)
                {
                    leftHandDamageCollider.EnableDamageCollider();         //启用碰撞器;
                }
            }
            if (characterManager.isUsingRightHand)
            {
                if (rightHandDamageCollider != null)
                {
                    rightHandDamageCollider.EnableDamageCollider();         //启用碰撞器;
                }
            }
        }

        //关闭武器的碰撞器(帧动画调用);
        public virtual void CloseDamageCollider()
        {
            if (rightHandDamageCollider != null)
            {
                rightHandDamageCollider.DisaleDamageCollider();
            }

            if (leftHandDamageCollider != null)
            {
                leftHandDamageCollider.DisaleDamageCollider();
            }
        }

        #region 武器出手韧性奖励:
        //给予武器出手奖励:(帧动画调用)
        protected virtual void GrantWeaponAttackingPoiseBonus()
        {
            WeaponItem currentWeapon = characterInventoryManager.currentItemBeingUsed as WeaponItem;
            characterStatsManager.totalPoiseDefence += currentWeapon.offensivePoiseBonus;
        }

        protected virtual void ResetWeaponAttackingPoiseBonus()
        {
            WeaponItem currentWeapon = characterInventoryManager.currentItemBeingUsed as WeaponItem;
            characterStatsManager.totalPoiseDefence -= currentWeapon.offensivePoiseBonus;
            if (characterStatsManager.totalPoiseDefence < 0)
                characterStatsManager.totalPoiseDefence = 0;
        }
        #endregion
    }
}
