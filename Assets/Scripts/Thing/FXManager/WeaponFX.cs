using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class WeaponFX : MonoBehaviour
    {
        [Header("������β����Ч��:")]
        public ParticleSystem normalWeaponTrail;    //��ͨ��β����Ч��;
        //������β����Ч��
        //�׵���β����Ч��
        //��˪��β����Ч��

        //��������Ч��:
        public void PlayWeaponFX()
        {
            normalWeaponTrail.Stop();

            //����Ѿ�ֹͣ����,�����¿�ʼ:
            if (normalWeaponTrail.isStopped)
            {
                normalWeaponTrail.Play();
            }
        }
    }
}
