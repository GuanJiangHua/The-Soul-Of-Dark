using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    //开始boss战碰撞器:
    public class EventColliderBeginBossFight : MonoBehaviour
    {
        WorldEventManager worldEventManager;

        EnemyStatsManager enemyStats;
        EnemyManager enemyManager;

        BoxCollider boxCollider;
        public EnemyBossManager bossManager;//本碰撞器关联的boss;
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

            //boss是启用的:
            if (playerManger!=null && bossManager.gameObject.activeInHierarchy == true)
            {
                //开启boss战:
                worldEventManager.ActivateBossFight(bossManager);
                //给出属性到ui
                bossManager.GiveBossPropertiesToUI();
                //启用ui
                worldEventManager.uiBossHealthBar.SetUIHealthBarToActive();

                enemyManager.currentTarget = other.GetComponent<PlayerStatsManager>();

                boxCollider.enabled = false;
            }
        }
    }
}
