using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class BonfireLocation : Interactable
    {
        [Header("�����Ĵ�ص�����:")]
        public int gameAreaIndex;
        [Header("�ص�����:")]
        public string locationName = "ĳ�ص�";
        public Sprite locationIcon;
        [Header("�����Ƿ��ȼ��:")]
        public bool isLgnite = false;
        [Header("�����Ƿ񱻷���:")]
        public bool isOnceMet = false;
        [Header("�����Ӧ������:")]
        public int TargetMainlineProgress = 0;
        [Header("������Ч:")]
        public ParticleSystem fireEffects;
        [Header("������Ч:")]
        public ParticleSystem fogEffects;
        private void Start()
        {
            if (isLgnite)
            {
                InteractableText = "��R������Ϣ";

                IsLit(true);
            }
            else
            {
                InteractableText = "��R��ȼ����";

                IsLit(false);
            }
        }
        public override void Interact(PlayerManager playerManger)
        {
            if(isLgnite == false)
            {
                //��ȼ����:
                isLgnite = true;
                //�����Ѿ�������:
                isOnceMet = true;
                InteractableText = "��R������Ϣ";

                IsLit(true);

                //���ŵ����:
                playerManger.playerAnimatorManager.PlayTargetAnimation("Bonfire_Ignite", true);

                //Ϩ����������:
                WorldEventManager.single.ExtinguishOtherBonfires(this);
                //�ƽ�����:
                PlotProgressManager.single.AdvanceToMainPlot(TargetMainlineProgress);
            }
            else
            {
                playerManger.isResting = true;
                //����������Ϣ����
                playerManger.playerAnimatorManager.PlayTargetAnimation("Sit-Sitdown", true);
                playerManger.playerWeaponSlotManger.leftHandSlot.UnloadWeapon();
                //������Ը�ԭ:
                PlayerStatsManager playerStats = playerManger.playerStateManager;
                playerStats.currentHealth = playerStats.maxHealth;
                playerStats.currentFocusPoints = playerStats.maxFocusPoints;
                playerStats.currentStamina = playerStats.maxStamina;
                playerStats.healthBar.SetCurrentHealth(playerStats.currentHealth);
                playerStats.focusPointBar.SetCurrentFocusPoint(playerStats.currentFocusPoints);
                playerStats.staminaBar.SetCurrentStamina(playerStats.currentStamina);

                PlayerEffectsManager playerEffects = playerManger.playerEffectsManager;
                playerEffects.poisonBuildup = 0;
                playerEffects.poisonAmount = 0.1f;
                playerEffects.frostingBuildup = 0;
                playerEffects.frostingAmount = 0.1f;
                playerEffects.hemorrhageBuildup = 0;
                playerEffects.hemorrhageAmount = 0.1f;

                //����������ui����
                playerManger.uiManager.EnableCampfireUIWindow();
                //���ø�����Ϊ��һ����Ϣ������:
                playerManger.previousBonfireIndex = WorldEventManager.single.GetBonfiresIndex(this);
                //����Ԫ��ƿ:
                playerManger.uiManager.campfireUIWindowManager.assignElementBottleWindowManager.RestoreElementBottle(playerManger);
                //��������λ��:
                playerManger.rebirthPosition = playerManger.transform.position;

                //���˻ָ�ԭλ:
                WorldEventManager.single.AllEnemyReturnToOriginalState();

                PlayerSaveManager.SaveToFile(playerManger, PlotProgressManager.single, WorldEventManager.single);
            }
        }

        public void IsLit(bool isLit)
        {
            if (isLit)
            {
                //���û�����Ч
                fireEffects.gameObject.SetActive(true);
                fireEffects.Play();
                //����������Ч
                fogEffects.gameObject.SetActive(false);
                fogEffects.Stop();
            }
            else
            {
                //���û�����Ч
                fireEffects.gameObject.SetActive(false);
                fireEffects.Stop();
                //����������Ч
                fogEffects.gameObject.SetActive(true);
                if(isOnceMet == true)
                {
                    fogEffects.Play();
                }
                else
                {
                    fogEffects.Stop();
                }
            }

        }
    }
}