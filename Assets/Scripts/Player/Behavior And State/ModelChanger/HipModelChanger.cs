using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class HipModelChanger : MonoBehaviour
    {
        //�����Ȳ�����ģ��:
        string hipModelName_Male = "Chr_Hips_Male_00";
        string leftLegName_Male = "Chr_LegLeft_Male_00";
        string rightLegName_Male = "Chr_LegRight_Male_00";
        //Ů���Ȳ�����ģ��:
        string hipModelName = "Chr_Hips_Female_00";
        string leftLegName = "Chr_LegLeft_Female_00";
        string rightLegName = "Chr_LegRight_Female_00";

        [Header("Ů������ģ��:")]
        public Transform hip;
        public Transform rightLeg;
        public Transform leftLeg;
        [Header("��������ģ��:")]
        public Transform hip_Male;
        public Transform rightLeg_Male;
        public Transform leftLeg_Male;

        [Header("��ϥװ��ģ����������Ʒģ��:")]
        public Transform rightKneePad;  //�һ�ϥ
        public Transform leftKneePad;  //��ϥ
        public Transform accessories;   //������Ʒ
        [Header("ģ�ͼ���:")]
        public List<GameObject> hipModels = new List<GameObject>();
        public List<GameObject> kneePadModels = new List<GameObject>();
        public List<GameObject> accessorieModels = new List<GameObject>();
        private void Awake()
        {
            GetAllHipModels();
        }
        //��ȡ�Ȳ�ģ��:
        private void GetAllHipModels()
        {
            #region Ů������ģ�ͼ���:
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

            #region ��������ģ�ͼ���:
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
        //�������������Ȳ�ģ��:
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
        //ж�������Ȳ�ģ��:
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
        //���������Ȳ�ģ��:
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
        //����id����������Ʒģ��:(����Ϊ��)
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
