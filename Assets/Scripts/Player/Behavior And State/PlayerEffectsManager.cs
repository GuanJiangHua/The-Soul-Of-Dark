using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerEffectsManager : CharacterEffectsManager
    {
        [Header("回复效果与特效:")]
        public int amountToBeHealed;                //待治疗量
        public int amountToBeFocus;                  //待回复法力

        public string currentAudio;                     //当前音效;
        public Transform effectPosition;            //特效的位置;

        [Header("当前特效与临时物品:")]
        public GameObject currentParticleFX;    //当前粒子效果(特效:恢复和中毒等)
        public GameObject instantiatedFXModel;      //实例化的物品模型(水瓶等)
        [Header("当前范围效果:")]
        public GameObject currentRangeFX;

        PoisonBuildUpBar poisonBuildUpBar;                 //中毒前的缓慢上升的毒药累积量的滑动条
        PoisonAmountBar poisonAmountBar;                //中毒后的缓慢下降的毒药量的滑动条

        FrostBuildUpBar frostBuildUp;                            //霜冻值滑动条
        FrostAmountBar frostAmountBar;                      //被霜冻后滑动条

        HemorrhageBuildUpBar hemorrhageBuildUpBar;      //出血条滑动条
        HemorrhageAmountBar hemorrhageAmountBar;     //出血后处理的滑动条
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

        //治愈玩家:
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
        //帧动画调用:
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

        //毒药积累:
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

        //中毒效果:(中毒后启用毒液缓慢下降的滑动条)
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

        //霜冻效果:
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
        //被霜冻后:
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

        //血条缓慢下降:
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
        //出血后缓慢下降的滑动条:
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
