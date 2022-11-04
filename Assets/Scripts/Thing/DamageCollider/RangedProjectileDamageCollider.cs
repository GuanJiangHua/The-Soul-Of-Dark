using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class RangedProjectileDamageCollider : DamageCollider
    {
        public RangedAmmoItem ammoItem;
        public bool hasAlreadyPenetratedASurface;         //�Ƿ��Ѿ���͸��ĳ����
        public GameObject penetratedProjectile;             //��͸��ĳ����ĵ���ģ��
        protected override void OnTriggerEnter(Collider collision)
        {
            if (collision.tag.Equals("Hittable"))                                                      //��ײ�Ͽ����ж���(��ǩ)
            {

            }

            if (collision.tag.Equals("Character"))                                                         //��������;
            {
                hasBeenParried = false;         //(��������)
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

                //���Է����ڵ������״̬:
                if (enemyManager != null)
                {
                    //�����ײĿ����Ŷ�id�뱾��ײ�����Ѿ�id�����ͬ,�򲻲����˺�
                    if (enemyState.teamIDNumber == this.teamIDNumber)
                        return;

                    //���Ʒ����˺����:
                    CheckForBlock(enemyManager, enemyState, enemyEffectsManager ,shidle);
                }

                if (enemyState != null)
                {
                    //�����ײĿ����Ŷ�id�뱾��ײ�����Ѿ�id�����ͬ,�򲻲����˺�
                    if (enemyState.teamIDNumber == this.teamIDNumber)
                        return;

                    if (shieldHasBeenHit)   //����Ѿ��񵲷���
                        return;

                    //��ȡѪҺ�罦��Чλ��:
                    Vector3 contactPoint = collision.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);  //��ȡ����������������ײ������ĵ�

                    //��ȡ"������(����)ǰ������"��"����ǰ������"֮��ļнǴ����ŽǶ�
                    float directionHitFrom = Vector3.SignedAngle(chaeacterManager.transform.forward, enemyManager.transform.forward, Vector3.up);
                    ChooseWhichDirectionDamageCameFrom(directionHitFrom);       //��ȡ��ȷ���˷���Ķ���

                    if (enemyManager.isInvulnerab == false)
                    {
                        enemyEffectsManager.PlayBloodSplatterFX(contactPoint);
                        if (fireDamage > 0)
                        {
                            enemyEffectsManager.PlayFireSplatterFX(contactPoint);
                        }
                        //��������Ч��:
                        //1����ҩ:
                        enemyEffectsManager.HandlePoisonBuildup(Mathf.RoundToInt(poison), Mathf.RoundToInt(poisonDamage));
                        //2������:
                        enemyEffectsManager.HandleFrostBuildup(Mathf.RoundToInt(frost), Mathf.RoundToInt(frostDamage));
                        //3����Ѫ:
                        enemyEffectsManager.HandleHemorrhageBuildup(Mathf.RoundToInt(hemorrhage), Mathf.RoundToInt(hemorrhageDamage));
                    }

                    //���Լ���:
                    enemyState.poiseResetTimer = enemyState.totalPoiseResetTime;
                    enemyState.totalPoiseDefence -= poiseBreak;
                    //�˺�:
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
