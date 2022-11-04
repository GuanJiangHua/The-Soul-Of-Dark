using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "新建戒指信息", menuName = "游戏物品/新建装备/新建戒指")]
    public class RingItem : Item
    {
        [Header("介绍文本:")]
        [TextArea] public string equipmentIDescription;
        [Header("功能描述:")]
        [TextArea] public string functionDescription = "装备戒指可以获得特殊属性加成。";

        //基础属性:
        [Header("基础属性加成:")]
        public int maxHealthAddition;                   //最大生命值
        public float maxStaminaAddition;             //最大耐力值
        public float maxFocusPointsAddition;       //最大法力值
        //属性等级:
        [Header("属性等级加成:")]
        public int healthLevelAddition = 0;             //健康级别
        public int staminaLevelAddition = 0;          //体力级别
        public int focusLevelAddition = 0;              //法力级别
        public int strengthLevelAddition = 0;         //力量级别
        public int dexterityLevelAddition = 0;        //敏捷级别
        public int intelligenceLevelAddition = 0;    //智力级别
        public int faithLevelAddition = 0;               //信仰级别
        public int soulsRewardLevelAddition = 0;  //灵魂奖励级别
        [Header("临时抗性:")]
        public float temporaryPhysicalDamageAbsorption;
        public float temporaryFireDamageAbsorption;
        public float temporaryMagicDamageAbsorption;
        public float temporaryLightningDamageAbsorption;
        public float temporaryDarkDamageAbsorption;
        [Header("临时抵抗力:")]
        public float temporaryPoisonDefenseAbsorption;
        public float temporaryHemorrhageDefenseAbsorption;
        public float temporaryFrostDamageAbsorption;
        public float temporaryCurseDefenseAbsorption;

        //特殊效果方法
        //...

        //带上戒指:
        public virtual void PutOnTheRing(PlayerManager player)
        {
            PlayerStatsManager playerState = player.playerStateManager;
            playerState.maxHealth += maxHealthAddition;
            playerState.maxStamina += maxStaminaAddition;
            playerState.maxFocusPoints += maxFocusPointsAddition;

            playerState.healthLevel += healthLevelAddition;
            playerState.staminaLevel += staminaLevelAddition;
            playerState.focusLevel += faithLevelAddition;
            playerState.strengthLevel += strengthLevelAddition;
            playerState.dexterityLevel += dexterityLevelAddition;
            playerState.intelligenceLevel += intelligenceLevelAddition;
            playerState.faithLevel += faithLevelAddition;
            playerState.soulsRewardLevel += soulsRewardLevelAddition;

            //更新基础属性:
            playerState.SetMaxHealthFromHealthLevel();
            playerState.SetMaxStaminaFromLevel();
            playerState.SetMaxFocusPointsFormLevel();

            //更新临时抗性:
            playerState.temporaryPhysicalDamageAbsorption += temporaryPhysicalDamageAbsorption;
            playerState.temporaryFireDamageAbsorption += temporaryFireDamageAbsorption;
            playerState.temporaryMagicDamageAbsorption += temporaryMagicDamageAbsorption;
            playerState.temporaryLightningDamageAbsorption += temporaryLightningDamageAbsorption;
            playerState.temporaryDarkDamageAbsorption += temporaryDarkDamageAbsorption;
            //更新临时抵抗力:
            playerState.temporaryPoisonDefenseAbsorption += temporaryPoisonDefenseAbsorption;
            playerState.temporaryFrostDamageAbsorption += temporaryFrostDamageAbsorption;
            playerState.temporaryHemorrhageDefenseAbsorption += temporaryHemorrhageDefenseAbsorption;
            playerState.temporaryCurseDefenseAbsorption += temporaryCurseDefenseAbsorption;

            //计算记忆空格:
            player.playerInventoryManager.CountMemorySpellSlotNumber();
        }

        //摘下戒指:
        public virtual void TakeOffTheRing(PlayerManager player)
        {
            PlayerStatsManager playerState = player.playerStateManager;
            playerState.maxHealth -= maxHealthAddition;
            playerState.maxStamina -= maxStaminaAddition;
            playerState.maxFocusPoints -= maxFocusPointsAddition;

            playerState.healthLevel -= healthLevelAddition;
            playerState.staminaLevel -= staminaLevelAddition;
            playerState.focusLevel -= faithLevelAddition;
            playerState.strengthLevel -= strengthLevelAddition;
            playerState.dexterityLevel -= dexterityLevelAddition;
            playerState.intelligenceLevel -= intelligenceLevelAddition;
            playerState.faithLevel -= faithLevelAddition;
            playerState.soulsRewardLevel -= soulsRewardLevelAddition;

            //更新基础属性:
            playerState.SetMaxHealthFromHealthLevel();
            playerState.SetMaxStaminaFromLevel();
            playerState.SetMaxFocusPointsFormLevel();

            //更新临时抗性:
            playerState.temporaryPhysicalDamageAbsorption -= temporaryPhysicalDamageAbsorption;
            playerState.temporaryFireDamageAbsorption -= temporaryFireDamageAbsorption;
            playerState.temporaryMagicDamageAbsorption -= temporaryMagicDamageAbsorption;
            playerState.temporaryLightningDamageAbsorption -= temporaryLightningDamageAbsorption;
            playerState.temporaryDarkDamageAbsorption -= temporaryDarkDamageAbsorption;
            //更新临时抵抗力:
            playerState.temporaryPoisonDefenseAbsorption -= temporaryPoisonDefenseAbsorption;
            playerState.temporaryFrostDamageAbsorption -= temporaryFrostDamageAbsorption;
            playerState.temporaryHemorrhageDefenseAbsorption -= temporaryHemorrhageDefenseAbsorption;
            playerState.temporaryCurseDefenseAbsorption -= temporaryCurseDefenseAbsorption;

            //计算记忆空格:
            player.playerInventoryManager.CountMemorySpellSlotNumber();
        }
    }
}