using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "�񵲶���", menuName = "��Ϸ��Ʒ/�½���������/(F����ס)�񵲶���")]
    public class BlockAction : ItemAction
    {
        public override void PerformAction(PlayerManager player)
        {
            BlockingAction(player);
        }

        //ִ�оٶܸ񵲶���: 
        private void BlockingAction(PlayerManager player)
        {
            if (player.isInteracting) return;
            if (player.isBlocking) return;

            player.isBlocking = true;

            WeaponItem rightWeapon = player.playerInventoryManager.rightWeapon;
            WeaponItem leftWeapon = player.playerInventoryManager.leftWeapon;
            bool isOffHandWeapon = leftWeapon != null && leftWeapon.weaponType != WeaponType.PyromancyCaster && leftWeapon.weaponType != WeaponType.SpellCaster && leftWeapon.weaponType != WeaponType.Unarmed;
            //˫��״̬:(����������Ϊ��)
            if (player.inputHandler.twoHandFlag || leftWeapon.isUnarmed)
            {
                string blockAnim = rightWeapon.mainHandBlock; //ʹ�������������������񵲶���
                player.playerAnimatorManager.PlayTargetAnimation(blockAnim, false, true);
            }
            //����������Ϊ�գ����Ƕ�������
            else if (leftWeapon != null && (leftWeapon.weaponType == WeaponType.Shield || leftWeapon.weaponType == WeaponType.SmallShield))
            {
                player.playerAnimatorManager.PlayTargetAnimation("Block_Start", false, true);
            }
            //����������Ϊ�գ����ǽ�ս����
            else if (isOffHandWeapon)
            {
                string blockAnim = leftWeapon.offHandBlock; //ʹ�����������ĸ������񵲶���
                player.playerAnimatorManager.PlayTargetAnimation(blockAnim, false, true);
            }
            player.playerEquipmentManager.OpenBlockingCollider();
        }
    }
}