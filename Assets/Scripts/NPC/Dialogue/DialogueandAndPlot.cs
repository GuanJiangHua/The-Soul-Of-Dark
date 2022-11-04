using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "对话与剧情", menuName = "游戏对话系统模块/新建新对话与剧情")]
    public class DialogueandAndPlot : ScriptableObject
    {
        public int dialogStatus = -1;   //[-1,0,1](未满足前提),(满足前提为满足后续),(满足后续)
        public string plotName;
        [Header("剧情开始的好感动:")]
        public int currentPlotFavorability = 0;
        [Header("剧情开始时npc位置:")]
        public Vector3 plotOpeningPosition;
        public Vector3 plotOpeningRotation;
        [Header("剧情开始时的动作:")]
        public string startAnimName = "Idle_Neutral";
        [Header("主线推进至某阶段时开始此剧情:")]
        public int startPlotAtMainIndex;
        //前提条件类:
        [Header("开启当前剧情条件:")]
        public ConditionClass prerequisite;
        //后续条件类:
        [Header("开启后续剧情条件:")]
        public ConditionClass subsequentCondition;

        [Header("未满足剧情条件对话内容:")]
        [TextArea] public string[] preconditionsNotMetDialogue;
        [Header("本次剧情对话内容:")]
        [TextArea] public string[] mainDialogue;
        [Header("满足后续剧情条件对话内容:")]
        [TextArea] public string[] meetSubsequentConditions;

        [Header("未满足剧情条件对话内容结束后的选项列表:")]
        public OptionMethodClass[] preconditionsNotMetOptions;
        [Header("本次剧情对话内容结束后的选项列表:")]
        public OptionMethodClass[] currentPlotOptions;

        [Header("后续剧情名称:")]
        public string followDialogueandAndPlotName;

        //获取剧情对话:
        public string[] GiveDialogue(PlayerManager playerManager , NpcManager npc)
        {
            switch (dialogStatus)
            {
                case -1:
                    if (prerequisite == null)
                        dialogStatus = 0;
                    else if (prerequisite.CheckConditionsMet(playerManager))
                        dialogStatus = 0;

                    break;
                case 0:
                    if (subsequentCondition == null)                                                          //后续条件是空;
                        dialogStatus = 1; 
                    else if (subsequentCondition.CheckConditionsMet(playerManager)) //不为空时,满足后续条件
                        dialogStatus = 1;

                    break;
            }

            switch (dialogStatus)
            {
                case -1:
                    //给出未满足前提对话内容:
                    return preconditionsNotMetDialogue;
                case 0:
                    //给出主对话:
                    return mainDialogue;
                case 1:
                    npc.currentPlotName = followDialogueandAndPlotName;//设置当前剧情名称为后续剧情
                    return meetSubsequentConditions;                    //给出后续对话
            }

            return preconditionsNotMetDialogue;
        }
        public OptionMethodClass[] GetOptions()
        {
            switch (dialogStatus)
            {
                case -1:
                    return preconditionsNotMetOptions;
                case 0:
                    return currentPlotOptions;
            }

            return null;
        }
    }

    [System.Serializable]
    public class DialogContentClass
    {
        [TextArea] public string[] dialogueContent;                              //对话内容
    }
}