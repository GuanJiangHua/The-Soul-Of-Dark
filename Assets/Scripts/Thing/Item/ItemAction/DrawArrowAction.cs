using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "�������", menuName = "��Ϸ��Ʒ/�½���������/(�����ס)�������")]
    public class DrawArrowAction : ItemAction
    {
        public override void PerformAction(PlayerManager player)
        {
            if (player.isInteracting)
                return;
            //��������:
            if (player.playerStateManager.currentStamina <= 0)
                return;

            //�Ƿ������ż�:
            if (!player.isHoldingArrow && player.isTwoHandingWeapon)
            {
                //��������е�ҩ
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
                    //���򲥷Ŷ����Ա�������û�е�ҩ
                    player.playerAnimatorManager.PlayTargetAnimation("Shrug", true);
                }
                //ʵ������ͷ
                //�������ͷ�ʱ���
            }
        }

        private void DrawArrowActio(RangedAmmoItem ammoItem , PlayerManager player)
        {
            //����������������
            player.isHoldingArrow = true;
            player.playerAnimatorManager.PlayTargetAnimation("Bow_TH_Draw_01", true);
            GameObject loadedArrow = Instantiate(ammoItem.loadedItemModel, player.playerWeaponSlotManger.leftHandSlot.transform);
            loadedArrow.transform.localPosition = Vector3.zero;

            Destroy(player.playerEffectsManager.currentRangeFX);

            player.playerEffectsManager.currentRangeFX = loadedArrow;
            //���Ź�����������:
            Animator bowAnimator = player.playerWeaponSlotManger.rightHandSlot.GetComponentInChildren<Animator>();
            bowAnimator.SetBool("isDrawn", true);
            bowAnimator.Play("Bow_HT_Draw");
        }
    }
}