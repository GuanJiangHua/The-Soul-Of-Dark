using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class TorsoModelChanger : MonoBehaviour
    {
        //�������������ģ��:
        string torsoModelName_Male = "Chr_Torso_Male_00";
        string leftRearArmModelName_Male = "Chr_ArmUpperLeft_Male_00";
        string rightRearArmModeName__Male = "Chr_ArmUpperRight_Male_00";
        //Ů�����������ģ��:
        string torsoModelName = "Chr_Torso_Female_00";
        string leftRearArmModelName = "Chr_ArmUpperLeft_Female_00";
        string rightRearArmModeName = "Chr_ArmUpperRight_Female_00";

        [Header("���:")]
        public Transform leftShoulder;
        public Transform rightShoulder;
        [Header("������Ʒ����:")]
        public Transform behind;
        [Header("Ů��ģ�͸���������:")]
        public Transform torso;
        public Transform leftRearArm;
        public Transform rightRearArm;

        [Header("����ģ�͸���������:")]
        public Transform torso_Male;
        public Transform leftRearArm_Male;
        public Transform rightRearArm_Male;

        [Header("ģ�ͼ���:")]
        public List<GameObject> torsoModels = new List<GameObject>();   //����ģ��
        public List<GameObject> leftRearArmModels = new List<GameObject>(); //����ģ��
        public List<GameObject> rightRearArmModels = new List<GameObject>(); //�Һ��ģ��
        [Header("���ģ��:")]
        public List<GameObject> leftShoulderModels = new List<GameObject>();    //����ģ��
        public List<GameObject> rightShoulderModels = new List<GameObject>();   //�Ҽ��ģ��
        [Header("������Ʒģ��:")]
        public List<GameObject> behindModels = new List<GameObject>();
        public void Awake()
        {
            GetAllTorsoModesls();
        }
        private void GetAllTorsoModesls()
        {
            //����:
            int childrenGameObjects = torso.childCount;
            for (int i = 0; i < childrenGameObjects; i++)
            {
                torsoModels.Add(torso.GetChild(i).gameObject);
            }

            childrenGameObjects = torso_Male.childCount;
            for (int i = 0; i < childrenGameObjects; i++)
            {
                torsoModels.Add(torso_Male.GetChild(i).gameObject);
            }

            #region ���Һ��:
            childrenGameObjects = leftRearArm.childCount;
            for (int i = 0; i < childrenGameObjects; i++)
            {
                leftRearArmModels.Add(leftRearArm.GetChild(i).gameObject);
            }
            childrenGameObjects = rightRearArm.childCount;
            for (int i = 0; i < childrenGameObjects; i++)
            {
                rightRearArmModels.Add(rightRearArm.GetChild(i).gameObject);
            }

            childrenGameObjects = leftRearArm_Male.childCount;
            for (int i = 0; i < childrenGameObjects; i++)
            {
                leftRearArmModels.Add(leftRearArm_Male.GetChild(i).gameObject);
            }
            childrenGameObjects = rightRearArm_Male.childCount;
            for (int i = 0; i < childrenGameObjects; i++)
            {
                rightRearArmModels.Add(rightRearArm_Male.GetChild(i).gameObject);
            }
            #endregion

            #region ���Ҽ��:
            childrenGameObjects = leftShoulder.childCount;
            for (int i = 0; i < childrenGameObjects; i++)
            {
                leftShoulderModels.Add(leftShoulder.GetChild(i).gameObject);
            }

            childrenGameObjects = rightShoulder.childCount;
            for (int i = 0; i < childrenGameObjects; i++)
            {
                rightShoulderModels.Add(rightShoulder.GetChild(i).gameObject);
            }
            #endregion

            //������Ʒ:
            childrenGameObjects = behind.childCount;
            for(int i = 0; i < childrenGameObjects; i++)
            {
                behindModels.Add(behind.GetChild(i).gameObject);
            }
        }

        //ж�������ؼ�,���,���ģ��:
        public void UnequipAllTorsoModels()
        {
            foreach (GameObject torso in torsoModels)
            {
                torso.SetActive(false);
            }
            foreach(GameObject rightRearArmModel in rightRearArmModels)
            {
                rightRearArmModel.SetActive(false);
            }
            foreach(GameObject leftRearArmArmModel in leftRearArmModels)
            {
                leftRearArmArmModel.SetActive(false);
            }
            foreach (GameObject leftShoulderModel in leftShoulderModels)
            {
                leftShoulderModel.SetActive(false);
            }
            foreach (GameObject rightShoulderModel in rightShoulderModels)
            {
                rightShoulderModel.SetActive(false);
            }
            foreach(GameObject behindModel in behindModels)
            {
                behindModel.SetActive(false);
            }
        }

        //�������Ƹ�������ģ��:
        public void EquipTorsoModelByName(string torsoName,string rightRearArmModelName, string leftRearArmModelName,string rightShoulderModelName = "",string leftShoulderModelName = "")
        {
            for (int i = 0; i < torsoModels.Count; i++)
            {
                if (torsoModels[i].name.Equals(torsoName))
                {
                    torsoModels[i].SetActive(true);
                    break;
                }
            }

            for (int i = 0; i < rightRearArmModels.Count; i++)
            {
                if (rightRearArmModels[i].name.Equals(rightRearArmModelName))
                {
                    rightRearArmModels[i].SetActive(true);
                    break;
                }
            }

            for (int i = 0; i < leftRearArmModels.Count; i++)
            {
                if (leftRearArmModels[i].name.Equals(leftRearArmModelName))
                {
                    leftRearArmModels[i].SetActive(true);
                    break;
                }
            }

            if (leftShoulderModelName.Equals("") == false && leftShoulderModelName !=null)
            {
                for (int i = 0; i < leftShoulderModels.Count; i++)
                {
                    if (leftShoulderModels[i].name.Equals(leftShoulderModelName))
                    {
                        leftShoulderModels[i].SetActive(true);
                        break;
                    }
                }
            }

            if(rightShoulderModelName.Equals("") == false && rightShoulderModelName != null)
            {
                for (int i = 0; i < rightShoulderModels.Count; i++)
                {
                    if (rightShoulderModels[i].name.Equals(rightShoulderModelName))
                    {
                        rightShoulderModels[i].SetActive(true);
                        break;
                    }
                }
            }
        }
    
        //��������ģ��,���Ҵ��ģ��:
        public void LoadNudeModel(bool isMale)
        {
            if (isMale)
            {
                EquipTorsoModelByName(torsoModelName_Male, rightRearArmModeName__Male, leftRearArmModelName_Male);
            }
            else
            {
                EquipTorsoModelByName(torsoModelName, rightRearArmModeName, leftRearArmModelName);
            }
        }

        public void LoadBehindModel(int index)
        {
            if(index != -1)
            {
                behindModels[index].SetActive(true);
            }
        }

        public void SetTorsoModelColor(string attributeSkinColor, Color color)
        {
            Material material = new Material(torsoModels[0].GetComponent<SkinnedMeshRenderer>().material);
            material.SetColor(attributeSkinColor, color);

            foreach (GameObject torso in torsoModels)
            {
                SkinnedMeshRenderer skinnedMesh = torso.GetComponent<SkinnedMeshRenderer>();
                skinnedMesh.material = material;
            }

            foreach (GameObject torso in leftRearArmModels)
            {
                SkinnedMeshRenderer skinnedMesh = torso.GetComponent<SkinnedMeshRenderer>();
                skinnedMesh.material = material;
            }

            foreach (GameObject torso in rightRearArmModels)
            {
                SkinnedMeshRenderer skinnedMesh = torso.GetComponent<SkinnedMeshRenderer>();
                skinnedMesh.material = material;
            }
        }
    }
}
