using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class StaminaBar : MonoBehaviour
    {
        public Slider slider;

        private void Awake()
        {
            slider = GetComponent<Slider>();
        }

        //设置最大耐力值：(滑动条的最大值)
        public void SetMaxStamina(float maxStamina)
        {
            slider.maxValue = maxStamina;
            slider.value = maxStamina;
        }
        //更新当前耐力值:（滑动条的值)
        public void SetCurrentStamina(float currentStamina)
        {
            slider.value = currentStamina;
        }
    }
}
