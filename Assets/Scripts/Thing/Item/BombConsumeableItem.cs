using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "新炸弹信息" , menuName = "游戏物品/消耗品/炸弹")]
    public class BombConsumeableItem : ConsumableItem
    {
        [Header("弹丸速度:")]
        public int upwardVelocity;  //向上速度;
        public int forwardVelocity; //向前速度;
        public int bombMass;        //弹丸质量;

        [Header("可碰撞弹丸模型:")]
        public GameObject liveBombModle;
        [Header("伤害:")]
        public int baseDamage = 200;
        public int explosiveDamage = 75;
        [Header("爆炸半径:")]
        public int explosiveRadius = 1;

        public override void AttemptToConsumeItem(PlayerAnimatorManager playerAnimatorManager, PlayerWeaponSlotManger weaponSlotManger, PlayerEffectsManager playerEffectsManager)
        {
            if(currentItemAmount > 0)
            {
                currentItemAmount -= 1;
                weaponSlotManger.rightHandSlot.UnloadWeaponAndDestroy();  //卸载武器模型;
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
