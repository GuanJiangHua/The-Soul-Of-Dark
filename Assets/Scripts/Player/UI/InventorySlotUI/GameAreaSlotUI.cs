using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class GameAreaSlotUI : MonoBehaviour
    {
        public Text areaNameText;               //���������ı�

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
            areaNameText.text = "���͵�����:" + areaName;
            //���¸õ�����һ�������ͼ��:
            campfireUIWindowManager.transferFunctionWindowManager.UpdateTransferPointSlot(areaIndex);
            //���¸������ڵ�������λ��:
            //...
        }
    }
}