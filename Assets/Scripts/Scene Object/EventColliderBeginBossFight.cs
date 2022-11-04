using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    //��ʼbossս��ײ��:
    public class EventColliderBeginBossFight : MonoBehaviour
    {
        WorldEventManager worldEventManager;

        EnemyStatsManager enemyStats;
        EnemyManager enemyManager;

        BoxCollider boxCollider;
        public EnemyBossManager bossManager;//����ײ��������boss;
        private void Awake()
        {
            worldEventManager = FindObjectOfType<WorldEventManager>();
            boxCollider = GetComponent<BoxCollider>();
            if (bossManager != null)
            {
                enemyStats = bossManager.GetComponent<EnemyStatsManager>();
                enemyManager = bossManager.GetComponent<EnemyManager>();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerManager playerManger = other.GetComponent<PlayerManager>();

            //boss�����õ�:
            if (playerManger!=null && bossManager.gameObject.activeInHierarchy == true)
            {
                //����bossս:
                worldEventManager.ActivateBossFight(bossManager);
                //�������Ե�ui
                bossManager.GiveBossPropertiesToUI();
                //����ui
                worldEventManager.uiBossHealthBar.SetUIHealthBarToActive();

                enemyManager.currentTarget = other.GetComponent<PlayerStatsManager>();

                boxCollider.enabled = false;
            }
        }
    }
}
