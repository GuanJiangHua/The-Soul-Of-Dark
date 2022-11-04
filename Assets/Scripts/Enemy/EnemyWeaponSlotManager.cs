using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class EnemyWeaponSlotManager : CharacterWeaponSlotManager
    {
        protected override void Awake()
        {
            base.Awake();

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
        }
    }
}
