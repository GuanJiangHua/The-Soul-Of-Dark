using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class ConsumableItem : Item
    {
        [Header("��Ʒ����:")]
        public int maxItemAmount;   //�����Ʒ����;
        public int currentItemAmount;   //��ǰ��Ʒ����;
        [Header("��Ʒģ��:")]
        public GameObject itemModel;
        [Header("��Ʒʹ�ö���:")]
        public string cosumeAnimatrion; //ʹ����Ʒʱ���ŵĶ���;
        public bool isInteracting;             //ʹ��ʱ�Ƿ��ǽ���״̬;
        [Header("��Ʒ����:")]
        [TextArea] public string consumableDescription;
        [Header("��������:")]
        [TextArea] public string functionDescription;
        //����ʹ����Ʒ:
        public virtual void AttemptToConsumeItem(PlayerAnimatorManager playerAnimatorManager , PlayerWeaponSlotManger weaponSlotManger , PlayerEffectsManager playerEffectsManager)
        {
            playerEffectsManager.LoadAndDeleteModel();
        }
    }
}
