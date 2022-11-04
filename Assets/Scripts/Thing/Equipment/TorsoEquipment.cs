using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "新建胸甲信息", menuName = "游戏物品/新建装备/新建胸甲")]
    public class TorsoEquipment : EquipmentItem
    {
        [Header("胸甲模型名称:(必选)")]
        public string torsoModelName;
        public string torsoModelName_Male;
        [Header("左右上臂模型名称:(必选)")]
        public string leftRearArmModelName;
        public string rightRearArmModeName;
        public string leftRearArmModelName_Male;
        public string rightRearArmModeName_Male;
        [Header("左右肩甲名称:(非必选)")]
        public string leftShoulderModelsName;
        public string rightShoulderModelName;
        [Header("背后饰品:(后背/非必选)")]
        public int behindOrnamentID = -1;    //饰品id数组;
    }
}