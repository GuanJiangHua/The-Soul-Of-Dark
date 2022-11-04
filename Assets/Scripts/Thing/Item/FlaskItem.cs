using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "新建元素瓶信息", menuName = "游戏物品/消耗品/元素瓶")]
    public class FlaskItem : ConsumableItem
    {
        [Header("回复类型:")]
        public bool estusFlask;     //元素瓶
        public bool ashenFlask;    //元素灰瓶
        [Header("回复量:")]
        public int healthRecoveryAmount;    //生命值回复量
        public int focusPointsRecoveryAmount; //蓝条回复量
        [Header("回复特效预制体:")]
        public GameObject recoveryFX;
        [Header("空瓶模型:")]
        public GameObject emptyModel;
        [Header("回复音效:")]
        public string healedAudio;

        public override void AttemptToConsumeItem(PlayerAnimatorManager playerAnimatorManager , PlayerWeaponSlotManger weaponSlotManger , PlayerEffectsManager playerEffectsManager)
        {
            base.AttemptToConsumeItem(playerAnimatorManager, weaponSlotManger , playerEffectsManager);
            if (currentItemAmount > 0)
            {
                playerAnimatorManager.PlayTargetAnimation(cosumeAnimatrion, isInteracting, true);
                currentItemAmount -= 1;
                
                playerAnimatorManager.GetComponentInParent<InputHandler>().sneakMove_Input = true;
                //设置恢复粒子效果，设置待恢复量:
                playerEffectsManager.currentParticleFX = recoveryFX;
                playerEffectsManager.amountToBeHealed = healthRecoveryAmount + healthRecoveryAmount * playerAnimatorManager.GetComponent<PlayerManager>().restoreHealthLevel;
                playerEffectsManager.amountToBeFocus = focusPointsRecoveryAmount + focusPointsRecoveryAmount * playerAnimatorManager.GetComponent<PlayerManager>().restoreHealthLevel;
                playerEffectsManager.effectPosition = weaponSlotManger.rightHandSlot.transform;
                playerEffectsManager.currentAudio = healedAudio;
                //实例化元素瓶并播放动画
                GameObject flask = Instantiate(itemModel, weaponSlotManger.rightHandSlot.transform);
                playerEffectsManager.instantiatedFXModel = flask;
                weaponSlotManger.rightHandSlot.UnloadWeapon();  //卸载武器模型;
                //如果回复没有被打断播放回复特效
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
