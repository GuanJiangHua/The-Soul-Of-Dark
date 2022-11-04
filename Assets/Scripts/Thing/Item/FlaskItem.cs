using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "�½�Ԫ��ƿ��Ϣ", menuName = "��Ϸ��Ʒ/����Ʒ/Ԫ��ƿ")]
    public class FlaskItem : ConsumableItem
    {
        [Header("�ظ�����:")]
        public bool estusFlask;     //Ԫ��ƿ
        public bool ashenFlask;    //Ԫ�ػ�ƿ
        [Header("�ظ���:")]
        public int healthRecoveryAmount;    //����ֵ�ظ���
        public int focusPointsRecoveryAmount; //�����ظ���
        [Header("�ظ���ЧԤ����:")]
        public GameObject recoveryFX;
        [Header("��ƿģ��:")]
        public GameObject emptyModel;
        [Header("�ظ���Ч:")]
        public string healedAudio;

        public override void AttemptToConsumeItem(PlayerAnimatorManager playerAnimatorManager , PlayerWeaponSlotManger weaponSlotManger , PlayerEffectsManager playerEffectsManager)
        {
            base.AttemptToConsumeItem(playerAnimatorManager, weaponSlotManger , playerEffectsManager);
            if (currentItemAmount > 0)
            {
                playerAnimatorManager.PlayTargetAnimation(cosumeAnimatrion, isInteracting, true);
                currentItemAmount -= 1;
                
                playerAnimatorManager.GetComponentInParent<InputHandler>().sneakMove_Input = true;
                //���ûָ�����Ч�������ô��ָ���:
                playerEffectsManager.currentParticleFX = recoveryFX;
                playerEffectsManager.amountToBeHealed = healthRecoveryAmount + healthRecoveryAmount * playerAnimatorManager.GetComponent<PlayerManager>().restoreHealthLevel;
                playerEffectsManager.amountToBeFocus = focusPointsRecoveryAmount + focusPointsRecoveryAmount * playerAnimatorManager.GetComponent<PlayerManager>().restoreHealthLevel;
                playerEffectsManager.effectPosition = weaponSlotManger.rightHandSlot.transform;
                playerEffectsManager.currentAudio = healedAudio;
                //ʵ����Ԫ��ƿ�����Ŷ���
                GameObject flask = Instantiate(itemModel, weaponSlotManger.rightHandSlot.transform);
                playerEffectsManager.instantiatedFXModel = flask;
                weaponSlotManger.rightHandSlot.UnloadWeapon();  //ж������ģ��;
                //����ظ�û�б���ϲ��Żظ���Ч
            }
            else
            {
                GameObject flask = Instantiate(emptyModel, weaponSlotManger.rightHandSlot.transform);
                playerEffectsManager.instantiatedFXModel = flask;
                weaponSlotManger.rightHandSlot.UnloadWeapon();  //ж������ģ��;
                playerAnimatorManager.PlayTargetAnimation("Potion_Empty", true);
            }
        }
    }
}
