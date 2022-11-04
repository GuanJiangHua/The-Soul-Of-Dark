using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class BaseEffectAmountBar : MonoBehaviour
    {
        public Slider slider;

        private void Start()
        {
            slider = GetComponent<Slider>();

            gameObject.SetActive(false);
        }

        //设置最大异常效果值:
        public virtual void SetMaxEffectAmount(float maxEffectAmount)
        {
            slider.maxValue = maxEffectAmount;
            slider.value = maxEffectAmount;
        }
        //更新当前异常效果值:（缓慢下降，直至解除异常效果)
        public virtual void SetEffectAmount(int effectAmount)
        {
            slider.value = effectAmount;
        }
    }
}
