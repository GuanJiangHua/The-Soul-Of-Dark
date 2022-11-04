using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class CharacterEffectsManager : MonoBehaviour
    {
        [Header("�˺���Ч:")]
        public GameObject bloodSplatterFX;
        public GameObject fireSplatterFX;
        public GameObject magicSplatterFX;
        public GameObject lightningSplatterFX;
        public GameObject darkSplatterFX;

        [Header("������β��Ч:")]
        public WeaponFX leftWeaponFX;
        public WeaponFX rightWeaponFX;

        [Header("�ж�Ч������ЧԤ����:")]
        public bool isPoisoned;                     //�Ƿ��ж�;
        public float poisonBuildup = 0;         //��ҩ����(�����۵�100ʱ,��һ��ж�);
        public float poisonAmount = 100;    //��ҩ��(�ж���,���Ҫ���������ҩ��);
        public float poisonTimer = 2;           //�˺���ʱ;
        public float defaultPoisonAmount = 100;   //Ĭ�϶�ҩ��;
        public int poisonDamage = 1;
        public int defaultPoisonDamage = 1;         //Ĭ�϶�ҩ�˺�;
        public Transform buildUpTransform;
        public GameObject defaultPoisonParticleFX;  //Ĭ���ж���Ч;
        public GameObject currentPoisonParticleFX;  //��ǰ�ж���������Ч;
        float timer;
        [Header("˪��Ч����Ԥ����:")]
        public bool isFrosting;                                   //�Ƿ�˪����
        public int frostDamage = 45;                        //˪���˺�
        public int defaultFrostDamage = 45;            //Ĭ��˪���˺�
        public float frostingBuildup = 0;                   //˪��ֵ����(�����۵�100ʱ,��һ��ܵ�˪���˺�);
        public float frostingAmount = 100;              //˪����(��˪����,���Ҫ���������,��˪���ڼ��޷��ڻ��ۡ�˪��ֵ��);
        public GameObject defaultFrostParticleFX;  //Ĭ��˪����Ч;
        [Header("��ѪЧ����Ԥ����:")]
        public bool isHemorrhage;
        public int hemorrhageDamage = 55;                       //��Ѫ�˺�
        public int defaultHemorrhageDamage = 55;           //Ĭ�ϳ�Ѫ�˺�
        public float hemorrhageBuildup = 0;                       //��Ѫֵ�ۻ�(���ۻ���100ʱ,��һ��ܵ���Ѫ�˺�);
        public float hemorrhageAmount = 100;                  //��Ѫ��(����Ѫ�˺���,���Ҫ���������,�ڴ��ڼ��޷��ۻ�"��Ѫֵ");
        public GameObject defaultHemorrhageParticleFX; //Ĭ�ϳ�Ѫ��Ч;
        public GameObject currentHemorrhageParticleFX; //��ǰ��Ѫ��Ч;

        protected CharacterStatsManager characterStatsManager;
        protected virtual void Awake()
        {
            characterStatsManager = GetComponent<CharacterStatsManager>();
        }

        //����������β��Ч:
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

        //�����˺�(ѪҺ�罦)��Ч:
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

        //����ҩ�Ļ��ۺ��ж����Ч��:
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
        //����ҩ����:
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
        //����ҩЧ��:
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

        //˪��Ч���½�:(δ��˪��ʱÿ֡����)
        protected virtual void HandleFrostBuildup()
        {
            if (isFrosting) return;
            //���۵�˪��ֵ�ۼ�:
            if (frostingBuildup >= 0 && frostingBuildup < 100)
            {
                frostingBuildup -= 1 * Time.deltaTime;
            }
            //�ܵ�˪���˺�:(ʵ����˪����Ч)
            else if (frostingBuildup >= 100)
            {
                isFrosting = true;
                frostingBuildup = 0;

                GameObject currentFrostFX = Instantiate(defaultFrostParticleFX, transform);
                Destroy(currentFrostFX, 5);

                characterStatsManager.TakeFrostDamage(frostDamage);
                //�������ظ��½�:
            }
        }

        //�ܵ�˪������:
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
                //�������ظ��½�:
            }
        }
        //����˪������Ч��:
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

                    //�ָ����������ָ�:
                }
            }
        }

        //��Ѫֵ�����½�:
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
                //��Ѫ�˺�:
                characterStatsManager.TakeHemorrhageDamage(hemorrhageDamage);
                //��Ѫ��Чʵ����:
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

        //��Ѫ��"��Ѫ��"�����½�:(�½���0�󣬲����ٴγ�Ѫ)
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
