using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    //�����жϷ���:
    public class ConditionClass : ScriptableObject
    {
        public virtual bool CheckConditionsMet(PlayerManager playerManager)
        {
            return false;
        }
    }
}