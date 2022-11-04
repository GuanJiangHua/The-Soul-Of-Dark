using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class DamageCollider : MonoBehaviour
    {
        public CharacterManager chaeacterManager;
        protected Collider damageCollider;

        [Header("�Ѿ�ID:")]
        public int teamIDNumber = 0;                 //���ֵ���,��ֹ����;

        [Header("�����˺�:")]
        public float poiseBreak;                     //�����˺�
        public float offensivePoiseBonus;     //�������Խ���
        [Header("�����˺�:")]
        public int physicalDamage = 25;
        public int fireDamage;                   //�����˺�
        public int magicDamage;              //ħ���˺�
        public int lightningDamage;         //�׵��˺�
        public int darkDamage;                //�ڰ��˺�
        [Header("��������Ч��:")]
        public float poison;                                     //�������ۻ�;
        public float frost;                                        //���������ۻ�;
        public float hemorrhage;                           //��ѪЧ���ۻ�;
        [Header("��������Ч���˺�:")]
        public float poisonDamage;
        public float frostDamage;
        public float hemorrhageDamage;
        [Header("�����˺��ӳ�:")]
        public float strengthAddition;                     //�����ӳ�
        public float dexterityAddition;                    //���ݼӳ�
        public float intelligenceAddition;                //�����ӳ�
        public float faithAddition;                           //�����ӳ�

        [Header("����ʱ������ײ��:")]
        public bool enabledOnStartUp = false;       //�Ƿ�����ʱ������ײ��

        protected string currentDamageAnimation;               //��ǰ�����ж���;
        protected bool shieldHasBeenHit;                              //���ж���;(����bool�������ڴ����ײ�����ļ���ִ��)
        protected bool hasBeenParried;                                 //�Ѿ��м�;
        protected virtual void Awake()
        {
            damageCollider = GetComponent<Collider>();
            chaeacterManager = GetComponentInParent<CharacterManager>();
            damageCollider.gameObject.SetActive(true);
            damageCollider.isTrigger = true;
            damageCollider.enabled = enabledOnStartUp;                                     //�Ƿ�����ʱ������ײ��
        }
        
        //�����˺���ײ:
        public void EnableDamageCollider()
        {
            damageCollider.enabled = true;
        }

        //�ر��˺���ײ:
        public void DisaleDamageCollider()
        {
            damageCollider.enabled = false;
        }

        //�мܼ��:
        private void CheckForParry(CharacterManager enemyManager)
        {
            //Ŀ���ǵ���״̬:
            if (enemyManager.isParrying)
            {
                //����������������:
                this.chaeacterManager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("Parried", true, 0);
                hasBeenParried = true;  //���������Ѿ�������(������ײ��Ϻ���)
            }
        }

        //���Ʒ������˺����:
        protected virtual void CheckForBlock(CharacterManager enemyManager , CharacterStatsManager enemyStatsManager , CharacterEffectsManager enemyEffectsManager , BlockingCollider shidle)
        {
            if (teamIDNumber == enemyStatsManager.teamIDNumber)
                return;

            if (shidle != null && enemyManager.isBlocking)
            {
                //�����ƵĿ��Լ���:
                float physicalDamageAfterBlock =DamageCalculation(physicalDamage , strengthAddition , dexterityAddition , 0 , 0) * (1 - shidle.blockingPhysicalDamageAbsorption);
                float fireDamageAfterBlock = DamageCalculation(fireDamage , 0 , 0 , intelligenceAddition , faithAddition) * (1 - shidle.blockingFireDamageAbsorption);
                float magicDamageAfterBlock = DamageCalculation(magicDamage , 0 , 0 , intelligenceAddition , 0) * (1 - shidle.blockingMagicDamagerAbsorption);
                float lightningDamageAfterBlock = DamageCalculation(lightningDamage , strengthAddition , 0 , 0 , faithAddition) * (1 - shidle.blockingLightningDamageAbsorption);
                float darkDamageAfterBlock = DamageCalculation(darkDamage, strengthAddition, dexterityAddition, intelligenceAddition, faithAddition);
                //������ļ���:
                float staminaDamage = physicalDamage * ((100 - shidle.blockingForce) / 100);
                //�۳�����:
                enemyStatsManager.TakeStaminaDamage(Mathf.RoundToInt(staminaDamage));
                //�۳�Ŀ��Ѫ��:
                enemyStatsManager.TakeBlockingDamage(Mathf.RoundToInt(physicalDamageAfterBlock), Mathf.RoundToInt(fireDamageAfterBlock) , Mathf.RoundToInt(magicDamageAfterBlock) , Mathf.RoundToInt(lightningDamageAfterBlock) , Mathf.RoundToInt(darkDamageAfterBlock));
                //��������Ч��:
                if (enemyManager.isInvulnerab == false)
                {
                    //1����ҩ:
                    enemyEffectsManager.HandlePoisonBuildup(Mathf.RoundToInt(poison), Mathf.RoundToInt(poisonDamage));
                    //2������:
                    enemyEffectsManager.HandleFrostBuildup(Mathf.RoundToInt(frost), Mathf.RoundToInt(frostDamage));
                    //3����Ѫ:
                    enemyEffectsManager.HandleHemorrhageBuildup(Mathf.RoundToInt(hemorrhage), Mathf.RoundToInt(hemorrhageDamage));
                }

                shieldHasBeenHit = true;
            }
        }

        //��ײ���:
        protected virtual void OnTriggerEnter(Collider collision)
        {
            //Debug.Log("��ײ������"+collision.name+collision.tag);
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

                    //�������:
                    CheckForParry(enemyManager);

                    //���Ʒ����˺����:
                    CheckForBlock(enemyManager, enemyState, enemyEffectsManager , shidle);
                }

                if (enemyState != null)
                {
                    //�����ײĿ����Ŷ�id�뱾��ײ�����Ѿ�id�����ͬ,�򲻲����˺�
                    if (enemyState.teamIDNumber == this.teamIDNumber)
                        return;

                    if (hasBeenParried) //����Ѿ�������
                        return;
                    if (shieldHasBeenHit)   //����Ѿ��񵲷���
                        return;

                    //��ȡ"������(����)ǰ������"��"����ǰ������"֮��ļнǴ����ŽǶ�
                    float directionHitFrom = Vector3.SignedAngle(chaeacterManager.transform.forward, enemyManager.transform.forward, Vector3.up);
                    ChooseWhichDirectionDamageCameFrom(directionHitFrom);       //��ȡ��ȷ���˷���Ķ���

                    //���Լ���:
                    enemyState.poiseResetTimer = enemyState.totalPoiseResetTime;
                    enemyState.totalPoiseDefence -= poiseBreak;
                    //�˺�����:
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

                    //��ȡѪҺ�罦��Чλ��:
                    Vector3 contactPoint = collision.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);  //��ȡ����������������ײ������ĵ�


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

                        //��������Ч��:
                        //1����ҩ:
                        enemyEffectsManager.HandlePoisonBuildup(Mathf.RoundToInt(poison), Mathf.RoundToInt(poisonDamage));
                        //2������:
                        enemyEffectsManager.HandleFrostBuildup(Mathf.RoundToInt(frost), Mathf.RoundToInt(frostDamage));
                        //3����Ѫ:
                        enemyEffectsManager.HandleHemorrhageBuildup(Mathf.RoundToInt(hemorrhage), Mathf.RoundToInt(hemorrhageDamage));
                    }
                }
            }

            //��ײ��ǽ:
            if (collision.tag.Equals("Illusionary Wall"))
            {
                collision.GetComponent<IllusionaryWall>().wallHasBeenHit = true;
            }
        }

        //�˺�����:
        protected virtual int DamageCalculation(int damage , float strengthAddition , float dexterityAddition , float intelligenceAddition , float faithAddition)
        {
            //���Եȼ�:
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
        //��ȷ�����˶���:
        protected virtual void ChooseWhichDirectionDamageCameFrom(float direction)
        {
            //����������90����:
            if (direction <= 180 && direction >= 145)
            {
                currentDamageAnimation = "Damager_Forward_01";
            }
            else if (direction >= -180 && direction <= -145)
            {
                currentDamageAnimation = "Damager_Forward_01";
            }
            //�������Ҳ�90��:
            else if(direction >= 45 && direction < 144)
            {
                currentDamageAnimation = "Damager_Left_01";
            }
            //���������90��:
            else if(direction <= -45 && direction >= -144)
            {
                currentDamageAnimation = "Damager_Right_01";
            }
            //��90��:
            else
            {
                currentDamageAnimation = "Damager_Back_01";
            }
        }
    }
}
