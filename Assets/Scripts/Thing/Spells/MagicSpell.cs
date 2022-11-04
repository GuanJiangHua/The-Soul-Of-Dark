using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "�½�Ͷħ��������Ʒ��Ϣ", menuName = "��Ϸ��Ʒ/�½�ħ��������Ʒ")]
    public class MagicSpell : SpellItem
    {
        [Header("��ִ��:")]
        public bool isGuidance = false;
        [Header("�����˺�:")]
        public int physicalDamage = 25;
        public int fireDamage;                   //�����˺�
        public int magicDamage;              //ħ���˺�
        public int lightningDamage;         //�׵��˺�
        public int darkDamage;                //�ڰ��˺�
        [Header("�����˺�:")]
        public float poiseBreak = 5;                     //�����˺�
        [Header("����ĸ������ԣ�")]
        public float projectileForwardVelocity;    //����ǰ���ٶ�;
        public float projectileUpwartVelocity;  //���ϵ��ٶ�;
        public float projectileMass;
        public bool isEffectedByGravity;           //������Ӱ��;

        Rigidbody rigidbody;
        //ʩ����:
        public override void AttemptToCastSpell(AnimatorManager animatorHandler, CharacterStatsManager playerState, PlayerWeaponSlotManger weaponSlotManger, bool isLeftHanded)
        {
            base.AttemptToCastSpell(animatorHandler, playerState, weaponSlotManger, isLeftHanded);

            if (isLeftHanded)
            {
                //ʵ����ʩ�����̵���ЧԤ����;(������ʩ��ʱ:)
                GameObject instantiatedWarmUpSpellFX = Instantiate(spellWarmUpFX, weaponSlotManger.leftHandSlot.transform);
                instantiatedWarmUpSpellFX.transform.localScale = Vector3.one/playerState.transform.localScale.x;
                Destroy(instantiatedWarmUpSpellFX, 5);
                //����ʩ������;(���ö�������)
                animatorHandler.PlayTargetAnimation(spellAnimation, true, false, true);
            }
            else
            {
                //ʵ����ʩ�����̵���ЧԤ����;
                GameObject instantiatedWarmUpSpellFX = Instantiate(spellWarmUpFX, weaponSlotManger.rightHandSlot.transform);
                //����ʩ������;
                animatorHandler.PlayTargetAnimation(spellAnimation, true);
            }
        }

        //ʩ���ɹ�:
        public override void SucessfullyCastSpell(AnimatorManager animatorHandler, CharacterStatsManager playerState, PlayerWeaponSlotManger weaponSlotManger, CameraHandler cameraHandler, bool isLeftHanded)
        {
            base.SucessfullyCastSpell(animatorHandler, playerState, weaponSlotManger, cameraHandler, isLeftHanded);
            GameObject instantiateSpellFX = null;
            //����ʩ��:
            if (isLeftHanded)
            {
                //ʵ��������:(������λ������ת)
                instantiateSpellFX = Instantiate(spellCastFX, weaponSlotManger.leftHandSlot.transform.position, cameraHandler.cameraPivotTransform.rotation);
                //���������һ��ʵ����λ�ã�������ʩ���߱���
            }
            else
            {
                //ʵ��������:(������λ������ת)
                instantiateSpellFX = Instantiate(spellCastFX, weaponSlotManger.rightHandSlot.transform.position, cameraHandler.cameraPivotTransform.rotation);
            }

            rigidbody = instantiateSpellFX.GetComponent<Rigidbody>();
            SpellDamageCollider spellDamageCollider = instantiateSpellFX.GetComponent<SpellDamageCollider>();

            if (cameraHandler.currentLockOnTarger != null)
            {
                instantiateSpellFX.transform.LookAt(cameraHandler.currentLockOnTarger.transform);
                //����Ŀ��:
                spellDamageCollider.SetSpellTarget(cameraHandler.currentLockOnTarger.GetComponent<CharacterStatsManager>());
            }
            else
            {
                instantiateSpellFX.transform.rotation = Quaternion.Euler(cameraHandler.cameraPivotTransform.eulerAngles.x, playerState.transform.eulerAngles.y, 0);
            }
            //����
            spellDamageCollider.chaeacterManager = playerState.GetComponent<CharacterManager>();
            //�����Ѿ����:
            spellDamageCollider.teamIDNumber = playerState.teamIDNumber;

            WeaponItem spellWeapon = playerState.GetComponent<PlayerInventoryManager>().rightWeapon;
            if (isLeftHanded)
            {
                spellWeapon = playerState.GetComponent<PlayerInventoryManager>().leftWeapon;
            }
            //���õ����˺�:
            spellDamageCollider.physicalDamage = DamageCalculation(physicalDamage, spellWeapon, playerState);
            spellDamageCollider.magicDamage = DamageCalculation(magicDamage, spellWeapon, playerState);
            spellDamageCollider.fireDamage = DamageCalculation(fireDamage, spellWeapon, playerState);
            spellDamageCollider.lightningDamage = DamageCalculation(lightningDamage, spellWeapon, playerState);
            //���������˺�:
            spellDamageCollider.poiseBreak = poiseBreak;
            //�����Ƿ���Ƶ�:
            spellDamageCollider.isGuidance = isGuidance;

            //���õ������Ƿ�������Ӱ��:
            rigidbody.useGravity = isEffectedByGravity;
            rigidbody.mass = projectileMass;

            instantiateSpellFX.transform.parent = null;
        }
    }
}