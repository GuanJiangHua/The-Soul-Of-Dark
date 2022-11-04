using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "�½��ǻ���Ϣ", menuName = "��Ϸ��Ʒ/����Ʒ/�ǻ�")]
    public class AshesItem : ConsumableItem
    {
        [Header("��������:")]
        public string shopownerName;
        //�����ǻҵ�ʱ���õ������ƶ�ȡ�浵�ļ�����ȡ�����̵����Ʒ��Ϣ��ӵ��������̵����Ķ�
        public override void AttemptToConsumeItem(PlayerAnimatorManager playerAnimatorManager, PlayerWeaponSlotManger weaponSlotManger, PlayerEffectsManager playerEffectsManager)
        {
            playerAnimatorManager.PlayTargetAnimation("Shrug", true);
        }
    }
}