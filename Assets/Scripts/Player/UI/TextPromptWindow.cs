using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class TextPromptWindow : MonoBehaviour
    {
        public Text promptText;
        //给出提示文本:
        public void SetTextPrompt(string textPromptContent, float displayTime)
        {
            gameObject.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(SetTextContent(textPromptContent, displayTime));
        }
        public void SetTextPrompt(string[] textPromptContent, float displayTime)
        {
            gameObject.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(SetTextsContent(textPromptContent, displayTime));
        }

        IEnumerator SetTextContent(string textPromptContent, float displayTime)
        {
            promptText.text = textPromptContent;
            yield return new WaitForSeconds(displayTime);
            gameObject.SetActive(false);
        }

        IEnumerator SetTextsContent(string[] textPromptContent, float displayTime)
        {
            for(int i = 0; i < textPromptContent.Length; i++)
            {
                promptText.text = textPromptContent[i];
                yield return new WaitForSeconds(displayTime);
            }
            gameObject.SetActive(false);
        }

    }
}