using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [System.Serializable]
    public class ClassStats
    {
        public string playerClass;              //职业名称
        public int classLevel;
        [TextArea]
        public string classDescription;      //职业描述
        [Header("职业基础属性等级:")]
        public int healthLevel = 10;           //健康级别
        public int staminaLevel = 10;        //体力级别
        public int focusLevel = 10;            //法力级别
        public int strengthLevel = 10;       //力量级别
        public int dexterityLevel = 10;      //敏捷级别
        public int intelligenceLevel = 10;  //智力级别
        public int faithLevel = 10;             //信仰级别
        public int soulsRewardLevel = 1;  //灵魂奖励级别
    }
}