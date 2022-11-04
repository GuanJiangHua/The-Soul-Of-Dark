using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG {
    //����������:
    [CreateAssetMenu(fileName = "����������ѡ��", menuName = "��Ϸ�Ի�ϵͳģ��/�½��¶Ի�ѡ��/����������ѡ��")]
    public class OpenUpgradeWindowDialogue : OptionMethodClass
    {
        public override void OptionMethod(PlayerManager playerManger, NpcManager npc)
        {
            playerManger.uiManager.playerLeveUpWindow.SetActive(true);  //������������

            //��������������:
            playerManger.playerAnimatorManager.PlayTargetAnimation("Praying", true);
        }
    }
}