using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "�½�̦޺����Ϣ", menuName = "��Ϸ��Ʒ/����Ʒ/̦޺��")]
    public class ClumpConsumeableItem : ConsumableItem
    {
        [Header("������Ч:")]
        public string currAudio;
        [Header("������ЧԤ����:")]
        public GameObject currFX;
        [Header("��ƿģ��:")]
        public GameObject emptyModel;
        [Header("����Ч����Ч:")]
        public bool currPoison;
        public override void AttemptToConsumeItem(PlayerAnimatorManager playerAnimatorManager, PlayerWeaponSlotManger weaponSlotManger, PlayerEffectsManager playerEffectsManager)
        {
            base.AttemptToConsumeItem(playerAnimatorManager, weaponSlotManger, playerEffectsManager);
            if (currentItemAmount > 0)
            {
                playerAnimatorManager.PlayTargetAnimation(cosumeAnimatrion, isInteracting, true);
                currentItemAmount -= 1;

                playerAnimatorManager.GetComponentInParent<InputHandler>().sneakMove_Input = true;
                //���ûָ�����Ч�������ô��ָ���:
                playerEffectsManager.currentAudio = currAudio;
                playerEffectsManager.currentParticleFX = currFX;
                playerEffectsManager.effectPosition = weaponSlotManger.rightHandSlot.transform;

                //ʵ�����ſ�(̦޺��)�����Ŷ���
                GameObject mossBall = Instantiate(itemModel, weaponSlotManger.rightHandSlot.transform);
                playerEffectsManager.instantiatedFXModel = mossBall;
                weaponSlotManger.rightHandSlot.UnloadWeapon();  //ж������ģ��;
                //����Ч��:(����ж�,���������)
                if (currPoison)
                {
                    playerEffectsManager.poisonBuildup = 0;
                    playerEffectsManager.poisonAmount = 0;
                }
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
