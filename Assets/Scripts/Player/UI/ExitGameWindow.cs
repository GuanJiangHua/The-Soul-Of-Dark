using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class ExitGameWindow : MonoBehaviour
    {
        
        [Header("退出游戏窗口:")]
        public GameObject exitGameWindow;
        public Camera headRenderingCamera;    //渲染头像的摄像机:

        UIManager uiManager;
        private void Awake()
        {
            uiManager = GetComponentInParent<UIManager>();
        }
        //启用渲染头像的摄像机[会在点击设置按钮时调用出来,顺便开启退出游戏窗口]
        public void EnableExitGameWindow()
        {
            //启用退出游戏窗口:
            exitGameWindow.SetActive(true);
            //玩家设置为正在退出游戏窗口:
            uiManager.playerManager.isExitGameWindow = true;
            //播放立定动画:
            uiManager.playerManager.playerAnimatorManager.PlayTargetAnimation("Idle_ExitWindow", true, 0);
            uiManager.playerManager.playerAnimatorManager.anim.SetBool("isExitWindow", true);
            //启用渲染头像摄像头
            headRenderingCamera.gameObject.SetActive(true);

            StartCoroutine(MobileCamera());
        }
        //禁用渲染头像的摄像机
        public void CloseExitGameWindow()
        {
            //禁用渲染头像摄像头
            headRenderingCamera.gameObject.SetActive(false);
            //玩家设置为不是在退出游戏窗口:
            uiManager.playerManager.isExitGameWindow = false;
            //取消立定动画:
            uiManager.playerManager.playerAnimatorManager.PlayTargetAnimation("Idle_eExitWindow", false);
            uiManager.playerManager.playerAnimatorManager.anim.SetBool("isExitWindow", false);
            //禁用退出游戏窗口:
            uiManager.exitGameWindow.SetActive(false);
        }

        public void ExitGameToMainMenu()
        {
            uiManager.playerManager.rebirthPosition = uiManager.playerManager.transform.position;
            PlayerSaveManager.SaveToFile(uiManager.playerManager, PlotProgressManager.single ,WorldEventManager.single);
        }

        IEnumerator MobileCamera()
        {
            //控制摄像头旋转
            float timer = 1;
            while(timer >= 0)
            {
                //将渲染头像摄像头移动到响应位置
                Vector3 targetPos = uiManager.playerManager.headPosition.position;
                headRenderingCamera.transform.position = Vector3.Lerp(headRenderingCamera.transform.position, targetPos, (1 - timer));
                timer -= Time.deltaTime;
                yield return null;
            }

            headRenderingCamera.transform.rotation = uiManager.playerManager.headPosition.rotation;
        }
    }
}