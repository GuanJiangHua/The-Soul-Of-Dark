using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class ArmsModelChanger : MonoBehaviour
    {
        //�����ֱ�
        string leftForearmName_Male = "Chr_ArmLowerLeft_Male_00";
        string rightForearmName_Male = "Chr_ArmLowerRight_Male_00";
        //��������
        string leftPalmName_Male = "Chr_HandLeft_Male_00";
        string rightPalmName_Male = "Chr_HandRight_Male_00";

        //Ů���ֱ�
        string leftForearmName = "Chr_ArmLowerLeft_Female_00";
        string rightForearmName = "Chr_ArmLowerRight_Female_00";
        //Ů������
        string leftPalmName = "Chr_HandLeft_Female_00";
        string rightPalmName = "Chr_HandRight_Female_00";

        [Header("Ů��ǰ������ģ�͸���������:")]
        public Transform leftForearm;   //��ǰ��
        public Transform rightForearm; //��ǰ��
        public Transform leftPalm;  //������
        public Transform rightPalm; //������

        [Header("����ǰ������ģ�͸���������:")]
        public Transform leftForearm_Male;   //��ǰ��
        public Transform rightForearm_Male; //��ǰ��
        public Transform leftPalm_Male;  //������
        public Transform rightPalm_Male; //������

        [Header("�ⲿ����:")]
        public Transform leftElbow;     //������
        public Transform rightElbow;    //������
        [Header("ģ�ͼ���:")]
        public List<GameObject> forearmModels = new List<GameObject>();
        public List<GameObject> palmModels = new List<GameObject>();
        public List<GameObject> elbowModels = new List<GameObject>();

        private void Awake()
        {
            GetAllForearmModels();
            GetAllPalmModels();
            GetAllElbowModels();
        }
        //��ȡ����ǰ��ģ��:
        private void GetAllForearmModels()
        {
            for(int i = 0; i < leftForearm.childCount; i++)
            {
                forearmModels.Add(leftForearm.GetChild(i).gameObject);
            }
            for (int i = 0; i < rightForearm.childCount; i++)
            {
                forearmModels.Add(rightForearm.GetChild(i).gameObject);
            }

            for (int i = 0; i < leftForearm_Male.childCount; i++)
            {
                forearmModels.Add(leftForearm_Male.GetChild(i).gameObject);
            }
            for (int i = 0; i < rightForearm_Male.childCount; i++)
            {
                forearmModels.Add(rightForearm_Male.GetChild(i).gameObject);
            }
        }
        //��ȡ��������ģ��:
        private void GetAllPalmModels()
        {
            for(int i = 0; i < leftPalm.childCount; i++)
            {
                palmModels.Add(leftPalm.GetChild(i).gameObject);
            }
            for (int i = 0; i < rightPalm.childCount; i++)
            {
                palmModels.Add(rightPalm.GetChild(i).gameObject);
            }

            for (int i = 0; i < leftPalm_Male.childCount; i++)
            {
                palmModels.Add(leftPalm_Male.GetChild(i).gameObject);
            }
            for (int i = 0; i < rightPalm_Male.childCount; i++)
            {
                palmModels.Add(rightPalm_Male.GetChild(i).gameObject);
            }
        }
        //��ȡ����ģ��:
        private void GetAllElbowModels()
        {
            for(int i = 0; i < leftElbow.childCount; i++)
            {
                elbowModels.Add(leftElbow.GetChild(i).gameObject);
            }
            for (int i = 0; i < rightElbow.childCount; i++)
            {
                elbowModels.Add(rightElbow.GetChild(i).gameObject);
            }
        }

        //���������ֲ�ģ��:
        public void UnEquipAllHandModel()
        {
            foreach(GameObject model in forearmModels)
            {
                model.SetActive(false);
            }
            foreach(GameObject model in palmModels)
            {
                model.SetActive(false);
            }
            foreach(GameObject model in elbowModels)
            {
                model.SetActive(false);
            }
        }
        //�������������ֲ�ģ��:
        public void EquipHandModelByName(string leftForearmName,string rightForearmName,string leftPalmName,string rightPalmName,string leftElbowName = "", string rightElbowName = "")
        {
            foreach(GameObject model in forearmModels)
            {
                if(model.name.Equals(leftForearmName) || model.name.Equals(rightForearmName))
                {
                    model.SetActive(true);
                }
            }

            foreach(GameObject model in palmModels)
            {
                if(model.name.Equals(leftPalmName) || model.name.Equals(rightPalmName))
                {
                    model.SetActive(true);
                }
            }
        
            foreach(GameObject model in elbowModels)
            {
                if(model.name.Equals(leftElbowName) || model.name.Equals(rightElbowName))
                {
                    model.SetActive(true);
                }
            }
        }
    
        //��������ģ��:
        public void LoadNudeModel(bool isMale)
        {
            if (isMale)
            {
                EquipHandModelByName(leftForearmName_Male, rightForearmName_Male, leftPalmName_Male, rightPalmName_Male);
            }
            else
            {
                EquipHandModelByName(leftForearmName, rightForearmName, leftPalmName, rightPalmName);
            }
        }

        //����Ƥ����ɫ:
        public void SetHandModelColor(string attributeSkinColor, Color color)
        {
            Material material = new Material(forearmModels[0].GetComponent<SkinnedMeshRenderer>().material);
            material.SetColor(attributeSkinColor, color);

            foreach(GameObject model in forearmModels)
            {
                SkinnedMeshRenderer skinnedMesh = model.GetComponent<SkinnedMeshRenderer>();
                skinnedMesh.material = material;
            }

            foreach (GameObject model in palmModels)
            {
                SkinnedMeshRenderer skinnedMesh = model.GetComponent<SkinnedMeshRenderer>();
                skinnedMesh.material = material;
            }
        }
    }
}