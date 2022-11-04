using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [System.Serializable]
    public class ClassGear
    {
        [Header("ְҵ��:")]
        public string className;
        [Header("����:")]
        public WeaponItem primaryWeapon;
        public WeaponItem offHandWeapon;
        [Header("װ��:")]
        public HelmetEquipment headEquipment;   //ͷ��
        public TorsoEquipment torsoEquipment;     //�ؼ�
        public HandEquipment handEquipment;     //�ۼ�
        public LegEquipment legEquipment;           //�ȼ�
        [Header("������:")]
        public ItemType itemType;
        public Item funeraryItemOne;
        public int funeraryItemTwoAmount;           //����Ʒ����:
        public ConsumableItem funeraryItemTwo;
        public int funeraryItemThreeAmount;         //����Ʒ����:
        public ConsumableItem funeraryItemThree;
    }
}