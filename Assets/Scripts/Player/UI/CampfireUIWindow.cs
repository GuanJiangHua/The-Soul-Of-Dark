using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{

    public class CampfireUIWindow : MonoBehaviour    //篝火功能窗口
    {
        public Text placeNameText;  //地点名称的文本引用
        [Header("传送功能窗口:")]
        public GameObject transferFunctionWindow;
        public TransferFunctionWindow transferFunctionWindowManager;
        [Header("记忆法术功能窗口:")]
        public GameObject memorySpellWindow;
        public MemorySpellWindow memorySpellWindowManaget;

        [Header("分配元素瓶窗口:")]
        public GameObject assignElementBottleWindow;
        public AssignElementBottleWindow assignElementBottleWindowManager;

        [Header("强化元素瓶窗口:")]
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

        //启用传送窗口:
        public void EnableTransferFunctionWindow()
        {
            transferFunctionWindow.SetActive(true);
            transferFunctionWindowManager.UpdateTransferFunctionWindow();
            transferFunctionWindowManager.UpdateTransferPointSlot(0);
        }
        //禁用传送窗口:
        public void CloseTransferFunctionWindow()
        {
            transferFunctionWindow.SetActive(false);
        }

        //启用记忆法术功能窗口:
        public void EnableMemorySpellWindow()
        {
            memorySpellWindow.SetActive(true);
            memorySpellWindowManaget.UpdateSpellInventorySlot();
        }
        //禁用记忆法术功能窗口:
        public void CloseMemorySpellWindow()
        {
            memorySpellWindow.SetActive(false);
        }

        //注入元素结晶方法:
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

        //启用分配元素瓶的窗口:
        public void EnableAssignElementBottleWindow()
        {
            assignElementBottleWindowManager.uiManager = uiManager;
            assignElementBottleWindowManager.EnableAssignElementBottleWindow();
            gameObject.SetActive(false);
        }
        //禁用分配元素瓶的窗口:
        public void CloseAssignElementBottleWindow()
        {
            assignElementBottleWindowManager.uiManager = uiManager;
            assignElementBottleWindowManager.CloseAssignElementBottleWindow(true);
        }

        //离去方法:
        public void PlayerLeave()
        {
            //结束玩家正在休息的状态:
            uiManager.playerManager.isResting = false;
            //关闭篝火功能窗口:
            uiManager.CloseCampfireUIWindow();
            //玩家播放起身动画:
            uiManager.playerManager.playerAnimatorManager.PlayTargetAnimation("Sit-Standup", true);
        }
    }
}