using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "�Ի������", menuName = "��Ϸ�Ի�ϵͳģ��/�½��¶Ի������")]
    public class DialogueandAndPlot : ScriptableObject
    {
        public int dialogStatus = -1;   //[-1,0,1](δ����ǰ��),(����ǰ��Ϊ�������),(�������)
        public string plotName;
        [Header("���鿪ʼ�ĺøж�:")]
        public int currentPlotFavorability = 0;
        [Header("���鿪ʼʱnpcλ��:")]
        public Vector3 plotOpeningPosition;
        public Vector3 plotOpeningRotation;
        [Header("���鿪ʼʱ�Ķ���:")]
        public string startAnimName = "Idle_Neutral";
        [Header("�����ƽ���ĳ�׶�ʱ��ʼ�˾���:")]
        public int startPlotAtMainIndex;
        //ǰ��������:
        [Header("������ǰ��������:")]
        public ConditionClass prerequisite;
        //����������:
        [Header("����������������:")]
        public ConditionClass subsequentCondition;

        [Header("δ������������Ի�����:")]
        [TextArea] public string[] preconditionsNotMetDialogue;
        [Header("���ξ���Ի�����:")]
        [TextArea] public string[] mainDialogue;
        [Header("����������������Ի�����:")]
        [TextArea] public string[] meetSubsequentConditions;

        [Header("δ������������Ի����ݽ������ѡ���б�:")]
        public OptionMethodClass[] preconditionsNotMetOptions;
        [Header("���ξ���Ի����ݽ������ѡ���б�:")]
        public OptionMethodClass[] currentPlotOptions;

        [Header("������������:")]
        public string followDialogueandAndPlotName;

        //��ȡ����Ի�:
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
                    if (subsequentCondition == null)                                                          //���������ǿ�;
                        dialogStatus = 1; 
                    else if (subsequentCondition.CheckConditionsMet(playerManager)) //��Ϊ��ʱ,�����������
                        dialogStatus = 1;

                    break;
            }

            switch (dialogStatus)
            {
                case -1:
                    //����δ����ǰ��Ի�����:
                    return preconditionsNotMetDialogue;
                case 0:
                    //�������Ի�:
                    return mainDialogue;
                case 1:
                    npc.currentPlotName = followDialogueandAndPlotName;//���õ�ǰ��������Ϊ��������
                    return meetSubsequentConditions;                    //���������Ի�
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
        [TextArea] public string[] dialogueContent;                              //�Ի�����
    }
}