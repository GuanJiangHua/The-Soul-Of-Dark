using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class WeaponFX : MonoBehaviour
    {
        [Header("武器拖尾粒子效果:")]
        public ParticleSystem normalWeaponTrail;    //普通拖尾粒子效果;
        //火焰拖尾粒子效果
        //雷电拖尾粒子效果
        //冰霜拖尾粒子效果

        //播放粒子效果:
        public void PlayWeaponFX()
        {
            normalWeaponTrail.Stop();

            //如果已经停止播放,则重新开始:
            if (normalWeaponTrail.isStopped)
            {
                normalWeaponTrail.Play();
            }
        }
    }
}
