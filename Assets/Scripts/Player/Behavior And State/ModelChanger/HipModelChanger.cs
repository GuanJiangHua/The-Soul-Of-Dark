using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class HipModelChanger : MonoBehaviour
    {
        //男性腿部裸体模型:
        string hipModelName_Male = "Chr_Hips_Male_00";
        string leftLegName_Male = "Chr_LegLeft_Male_00";
        string rightLegName_Male = "Chr_LegRight_Male_00";
        //女性腿部裸体模型:
        string hipModelName = "Chr_Hips_Female_00";
        string leftLegName = "Chr_LegLeft_Female_00";
        string rightLegName = "Chr_LegRight_Female_00";

        [Header("女性下身模型:")]
        public Transform hip;
        public Transform rightLeg;
        public Transform leftLeg;
        [Header("男性下身模型:")]
        public Transform hip_Male;
        public Transform rightLeg_Male;
        public Transform leftLeg_Male;

        [Header("护膝装甲模型与腰部饰品模型:")]
        public Transform rightKneePad;  //右护膝
        public Transform leftKneePad;  //左护膝
        public Transform accessories;   //腰部饰品
        [Header("模型集合:")]
        public List<GameObject> hipModels = new List<GameObject>();
        public List<GameObject> kneePadModels = new List<GameObject>();
        public List<GameObject> accessorieModels = new List<GameObject>();
        private void Awake()
        {
            GetAllHipModels();
        }
        //获取腿部模型:
        private void GetAllHipModels()
        {
            #region 女性下身模型加载:
            for (int i = 0; i < hip.childCount; i++)
            {
                hipModels.Add(hip.GetChild(i).gameObject);
            }

            for(int i = 0; i < rightLeg.childCount; i++)
            {
                hipModels.Add(rightLeg.GetChild(i).gameObject);
            }

            for (int i = 0; i < leftLeg.childCount; i++)
            {
                hipModels.Add(leftLeg.GetChild(i).gameObject);
            }
            #endregion

            #region 男性下身模型加载:
            for (int i = 0; i < hip_Male.childCount; i++)
            {
                hipModels.Add(hip_Male.GetChild(i).gameObject);
            }

            for (int i = 0; i < rightLeg_Male.childCount; i++)
            {
                hipModels.Add(rightLeg_Male.GetChild(i).gameObject);
            }

            for (int i = 0; i < leftLeg_Male.childCount; i++)
            {
                hipModels.Add(leftLeg_Male.GetChild(i).gameObject);
            }
            #endregion

            for (int i = 0; i < rightKneePad.childCount; i++)
            {
                kneePadModels.Add(rightKneePad.GetChild(i).gameObject);
            }

            for (int i = 0; i < leftKneePad.childCount; i++)
            {
                kneePadModels.Add(leftKneePad.GetChild(i).gameObject);
            }
        
            for(int i = 0; i < accessories.childCount; i++)
            {
                accessorieModels.Add(accessories.GetChild(i).gameObject);
            }
        }
        //根据名称启用腿部模型:
        public void EquipHipModelByName(string hipNmae,string rightLeg,string leftLeg,string rightKneePadName = "",string leftKneePadName = "")
        {
            foreach(GameObject model in hipModels)
            {
                if (model.name.Equals(hipNmae) || model.name.Equals(rightLeg) || model.name.Equals(leftLeg))
                {
                    model.SetActive(true);
                }
            }

            foreach(GameObject model in kneePadModels)
            {
                if(model.name.Equals(rightKneePadName) || model.name.Equals(leftKneePadName))
                {
                    model.SetActive(true);
                }
            }
        }
        //卸载所有腿部模型:
        public void UnEquipAllHipModels()
        {
            foreach (GameObject model in hipModels)
            {
                model.SetActive(false);
            }

            foreach(GameObject model in kneePadModels)
            {
                model.SetActive(false);
            }

            foreach(GameObject model in accessorieModels)
            {
                model.SetActive(false);
            }
        }
        //加载裸体腿部模型:
        public void LoadNudeModel(bool isMale)
        {
            if (isMale)
            {
                EquipHipModelByName(hipModelName_Male, rightLegName_Male, leftLegName_Male);
            }
            else
            {
                EquipHipModelByName(hipModelName, rightLegName, leftLegName);
            }
        }
        //根据id加载腰部饰品模型:(可以为空)
        public void EquipAccessoriesModelsById(int[] accessoriesId)
        {
            if (accessoriesId != null)
            {
                foreach(int id in accessoriesId)
                {
                    accessorieModels[id].SetActive(true);
                }
            }
        }

        public void SetLegModelColor(string attributeSkinColor, Color color)
        {
            Material material = new Material(hipModels[0].GetComponent<SkinnedMeshRenderer>().material);
            material.SetColor(attributeSkinColor, color);

            foreach(GameObject model in hipModels)
            {
                SkinnedMeshRenderer skinnedMesh = model.GetComponent<SkinnedMeshRenderer>();
                skinnedMesh.material = material;
            }
        }
    }
}
