using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "�½�����������Ϣ", menuName = "��Ϸ��������/�½���������")]
    public class DodyModelData : ScriptableObject
    {
        [Header("������:")]
        public bool isMale = true;
        [Header("ͷ��:")]
        public int headId;
        [Header("����:")]
        public int hairstyle;
        [Header("�沿ë��:")]
        public int facialHairId = -1;
        [Header("üë:")]
        public int eyebrow;
        [Header("ë����ɫ:")]
        public Color hairColor;
        [Header("Ƥ����ɫ:")]
        public Color skinColor;
        [Header("�沿Ϳѻ��ɫ:")]
        public Color facialMarkColor;

        [Header("����������:")]
        //ë����ɫ����:
        public string attributeHairColor = "_Color_Hair";
        //Ƥ����ɫ����:
        public string attributeSkinColor = "_Color_Skin";
        //�沿Ϳѻ��ɫ:
        public string attributeFacialMarkColor = "_Color_BodyArt";
    }
}
