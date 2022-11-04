using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [System.Serializable]
    public enum WeaponType
    {
        PyromancyCaster,        //����ʩ������
        FaithCaster,                  //ħ��ʩ������
        SpellCaster,                  //�漣ʩ������
        StraightSword,             //ֱ��
        Spear,                           //ì
        SmallShield,                 //С��
        Shield,                          //�ж�
        Bow,                             //�ǹ�
        Unarmed,                     //����
        BluntInstrument,          //����
        Dagger,                         //ذ��
        BigSword                      //��
    }
    [System.Serializable]
    public enum ItemType
    {
        Weapon,
        Helmet,
        Torso,
        Leg,
        Hand,
        Spell,
        Consumable,
        Ring,
        Ammo
    }
    public enum AmmoType
    {
        Arrow,      //��
        Bolt          //���
    }
    public class Enums : MonoBehaviour
    {

    }
}
