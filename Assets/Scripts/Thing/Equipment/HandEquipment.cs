using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "新建臂甲信息", menuName = "游戏物品/新建装备/新建臂甲")]
    public class HandEquipment : EquipmentItem
    {
        [Header("前臂:(必备)")]
        public string leftForearmName;
        public string rightForearmName;
        public string leftForearmName_Male;
        public string rightForearmName_Male;
        [Header("手掌:(必备)")]
        public string leftPalmName;
        public string rightPalmName;
        public string leftPalmName_Male;
        public string rightPalmName_Male;
        [Header("肘部护甲:(可不填)")]
        public string leftElbowName;
        public string rightElbowName;
    }
}
