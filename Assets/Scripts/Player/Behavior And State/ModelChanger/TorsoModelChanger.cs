using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class TorsoModelChanger : MonoBehaviour
    {
        //男性裸体的躯干模型:
        string torsoModelName_Male = "Chr_Torso_Male_00";
        string leftRearArmModelName_Male = "Chr_ArmUpperLeft_Male_00";
        string rightRearArmModeName__Male = "Chr_ArmUpperRight_Male_00";
        //女性裸体的躯干模型:
        string torsoModelName = "Chr_Torso_Female_00";
        string leftRearArmModelName = "Chr_ArmUpperLeft_Female_00";
        string rightRearArmModeName = "Chr_ArmUpperRight_Female_00";

        [Header("肩甲:")]
        public Transform leftShoulder;
        public Transform rightShoulder;
        [Header("背后物品引用:")]
        public Transform behind;
        [Header("女性模型父物体引用:")]
        public Transform torso;
        public Transform leftRearArm;
        public Transform rightRearArm;

        [Header("男性模型父物体引用:")]
        public Transform torso_Male;
        public Transform leftRearArm_Male;
        public Transform rightRearArm_Male;

        [Header("模型集合:")]
        public List<GameObject> torsoModels = new List<GameObject>();   //躯干模型
        public List<GameObject> leftRearArmModels = new List<GameObject>(); //左后臂模型
        public List<GameObject> rightRearArmModels = new List<GameObject>(); //右后臂模型
        [Header("肩甲模型:")]
        public List<GameObject> leftShoulderModels = new List<GameObject>();    //左肩甲模型
        public List<GameObject> rightShoulderModels = new List<GameObject>();   //右肩甲模型
        [Header("背后饰品模型:")]
        public List<GameObject> behindModels = new List<GameObject>();
        public void Awake()
        {
            GetAllTorsoModesls();
        }
        private void GetAllTorsoModesls()
        {
            //躯干:
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

            #region 左右后臂:
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

            #region 左右肩甲:
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

            //背后饰品:
            childrenGameObjects = behind.childCount;
            for(int i = 0; i < childrenGameObjects; i++)
            {
                behindModels.Add(behind.GetChild(i).gameObject);
            }
        }

        //卸载所有胸甲,后臂,肩甲模型:
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

        //根据名称更换躯干模型:
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
    
        //加载身体模型,左右大臂模型:
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
