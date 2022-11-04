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

        //ÊÕÆðÓÒ×óÊÖÎäÆ÷:
        private void PutWeaponsHolderOfSlot()
        {
            if (leftWeapon != null)
            {
                //×óÊÖÎäÆ÷°²·Å²ÛÎ»ÅÐ¶Ï:   

                //±³ºóÎäÆ÷²Û:
                if(leftWeapon.weaponType == WeaponType.BigSword         //´ó½£
                    || leftWeapon.weaponType == WeaponType.Spear           //³¤Ã¬
                    || leftWeapon.weaponType == WeaponType.SpellCaster  //·¨ÕÈ
                )
                {
                    characterWeaponSlotManger.backSlot.LoadWeaponModel(leftWeapon);
                }
                //±³ºó¶ÜÅÆ²Û:
                else if(leftWeapon.weaponType == WeaponType.Shield          //ÖÐ¶Ü
                    || leftWeapon.weaponType == WeaponType.SmallShield     //Ð¡¶Ü
                )
                {
                    characterWeaponSlotManger.backShieldSlot.LoadWeaponModel(leftWeapon);
                }
                //Ñü¼äÎäÆ÷²Û:
                else if (leftWeapon.weaponType == WeaponType.StraightSword //Ö±½£
                    || leftWeapon.weaponType == WeaponType.Dagger                 //Ø°Ê×
                    || leftWeapon.weaponType == WeaponType.BluntInstrument  //¶ÛÆ÷
                    || leftWeapon.weaponType == WeaponType.SpellCaster          //Ææ¼£´¥Ã½
                )
                {
                    characterWeaponSlotManger.waistWeaponSlot.LoadWeaponModel(leftWeapon);
                }
                //±³ºó¹­¼ý²Û:
                else if(leftWeapon.weaponType == WeaponType.Bow)
                {
                    characterWeaponSlotManger.backBowSlot.LoadWeaponModel(leftWeapon);
                }
            }

            if(rightWeapon != null)
            {
                //±³ºóÎäÆ÷²Û:
                if (rightWeapon.weaponType == WeaponType.BigSword         //´ó½£
                    || rightWeapon.weaponType == WeaponType.Spear           //³¤Ã¬
                    || rightWeapon.weaponType == WeaponType.SpellCaster  //·¨ÕÈ
                )
                {
                    characterWeaponSlotManger.backSlot.LoadWeaponModel(rightWeapon);
                }
                //Ñü¼äÎäÆ÷²Û:
                else if (rightWeapon.weaponType == WeaponType.StraightSword //Ö±½£
                    || rightWeapon.weaponType == WeaponType.Dagger                 //Ø°Ê×
                    || rightWeapon.weaponType == WeaponType.BluntInstrument  //¶ÛÆ÷
                    || rightWeapon.weaponType == WeaponType.SpellCaster          //Ææ¼£´¥Ã½
                )
                {
                    characterWeaponSlotManger.waistWeaponSlot.LoadWeaponModel(rightWeapon);
                }
                //±³ºó¹­¼ý²Û:
                else if (rightWeapon.weaponType == WeaponType.Bow)
                {
                    characterWeaponSlotManger.backBowSlot.LoadWeaponModel(rightWeapon);
                }
            }
        }
    }
}
