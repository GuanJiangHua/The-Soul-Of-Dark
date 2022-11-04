using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "新建腿甲信息", menuName = "游戏物品/新建装备/新建腿甲")]
    public class LegEquipment : EquipmentItem
    {
        [Header("臀部:(必要)")]
        public string hipModleName;
        [Header("男性的臀部:(必要)")]
        public string hipModleName_Male;

        [Header("小腿:(必要)")]
        public string leftLegName;      //左脚
        public string rightLegName;
        [Header("男性的小腿:(必要)")]
        public string leftLegName_Male;      //左脚
        public string rightLegName_Male;
        [Header("可无:")]

        public string leftKneePadName;  //左护膝
        public string rightKneePadName; //右护膝
        [Header("腰部饰品:")]
        public int[] accessorieId;
    }
}
