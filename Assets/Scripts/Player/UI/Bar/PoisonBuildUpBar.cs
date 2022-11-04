using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class PoisonBuildUpBar : BaseEffectBuildUpBar
    {
        //更新当前毒液累积值:（滑动条的值)
        public override void SetEffectBuildUpAmount(int currentEffectBuildUp)
        {
            slider.value = currentEffectBuildUp;
        }
    }
}
