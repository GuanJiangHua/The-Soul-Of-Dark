using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class CharacterInventoryManager : MonoBehaviour
    {
        [Header("当前正在使用的道具:")]
        public Item currentItemBeingUsed;

        [Header("快速装备槽位:")]
        public SpellItem currentSpell;                              //法术槽位
        public WeaponItem rightWeapon;                      //右手武器槽位
        public WeaponItem leftWeapon;                        //左手武器槽位
        public ConsumableItem currentConsumable;    //当前消耗品槽位
        [Header("弹丸槽:")]
        public RangedAmmoItem currentBow;              //当前弓箭槽
        public RangedAmmoItem spareBow;                 //备用弓箭槽
        public RangedAmmoItem otherAmmo;             //其他弹丸槽
        public RangedAmmoItem spareOtherAmmo;   //备用其他弹丸槽
        [Header("当前头盔:")]
        public HelmetEquipment currentHelmetEquipment;  //当前头盔;
        [Header("当前胸甲:")]
        public TorsoEquipment currentTorsoEquipment;    //当前胸甲;
        [Header("当前腿甲:")]
        public LegEquipment currentLegEquipment;     //当前腿甲;
        [Header("当前臂甲:")]
        public HandEquipment currentHandEquipment;  //当前臂甲;

        public WeaponItem unarmedWeapon;
        [Header("左右手装备:")]
        public WeaponItem[] WeaponRightHandSlots = new WeaponItem[3];
        public WeaponItem[] WeaponLeftHandSlots = new WeaponItem[3];

        protected CharacterManager characterManager;
        protected CharacterWeaponSlotManager characterWeaponSlotManger;
        protected virtual void Awake()
        {
            characterManager = GetComponent<CharacterManager>();
            characterWeaponSlotManger = GetComponent<CharacterWeaponSlotManager>();
        }
    }
}
