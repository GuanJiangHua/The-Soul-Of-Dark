using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    //ͷ��ģ��ת����:
    public class HelmetsModelChanger : MonoBehaviour
    {

        public Transform hat;                                         //ñ��
        public Transform hairstyle;                                 //����
        public Transform notFaceCoveringHeadgear;    //�Ǹ���ͷ��
        public Transform faceCoveringFaciale;               //�����沿��ͷ��
        public Transform accessories;                             //��Ʒ
        [Header("Ů��ģ�͸���������:")]
        public Transform helmet;       //ͷ��
        public Transform head;          //ͷ
        public Transform eyebrow;     //üë
        [Header("����ģ�͸���������:")]
        public Transform facialHair_Male;   //�沿ë��;
        public Transform helmet_Male;       //ͷ��
        public Transform head_Male;          //ͷ
        public Transform eyebrow_Male;     //üë

        [Header("ģ�ͼ���:")]
        public List<GameObject> hairstyleModels = new List<GameObject>();      //����ģ��
        public List<GameObject> accessoriesModels = new List<GameObject>();  //��Ʒģ��

        public List<GameObject> helmetModels = new List<GameObject>();      //ͷ��ģ��
        public List<GameObject> headModels = new List<GameObject>();         //ͷ��ģ��
        public List<GameObject> eyebrowModels = new List<GameObject>();   //üëģ��

        public List<GameObject> maleHelmetModels = new List<GameObject>();     //ͷ��ģ��
        public List<GameObject> facialHairModels = new List<GameObject>();          //�沿ë��
        public List<GameObject> maleHeadModels = new List<GameObject>();        //����ͷ��ģ��
        public List<GameObject> maleEyebrowModels = new List<GameObject>();   //����üëģ��
        public void Awake()
        {
            GetAllHelmetModesls();
        }
        private void GetAllHelmetModesls()
        {
            int childrenGameObjects = helmet.childCount;
            for(int i = 0; i < childrenGameObjects; i++)
            {
                helmetModels.Add(helmet.GetChild(i).gameObject);
            }
            childrenGameObjects = notFaceCoveringHeadgear.childCount;
            for (int i = 0; i < childrenGameObjects; i++)
            {
                helmetModels.Add(notFaceCoveringHeadgear.GetChild(i).gameObject);
            }
            childrenGameObjects = hat.childCount;
            for (int i = 0; i < childrenGameObjects; i++)
            {
                helmetModels.Add(hat.GetChild(i).gameObject);
            }

            childrenGameObjects = head.childCount;
            for (int i = 0; i < childrenGameObjects; i++)
            {
                headModels.Add(head.GetChild(i).gameObject);
            }

            childrenGameObjects = hairstyle.childCount;
            for (int i = 0; i < childrenGameObjects; i++)
            {
                hairstyleModels.Add(hairstyle.GetChild(i).gameObject);
            }

            childrenGameObjects = eyebrow.childCount;
            for (int i = 0; i < childrenGameObjects; i++)
            {
                eyebrowModels.Add(eyebrow.GetChild(i).gameObject);
            }

            childrenGameObjects = accessories.childCount;
            for(int i = 0; i < childrenGameObjects; i++)
            {
                accessoriesModels.Add(accessories.GetChild(i).gameObject);
            }


            childrenGameObjects = head_Male.childCount;
            for (int i = 0; i < childrenGameObjects; i++)
            {
                maleHeadModels.Add(head_Male.GetChild(i).gameObject);
            }

            childrenGameObjects = helmet_Male.childCount;
            for (int i = 0; i < childrenGameObjects; i++)
            {
                maleHelmetModels.Add(helmet_Male.GetChild(i).gameObject);
            }
            childrenGameObjects = hat.childCount;
            for (int i = 0; i < childrenGameObjects; i++)
            {
                maleHelmetModels.Add(hat.GetChild(i).gameObject);
            }
            childrenGameObjects = notFaceCoveringHeadgear.childCount;
            for (int i = 0; i < childrenGameObjects; i++)
            {
                maleHelmetModels.Add(notFaceCoveringHeadgear.GetChild(i).gameObject);
            }

            childrenGameObjects = eyebrow_Male.childCount;
            for (int i = 0; i < childrenGameObjects; i++)
            {
                maleEyebrowModels.Add(eyebrow_Male.GetChild(i).gameObject);
            }

            childrenGameObjects = facialHair_Male.childCount;
            for (int i = 0; i < childrenGameObjects; i++)
            {
                facialHairModels.Add(facialHair_Male.GetChild(i).gameObject);
            }

            childrenGameObjects = faceCoveringFaciale.childCount;
            for (int i = 0; i < childrenGameObjects; i++)
            {
                maleHelmetModels.Add(faceCoveringFaciale.GetChild(i).gameObject);
                helmetModels.Add(faceCoveringFaciale.GetChild(i).gameObject);
            }
        }
    
        //ж������ͷ��ģ��:
        public void UnequipAllHelmetModels()
        {
            foreach(GameObject helmet in helmetModels)
            {
                helmet.SetActive(false);
            }
            foreach (GameObject helmet in maleHelmetModels)
            {
                helmet.SetActive(false);
            }

            foreach (GameObject accessories in accessoriesModels)
            {
                accessories.SetActive(false);
            }
        }

        //�������Ƹ���ͷ��ģ��:
        public void EquipHelmetModelByName(string helmetName, bool isMale , int[] accessoriesIDs = null)
        {
            if (isMale)
            {
                for (int i = 0; i < maleHelmetModels.Count; i++)
                {
                    if (maleHelmetModels[i].name.Equals(helmetName))
                    {
                        maleHelmetModels[i].SetActive(true);
                    }
                }


            }
            else
            {
                for(int i = 0; i < helmetModels.Count; i++)
                {
                    if (helmetModels[i].name.Equals(helmetName))
                    {
                        helmetModels[i].SetActive(true);
                    }
                }
            }


            //����ID����װ��ͷ����Ʒ:
            if(accessoriesIDs != null)
            {
                foreach(int i in accessoriesIDs)
                {
                    accessoriesModels[i].SetActive(true);
                }
            }
        }

        //ж��ͷ��ģ��:
        public void UnequipAllHeadModels()
        {
            foreach (GameObject head in headModels)
            {
                head.SetActive(false);
            }
            foreach(GameObject eyebrow in eyebrowModels)
            {
                eyebrow.SetActive(false);
            }

            foreach (GameObject head in maleHeadModels)
            {
                head.SetActive(false);
            }
            foreach (GameObject eyebrow in maleEyebrowModels)
            {
                eyebrow.SetActive(false);
            }
            
            foreach (GameObject hairstyle in hairstyleModels)
            {
                hairstyle.SetActive(false);
            }
            foreach (GameObject accessories in accessoriesModels)
            {
                accessories.SetActive(false);
            }

            foreach (GameObject facialHair in facialHairModels)
            {
                facialHair.SetActive(false);
            }
        }
        //����id����ͷ��ģ��
        public void EquipHeadModelById(int headId,int hairstyleId , int facialHairId, int eyebrowId , bool isMale)
        {
            if(isMale == false)
            {
                headModels[headId].SetActive(true);
                if(eyebrowId >= 0 && eyebrowId < eyebrowModels.Count)
                {
                    eyebrowModels[eyebrowId].SetActive(true);
                }
            }
            else
            {
                if (facialHairId >= 0)
                {
                    //���غ���ģ��
                    facialHairModels[facialHairId].SetActive(true);
                }
                else
                {
                    foreach (GameObject facialHair in facialHairModels)
                    {
                        facialHair.SetActive(false);
                    }
                }

                if (eyebrowId >= 0)
                {
                    maleEyebrowModels[eyebrowId].SetActive(true);
                }
                maleHeadModels[headId].SetActive(true);

            }
            
            if (hairstyleId >= 0)
            {
                hairstyleModels[hairstyleId].SetActive(true);
            }
        }
    
        //ж�ط���:
        public void UnequipHairstyleModels()
        {
            foreach (GameObject hairstyle in hairstyleModels)
            {
                hairstyle.SetActive(false);
            }
        }

        //����Ƥ����ɫ:
        public void SetHeadModelColor(string attributeSkinColor, Color color)
        {
            Material material = new Material(helmetModels[0].GetComponent<SkinnedMeshRenderer>().material);
            material.SetColor(attributeSkinColor, color);

            foreach(GameObject model in helmetModels)
            {
                SkinnedMeshRenderer skinnedMesh = model.GetComponent<SkinnedMeshRenderer>();
                skinnedMesh.material = material;
            }
            foreach (GameObject model in headModels)
            {
                SkinnedMeshRenderer skinnedMesh = model.GetComponent<SkinnedMeshRenderer>();
                skinnedMesh.material = material;
            }

            foreach (GameObject model in maleHelmetModels)
            {
                SkinnedMeshRenderer skinnedMesh = model.GetComponent<SkinnedMeshRenderer>();
                skinnedMesh.material = material;
            }
            foreach (GameObject model in maleHeadModels)
            {
                SkinnedMeshRenderer skinnedMesh = model.GetComponent<SkinnedMeshRenderer>();
                skinnedMesh.material = material;
            }
        }
        //����ͷ����ɫ:facialMarkColorl
        public void SetHairModelColor(string attributeHairColor, Color color)
        {
            Material material = new Material(helmetModels[0].GetComponent<SkinnedMeshRenderer>().material);
            material.SetColor(attributeHairColor, color);

            foreach (GameObject model in hairstyleModels)
            {
                SkinnedMeshRenderer skinnedMesh = model.GetComponent<SkinnedMeshRenderer>();
                if(skinnedMesh!=null)
                    skinnedMesh.material = material;
            }
            foreach (GameObject model in eyebrowModels)
            {
                SkinnedMeshRenderer skinnedMesh = model.GetComponent<SkinnedMeshRenderer>();
                skinnedMesh.material = material;
            }

            foreach (GameObject model in maleEyebrowModels)
            {
                SkinnedMeshRenderer skinnedMesh = model.GetComponent<SkinnedMeshRenderer>();
                skinnedMesh.material = material;
            }
            foreach (GameObject model in facialHairModels)
            {
                SkinnedMeshRenderer skinnedMesh = model.GetComponent<SkinnedMeshRenderer>();
                skinnedMesh.material = material;
            }
        }
        //�����沿Ϳѻ��ɫ:(Ҫ�ڸ���Ƥ���������ú�ʹ��)
        public void SetfacialMarkColorl(string attributeFacialMarkColor, Color color)
        {
            Material material = helmetModels[0].GetComponent<SkinnedMeshRenderer>().material;
            material.SetColor(attributeFacialMarkColor, color);

            foreach(GameObject model in headModels)
            {
                SkinnedMeshRenderer skinnedMesh = model.GetComponent<SkinnedMeshRenderer>();
                skinnedMesh.material = material;
            }
            foreach (GameObject model in maleHeadModels)
            {
                SkinnedMeshRenderer skinnedMesh = model.GetComponent<SkinnedMeshRenderer>();
                skinnedMesh.material = material;
            }
        }
    }
}
