using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class OptionButtonSlot : MonoBehaviour
    {
        public Text optionDescriptionText;

        PlayerManager player;                        //���
        NpcManager targetNpc;                     //Ŀ��npc
        OptionMethodClass optionMethod;  //ѡ���

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

            //��������Ϊ��,���߷ǿ�ʱ���������㣺
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