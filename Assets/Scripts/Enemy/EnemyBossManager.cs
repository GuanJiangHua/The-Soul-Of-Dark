using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    //�����л��׶�
    //�����л�����ģʽ
    public class EnemyBossManager : MonoBehaviour
    {
        [Header("Boss����:")]
        public string bossName;
        public bool bossFightIsActive;              //bossս������,������bossս��
        public bool bossHasBeenAwakened;    //boss�Ѿ������ѣ����˹�������������ս�������ˣ������ٹۿ�һ�飩
        public bool bossHasBeenDefeated;      //boss�Ѿ�������?
        [Header("ת�׶���Ч:")]
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

        //����boss���Ե�ui
        public void GiveBossPropertiesToUI()
        {
            uiBossHealthBar.SetBossName(bossName);
            uiBossHealthBar.SetBossMaxHealth(enemyStats.maxHealth);
        }
    
        //����bossѪ���ĵ�ǰѪ��:
        public void UpdateBossHealthBar(int currentHelth , int maxHelth)
        {
            uiBossHealthBar.SetBossCurrentHealth(currentHelth);
            if(currentHelth < maxHelth / 2 && bossCombatStanceState.hasPhaseShifted == false)
            {
                ShiftToSecondPhase();
            }
        }

        //ת��ڶ��׶�:
        public void ShiftToSecondPhase()
        {
            //���Ŷ���/�¼�����������Ч/������Ч
            enemyAnimatorManager.PlayTargetAnimation("Phase_Shift", true);
            enemyAnimatorManager.anim.SetBool("isPhaseShifting", true);
            //�л���������
            bossCombatStanceState.hasPhaseShifted = true;

            enemyWeaponSlotManager.LoadWeaponHolderOfSlot();
        }
    }
}
