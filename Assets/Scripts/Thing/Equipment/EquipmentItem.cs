using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class EquipmentItem : Item
    {
        [Header("��������:")]
        public bool isHelmet;
        public bool isTorso;
        public bool isLeg;
        public bool isHand;
        [Header("�����ı�:")]
        [TextArea] public string equipmentIDescription;
        [Header("��������:")]
        [TextArea] public string functionDescription = "���ڷ���������";
        [Header("�����ӳ�:")]
        public float physicalDefense;       //��������ӳ�;
        public float fireDefense;               //�����˺�����;
        public float magicDefense;          //ħ���˺�����;
        public float lightningDefense;     //�׵��˺�����;
        public float darkDefense;            //�ڰ��˺�����;
        [Header("�쳣���Եֿ���:")]
        [Range(0,1)] public float poisonDefense;                 //�����Եֿ���;
        [Range(0, 1)] public float frostDefense;                    //�������Եֿ���;
        [Range(0, 1)] public float hemorrhageDefense;       //��Ѫ���Եֿ���;
        [Range(0, 1)] public float curseDefense;                  //��Ѫ���Եֿ���;
    }
}
