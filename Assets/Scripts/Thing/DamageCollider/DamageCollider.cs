using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class DamageCollider : MonoBehaviour
    {
        public CharacterManager chaeacterManager;
        protected Collider damageCollider;

        [Header("友军ID:")]
        public int teamIDNumber = 0;                 //区分敌我,防止友伤;

        [Header("韧性伤害:")]
        public float poiseBreak;                     //韧性伤害
        public float offensivePoiseBonus;     //出手韧性奖励
        [Header("武器伤害:")]
        public int physicalDamage = 25;
        public int fireDamage;                   //火焰伤害
        public int magicDamage;              //魔力伤害
        public int lightningDamage;         //雷电伤害
        public int darkDamage;                //黑暗伤害
        [Header("武器特殊效果:")]
        public float poison;                                     //毒属性累积;
        public float frost;                                        //寒冷属性累积;
        public float hemorrhage;                           //出血效果累积;
        [Header("武器特殊效果伤害:")]
        public float poisonDamage;
        public float frostDamage;
        public float hemorrhageDamage;
        [Header("属性伤害加成:")]
        public float strengthAddition;                     //力量加成
        public float dexterityAddition;                    //敏捷加成
        public float intelligenceAddition;                //智力加成
        public float faithAddition;                           //信仰加成

        [Header("生成时启用碰撞器:")]
        public bool enabledOnStartUp = false;       //是否启用时启用碰撞器

        protected string currentDamageAnimation;               //当前被击中动画;
        protected bool shieldHasBeenHit;                              //击中盾牌;(两个bool属性用于打断碰撞函数的继续执行)
        protected bool hasBeenParried;                                 //已经招架;
        protected virtual void Awake()
        {
            damageCollider = GetComponent<Collider>();
            chaeacterManager = GetComponentInParent<CharacterManager>();
            damageCollider.gameObject.SetActive(true);
            damageCollider.isTrigger = true;
            damageCollider.enabled = enabledOnStartUp;                                     //是否启用时启用碰撞器
        }
        
        //开启伤害碰撞:
        public void EnableDamageCollider()
        {
            damageCollider.enabled = true;
        }

        //关闭伤害碰撞:
        public void DisaleDamageCollider()
        {
            damageCollider.enabled = false;
        }

        //招架检测:
        private void CheckForParry(CharacterManager enemyManager)
        {
            //目标是弹反状态:
            if (enemyManager.isParrying)
            {
                //播放自身被弹反动画:
                this.chaeacterManager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("Parried", true, 0);
                hasBeenParried = true;  //设置属性已经被弹反(用于碰撞打断函数)
            }
        }

        //盾牌防御的伤害检测:
        protected virtual void CheckForBlock(CharacterManager enemyManager , CharacterStatsManager enemyStatsManager , CharacterEffectsManager enemyEffectsManager , BlockingCollider shidle)
        {
            if (teamIDNumber == enemyStatsManager.teamIDNumber)
                return;

            if (shidle != null && enemyManager.isBlocking)
            {
                //做盾牌的抗性计算:
                float physicalDamageAfterBlock =DamageCalculation(physicalDamage , strengthAddition , dexterityAddition , 0 , 0) * (1 - shidle.blockingPhysicalDamageAbsorption);
                float fireDamageAfterBlock = DamageCalculation(fireDamage , 0 , 0 , intelligenceAddition , faithAddition) * (1 - shidle.blockingFireDamageAbsorption);
                float magicDamageAfterBlock = DamageCalculation(magicDamage , 0 , 0 , intelligenceAddition , 0) * (1 - shidle.blockingMagicDamagerAbsorption);
                float lightningDamageAfterBlock = DamageCalculation(lightningDamage , strengthAddition , 0 , 0 , faithAddition) * (1 - shidle.blockingLightningDamageAbsorption);
                float darkDamageAfterBlock = DamageCalculation(darkDamage, strengthAddition, dexterityAddition, intelligenceAddition, faithAddition);
                //精力损耗计算:
                float staminaDamage = physicalDamage * ((100 - shidle.blockingForce) / 100);
                //扣除精力:
                enemyStatsManager.TakeStaminaDamage(Mathf.RoundToInt(staminaDamage));
                //扣除目标血量:
                enemyStatsManager.TakeBlockingDamage(Mathf.RoundToInt(physicalDamageAfterBlock), Mathf.RoundToInt(fireDamageAfterBlock) , Mathf.RoundToInt(magicDamageAfterBlock) , Mathf.RoundToInt(lightningDamageAfterBlock) , Mathf.RoundToInt(darkDamageAfterBlock));
                //武器特殊效果:
                if (enemyManager.isInvulnerab == false)
                {
                    //1，毒药:
                    enemyEffectsManager.HandlePoisonBuildup(Mathf.RoundToInt(poison), Mathf.RoundToInt(poisonDamage));
                    //2，寒气:
                    enemyEffectsManager.HandleFrostBuildup(Mathf.RoundToInt(frost), Mathf.RoundToInt(frostDamage));
                    //3，出血:
                    enemyEffectsManager.HandleHemorrhageBuildup(Mathf.RoundToInt(hemorrhage), Mathf.RoundToInt(hemorrhageDamage));
                }

                shieldHasBeenHit = true;
            }
        }

        //碰撞检测:
        protected virtual void OnTriggerEnter(Collider collision)
        {
            //Debug.Log("碰撞体名称"+collision.name+collision.tag);
            if (collision.tag.Equals("Hittable"))                                                      //碰撞上可命中对象(标签)
            {

            }

            if (collision.tag.Equals("Character"))                                                         //碰上人形;
            {
                hasBeenParried = false;         //(更新属性)
                shieldHasBeenHit = false;

                DamageDetectionCollition damageDetection = collision.GetComponent<DamageDetectionCollition>();
                CharacterStatsManager enemyState = damageDetection.characterStatsManager;   //collision.GetComponent<CharacterStatsManager>();
                CharacterManager enemyManager = null;
                CharacterEffectsManager enemyEffectsManager = null;
                BlockingCollider shidle = null;
                if (enemyState != null)
                {
                    enemyManager = enemyState.GetComponent<CharacterManager>();
                    enemyEffectsManager = enemyState.GetComponent<CharacterEffectsManager>();
                    shidle = enemyState.GetComponentInChildren<BlockingCollider>();
                }
                
                //当对方处于弹反或格挡状态:
                if (enemyManager != null)
                {
                    //如果碰撞目标的团队id与本碰撞器的友军id标记相同,则不产生伤害
                    if (enemyState.teamIDNumber == this.teamIDNumber)
                        return;

                    //弹反检测:
                    CheckForParry(enemyManager);

                    //盾牌防御伤害检测:
                    CheckForBlock(enemyManager, enemyState, enemyEffectsManager , shidle);
                }

                if (enemyState != null)
                {
                    //如果碰撞目标的团队id与本碰撞器的友军id标记相同,则不产生伤害
                    if (enemyState.teamIDNumber == this.teamIDNumber)
                        return;

                    if (hasBeenParried) //如果已经被弹反
                        return;
                    if (shieldHasBeenHit)   //如果已经格挡防御
                        return;

                    //获取"攻击者(自身)前方向量"和"敌人前方向量"之间的夹角带符号角度
                    float directionHitFrom = Vector3.SignedAngle(chaeacterManager.transform.forward, enemyManager.transform.forward, Vector3.up);
                    ChooseWhichDirectionDamageCameFrom(directionHitFrom);       //获取正确受伤方向的动画

                    //韧性计算:
                    enemyState.poiseResetTimer = enemyState.totalPoiseResetTime;
                    enemyState.totalPoiseDefence -= poiseBreak;
                    //伤害计算:
                    int finalPhysicalDamage = DamageCalculation(physicalDamage , strengthAddition , dexterityAddition , 0 , 0);
                    int finalFireDamage = DamageCalculation(fireDamage , 0 , 0 , intelligenceAddition , faithAddition);
                    int finalMagicDamage = DamageCalculation(magicDamage , 0 , 0 , intelligenceAddition , 0);
                    int finalLightningDamage = DamageCalculation(lightningDamage, 0, 0, 0 , faithAddition);
                    int finalDarkDamage = DamageCalculation(darkDamage, strengthAddition, dexterityAddition, intelligenceAddition, faithAddition);

                    if (enemyState.totalPoiseDefence > 0)
                    {
                        enemyState.TakeDamageNoAnimation(finalPhysicalDamage, finalFireDamage , finalMagicDamage , finalLightningDamage , finalDarkDamage);
                    }
                    else
                    {
                        enemyState.TakeDamage(finalPhysicalDamage, finalFireDamage , finalMagicDamage , finalLightningDamage , finalDarkDamage , currentDamageAnimation);
                    }

                    //获取血液喷溅特效位置:
                    Vector3 contactPoint = collision.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);  //获取到离参数点最近的碰撞器表面的点


                    if (enemyManager.isInvulnerab == false)
                    {
                        enemyEffectsManager.PlayBloodSplatterFX(contactPoint);
                        if(fireDamage > 0)
                        {
                            enemyEffectsManager.PlayFireSplatterFX(contactPoint);
                        }
                        if(magicDamage > 0)
                        {
                            enemyEffectsManager.PlayMagicSplatterFX(contactPoint);
                        }
                        if(lightningDamage > 0)
                        {
                            enemyEffectsManager.PlayLightningSplatterFX(contactPoint);
                        }
                        if(darkDamage > 0)
                        {
                            enemyEffectsManager.PlayDarkSplatterFX(contactPoint);
                        }

                        //武器特殊效果:
                        //1，毒药:
                        enemyEffectsManager.HandlePoisonBuildup(Mathf.RoundToInt(poison), Mathf.RoundToInt(poisonDamage));
                        //2，寒气:
                        enemyEffectsManager.HandleFrostBuildup(Mathf.RoundToInt(frost), Mathf.RoundToInt(frostDamage));
                        //3，出血:
                        enemyEffectsManager.HandleHemorrhageBuildup(Mathf.RoundToInt(hemorrhage), Mathf.RoundToInt(hemorrhageDamage));
                    }
                }
            }

            //碰撞雾墙:
            if (collision.tag.Equals("Illusionary Wall"))
            {
                collision.GetComponent<IllusionaryWall>().wallHasBeenHit = true;
            }
        }

        //伤害计算:
        protected virtual int DamageCalculation(int damage , float strengthAddition , float dexterityAddition , float intelligenceAddition , float faithAddition)
        {
            //属性等级:
            int strengthLevel = chaeacterManager.characterStatsManager.strengthLevel;
            int dexterityLevel = chaeacterManager.characterStatsManager.dexterityLevel;
            int intelligenceLevel = chaeacterManager.characterStatsManager.intelligenceLevel;
            int faithLevel = chaeacterManager.characterStatsManager.faithLevel;

            float finalDamage = damage + damage * (strengthAddition * strengthLevel/20);
            finalDamage += damage * (dexterityAddition * dexterityLevel/20);
            finalDamage += damage * (intelligenceAddition * intelligenceLevel/20);
            finalDamage += damage * (faithAddition * faithLevel/20);

            return Mathf.RoundToInt(finalDamage);
        }
        //正确的受伤动画:
        protected virtual void ChooseWhichDirectionDamageCameFrom(float direction)
        {
            //方向在正面90度内:
            if (direction <= 180 && direction >= 145)
            {
                currentDamageAnimation = "Damager_Forward_01";
            }
            else if (direction >= -180 && direction <= -145)
            {
                currentDamageAnimation = "Damager_Forward_01";
            }
            //方向在右侧90内:
            else if(direction >= 45 && direction < 144)
            {
                currentDamageAnimation = "Damager_Left_01";
            }
            //方向在左侧90内:
            else if(direction <= -45 && direction >= -144)
            {
                currentDamageAnimation = "Damager_Right_01";
            }
            //后方90度:
            else
            {
                currentDamageAnimation = "Damager_Back_01";
            }
        }
    }
}
