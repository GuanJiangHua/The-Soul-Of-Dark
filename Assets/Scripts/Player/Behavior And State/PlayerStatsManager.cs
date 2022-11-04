using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerStatsManager : CharacterStatsManager
    {
        public float regenerateStaminaValue = 1;  //�����ָ��ٶ�

        [Header("������UI:")]
        public HealthBar healthBar;                         //����ֵ������
        public StaminaBar staminaBar;                    //����ֵ������
        public FocusPointBar focusPointBar;           //����ֵ������
        [Header("������ı�:")]
        public SoulCountBar soulCountBar;
        
        float regenerateStaminaTime = 0.5f;
        
        InputHandler inputHandler;
        AudioManager audioManager;
        PlayerAnimatorManager playerAnimatorManager;
        PlayerInventoryManager playerInventory;
        PlayerManager playerManager;
        protected override void Awake()
        {
            base.Awake();

            healthBar = FindObjectOfType<HealthBar>();
            staminaBar = FindObjectOfType<StaminaBar>();
            focusPointBar = FindObjectOfType<FocusPointBar>();
            soulCountBar = FindObjectOfType<SoulCountBar>();

            inputHandler = GetComponent<InputHandler>();
            audioManager = GetComponent<AudioManager>();
            playerInventory = GetComponent<PlayerInventoryManager>();
            playerAnimatorManager = GetComponent<PlayerAnimatorManager>();

            playerManager = (PlayerManager)chaeacterManager;
        }
        protected override void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetCurrentHealth(currentHealth);

            maxStamina = SetMaxStaminaFromLevel();
            staminaBar.SetMaxStamina(maxStamina);
            staminaBar.SetCurrentStamina(currentStamina);

            maxFocusPoints = SetMaxFocusPointsFormLevel();
            focusPointBar.SetMaxFocusPoint(maxFocusPoints);
            focusPointBar.SetCurrentFocusPoint(currentFocusPoints);
            if (soulCountBar != null)
            {
                soulCountBar.SetSoulCountText(currentSoulCount);
            }

            base.Start();
        }

        //�ܵ��˺�:
        public override void TakeDamage(int damage, int fireDamage , int magicDamage,int lightningDamage , int darkDamage, string damageAnimation = "Damage")
        {
            if (isDead == true) return;
            if (chaeacterManager.isInvulnerab) return;

            base.TakeDamage(damage , fireDamage , magicDamage , lightningDamage , darkDamage);

            healthBar.SetCurrentHealth(currentHealth);

            playerAnimatorManager.DisableWeaponCollisionEvent();
            playerAnimatorManager.PlayTargetAnimation(damageAnimation, true, 0);
            if (currentHealth == 0)
            {
                HanleDead();
            }
        }
        //�޶����˺�����:
        public override void TakeDamageNoAnimation(int physicalDamage , int fireDamage , int magicDamage , int lightningDamage , int darkDamage)
        {
            if (isDead == true) return;
            if (chaeacterManager.isInvulnerab) return;

            base.TakeDamageNoAnimation(physicalDamage , fireDamage , magicDamage , lightningDamage , darkDamage);
            healthBar.SetCurrentHealth(currentHealth);

            if (currentHealth == 0) 
            {
                HanleDead();
            }
        }
        //��ʱ����:
        public override void TakeBlockingDamage(int physicalDamage , int fireDamage , int magicDamage , int lightningDamage, int darkDamage)
        {
            string blockImpactAnim = "Damage";
            string blockAudio = "�ܵ�ס����";
            bool isMeleeWeapon = playerInventory.leftWeapon.weaponType == WeaponType.StraightSword 
                || playerInventory.leftWeapon.weaponType == WeaponType.BigSword
                || playerInventory.leftWeapon.weaponType == WeaponType.Dagger
                || playerInventory.leftWeapon.weaponType == WeaponType.Spear
                || playerInventory.leftWeapon.weaponType == WeaponType.BluntInstrument
                || playerInventory.leftWeapon.weaponType == WeaponType.FaithCaster;
            //˫��״̬:(����������Ϊ��)
            if (inputHandler.twoHandFlag || playerInventory.leftWeapon.isUnarmed)
            {
                blockImpactAnim = playerInventory.rightWeapon.mainHandBlock_Impact; //ʹ�������������������񵲶���
                blockAudio = playerInventory.rightWeapon.waeaponBlockAudio;
            }
            //����������Ϊ�գ����Ƕ�������
            else if (playerInventory.leftWeapon != null && (playerInventory.leftWeapon.weaponType == WeaponType.Shield || playerInventory.leftWeapon.weaponType == WeaponType.SmallShield))
            {
                blockImpactAnim = "Shield_Block_Impact";
                blockAudio = playerInventory.leftWeapon.waeaponBlockAudio;
            }
            //����������Ϊ�գ����ǽ�ս����
            else if (playerInventory.leftWeapon != null && isMeleeWeapon)
            {
                blockImpactAnim = playerInventory.leftWeapon.offHandBlock_Impact; //ʹ�����������ĸ��������˺�����
                blockAudio = playerInventory.leftWeapon.waeaponBlockAudio;
            }

            audioManager.PlayAudioByName(blockAudio);
            TakeDamage(physicalDamage, fireDamage , magicDamage, lightningDamage, darkDamage, blockImpactAnim);
        }
        //�ж��˺�:
        public override void TakePoisonDamage(int damage)
        {
            if (isDead) return;

            base.TakePoisonDamage(damage);
            healthBar.SetCurrentHealth(currentHealth);

            if (currentHealth <= 0)
            {
                HanleDead();
            }
        }
        //˪���˺�:
        public override void TakeFrostDamage(int damage)
        {
            if (isDead) return;
            base.TakeFrostDamage(damage);
            healthBar.SetCurrentHealth(currentHealth);

            if (currentHealth <= 0)
            {
                HanleDead();
            }
            else
            {
                playerAnimatorManager.PlayTargetAnimation("Damage", true);
            }
        }
        //��Ѫ�˺�:
        public override void TakeHemorrhageDamage(int damage)
        {
            if (isDead) return;
            base.TakeHemorrhageDamage(damage);
            healthBar.SetCurrentHealth(currentHealth);

            if (currentHealth <= 0)
            {
                HanleDead();
            }
            else
            {
                playerAnimatorManager.PlayTargetAnimation("Damage", true);
            }
        }

        //�������:
        public override void TakeStaminaDamage(int damage)
        {
            base.TakeStaminaDamage(damage);
            currentStamina = (currentStamina - damage) >= 0 ? currentStamina - damage : 0;
            staminaBar.SetCurrentStamina((int)currentStamina);

            regenerateStaminaTime = 0.5f;
        }
        //�������:
        public override void TakeFocusPoints(int damage)
        {
            base.TakeFocusPoints(damage);
            currentFocusPoints = (currentFocusPoints - damage) > 0 ? (currentFocusPoints - damage) : 0;
            focusPointBar.SetCurrentFocusPoint(currentFocusPoints);
        }
    
        //�Զ��ָ�����:
        public void RegenerateStamina()
        {
            if (regenerateStaminaTime >= 0)
            {
                regenerateStaminaTime -= Time.deltaTime;
                return;
            }

            if (chaeacterManager.isInteracting == true) return;

            currentStamina = currentStamina < maxStamina ? (currentStamina + regenerateStaminaValue * Time.deltaTime) : maxStamina;
            staminaBar.SetCurrentStamina((int)currentStamina);
        }

        //����������:
        public override void HealThisTarget(int healAmount)
        {
            base.HealThisTarget(healAmount);
            currentHealth = (currentHealth + healAmount) <= maxHealth ? (currentHealth + healAmount) : maxHealth;
            healthBar.SetCurrentHealth(currentHealth);
        }
        public override void FocusThisTarget(int focusAmount)
        {
            base.FocusThisTarget(focusAmount);
            currentFocusPoints = (currentFocusPoints + focusAmount) <= maxFocusPoints ? (currentFocusPoints + focusAmount) : maxFocusPoints;
            focusPointBar.SetCurrentFocusPoint(currentFocusPoints);

        }

        //��������:
        public void HanleDead(string animName = "Death_01")
        {
            isDead = true;
            playerAnimatorManager.PlayTargetAnimation(animName , true);

            currentHealth = maxHealth;
            currentStamina = maxStamina;
            currentFocusPoints = maxFocusPoints;

            //����λ������:
            if (playerManager.previousBonfireIndex >= 0)
            {
                Transform bonfireTs = WorldEventManager.single.bonfireLocations[playerManager.previousBonfireIndex].transform;
                playerManager.rebirthPosition = bonfireTs.position + bonfireTs.forward;
            }
            else
            {
                playerManager.rebirthPosition = new Vector3(0, 0, 0);
            }

            WorldEventManager.single.playerRegenerationData.isLostSoulNotFound = true;
            WorldEventManager.single.playerRegenerationData.soulLossPoint = playerManager.playerLocomotion.lostPropertyPosition;
            WorldEventManager.single.playerRegenerationData.soulLossAmount = currentSoulCount;

            currentSoulCount = 0;
            //������Ϸ:
            //...
            PlayerSaveManager.SaveToFile(playerManager, PlotProgressManager.single, WorldEventManager.single);

            //����������ʾ����:
            playerManager.uiManager.EnableYouDeadWindow();
        }
        //������:
        public void AddSouls(int souls)
        {
            currentSoulCount = currentSoulCount + Mathf.RoundToInt(souls * soulsRewardLevel);
            soulCountBar.SetSoulCountText(currentSoulCount);
        }
    }
}
