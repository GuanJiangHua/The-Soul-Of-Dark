using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "新建苔藓球信息", menuName = "游戏物品/消耗品/苔藓球")]
    public class ClumpConsumeableItem : ConsumableItem
    {
        [Header("治愈音效:")]
        public string currAudio;
        [Header("治愈特效预制体:")]
        public GameObject currFX;
        [Header("空瓶模型:")]
        public GameObject emptyModel;
        [Header("治愈效果特效:")]
        public bool currPoison;
        public override void AttemptToConsumeItem(PlayerAnimatorManager playerAnimatorManager, PlayerWeaponSlotManger weaponSlotManger, PlayerEffectsManager playerEffectsManager)
        {
            base.AttemptToConsumeItem(playerAnimatorManager, weaponSlotManger, playerEffectsManager);
            if (currentItemAmount > 0)
            {
                playerAnimatorManager.PlayTargetAnimation(cosumeAnimatrion, isInteracting, true);
                currentItemAmount -= 1;

                playerAnimatorManager.GetComponentInParent<InputHandler>().sneakMove_Input = true;
                //设置恢复粒子效果，设置待恢复量:
                playerEffectsManager.currentAudio = currAudio;
                playerEffectsManager.currentParticleFX = currFX;
                playerEffectsManager.effectPosition = weaponSlotManger.rightHandSlot.transform;

                //实例化团块(苔藓球)并播放动画
                GameObject mossBall = Instantiate(itemModel, weaponSlotManger.rightHandSlot.transform);
                playerEffectsManager.instantiatedFXModel = mossBall;
                weaponSlotManger.rightHandSlot.UnloadWeapon();  //卸载武器模型;
                //治疗效果:(解除中毒,解除冰冻等)
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
                weaponSlotManger.rightHandSlot.UnloadWeapon();  //卸载武器模型;
                playerAnimatorManager.PlayTargetAnimation("Potion_Empty", true);
            }
        }
    }
}
