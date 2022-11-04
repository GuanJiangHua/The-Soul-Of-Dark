using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class SpellItem : Item
    {
        public GameObject spellWarmUpFX;    //��������Ч��
        public GameObject spellCastFX;           //ʩ���ɹ�Ч��

        public string spellAnimation;               //ʩ������
        [Header("ռ�ݵķ�����:")]
        public int occupiedSpellGrid = 1;
        [Header("��������:")]
        public int focusPointCost;            //��������

        [Header("��������:")]
        public bool isFaithSpell;               //�漣����
        public bool isMagicSpell;             //ħ������
        public bool isPyroSpell;               //��������
        [Header("�ȼ�Ҫ��:")]
        public int requiredIntelligenceLevel;   //���������
        public int requiredFaithLevel;             //���������

        [Header("��������:")]
        [TextArea]
        public string spellDescription;     //�������� 
        [Header("��������:")]
        [TextArea]
        public string functionDescription = "ʹ��ʩ������ʱ����װ����Ӧ���͵����";

        //����ʩ��:
        public virtual void AttemptToCastSpell(AnimatorManager animatorHandler, CharacterStatsManager playerState , PlayerWeaponSlotManger weaponSlotManger , bool isLeftHanded)
        {
            
        }

        public virtual void SucessfullyCastSpell(AnimatorManager animatorHandler, CharacterStatsManager playerState, PlayerWeaponSlotManger weaponSlotManger , CameraHandler cameraHandler , bool isLeftHanded)
        {
            //��ķ���ֵ:
            playerState.TakeFocusPoints(focusPointCost);
        }

        //�˺��ӳ�:
        protected int DamageCalculation(int damage, WeaponItem weapon, CharacterStatsManager playerState)
        {
            //�ȼ�:
            int intelligence = playerState.intelligenceLevel;
            int faith = playerState.faithLevel;
            //�ӳ�:
            float intelligenceAddition = weapon.intelligenceAddition;
            float faithAddition = weapon.faithAddition;

            float finalDamage = damage + damage * (intelligenceAddition * intelligence / 20);
            finalDamage += damage * (faithAddition * faith / 20);

            return Mathf.RoundToInt(finalDamage);
        }
    }
}
