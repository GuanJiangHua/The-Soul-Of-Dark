using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "�½��ۼ���Ϣ", menuName = "��Ϸ��Ʒ/�½�װ��/�½��ۼ�")]
    public class HandEquipment : EquipmentItem
    {
        [Header("ǰ��:(�ر�)")]
        public string leftForearmName;
        public string rightForearmName;
        public string leftForearmName_Male;
        public string rightForearmName_Male;
        [Header("����:(�ر�)")]
        public string leftPalmName;
        public string rightPalmName;
        public string leftPalmName_Male;
        public string rightPalmName_Male;
        [Header("�ⲿ����:(�ɲ���)")]
        public string leftElbowName;
        public string rightElbowName;
    }
}
