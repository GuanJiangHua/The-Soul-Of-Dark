using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "������", menuName = "��Ϸ��Ʒ/�½���������/(���̧��)������")]
    public class FierArrowAction : ItemAction
    {
        public override void PerformAction(PlayerManager player)
        {
            if (player.isTwoHandingWeapon == false)
                return;
            if (player.canFire != true)
                return;

            RangedAmmoItem currAmmo = player.playerCombatManager.currAmmo;
            //����ʵʱ��ͷ�ض�λ��:
            ArrowInstantiationLocation arrowInstantiationLocation = player.playerWeaponSlotManger.rightHandSlot.GetComponentInChildren<ArrowInstantiationLocation>();
            //ʵ��������:
            GameObject ammo = Instantiate(currAmmo.liveAmmoModel, arrowInstantiationLocation.transform.position, Quaternion.identity);
            Rigidbody ammoRigidbody = ammo.GetComponentInChildren<Rigidbody>();
            //������ǰ���صļ�ͷ FX
            Destroy(player.playerEffectsManager.currentRangeFX);
            //����ʵ���˺�
            WeaponItem bow = player.playerInventoryManager.rightWeapon;
            RangedProjectileDamageCollider rangedProjectileDamageCollider = ammo.GetComponentInChildren<RangedProjectileDamageCollider>();
            rangedProjectileDamageCollider.physicalDamage = DamageCalculation(currAmmo.physicalDamage, bow.strengthAddition, bow.dexterityAddition, 0, 0, player.playerStateManager); ;
            rangedProjectileDamageCollider.fireDamage =DamageCalculation(currAmmo.fierDamage, 0 , 0 , bow.intelligenceAddition , bow.faithAddition , player.playerStateManager);                  //�����˺�
            rangedProjectileDamageCollider.magicDamage =DamageCalculation(currAmmo.magicDamage , 0 , 0, bow.intelligenceAddition, 0 , player.playerStateManager);             //ħ���˺�
            rangedProjectileDamageCollider.darkDamage = DamageCalculation(currAmmo.darkDamage , bow.strengthAddition , bow.dexterityAddition , bow.intelligenceAddition , bow.faithAddition , player.playerStateManager);               //�������˺�
            rangedProjectileDamageCollider.lightningDamage = currAmmo.lightningDamage;      //�׵��˺�
            //���ü�ͷ�����˺�:
            rangedProjectileDamageCollider.poiseBreak = currAmmo.poiseBreak;
            
            rangedProjectileDamageCollider.chaeacterManager = player;
            rangedProjectileDamageCollider.teamIDNumber = player.playerStateManager.teamIDNumber;
            rangedProjectileDamageCollider.ammoItem = currAmmo;

            //������ҩ�ٶ�
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
                    Debug.Log("��׼�����:");
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

            //���Ŷ���������
            Animator bowAnimator = player.playerWeaponSlotManger.rightHandSlot.GetComponentInChildren<Animator>();
            bowAnimator.SetBool("isDrawn", false);
            bowAnimator.Play("Bow_HT_Fire");
            //���Ž�ɫ�Ŀ�����:
            player.playerAnimatorManager.PlayTargetAnimation("Bow_TH_Fire_01", true);
            player.playerAnimatorManager.anim.SetBool("canFire", false);
            player.isHoldingArrow = false;
        }

        private int DamageCalculation(int damage, float strengthAddition, float dexterityAddition, float intelligenceAddition, float faithAddition , CharacterStatsManager characterStatsManager)
        {
            //���Եȼ�:
            int strengthLevel = characterStatsManager.strengthLevel;
            int dexterityLevel = characterStatsManager.dexterityLevel;
            int intelligenceLevel = characterStatsManager.intelligenceLevel;
            int faithLevel = characterStatsManager.faithLevel;
            //�����˺�:
            float finalDamage = damage + damage * (strengthAddition * strengthLevel / 20);
            finalDamage += damage * (dexterityAddition * dexterityLevel / 20);
            finalDamage += damage * (intelligenceAddition * intelligenceLevel / 20);
            finalDamage += damage * (faithAddition * faithLevel / 20);

            return Mathf.RoundToInt(finalDamage);
        }
    }
}