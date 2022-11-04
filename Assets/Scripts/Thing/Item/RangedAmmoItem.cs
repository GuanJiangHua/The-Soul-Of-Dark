using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "新弹丸信息", menuName = "游戏物品/新建弹丸")]
    public class RangedAmmoItem : Item
    {
        [Header("弹丸类型:")]
        public AmmoType ammoType;
        [Header("弹丸描述:")]
        [TextArea]
        public string weaponDescription;     //武器描述
        [Header("功能描述:")]
        [TextArea]
        public string functionDescription = "远程武器必须装备弹药才能使用。";

        [Header("弹丸速度:")]
        public float forwardVelocity = 375;    //向前速度
        public float upwardVelocity = 0;        //向上速度
        public float ammoMass = 0;              //弹丸质量
        public bool useGravity = false;          //启用重力

        [Header("弹丸数量:")]
        public int carryLimit = 99;                  //进位限制(最大个数)
        public int currentAmount = 99;         //当前弹丸数量

        [Header("弹丸伤害:")]
        public int physicalDamage = 50;
        public int fierDamage = 0;                  //火焰伤害
        public int magicDamage = 0;             //魔力伤害
        public int darkDamage = 0;               //暗属性伤害
        public int lightningDamage = 0;       //雷电伤害

        [Header("特殊效果:")]
        public float poison;                                     //毒属性累积;
        public float frost;                                        //寒冷属性累积;
        public float hemorrhage;                            //出血效果累积;
        [Header("特殊效果伤害:")]
        public float poisonDamage;
        public float frostDamage;
        public float hemorrhageDamage;

        [Header("韧性伤害:")]
        public int poiseBreak = 15;

        [Header("物品模型:")]
        public GameObject loadedItemModel;          //这个模型是显示用模型，即拉弓时在手上实例化出用于观赏
        public GameObject liveAmmoModel;            //这个是射出,飞行并能伤害人物的弹丸模型
        public GameObject penetratedModel;          //这个是射中后留在目标身上的弹丸模型
    }
}