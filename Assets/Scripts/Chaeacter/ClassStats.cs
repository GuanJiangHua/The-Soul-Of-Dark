using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [System.Serializable]
    public class ClassStats
    {
        public string playerClass;              //ְҵ����
        public int classLevel;
        [TextArea]
        public string classDescription;      //ְҵ����
        [Header("ְҵ�������Եȼ�:")]
        public int healthLevel = 10;           //��������
        public int staminaLevel = 10;        //��������
        public int focusLevel = 10;            //��������
        public int strengthLevel = 10;       //��������
        public int dexterityLevel = 10;      //���ݼ���
        public int intelligenceLevel = 10;  //��������
        public int faithLevel = 10;             //��������
        public int soulsRewardLevel = 1;  //��꽱������
    }
}