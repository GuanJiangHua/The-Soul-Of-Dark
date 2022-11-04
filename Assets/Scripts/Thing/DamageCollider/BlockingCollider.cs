using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class BlockingCollider : MonoBehaviour
    {
        public BoxCollider blockingCollider;

        [Header("杀伤抗性:")]
        public float blockingPhysicalDamageAbsorption;     //物理减伤率;
        public float blockingFireDamageAbsorption;            //火焰减伤率;
        public float blockingMagicDamagerAbsorption;      //魔力减伤率;
        public float blockingLightningDamageAbsorption;  //雷电减伤率;
        public float blockingDarkDamageAbsorption;          //暗属性减伤率;
        public int blockingForce;                                           //格挡力;
        private void Awake()
        {
            blockingCollider = GetComponent<BoxCollider>();
        }

        //设置当前的物理减伤率:(根据当前装备的盾牌)
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

        //启用盾牌碰撞器:
        public void EnableBlockingCollider()
        {
            blockingCollider.enabled = true;
        }
        //禁用盾牌碰撞:
        public void DisableBlockingCollider()
        {
            blockingCollider.enabled = false;
        }
    }
}
