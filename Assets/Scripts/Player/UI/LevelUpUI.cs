using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class LevelUpUI : MonoBehaviour
    {
        UIManager uiManager;
        public PlayerManager playerManager;
        [Header("����ȼ�:")]
        public int currentPlayerLevel;
        public int projectedPlayerLevel;
        public Text currentPlayerLevelText;
        public Text projectedPlayerLevelText;
        [Header("�������:")]
        public int soulsRequiredToLevelUp = 0;           //����������������;
        public int baseLevelUpCost = 5;                       //���������ɱ�;
        public Text currentSoulsText;                           //��ǰ�������������ʾ�ı�;
        public Text soulsRequiredToLevelUpText;       //�������������ʾ�ı�;

        #region �������Եȼ�����ʾ�ı��ͻ�����:
        [Header("������:")]
        public Slider healthSlider;                 //����������;
        public Text currentHealthLevel;        //�������ȼ�;
        [Header("������:")]
        public Slider focusSlider;                  //����������
        public Text currentFocusLevel;         //�������ȼ�
        [Header("����:")]
        public Slider staminaSlider;              //����������;
        public Text currentStaminaLevel;     //�����ȼ�;
        [Header("����:")]
        public Slider strengthSlider;
        public Text currentStrengthLevel;    //��ǰ�����ȼ�;d
        [Header("����:")]
        public Slider dexteritySlider;
        public Text currentDexterityLevel;    //��ǰ���ݵȼ�;Faith
        [Header("����:")]
        public Slider intelligenceSlider;
        public Text currentIntelligenceLevel;//��ǰ�����ȼ�;
        [Header("����:")]
        public Slider faithSlider;
        public Text currentFaithLevel;          //��ǰ�����ȼ�;Intelligence
        #endregion
        [Header("-------------------------��������-------------------------")]
        [Header("����ֵ:")]
        public Text currentHealtValueText;
        public Text projectedHealthValueText;
        [Header("����ֵ:")]
        public Text currentFocusValueText;
        public Text projectedFocusValueText;
        [Header("����ֵ:")]
        public Text currentStaminaValueText;
        public Text projectedStaminaValueText;
        [Header("���ֹ�����:")]
        public Text currentRightHandAggressivityText01;
        public Text currentRightHandAggressivityText02;
        public Text currentRightHandAggressivityText03;
        public Text projectedRightHandAggressivityText01;
        public Text projectedRightHandAggressivityText02;
        public Text projectedRightHandAggressivityText03;
        [Header("���ֹ�����:")]
        public Text currentLeftHandAggressivityText01;
        public Text currentLeftHandAggressivityText02;
        public Text currentLeftHandAggressivityText03;
        public Text projectedLeftHandAggressivityText01;
        public Text projectedLeftHandAggressivityText02;
        public Text projectedLeftHandAggressivityText03;
        [Header("��פ�����ı�:")]
        public Text currentPhysicalDamageAbsorptionText;
        public Text currentFireDamageAbsorptionText;
        public Text currentMagicDamageAbsorptionText;
        public Text currentLightningDamageAbsorptionText;
        public Text currentDarkDamageAbsorptionText;
        public Text projectedPhysicalDamageAbsorptionText;
        public Text projectedFireDamageAbsorptionText;
        public Text projectedMagicDamageAbsorptionText;
        public Text projectedLightningDamageAbsorptionText;
        public Text projectedDarkDamageAbsorptionText;
        [Header("��פ�쳣���Եֿ���:")]
        public Text currentPoisonDefenseAbsorptionText;
        public Text currentHemorrhageDefenseAbsorptionText;
        public Text currentFrostDamageAbsorptionText;
        public Text currentCurseDefenseAbsorptionText;
        public Text projectedPoisonDefenseAbsorptionText;
        public Text projectedHemorrhageDefenseAbsorptionText;
        public Text projectedFrostDamageAbsorptionText;
        public Text projectedCurseDefenseAbsorptionText;

        [Header("������ť:")]
        public Button confirmLevelUpButton;

        //���Ե�Ԥ�ڵȼ�:
        int projectedHealtLevel = 0;            //������Ԥ��Ԥ��
        int projectedFocusLevel = 0;           //������Ԥ��Ԥ��
        int projectedStaminaLevel = 0;       //����Ԥ�ڵȼ�
        int projectedStrengthLevel = 0;      //����Ԥ�ڵȼ�
        int projectedDexterityLevel = 0;     //����Ԥ�ڵȼ�
        int projectedIntelligenceLevel = 0; //����Ԥ�ڵȼ�
        int projectedFaithLevel = 0;            //����Ԥ�ڵȼ�

        //����������Ե�Ԥ������:(�������Եȼ�����������ľ������ݵ�Ԥ��ֵ:��"����ֵ")
        int projectedHealthValue = 0;
        int projectedFocusValue = 0;
        int projectedStaminaValue = 0;

        private void Awake()
        {
            uiManager = FindObjectOfType<UIManager>();
        }
        private void OnEnable()
        {
            currentPlayerLevel = playerManager.playerStateManager.characterLeve;
            projectedPlayerLevel = playerManager.playerStateManager.characterLeve;

            currentPlayerLevelText.text = currentPlayerLevel.ToString("d2");
            projectedPlayerLevelText.text = projectedPlayerLevel.ToString("d2");
            projectedPlayerLevelText.color = Color.white;

            //��ҵȼ����Եĳ�ʼ��:
            #region �ȼ����Գ�ʼ��:
            healthSlider.minValue = 0;
            healthSlider.maxValue = 99;
            healthSlider.value = playerManager.playerStateManager.healthLevel;
            projectedHealtLevel = playerManager.playerStateManager.healthLevel;   //����Ԥ�ڵȼ�
            currentHealthLevel.text = playerManager.playerStateManager.healthLevel.ToString("d2");
            currentHealthLevel.color = Color.white;

            focusSlider.minValue = 0;
            focusSlider.maxValue = 99;
            focusSlider.value = playerManager.playerStateManager.focusLevel;
            projectedFocusLevel = playerManager.playerStateManager.focusLevel;
            currentFocusLevel.text = playerManager.playerStateManager.focusLevel.ToString("d2");
            currentFocusLevel.color = Color.white;

            staminaSlider.minValue = 0;
            staminaSlider.maxValue = 99;
            staminaSlider.value = playerManager.playerStateManager.staminaLevel;
            projectedStaminaLevel = playerManager.playerStateManager.staminaLevel;
            currentStaminaLevel.text = playerManager.playerStateManager.staminaLevel.ToString("d2");
            currentStaminaLevel.color = Color.white;

            strengthSlider.minValue = 0;
            strengthSlider.maxValue = 99;
            strengthSlider.value = playerManager.playerStateManager.strengthLevel;
            projectedStrengthLevel = playerManager.playerStateManager.strengthLevel;
            currentStrengthLevel.text = playerManager.playerStateManager.strengthLevel.ToString("d2");
            currentStrengthLevel.color = Color.white;

            dexteritySlider.minValue = 0;
            dexteritySlider.maxValue = 99;
            dexteritySlider.value = playerManager.playerStateManager.dexterityLevel;
            projectedDexterityLevel = playerManager.playerStateManager.dexterityLevel;
            currentDexterityLevel.text = playerManager.playerStateManager.dexterityLevel.ToString("d2");
            currentDexterityLevel.color = Color.white;

            intelligenceSlider.minValue = 0;
            intelligenceSlider.maxValue = 99;
            intelligenceSlider.value = playerManager.playerStateManager.intelligenceLevel;
            projectedIntelligenceLevel = playerManager.playerStateManager.intelligenceLevel;
            currentIntelligenceLevel.text = playerManager.playerStateManager.intelligenceLevel.ToString("d2");
            currentIntelligenceLevel.color = Color.white;

            faithSlider.minValue = 0;
            faithSlider.maxValue = 99;
            faithSlider.value = playerManager.playerStateManager.faithLevel;
            projectedFaithLevel = playerManager.playerStateManager.faithLevel;
            currentFaithLevel.text = playerManager.playerStateManager.faithLevel.ToString("d2");
            currentFaithLevel.color = Color.white;
            #endregion

            //��Ҿ������Եĳ�ʼ��:(������ֵ,����ֵ��)
            currentHealtValueText.text = playerManager.playerStateManager.maxHealth.ToString("d2");
            projectedHealthValue = playerManager.playerStateManager.maxHealth;
            projectedHealthValueText.text = projectedHealthValue.ToString("d2");
            projectedHealthValueText.color = Color.white;

            currentFocusValueText.text = Mathf.RoundToInt(playerManager.playerStateManager.maxFocusPoints).ToString("d2");
            projectedFocusValue = Mathf.RoundToInt(playerManager.playerStateManager.maxFocusPoints);
            projectedFocusValueText.text = projectedFocusValue.ToString("d2");
            projectedFocusValueText.color = Color.white;

            currentStaminaValueText.text = Mathf.RoundToInt(playerManager.playerStateManager.maxStamina).ToString("d2");
            projectedStaminaValue = Mathf.RoundToInt(playerManager.playerStateManager.maxStamina);
            projectedStaminaValueText.text = projectedStaminaValue.ToString("d2");
            projectedStaminaValueText.color = Color.white;

            WeaponItem[] rightWeapons = playerManager.playerInventoryManager.WeaponRightHandSlots;
            WeaponItem[] leftWeapons = playerManager.playerInventoryManager.WeaponLeftHandSlots;
            //���µ�ǰ����������:
            UpdateCurrentWeaponAggressivity(currentRightHandAggressivityText01, rightWeapons[0]);
            UpdateCurrentWeaponAggressivity(currentRightHandAggressivityText02, rightWeapons[1]);
            UpdateCurrentWeaponAggressivity(currentRightHandAggressivityText03, rightWeapons[2]);

            UpdateCurrentWeaponAggressivity(currentLeftHandAggressivityText01, leftWeapons[0]);
            UpdateCurrentWeaponAggressivity(currentLeftHandAggressivityText02, leftWeapons[1]);
            UpdateCurrentWeaponAggressivity(currentLeftHandAggressivityText03, leftWeapons[2]);
            //����Ԥ������������:
            UpdateProjectedWeaponAggressivity(projectedRightHandAggressivityText01, rightWeapons[0]);
            UpdateProjectedWeaponAggressivity(projectedRightHandAggressivityText02, rightWeapons[1]);
            UpdateProjectedWeaponAggressivity(projectedRightHandAggressivityText03, rightWeapons[2]);

            UpdateProjectedWeaponAggressivity(projectedLeftHandAggressivityText01, leftWeapons[0]);
            UpdateProjectedWeaponAggressivity(projectedLeftHandAggressivityText02, leftWeapons[1]);
            UpdateProjectedWeaponAggressivity(projectedLeftHandAggressivityText03, leftWeapons[2]);

            projectedRightHandAggressivityText01.color = Color.white;
            projectedRightHandAggressivityText02.color = Color.white;
            projectedRightHandAggressivityText03.color = Color.white;
            projectedLeftHandAggressivityText01.color = Color.white;
            projectedLeftHandAggressivityText02.color = Color.white;
            projectedLeftHandAggressivityText03.color = Color.white;
        }

        //����Ԥ�ڵȼ�����ʾ�ı�����:
        private void UpdateProjectedPlayerLevel()
        {
            int beyondLevel = 0;
            soulsRequiredToLevelUp = 0;

            beyondLevel += Mathf.RoundToInt(projectedHealtLevel - playerManager.playerStateManager.healthLevel);
            beyondLevel += Mathf.RoundToInt(projectedFocusLevel - playerManager.playerStateManager.focusLevel);
            beyondLevel += Mathf.RoundToInt(projectedStaminaLevel - playerManager.playerStateManager.staminaLevel);
            beyondLevel += Mathf.RoundToInt(projectedStrengthLevel - playerManager.playerStateManager.strengthLevel);
            beyondLevel += Mathf.RoundToInt(projectedDexterityLevel - playerManager.playerStateManager.dexterityLevel);
            beyondLevel += Mathf.RoundToInt(projectedIntelligenceLevel - playerManager.playerStateManager.intelligenceLevel);
            beyondLevel += Mathf.RoundToInt(projectedFaithLevel - playerManager.playerStateManager.faithLevel);

            projectedPlayerLevel = playerManager.playerStateManager.characterLeve + beyondLevel;  //��ɫ�ȼ�+���Եĳ����ȼ�

            projectedPlayerLevelText.text = projectedPlayerLevel.ToString("d2");
            if (projectedPlayerLevel > playerManager.playerStateManager.characterLeve)
            {
                projectedPlayerLevelText.color = Color.blue;
            }

            CalculateSoulCostToLeveUp();

            //(�������������������������꣬�����"������ť")
            if (playerManager.playerStateManager.currentSoulCount < soulsRequiredToLevelUp)
            {
                confirmLevelUpButton.interactable = false;
            }
            else
            {
                confirmLevelUpButton.interactable = true;
            }
        }

        //ȷ�������������:(��ť�¼�)
        public void ConfirmPlayerLevelUpStats()
        {
            playerManager.playerStateManager.characterLeve = projectedPlayerLevel;
            playerManager.playerStateManager.healthLevel = projectedHealtLevel;
            playerManager.playerStateManager.focusLevel = projectedFocusLevel;
            playerManager.playerStateManager.staminaLevel = projectedStaminaLevel;
            playerManager.playerStateManager.strengthLevel = projectedStrengthLevel;
            playerManager.playerStateManager.dexterityLevel = projectedDexterityLevel;
            playerManager.playerStateManager.intelligenceLevel = projectedIntelligenceLevel;
            playerManager.playerStateManager.faithLevel = projectedFaithLevel;

            playerManager.playerStateManager.maxHealth = playerManager.playerStateManager.SetMaxHealthFromHealthLevel();
            playerManager.playerStateManager.currentHealth = playerManager.playerStateManager.maxHealth;
            playerManager.playerStateManager.healthBar.SetMaxHealth(playerManager.playerStateManager.maxHealth);

            playerManager.playerStateManager.maxStamina = playerManager.playerStateManager.SetMaxStaminaFromLevel();
            playerManager.playerStateManager.currentStamina = playerManager.playerStateManager.maxStamina;
            playerManager.playerStateManager.staminaBar.SetMaxStamina(playerManager.playerStateManager.maxStamina);

            playerManager.playerStateManager.maxFocusPoints = playerManager.playerStateManager.SetMaxFocusPointsFormLevel();
            playerManager.playerStateManager.currentFocusPoints = playerManager.playerStateManager.maxFocusPoints;
            playerManager.playerStateManager.focusPointBar.SetMaxFocusPoint(playerManager.playerStateManager.maxFocusPoints);

            playerManager.playerStateManager.currentSoulCount -= soulsRequiredToLevelUp;

            //����һ�鿹��:
            playerManager.playerStateManager.SetResistance();
            //�������ո�:
            playerManager.playerInventoryManager.CountMemorySpellSlotNumber();

            gameObject.SetActive(false);
            playerManager.playerAnimatorManager.PlayTargetAnimation("EndPhase", true);
        }

        //���������ɱ�:
        private void CalculateSoulCostToLeveUp()
        {
            for (int i = 0; i < projectedPlayerLevel; i++)
            {
                soulsRequiredToLevelUp = soulsRequiredToLevelUp + Mathf.RoundToInt((projectedPlayerLevel * baseLevelUpCost) * 1.5f);
            }

            currentSoulsText.text = playerManager.playerStateManager.currentSoulCount.ToString("d2");
            soulsRequiredToLevelUpText.text = soulsRequiredToLevelUp.ToString("d2");
        }

        //(��ʾ�����Եȼ��ı���ΪԤ�ڵȼ���������ʵ�������Եȼ�)
        //����Ԥ��"������"�ȼ�����ʾ�ı�����:[Ӱ������������ֵ]
        public void UpdateHealthLevel()
        {
            projectedHealtLevel += 1;
            if (projectedHealtLevel > 99)
            {
                projectedHealtLevel = 99;
            }

            healthSlider.value = projectedHealtLevel;
            currentHealthLevel.text = projectedHealtLevel.ToString("d2");
            if (projectedHealtLevel > playerManager.playerStateManager.healthLevel)
            {
                currentHealthLevel.color = Color.blue;
            }

            projectedHealthValue = projectedHealtLevel * 10;        //����Ԥ������ֵ;
            projectedHealthValueText.text = projectedHealthValue.ToString("d2");
            if (projectedHealthValue > playerManager.playerStateManager.maxHealth)
            {
                projectedHealthValueText.color = Color.blue;
            }

            UpdateProjectedPlayerLevel();   //����Ԥ�ڵȼ�;
        }

        //����Ԥ��"������"�ȼ�����ʾ�ı�����:(��ʾ�����Եȼ��ı���ΪԤ�ڵȼ���������ʵ�������Եȼ�)
        public void UpdateFocusLevel()
        {
            projectedFocusLevel += 1;
            if (projectedFocusLevel > 99)
            {
                projectedFocusLevel = 99;
            }

            focusSlider.value = projectedFocusLevel;
            currentFocusLevel.text = projectedFocusLevel.ToString("d2");
            if (projectedFocusLevel > playerManager.playerStateManager.focusLevel)
            {
                currentFocusLevel.color = Color.blue;
            }

            projectedFocusValue = projectedFocusLevel * 10;        //����Ԥ�ڷ���ֵ;
            projectedFocusValueText.text = projectedFocusValue.ToString("d2");
            if (projectedFocusValue > playerManager.playerStateManager.maxFocusPoints)
            {
                projectedFocusValueText.color = Color.blue;
            }

            UpdateProjectedPlayerLevel();   //����Ԥ�ڵȼ�;
        }

        //����Ԥ��"����"�ȼ�����ʾ�ı�����:(��ʾ�����Եȼ��ı���ΪԤ�ڵȼ���������ʵ�������Եȼ�)
        public void UpdateStaminaLevel()
        {
            projectedStaminaLevel += 1;
            if (projectedStaminaLevel > 99)
            {
                projectedStaminaLevel = 99;
            }

            staminaSlider.value = projectedStaminaLevel;
            currentStaminaLevel.text = projectedStaminaLevel.ToString("d2");
            if (projectedStaminaLevel > playerManager.playerStateManager.staminaLevel)
            {
                currentStaminaLevel.color = Color.blue;
            }

            projectedStaminaValue = projectedStaminaLevel * 10;        //����Ԥ������ֵ;
            projectedStaminaValueText.text = projectedStaminaValue.ToString("d2");
            if (projectedStaminaValue > playerManager.playerStateManager.maxStamina)
            {
                projectedStaminaValueText.color = Color.blue;
            }

            UpdateProjectedPlayerLevel();   //����Ԥ�ڵȼ�;
        }

        //����Ԥ��"����"�ȼ�����ʾ�ı�����:(��ʾ�����Եȼ��ı���ΪԤ�ڵȼ���������ʵ�������Եȼ�)
        public void UpdateStrengthLevel()
        {
            projectedStrengthLevel += 1;
            if (projectedStrengthLevel > 99)
            {
                projectedStrengthLevel = 99;
            }

            strengthSlider.value = projectedStrengthLevel;
            currentStrengthLevel.text = projectedStrengthLevel.ToString("d2");

            if (projectedStrengthLevel > playerManager.playerStateManager.strengthLevel)
            {
                currentStrengthLevel.color = Color.blue;
            }

            UpdateProjectedPlayerLevel();   //����Ԥ�ڵȼ�;
            WeaponItem[] rightWeapons = playerManager.playerInventoryManager.WeaponRightHandSlots;
            WeaponItem[] leftWeapons = playerManager.playerInventoryManager.WeaponLeftHandSlots;
            //���µ�ǰ����������:
            UpdateCurrentWeaponAggressivity(currentRightHandAggressivityText01, rightWeapons[0]);
            UpdateCurrentWeaponAggressivity(currentRightHandAggressivityText02, rightWeapons[1]);
            UpdateCurrentWeaponAggressivity(currentRightHandAggressivityText03, rightWeapons[2]);

            UpdateCurrentWeaponAggressivity(currentLeftHandAggressivityText01, leftWeapons[0]);
            UpdateCurrentWeaponAggressivity(currentLeftHandAggressivityText02, leftWeapons[1]);
            UpdateCurrentWeaponAggressivity(currentLeftHandAggressivityText03, leftWeapons[2]);
            //����Ԥ������������:
            UpdateProjectedWeaponAggressivity(projectedRightHandAggressivityText01, rightWeapons[0]);
            UpdateProjectedWeaponAggressivity(projectedRightHandAggressivityText02, rightWeapons[1]);
            UpdateProjectedWeaponAggressivity(projectedRightHandAggressivityText03, rightWeapons[2]);

            UpdateProjectedWeaponAggressivity(projectedLeftHandAggressivityText01, leftWeapons[0]);
            UpdateProjectedWeaponAggressivity(projectedLeftHandAggressivityText02, leftWeapons[1]);
            UpdateProjectedWeaponAggressivity(projectedLeftHandAggressivityText03, leftWeapons[2]);

            //���¿��Ժ͵ֿ����ı�:
            UpdateDefenseText();
        }

        //����Ԥ��"����"�ȼ�����ʾ�ı�����:(��ʾ�����Եȼ��ı���ΪԤ�ڵȼ���������ʵ�������Եȼ�)
        public void UpdateDexterityLevel()
        {
            projectedDexterityLevel += 1;
            if (projectedDexterityLevel > 99)
            {
                projectedDexterityLevel = 99;
            }

            dexteritySlider.value = projectedDexterityLevel;
            currentDexterityLevel.text = projectedDexterityLevel.ToString("d2");

            if (projectedDexterityLevel > playerManager.playerStateManager.dexterityLevel)
            {
                currentDexterityLevel.color = Color.blue;
            }

            UpdateProjectedPlayerLevel();   //����Ԥ�ڵȼ�;
            WeaponItem[] rightWeapons = playerManager.playerInventoryManager.WeaponRightHandSlots;
            WeaponItem[] leftWeapons = playerManager.playerInventoryManager.WeaponLeftHandSlots;
            //���µ�ǰ����������:
            UpdateCurrentWeaponAggressivity(currentRightHandAggressivityText01, rightWeapons[0]);
            UpdateCurrentWeaponAggressivity(currentRightHandAggressivityText02, rightWeapons[1]);
            UpdateCurrentWeaponAggressivity(currentRightHandAggressivityText03, rightWeapons[2]);

            UpdateCurrentWeaponAggressivity(currentLeftHandAggressivityText01, leftWeapons[0]);
            UpdateCurrentWeaponAggressivity(currentLeftHandAggressivityText02, leftWeapons[1]);
            UpdateCurrentWeaponAggressivity(currentLeftHandAggressivityText03, leftWeapons[2]);
            //����Ԥ������������:
            UpdateProjectedWeaponAggressivity(projectedRightHandAggressivityText01, rightWeapons[0]);
            UpdateProjectedWeaponAggressivity(projectedRightHandAggressivityText02, rightWeapons[1]);
            UpdateProjectedWeaponAggressivity(projectedRightHandAggressivityText03, rightWeapons[2]);

            UpdateProjectedWeaponAggressivity(projectedLeftHandAggressivityText01, leftWeapons[0]);
            UpdateProjectedWeaponAggressivity(projectedLeftHandAggressivityText02, leftWeapons[1]);
            UpdateProjectedWeaponAggressivity(projectedLeftHandAggressivityText03, leftWeapons[2]);

            //���¿��Ժ͵ֿ����ı�:
            UpdateDefenseText();
        }

        //����Ԥ��"����"�ȼ�����ʾ�ı�����:(��ʾ�����Եȼ��ı���ΪԤ�ڵȼ���������ʵ�������Եȼ�)
        public void UpdateIntelligenceLevel()
        {
            projectedIntelligenceLevel += 1;
            if (projectedIntelligenceLevel > 99)
            {
                projectedIntelligenceLevel = 99;
            }

            intelligenceSlider.value = projectedIntelligenceLevel;
            currentIntelligenceLevel.text = projectedIntelligenceLevel.ToString("d2");

            if (projectedIntelligenceLevel > playerManager.playerStateManager.intelligenceLevel)
            {
                currentIntelligenceLevel.color = Color.blue;
            }

            UpdateProjectedPlayerLevel();   //����Ԥ�ڵȼ�;
            WeaponItem[] rightWeapons = playerManager.playerInventoryManager.WeaponRightHandSlots;
            WeaponItem[] leftWeapons = playerManager.playerInventoryManager.WeaponLeftHandSlots;
            //���µ�ǰ����������:
            UpdateCurrentWeaponAggressivity(currentRightHandAggressivityText01, rightWeapons[0]);
            UpdateCurrentWeaponAggressivity(currentRightHandAggressivityText02, rightWeapons[1]);
            UpdateCurrentWeaponAggressivity(currentRightHandAggressivityText03, rightWeapons[2]);

            UpdateCurrentWeaponAggressivity(currentLeftHandAggressivityText01, leftWeapons[0]);
            UpdateCurrentWeaponAggressivity(currentLeftHandAggressivityText02, leftWeapons[1]);
            UpdateCurrentWeaponAggressivity(currentLeftHandAggressivityText03, leftWeapons[2]);
            //����Ԥ������������:
            UpdateProjectedWeaponAggressivity(projectedRightHandAggressivityText01, rightWeapons[0]);
            UpdateProjectedWeaponAggressivity(projectedRightHandAggressivityText02, rightWeapons[1]);
            UpdateProjectedWeaponAggressivity(projectedRightHandAggressivityText03, rightWeapons[2]);

            UpdateProjectedWeaponAggressivity(projectedLeftHandAggressivityText01, leftWeapons[0]);
            UpdateProjectedWeaponAggressivity(projectedLeftHandAggressivityText02, leftWeapons[1]);
            UpdateProjectedWeaponAggressivity(projectedLeftHandAggressivityText03, leftWeapons[2]);

            //���¿��Ժ͵ֿ����ı�:
            UpdateDefenseText();
        }

        //����Ԥ��"����"�ȼ�����ʾ�ı�����:(��ʾ�����Եȼ��ı���ΪԤ�ڵȼ���������ʵ�������Եȼ�)
        public void UpdateFaithLevel()
        {
            projectedFaithLevel += 1;
            if (projectedFaithLevel > 99)
            {
                projectedFaithLevel = 99;
            }

            faithSlider.value = projectedFaithLevel;
            currentFaithLevel.text = projectedFaithLevel.ToString("d2");

            if (projectedFaithLevel > playerManager.playerStateManager.faithLevel)
            {
                currentFaithLevel.color = Color.blue;
            }

            UpdateProjectedPlayerLevel();   //����Ԥ�ڵȼ�;
            WeaponItem[] rightWeapons = playerManager.playerInventoryManager.WeaponRightHandSlots;
            WeaponItem[] leftWeapons = playerManager.playerInventoryManager.WeaponLeftHandSlots;
            //���µ�ǰ����������:
            UpdateCurrentWeaponAggressivity(currentRightHandAggressivityText01, rightWeapons[0]);
            UpdateCurrentWeaponAggressivity(currentRightHandAggressivityText02, rightWeapons[1]);
            UpdateCurrentWeaponAggressivity(currentRightHandAggressivityText03, rightWeapons[2]);

            UpdateCurrentWeaponAggressivity(currentLeftHandAggressivityText01, leftWeapons[0]);
            UpdateCurrentWeaponAggressivity(currentLeftHandAggressivityText02, leftWeapons[1]);
            UpdateCurrentWeaponAggressivity(currentLeftHandAggressivityText03, leftWeapons[2]);
            //����Ԥ������������:
            UpdateProjectedWeaponAggressivity(projectedRightHandAggressivityText01, rightWeapons[0]);
            UpdateProjectedWeaponAggressivity(projectedRightHandAggressivityText02, rightWeapons[1]);
            UpdateProjectedWeaponAggressivity(projectedRightHandAggressivityText03, rightWeapons[2]);

            UpdateProjectedWeaponAggressivity(projectedLeftHandAggressivityText01, leftWeapons[0]);
            UpdateProjectedWeaponAggressivity(projectedLeftHandAggressivityText02, leftWeapons[1]);
            UpdateProjectedWeaponAggressivity(projectedLeftHandAggressivityText03, leftWeapons[2]);

            //���¿��Ժ͵ֿ����ı�:
            UpdateDefenseText();
        }


        //�ر����뷽��:
        public void CloseInputEventMethod()
        {
            //ȡ���������ڵ����а�ť�Ľ�����
            Button[] buttons = GetComponentsInChildren<Button>();
            foreach (Button button in buttons)
            {
                button.interactable = false;
            }
            //������ʾ����:
            uiManager.EnablePromptWindow();
            //����"ȷ�ϰ�ť�¼�"��"���ϰ�ť�¼�"��"��ʾ�ı�����"
            uiManager.deathPromptWindow.promptText.text = "ȡ������?";
            uiManager.deathPromptWindow.confirmButtonEvent.AddListener(PromptWindowConfirmMethod);
            uiManager.deathPromptWindow.denyButtonEvent.AddListener(PromptWindowDenyMethod);
        }

        //ȷ�ϰ�ť�¼�:
        private void PromptWindowConfirmMethod()
        {
            Button[] buttons = GetComponentsInChildren<Button>();
            foreach (Button button in buttons)
            {
                button.interactable = true;
            }

            //����"��������",����"��ʾ����":
            uiManager.deathPromptWindow.gameObject.SetActive(false);
            gameObject.SetActive(false);
            //�������(player)��"������"����:
            playerManager.playerAnimatorManager.PlayTargetAnimation("EndPhase", true);
        }
        //���ϰ�ť�¼�:
        private void PromptWindowDenyMethod()
        {
            Button[] buttons = GetComponentsInChildren<Button>();
            foreach (Button button in buttons)
            {
                button.interactable = true;
            }
            //����"��ʾ����":
            uiManager.deathPromptWindow.gameObject.SetActive(false);
        }

        //���µ�ǰ����������:
        private void UpdateCurrentWeaponAggressivity(Text aggressivityText, WeaponItem weapon)
        {
            if (weapon != null)
            {
                int damage = weapon.physicalDamage + weapon.magicDamage + weapon.lightningDamage + weapon.fierDamage + weapon.darkDamage;
                //��������:
                float strengthAddition = weapon.strengthAddition;                     //�����ӳ�
                float dexterityAddition = weapon.dexterityAddition;                    //���ݼӳ�
                float intelligenceAddition = weapon.intelligenceAddition;           //�����ӳ�
                float faithAddition = weapon.faithAddition;                                  //�����ӳ�
                                                                                             //���Եȼ�:
                int strengthLevel = playerManager.characterStatsManager.strengthLevel;
                int dexterityLevel = playerManager.characterStatsManager.dexterityLevel;
                int intelligenceLevel = playerManager.characterStatsManager.intelligenceLevel;
                int faithLevel = playerManager.characterStatsManager.faithLevel;

                float finalDamage = damage + damage * (strengthAddition * strengthLevel / 20);
                finalDamage += damage * (dexterityAddition * dexterityLevel / 20);
                finalDamage += damage * (intelligenceAddition * intelligenceLevel / 20);
                finalDamage += damage * (faithAddition * faithLevel / 20);

                aggressivityText.text = Mathf.RoundToInt(finalDamage).ToString("d3");
            }
            else
            {
                aggressivityText.text = "000";
            }
        }
        //����Ԥ������������:
        private void UpdateProjectedWeaponAggressivity(Text aggressivityText, WeaponItem weapon)
        {
            if (weapon != null)
            {
                int damage = weapon.physicalDamage + weapon.magicDamage + weapon.lightningDamage + weapon.fierDamage + weapon.darkDamage;
                //��������:
                float strengthAddition = weapon.strengthAddition;                     //�����ӳ�
                float dexterityAddition = weapon.dexterityAddition;                    //���ݼӳ�
                float intelligenceAddition = weapon.intelligenceAddition;           //�����ӳ�
                float faithAddition = weapon.faithAddition;                                  //�����ӳ�

                float finalDamage = damage + damage * (strengthAddition * projectedStrengthLevel / 20);
                finalDamage += damage * (dexterityAddition * projectedDexterityLevel / 20);
                finalDamage += damage * (intelligenceAddition * projectedIntelligenceLevel / 20);
                finalDamage += damage * (faithAddition * projectedFaithLevel / 20);

                aggressivityText.color = Color.blue;
                aggressivityText.text = Mathf.RoundToInt(finalDamage).ToString("d3");
            }
            else
            {
                aggressivityText.color = Color.white;
                aggressivityText.text = "000";
            }
        }

        private void UpdateDefenseText()
        {
            PlayerStatsManager playerStats = playerManager.playerStateManager;
            //����ֵ:
            int currentPhysicalDamageAbsorption = (int)(playerStats.physicalDamageAbsorption * 100);
            int currentFireDamageAbsorption = (int)(playerStats.fireDamageAbsorption * 100);
            int currentMagicDamageAbsorption = (int)(playerStats.magicDamageAbsorption * 100);
            int currentLightningDamageAbsorption = (int)(playerStats.lightningDamageAbsorption * 100);
            int currentDarkDamageAbsorption = (int)(playerStats.darkDamageAbsorption * 100);
            //�ֿ���ֵ:
            int currentPoisonDefenseAbsorption = (int)(playerStats.poisonDefenseAbsorption * 100);
            int currentHemorrhageDefenseAbsorption = (int)(playerStats.hemorrhageDefenseAbsorption * 100);
            int currentFrostDamageAbsorption = (int)(playerStats.frostDamageAbsorption * 100);
            int currentCurseDefenseAbsorption = (int)(playerStats.curseDefenseAbsorption * 100);

            //Ԥ�ڿ���ֵ:
            int projectedPhysicalDamageAbsorption = currentPhysicalDamageAbsorption + (int)(100 * (projectedStrengthLevel / 2 * 0.001f + projectedDexterityLevel / 2 * 0.002f));
            int projectedFireDamageAbsorption = currentFireDamageAbsorption + (int)(100 * (projectedFaithLevel / 2 * 0.001f));
            int projectedMagicDamageAbsorption = currentMagicDamageAbsorption + (int)(100 * (projectedIntelligenceLevel / 2 * 0.002f));
            int projectedLightningDamageAbsorption = currentLightningDamageAbsorption + (int)(100 * (projectedFaithLevel / 2 * 0.002f));
            int projectedDarkDamageAbsorption = currentDarkDamageAbsorption + (int)(100 * (projectedFaithLevel / 2 * 0.0005f + projectedIntelligenceLevel / 2 * 0.0005f));
            //Ԥ�ڵֿ���:
            int projectedPoisonDefenseAbsorption = currentPoisonDefenseAbsorption + (int)(100 * (projectedStrengthLevel / 2 * 0.001f));
            int projectedHemorrhageDefenseAbsorption = currentHemorrhageDefenseAbsorption + (int)(100 * (projectedStrengthLevel / 2 * 0.001f)); ;
            int projectedFrostDamageAbsorption = currentFrostDamageAbsorption + (int)(100 * (projectedDexterityLevel / 2 * 0.001f));
            int projectedCurseDefenseAbsorption = currentCurseDefenseAbsorption + (int)(100 * (projectedFaithLevel / 2 * 0.0005f + projectedIntelligenceLevel / 2 * 0.0005f));

            //��ֵ:
            //����------
            currentPhysicalDamageAbsorptionText.text = currentPhysicalDamageAbsorption.ToString("d3");
            currentFireDamageAbsorptionText.text = currentFireDamageAbsorption.ToString("d3");
            currentMagicDamageAbsorptionText.text = currentMagicDamageAbsorption.ToString("d3");
            currentLightningDamageAbsorptionText.text = currentLightningDamageAbsorption.ToString("d3");
            currentDarkDamageAbsorptionText.text = currentDarkDamageAbsorption.ToString("d3");

            projectedPhysicalDamageAbsorptionText.text = projectedPhysicalDamageAbsorption.ToString("d3");
            projectedFireDamageAbsorptionText.text = projectedFireDamageAbsorption.ToString("d3");
            projectedMagicDamageAbsorptionText.text = projectedMagicDamageAbsorption.ToString("d3");
            projectedLightningDamageAbsorptionText.text = projectedLightningDamageAbsorption.ToString("d3");
            projectedDarkDamageAbsorptionText.text = projectedDarkDamageAbsorption.ToString("d3");
            //�ֿ���-----
            currentPoisonDefenseAbsorptionText.text = currentPoisonDefenseAbsorption.ToString("d3");
            currentHemorrhageDefenseAbsorptionText.text = currentHemorrhageDefenseAbsorption.ToString("d3");
            currentFrostDamageAbsorptionText.text = currentFrostDamageAbsorption.ToString("d3");
            currentCurseDefenseAbsorptionText.text = currentCurseDefenseAbsorption.ToString("d3");

            projectedPoisonDefenseAbsorptionText.text = projectedPoisonDefenseAbsorption.ToString("d3");
            projectedHemorrhageDefenseAbsorptionText.text = projectedHemorrhageDefenseAbsorption.ToString("d3");
            projectedFrostDamageAbsorptionText.text = projectedFrostDamageAbsorption.ToString("d3");
            projectedCurseDefenseAbsorptionText.text = projectedCurseDefenseAbsorption.ToString("d3");

            //�ı���ɫ�ж�:
            #region �����ı�:
            if (currentPhysicalDamageAbsorption < projectedPhysicalDamageAbsorption)
            {
                projectedPhysicalDamageAbsorptionText.color = Color.blue;
            }
            else
            {
                projectedPhysicalDamageAbsorptionText.color = Color.white;
            }

            if (currentFireDamageAbsorption < projectedFireDamageAbsorption)
            {
                projectedFireDamageAbsorptionText.color = Color.blue;
            }
            else
            {
                projectedFireDamageAbsorptionText.color = Color.white;
            }

            if (currentMagicDamageAbsorption < projectedMagicDamageAbsorption)
            {
                projectedMagicDamageAbsorptionText.color = Color.blue;
            }
            else
            {
                projectedMagicDamageAbsorptionText.color = Color.white;
            }

            if (currentLightningDamageAbsorption < projectedLightningDamageAbsorption)
            {
                projectedLightningDamageAbsorptionText.color = Color.blue;
            }
            else
            {
                projectedLightningDamageAbsorptionText.color = Color.white;
            }

            if (currentDarkDamageAbsorption < projectedDarkDamageAbsorption)
            {
                projectedDarkDamageAbsorptionText.color = Color.blue;
            }
            else
            {
                projectedDarkDamageAbsorptionText.color = Color.white;
            }
            #endregion

            #region �ֿ����ı�:
            if(currentPoisonDefenseAbsorption < projectedPoisonDefenseAbsorption)
            {
                projectedPoisonDefenseAbsorptionText.color = Color.blue;
            }
            else
            {
                projectedPoisonDefenseAbsorptionText.color = Color.white;
            }

            if (currentHemorrhageDefenseAbsorption < projectedHemorrhageDefenseAbsorption)
            {
                projectedHemorrhageDefenseAbsorptionText.color = Color.blue;
            }
            else
            {
                projectedHemorrhageDefenseAbsorptionText.color = Color.white;
            }

            if (currentFrostDamageAbsorption < projectedFrostDamageAbsorption)
            {
                projectedFrostDamageAbsorptionText.color = Color.blue;
            }
            else
            {
                projectedFrostDamageAbsorptionText.color = Color.white;
            }

            if (currentCurseDefenseAbsorption < projectedCurseDefenseAbsorption)
            {
                projectedCurseDefenseAbsorptionText.color = Color.blue;
            }
            else
            {
                projectedCurseDefenseAbsorptionText.color = Color.white;
            }
            #endregion
        }
    }
}