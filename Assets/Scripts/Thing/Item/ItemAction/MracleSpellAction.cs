using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "�漣ʩ������", menuName = "��Ϸ��Ʒ/�½���������/(�����ס)�漣ʩ������")]
    public class MracleSpellAction : ItemAction
    {
        public override void PerformAction(PlayerManager player)
        {
            SpellItem currentSpell = player.playerInventoryManager.currentSpell;
            if (currentSpell != null && currentSpell.isFaithSpell == true)
            {
                //playerWeaponSlotManger.attackingWeapon = weapon;
                bool isMeetCondition = player.playerStateManager.intelligenceLevel >= currentSpell.requiredIntelligenceLevel && player.playerStateManager.faithLevel >= currentSpell.requiredFaithLevel;
                //ʣ�෨��ֵ���ڵ��ڵ�ǰ�����ķ������:
                if (player.playerStateManager.currentFocusPoints >= player.playerInventoryManager.currentSpell.focusPointCost && isMeetCondition)
                {
                    //ʩ���漣����:(��������)
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