using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class NpcManager : MonoBehaviour
    {
        public string npcName;
        public int favorability = 1;    //只有好感动为0以上才能正常交流
        [Header("允许被攻击次数:")]
        public int numberAttacks = 0;
        [Header("当前剧情名称:")]
        public string currentPlotName;
        [Header("当前对话:")]
        //当前对话:
        public DialogueandAndPlot currentDialogueAndPlot;
        //当前对话内容:
        public string[] currentDialogContent = { };
        //中立状态机:
        [Header("状态机:")]
        public NeutralState neutralState;
        public IdleState idleState;

        //对话交互检查:
        [Header("对话交互检查:")]
        public DialogueInteraction dialogueInteraction;
        [Header("被攻击时的劝阻对话:")]
        [TextArea] public string[] dissuadeDialogueContent = {"你在干什么?","快停下!","好吧，竟然如此我是不会畏惧的!" };
        [Header("遗言:")]
        [TextArea] public string[] lastWord = {"诅咒你屠夫！","不,我还不能倒下","居然是这样的结局吗？愿你前路有火的指引..."};

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
        //给出劝阻对话:(被攻击时)
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
        //给出遗言:(根据好感度:)
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