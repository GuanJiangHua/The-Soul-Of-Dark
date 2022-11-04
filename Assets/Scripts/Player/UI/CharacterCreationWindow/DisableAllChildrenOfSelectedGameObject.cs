using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class DisableAllChildrenOfSelectedGameObject : MonoBehaviour
    {
        public PlayerManager player;
        public GameObject parentObject;

        public int selectedChildID = -1;
        [Header("正在选择眉毛:")]
        public bool isEyebrows = false;
        [Header("正在选择面部毛发:")]
        public bool isFacialHair = false;
        [Header("正在选择发型:")]
        public bool isHair = false;
        [Header("正在选择头部:")]
        public bool isHead;

        [Header("身体模型数据:")]
        public DodyModelData bodyModelData;

        private void Start()
        {
            player = FindObjectOfType<PlayerManager>();
            bodyModelData = player.playerEquipmentManager.bodyModelData;
        }

        //禁用所有父类的子对象:
        public void DisableAllGameObjectsOfParent()
        {
            for (int i = 0; i < parentObject.transform.childCount; i++)
            {
                GameObject child = parentObject.transform.GetChild(i).gameObject;

                if (child != null)
                {
                    selectedChildID = -1;
                    child.SetActive(false);
                }


            }

            if(isHair == true)
            {
                bodyModelData.hairstyle = selectedChildID;
            }
            else if(isFacialHair)
            {
                bodyModelData.facialHairId = selectedChildID;
            }
            else if (isEyebrows)
            {
                bodyModelData.eyebrow = selectedChildID;
            }
            else if (isHead)
            {
                bodyModelData.headId = 0;
            }
        }

        public void EnableObjectByID(int childID)
        {
            DisableAllGameObjectsOfParent();

            if (childID >= 0 && childID <= parentObject.transform.childCount)
            {
                selectedChildID = childID;
            }
            else
            {
                selectedChildID = -1;
            }

            //可能是秃头:
            if (selectedChildID >= 0)
            {
                GameObject child = parentObject.transform.GetChild(selectedChildID).gameObject;

                if (child != null)
                {
                    child.SetActive(true);
                }
            }

            if (isHair == true)
            {
                bodyModelData.hairstyle = selectedChildID;
            }
            else if (isFacialHair)
            {
                bodyModelData.facialHairId = selectedChildID;
            }
            else if (isEyebrows)
            {
                bodyModelData.eyebrow = selectedChildID;
            }
            else if (isHead)
            {
                if(selectedChildID < 0)
                {
                    GameObject child = parentObject.transform.GetChild(0).gameObject;

                    if (child != null)
                    {
                        child.SetActive(true);
                    }

                    selectedChildID = 0;
                }

                bodyModelData.headId = selectedChildID;
            }
        }
    }
}