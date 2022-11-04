using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class DeathPromptWindow : MonoBehaviour
    {
        [Header("提示文本:")]
        public Text promptText;
        [Header("确认按钮:")]
        public Button confirmButton;
        [Header("否认按钮:")]
        public Button denyButton;

        [Header("确认按钮事件:")]
        public NoParameterEvent confirmButtonEvent;
        [Header("否认按钮事件:")]
        public NoParameterEvent denyButtonEvent;

        public void EnableConfirmButtonEvent()
        {
            confirmButtonEvent.Invoke();
        }

        public void EnableDenyButtonEvent()
        {
            denyButtonEvent.Invoke();
        }
    }
}