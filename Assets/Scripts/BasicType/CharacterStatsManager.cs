using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class CharacterStatsManager : MonoBehaviour
    {
        public int maxHealth;
        public int currentHealth;

        public float maxStamina;              //最大耐力值
        public float currentStamina;         //当前耐力值

        public float maxFocusPoints;       //最大法力值
        public float currentFocusPoints;  //当前法力值

        public int currentSoulCount = 0;         //灵魂量
        [Header("玩家名称:")]
        public string characterName = "DefaultName";

        [Header("人物等级:")]

        public int characterLeve = 1;

        [Header("团队ID:")]
        public int teamIDNumber = 0;                 //区分敌我,防止友伤;

        [Header("属性等级:")]
        public int healthLevel = 10;           //健康级别
        public int staminaLevel = 10;        //体力级别
        public int focusLevel = 10;            //法力级别
        public int poiseLevel = 10;            //韧性级别
        public int strengthLevel = 10;       //力量级别
        public int dexterityLevel = 10;      //敏捷级别
        public int intelligenceLevel = 10;  //智力级别
        public int faithLevel = 10;             //信仰级别
        public float soulsRewardLevel = 1;  //灵魂奖励级别

        public bool isDead;
        [Header("韧性:")]
        public float totalPoiseDefence;                               //伤害计算期间的总柔韧值
        public float offensivePoiseBonus;                           //出手韧性
        public float armorPoiseBonus;                                //盔甲韧性奖励(常驻韧性)
        public float totalPoiseResetTime = 15;                   //总韧性重置时间
        public float poiseResetTimer = 0;                           //韧性重置计时器

        #region 抗性:
        [Header("常驻抗性加成:")]
        [HideInInspector] public float physicalDamageAbsorption;                //属性等级带来的额外抗性;
        [HideInInspector] public float fireDamageAbsorption;
        [HideInInspector] public float magicDamageAbsorption;
        [HideInInspector] public float lightningDamageAbsorption;
        [HideInInspector] public float darkDamageAbsorption;
        [Header("临时抗性加成:")]
        public float temporaryPhysicalDamageAbsorption;                //戒指,法术带来的额外抗性;
        public float temporaryFireDamageAbsorption;
        public float temporaryMagicDamageAbsorption;
        public float temporaryLightningDamageAbsorption;
        public float temporaryDarkDamageAbsorption;

        [Header("护甲物理伤害抗性:")]
        public float physicalDamageAbsorptionHead;       //头部护甲(物理伤害吸收)
        public float physicalDamageAbsorptionBody;       //躯干护甲(物理伤害吸收)
        public float physicalDamageAbsorptionLegs;        //腿部护甲(物理伤害吸收)
        public float physicalDamageAbsorptionHands;     //手臂护甲(物理伤害吸收)

        [Header("护甲火焰伤害抗性:")]
        public float fireDamageAbsorptionHead;       //头部护甲(火焰伤害吸收)
        public float fireDamageAbsorptionBody;       //躯干护甲(火焰伤害吸收)
        public float fireDamageAbsorptionLegs;        //腿部护甲(火焰伤害吸收)
        public float fireDamageAbsorptionHands;     //手臂护甲(火焰伤害吸收)

        [Header("护甲魔力伤害抗性:")]
        public float magicDamageAbsorptionHead;       //头部护甲(魔力伤害吸收)
        public float magicDamageAbsorptionBody;       //躯干护甲(魔力伤害吸收)
        public float magicDamageAbsorptionLegs;        //腿部护甲(魔力伤害吸收)
        public float magicDamageAbsorptionHands;     //手臂护甲(魔力伤害吸收)

        [Header("护甲雷电伤害抗性:")]
        public float lightningDamageAbsorptionHead;       //头部护甲(雷电伤害吸收)
        public float lightningDamageAbsorptionBody;       //躯干护甲(雷电伤害吸收)
        public float lightningDamageAbsorptionLegs;        //腿部护甲(雷电伤害吸收)
        public float lightningDamageAbsorptionHands;     //手臂护甲(雷电伤害吸收)dark

        [Header("护甲暗属性伤害抗性:")]
        public float darkDamageAbsorptionHead;       //头部护甲(雷电伤害吸收)
        public float darkDamageAbsorptionBody;       //躯干护甲(雷电伤害吸收)
        public float darkDamageAbsorptionLegs;        //腿部护甲(雷电伤害吸收)
        public float darkDamageAbsorptionHands;     //手臂护甲(雷电伤害吸收)

        [Header("常驻异常属性抵抗力:")]
        [HideInInspector] public float poisonDefenseAbsorption;
        [HideInInspector] public float hemorrhageDefenseAbsorption;
        [HideInInspector] public float frostDamageAbsorption;
        [HideInInspector] public float curseDefenseAbsorption;
        [Header("临时异常属性抵抗力:")]
        public float temporaryPoisonDefenseAbsorption;
        public float temporaryHemorrhageDefenseAbsorption;
        public float temporaryFrostDamageAbsorption;
        public float temporaryCurseDefenseAbsorption;
        [Header("异常属性抵抗力:")]
        public float poisonDefenseAbsorptionHead;                        //毒属性抵抗力头盔;
        public float poisonDefenseAbsorptionBody;                        //毒属性抵抗力胸甲;
        public float poisonDefenseAbsorptionLegs;                        //毒属性抵抗力腿甲;
        public float poisonDefenseAbsorptionHands;                     //毒属性抵抗力护手;

        public float hemorrhageDefenseAbsorptionHead;               //出血属性抵抗力头盔;
        public float hemorrhageDefenseAbsorptionBody;               //出血属性抵抗力胸甲;
        public float hemorrhageDefenseAbsorptionLegs;                //出血属性抵抗力腿甲;
        public float hemorrhageDefenseAbsorptionHands;             //出血属性抵抗力护手;

        public float frostDamageAbsorptionHead;                          //霜冻抵抗力头盔;
        public float frostDamageAbsorptionBody;                          //霜冻抵抗力胸甲;
        public float frostDamageAbsorptionLegs;                           //霜冻抵抗力腿甲;
        public float frostDamageAbsorptionHands;                        //霜冻抵抗力护手;

        public float curseDefenseAbsorptionHead;                          //咒死属性抵抗力;
        public float curseDefenseAbsorptionBody;                          //咒死属性抵抗力;
        public float curseDefenseAbsorptionLegs;                          //咒死属性抵抗力;
        public float curseDefenseAbsorptionHands;                       //咒死属性抵抗力;
        #endregion

        [Header("死亡获取的灵魂:")]
        public int soulsAwardedOnDeath = 50;    //死亡后获得的灵魂

        public CharacterManager chaeacterManager;
        [Header("躯干受伤碰撞器:")]
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
        //生命值损耗:
        public virtual void TakeDamage(int physicalDamage, int fireDamage , int magicDamage ,int lightningDamage , int darkDamage, string damageAnimation = "Damage")
        {
            //总物理减伤率:
            float totalPhysicalDamageAbsorption = 1
                - (physicalDamageAbsorptionHead
                + physicalDamageAbsorptionBody
                + physicalDamageAbsorptionHands
                + physicalDamageAbsorptionLegs
                + physicalDamageAbsorption
                + temporaryPhysicalDamageAbsorption) / 100;
            //最终物理伤害:
            physicalDamage = Mathf.RoundToInt(physicalDamage * totalPhysicalDamageAbsorption);
            //总火焰减伤率:
            float totalFireDamageAbsorption = 1
                - (fireDamageAbsorptionHead
                + fireDamageAbsorptionBody
                + fireDamageAbsorptionHands
                + fireDamageAbsorptionLegs
                + fireDamageAbsorption
                + temporaryFireDamageAbsorption) / 100;
            //最终火焰伤害:
            fireDamage = Mathf.RoundToInt(totalFireDamageAbsorption * fireDamage);
            //总魔法减伤率:
            float totalMagicDamageAbsorption = 1
                 - (magicDamageAbsorptionHead
                + magicDamageAbsorptionBody
                + magicDamageAbsorptionHands
                + magicDamageAbsorptionLegs
                + magicDamageAbsorption
                + temporaryMagicDamageAbsorption) / 100;
            //最终魔法伤害:
            magicDamage = Mathf.RoundToInt(totalMagicDamageAbsorption * magicDamage);
            //雷电伤害减伤率:
            float totalLightningDamageAbsorption = 1
                - (lightningDamageAbsorptionHead
                + lightningDamageAbsorptionBody
                + lightningDamageAbsorptionHands
                + lightningDamageAbsorptionLegs
                + lightningDamageAbsorption
                + temporaryLightningDamageAbsorption) / 100;
            //最终雷电伤害:
            lightningDamage = Mathf.RoundToInt(totalLightningDamageAbsorption * lightningDamage);
            //暗属性伤害减伤率:
            float totalDarkDamageDamageAbsorption = 1
                - (darkDamageAbsorptionHead
                + darkDamageAbsorptionBody
                + darkDamageAbsorptionHands
                + darkDamageAbsorptionLegs
                + darkDamageAbsorption
                + temporaryDarkDamageAbsorption) / 100;
            //最终暗属性伤害:
            darkDamage = Mathf.RoundToInt(totalDarkDamageDamageAbsorption * darkDamage);
            //最终伤害:
            int finalDamage = physicalDamage + fireDamage + magicDamage + lightningDamage + darkDamage;

            Debug.Log("物理伤害:" + physicalDamage + " 火焰伤害:" + fireDamage + " 魔法伤害:" + magicDamage + " 雷电伤害:" + lightningDamage + "暗伤害:" + darkDamage);

            currentHealth = currentHealth - finalDamage < 0 ? 0 : (currentHealth - finalDamage);

            characterWeaponSlotManager.CloseDamageCollider();

            StartCoroutine(DisableDamageDetectionCollitions());
        }
        public virtual void TakeDamageNoAnimation(int physicalDamage , int fireDamage , int magicDamage , int lightningDamage , int darkDamage)
        {
            //总物理减伤率:
            float totalPhysicalDamageAbsorption = 1 
                - (physicalDamageAbsorptionHead 
                + physicalDamageAbsorptionBody 
                + physicalDamageAbsorptionHands 
                + physicalDamageAbsorptionLegs 
                + physicalDamageAbsorption
                + temporaryPhysicalDamageAbsorption) / 100;
            //最终物理伤害:
            physicalDamage = Mathf.RoundToInt(physicalDamage * totalPhysicalDamageAbsorption);
            //总火焰减伤率:
            float totalFireDamageAbsorption = 1 
                -  (fireDamageAbsorptionHead 
                + fireDamageAbsorptionBody 
                + fireDamageAbsorptionHands 
                + fireDamageAbsorptionLegs 
                + fireDamageAbsorption
                + temporaryFireDamageAbsorption) / 100;
            //最终火焰伤害:
            fireDamage = Mathf.RoundToInt(totalFireDamageAbsorption * fireDamage);
            //总魔法减伤率:
            float totalMagicDamageAbsorption = 1 
                 - (magicDamageAbsorptionHead 
                + magicDamageAbsorptionBody 
                + magicDamageAbsorptionHands 
                + magicDamageAbsorptionLegs 
                + magicDamageAbsorption
                + temporaryMagicDamageAbsorption) / 100;
            //最终魔法伤害:
            magicDamage = Mathf.RoundToInt(totalMagicDamageAbsorption * magicDamage);
            //雷电伤害减伤率:
            float totalLightningDamageAbsorption = 1 
                - (lightningDamageAbsorptionHead 
                + lightningDamageAbsorptionBody 
                + lightningDamageAbsorptionHands 
                + lightningDamageAbsorptionLegs 
                + lightningDamageAbsorption
                + temporaryLightningDamageAbsorption) / 100;
            //最终雷电伤害:
            lightningDamage = Mathf.RoundToInt(totalLightningDamageAbsorption * lightningDamage);
            //暗属性伤害减伤率:
            float totalDarkDamageDamageAbsorption = 1 
                - (darkDamageAbsorptionHead 
                + darkDamageAbsorptionBody 
                + darkDamageAbsorptionHands 
                + darkDamageAbsorptionLegs 
                + darkDamageAbsorption
                + temporaryDarkDamageAbsorption) / 100;
            //最终暗属性伤害:
            darkDamage = Mathf.RoundToInt(totalDarkDamageDamageAbsorption * darkDamage);
            //最终伤害:
            int finalDamage = physicalDamage + fireDamage + magicDamage + lightningDamage + darkDamage;

            Debug.Log("物理伤害:" + physicalDamage + " 火焰伤害:" + fireDamage + " 魔法伤害:" + magicDamage + " 雷电伤害:" + lightningDamage + "暗伤害:"+ darkDamage);

            currentHealth = (currentHealth - finalDamage) >= 0 ? currentHealth - finalDamage : 0;

            StartCoroutine(DisableDamageDetectionCollitions());
        }
        //盾牌格挡伤害:
        public virtual void TakeBlockingDamage(int physicalDamage, int fireDamage , int magicDamage , int lightningDamage , int darkDamage)
        {
            StartCoroutine(DisableDamageDetectionCollitions());
        }
        //中毒伤害:
        public virtual void TakePoisonDamage(int damage)
        {
            if (isDead) return;

            //毒抗的伤害计算:
            int finalDamage = Mathf.RoundToInt(damage * (1 
                - poisonDefenseAbsorptionHead 
                - poisonDefenseAbsorptionBody 
                - poisonDefenseAbsorptionHead 
                - poisonDefenseAbsorptionLegs - poisonDefenseAbsorption));
            if (finalDamage < 1) finalDamage = 1;

            currentHealth -= finalDamage;
            Debug.Log("中毒伤害:" + finalDamage);
            if (currentHealth <= 0)
            {
                currentHealth = 0;
            }
        }
        //霜冻伤害:
        public virtual void TakeFrostDamage(int damage)
        {
            if (isDead) return;
            //最终伤害:
            int finalDamage = Mathf.RoundToInt(damage * (1 
                - frostDamageAbsorptionHead 
                - frostDamageAbsorptionBody 
                - frostDamageAbsorptionLegs 
                - frostDamageAbsorptionHands - frostDamageAbsorption));

            currentHealth = currentHealth - finalDamage < 0 ? 0 : (currentHealth - finalDamage);

            characterWeaponSlotManager.CloseDamageCollider();
        }
        //出血伤害:
        public virtual void TakeHemorrhageDamage(int damage)
        {
            if (isDead) return;

            //出血的伤害计算:
            int finalDamage = Mathf.RoundToInt(damage * (1 
                - hemorrhageDefenseAbsorptionHead 
                - hemorrhageDefenseAbsorptionBody 
                - hemorrhageDefenseAbsorptionLegs 
                - hemorrhageDefenseAbsorptionHands - hemorrhageDefenseAbsorption));
            if (finalDamage < 1) finalDamage = 1;
            Debug.Log("出血伤害:" + finalDamage);
            currentHealth -= finalDamage;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
            }
        }

        //耐力损耗:
        public virtual void TakeStaminaDamage(int damage)
        {

        }

        //玩家的耐力消耗方法:(根据攻击类型消耗体力:)
        private void DrainStaminaBasedOnAttackType()
        {

        }

        //法力损耗:
        public virtual void TakeFocusPoints(int damage)
        {
            Debug.Log("损耗了 " + damage.ToString() + " 的法力值");
        }

        //被治疗:
        public virtual void HealThisTarget(int healAmount)
        {
            Debug.Log("正在被治疗");
        }
        public virtual void FocusThisTarget(int focusAmount)
        {
            Debug.Log("法力恢复");
        }

        //韧性恢复计时:
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

        //设置最大生命:
        public int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }
        //设置最大耐力:
        public float SetMaxStaminaFromLevel()
        {
            maxStamina = staminaLevel * 10;
            return maxStamina;
        }
        //设置最大法力:
        public float SetMaxFocusPointsFormLevel()
        {
            maxFocusPoints = focusLevel * 10;
            return maxFocusPoints;
        }

        //被攻击后碰撞器关闭后重启的协程
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

        //初始化其他抗性:
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
