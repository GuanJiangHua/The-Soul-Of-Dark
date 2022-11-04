using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class OptionButtonSlot : MonoBehaviour
    {
        public Text optionDescriptionText;

        PlayerManager player;                        //玩家
        NpcManager targetNpc;                     //目标npc
        OptionMethodClass optionMethod;  //选项方法

        public void SelectThisSlot()
        {
            optionMethod.OptionMethod(player , targetNpc);
        }

        public void AddOptionMethod(PlayerManager playerManager , NpcManager npcManager, OptionMethodClass option)
        {
            optionMethod = option;
            player = playerManager;
            targetNpc = npcManager;
            optionDescriptionText.text = optionMethod.optionDescription;

            //启用条件为空,或者非空时条件被满足：
            bool isEnable = option.enableOptionCondition == null || option.enableOptionCondition.CheckConditionsMet(playerManager);
            if (isEnable)
                gameObject.SetActive(true);
            else
                gameObject.SetActive(false);
        }
        public void ClearOptionMethod()
        {
            player = null;
            targetNpc = null;
            optionMethod = null;
            optionDescriptionText.text = null;

            gameObject.SetActive(false);
        }
    }
}