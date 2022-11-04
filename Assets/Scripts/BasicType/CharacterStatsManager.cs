using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class CharacterStatsManager : MonoBehaviour
    {
        public int maxHealth;
        public int currentHealth;

        public float maxStamina;              //�������ֵ
        public float currentStamina;         //��ǰ����ֵ

        public float maxFocusPoints;       //�����ֵ
        public float currentFocusPoints;  //��ǰ����ֵ

        public int currentSoulCount = 0;         //�����
        [Header("�������:")]
        public string characterName = "DefaultName";

        [Header("����ȼ�:")]

        public int characterLeve = 1;

        [Header("�Ŷ�ID:")]
        public int teamIDNumber = 0;                 //���ֵ���,��ֹ����;

        [Header("���Եȼ�:")]
        public int healthLevel = 10;           //��������
        public int staminaLevel = 10;        //��������
        public int focusLevel = 10;            //��������
        public int poiseLevel = 10;            //���Լ���
        public int strengthLevel = 10;       //��������
        public int dexterityLevel = 10;      //���ݼ���
        public int intelligenceLevel = 10;  //��������
        public int faithLevel = 10;             //��������
        public float soulsRewardLevel = 1;  //��꽱������

        public bool isDead;
        [Header("����:")]
        public float totalPoiseDefence;                               //�˺������ڼ��������ֵ
        public float offensivePoiseBonus;                           //��������
        public float armorPoiseBonus;                                //�������Խ���(��פ����)
        public float totalPoiseResetTime = 15;                   //����������ʱ��
        public float poiseResetTimer = 0;                           //�������ü�ʱ��

        #region ����:
        [Header("��פ���Լӳ�:")]
        [HideInInspector] public float physicalDamageAbsorption;                //���Եȼ������Ķ��⿹��;
        [HideInInspector] public float fireDamageAbsorption;
        [HideInInspector] public float magicDamageAbsorption;
        [HideInInspector] public float lightningDamageAbsorption;
        [HideInInspector] public float darkDamageAbsorption;
        [Header("��ʱ���Լӳ�:")]
        public float temporaryPhysicalDamageAbsorption;                //��ָ,���������Ķ��⿹��;
        public float temporaryFireDamageAbsorption;
        public float temporaryMagicDamageAbsorption;
        public float temporaryLightningDamageAbsorption;
        public float temporaryDarkDamageAbsorption;

        [Header("���������˺�����:")]
        public float physicalDamageAbsorptionHead;       //ͷ������(�����˺�����)
        public float physicalDamageAbsorptionBody;       //���ɻ���(�����˺�����)
        public float physicalDamageAbsorptionLegs;        //�Ȳ�����(�����˺�����)
        public float physicalDamageAbsorptionHands;     //�ֱۻ���(�����˺�����)

        [Header("���׻����˺�����:")]
        public float fireDamageAbsorptionHead;       //ͷ������(�����˺�����)
        public float fireDamageAbsorptionBody;       //���ɻ���(�����˺�����)
        public float fireDamageAbsorptionLegs;        //�Ȳ�����(�����˺�����)
        public float fireDamageAbsorptionHands;     //�ֱۻ���(�����˺�����)

        [Header("����ħ���˺�����:")]
        public float magicDamageAbsorptionHead;       //ͷ������(ħ���˺�����)
        public float magicDamageAbsorptionBody;       //���ɻ���(ħ���˺�����)
        public float magicDamageAbsorptionLegs;        //�Ȳ�����(ħ���˺�����)
        public float magicDamageAbsorptionHands;     //�ֱۻ���(ħ���˺�����)

        [Header("�����׵��˺�����:")]
        public float lightningDamageAbsorptionHead;       //ͷ������(�׵��˺�����)
        public float lightningDamageAbsorptionBody;       //���ɻ���(�׵��˺�����)
        public float lightningDamageAbsorptionLegs;        //�Ȳ�����(�׵��˺�����)
        public float lightningDamageAbsorptionHands;     //�ֱۻ���(�׵��˺�����)dark

        [Header("���װ������˺�����:")]
        public float darkDamageAbsorptionHead;       //ͷ������(�׵��˺�����)
        public float darkDamageAbsorptionBody;       //���ɻ���(�׵��˺�����)
        public float darkDamageAbsorptionLegs;        //�Ȳ�����(�׵��˺�����)
        public float darkDamageAbsorptionHands;     //�ֱۻ���(�׵��˺�����)

        [Header("��פ�쳣���Եֿ���:")]
        [HideInInspector] public float poisonDefenseAbsorption;
        [HideInInspector] public float hemorrhageDefenseAbsorption;
        [HideInInspector] public float frostDamageAbsorption;
        [HideInInspector] public float curseDefenseAbsorption;
        [Header("��ʱ�쳣���Եֿ���:")]
        public float temporaryPoisonDefenseAbsorption;
        public float temporaryHemorrhageDefenseAbsorption;
        public float temporaryFrostDamageAbsorption;
        public float temporaryCurseDefenseAbsorption;
        [Header("�쳣���Եֿ���:")]
        public float poisonDefenseAbsorptionHead;                        //�����Եֿ���ͷ��;
        public float poisonDefenseAbsorptionBody;                        //�����Եֿ����ؼ�;
        public float poisonDefenseAbsorptionLegs;                        //�����Եֿ����ȼ�;
        public float poisonDefenseAbsorptionHands;                     //�����Եֿ�������;

        public float hemorrhageDefenseAbsorptionHead;               //��Ѫ���Եֿ���ͷ��;
        public float hemorrhageDefenseAbsorptionBody;               //��Ѫ���Եֿ����ؼ�;
        public float hemorrhageDefenseAbsorptionLegs;                //��Ѫ���Եֿ����ȼ�;
        public float hemorrhageDefenseAbsorptionHands;             //��Ѫ���Եֿ�������;

        public float frostDamageAbsorptionHead;                          //˪���ֿ���ͷ��;
        public float frostDamageAbsorptionBody;                          //˪���ֿ����ؼ�;
        public float frostDamageAbsorptionLegs;                           //˪���ֿ����ȼ�;
        public float frostDamageAbsorptionHands;                        //˪���ֿ�������;

        public float curseDefenseAbsorptionHead;                          //�������Եֿ���;
        public float curseDefenseAbsorptionBody;                          //�������Եֿ���;
        public float curseDefenseAbsorptionLegs;                          //�������Եֿ���;
        public float curseDefenseAbsorptionHands;                       //�������Եֿ���;
        #endregion

        [Header("������ȡ�����:")]
        public int soulsAwardedOnDeath = 50;    //�������õ����

        public CharacterManager chaeacterManager;
        [Header("����������ײ��:")]
        public DamageDetectionCollition[] damageDetectionCollitions;
        protected CharacterWeaponSlotManager characterWeaponSlotManager;
        protected virtual void Awake()
        {
            chaeacterManager = GetComponent<CharacterManager>();
            characterWeaponSlotManager = GetComponent<CharacterWeaponSlotManager>();

            damageDetectionCollitions = GetComponentsInChildren<DamageDetectionCollition>();
            foreach(var damageDetection in damageDetectionCollitions)
            {
                damageDetection.characterStatsManager = this;
            }
        }
        protected virtual void Start()
        {
            SetResistance();
        }
        protected virtual void Update()
        {
            HandlePoiseResetTimer();
        }
        //����ֵ���:
        public virtual void TakeDamage(int physicalDamage, int fireDamage , int magicDamage ,int lightningDamage , int darkDamage, string damageAnimation = "Damage")
        {
            //�����������:
            float totalPhysicalDamageAbsorption = 1
                - (physicalDamageAbsorptionHead
                + physicalDamageAbsorptionBody
                + physicalDamageAbsorptionHands
                + physicalDamageAbsorptionLegs
                + physicalDamageAbsorption
                + temporaryPhysicalDamageAbsorption) / 100;
            //���������˺�:
            physicalDamage = Mathf.RoundToInt(physicalDamage * totalPhysicalDamageAbsorption);
            //�ܻ��������:
            float totalFireDamageAbsorption = 1
                - (fireDamageAbsorptionHead
                + fireDamageAbsorptionBody
                + fireDamageAbsorptionHands
                + fireDamageAbsorptionLegs
                + fireDamageAbsorption
                + temporaryFireDamageAbsorption) / 100;
            //���ջ����˺�:
            fireDamage = Mathf.RoundToInt(totalFireDamageAbsorption * fireDamage);
            //��ħ��������:
            float totalMagicDamageAbsorption = 1
                 - (magicDamageAbsorptionHead
                + magicDamageAbsorptionBody
                + magicDamageAbsorptionHands
                + magicDamageAbsorptionLegs
                + magicDamageAbsorption
                + temporaryMagicDamageAbsorption) / 100;
            //����ħ���˺�:
            magicDamage = Mathf.RoundToInt(totalMagicDamageAbsorption * magicDamage);
            //�׵��˺�������:
            float totalLightningDamageAbsorption = 1
                - (lightningDamageAbsorptionHead
                + lightningDamageAbsorptionBody
                + lightningDamageAbsorptionHands
                + lightningDamageAbsorptionLegs
                + lightningDamageAbsorption
                + temporaryLightningDamageAbsorption) / 100;
            //�����׵��˺�:
            lightningDamage = Mathf.RoundToInt(totalLightningDamageAbsorption * lightningDamage);
            //�������˺�������:
            float totalDarkDamageDamageAbsorption = 1
                - (darkDamageAbsorptionHead
                + darkDamageAbsorptionBody
                + darkDamageAbsorptionHands
                + darkDamageAbsorptionLegs
                + darkDamageAbsorption
                + temporaryDarkDamageAbsorption) / 100;
            //���հ������˺�:
            darkDamage = Mathf.RoundToInt(totalDarkDamageDamageAbsorption * darkDamage);
            //�����˺�:
            int finalDamage = physicalDamage + fireDamage + magicDamage + lightningDamage + darkDamage;

            Debug.Log("�����˺�:" + physicalDamage + " �����˺�:" + fireDamage + " ħ���˺�:" + magicDamage + " �׵��˺�:" + lightningDamage + "���˺�:" + darkDamage);

            currentHealth = currentHealth - finalDamage < 0 ? 0 : (currentHealth - finalDamage);

            characterWeaponSlotManager.CloseDamageCollider();

            StartCoroutine(DisableDamageDetectionCollitions());
        }
        public virtual void TakeDamageNoAnimation(int physicalDamage , int fireDamage , int magicDamage , int lightningDamage , int darkDamage)
        {
            //�����������:
            float totalPhysicalDamageAbsorption = 1 
                - (physicalDamageAbsorptionHead 
                + physicalDamageAbsorptionBody 
                + physicalDamageAbsorptionHands 
                + physicalDamageAbsorptionLegs 
                + physicalDamageAbsorption
                + temporaryPhysicalDamageAbsorption) / 100;
            //���������˺�:
            physicalDamage = Mathf.RoundToInt(physicalDamage * totalPhysicalDamageAbsorption);
            //�ܻ��������:
            float totalFireDamageAbsorption = 1 
                -  (fireDamageAbsorptionHead 
                + fireDamageAbsorptionBody 
                + fireDamageAbsorptionHands 
                + fireDamageAbsorptionLegs 
                + fireDamageAbsorption
                + temporaryFireDamageAbsorption) / 100;
            //���ջ����˺�:
            fireDamage = Mathf.RoundToInt(totalFireDamageAbsorption * fireDamage);
            //��ħ��������:
            float totalMagicDamageAbsorption = 1 
                 - (magicDamageAbsorptionHead 
                + magicDamageAbsorptionBody 
                + magicDamageAbsorptionHands 
                + magicDamageAbsorptionLegs 
                + magicDamageAbsorption
                + temporaryMagicDamageAbsorption) / 100;
            //����ħ���˺�:
            magicDamage = Mathf.RoundToInt(totalMagicDamageAbsorption * magicDamage);
            //�׵��˺�������:
            float totalLightningDamageAbsorption = 1 
                - (lightningDamageAbsorptionHead 
                + lightningDamageAbsorptionBody 
                + lightningDamageAbsorptionHands 
                + lightningDamageAbsorptionLegs 
                + lightningDamageAbsorption
                + temporaryLightningDamageAbsorption) / 100;
            //�����׵��˺�:
            lightningDamage = Mathf.RoundToInt(totalLightningDamageAbsorption * lightningDamage);
            //�������˺�������:
            float totalDarkDamageDamageAbsorption = 1 
                - (darkDamageAbsorptionHead 
                + darkDamageAbsorptionBody 
                + darkDamageAbsorptionHands 
                + darkDamageAbsorptionLegs 
                + darkDamageAbsorption
                + temporaryDarkDamageAbsorption) / 100;
            //���հ������˺�:
            darkDamage = Mathf.RoundToInt(totalDarkDamageDamageAbsorption * darkDamage);
            //�����˺�:
            int finalDamage = physicalDamage + fireDamage + magicDamage + lightningDamage + darkDamage;

            Debug.Log("�����˺�:" + physicalDamage + " �����˺�:" + fireDamage + " ħ���˺�:" + magicDamage + " �׵��˺�:" + lightningDamage + "���˺�:"+ darkDamage);

            currentHealth = (currentHealth - finalDamage) >= 0 ? currentHealth - finalDamage : 0;

            StartCoroutine(DisableDamageDetectionCollitions());
        }
        //���Ƹ��˺�:
        public virtual void TakeBlockingDamage(int physicalDamage, int fireDamage , int magicDamage , int lightningDamage , int darkDamage)
        {
            StartCoroutine(DisableDamageDetectionCollitions());
        }
        //�ж��˺�:
        public virtual void TakePoisonDamage(int damage)
        {
            if (isDead) return;

            //�������˺�����:
            int finalDamage = Mathf.RoundToInt(damage * (1 
                - poisonDefenseAbsorptionHead 
                - poisonDefenseAbsorptionBody 
                - poisonDefenseAbsorptionHead 
                - poisonDefenseAbsorptionLegs - poisonDefenseAbsorption));
            if (finalDamage < 1) finalDamage = 1;

            currentHealth -= finalDamage;
            Debug.Log("�ж��˺�:" + finalDamage);
            if (currentHealth <= 0)
            {
                currentHealth = 0;
            }
        }
        //˪���˺�:
        public virtual void TakeFrostDamage(int damage)
        {
            if (isDead) return;
            //�����˺�:
            int finalDamage = Mathf.RoundToInt(damage * (1 
                - frostDamageAbsorptionHead 
                - frostDamageAbsorptionBody 
                - frostDamageAbsorptionLegs 
                - frostDamageAbsorptionHands - frostDamageAbsorption));

            currentHealth = currentHealth - finalDamage < 0 ? 0 : (currentHealth - finalDamage);

            characterWeaponSlotManager.CloseDamageCollider();
        }
        //��Ѫ�˺�:
        public virtual void TakeHemorrhageDamage(int damage)
        {
            if (isDead) return;

            //��Ѫ���˺�����:
            int finalDamage = Mathf.RoundToInt(damage * (1 
                - hemorrhageDefenseAbsorptionHead 
                - hemorrhageDefenseAbsorptionBody 
                - hemorrhageDefenseAbsorptionLegs 
                - hemorrhageDefenseAbsorptionHands - hemorrhageDefenseAbsorption));
            if (finalDamage < 1) finalDamage = 1;
            Debug.Log("��Ѫ�˺�:" + finalDamage);
            currentHealth -= finalDamage;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
            }
        }

        //�������:
        public virtual void TakeStaminaDamage(int damage)
        {

        }

        //��ҵ��������ķ���:(���ݹ���������������:)
        private void DrainStaminaBasedOnAttackType()
        {

        }

        //�������:
        public virtual void TakeFocusPoints(int damage)
        {
            Debug.Log("����� " + damage.ToString() + " �ķ���ֵ");
        }

        //������:
        public virtual void HealThisTarget(int healAmount)
        {
            Debug.Log("���ڱ�����");
        }
        public virtual void FocusThisTarget(int focusAmount)
        {
            Debug.Log("�����ָ�");
        }

        //���Իָ���ʱ:
        public virtual void HandlePoiseResetTimer()
        {
            if(poiseResetTimer > 0)
            {
                poiseResetTimer -= Time.deltaTime;
            }
            else
            {
                totalPoiseDefence = armorPoiseBonus;
                poiseResetTimer = totalPoiseResetTime;
            }
        }

        //�����������:
        public int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }
        //�����������:
        public float SetMaxStaminaFromLevel()
        {
            maxStamina = staminaLevel * 10;
            return maxStamina;
        }
        //���������:
        public float SetMaxFocusPointsFormLevel()
        {
            maxFocusPoints = focusLevel * 10;
            return maxFocusPoints;
        }

        //����������ײ���رպ�������Э��
        protected IEnumerator DisableDamageDetectionCollitions()
        {
            foreach(DamageDetectionCollition damageDetection in damageDetectionCollitions)
            {
                damageDetection.myCollider.enabled = false;
            }
            yield return new WaitForSeconds  (0.2f);

            foreach (DamageDetectionCollition damageDetection in damageDetectionCollitions)
            {
                damageDetection.myCollider.enabled = true;
            }
        }

        //��ʼ����������:
        public void SetResistance()
        {
            physicalDamageAbsorption = strengthLevel/2 * 0.001f + dexterityLevel * 0.002f;
            magicDamageAbsorption = intelligenceLevel / 2 * 0.002f;
            fireDamageAbsorption = faithLevel / 2 * 0.001f;
            lightningDamageAbsorption = faithLevel / 2 * 0.002f;
            darkDamageAbsorption = faithLevel / 2 * 0.0005f + intelligenceLevel / 2 * 0.0005f;

            poisonDefenseAbsorption = strengthLevel / 2 * 0.001f;
            hemorrhageDefenseAbsorption = strengthLevel / 2 * 0.001f;
            frostDamageAbsorption = dexterityLevel / 2 * 0.001f;
            curseDefenseAbsorption = faithLevel / 2 * 0.0005f + intelligenceLevel / 2 * 0.0005f;
        }
    }
}
