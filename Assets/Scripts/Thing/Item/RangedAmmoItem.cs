using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "�µ�����Ϣ", menuName = "��Ϸ��Ʒ/�½�����")]
    public class RangedAmmoItem : Item
    {
        [Header("��������:")]
        public AmmoType ammoType;
        [Header("��������:")]
        [TextArea]
        public string weaponDescription;     //��������
        [Header("��������:")]
        [TextArea]
        public string functionDescription = "Զ����������װ����ҩ����ʹ�á�";

        [Header("�����ٶ�:")]
        public float forwardVelocity = 375;    //��ǰ�ٶ�
        public float upwardVelocity = 0;        //�����ٶ�
        public float ammoMass = 0;              //��������
        public bool useGravity = false;          //��������

        [Header("��������:")]
        public int carryLimit = 99;                  //��λ����(������)
        public int currentAmount = 99;         //��ǰ��������

        [Header("�����˺�:")]
        public int physicalDamage = 50;
        public int fierDamage = 0;                  //�����˺�
        public int magicDamage = 0;             //ħ���˺�
        public int darkDamage = 0;               //�������˺�
        public int lightningDamage = 0;       //�׵��˺�

        [Header("����Ч��:")]
        public float poison;                                     //�������ۻ�;
        public float frost;                                        //���������ۻ�;
        public float hemorrhage;                            //��ѪЧ���ۻ�;
        [Header("����Ч���˺�:")]
        public float poisonDamage;
        public float frostDamage;
        public float hemorrhageDamage;

        [Header("�����˺�:")]
        public int poiseBreak = 15;

        [Header("��Ʒģ��:")]
        public GameObject loadedItemModel;          //���ģ������ʾ��ģ�ͣ�������ʱ������ʵ���������ڹ���
        public GameObject liveAmmoModel;            //��������,���в����˺�����ĵ���ģ��
        public GameObject penetratedModel;          //��������к�����Ŀ�����ϵĵ���ģ��
    }
}