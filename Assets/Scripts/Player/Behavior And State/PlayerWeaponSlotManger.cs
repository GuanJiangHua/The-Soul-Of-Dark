using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerWeaponSlotManger : CharacterWeaponSlotManager
    {
        QuickSlotUI quickSlotUI;
        CameraHandler cameraHandler;
        PlayerEffectsManager playerEffectsManager;
        InputHandler inputHandler;
        protected override void Awake()
        {
            base.Awake();

            quickSlotUI = FindObjectOfType<QuickSlotUI>();
            cameraHandler = FindObjectOfType<CameraHandler>();

            playerEffectsManager = characterEffectsManager as PlayerEffectsManager;
            inputHandler = GetComponent<InputHandler>();
        }

        protected override void Start()
        {
            base.Start();
            AnimatorManager animatorManager = GetComponent<AnimatorManager>();
            //武器碰撞的开始与结束:
            animatorManager.weaponCollisionStartEvent.AddListener(base.OpenDamageCollider);
            animatorManager.weaponCollisionEntEvent.AddListener(base.CloseDamageCollider);

            //武器出手韧性奖励的添加与扣除:
            animatorManager.grantPoiseBonusEvent.AddListener(base.GrantWeaponAttackingPoiseBonus);
            animatorManager.resetPoiseBonusEvent.AddListener(base.ResetWeaponAttackingPoiseBonus);

            LoadWeaponHolderOfSlot();
        }

        //加载武器：
        public override void LoadWeaponHolderOfSlot()
        {
            LoadWeaponHolderOfSlot(characterInventoryManager.rightWeapon, false);
            if(inputHandler.twoHandFlag ==  false)
            {
                LoadWeaponHolderOfSlot(characterInventoryManager.leftWeapon, true);
            }
        }

        public override void LoadWeaponHolderOfSlot(WeaponItem weaponItem, bool isLeft)
        {
            base.LoadWeaponHolderOfSlot(weaponItem, isLeft);
            quickSlotUI.UpdateWeapomQuickSlotUI(characterInventoryManager.leftWeapon, true);
            quickSlotUI.UpdateWeapomQuickSlotUI(characterInventoryManager.rightWeapon, false);
        }

        #region 投掷消耗品炸弹
        public void SucessfullyThrowFireBomb()
        {
            Destroy(playerEffectsManager.instantiatedFXModel);

            BombConsumeableItem bombItem = characterInventoryManager.currentConsumable as BombConsumeableItem;  //类型转换
            GameObject activeModleBomb = Instantiate(bombItem.liveBombModle, rightHandSlot.transform.position, cameraHandler.cameraPivotTransform.rotation);            //实例化

            activeModleBomb.transform.rotation = Quaternion.Euler(cameraHandler.cameraPivotTransform.localEulerAngles.x, characterManager.transform.localEulerAngles.y, 0);  //控制炸弹的朝向
            if (cameraHandler.currentLockOnTarger != null)
            {
                activeModleBomb.transform.LookAt(cameraHandler.currentLockOnTarger.transform);
            }

            BombDamageCollider bombDamageCollider = activeModleBomb.GetComponentInChildren<BombDamageCollider>();   //获取伤害碰撞组件
            //给予友军id,后用于友伤判断:
            bombDamageCollider.teamIDNumber = characterStatsManager.teamIDNumber;

            //给予爆炸半径与爆炸伤害伤害属性:
            bombDamageCollider.explosiveRadius = bombItem.explosiveRadius;
            bombDamageCollider.explosiveDamage = bombItem.baseDamage;
            bombDamageCollider.explosiveSplashDamage = bombItem.explosiveDamage;

            bombDamageCollider.bombRigidbody.AddForce(activeModleBomb.transform.forward * bombItem.forwardVelocity);       //施加向前的力
            bombDamageCollider.bombRigidbody.AddForce(activeModleBomb.transform.up * bombItem.upwardVelocity);                //施加向上的力

            print(characterInventoryManager.rightWeapon == null);
            //持有武器:
            if(characterInventoryManager.rightWeapon!= null && characterInventoryManager.rightWeapon.isUnarmed == false)
            {
                characterAnimatorManager.anim.SetBool("holdWeapon", true);
            }
        }
        #endregion
    }
}
