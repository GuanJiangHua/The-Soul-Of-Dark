using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    //条件判断方法:
    public class ConditionClass : ScriptableObject
    {
        public virtual bool CheckConditionsMet(PlayerManager playerManager)
        {
            return false;
        }
    }
}