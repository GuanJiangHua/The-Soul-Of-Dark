using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "搭箭动作", menuName = "游戏物品/新建按键动作/(左键按住)搭箭动作")]
    public class DrawArrowAction : ItemAction
    {
        public override void PerformAction(PlayerManager player)
        {
            if (player.isInteracting)
                return;
            //仍有耐力:
            if (player.playerStateManager.currentStamina <= 0)
                return;

            //是否在拿着箭:
            if (!player.isHoldingArrow && player.isTwoHandingWeapon)
            {
                //如果我们有弹药
                if (player.playerInventoryManager.currentBow != null)
                {
                    DrawArrowActio(player.playerInventoryManager.currentBow , player);
                    player.playerCombatManager.currAmmo = player.playerInventoryManager.currentBow;
                }
                else if (player.playerInventoryManager.spareBow != null)
                {
                    DrawArrowActio(player.playerInventoryManager.spareBow , player);
                    player.playerCombatManager.currAmmo = player.playerInventoryManager.spareBow;
                }
                else
                {
                    //否则播放动画以表明我们没有弹药
                    player.playerAnimatorManager.PlayTargetAnimation("Shrug", true);
                }
                //实例化箭头
                //当我们释放时射箭
            }
        }

        private void DrawArrowActio(RangedAmmoItem ammoItem , PlayerManager player)
        {
            //播放人物拉弓动画
            player.isHoldingArrow = true;
            player.playerAnimatorManager.PlayTargetAnimation("Bow_TH_Draw_01", true);
            GameObject loadedArrow = Instantiate(ammoItem.loadedItemModel, player.playerWeaponSlotManger.leftHandSlot.transform);
            loadedArrow.transform.localPosition = Vector3.zero;

            Destroy(player.playerEffectsManager.currentRangeFX);

            player.playerEffectsManager.currentRangeFX = loadedArrow;
            //播放弓被拉开动画:
            Animator bowAnimator = player.playerWeaponSlotManger.rightHandSlot.GetComponentInChildren<Animator>();
            bowAnimator.SetBool("isDrawn", true);
            bowAnimator.Play("Bow_HT_Draw");
        }
    }
}