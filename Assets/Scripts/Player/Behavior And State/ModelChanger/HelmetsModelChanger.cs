using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    //头盔模型转换器:
    public class HelmetsModelChanger : MonoBehaviour
    {

        public Transform hat;                                         //帽子
        public Transform hairstyle;                                 //发型
        public Transform notFaceCoveringHeadgear;    //非覆发头盔
        public Transform faceCoveringFaciale;               //覆盖面部的头盔
        public Transform accessories;                             //饰品
        [Header("女性模型父物体引用:")]
        public Transform helmet;       //头盔
        public Transform head;          //头
        public Transform eyebrow;     //眉毛
        [Header("男性模型父物体引用:")]
        public Transform facialHair_Male;   //面部毛发;
        public Transform helmet_Male;       //头盔
        public Transform head_Male;          //头
        public Transform eyebrow_Male;     //眉毛

        [Header("模型集合:")]
        public List<GameObject> hairstyleModels = new List<GameObject>();      //发型模型
        public List<GameObject> accessoriesModels = new List<GameObject>();  //饰品模型

        public List<GameObject> helmetModels = new List<GameObject>();      //头盔模型
        public List<GameObject> headModels = new List<GameObject>();         //头部模型
        public List<GameObject> eyebrowModels = new List<GameObject>();   //眉毛模型

        public List<GameObject> maleHelmetModels = new List<GameObject>();     //头盔模型
        public List<GameObject> facialHairModels = new List<GameObject>();          //面部毛发
        public List<GameObject> maleHeadModels = new List<GameObject>();        //男性头部模型
        public List<GameObject> maleEyebrowModels = new List<GameObject>();   //男性眉毛模型
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
    
        //卸载所有头盔模型:
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

        //根据名称更换头盔模型:
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


            //根据ID数组装备头部饰品:
            if(accessoriesIDs != null)
            {
                foreach(int i in accessoriesIDs)
                {
                    accessoriesModels[i].SetActive(true);
                }
            }
        }

        //卸载头部模型:
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
        //根据id更换头部模型
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
                    //加载胡须模型
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
    
        //卸载发型:
        public void UnequipHairstyleModels()
        {
            foreach (GameObject hairstyle in hairstyleModels)
            {
                hairstyle.SetActive(false);
            }
        }

        //更换皮肤颜色:
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
        //更换头发颜色:facialMarkColorl
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
        //更换面部涂鸦颜色:(要在更换皮肤方法调用后使用)
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
