using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class ConsumableItem : Item
    {
        [Header("物品数量:")]
        public int maxItemAmount;   //最大物品数量;
        public int currentItemAmount;   //当前物品数量;
        [Header("物品模型:")]
        public GameObject itemModel;
        [Header("物品使用动画:")]
        public string cosumeAnimatrion; //使用物品时播放的动画;
        public bool isInteracting;             //使用时是否是交互状态;
        [Header("物品描述:")]
        [TextArea] public string consumableDescription;
        [Header("功能描述:")]
        [TextArea] public string functionDescription;
        //尝试使用物品:
        public virtual void AttemptToConsumeItem(PlayerAnimatorManager playerAnimatorManager , PlayerWeaponSlotManger weaponSlotManger , PlayerEffectsManager playerEffectsManager)
        {
            playerEffectsManager.LoadAndDeleteModel();
        }
    }
}
