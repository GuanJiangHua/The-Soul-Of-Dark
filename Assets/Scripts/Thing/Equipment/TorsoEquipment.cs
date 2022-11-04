using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "�½��ؼ���Ϣ", menuName = "��Ϸ��Ʒ/�½�װ��/�½��ؼ�")]
    public class TorsoEquipment : EquipmentItem
    {
        [Header("�ؼ�ģ������:(��ѡ)")]
        public string torsoModelName;
        public string torsoModelName_Male;
        [Header("�����ϱ�ģ������:(��ѡ)")]
        public string leftRearArmModelName;
        public string rightRearArmModeName;
        public string leftRearArmModelName_Male;
        public string rightRearArmModeName_Male;
        [Header("���Ҽ������:(�Ǳ�ѡ)")]
        public string leftShoulderModelsName;
        public string rightShoulderModelName;
        [Header("������Ʒ:(��/�Ǳ�ѡ)")]
        public int behindOrnamentID = -1;    //��Ʒid����;
    }
}