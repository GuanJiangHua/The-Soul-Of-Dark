using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "新建武器信息",menuName ="游戏物品/新建武器")]
    public class WeaponItem : Item
    {
        public GameObject modlPrefab;       //武器模型预制体;
        public bool isUnarmed;                     //是否徒手;

        [Header("动画控制覆盖器:")]
        public AnimatorOverrideController weaponController;
        [Header("武器种类:")]
        public WeaponType weaponType;

        [Header("副手闲置动画:")]
        public string offHandIdleAnimation = "Left_Arm_Idle_01";
        [Header("武器描述:")]
        [TextArea]
        public string weaponDescription;     //武器描述
        [Header("功能描述:")]
        [TextArea]
        public string functionDescription = "用于近战战斗。";
        [Header("韧性:")]
        public float poiseBreak;                    //武器韧性伤害
        public float offensivePoiseBonus;    //出手韧性奖励

        [Header("武器伤害:")]
        public int physicalDamage;                          //物理伤害;
        public int fierDamage;                                  //火焰伤害;
        public int magicDamage;                             //魔力伤害;
        public int lightningDamage;                        //雷电伤害;
        public int darkDamage;                               //暗属性伤害;
        public int criticalDamageMuiltiplier = 4;     //处决伤害倍率
        [Header("属性伤害加成:")]
        [Range(0, 1)] public float strengthAddition;                     //力量加成
        [Range(0, 1)] public float dexterityAddition;                    //敏捷加成
        [Range(0, 1)] public float intelligenceAddition;                //智力加成
        [Range(0, 1)] public float faithAddition;                           //信仰加成
        [Header("特殊效果:")]
        public float poison;                                     //毒属性累积;
        public float frost;                                        //寒冷属性累积;
        public float hemorrhage;                            //出血效果累积;
        [Header("特殊效果伤害:")]
        public float poisonDamage;
        public float frostDamage;
        public float hemorrhageDamage;

        [Header("格挡时的减伤率:")]
        [Range(0,1)] public float physicalDamageAbsorption;     //物理减伤率;
        [Range(0, 1)] public float fierDamageAbsorption;           //火焰减伤率;
        [Range(0, 1)] public float magicDamageAbsorption;       //魔力减伤率;
        [Range(0, 1)] public float lightningDamageAbsorption;  //雷电减伤率;
        [Range(0, 1)] public float darkDamageAbsorption;         //暗属性减伤率;
        [Header("格挡力")]
        [Range(0, 100)] public int blockingForce = 20;

        [Header("格挡与格挡受伤动画:")]
        public string offHandBlock = "OffHandBlock";                                //做为副武器时格挡动作【左右手都持有武器时(不双持武器)，优先左手武器的副武器格挡】
        public string mainHandBlock = "MainHandBlock";                         //主武器格挡动作【只有右手持有武器或正在双持右手武器，用右手武器的主格挡动作】
        public string offHandBlock_Impact = "OffHandBlock_Impact";                  //副武器格挡受伤动画
        public string mainHandBlock_Impact = "MainHandBlock_Impact";            //主武器格挡受伤动画

        [Header("格挡音频:")]
        public string waeaponBlockAudio;                       //武器格挡的音频

        [Header("耐力消耗")]
        public int baseStamina;                     //基础耐力
        public float lightAttackMultplier;      //轻攻击倍率;
        public float heavyAttackMultplier;    //重攻击倍率;

        [Header("按键动作:")]
        public ItemAction tap_RB_Action;    //按下轻攻击("鼠标左键")键;
        public ItemAction hold_RB_Action;  //按住轻攻击("鼠标左键")键;

        public ItemAction tap_RT_Action;     //鼠标右键按下
        public ItemAction hold_RT_Action;   //鼠标右键按住

        public ItemAction tap_LB_Action;    //按下举盾,瞄准,左手施法("键盘F键")键;
        public ItemAction hold_LB_Action;  //按住举盾,瞄准,左手施法("键盘F键")键;

        public ItemAction tap_LT_Action;    //按下战技("shift键")键;
        public ItemAction hold_LT_Action;  //按住战技("shift键")键;
    }
}
