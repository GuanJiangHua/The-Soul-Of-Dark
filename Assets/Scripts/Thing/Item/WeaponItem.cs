using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "�½�������Ϣ",menuName ="��Ϸ��Ʒ/�½�����")]
    public class WeaponItem : Item
    {
        public GameObject modlPrefab;       //����ģ��Ԥ����;
        public bool isUnarmed;                     //�Ƿ�ͽ��;

        [Header("�������Ƹ�����:")]
        public AnimatorOverrideController weaponController;
        [Header("��������:")]
        public WeaponType weaponType;

        [Header("�������ö���:")]
        public string offHandIdleAnimation = "Left_Arm_Idle_01";
        [Header("��������:")]
        [TextArea]
        public string weaponDescription;     //��������
        [Header("��������:")]
        [TextArea]
        public string functionDescription = "���ڽ�սս����";
        [Header("����:")]
        public float poiseBreak;                    //���������˺�
        public float offensivePoiseBonus;    //�������Խ���

        [Header("�����˺�:")]
        public int physicalDamage;                          //�����˺�;
        public int fierDamage;                                  //�����˺�;
        public int magicDamage;                             //ħ���˺�;
        public int lightningDamage;                        //�׵��˺�;
        public int darkDamage;                               //�������˺�;
        public int criticalDamageMuiltiplier = 4;     //�����˺�����
        [Header("�����˺��ӳ�:")]
        [Range(0, 1)] public float strengthAddition;                     //�����ӳ�
        [Range(0, 1)] public float dexterityAddition;                    //���ݼӳ�
        [Range(0, 1)] public float intelligenceAddition;                //�����ӳ�
        [Range(0, 1)] public float faithAddition;                           //�����ӳ�
        [Header("����Ч��:")]
        public float poison;                                     //�������ۻ�;
        public float frost;                                        //���������ۻ�;
        public float hemorrhage;                            //��ѪЧ���ۻ�;
        [Header("����Ч���˺�:")]
        public float poisonDamage;
        public float frostDamage;
        public float hemorrhageDamage;

        [Header("��ʱ�ļ�����:")]
        [Range(0,1)] public float physicalDamageAbsorption;     //���������;
        [Range(0, 1)] public float fierDamageAbsorption;           //���������;
        [Range(0, 1)] public float magicDamageAbsorption;       //ħ��������;
        [Range(0, 1)] public float lightningDamageAbsorption;  //�׵������;
        [Range(0, 1)] public float darkDamageAbsorption;         //�����Լ�����;
        [Header("����")]
        [Range(0, 100)] public int blockingForce = 20;

        [Header("��������˶���:")]
        public string offHandBlock = "OffHandBlock";                                //��Ϊ������ʱ�񵲶����������ֶ���������ʱ(��˫������)���������������ĸ������񵲡�
        public string mainHandBlock = "MainHandBlock";                         //�������񵲶�����ֻ�����ֳ�������������˫���������������������������񵲶�����
        public string offHandBlock_Impact = "OffHandBlock_Impact";                  //�����������˶���
        public string mainHandBlock_Impact = "MainHandBlock_Impact";            //�����������˶���

        [Header("����Ƶ:")]
        public string waeaponBlockAudio;                       //�����񵲵���Ƶ

        [Header("��������")]
        public int baseStamina;                     //��������
        public float lightAttackMultplier;      //�ṥ������;
        public float heavyAttackMultplier;    //�ع�������;

        [Header("��������:")]
        public ItemAction tap_RB_Action;    //�����ṥ��("������")��;
        public ItemAction hold_RB_Action;  //��ס�ṥ��("������")��;

        public ItemAction tap_RT_Action;     //����Ҽ�����
        public ItemAction hold_RT_Action;   //����Ҽ���ס

        public ItemAction tap_LB_Action;    //���¾ٶ�,��׼,����ʩ��("����F��")��;
        public ItemAction hold_LB_Action;  //��ס�ٶ�,��׼,����ʩ��("����F��")��;

        public ItemAction tap_LT_Action;    //����ս��("shift��")��;
        public ItemAction hold_LT_Action;  //��סս��("shift��")��;
    }
}
