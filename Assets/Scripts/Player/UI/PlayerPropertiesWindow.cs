using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class PlayerPropertiesWindow : MonoBehaviour
    {
        [Header("����ȼ��ı�:")]
        public Text playerLevelText;
        [Header("�������Եȼ��ı�:")]
        public Text healthLevelText;             //��������
        public Text staminaLevelText;          //��������
        public Text focusLevelText;              //��������
        public Text strengthLevelText;         //��������
        public Text dexterityLevelText;        //���ݼ���
        public Text intelligenceLevelText;    //��������
        public Text faithLevelText;               //��������
        public Text soulsRewardLevelText;  //��꽱������

        [Header("����������ֵ�ı�:")]
        public Text maxHealthText;
        public Text currentHealthText;
        public Text maxStaminaText;              //�������ֵ
        public Text currentStaminaText;         //��ǰ����ֵ
        public Text maxFocusPointsText;       //�����ֵ
        public Text currentFocusPointsText;  //��ǰ����ֵ

        public void UpdatePropertiesText(CharacterStatsManager characterStats)
        {
            //��ҵȼ�:
            int playerLevel = 0;
            //���Եȼ�:
            int healthLevel = 0;
            int staminaLevel = 0;
            int focusLevel = 0;
            int strengthLevel = 0;
            int dexterityLevel = 0;
            int intelligenceLevel = 0;
            int faithLevel = 0;
            int soulsRewardLevel = 0;
            //��������:
            int maxHealth = 0;
            int currentHealth = 0;
            float maxStamina = 0;
            float currentStamina = 0;
            float maxFocusPoints = 0;
            float currentFocusPoints = 0;

            if (characterStats != null)
            {
                playerLevel = characterStats.characterLeve;
                //���Եȼ�:
                healthLevel = characterStats.healthLevel;
                staminaLevel = characterStats.staminaLevel;
                focusLevel = characterStats.focusLevel;
                strengthLevel = characterStats.strengthLevel;
                dexterityLevel = characterStats.dexterityLevel;
                intelligenceLevel = characterStats.intelligenceLevel;
                faithLevel = characterStats.faithLevel;
                soulsRewardLevel = (int)(characterStats.soulsRewardLevel * 10);
                //��������:
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

            healthLevelText.color = Color.white;             //��������
            staminaLevelText.color = Color.white;          //��������
            focusLevelText.color = Color.white;              //��������
            strengthLevelText.color = Color.white;         //��������
            dexterityLevelText.color = Color.white;        //���ݼ���
            intelligenceLevelText.color = Color.white;    //��������
            faithLevelText.color = Color.white;               //��������
            soulsRewardLevelText.color = Color.white;  //��꽱������

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
            //���Եȼ�:
            int healthLevel = characterStats.healthLevel;
            int staminaLevel = characterStats.staminaLevel;
            int focusLevel = characterStats.focusLevel;
            int strengthLevel = characterStats.strengthLevel;
            int dexterityLevel = characterStats.dexterityLevel;
            int intelligenceLevel = characterStats.intelligenceLevel;
            int faithLevel = characterStats.faithLevel;
            int soulsRewardLevel = (int)(characterStats.soulsRewardLevel * 10);
            //��������:
            int maxHealth = characterStats.maxHealth;
            int maxStamina = Mathf.RoundToInt(characterStats.maxStamina);
            int maxFocusPoints = Mathf.RoundToInt(characterStats.maxFocusPoints);

            if (ring != null)
            {
                //�ȼ�����:
                healthLevel += ring.healthLevelAddition;
                staminaLevel += ring.staminaLevelAddition;
                focusLevel += ring.focusLevelAddition;
                strengthLevel += ring.strengthLevelAddition;
                dexterityLevel += ring.dexterityLevelAddition;
                intelligenceLevel += ring.intelligenceLevelAddition;
                faithLevel += ring.faithLevelAddition;
                soulsRewardLevel += ring.soulsRewardLevelAddition * 10;

                //��������:
                maxHealth += ring.maxHealthAddition;
                maxStamina += (int)ring.maxStaminaAddition;
                maxFocusPoints += (int)ring.maxFocusPointsAddition;

                #region ���Եȼ���ɫ
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
                #region ���������ı���ɫ:
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

                healthLevelText.color = Color.white;             //��������
                staminaLevelText.color = Color.white;          //��������
                focusLevelText.color = Color.white;              //��������
                strengthLevelText.color = Color.white;         //��������
                dexterityLevelText.color = Color.white;        //���ݼ���
                intelligenceLevelText.color = Color.white;    //��������
                faithLevelText.color = Color.white;               //��������
                soulsRewardLevelText.color = Color.white;  //��꽱������
            }

            //���Եȼ�:
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