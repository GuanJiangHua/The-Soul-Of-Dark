using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "是或不是选项", menuName = "游戏对话系统模块/新建新对话选项/是或不是选项")]
    public class OptionYesOrNo : OptionMethodClass
    {
        [Header("是否推进剧情:")]
        public bool promotePlot;
        [Header("是否进入其他剧情:")]
        public bool canGotoPlot;
        public string otherPlotName;
        [Header("选择此选项后对话:")]
        public string[] newDialogueContent = { "这样啊，好吧。" };
        public override void OptionMethod(PlayerManager player, NpcManager npc)
        {
            if (promotePlot)
            {
                npc.currentDialogueAndPlot.dialogStatus++;
            }

            if (canGotoPlot)
            {
                npc.currentPlotName = otherPlotName;
            }

            npc.uiManager.dialogSystemWindow.SetActive(true);
            npc.uiManager.dialogSystemWindowManager.GiveDialogueContentToUI(newDialogueContent, true);
        }
    }
}