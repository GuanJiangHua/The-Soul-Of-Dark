using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class SpellItem : Item
    {
        public GameObject spellWarmUpFX;    //法术读条效果
        public GameObject spellCastFX;           //施法成功效果

        public string spellAnimation;               //施法动画
        [Header("占据的法术格:")]
        public int occupiedSpellGrid = 1;
        [Header("法术消耗:")]
        public int focusPointCost;            //法术消耗

        [Header("法术类型:")]
        public bool isFaithSpell;               //奇迹法术
        public bool isMagicSpell;             //魔法法术
        public bool isPyroSpell;               //咒术法术
        [Header("等级要求:")]
        public int requiredIntelligenceLevel;   //必需的智力
        public int requiredFaithLevel;             //必须的信仰

        [Header("法术描述:")]
        [TextArea]
        public string spellDescription;     //法术描述 
        [Header("功能描述:")]
        [TextArea]
        public string functionDescription = "使用施法武器时必须装备对应类型的咒语。";

        //尝试施法:
        public virtual void AttemptToCastSpell(AnimatorManager animatorHandler, CharacterStatsManager playerState , PlayerWeaponSlotManger weaponSlotManger , bool isLeftHanded)
        {
            
        }

        public virtual void SucessfullyCastSpell(AnimatorManager animatorHandler, CharacterStatsManager playerState, PlayerWeaponSlotManger weaponSlotManger , CameraHandler cameraHandler , bool isLeftHanded)
        {
            //损耗法力值:
            playerState.TakeFocusPoints(focusPointCost);
        }

        //伤害加成:
        protected int DamageCalculation(int damage, WeaponItem weapon, CharacterStatsManager playerState)
        {
            //等级:
            int intelligence = playerState.intelligenceLevel;
            int faith = playerState.faithLevel;
            //加成:
            float intelligenceAddition = weapon.intelligenceAddition;
            float faithAddition = weapon.faithAddition;

            float finalDamage = damage + damage * (intelligenceAddition * intelligence / 20);
            finalDamage += damage * (faithAddition * faith / 20);

            return Mathf.RoundToInt(finalDamage);
        }
    }
}
