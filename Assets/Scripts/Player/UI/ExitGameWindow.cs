using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class ExitGameWindow : MonoBehaviour
    {
        
        [Header("�˳���Ϸ����:")]
        public GameObject exitGameWindow;
        public Camera headRenderingCamera;    //��Ⱦͷ��������:

        UIManager uiManager;
        private void Awake()
        {
            uiManager = GetComponentInParent<UIManager>();
        }
        //������Ⱦͷ��������[���ڵ�����ð�ťʱ���ó���,˳�㿪���˳���Ϸ����]
        public void EnableExitGameWindow()
        {
            //�����˳���Ϸ����:
            exitGameWindow.SetActive(true);
            //�������Ϊ�����˳���Ϸ����:
            uiManager.playerManager.isExitGameWindow = true;
            //������������:
            uiManager.playerManager.playerAnimatorManager.PlayTargetAnimation("Idle_ExitWindow", true, 0);
            uiManager.playerManager.playerAnimatorManager.anim.SetBool("isExitWindow", true);
            //������Ⱦͷ������ͷ
            headRenderingCamera.gameObject.SetActive(true);

            StartCoroutine(MobileCamera());
        }
        //������Ⱦͷ��������
        public void CloseExitGameWindow()
        {
            //������Ⱦͷ������ͷ
            headRenderingCamera.gameObject.SetActive(false);
            //�������Ϊ�������˳���Ϸ����:
            uiManager.playerManager.isExitGameWindow = false;
            //ȡ����������:
            uiManager.playerManager.playerAnimatorManager.PlayTargetAnimation("Idle_eExitWindow", false);
            uiManager.playerManager.playerAnimatorManager.anim.SetBool("isExitWindow", false);
            //�����˳���Ϸ����:
            uiManager.exitGameWindow.SetActive(false);
        }

        public void ExitGameToMainMenu()
        {
            uiManager.playerManager.rebirthPosition = uiManager.playerManager.transform.position;
            PlayerSaveManager.SaveToFile(uiManager.playerManager, PlotProgressManager.single ,WorldEventManager.single);
        }

        IEnumerator MobileCamera()
        {
            //��������ͷ��ת
            float timer = 1;
            while(timer >= 0)
            {
                //����Ⱦͷ������ͷ�ƶ�����Ӧλ��
                Vector3 targetPos = uiManager.playerManager.headPosition.position;
                headRenderingCamera.transform.position = Vector3.Lerp(headRenderingCamera.transform.position, targetPos, (1 - timer));
                timer -= Time.deltaTime;
                yield return null;
            }

            headRenderingCamera.transform.rotation = uiManager.playerManager.headPosition.rotation;
        }
    }
}