using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerEffectsManager : CharacterEffectsManager
    {
        [Header("�ظ�Ч������Ч:")]
        public int amountToBeHealed;                //��������
        public int amountToBeFocus;                  //���ظ�����

        public string currentAudio;                     //��ǰ��Ч;
        public Transform effectPosition;            //��Ч��λ��;

        [Header("��ǰ��Ч����ʱ��Ʒ:")]
        public GameObject currentParticleFX;    //��ǰ����Ч��(��Ч:�ָ����ж���)
        public GameObject instantiatedFXModel;      //ʵ��������Ʒģ��(ˮƿ��)
        [Header("��ǰ��ΧЧ��:")]
        public GameObject currentRangeFX;

        PoisonBuildUpBar poisonBuildUpBar;                 //�ж�ǰ�Ļ��������Ķ�ҩ�ۻ����Ļ�����
        PoisonAmountBar poisonAmountBar;                //�ж���Ļ����½��Ķ�ҩ���Ļ�����

        FrostBuildUpBar frostBuildUp;                            //˪��ֵ������
        FrostAmountBar frostAmountBar;                      //��˪���󻬶���

        HemorrhageBuildUpBar hemorrhageBuildUpBar;      //��Ѫ��������
        HemorrhageAmountBar hemorrhageAmountBar;     //��Ѫ����Ļ�����
        PlayerWeaponSlotManger playerWeaponSlotManger;
        AudioManager audioManager;
        protected override void Awake()
        {
            base.Awake();

            audioManager = GetComponent<AudioManager>();
            playerWeaponSlotManger = GetComponent<PlayerWeaponSlotManger>();

            poisonBuildUpBar = FindObjectOfType<PoisonBuildUpBar>();
            poisonAmountBar = FindObjectOfType<PoisonAmountBar>();

            frostBuildUp = FindObjectOfType<FrostBuildUpBar>();
            frostAmountBar = FindObjectOfType<FrostAmountBar>();

            hemorrhageBuildUpBar = FindObjectOfType<HemorrhageBuildUpBar>();
            hemorrhageAmountBar = FindObjectOfType<HemorrhageAmountBar>();
        }

        //�������:
        public void HealPlayerFromEffect()
        {
            characterStatsManager.HealThisTarget(amountToBeHealed);
            characterStatsManager.FocusThisTarget(amountToBeFocus);
            GameObject healParticles = null;
            if (effectPosition == null)
            {
                healParticles = Instantiate(currentParticleFX, characterStatsManager.transform);
            }
            else
            {
                healParticles = Instantiate(currentParticleFX, effectPosition);
            }
            if (currentAudio != null)
            {
                audioManager.PlayAudioByName(currentAudio);
                currentAudio = null;
            }
            Destroy(healParticles, 5);
        }
        //֡��������:
        public void LoadAndDeleteModel()
        {
            if (instantiatedFXModel != null)
            {
                Destroy(instantiatedFXModel.gameObject);
                instantiatedFXModel = null;
            }
            playerWeaponSlotManger.LoadWeaponHolderOfSlot();
            GetComponentInParent<InputHandler>().sneakMove_Input = false;
        }

        //��ҩ����:
        protected override void HandlePoisonBuildup()
        {
            if(poisonBuildup <= 0)
            {
                poisonBuildUpBar.gameObject.SetActive(false);
            }
            else
            {
                poisonBuildUpBar.gameObject.SetActive(true);
            }

            base.HandlePoisonBuildup();

            poisonBuildUpBar.SetEffectBuildUpAmount(Mathf.RoundToInt(poisonBuildup));
        }
        public override void HandlePoisonBuildup(int poisonAmount, int poisonDamage)
        {
            if (poisonBuildup <= 0)
            {
                poisonBuildUpBar.gameObject.SetActive(false);
            }
            else
            {
                poisonBuildUpBar.gameObject.SetActive(true);
            }
            base.HandlePoisonBuildup(poisonAmount, poisonDamage);
            poisonBuildUpBar.SetEffectBuildUpAmount(Mathf.RoundToInt(poisonBuildup));
        }

        //�ж�Ч��:(�ж������ö�Һ�����½��Ļ�����)
        protected override void HandlePoisonEffect()
        {
            if(isPoisoned == false)
            {
                poisonAmountBar.gameObject.SetActive(false);
            }
            else
            {
                poisonAmountBar.SetMaxEffectAmount(defaultPoisonAmount);
                poisonAmountBar.gameObject.SetActive(true);
            }
            base.HandlePoisonEffect();

            poisonAmountBar.SetEffectAmount(Mathf.RoundToInt(poisonAmount));
        }

        //˪��Ч��:
        protected override void HandleFrostBuildup()
        {
            if(frostingBuildup <= 0)
            {
                frostBuildUp.gameObject.SetActive(false);
            }
            else
            {
                frostBuildUp.gameObject.SetActive(true);
            }
            base.HandleFrostBuildup();
            frostBuildUp.SetEffectBuildUpAmount(Mathf.RoundToInt(frostingBuildup));
        }
        public override void HandleFrostBuildup(int frostAmount, int frostDamage)
        {
            if(frostingBuildup <= 0)
            {
                frostBuildUp.gameObject.SetActive(false);
            }
            else
            {
                frostBuildUp.gameObject.SetActive(true);
            }
            base.HandleFrostBuildup(frostAmount, frostDamage);
            frostBuildUp.SetEffectBuildUpAmount(Mathf.RoundToInt(frostingBuildup));
        }
        //��˪����:
        protected override void HandleFrostEffect()
        {
            if (isFrosting == false)
            {
                frostAmountBar.gameObject.SetActive(false);
            }
            else
            {
                frostAmountBar.SetMaxEffectAmount(100);
                frostAmountBar.gameObject.SetActive(true);
            }
            base.HandleFrostEffect();

            frostAmountBar.SetEffectAmount(Mathf.RoundToInt(frostingAmount));
        }

        //Ѫ�������½�:
        protected override void HandleHemorrhageBuildup()
        {
            if(hemorrhageBuildup <= 0)
            {
                hemorrhageBuildUpBar.gameObject.SetActive(false);
            }
            else
            {
                hemorrhageBuildUpBar.gameObject.SetActive(true);
            }
            base.HandleHemorrhageBuildup();
            hemorrhageBuildUpBar.SetEffectBuildUpAmount(Mathf.RoundToInt(hemorrhageBuildup));
        }
        public override void HandleHemorrhageBuildup(int hemorrhage, int hemorrhageDamage)
        {
            if (hemorrhageBuildup <= 0)
            {
                hemorrhageBuildUpBar.gameObject.SetActive(false);
            }
            else
            {
                hemorrhageBuildUpBar.gameObject.SetActive(true);
            }
            base.HandleHemorrhageBuildup(hemorrhage, hemorrhageDamage);
            hemorrhageBuildUpBar.SetEffectBuildUpAmount(Mathf.RoundToInt(hemorrhageBuildup));
        }
        //��Ѫ�����½��Ļ�����:
        protected override void HandleHemorrhageEffect()
        {
            if(isHemorrhage == false)
            {
                hemorrhageAmountBar.gameObject.SetActive(false);
            }
            else
            {
                hemorrhageAmountBar.SetMaxEffectAmount(100);
                hemorrhageAmountBar.gameObject.SetActive(true);
            }
            base.HandleHemorrhageEffect();
            hemorrhageAmountBar.SetEffectAmount(Mathf.RoundToInt(hemorrhageAmount));
        }
    }
}
