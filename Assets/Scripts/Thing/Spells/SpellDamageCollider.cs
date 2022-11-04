using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class SpellDamageCollider : DamageCollider
    {
        [Header("可执导:")]
        public bool isGuidance = false;
        [Header("导航飞行速度:")]
        public float flightSpeed = 50;
        [Header("偏转力度:")]
        public float deflectionForce = 1;

        [Header("碰撞时的粒子特效:")]
        public GameObject impactParticles;          //碰撞粒子
        [Header("飞行弹丸的粒子特效:")]
        public GameObject projectileParticles;      //弹丸拖尾粒子
        [Header("出膛时的粒子特效:")]
        public GameObject muzzleParticles;          //枪口粒子
        [Header("死亡时间:")]
        public float apoptosisTime = 3;

        [Header("法术目标:")]
        CharacterStatsManager spellTarget;    //法术目标;

        bool hasCollided = false;                           //已碰撞
        Vector3 impactNormal;                             //碰撞面的法向量
        Rigidbody myRigidbody;
        AudioSource audioSource;

        //出膛方向:
        Vector3 exitDirection;
        protected override void Awake()
        {
            myRigidbody = this.GetComponent<Rigidbody>();
            audioSource = GetComponent<AudioSource>();
        }
        private void Start()
        {
            if (isGuidance)
            {
                exitDirection = transform.forward;

                float YDirectionOffset = Random.Range(0 , 5) * 0.1f;
                float XDirectionOffset = Random.Range(-3, 4) * 0.1f;
                print(XDirectionOffset + "," + YDirectionOffset);

                myRigidbody.velocity = (transform.up * YDirectionOffset + transform.forward * 1f + transform.right * XDirectionOffset).normalized;
            }

            //如果弹丸拖尾粒子效果不是空的:
            if (projectileParticles != null)
            {
                projectileParticles = Instantiate(projectileParticles, transform.position, transform.rotation);
                projectileParticles.transform.parent = transform;
            }

            //如果枪口粒子(出膛时)不为空:
            if (muzzleParticles != null)
            {
                GameObject istantiatemuzzleParticles = Instantiate(muzzleParticles, transform.position, transform.rotation);
                //istantiatemuzzleParticles.transform.parent = transform;
                Destroy(istantiatemuzzleParticles, 5);
            }
        }

        private void Update()
        {
            if (isGuidance)
            {
                MoveForward();
            }
        }

        //给予目标:
        public void SetSpellTarget(CharacterStatsManager enemyStatsManager)
        {
            spellTarget = enemyStatsManager;
        }

        private new void OnTriggerEnter(Collider other)
        {
            if (hasCollided == false)
            {
                print("物体初次碰撞物名称:" + other.name);
                DamageDetectionCollition damageDetection = other.GetComponent<DamageDetectionCollition>();
                CharacterStatsManager enemyStats = null;
                if (damageDetection != null)
                {
                    enemyStats = damageDetection.characterStatsManager;
                }

                if (enemyStats != null)
                {
                    //如果碰撞到友军,则结束以下代码,当作未碰撞到
                    //print("物体碰撞人物团队id:" + enemyStats.teamIDNumber);
                    if (enemyStats.teamIDNumber == this.teamIDNumber)
                        return;

                    //韧性计算:
                    enemyStats.poiseResetTimer = enemyStats.totalPoiseResetTime;
                    enemyStats.totalPoiseDefence -= poiseBreak;
                    //伤害计算:
                    int finalPhysicalDamage =physicalDamage;
                    int finalFireDamage = fireDamage;
                    int finalMagicDamage = magicDamage;
                    int finalLightningDamage = lightningDamage;
                    int finalDarkDamage = darkDamage;
                    if (enemyStats.totalPoiseDefence > poiseBreak)
                    {
                        enemyStats.TakeDamageNoAnimation(finalPhysicalDamage, finalFireDamage, finalMagicDamage , finalLightningDamage , finalDarkDamage);
                    }
                    else
                    {
                        enemyStats.TakeDamage(finalPhysicalDamage, finalFireDamage, finalMagicDamage , finalLightningDamage , finalDarkDamage);
                    }
                }

                if (audioSource != null)
                {
                    audioSource.Play();
                }
                hasCollided = true;
                impactParticles = Instantiate(impactParticles, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal));

                Destroy(impactParticles, 5);
                Destroy(gameObject, apoptosisTime);

                isGuidance = false;
            }
        }
    
        private void MoveForward()
        {
            if(spellTarget != null)
            {
                Vector3 dir = spellTarget.transform.position - transform.position + new Vector3(0,1,0);

                Vector3 newVelocity= Vector3.Lerp(myRigidbody.velocity , dir , Time.deltaTime * deflectionForce);

                newVelocity.Normalize();

                myRigidbody.velocity = newVelocity * flightSpeed;
            }
            else
            {

                myRigidbody.velocity = Vector3.Lerp(myRigidbody.velocity, exitDirection, Time.deltaTime * 50).normalized * flightSpeed;
            }
        }
    }
}
