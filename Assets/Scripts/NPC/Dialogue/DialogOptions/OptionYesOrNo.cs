using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "�ǻ���ѡ��", menuName = "��Ϸ�Ի�ϵͳģ��/�½��¶Ի�ѡ��/�ǻ���ѡ��")]
    public class OptionYesOrNo : OptionMethodClass
    {
        [Header("�Ƿ��ƽ�����:")]
        public bool promotePlot;
        [Header("�Ƿ������������:")]
        public bool canGotoPlot;
        public string otherPlotName;
        [Header("ѡ���ѡ���Ի�:")]
        public string[] newDialogueContent = { "���������ðɡ�" };
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