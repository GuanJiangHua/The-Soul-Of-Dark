using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "新建投掷类法术物品信息", menuName = "游戏物品/新建投掷类法术物品")]
    public class ProjectileSpell : SpellItem
    {
        [Header("弹丸伤害:")]
        public int physicalDamage = 25;
        public int fireDamage;                   //火焰伤害
        public int magicDamage;              //魔力伤害
        public int lightningDamage;         //雷电伤害
        public int darkDamage;                //黑暗伤害
        [Header("弹丸的刚体属性：")]
        public float projectileForwardVelocity;    //弹丸前进速度;
        public float projectileUpwartVelocity;  //向上的速度;
        public float projectileMass;
        public bool isEffectedByGravity;           //受重力影响;
        Rigidbody rigidbody;

        //施法中:
        public override void AttemptToCastSpell(AnimatorManager animatorHandler, CharacterStatsManager playerState , PlayerWeaponSlotManger weaponSlotManger , bool isLeftHanded)
        {
            
            if (animatorHandler.anim.GetBool("isInteracting") == true) return;
            base.AttemptToCastSpell(animatorHandler, playerState , weaponSlotManger , isLeftHanded);
            //左手施法:
            if (isLeftHanded)
            {
                //实例化施法过程的特效预制体;(在左手施法时:)
                GameObject instantiatedWarmUpSpellFX = Instantiate(spellWarmUpFX, weaponSlotManger.leftHandSlot.transform);
                //播放施法动画;(设置动作镜像)
                animatorHandler.PlayTargetAnimation(spellAnimation, true , false , true);
            }
            else
            {
                //实例化施法过程的特效预制体;
                GameObject instantiatedWarmUpSpellFX = Instantiate(spellWarmUpFX, weaponSlotManger.rightHandSlot.transform);
                //播放施法动画;
                animatorHandler.PlayTargetAnimation(spellAnimation, true);
            }
        }
        
        //施法成功:
        public override void SucessfullyCastSpell(AnimatorManager animatorHandler, CharacterStatsManager playerState, PlayerWeaponSlotManger weaponSlotManger, CameraHandler cameraHandler , bool isLeftHanded)
        {
            base.SucessfullyCastSpell(animatorHandler, playerState, weaponSlotManger, cameraHandler , isLeftHanded);
            GameObject instantiateSpellFX = null;
            //左手施法:
            if (isLeftHanded)
            {
                //实例化火球:(设置其位置与旋转)
                instantiateSpellFX = Instantiate(spellCastFX, weaponSlotManger.leftHandSlot.transform.position, cameraHandler.cameraPivotTransform.rotation);
                //将来，添加一个实例化位置，而不是施法者本身。
            }
            else
            {
                //实例化火球:(设置其位置与旋转)
                instantiateSpellFX = Instantiate(spellCastFX, weaponSlotManger.rightHandSlot.transform.position, cameraHandler.cameraPivotTransform.rotation);
            }


            rigidbody = instantiateSpellFX.GetComponent<Rigidbody>();

            if (cameraHandler.currentLockOnTarger != null)
            {
                instantiateSpellFX.transform.LookAt(cameraHandler.currentLockOnTarger.transform);
            }
            else
            {
                instantiateSpellFX.transform.rotation = Quaternion.Euler(cameraHandler.cameraPivotTransform.eulerAngles.x, playerState.transform.eulerAngles.y, 0);
            }

            //设置友军标记:
            SpellDamageCollider spellDamageCollider = instantiateSpellFX.GetComponent<SpellDamageCollider>();
            spellDamageCollider.teamIDNumber = playerState.teamIDNumber;
            //设置火球飞行速度:(与是否受重力影响)
            rigidbody.AddForce(instantiateSpellFX.transform.forward * projectileForwardVelocity);
            rigidbody.AddForce(instantiateSpellFX.transform.up * projectileUpwartVelocity);
            rigidbody.useGravity = isEffectedByGravity;
            rigidbody.mass = projectileMass;
            //设置火球伤害:
            spellDamageCollider.chaeacterManager = playerState.GetComponent<PlayerManager>();
            WeaponItem spellWeapon = playerState.GetComponent<PlayerInventoryManager>().rightWeapon;
            if (isLeftHanded)
            {
                spellWeapon = playerState.GetComponent<PlayerInventoryManager>().leftWeapon;
            }
            //设置弹丸伤害:
            spellDamageCollider.physicalDamage = DamageCalculation(physicalDamage, spellWeapon, playerState);
            spellDamageCollider.magicDamage = DamageCalculation(magicDamage, spellWeapon, playerState);
            spellDamageCollider.fireDamage = DamageCalculation(fireDamage, spellWeapon, playerState);
            spellDamageCollider.lightningDamage = DamageCalculation(lightningDamage, spellWeapon, playerState);

            instantiateSpellFX.transform.parent = null;
        }
    }
}
