using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [System.Serializable]
    public class ClassGear
    {
        [Header("职业名:")]
        public string className;
        [Header("武器:")]
        public WeaponItem primaryWeapon;
        public WeaponItem offHandWeapon;
        [Header("装备:")]
        public HelmetEquipment headEquipment;   //头盔
        public TorsoEquipment torsoEquipment;     //胸甲
        public HandEquipment handEquipment;     //臂甲
        public LegEquipment legEquipment;           //腿甲
        [Header("陪葬物:")]
        public ItemType itemType;
        public Item funeraryItemOne;
        public int funeraryItemTwoAmount;           //消耗品数量:
        public ConsumableItem funeraryItemTwo;
        public int funeraryItemThreeAmount;         //消耗品数量:
        public ConsumableItem funeraryItemThree;
    }
}