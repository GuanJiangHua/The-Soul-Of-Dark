using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "�½��ȼ���Ϣ", menuName = "��Ϸ��Ʒ/�½�װ��/�½��ȼ�")]
    public class LegEquipment : EquipmentItem
    {
        [Header("�β�:(��Ҫ)")]
        public string hipModleName;
        [Header("���Ե��β�:(��Ҫ)")]
        public string hipModleName_Male;

        [Header("С��:(��Ҫ)")]
        public string leftLegName;      //���
        public string rightLegName;
        [Header("���Ե�С��:(��Ҫ)")]
        public string leftLegName_Male;      //���
        public string rightLegName_Male;
        [Header("����:")]

        public string leftKneePadName;  //��ϥ
        public string rightKneePadName; //�һ�ϥ
        [Header("������Ʒ:")]
        public int[] accessorieId;
    }
}
