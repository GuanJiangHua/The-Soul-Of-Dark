using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "ħ��ʩ������", menuName = "��Ϸ��Ʒ/�½���������/(�������)ħ��ʩ������")]
    public class MagicSpellAction : ItemAction
    {
        public override void PerformAction(PlayerManager player)
        {
            if (player.isInteracting)
                return;

            SpellItem currentSpell = player.playerInventoryManager.currentSpell;
            if (currentSpell != null && currentSpell.isMagicSpell)
            {
                //�����ȼ��Ƿ�����
                bool isMeetCondition = player.playerStateManager.intelligenceLevel >= currentSpell.requiredIntelligenceLevel && player.playerStateManager.faithLevel >= currentSpell.requiredFaithLevel;
                //ʣ�෨��ֵ���ڵ��ڵ�ǰ�����ķ������:
                if (player.playerStateManager.currentFocusPoints >= currentSpell.focusPointCost && isMeetCondition)
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