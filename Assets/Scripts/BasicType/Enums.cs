using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [System.Serializable]
    public enum WeaponType
    {
        PyromancyCaster,        //咒术施法武器
        FaithCaster,                  //魔法施法武器
        SpellCaster,                  //奇迹施法武器
        StraightSword,             //直剑
        Spear,                           //矛
        SmallShield,                 //小盾
        Shield,                          //中盾
        Bow,                             //是弓
        Unarmed,                     //空手
        BluntInstrument,          //钝器
        Dagger,                         //匕首
        BigSword                      //大剑
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
        Arrow,      //箭
        Bolt          //弩箭
    }
    public class Enums : MonoBehaviour
    {

    }
}
