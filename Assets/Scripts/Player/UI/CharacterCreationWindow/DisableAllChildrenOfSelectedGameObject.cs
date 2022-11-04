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
        [Header("����ѡ��üë:")]
        public bool isEyebrows = false;
        [Header("����ѡ���沿ë��:")]
        public bool isFacialHair = false;
        [Header("����ѡ����:")]
        public bool isHair = false;
        [Header("����ѡ��ͷ��:")]
        public bool isHead;

        [Header("����ģ������:")]
        public DodyModelData bodyModelData;

        private void Start()
        {
            player = FindObjectOfType<PlayerManager>();
            bodyModelData = player.playerEquipmentManager.bodyModelData;
        }

        //�������и�����Ӷ���:
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

            //������ͺͷ:
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