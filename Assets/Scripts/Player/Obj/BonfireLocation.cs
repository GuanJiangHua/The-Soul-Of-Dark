using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class BonfireLocation : Interactable
    {
        [Header("归属的大地点索引:")]
        public int gameAreaIndex;
        [Header("地点名称:")]
        public string locationName = "某地点";
        public Sprite locationIcon;
        [Header("篝火是否点燃了:")]
        public bool isLgnite = false;
        [Header("篝火是否被发现:")]
        public bool isOnceMet = false;
        [Header("篝火对应的主线:")]
        public int TargetMainlineProgress = 0;
        [Header("火焰特效:")]
        public ParticleSystem fireEffects;
        [Header("烟雾特效:")]
        public ParticleSystem fogEffects;
        private void Start()
        {
            if (isLgnite)
            {
                InteractableText = "按R坐下休息";

                IsLit(true);
            }
            else
            {
                InteractableText = "按R点燃篝火";

                IsLit(false);
            }
        }
        public override void Interact(PlayerManager playerManger)
        {
            if(isLgnite == false)
            {
                //点燃篝火:
                isLgnite = true;
                //篝火已经被发现:
                isOnceMet = true;
                InteractableText = "按R坐下休息";

                IsLit(true);

                //播放点火动作:
                playerManger.playerAnimatorManager.PlayTargetAnimation("Bonfire_Ignite", true);

                //熄灭其他篝火:
                WorldEventManager.single.ExtinguishOtherBonfires(this);
                //推进剧情:
                PlotProgressManager.single.AdvanceToMainPlot(TargetMainlineProgress);
            }
            else
            {
                playerManger.isResting = true;
                //播放坐下休息动画
                playerManger.playerAnimatorManager.PlayTargetAnimation("Sit-Sitdown", true);
                playerManger.playerWeaponSlotManger.leftHandSlot.UnloadWeapon();
                //玩家属性复原:
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

                //启用篝火功能ui窗口
                playerManger.uiManager.EnableCampfireUIWindow();
                //设置该篝火为上一个休息的篝火:
                playerManger.previousBonfireIndex = WorldEventManager.single.GetBonfiresIndex(this);
                //重置元素瓶:
                playerManger.uiManager.campfireUIWindowManager.assignElementBottleWindowManager.RestoreElementBottle(playerManger);
                //设置重生位置:
                playerManger.rebirthPosition = playerManger.transform.position;

                //敌人恢复原位:
                WorldEventManager.single.AllEnemyReturnToOriginalState();

                PlayerSaveManager.SaveToFile(playerManger, PlotProgressManager.single, WorldEventManager.single);
            }
        }

        public void IsLit(bool isLit)
        {
            if (isLit)
            {
                //启用火焰特效
                fireEffects.gameObject.SetActive(true);
                fireEffects.Play();
                //禁用烟雾特效
                fogEffects.gameObject.SetActive(false);
                fogEffects.Stop();
            }
            else
            {
                //禁用火焰特效
                fireEffects.gameObject.SetActive(false);
                fireEffects.Stop();
                //启用烟雾特效
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