using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "开火动作", menuName = "游戏物品/新建按键动作/(左键抬起)开火动作")]
    public class FierArrowAction : ItemAction
    {
        public override void PerformAction(PlayerManager player)
        {
            if (player.isTwoHandingWeapon == false)
                return;
            if (player.canFire != true)
                return;

            RangedAmmoItem currAmmo = player.playerCombatManager.currAmmo;
            //创建实时箭头特定位置:
            ArrowInstantiationLocation arrowInstantiationLocation = player.playerWeaponSlotManger.rightHandSlot.GetComponentInChildren<ArrowInstantiationLocation>();
            //实例化弹丸:
            GameObject ammo = Instantiate(currAmmo.liveAmmoModel, arrowInstantiationLocation.transform.position, Quaternion.identity);
            Rigidbody ammoRigidbody = ammo.GetComponentInChildren<Rigidbody>();
            //销毁先前加载的箭头 FX
            Destroy(player.playerEffectsManager.currentRangeFX);
            //设置实箭伤害
            WeaponItem bow = player.playerInventoryManager.rightWeapon;
            RangedProjectileDamageCollider rangedProjectileDamageCollider = ammo.GetComponentInChildren<RangedProjectileDamageCollider>();
            rangedProjectileDamageCollider.physicalDamage = DamageCalculation(currAmmo.physicalDamage, bow.strengthAddition, bow.dexterityAddition, 0, 0, player.playerStateManager); ;
            rangedProjectileDamageCollider.fireDamage =DamageCalculation(currAmmo.fierDamage, 0 , 0 , bow.intelligenceAddition , bow.faithAddition , player.playerStateManager);                  //火焰伤害
            rangedProjectileDamageCollider.magicDamage =DamageCalculation(currAmmo.magicDamage , 0 , 0, bow.intelligenceAddition, 0 , player.playerStateManager);             //魔力伤害
            rangedProjectileDamageCollider.darkDamage = DamageCalculation(currAmmo.darkDamage , bow.strengthAddition , bow.dexterityAddition , bow.intelligenceAddition , bow.faithAddition , player.playerStateManager);               //暗属性伤害
            rangedProjectileDamageCollider.lightningDamage = currAmmo.lightningDamage;      //雷电伤害
            //设置箭头韧性伤害:
            rangedProjectileDamageCollider.poiseBreak = currAmmo.poiseBreak;
            
            rangedProjectileDamageCollider.chaeacterManager = player;
            rangedProjectileDamageCollider.teamIDNumber = player.playerStateManager.teamIDNumber;
            rangedProjectileDamageCollider.ammoItem = currAmmo;

            //给出弹药速度
            ammoRigidbody.transform.parent = null;
            Vector3 rotationVector3 = player.transform.eulerAngles;
            rotationVector3.z = 0;
            if (player.isAiming)
            {
                Ray ray = player.cameraHandler.cameraObject.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    //ammoRigidbody.transform.LookAt(hit.point);
                    //rotationVector3 = player.cameraHandler.transform.eulerAngles;
                    Debug.Log("瞄准中射击:");
                    ammoRigidbody.transform.rotation = Quaternion.Euler(player.cameraHandler.cameraPivotTransform.eulerAngles.x, rotationVector3.y, 0);
                }
                else
                {
                    //rotationVector3 = player.cameraHandler.transform.eulerAngles;
                    ammoRigidbody.transform.rotation = Quaternion.Euler(player.cameraHandler.cameraPivotTransform.eulerAngles.x, rotationVector3.y, 0);
                }
            }
            else
            {
                if (player.cameraHandler.currentLockOnTarger != null)
                {
                    ammoRigidbody.transform.rotation = Quaternion.LookRotation(player.transform.forward);
                }
                else
                {
                    ammoRigidbody.transform.rotation = Quaternion.Euler(player.cameraHandler.cameraPivotTransform.eulerAngles.x, rotationVector3.y, 0);
                }
            }

            ammoRigidbody.AddForce(ammoRigidbody.transform.forward * currAmmo.forwardVelocity);
            ammoRigidbody.AddForce(ammoRigidbody.transform.up * currAmmo.upwardVelocity);
            ammoRigidbody.useGravity = currAmmo.useGravity;
            ammoRigidbody.mass = currAmmo.ammoMass;


            Destroy(ammo);

            //播放动画弓发射
            Animator bowAnimator = player.playerWeaponSlotManger.rightHandSlot.GetComponentInChildren<Animator>();
            bowAnimator.SetBool("isDrawn", false);
            bowAnimator.Play("Bow_HT_Fire");
            //播放角色的开火动作:
            player.playerAnimatorManager.PlayTargetAnimation("Bow_TH_Fire_01", true);
            player.playerAnimatorManager.anim.SetBool("canFire", false);
            player.isHoldingArrow = false;
        }

        private int DamageCalculation(int damage, float strengthAddition, float dexterityAddition, float intelligenceAddition, float faithAddition , CharacterStatsManager characterStatsManager)
        {
            //属性等级:
            int strengthLevel = characterStatsManager.strengthLevel;
            int dexterityLevel = characterStatsManager.dexterityLevel;
            int intelligenceLevel = characterStatsManager.intelligenceLevel;
            int faithLevel = characterStatsManager.faithLevel;
            //最终伤害:
            float finalDamage = damage + damage * (strengthAddition * strengthLevel / 20);
            finalDamage += damage * (dexterityAddition * dexterityLevel / 20);
            finalDamage += damage * (intelligenceAddition * intelligenceLevel / 20);
            finalDamage += damage * (faithAddition * faithLevel / 20);

            return Mathf.RoundToInt(finalDamage);
        }
    }
}