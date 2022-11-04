using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class RangedProjectileDamageCollider : DamageCollider
    {
        public RangedAmmoItem ammoItem;
        public bool hasAlreadyPenetratedASurface;         //是否已经穿透在某表面
        public GameObject penetratedProjectile;             //穿透在某表面的弹丸模型
        protected override void OnTriggerEnter(Collider collision)
        {
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

                    //盾牌防御伤害检测:
                    CheckForBlock(enemyManager, enemyState, enemyEffectsManager ,shidle);
                }

                if (enemyState != null)
                {
                    //如果碰撞目标的团队id与本碰撞器的友军id标记相同,则不产生伤害
                    if (enemyState.teamIDNumber == this.teamIDNumber)
                        return;

                    if (shieldHasBeenHit)   //如果已经格挡防御
                        return;

                    //获取血液喷溅特效位置:
                    Vector3 contactPoint = collision.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);  //获取到离参数点最近的碰撞器表面的点

                    //获取"攻击者(自身)前方向量"和"敌人前方向量"之间的夹角带符号角度
                    float directionHitFrom = Vector3.SignedAngle(chaeacterManager.transform.forward, enemyManager.transform.forward, Vector3.up);
                    ChooseWhichDirectionDamageCameFrom(directionHitFrom);       //获取正确受伤方向的动画

                    if (enemyManager.isInvulnerab == false)
                    {
                        enemyEffectsManager.PlayBloodSplatterFX(contactPoint);
                        if (fireDamage > 0)
                        {
                            enemyEffectsManager.PlayFireSplatterFX(contactPoint);
                        }
                        //武器特殊效果:
                        //1，毒药:
                        enemyEffectsManager.HandlePoisonBuildup(Mathf.RoundToInt(poison), Mathf.RoundToInt(poisonDamage));
                        //2，寒气:
                        enemyEffectsManager.HandleFrostBuildup(Mathf.RoundToInt(frost), Mathf.RoundToInt(frostDamage));
                        //3，出血:
                        enemyEffectsManager.HandleHemorrhageBuildup(Mathf.RoundToInt(hemorrhage), Mathf.RoundToInt(hemorrhageDamage));
                    }

                    //韧性计算:
                    enemyState.poiseResetTimer = enemyState.totalPoiseResetTime;
                    enemyState.totalPoiseDefence -= poiseBreak;
                    //伤害:
                    int finalPhysicalDamage = physicalDamage;
                    int finalFireDamage = fireDamage;
                    int finalMagicDamage = magicDamage;
                    int finalLightningDamage = lightningDamage;
                    int finalDarkDamage = darkDamage;
                    if (enemyState.totalPoiseDefence > 0)
                    {
                        enemyState.TakeDamageNoAnimation(finalPhysicalDamage, finalFireDamage, finalMagicDamage , finalLightningDamage , finalDarkDamage);
                    }
                    else
                    {
                        enemyState.TakeDamage(finalPhysicalDamage, finalFireDamage, finalMagicDamage, finalLightningDamage, finalDarkDamage , currentDamageAnimation);
                    }
                }
            }

            if(hasAlreadyPenetratedASurface == false && penetratedProjectile == null)
            {
                hasAlreadyPenetratedASurface = true;
                Vector3 contactPoint = collision.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                GameObject penetratedModle = Instantiate(ammoItem.penetratedModel, contactPoint, Quaternion.Euler(0, 0, 0));
                Debug.Log(penetratedModle == null);

                penetratedProjectile = penetratedModle;
                penetratedModle.transform.rotation = Quaternion.LookRotation(gameObject.transform.forward);
                penetratedModle.transform.parent = collision.transform;
            }

            Destroy(transform.root.gameObject);
        }
    }
}
