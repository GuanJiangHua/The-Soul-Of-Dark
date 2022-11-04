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

        //��������쳣Ч��ֵ:
        public virtual void SetMaxEffectAmount(float maxEffectAmount)
        {
            slider.maxValue = maxEffectAmount;
            slider.value = maxEffectAmount;
        }
        //���µ�ǰ�쳣Ч��ֵ:�������½���ֱ������쳣Ч��)
        public virtual void SetEffectAmount(int effectAmount)
        {
            slider.value = effectAmount;
        }
    }
}
