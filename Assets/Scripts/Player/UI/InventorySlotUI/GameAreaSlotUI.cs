using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class GameAreaSlotUI : MonoBehaviour
    {
        public Text areaNameText;               //区域名称文本

        string areaName;
        int areaIndex;
        CampfireUIWindow campfireUIWindowManager;
        private void Awake()
        {
            campfireUIWindowManager = FindObjectOfType<CampfireUIWindow>();
        }
        public void SetNameAndIndex(string areaName, int index)
        {
            this.areaName = areaName;
            this.areaIndex = index;
            gameObject.SetActive(true);
        }
        public void ClearNameAndIndex()
        {
            areaName = null;
            areaIndex = -1;
            gameObject.SetActive(false);
        }

        public void MouseDown()
        {
            areaNameText.text = "传送地名称:" + areaName;
            //更新该地区第一个篝火的图像:
            campfireUIWindowManager.transferFunctionWindowManager.UpdateTransferPointSlot(areaIndex);
            //更新该区域内的篝火传送位置:
            //...
        }
    }
}