using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class WeaponHolderSlot : MonoBehaviour
    {
        public Transform parentOverride;                           //���Ǹ�����
        public WeaponItem currentWeapon;                      //��ǰ����
        public bool isLeftHandSlot;                                     //����������
        public bool isRightHandSlot;                                  //����������
        public bool backSlot;                                              //����������
        public bool backShield;                                          //������Ʋ�
        public bool backBow;                                             //���󹭼���λ
        public bool waistSlot;                                             //����������
        public GameObject currentWeaponModel;           //��ǰ����ģ��;

        //����:ж������ģ��:
        public void UnloadWeapon()
        {
            if (currentWeaponModel != null)
            {
                currentWeaponModel.SetActive(false);                                                        //�����ǰ������Ϊ�գ��ͽ��ø�����;
            }
        }

        //����:ж������������:
        public void UnloadWeaponAndDestroy()
        {
            if (currentWeaponModel != null)
            {
                Destroy(currentWeaponModel);
            }
        }

        //����:��������ģ��:
        public void LoadWeaponModel(WeaponItem weaponItem)
        {
            UnloadWeaponAndDestroy();                                                                           //������һ������;

            if (weaponItem == null)
            {
                //�������Ϊnull,��������;
                UnloadWeapon();
                return;
            }

            GameObject model = Instantiate(weaponItem.modlPrefab) as GameObject;    //ʵ��������ģ��:
            if (model != null)
            {
                if(parentOverride != null)
                {
                    model.transform.parent = parentOverride;                                                //���ָ���˸���������ѡ��˶���Ϊ����ģ�Ͷ���;
                }
                else
                {
                    model.transform.parent = transform;                                                         //����ѡ���Լ���Ϊ����ģ�͵ĸ�����;
                }
                model.transform.localPosition = Vector3.zero;                                               //����ģ�ͱ���λ�ù��㣨���ڸ�����λ�ã�
                model.transform.localRotation = Quaternion.identity;                                   //����ģ�ͱ�����ת���㣨���ڸ�������ת)
                model.transform.localScale = Vector3.one;                                                    //����ģ�ͱ������Ź��㣨���ڸ���������)
            }

            currentWeaponModel = model;                                                                         //������ʵ������������Ϊ��ǰ����;
        }
    }
}
