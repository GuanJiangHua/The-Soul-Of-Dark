using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class FocusPointBar : MonoBehaviour
    {
        public Slider slider;

        private void Awake()
        {
            slider = GetComponent<Slider>();
        }

        //设置最大耐力值：(滑动条的最大值)
        public void SetMaxFocusPoint(float maxFocusPoint)
        {
            slider.maxValue = maxFocusPoint;
            slider.value = maxFocusPoint;
        }
        //更新当前耐力值:（滑动条的值)
        public void SetCurrentFocusPoint(float currentFocusPoint)
        {
            slider.value = currentFocusPoint;
        }
    }
}
