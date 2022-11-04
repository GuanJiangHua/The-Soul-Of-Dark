using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class CharacterWeaponSlotManager : MonoBehaviour
    {
        [Header("��������:")]
        public WeaponItem unarmedWeapon;

        [Header("������:")]
        public WeaponHolderSlot rightHandSlot;
        public WeaponHolderSlot leftHandSlot;
        public WeaponHolderSlot backSlot;                        //����������
        public WeaponHolderSlot backShieldSlot;              //������Ʋ�
        public WeaponHolderSlot backBowSlot;                 //���󹭼�
        public WeaponHolderSlot waistWeaponSlot;         //����������

        [Header("������ײ��:")]
        public DamageCollider rightHandDamageCollider;
        public DamageCollider leftHandDamageCollider;
        [Header("��ǰ��������Ŀ��:")]
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

        //����������λ:
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

        //����������
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
                //���ص�ǰ��������Ŀ��,������������ȡ:
            }
        }

        public virtual void LoadWeaponHolderOfSlot(WeaponItem weaponItem, bool isLeft)
        {
            if (weaponItem != null)
            {
                //��������:
                if (isLeft)
                {
                    leftHandSlot.LoadWeaponModel(weaponItem);

                    leftHandSlot.currentWeapon = weaponItem;
                    //����������ײ������:
                    LoadLeftWeaponHolderDamageCollider();
                }
                else
                {
                    //������ʩ���������漣ʩ���������ÿ��ֵĶ���������
                    if (weaponItem.weaponType == WeaponType.PyromancyCaster
                        || weaponItem.weaponType == WeaponType.SpellCaster)
                    {
                        characterAnimatorManager.anim.runtimeAnimatorController = unarmedWeapon.weaponController;
                    }
                    else
                    {
                        //��������״̬������˫������Ҫ���ⲥ��˫�ֵ����ö���;
                        characterAnimatorManager.anim.runtimeAnimatorController = weaponItem.weaponController;
                    }

                    if (characterInventoryManager.leftWeapon != null)
                    {
                        #region -------���������ж�:------
                        //����Ǳ����λ���ͣ����ڱ��������ۼ�������:
                        bool rearSlotType = characterInventoryManager.leftWeapon.weaponType == WeaponType.FaithCaster
                            || characterInventoryManager.leftWeapon.weaponType == WeaponType.Spear
                            || characterInventoryManager.leftWeapon.weaponType == WeaponType.BigSword;
                        //������:
                        bool waistSlot = characterInventoryManager.leftWeapon.weaponType == WeaponType.StraightSword  // ֱ��
                            || characterInventoryManager.leftWeapon.weaponType == WeaponType.BluntInstrument                    //����
                            || characterInventoryManager.leftWeapon.weaponType == WeaponType.Dagger                                  //ذ��
                            || characterInventoryManager.leftWeapon.weaponType == WeaponType.SpellCaster;                           //�漣��ý

                        //����Ƕ���:
                        bool isShieldSlotType = characterInventoryManager.leftWeapon.weaponType == WeaponType.Shield
                            || characterInventoryManager.leftWeapon.weaponType == WeaponType.SmallShield;
                        #endregion

                        //���ֲ��Ƕ���(˫����������:)
                        if (characterManager.isTwoHandingWeapon && rearSlotType)
                        {
                            backSlot.LoadWeaponModel(characterInventoryManager.leftWeapon);
                            leftHandSlot.UnloadWeaponAndDestroy();

                            characterAnimatorManager.PlayTargetAnimation("Left Arm Empty", false, true);  //���Ÿ��ֶ���;
                        }
                        else if (characterManager.isTwoHandingWeapon && waistSlot)
                        {
                            waistWeaponSlot.LoadWeaponModel(characterInventoryManager.leftWeapon);
                            leftHandSlot.UnloadWeaponAndDestroy();

                            characterAnimatorManager.PlayTargetAnimation("Left Arm Empty", false, true);  //���Ÿ��ֶ���;
                        }
                        //�����Ƕ���(˫����������)
                        else if (characterManager.isTwoHandingWeapon && isShieldSlotType)
                        {
                            backShieldSlot.LoadWeaponModel(characterInventoryManager.leftWeapon);
                            leftHandSlot.UnloadWeaponAndDestroy();

                            characterAnimatorManager.PlayTargetAnimation("Left Arm Empty", false, true);  //���Ÿ��ֶ���;
                        }
                        //���������ǹ���:
                        else if (characterManager.isTwoHandingWeapon && characterInventoryManager.leftWeapon.weaponType == WeaponType.Bow)
                        {
                            backBowSlot.LoadWeaponModel(characterInventoryManager.leftWeapon);
                            leftHandSlot.UnloadWeaponAndDestroy();

                            characterAnimatorManager.PlayTargetAnimation("Left Arm Empty", false, true);  //���Ÿ��ֶ���;
                        }
                        else if (characterManager.isTwoHandingWeapon == false)
                        {
                            backBowSlot.UnloadWeaponAndDestroy();
                            backSlot.UnloadWeaponAndDestroy();                               //ɾ����������ģ��;
                            backShieldSlot.UnloadWeaponAndDestroy();
                            waistWeaponSlot.UnloadWeaponAndDestroy();
                        }
                    }

                    rightHandSlot.LoadWeaponModel(weaponItem);               //��������������ײ��:
                    rightHandSlot.currentWeapon = weaponItem;
                    LoadRightWeaponHandDamageCollider();                          //��������������ײ

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
                //�ı����ö���:
                if (isLeft)
                {
                    characterInventoryManager.leftWeapon = unarmedWeapon;

                    characterAnimatorManager.anim.CrossFade("Left Arm Empty", 0.2f);             //ȡ��������������;
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
        //������������:
        protected virtual void PutAwayRightHandWeapon()
        {
            //����Ǳ����λ���ͣ����ڱ��������ۼ�������:
            bool rearSlotType = characterInventoryManager.rightWeapon.weaponType == WeaponType.FaithCaster
                || characterInventoryManager.rightWeapon.weaponType == WeaponType.Spear
                || characterInventoryManager.rightWeapon.weaponType == WeaponType.BigSword;
            //������:
            bool waistSlot = characterInventoryManager.rightWeapon.weaponType == WeaponType.StraightSword  // ֱ��
                || characterInventoryManager.rightWeapon.weaponType == WeaponType.BluntInstrument                    //����
                || characterInventoryManager.rightWeapon.weaponType == WeaponType.Dagger                                  //ذ��
                || characterInventoryManager.rightWeapon.weaponType == WeaponType.SpellCaster;                           //�漣��ý
            //�ǹ���:
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
        //�γ���������:
        protected virtual void PullOutRightHandWeapon()
        {
            //����Ǳ����λ���ͣ����ڱ��������ۼ�������:
            bool rearSlotType = characterInventoryManager.rightWeapon.weaponType == WeaponType.FaithCaster
                || characterInventoryManager.rightWeapon.weaponType == WeaponType.Spear
                || characterInventoryManager.rightWeapon.weaponType == WeaponType.BigSword;
            //������:
            bool waistSlot = characterInventoryManager.rightWeapon.weaponType == WeaponType.StraightSword  // ֱ��
                || characterInventoryManager.rightWeapon.weaponType == WeaponType.BluntInstrument                    //����
                || characterInventoryManager.rightWeapon.weaponType == WeaponType.Dagger                                  //ذ��
                || characterInventoryManager.rightWeapon.weaponType == WeaponType.SpellCaster;                           //�漣��ý
            //�ǹ���:
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

        //���������������˺���ײ������:
        protected virtual void LoadLeftWeaponHolderDamageCollider()
        {
            leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
            if (leftHandDamageCollider != null)
            {
                //�����Ѿ�id����ײ��:
                leftHandDamageCollider.teamIDNumber = characterStatsManager.teamIDNumber;

                //�����˺�����:
                leftHandDamageCollider.physicalDamage = characterInventoryManager.leftWeapon.physicalDamage;
                leftHandDamageCollider.fireDamage = characterInventoryManager.leftWeapon.fierDamage;
                leftHandDamageCollider.magicDamage = characterInventoryManager.leftWeapon.magicDamage;
                leftHandDamageCollider.lightningDamage = characterInventoryManager.leftWeapon.lightningDamage;
                leftHandDamageCollider.darkDamage = characterInventoryManager.leftWeapon.darkDamage;
                //��������Ч������:
                leftHandDamageCollider.frost = characterInventoryManager.leftWeapon.frost;
                leftHandDamageCollider.frostDamage = characterInventoryManager.leftWeapon.frostDamage;
                leftHandDamageCollider.poison = characterInventoryManager.leftWeapon.poison;
                leftHandDamageCollider.poisonDamage = characterInventoryManager.leftWeapon.poisonDamage;
                leftHandDamageCollider.hemorrhage = characterInventoryManager.leftWeapon.hemorrhage;
                leftHandDamageCollider.hemorrhageDamage = characterInventoryManager.leftWeapon.hemorrhageDamage;
                //�����˺��ӳ�����:
                leftHandDamageCollider.strengthAddition = characterInventoryManager.leftWeapon.strengthAddition;
                leftHandDamageCollider.dexterityAddition = characterInventoryManager.leftWeapon.dexterityAddition;
                leftHandDamageCollider.intelligenceAddition = characterInventoryManager.leftWeapon.intelligenceAddition;
                leftHandDamageCollider.faithAddition = characterInventoryManager.leftWeapon.faithAddition;


                leftHandDamageCollider.poiseBreak = characterInventoryManager.leftWeapon.poiseBreak;
                leftHandDamageCollider.chaeacterManager = characterManager;     //�����봦��ʱ�õ�playerManger���ԣ����û�ȡ�����ڼ���ʱ����

                //��ȡ����������β��Ч����ű�:
                characterEffectsManager.leftWeaponFX = leftHandSlot.currentWeaponModel.GetComponentInChildren<WeaponFX>();
            }
        }
        //���������������˺���ײ������:
        protected virtual void LoadRightWeaponHandDamageCollider()
        {
            rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();

            if (rightHandDamageCollider != null)
            {
                //�����Ѿ�id����ײ��:
                rightHandDamageCollider.teamIDNumber = characterStatsManager.teamIDNumber;

                //�����˺�����:
                rightHandDamageCollider.physicalDamage = characterInventoryManager.rightWeapon.physicalDamage;
                rightHandDamageCollider.fireDamage = characterInventoryManager.rightWeapon.fierDamage;
                rightHandDamageCollider.magicDamage = characterInventoryManager.rightWeapon.magicDamage;
                rightHandDamageCollider.lightningDamage = characterInventoryManager.rightWeapon.lightningDamage;
                rightHandDamageCollider.darkDamage = characterInventoryManager.rightWeapon.darkDamage;

                rightHandDamageCollider.poiseBreak = characterInventoryManager.rightWeapon.poiseBreak;
                rightHandDamageCollider.chaeacterManager = characterManager;

                //��������Ч������:
                rightHandDamageCollider.frost = characterInventoryManager.rightWeapon.frost;
                rightHandDamageCollider.frostDamage = characterInventoryManager.rightWeapon.frostDamage;
                rightHandDamageCollider.poison = characterInventoryManager.rightWeapon.poison;
                rightHandDamageCollider.poisonDamage = characterInventoryManager.rightWeapon.poisonDamage;
                rightHandDamageCollider.hemorrhage = characterInventoryManager.rightWeapon.hemorrhage;
                rightHandDamageCollider.hemorrhageDamage = characterInventoryManager.rightWeapon.hemorrhageDamage;
                //�����˺��ӳ�����:
                rightHandDamageCollider.strengthAddition = characterInventoryManager.rightWeapon.strengthAddition;
                rightHandDamageCollider.dexterityAddition = characterInventoryManager.rightWeapon.dexterityAddition;
                rightHandDamageCollider.intelligenceAddition = characterInventoryManager.rightWeapon.intelligenceAddition;
                rightHandDamageCollider.faithAddition = characterInventoryManager.rightWeapon.faithAddition;

                //��ȡ����������β��Ч����ű�:
                characterEffectsManager.rightWeaponFX = rightHandSlot.currentWeaponModel.GetComponentInChildren<WeaponFX>();
            }
        }

        //������������ײ��(֡��������);
        protected virtual void OpenDamageCollider()
        {
            if (characterManager.isUsingLeftHand)
            {
                if (leftHandDamageCollider != null)
                {
                    leftHandDamageCollider.EnableDamageCollider();         //������ײ��;
                }
            }
            if (characterManager.isUsingRightHand)
            {
                if (rightHandDamageCollider != null)
                {
                    rightHandDamageCollider.EnableDamageCollider();         //������ײ��;
                }
            }
        }

        //�ر���������ײ��(֡��������);
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

        #region �����������Խ���:
        //�����������ֽ���:(֡��������)
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
