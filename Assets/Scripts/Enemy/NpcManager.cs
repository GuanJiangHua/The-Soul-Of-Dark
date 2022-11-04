using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class NpcManager : MonoBehaviour
    {
        public string npcName;
        public int favorability = 1;    //ֻ�кøж�Ϊ0���ϲ�����������
        [Header("������������:")]
        public int numberAttacks = 0;
        [Header("��ǰ��������:")]
        public string currentPlotName;
        [Header("��ǰ�Ի�:")]
        //��ǰ�Ի�:
        public DialogueandAndPlot currentDialogueAndPlot;
        //��ǰ�Ի�����:
        public string[] currentDialogContent = { };
        //����״̬��:
        [Header("״̬��:")]
        public NeutralState neutralState;
        public IdleState idleState;

        //�Ի��������:
        [Header("�Ի��������:")]
        public DialogueInteraction dialogueInteraction;
        [Header("������ʱ��Ȱ��Ի�:")]
        [TextArea] public string[] dissuadeDialogueContent = {"���ڸ�ʲô?","��ͣ��!","�ðɣ���Ȼ������ǲ���η���!" };
        [Header("����:")]
        [TextArea] public string[] lastWord = {"����������","��,�һ����ܵ���","��Ȼ�������Ľ����Ը��ǰ·�л��ָ��..."};

        [HideInInspector] public UIManager uiManager;
        [HideInInspector] public EnemyManager enemyManager;
        [HideInInspector] public StoreManager storeManager;
        private void Awake()
        {
            uiManager = FindObjectOfType<UIManager>();
            enemyManager = GetComponent<EnemyManager>();
            storeManager = GetComponent<StoreManager>();

            FindObjectOfType<PlotProgressManager>().NpcRegistrationMethod(this);
        }

        public void InitializeByPlot(bool isHostile)
        {
            string startAnim = "Idle_Neutral";
            if (currentDialogueAndPlot != null)
            {
                favorability = currentDialogueAndPlot.currentPlotFavorability;
                transform.position = currentDialogueAndPlot.plotOpeningPosition;
                transform.rotation = Quaternion.Euler(currentDialogueAndPlot.plotOpeningRotation);
                startAnim = currentDialogueAndPlot.startAnimName;

                if(PlotProgressManager.single.mainlineProgress == currentDialogueAndPlot.currentPlotFavorability)
                {

                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
            else
            {
                gameObject.SetActive(false);
            }

            if (favorability > -1)
            {
                enemyManager.currentState = neutralState;

                enemyManager.enemyAnimatorManager.rigBuilder.enabled = true;
                enemyManager.enemyAnimatorManager.anim.SetBool("isNeutral", true);
                enemyManager.enemyAnimatorManager.PlayTargetAnimation(startAnim, true);

                dialogueInteraction.enabled = true;
            }
            else
            {
                enemyManager.currentState = idleState;

                enemyManager.enemyAnimatorManager.rigBuilder.enabled = false;
                enemyManager.enemyAnimatorManager.anim.SetBool("isNeutral", false);

                dialogueInteraction.enabled = false;
            }

            if (isHostile)
            {
                favorability = -1;
                dialogueInteraction.enabled = false;
                enemyManager.currentState = idleState;
            }
        }
        //����Ȱ��Ի�:(������ʱ)
        public void GiveExhortDialogue()
        {
            if (numberAttacks > 2) 
            {
                dialogueInteraction.enabled = false;
                return; 
            }
            string[] stringArray = { dissuadeDialogueContent[numberAttacks] };

            uiManager.dialogSystemWindow.SetActive(true);
            uiManager.dialogSystemWindowManager.GiveDialogueContentToUI(stringArray, false);

            numberAttacks++;
        }
        //��������:(���ݺøж�:)
        public void GiveLastWord()
        {
            string[] stringArray = new string[1];
            switch (favorability)
            {
                case -1:
                    stringArray[0] = lastWord[0];
                    break;
                case 0:
                    stringArray[0] = lastWord[1];
                    break;
                case 1:
                    stringArray[0] = lastWord[2];
                    break;
            }
            uiManager.dialogSystemWindow.SetActive(true);
            uiManager.dialogSystemWindowManager.GiveDialogueContentToUI(stringArray, false);
        }
    }
}