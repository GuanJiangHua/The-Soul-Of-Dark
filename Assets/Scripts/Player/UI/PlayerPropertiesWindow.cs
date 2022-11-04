using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class PlayerPropertiesWindow : MonoBehaviour
    {
        [Header("人物等级文本:")]
        public Text playerLevelText;
        [Header("人物属性等级文本:")]
        public Text healthLevelText;             //健康级别
        public Text staminaLevelText;          //体力级别
        public Text focusLevelText;              //法力级别
        public Text strengthLevelText;         //力量级别
        public Text dexterityLevelText;        //敏捷级别
        public Text intelligenceLevelText;    //智力级别
        public Text faithLevelText;               //信仰级别
        public Text soulsRewardLevelText;  //灵魂奖励级别

        [Header("级别属性数值文本:")]
        public Text maxHealthText;
        public Text currentHealthText;
        public Text maxStaminaText;              //最大耐力值
        public Text currentStaminaText;         //当前耐力值
        public Text maxFocusPointsText;       //最大法力值
        public Text currentFocusPointsText;  //当前法力值

        public void UpdatePropertiesText(CharacterStatsManager characterStats)
        {
            //玩家等级:
            int playerLevel = 0;
            //属性等级:
            int healthLevel = 0;
            int staminaLevel = 0;
            int focusLevel = 0;
            int strengthLevel = 0;
            int dexterityLevel = 0;
            int intelligenceLevel = 0;
            int faithLevel = 0;
            int soulsRewardLevel = 0;
            //基础属性:
            int maxHealth = 0;
            int currentHealth = 0;
            float maxStamina = 0;
            float currentStamina = 0;
            float maxFocusPoints = 0;
            float currentFocusPoints = 0;

            if (characterStats != null)
            {
                playerLevel = characterStats.characterLeve;
                //属性等级:
                healthLevel = characterStats.healthLevel;
                staminaLevel = characterStats.staminaLevel;
                focusLevel = characterStats.focusLevel;
                strengthLevel = characterStats.strengthLevel;
                dexterityLevel = characterStats.dexterityLevel;
                intelligenceLevel = characterStats.intelligenceLevel;
                faithLevel = characterStats.faithLevel;
                soulsRewardLevel = (int)(characterStats.soulsRewardLevel * 10);
                //基础属性:
                maxHealth = characterStats.maxHealth;
                currentHealth = characterStats.currentHealth;
                maxStamina = characterStats.maxStamina;
                currentStamina = characterStats.currentStamina;
                maxFocusPoints = characterStats.maxFocusPoints;
                currentFocusPoints = characterStats.currentFocusPoints;
            }

            maxHealthText.color = Color.white;
            maxStaminaText.color = Color.white;
            maxFocusPointsText.color = Color.white;

            healthLevelText.color = Color.white;             //健康级别
            staminaLevelText.color = Color.white;          //体力级别
            focusLevelText.color = Color.white;              //法力级别
            strengthLevelText.color = Color.white;         //力量级别
            dexterityLevelText.color = Color.white;        //敏捷级别
            intelligenceLevelText.color = Color.white;    //智力级别
            faithLevelText.color = Color.white;               //信仰级别
            soulsRewardLevelText.color = Color.white;  //灵魂奖励级别

            playerLevelText.text = playerLevel.ToString("d3");
            healthLevelText.text = healthLevel.ToString("d3");
            staminaLevelText.text = staminaLevel.ToString("d3");
            focusLevelText.text = focusLevel.ToString("d3");
            strengthLevelText.text = strengthLevel.ToString("d3");
            dexterityLevelText.text = dexterityLevel.ToString("d3");
            intelligenceLevelText.text = intelligenceLevel.ToString("d3");
            faithLevelText.text = faithLevel.ToString("d3");
            soulsRewardLevelText.text = soulsRewardLevel.ToString("d3");

            maxHealthText.text = maxHealth.ToString("d3");
            currentHealthText.text = currentHealth.ToString("d3");
            maxStaminaText.text = Mathf.RoundToInt(maxStamina).ToString("d3");
            currentStaminaText.text = Mathf.RoundToInt(currentStamina).ToString("d3");
            maxFocusPointsText.text = Mathf.RoundToInt(maxFocusPoints).ToString("d3");
            currentFocusPointsText.text = Mathf.RoundToInt(currentFocusPoints).ToString("d3");
        }

        public void UpdatePropertiesText(CharacterStatsManager characterStats , RingItem ring)
        {
            //属性等级:
            int healthLevel = characterStats.healthLevel;
            int staminaLevel = characterStats.staminaLevel;
            int focusLevel = characterStats.focusLevel;
            int strengthLevel = characterStats.strengthLevel;
            int dexterityLevel = characterStats.dexterityLevel;
            int intelligenceLevel = characterStats.intelligenceLevel;
            int faithLevel = characterStats.faithLevel;
            int soulsRewardLevel = (int)(characterStats.soulsRewardLevel * 10);
            //基础属性:
            int maxHealth = characterStats.maxHealth;
            int maxStamina = Mathf.RoundToInt(characterStats.maxStamina);
            int maxFocusPoints = Mathf.RoundToInt(characterStats.maxFocusPoints);

            if (ring != null)
            {
                //等级属性:
                healthLevel += ring.healthLevelAddition;
                staminaLevel += ring.staminaLevelAddition;
                focusLevel += ring.focusLevelAddition;
                strengthLevel += ring.strengthLevelAddition;
                dexterityLevel += ring.dexterityLevelAddition;
                intelligenceLevel += ring.intelligenceLevelAddition;
                faithLevel += ring.faithLevelAddition;
                soulsRewardLevel += ring.soulsRewardLevelAddition * 10;

                //基础属性:
                maxHealth += ring.maxHealthAddition;
                maxStamina += (int)ring.maxStaminaAddition;
                maxFocusPoints += (int)ring.maxFocusPointsAddition;

                #region 属性等级颜色
                if(ring.healthLevelAddition == 0)
                {
                    healthLevelText.color = Color.white;
                }
                else if(ring.healthLevelAddition < 0)
                {
                    healthLevelText.color = Color.red;
                }
                else if(ring.healthLevelAddition > 0)
                {
                    healthLevelText.color = Color.blue;
                }

                if (ring.staminaLevelAddition == 0)
                {
                    staminaLevelText.color = Color.white;
                }
                else if (ring.staminaLevelAddition < 0)
                {
                    staminaLevelText.color = Color.red;
                }
                else if (ring.staminaLevelAddition > 0)
                {
                    staminaLevelText.color = Color.blue;
                }

                if (ring.focusLevelAddition == 0)
                {
                    focusLevelText.color = Color.white;
                }
                else if (ring.focusLevelAddition < 0)
                {
                    focusLevelText.color = Color.red;
                }
                else if (ring.focusLevelAddition > 0)
                {
                    focusLevelText.color = Color.blue;
                }

                if (ring.strengthLevelAddition == 0)
                {
                    strengthLevelText.color = Color.white;
                }
                else if (ring.strengthLevelAddition < 0)
                {
                    strengthLevelText.color = Color.red;
                }
                else if (ring.strengthLevelAddition > 0)
                {
                    strengthLevelText.color = Color.blue;
                }

                if (ring.dexterityLevelAddition == 0)
                {
                    dexterityLevelText.color = Color.white;
                }
                else if (ring.dexterityLevelAddition < 0)
                {
                    dexterityLevelText.color = Color.red;
                }
                else if (ring.dexterityLevelAddition > 0)
                {
                    dexterityLevelText.color = Color.blue;
                }

                if (ring.intelligenceLevelAddition == 0)
                {
                    intelligenceLevelText.color = Color.white;
                }
                else if (ring.intelligenceLevelAddition < 0)
                {
                    intelligenceLevelText.color = Color.red;
                }
                else if (ring.intelligenceLevelAddition > 0)
                {
                    intelligenceLevelText.color = Color.blue;
                }

                if (ring.faithLevelAddition == 0)
                {
                    faithLevelText.color = Color.white;
                }
                else if (ring.faithLevelAddition < 0)
                {
                    faithLevelText.color = Color.red;
                }
                else if (ring.faithLevelAddition > 0)
                {
                    faithLevelText.color = Color.blue;
                }

                if (ring.soulsRewardLevelAddition == 0)
                {
                    soulsRewardLevelText.color = Color.white;
                }
                else if (ring.soulsRewardLevelAddition < 0)
                {
                    soulsRewardLevelText.color = Color.red;
                }
                else if (ring.soulsRewardLevelAddition > 0)
                {
                    soulsRewardLevelText.color = Color.blue;
                }
                #endregion
                #region 基础属性文本颜色:
                if (ring.maxHealthAddition == 0)
                {
                    maxHealthText.color = Color.white;
                }
                else if(ring.maxHealthAddition > 0)
                {
                    maxHealthText.color = Color.blue;
                }
                else if (ring.maxHealthAddition < 0)
                {
                    maxStaminaText.color = Color.red;
                }

                if (ring.maxStaminaAddition == 0)
                {
                    maxStaminaText.color = Color.white;
                }
                else if (ring.maxStaminaAddition > 0)
                {
                    maxStaminaText.color = Color.blue;
                }
                else if (ring.maxStaminaAddition < 0)
                {
                    maxStaminaText.color = Color.red;
                }

                if (ring.maxFocusPointsAddition == 0)
                {
                    maxFocusPointsText.color = Color.white;
                }
                else if (ring.maxFocusPointsAddition > 0)
                {
                    maxFocusPointsText.color = Color.blue;
                }
                else if (ring.maxFocusPointsAddition < 0)
                {
                    maxFocusPointsText.color = Color.red;
                }
                #endregion
            }
            else
            {
                maxHealthText.color = Color.white;
                maxStaminaText.color = Color.white;
                maxFocusPointsText.color = Color.white;

                healthLevelText.color = Color.white;             //健康级别
                staminaLevelText.color = Color.white;          //体力级别
                focusLevelText.color = Color.white;              //法力级别
                strengthLevelText.color = Color.white;         //力量级别
                dexterityLevelText.color = Color.white;        //敏捷级别
                intelligenceLevelText.color = Color.white;    //智力级别
                faithLevelText.color = Color.white;               //信仰级别
                soulsRewardLevelText.color = Color.white;  //灵魂奖励级别
            }

            //属性等级:
            healthLevelText.text = healthLevel.ToString("d3");
            staminaLevelText.text = staminaLevel.ToString("d3");
            focusLevelText.text = focusLevel.ToString("d3");
            strengthLevelText.text =strengthLevel.ToString("d3");
            dexterityLevelText.text = dexterityLevel.ToString("d3");
            intelligenceLevelText.text =  intelligenceLevel.ToString("d3");
            faithLevelText.text = faithLevel.ToString("d3");
            soulsRewardLevelText.text = soulsRewardLevel.ToString("d3");

            maxHealthText.text = maxHealth.ToString("d3");
            maxStaminaText.text = maxStamina.ToString("d3");
            maxFocusPointsText.text = maxFocusPoints.ToString("d3");
        }
    }
}