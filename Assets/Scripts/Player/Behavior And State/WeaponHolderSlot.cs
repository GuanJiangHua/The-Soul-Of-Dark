using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class WeaponHolderSlot : MonoBehaviour
    {
        public Transform parentOverride;                           //覆盖父物体
        public WeaponItem currentWeapon;                      //当前武器
        public bool isLeftHandSlot;                                     //是左手武器
        public bool isRightHandSlot;                                  //是右手武器
        public bool backSlot;                                              //背后武器槽
        public bool backShield;                                          //背后盾牌槽
        public bool backBow;                                             //背后弓箭槽位
        public bool waistSlot;                                             //腰部武器槽
        public GameObject currentWeaponModel;           //当前武器模型;

        //方法:卸载武器模型:
        public void UnloadWeapon()
        {
            if (currentWeaponModel != null)
            {
                currentWeaponModel.SetActive(false);                                                        //如果当前武器不为空，就禁用该武器;
            }
        }

        //方法:卸载武器并销毁:
        public void UnloadWeaponAndDestroy()
        {
            if (currentWeaponModel != null)
            {
                Destroy(currentWeaponModel);
            }
        }

        //方法:加载武器模型:
        public void LoadWeaponModel(WeaponItem weaponItem)
        {
            UnloadWeaponAndDestroy();                                                                           //销毁上一把武器;

            if (weaponItem == null)
            {
                //传入参数为null,禁用武器;
                UnloadWeapon();
                return;
            }

            GameObject model = Instantiate(weaponItem.modlPrefab) as GameObject;    //实例化武器模型:
            if (model != null)
            {
                if(parentOverride != null)
                {
                    model.transform.parent = parentOverride;                                                //如果指定了父级对象，则选择此对象为武器模型对象;
                }
                else
                {
                    model.transform.parent = transform;                                                         //否则选择自己作为武器模型的父物体;
                }
                model.transform.localPosition = Vector3.zero;                                               //武器模型本地位置归零（等于父物体位置）
                model.transform.localRotation = Quaternion.identity;                                   //武器模型本地旋转归零（等于父物体旋转)
                model.transform.localScale = Vector3.one;                                                    //武器模型本地缩放归零（等于父物体缩放)
            }

            currentWeaponModel = model;                                                                         //保存新实例化出的武器为当前武器;
        }
    }
}
