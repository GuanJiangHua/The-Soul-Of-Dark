using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "主线到某进度", menuName = "游戏对话系统模块/新建对话条件判断/主线到某进度")]
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