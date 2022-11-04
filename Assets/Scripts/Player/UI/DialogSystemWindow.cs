using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class DialogSystemWindow : MonoBehaviour
    {
        public Text dialogueContentText;
        public Text dialogueTimeRemainingText;
        float singleDialogueTime = 0;

        UIManager uiManager;
        private void Awake()
        {
            uiManager = FindObjectOfType<UIManager>();
        }

        public void SkipDialogue(float dialogueTime = 1)
        {
            if(singleDialogueTime > dialogueTime)
            {
                singleDialogueTime = dialogueTime;
            }
        }

        //给出对话:
        public void GiveDialogueContentToUI(string[] dialogueContent , bool isActiveDialogue)
        {
            if (dialogueContent == null) return;

            StopAllCoroutines();
            StartCoroutine(GiveCurrentDialogueContentToUI(dialogueContent, isActiveDialogue, null, null, null));
        }
        public void GiveDialogueContentToUI(string[] dialogueContent, bool isActiveDialogue, OptionMethodClass[] optionMethods, PlayerManager player, NpcManager npc)
        {
            if (dialogueContent == null) return;
            if (optionMethods == null || optionMethods.Length < 1)
            {
                GiveDialogueContentToUI(dialogueContent, isActiveDialogue);
                return;
            }

            StopAllCoroutines();
            StartCoroutine(GiveCurrentDialogueContentToUI(dialogueContent, isActiveDialogue, player, npc, optionMethods));
        }

        //给出当前对话内容到UI
        IEnumerator GiveCurrentDialogueContentToUI(string[] dialogueContentOne, bool isActiveDialogue,PlayerManager player , NpcManager npc ,OptionMethodClass[] optionMethods)
        {
            string[] dialogueContent = dialogueContentOne;

            int dialogueIndex = 0;
            singleDialogueTime = dialogueContent[dialogueIndex].Length / 3 + 1f;
            StartCoroutine(FadesInAndOutText(dialogueContent[dialogueIndex]));

            while (singleDialogueTime > 0)
            {
                singleDialogueTime -= Time.fixedDeltaTime;
                if(singleDialogueTime >= 0)
                {
                    dialogueTimeRemainingText.text = singleDialogueTime.ToString("f1");
                }
                else
                {
                    dialogueTimeRemainingText.text = "0.0";
                }

                if(singleDialogueTime <= 0 && dialogueIndex < dialogueContent.Length - 1)
                {
                    dialogueIndex++;
                    singleDialogueTime = dialogueContent[dialogueIndex].Length / 3 + 1f;
                    //调用文本淡入淡出协程:
                    StartCoroutine(FadesInAndOutText(dialogueContent[dialogueIndex]));

                }
                yield return new WaitForFixedUpdate();
            }

            if (optionMethods != null && player != null && npc != null)
            {
                //弹出选项窗口:(如果有可选项)
                uiManager.dialogOptionsWindow.SetActive(true);
                uiManager.dialogOptionsWindowManager.UpdateOptionsWindow(player, npc, optionMethods);
                gameObject.SetActive(false);
            }
            else
            {
                if (isActiveDialogue)
                {
                    uiManager.playerManager.playerAnimatorManager.PlayTargetAnimation("PullOutRightHandWeapon", true);
                }
                gameObject.SetActive(false);
            }
        }

        //给出文本内容,并淡入淡出:
        IEnumerator FadesInAndOutText(string dialogue)
        {
            float alphaValue = 1;
            Color newColor = dialogueContentText.color;
            while (alphaValue <= 0)
            {
                alphaValue -= Time.deltaTime * 2;
                newColor.a = alphaValue;
                dialogueContentText.color = newColor;
                yield return null;
            }

            alphaValue = 0;
            newColor.a = 0;
            dialogueContentText.text = "\u3000\u3000" + dialogue;
            while (alphaValue <= 1)
            {
                alphaValue += Time.deltaTime * 2;
                newColor.a = alphaValue;
                dialogueContentText.color = newColor;
                yield return null;
            }
        }
    }
}