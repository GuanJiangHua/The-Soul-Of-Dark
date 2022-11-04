using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class TransferFunctionWindow : MonoBehaviour
    {
        public GameObject transferFunctionWindow;                //传送功能窗口;
        //地区显示槽:
        [Header("区域显示槽:")]
        public GameObject gameAreaSlotPreform;                   //地区列表槽预制体
        public Transform gameAreaSlotsParent;                        //地区列表父物体
        GameAreaSlotUI[] gameAreaSlotUIs;
        Vector2 gameAreaSlotsParentOriginalSize;
        Vector2 gameAreaSlotsParentOriginalPosition;
        [Header("传送篝火位置槽:")]
        public GameObject transferPointSlotPreform;              //传送点槽位预制体;
        public Transform transferPointSlotParent;                    //传送点列表父物体;
        TransmissionPlaceSlotUI[] transferPointSlotUIs;
        Vector2 transferPointSlotsParentOriginalSize;
        Vector2 transferPointSlotsParentOriginalPosition;

        UIManager uiManager;

        private void Awake()
        {
            uiManager = FindObjectOfType<UIManager>();

            gameAreaSlotsParentOriginalSize = gameAreaSlotsParent.GetComponent<RectTransform>().sizeDelta;
            gameAreaSlotsParentOriginalPosition = gameAreaSlotsParent.GetComponent<RectTransform>().anchoredPosition;

            transferPointSlotsParentOriginalSize = transferPointSlotParent.GetComponent<RectTransform>().sizeDelta;
            transferPointSlotsParentOriginalPosition = transferPointSlotParent.GetComponent<RectTransform>().anchoredPosition;
        }
        //更新传送窗口的区域列表:
        public void UpdateTransferFunctionWindow()
        {
            GameArea[] allGameArea = WorldEventManager.single.gameAreas;
            gameAreaSlotUIs = GetInChildSlotUI<GameAreaSlotUI>(gameAreaSlotsParent);
            for(int i = 0; i < gameAreaSlotUIs.Length; i++)
            {
                if(i < allGameArea.Length)
                {
                    if(gameAreaSlotUIs.Length < allGameArea.Length)
                    {
                        Instantiate(gameAreaSlotPreform, gameAreaSlotsParent);
                        gameAreaSlotUIs = GetInChildSlotUI<GameAreaSlotUI>(gameAreaSlotsParent);
                    }
                    if (allGameArea[i].isOnceMet)
                    {
                        gameAreaSlotUIs[i].SetNameAndIndex(allGameArea[i].areaName, i);
                    }
                    else
                    {
                        gameAreaSlotUIs[i].ClearNameAndIndex();
                    }
                }
                else
                {
                    gameAreaSlotUIs[i].ClearNameAndIndex();
                }
            }

            int enlargeNumber = allGameArea.Length / 8;
            Vector2 newSize = gameAreaSlotsParentOriginalSize;
            newSize.x += newSize.x / 8 * enlargeNumber;
            Vector2 newPosition = gameAreaSlotsParentOriginalPosition;
            newPosition.x += newPosition.y / 16 * enlargeNumber;

            gameAreaSlotsParent.GetComponent<RectTransform>().sizeDelta = newSize;
            gameAreaSlotsParent.GetComponent<RectTransform>().anchoredPosition = newPosition;
        }
        //传送篝火显示槽:
        public void UpdateTransferPointSlot(int gameAreaIndex)
        {
            List<BonfireLocation> bonfireLocations = WorldEventManager.single.gameAreas[gameAreaIndex].bonfireInArea;
            transferPointSlotUIs = GetInChildSlotUI<TransmissionPlaceSlotUI>(transferPointSlotParent);
            for(int i = 0; i < transferPointSlotUIs.Length; i++)
            {
                if(i < bonfireLocations.Count)
                {
                    if(transferPointSlotUIs.Length < bonfireLocations.Count)
                    {
                        Instantiate(transferPointSlotPreform, transferPointSlotParent);
                        transferPointSlotUIs = GetInChildSlotUI<TransmissionPlaceSlotUI>(transferPointSlotParent);
                    }

                    if (bonfireLocations[i].isOnceMet)
                    {
                        transferPointSlotUIs[i].SetThisLocation(bonfireLocations[i] , uiManager.playerManager);
                    }
                    else
                    {
                        transferPointSlotUIs[i].ResetTheLocation();
                    }

                }
                else
                {
                    transferPointSlotUIs[i].ResetTheLocation();
                }
            }

            int enlargeNumber = bonfireLocations.Count / 5;
            Vector2 newSize = transferPointSlotsParentOriginalSize;
            newSize.x += newSize.x / 5 * enlargeNumber;
            Vector2 newPosition = transferPointSlotsParentOriginalPosition;
            newPosition.x += newPosition.y / 10 * enlargeNumber;

            transferPointSlotParent.GetComponent<RectTransform>().sizeDelta = newSize;
            transferPointSlotParent.GetComponent<RectTransform>().anchoredPosition = newPosition;
        }

        private T[] GetInChildSlotUI<T>(Transform parent)
        {
            T[] slotUIs = new T[parent.childCount];
            for(int i = 0; i < slotUIs.Length; i++)
            {
                slotUIs[i] = parent.GetChild(i).GetComponent<T>();
            }

            return slotUIs;
        }
    }
}