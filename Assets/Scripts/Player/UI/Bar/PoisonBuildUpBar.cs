using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class PoisonBuildUpBar : BaseEffectBuildUpBar
    {
        //���µ�ǰ��Һ�ۻ�ֵ:����������ֵ)
        public override void SetEffectBuildUpAmount(int currentEffectBuildUp)
        {
            slider.value = currentEffectBuildUp;
        }
    }
}
