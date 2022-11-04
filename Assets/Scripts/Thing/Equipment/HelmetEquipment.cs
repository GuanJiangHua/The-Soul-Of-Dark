using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "新建头盔信息", menuName = "游戏物品/新建装备/新建头盔")]
    public class HelmetEquipment : EquipmentItem
    {
        [Header("是否覆盖发型:")]
        public bool isCoverHair = true;
        [Header("是否覆盖头部:")]
        public bool isCoverHead;                //是否覆盖头部:
        [Header("是否覆盖面部:")]
        public bool isCoverFaciale = false;
        [Header("头盔模型名称:")]
        public string helmerModelName;
        public string helmerModelName_male;
        [Header("头盔饰品ID数组:(可多选)")]
        public int[] accessoriesIDs;
    }
}
