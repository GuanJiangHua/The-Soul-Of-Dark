using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class EnemyStatsManager : CharacterStatsManager
    {
        EnemyAnimatorManager enemyAnimatorManager;
        EnemyManager enemyManager;
        EnemyBossManager enemyBossManager;

        UIEnemyHealthBar enemyHealthBar;

        public bool isBoss;
        protected override void Awake()
        {
            base.Awake();
            enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
            enemyManager = GetComponent<EnemyManager>();
            enemyHealthBar = GetComponentInChildren<UIEnemyHealthBar>();

            if (isBoss)
            {
                enemyBossManager = GetComponent<EnemyBossManager>();
            }

            maxHealth = healthLevel * 10;
            currentHealth = maxHealth;
        }

        protected override void Start()
        {
            base.Start();
            enemyHealthBar.SetMaxHealth(maxHealth);
            totalPoiseDefence = armorPoiseBonus;
        }

        //受到伤害:
        public override void TakeDamage(int physicalDamage , int fireDamage , int magicDamage, int lightningDamage , int darkDamage, string damageAnimation = "Damage")
        {
            if (isDead == true) return;
            if (chaeacterManager.isInvulnerab) return;

            if (enemyManager.isPhaseShifting == true)
            {
                TakeDamageNoAnimation(physicalDamage , fireDamage , magicDamage , lightningDamage , darkDamage);
            }
            else
            {
                base.TakeDamage(physicalDamage , fireDamage , magicDamage , lightningDamage , darkDamage);
                //enemyAnimatorManager.DisableWeaponCollisionEvent();
                enemyAnimatorManager.PlayTargetAnimation(damageAnimation, true, 0);
            }

            enemyManager.currentRecoveryTime = enemyManager.currentRecoveryTime > 0.5f ? enemyManager.currentRecoveryTime : 0.5f;

            enemyHealthBar.SetHealthBar(currentHealth);


            if (isBoss)
            {
                enemyBossManager.UpdateBossHealthBar(currentHealth , maxHealth);
            }

            if (currentHealth <= 0)
            {
                enemyAnimatorManager.PlayTargetAnimation("Death_01");
                HandleDeath();
            }

            //用对话系统表达不满,并提示玩家不要再继续...
            if (enemyManager.npcManager != null)
            {
                enemyManager.npcManager.GiveExhortDialogue();
            }
        }
        //受伤但是无动画:
        public override void TakeDamageNoAnimation(int physicalDamage, int fireDamage , int magicDamage , int lightningDamage , int darkDamage)
        {
            base.TakeDamageNoAnimation(physicalDamage, fireDamage, magicDamage , lightningDamage , darkDamage);
            currentHealth = (currentHealth - physicalDamage) > 0 ? currentHealth - physicalDamage : 0;

            enemyHealthBar.SetHealthBar(currentHealth);

            if (isBoss)
            {
                enemyBossManager.UpdateBossHealthBar(currentHealth , maxHealth);
            }
            if (currentHealth <= 0)
            {
                HandleDeath();
            }

            //用对话系统表达不满,并提示玩家不要再继续...
            if (enemyManager.npcManager != null)
            {
                enemyManager.npcManager.GiveExhortDialogue();
            }
        }
        //重置属性:
        public void ReturnToOriginalState()
        {
            if (enemyManager.enemyLocomotionManager.backStabCollider != null)
            {
                enemyManager.enemyLocomotionManager.backStabCollider.enabled = true;
            }
            enemyManager.enemyLocomotionManager.characterCollider.enabled = true;
            enemyManager.enemyLocomotionManager.characterCollidionBlockerCollider.enabled = true;

            currentHealth = maxHealth;
            currentStamina = enemyManager.enemyStats.maxStamina;
            currentFocusPoints = maxFocusPoints;
            isDead = false;

            enemyHealthBar.SetMaxHealth(maxHealth);
            enemyHealthBar.gameObject.SetActive(true);
        }
        //中毒伤害:
        public override void TakePoisonDamage(int damage)
        {
            if (isDead) return;

            base.TakePoisonDamage(damage);
            enemyHealthBar.SetHealthBar(currentHealth);

            if (isBoss)
            {
                enemyBossManager.UpdateBossHealthBar(currentHealth, maxHealth);
            }

            if (currentHealth <= 0)
            {
                enemyAnimatorManager.PlayTargetAnimation("Death_01", true);
                isDead = true;
                HandleDeath();
            }
        }
        //霜冻伤害:
        public override void TakeFrostDamage(int damage)
        {
            if (isDead) return;
            base.TakeFrostDamage(damage);

            enemyHealthBar.SetHealthBar(currentHealth);
            if (isBoss)
            {
                enemyBossManager.UpdateBossHealthBar(currentHealth, maxHealth);
            }

            if (currentHealth <= 0)
            {
                isDead = true;
                enemyAnimatorManager.PlayTargetAnimation("Death_01", true);
                HandleDeath();
            }
            else
            {
                enemyAnimatorManager.PlayTargetAnimation("Damage", true);
            }
        }
        //出血伤害:
        public override void TakeHemorrhageDamage(int damage)
        {
            if (isDead) return;
            base.TakeHemorrhageDamage(damage);
            enemyHealthBar.SetHealthBar(currentHealth);
            if (isBoss)
            {
                enemyBossManager.UpdateBossHealthBar(currentHealth, maxHealth);
            }

            if (currentHealth <= 0)
            {
                isDead = true;
                enemyAnimatorManager.PlayTargetAnimation("Death_01", true);
                HandleDeath();
            }
            else
            {
                enemyAnimatorManager.PlayTargetAnimation("Damage", true);
            }
        }
        //处理死亡:
        private void HandleDeath()
        {
            Collider[] colliders = GetComponentsInChildren<Collider>();
            foreach(Collider collider in colliders)
            {
                collider.enabled = false;
            }
            foreach(DamageDetectionCollition damageDetection in damageDetectionCollitions)
            {
                damageDetection.enabled = false;
            }
            CameraHandler cameraHandler = FindObjectOfType<CameraHandler>();
            isDead = true;
            chaeacterManager.isDeath = true;
            if(cameraHandler.currentLockOnTarger == enemyManager)
            {
                cameraHandler.ClearLockOnTargets();
                FindObjectOfType<InputHandler>().lockOnFlag = false;
                FindObjectOfType<InputHandler>().lockOn_Input = true;
            }
            if (isBoss)
            {
                FindObjectOfType<WorldEventManager>().BossHasBenDefeated(enemyBossManager);
            }

            if (enemyManager.npcManager != null)
            {
                enemyManager.npcManager.GiveLastWord();
                if (enemyManager.npcManager.storeManager != null)
                {
                    PlayerSaveManager.SaveToFile(FindObjectOfType<PlayerManager>(), PlotProgressManager.single, WorldEventManager.single);
                    enemyManager.npcManager.storeManager.InstantiationAshesItem();
                }
            }
        }
    }
}
