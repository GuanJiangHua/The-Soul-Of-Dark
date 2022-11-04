using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class CharacterEffectsManager : MonoBehaviour
    {
        [Header("伤害特效:")]
        public GameObject bloodSplatterFX;
        public GameObject fireSplatterFX;
        public GameObject magicSplatterFX;
        public GameObject lightningSplatterFX;
        public GameObject darkSplatterFX;

        [Header("武器拖尾特效:")]
        public WeaponFX leftWeaponFX;
        public WeaponFX rightWeaponFX;

        [Header("中毒效果与特效预制体:")]
        public bool isPoisoned;                     //是否中毒;
        public float poisonBuildup = 0;         //毒药积累(当积累到100时,玩家会中毒);
        public float poisonAmount = 100;    //毒药量(中毒后,玩家要处理这个毒药量);
        public float poisonTimer = 2;           //伤害计时;
        public float defaultPoisonAmount = 100;   //默认毒药量;
        public int poisonDamage = 1;
        public int defaultPoisonDamage = 1;         //默认毒药伤害;
        public Transform buildUpTransform;
        public GameObject defaultPoisonParticleFX;  //默认中毒特效;
        public GameObject currentPoisonParticleFX;  //当前中毒的粒子特效;
        float timer;
        [Header("霜冻效果与预制体:")]
        public bool isFrosting;                                   //是否被霜冻中
        public int frostDamage = 45;                        //霜冻伤害
        public int defaultFrostDamage = 45;            //默认霜冻伤害
        public float frostingBuildup = 0;                   //霜冻值积累(当积累到100时,玩家会受到霜冻伤害);
        public float frostingAmount = 100;              //霜冻量(被霜冻后,玩家要处理这个量,被霜冻期间无法在积累“霜冻值”);
        public GameObject defaultFrostParticleFX;  //默认霜冻特效;
        [Header("出血效果与预制体:")]
        public bool isHemorrhage;
        public int hemorrhageDamage = 55;                       //出血伤害
        public int defaultHemorrhageDamage = 55;           //默认出血伤害
        public float hemorrhageBuildup = 0;                       //出血值累积(当累积到100时,玩家会受到出血伤害);
        public float hemorrhageAmount = 100;                  //出血量(被出血伤害后,玩家要处理这个量,在此期间无法累积"出血值");
        public GameObject defaultHemorrhageParticleFX; //默认出血特效;
        public GameObject currentHemorrhageParticleFX; //当前出血特效;

        protected CharacterStatsManager characterStatsManager;
        protected virtual void Awake()
        {
            characterStatsManager = GetComponent<CharacterStatsManager>();
        }

        //播放武器拖尾特效:
        public virtual void PlayWeaponFX(bool isLeft)
        {
            if (isLeft)
            {
                if (leftWeaponFX != null)
                {
                    leftWeaponFX.PlayWeaponFX();
                }
            }
            else
            {
                if (rightWeaponFX != null)
                {
                    rightWeaponFX.PlayWeaponFX();
                }
            }
        }

        //播放伤害(血液喷溅)特效:
        public virtual void PlayBloodSplatterFX(Vector3 bloodSplatterLocation)
        {
            GameObject blood = Instantiate(bloodSplatterFX, bloodSplatterLocation, Quaternion.identity);
        }
        public virtual void PlayFireSplatterFX(Vector3 FireSplatterLocation)
        {
            GameObject blood = Instantiate(fireSplatterFX, FireSplatterLocation, Quaternion.identity);
            blood.transform.parent = transform;
        }
        public virtual void PlayMagicSplatterFX(Vector3 MagicSplatterLocation)
        {
            GameObject blood = Instantiate(magicSplatterFX, MagicSplatterLocation, Quaternion.identity);
            blood.transform.parent = transform;
        }
        public virtual void PlayLightningSplatterFX(Vector3 lightningSplatterLocation)
        {
            GameObject blood = Instantiate(lightningSplatterFX, lightningSplatterLocation, Quaternion.identity);
            blood.transform.parent = transform;
        }
        public virtual void PlayDarkSplatterFX(Vector3 darkSplatterLocation)
        {
            GameObject blood = Instantiate(darkSplatterFX, darkSplatterLocation, Quaternion.identity);
            blood.transform.parent = transform;
        }

        //处理毒药的积累和中毒后的效果:
        public virtual void HandleAllBuildupEffect()
        {
            if (characterStatsManager.isDead) return;

            HandlePoisonBuildup();
            HandlePoisonEffect();

            HandleFrostBuildup();
            HandleFrostEffect();

            HandleHemorrhageBuildup();
            HandleHemorrhageEffect();
        }
        //处理毒药积累:
        protected virtual void HandlePoisonBuildup()
        {
            if (isPoisoned) return;

            if (poisonBuildup >= 0 && poisonBuildup < 100)
            {
                poisonBuildup -= 1 * Time.deltaTime;
            }
            else if (poisonBuildup >= 100)
            {
                isPoisoned = true;
                poisonBuildup = 0;

                if (buildUpTransform != null)
                {
                    currentPoisonParticleFX = Instantiate(defaultPoisonParticleFX, buildUpTransform);
                    currentPoisonParticleFX.GetComponent<ParticleSystem>().Play();
                }
                else
                {
                    currentPoisonParticleFX = Instantiate(defaultPoisonParticleFX, characterStatsManager.transform);
                    currentPoisonParticleFX.GetComponent<ParticleSystem>().Play();
                }
            }
        }
        public virtual void HandlePoisonBuildup(int poisonAmount, int poisonDamage)
        {
            if (isPoisoned) return;

            this.poisonBuildup += poisonAmount;
            if (poisonBuildup >= 100)
            {
                isPoisoned = true;
                poisonBuildup = 0;

                this.poisonDamage = poisonDamage;

                if (buildUpTransform != null)
                {
                    currentPoisonParticleFX = Instantiate(defaultPoisonParticleFX, buildUpTransform);
                    currentPoisonParticleFX.GetComponent<ParticleSystem>().Play();
                }
                else
                {
                    currentPoisonParticleFX = Instantiate(defaultPoisonParticleFX, characterStatsManager.transform);
                    currentPoisonParticleFX.GetComponent<ParticleSystem>().Play();
                }
            }
        }
        //处理毒药效果:
        protected virtual void HandlePoisonEffect()
        {
            if (isPoisoned)
            {
                if(poisonAmount > 0)
                {
                    timer += Time.deltaTime;
                    poisonAmount -= 1 * Time.deltaTime;

                    if(timer > poisonTimer)
                    {
                        timer = 0;
                        characterStatsManager.TakePoisonDamage(poisonDamage);
                    }
                }
                else
                {
                    isPoisoned = false;
                    poisonAmount = defaultPoisonAmount;
                    poisonDamage = defaultPoisonDamage;

                    currentPoisonParticleFX.GetComponent<ParticleSystem>().Stop();
                    Destroy(currentPoisonParticleFX, 10);
                    currentPoisonParticleFX = null;
                }
            }
        }

        //霜冻效果下降:(未被霜冻时每帧调用)
        protected virtual void HandleFrostBuildup()
        {
            if (isFrosting) return;
            //积累的霜冻值累减:
            if (frostingBuildup >= 0 && frostingBuildup < 100)
            {
                frostingBuildup -= 1 * Time.deltaTime;
            }
            //受到霜冻伤害:(实例化霜冻特效)
            else if (frostingBuildup >= 100)
            {
                isFrosting = true;
                frostingBuildup = 0;

                GameObject currentFrostFX = Instantiate(defaultFrostParticleFX, transform);
                Destroy(currentFrostFX, 5);

                characterStatsManager.TakeFrostDamage(frostDamage);
                //让体力回复下降:
            }
        }

        //受到霜冻积累:
        public virtual void HandleFrostBuildup(int frostAmount , int frostDamage)
        {
            if (isFrosting) return;

            frostingBuildup += frostAmount;
            if(frostingBuildup >= 100)
            {
                isFrosting = true;
                frostingBuildup = 0;
                this.frostDamage = frostDamage;

                GameObject currentFrostFX = Instantiate(defaultFrostParticleFX, transform);
                Destroy(currentFrostFX, 5);

                characterStatsManager.TakeFrostDamage(frostDamage);
                //让体力回复下降:
            }
        }
        //处理霜冻后续效果:
        protected virtual void HandleFrostEffect()
        {
            if (isFrosting)
            {
                if(frostingAmount > 0)
                {
                    frostingAmount -= 1 * Time.deltaTime;
                }
                else
                {
                    isFrosting = false;
                    frostingAmount = 100;
                    frostDamage = defaultFrostDamage;

                    //恢复正常体力恢复:
                }
            }
        }

        //出血值缓慢下降:
        protected virtual void HandleHemorrhageBuildup()
        {
            if (isHemorrhage) return;

            if (hemorrhageBuildup >= 0 && hemorrhageBuildup < 100)
            {
                hemorrhageBuildup -= 1 * Time.deltaTime;
            }
            else if (hemorrhageBuildup >= 100)
            {
                isHemorrhage = true;
                hemorrhageBuildup = 0;

                Destroy(currentHemorrhageParticleFX);
                if (buildUpTransform != null)
                {

                    currentHemorrhageParticleFX = Instantiate(defaultHemorrhageParticleFX, buildUpTransform);
                    currentHemorrhageParticleFX.GetComponent<ParticleSystem>().Play();
                }
                else
                {
                    currentHemorrhageParticleFX = Instantiate(defaultHemorrhageParticleFX, characterStatsManager.transform);
                    currentHemorrhageParticleFX.GetComponent<ParticleSystem>().Play();
                }
            }
        }
        public virtual void HandleHemorrhageBuildup(int hemorrhage , int hemorrhageDamage)
        {
            if (isHemorrhage) return;

            hemorrhageBuildup += hemorrhage;
            if(hemorrhageBuildup >= 100)
            {
                isHemorrhage = true;
                hemorrhageBuildup = 0;
                this.hemorrhageDamage = hemorrhageDamage;
                //出血伤害:
                characterStatsManager.TakeHemorrhageDamage(hemorrhageDamage);
                //出血特效实例化:
                Destroy(currentHemorrhageParticleFX);
                if (buildUpTransform != null)
                {
                    currentHemorrhageParticleFX = Instantiate(defaultHemorrhageParticleFX, buildUpTransform);
                    currentHemorrhageParticleFX.GetComponent<ParticleSystem>().Play();
                }
                else
                {
                    currentHemorrhageParticleFX = Instantiate(defaultHemorrhageParticleFX, characterStatsManager.transform);
                    currentHemorrhageParticleFX.GetComponent<ParticleSystem>().Play();
                }
            }
        }

        //出血后"出血量"缓慢下降:(下降后到0后，才能再次出血)
        protected virtual void HandleHemorrhageEffect()
        {
            if (isHemorrhage)
            {
                if (hemorrhageAmount > 0)
                {
                    timer += Time.deltaTime;
                    hemorrhageAmount -= 1 * Time.deltaTime;
                }
                else
                {
                    isHemorrhage = false;
                    hemorrhageAmount = 100;
                    hemorrhageDamage = defaultHemorrhageDamage;

                    currentHemorrhageParticleFX.GetComponent<ParticleSystem>().Stop();
                    Destroy(currentHemorrhageParticleFX, 10);
                    currentHemorrhageParticleFX = null;
                }
            }
        }
    }
}
