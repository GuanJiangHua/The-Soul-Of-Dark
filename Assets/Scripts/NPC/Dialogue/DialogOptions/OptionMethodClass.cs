using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class OptionMethodClass : ScriptableObject
    {
        public string optionDescription;
        [Header("����ѡ�������:")]
        public ConditionClass enableOptionCondition;
        public virtual void OptionMethod(PlayerManager player , NpcManager npc) 
        {

        }
    }
}