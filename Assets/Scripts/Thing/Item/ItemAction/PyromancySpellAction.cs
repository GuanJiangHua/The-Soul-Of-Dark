using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "咒术施法动作", menuName = "游戏物品/新建按键动作/(左键按下)咒术施法动作")]
    public class PyromancySpellAction : ItemAction
    {
        public override void PerformAction(PlayerManager player)
        {
            SpellItem currentSpell = player.playerInventoryManager.currentSpell;
            if (currentSpell != null && currentSpell.isPyroSpell == true)
            {
                //playerWeaponSlotManger.attackingWeapon = weapon;
                bool isMeetCondition = player.playerStateManager.intelligenceLevel >= currentSpell.requiredIntelligenceLevel && player.playerStateManager.faithLevel >= currentSpell.requiredFaithLevel;
                //剩余法力值大于等于当前法术的法力损耗:
                if (player.playerStateManager.currentFocusPoints >= player.playerInventoryManager.currentSpell.focusPointCost && isMeetCondition)
                {
                    //施放法术:(法术吟唱)
                    currentSpell.AttemptToCastSpell(player.playerAnimatorManager, player.playerStateManager, player.playerWeaponSlotManger, player.isUsingLeftHand);
                }
                else
                {
                    player.playerAnimatorManager.PlayTargetAnimation("Shrug", true);
                }
            }
        }
    }
}
