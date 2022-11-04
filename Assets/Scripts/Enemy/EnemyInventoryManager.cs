using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class EnemyInventoryManager : CharacterInventoryManager
    {
        private void Start()
        {
            if (characterManager.isPutWeapons)
            {
                PutWeaponsHolderOfSlot();
            }
            else
            {
                characterWeaponSlotManger.LoadWeaponHolderOfSlot(); 
            }
        }

        //��������������:
        private void PutWeaponsHolderOfSlot()
        {
            if (leftWeapon != null)
            {
                //�����������Ų�λ�ж�:   

                //����������:
                if(leftWeapon.weaponType == WeaponType.BigSword         //��
                    || leftWeapon.weaponType == WeaponType.Spear           //��ì
                    || leftWeapon.weaponType == WeaponType.SpellCaster  //����
                )
                {
                    characterWeaponSlotManger.backSlot.LoadWeaponModel(leftWeapon);
                }
                //������Ʋ�:
                else if(leftWeapon.weaponType == WeaponType.Shield          //�ж�
                    || leftWeapon.weaponType == WeaponType.SmallShield     //С��
                )
                {
                    characterWeaponSlotManger.backShieldSlot.LoadWeaponModel(leftWeapon);
                }
                //����������:
                else if (leftWeapon.weaponType == WeaponType.StraightSword //ֱ��
                    || leftWeapon.weaponType == WeaponType.Dagger                 //ذ��
                    || leftWeapon.weaponType == WeaponType.BluntInstrument  //����
                    || leftWeapon.weaponType == WeaponType.SpellCaster          //�漣��ý
                )
                {
                    characterWeaponSlotManger.waistWeaponSlot.LoadWeaponModel(leftWeapon);
                }
                //���󹭼���:
                else if(leftWeapon.weaponType == WeaponType.Bow)
                {
                    characterWeaponSlotManger.backBowSlot.LoadWeaponModel(leftWeapon);
                }
            }

            if(rightWeapon != null)
            {
                //����������:
                if (rightWeapon.weaponType == WeaponType.BigSword         //��
                    || rightWeapon.weaponType == WeaponType.Spear           //��ì
                    || rightWeapon.weaponType == WeaponType.SpellCaster  //����
                )
                {
                    characterWeaponSlotManger.backSlot.LoadWeaponModel(rightWeapon);
                }
                //����������:
                else if (rightWeapon.weaponType == WeaponType.StraightSword //ֱ��
                    || rightWeapon.weaponType == WeaponType.Dagger                 //ذ��
                    || rightWeapon.weaponType == WeaponType.BluntInstrument  //����
                    || rightWeapon.weaponType == WeaponType.SpellCaster          //�漣��ý
                )
                {
                    characterWeaponSlotManger.waistWeaponSlot.LoadWeaponModel(rightWeapon);
                }
                //���󹭼���:
                else if (rightWeapon.weaponType == WeaponType.Bow)
                {
                    characterWeaponSlotManger.backBowSlot.LoadWeaponModel(rightWeapon);
                }
            }
        }
    }
}
