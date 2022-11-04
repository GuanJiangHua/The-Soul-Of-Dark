using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class OptionMethodClass : ScriptableObject
    {
        public string optionDescription;
        [Header("启用选项的条件:")]
        public ConditionClass enableOptionCondition;
        public virtual void OptionMethod(PlayerManager player , NpcManager npc) 
        {

        }
    }
}