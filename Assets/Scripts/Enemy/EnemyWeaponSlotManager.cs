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
            //武器碰撞的开始与结束:
            animatorManager.weaponCollisionStartEvent.AddListener(base.OpenDamageCollider);
            animatorManager.weaponCollisionEntEvent.AddListener(base.CloseDamageCollider);

            //武器出手韧性奖励的添加与扣除:
            animatorManager.grantPoiseBonusEvent.AddListener(base.GrantWeaponAttackingPoiseBonus);
            animatorManager.resetPoiseBonusEvent.AddListener(base.ResetWeaponAttackingPoiseBonus);
        }
    }
}
