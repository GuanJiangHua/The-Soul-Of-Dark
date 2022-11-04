using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class DeathPromptWindow : MonoBehaviour
    {
        [Header("��ʾ�ı�:")]
        public Text promptText;
        [Header("ȷ�ϰ�ť:")]
        public Button confirmButton;
        [Header("���ϰ�ť:")]
        public Button denyButton;

        [Header("ȷ�ϰ�ť�¼�:")]
        public NoParameterEvent confirmButtonEvent;
        [Header("���ϰ�ť�¼�:")]
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