using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "�½�ͷ����Ϣ", menuName = "��Ϸ��Ʒ/�½�װ��/�½�ͷ��")]
    public class HelmetEquipment : EquipmentItem
    {
        [Header("�Ƿ񸲸Ƿ���:")]
        public bool isCoverHair = true;
        [Header("�Ƿ񸲸�ͷ��:")]
        public bool isCoverHead;                //�Ƿ񸲸�ͷ��:
        [Header("�Ƿ񸲸��沿:")]
        public bool isCoverFaciale = false;
        [Header("ͷ��ģ������:")]
        public string helmerModelName;
        public string helmerModelName_male;
        [Header("ͷ����ƷID����:(�ɶ�ѡ)")]
        public int[] accessoriesIDs;
    }
}
