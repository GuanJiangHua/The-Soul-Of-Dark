using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "Ԫ�ؽᾧ��Ϣ", menuName = "��Ϸ��Ʒ/����Ʒ/Ԫ�ؽᾧ")]
    public class RedCrystalElementItem : ConsumableItem
    {
        //�����ǻҵ�ʱ���õ������ƶ�ȡ�浵�ļ�����ȡ�����̵����Ʒ��Ϣ��ӵ��������̵����Ķ�
        public override void AttemptToConsumeItem(PlayerAnimatorManager playerAnimatorManager, PlayerWeaponSlotManger weaponSlotManger, PlayerEffectsManager playerEffectsManager)
        {
            playerAnimatorManager.PlayTargetAnimation("Shrug", true);
        }
    }
}