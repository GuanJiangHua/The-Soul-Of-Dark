using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    //处理切换阶段
    //处理切换攻击模式
    public class EnemyBossManager : MonoBehaviour
    {
        [Header("Boss属性:")]
        public string bossName;
        public bool bossFightIsActive;              //boss战启动中,正在与boss战斗
        public bool bossHasBeenAwakened;    //boss已经被唤醒，看了过场动画，但在战斗中死了（不必再观看一遍）
        public bool bossHasBeenDefeated;      //boss已经被击败?
        [Header("转阶段特效:")]
        public GameObject particleFX;

        EnemyWeaponSlotManager enemyWeaponSlotManager;
        EnemyStatsManager enemyStats;
        EnemyAnimatorManager enemyAnimatorManager;
        BossCombatStanceState bossCombatStanceState;
        UIBossHealthBar uiBossHealthBar;
        private void Awake()
        {
            enemyStats = GetComponent<EnemyStatsManager>();
            uiBossHealthBar = FindObjectOfType<UIBossHealthBar>();
            enemyWeaponSlotManager = GetComponent<EnemyWeaponSlotManager>();
            enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
            bossCombatStanceState = GetComponentInChildren<BossCombatStanceState>();
        }
        private void Start()
        {

        }

        //给出boss属性到ui
        public void GiveBossPropertiesToUI()
        {
            uiBossHealthBar.SetBossName(bossName);
            uiBossHealthBar.SetBossMaxHealth(enemyStats.maxHealth);
        }
    
        //更新boss血条的当前血量:
        public void UpdateBossHealthBar(int currentHelth , int maxHelth)
        {
            uiBossHealthBar.SetBossCurrentHealth(currentHelth);
            if(currentHelth < maxHelth / 2 && bossCombatStanceState.hasPhaseShifted == false)
            {
                ShiftToSecondPhase();
            }
        }

        //转向第二阶段:
        public void ShiftToSecondPhase()
        {
            //播放动画/事件触发粒子特效/武器特效
            enemyAnimatorManager.PlayTargetAnimation("Phase_Shift", true);
            enemyAnimatorManager.anim.SetBool("isPhaseShifting", true);
            //切换攻击动作
            bossCombatStanceState.hasPhaseShifted = true;

            enemyWeaponSlotManager.LoadWeaponHolderOfSlot();
        }
    }
}
