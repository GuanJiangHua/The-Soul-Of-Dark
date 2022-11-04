using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class BlockingCollider : MonoBehaviour
    {
        public BoxCollider blockingCollider;

        [Header("ɱ�˿���:")]
        public float blockingPhysicalDamageAbsorption;     //���������;
        public float blockingFireDamageAbsorption;            //���������;
        public float blockingMagicDamagerAbsorption;      //ħ��������;
        public float blockingLightningDamageAbsorption;  //�׵������;
        public float blockingDarkDamageAbsorption;          //�����Լ�����;
        public int blockingForce;                                           //����;
        private void Awake()
        {
            blockingCollider = GetComponent<BoxCollider>();
        }

        //���õ�ǰ�����������:(���ݵ�ǰװ���Ķ���)
        public void SetColliderDamageAbsorption(WeaponItem weapon)
        {
            if (weapon != null)
            {
                blockingPhysicalDamageAbsorption = weapon.physicalDamageAbsorption;
                blockingFireDamageAbsorption = weapon.fierDamageAbsorption;
                blockingMagicDamagerAbsorption = weapon.magicDamageAbsorption;
                blockingLightningDamageAbsorption = weapon.darkDamageAbsorption;
                blockingForce = weapon.blockingForce;
            }
            else
            {
                blockingPhysicalDamageAbsorption = 0;
                blockingFireDamageAbsorption = 0;
                blockingMagicDamagerAbsorption = 0;
                blockingLightningDamageAbsorption = 0;
                blockingForce = 0;
            }
        }

        //���ö�����ײ��:
        public void EnableBlockingCollider()
        {
            blockingCollider.enabled = true;
        }
        //���ö�����ײ:
        public void DisableBlockingCollider()
        {
            blockingCollider.enabled = false;
        }
    }
}
