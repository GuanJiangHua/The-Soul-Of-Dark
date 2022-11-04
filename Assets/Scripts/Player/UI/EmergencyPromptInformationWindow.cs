using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class EmergencyPromptInformationWindow : MonoBehaviour
    {
        public Image background;
        public Text informationText;
        public Image frame;

        public static EmergencyPromptInformationWindow single;
        private void Awake()
        {
            single = this;
        }

        //启用提示:
        public void EnablePrompt(string promptContent , int transitionTime = 1)
        {
            informationText.text = promptContent;

            StartCoroutine(SlowOpeningPrompt(transitionTime));
        }
        //禁用提示:
        public void ClosingPrompt()
        {
            informationText.text = null;

            StopCoroutine(SlowOpeningPrompt(2));

            StartCoroutine(SlowClosingPrompt(2));
        }

        IEnumerator SlowOpeningPrompt(int transitionTime)
        {
            float timer = 0;
            Color backgroundColor = background.color;
            Color informationTextColor = informationText.color;
            Color frameColor = frame.color;

            while (timer <= transitionTime)
            {
                timer += Time.deltaTime;
                float alpha = timer / transitionTime;
                backgroundColor.a = alpha;
                informationTextColor.a = alpha;
                frameColor.a = alpha;

                background.color = backgroundColor;
                informationText.color = informationTextColor;
                frame.color = frameColor;
                yield return null;
            }

            Invoke("ClosingPrompt", 2);
        }
        IEnumerator SlowClosingPrompt(int transitionTime)
        {
            float timer = transitionTime;
            Color backgroundColor = background.color;
            Color informationTextColor = informationText.color;
            Color frameColor = frame.color;

            while (timer >= 0)
            {
                timer -= Time.deltaTime;
                float alpha = timer / transitionTime;
                backgroundColor.a = alpha;
                informationTextColor.a = alpha;
                frameColor.a = alpha;

                background.color = backgroundColor;
                informationText.color = informationTextColor;
                frame.color = frameColor;

                yield return null;
            }
        }
    }
}