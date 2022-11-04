using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class BaseEffectBuildUpBar : MonoBehaviour
    {
        public Slider slider;

        private void Start()
        {
            slider = GetComponent<Slider>();
            slider.maxValue = 100;
            slider.value = 0;

            gameObject.SetActive(false);
        }

        public virtual void SetEffectBuildUpAmount(int currentEffectBuildUp)
        {
            slider.value = currentEffectBuildUp;
        }
    }
}