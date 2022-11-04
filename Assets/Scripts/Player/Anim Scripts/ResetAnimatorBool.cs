using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [System.Serializable]
    public struct BoolAttributePairs
    {
        public string boolAttributeName;
        public bool status;
    }
    public class ResetAnimatorBool : StateMachineBehaviour
    {
        public List<BoolAttributePairs> boolAttributeNames = new List<BoolAttributePairs>();

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            CharacterManager characterManager = animator.GetComponent<CharacterManager>();
            characterManager.isUsingLeftHand = false;
            characterManager.isUsingRightHand = false;

            foreach (BoolAttributePairs targetBool in boolAttributeNames)
            {
                animator.SetBool(targetBool.boolAttributeName, targetBool.status);
            }
        }
    }
}