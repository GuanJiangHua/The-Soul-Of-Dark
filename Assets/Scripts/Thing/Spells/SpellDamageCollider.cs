using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class SpellDamageCollider : DamageCollider
    {
        [Header("��ִ��:")]
        public bool isGuidance = false;
        [Header("���������ٶ�:")]
        public float flightSpeed = 50;
        [Header("ƫת����:")]
        public float deflectionForce = 1;

        [Header("��ײʱ��������Ч:")]
        public GameObject impactParticles;          //��ײ����
        [Header("���е����������Ч:")]
        public GameObject projectileParticles;      //������β����
        [Header("����ʱ��������Ч:")]
        public GameObject muzzleParticles;          //ǹ������
        [Header("����ʱ��:")]
        public float apoptosisTime = 3;

        [Header("����Ŀ��:")]
        CharacterStatsManager spellTarget;    //����Ŀ��;

        bool hasCollided = false;                           //����ײ
        Vector3 impactNormal;                             //��ײ��ķ�����
        Rigidbody myRigidbody;
        AudioSource audioSource;

        //���ŷ���:
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

            //���������β����Ч�����ǿյ�:
            if (projectileParticles != null)
            {
                projectileParticles = Instantiate(projectileParticles, transform.position, transform.rotation);
                projectileParticles.transform.parent = transform;
            }

            //���ǹ������(����ʱ)��Ϊ��:
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

        //����Ŀ��:
        public void SetSpellTarget(CharacterStatsManager enemyStatsManager)
        {
            spellTarget = enemyStatsManager;
        }

        private new void OnTriggerEnter(Collider other)
        {
            if (hasCollided == false)
            {
                print("���������ײ������:" + other.name);
                DamageDetectionCollition damageDetection = other.GetComponent<DamageDetectionCollition>();
                CharacterStatsManager enemyStats = null;
                if (damageDetection != null)
                {
                    enemyStats = damageDetection.characterStatsManager;
                }

                if (enemyStats != null)
                {
                    //�����ײ���Ѿ�,��������´���,����δ��ײ��
                    //print("������ײ�����Ŷ�id:" + enemyStats.teamIDNumber);
                    if (enemyStats.teamIDNumber == this.teamIDNumber)
                        return;

                    //���Լ���:
                    enemyStats.poiseResetTimer = enemyStats.totalPoiseResetTime;
                    enemyStats.totalPoiseDefence -= poiseBreak;
                    //�˺�����:
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
