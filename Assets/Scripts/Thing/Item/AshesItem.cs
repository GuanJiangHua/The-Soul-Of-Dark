using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "新建骨灰信息", menuName = "游戏物品/消耗品/骨灰")]
    public class AshesItem : ConsumableItem
    {
        [Header("店主名称:")]
        public string shopownerName;
        //交付骨灰的时候，用店主名称读取存档文件，获取到该商店的商品信息添加到给出的商店主哪儿
        public override void AttemptToConsumeItem(PlayerAnimatorManager playerAnimatorManager, PlayerWeaponSlotManger weaponSlotManger, PlayerEffectsManager playerEffectsManager)
        {
            playerAnimatorManager.PlayTargetAnimation("Shrug", true);
        }
    }
}