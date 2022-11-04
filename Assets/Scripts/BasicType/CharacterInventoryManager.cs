using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class CharacterInventoryManager : MonoBehaviour
    {
        [Header("��ǰ����ʹ�õĵ���:")]
        public Item currentItemBeingUsed;

        [Header("����װ����λ:")]
        public SpellItem currentSpell;                              //������λ
        public WeaponItem rightWeapon;                      //����������λ
        public WeaponItem leftWeapon;                        //����������λ
        public ConsumableItem currentConsumable;    //��ǰ����Ʒ��λ
        [Header("�����:")]
        public RangedAmmoItem currentBow;              //��ǰ������
        public RangedAmmoItem spareBow;                 //���ù�����
        public RangedAmmoItem otherAmmo;             //���������
        public RangedAmmoItem spareOtherAmmo;   //�������������
        [Header("��ǰͷ��:")]
        public HelmetEquipment currentHelmetEquipment;  //��ǰͷ��;
        [Header("��ǰ�ؼ�:")]
        public TorsoEquipment currentTorsoEquipment;    //��ǰ�ؼ�;
        [Header("��ǰ�ȼ�:")]
        public LegEquipment currentLegEquipment;     //��ǰ�ȼ�;
        [Header("��ǰ�ۼ�:")]
        public HandEquipment currentHandEquipment;  //��ǰ�ۼ�;

        public WeaponItem unarmedWeapon;
        [Header("������װ��:")]
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
