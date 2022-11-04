using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "��ը����Ϣ" , menuName = "��Ϸ��Ʒ/����Ʒ/ը��")]
    public class BombConsumeableItem : ConsumableItem
    {
        [Header("�����ٶ�:")]
        public int upwardVelocity;  //�����ٶ�;
        public int forwardVelocity; //��ǰ�ٶ�;
        public int bombMass;        //��������;

        [Header("����ײ����ģ��:")]
        public GameObject liveBombModle;
        [Header("�˺�:")]
        public int baseDamage = 200;
        public int explosiveDamage = 75;
        [Header("��ը�뾶:")]
        public int explosiveRadius = 1;

        public override void AttemptToConsumeItem(PlayerAnimatorManager playerAnimatorManager, PlayerWeaponSlotManger weaponSlotManger, PlayerEffectsManager playerEffectsManager)
        {
            if(currentItemAmount > 0)
            {
                currentItemAmount -= 1;
                weaponSlotManger.rightHandSlot.UnloadWeaponAndDestroy();  //ж������ģ��;
                playerAnimatorManager.PlayTargetAnimation(cosumeAnimatrion, true);
                GameObject bombModel = Instantiate(itemModel, weaponSlotManger.rightHandSlot.transform);
                bombModel.transform.localPosition = Vector3.zero;
                bombModel.transform.localRotation = Quaternion.identity;

                playerEffectsManager.instantiatedFXModel = bombModel;
            }
            else
            {
                playerAnimatorManager.PlayTargetAnimation("Take-Item-Failed", true);
            }
        }
    }
}
