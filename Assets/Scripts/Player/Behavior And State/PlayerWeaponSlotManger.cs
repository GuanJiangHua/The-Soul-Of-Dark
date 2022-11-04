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
            //������ײ�Ŀ�ʼ�����:
            animatorManager.weaponCollisionStartEvent.AddListener(base.OpenDamageCollider);
            animatorManager.weaponCollisionEntEvent.AddListener(base.CloseDamageCollider);

            //�����������Խ����������۳�:
            animatorManager.grantPoiseBonusEvent.AddListener(base.GrantWeaponAttackingPoiseBonus);
            animatorManager.resetPoiseBonusEvent.AddListener(base.ResetWeaponAttackingPoiseBonus);

            LoadWeaponHolderOfSlot();
        }

        //����������
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

        #region Ͷ������Ʒը��
        public void SucessfullyThrowFireBomb()
        {
            Destroy(playerEffectsManager.instantiatedFXModel);

            BombConsumeableItem bombItem = characterInventoryManager.currentConsumable as BombConsumeableItem;  //����ת��
            GameObject activeModleBomb = Instantiate(bombItem.liveBombModle, rightHandSlot.transform.position, cameraHandler.cameraPivotTransform.rotation);            //ʵ����

            activeModleBomb.transform.rotation = Quaternion.Euler(cameraHandler.cameraPivotTransform.localEulerAngles.x, characterManager.transform.localEulerAngles.y, 0);  //����ը���ĳ���
            if (cameraHandler.currentLockOnTarger != null)
            {
                activeModleBomb.transform.LookAt(cameraHandler.currentLockOnTarger.transform);
            }

            BombDamageCollider bombDamageCollider = activeModleBomb.GetComponentInChildren<BombDamageCollider>();   //��ȡ�˺���ײ���
            //�����Ѿ�id,�����������ж�:
            bombDamageCollider.teamIDNumber = characterStatsManager.teamIDNumber;

            //���豬ը�뾶�뱬ը�˺��˺�����:
            bombDamageCollider.explosiveRadius = bombItem.explosiveRadius;
            bombDamageCollider.explosiveDamage = bombItem.baseDamage;
            bombDamageCollider.explosiveSplashDamage = bombItem.explosiveDamage;

            bombDamageCollider.bombRigidbody.AddForce(activeModleBomb.transform.forward * bombItem.forwardVelocity);       //ʩ����ǰ����
            bombDamageCollider.bombRigidbody.AddForce(activeModleBomb.transform.up * bombItem.upwardVelocity);                //ʩ�����ϵ���

            print(characterInventoryManager.rightWeapon == null);
            //��������:
            if(characterInventoryManager.rightWeapon!= null && characterInventoryManager.rightWeapon.isUnarmed == false)
            {
                characterAnimatorManager.anim.SetBool("holdWeapon", true);
            }
        }
        #endregion
    }
}
