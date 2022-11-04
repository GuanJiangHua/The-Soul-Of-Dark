using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "���ߵ�ĳ����", menuName = "��Ϸ�Ի�ϵͳģ��/�½��Ի������ж�/���ߵ�ĳ����")]
    public class ToCertainProgres : ConditionClass
    {
        public int mainProgressDemand;
        public override bool CheckConditionsMet(PlayerManager playerManager)
        {
            PlotProgressManager plotProgress = FindObjectOfType<PlotProgressManager>();
            if (plotProgress.mainlineProgress >= mainProgressDemand)
                return true;
            else
                return false;
        }
    }
}