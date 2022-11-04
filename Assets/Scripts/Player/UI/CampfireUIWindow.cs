using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{

    public class CampfireUIWindow : MonoBehaviour    //�����ܴ���
    {
        public Text placeNameText;  //�ص����Ƶ��ı�����
        [Header("���͹��ܴ���:")]
        public GameObject transferFunctionWindow;
        public TransferFunctionWindow transferFunctionWindowManager;
        [Header("���䷨�����ܴ���:")]
        public GameObject memorySpellWindow;
        public MemorySpellWindow memorySpellWindowManaget;

        [Header("����Ԫ��ƿ����:")]
        public GameObject assignElementBottleWindow;
        public AssignElementBottleWindow assignElementBottleWindowManager;

        [Header("ǿ��Ԫ��ƿ����:")]
        public GameObject fortifiedElementBottleWindow;
        public FortifiedElementBottleWindow fortifiedElementBottleWindowManager;

        UIManager uiManager;
        private void Awake()
        {
            uiManager = FindObjectOfType<UIManager>();

            transferFunctionWindowManager = transferFunctionWindow.GetComponent<TransferFunctionWindow>();
            memorySpellWindowManaget = memorySpellWindow.GetComponent<MemorySpellWindow>();
            assignElementBottleWindowManager = assignElementBottleWindow.GetComponent<AssignElementBottleWindow>();
            fortifiedElementBottleWindowManager = fortifiedElementBottleWindow.GetComponent<FortifiedElementBottleWindow>();
        }

        //���ô��ʹ���:
        public void EnableTransferFunctionWindow()
        {
            transferFunctionWindow.SetActive(true);
            transferFunctionWindowManager.UpdateTransferFunctionWindow();
            transferFunctionWindowManager.UpdateTransferPointSlot(0);
        }
        //���ô��ʹ���:
        public void CloseTransferFunctionWindow()
        {
            transferFunctionWindow.SetActive(false);
        }

        //���ü��䷨�����ܴ���:
        public void EnableMemorySpellWindow()
        {
            memorySpellWindow.SetActive(true);
            memorySpellWindowManaget.UpdateSpellInventorySlot();
        }
        //���ü��䷨�����ܴ���:
        public void CloseMemorySpellWindow()
        {
            memorySpellWindow.SetActive(false);
        }

        //ע��Ԫ�ؽᾧ����:
        public void EnableFortifiedElementBottleWindow()
        {
            fortifiedElementBottleWindow.SetActive(true);
            fortifiedElementBottleWindowManager.EnableFortifiedElementBottleWindow(uiManager.playerManager, uiManager);
            gameObject.SetActive(false);
        }
        public void CloseFortifiedElementBottleWindow()
        {
            fortifiedElementBottleWindow.SetActive(false);
        }

        //���÷���Ԫ��ƿ�Ĵ���:
        public void EnableAssignElementBottleWindow()
        {
            assignElementBottleWindowManager.uiManager = uiManager;
            assignElementBottleWindowManager.EnableAssignElementBottleWindow();
            gameObject.SetActive(false);
        }
        //���÷���Ԫ��ƿ�Ĵ���:
        public void CloseAssignElementBottleWindow()
        {
            assignElementBottleWindowManager.uiManager = uiManager;
            assignElementBottleWindowManager.CloseAssignElementBottleWindow(true);
        }

        //��ȥ����:
        public void PlayerLeave()
        {
            //�������������Ϣ��״̬:
            uiManager.playerManager.isResting = false;
            //�ر������ܴ���:
            uiManager.CloseCampfireUIWindow();
            //��Ҳ���������:
            uiManager.playerManager.playerAnimatorManager.PlayTargetAnimation("Sit-Standup", true);
        }
    }
}