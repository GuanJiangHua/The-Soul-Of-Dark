using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "���̵괰��ѡ��", menuName = "��Ϸ�Ի�ϵͳģ��/�½��¶Ի�ѡ��/���̵괰��ѡ��")]
    public class OpenStoreWindow : OptionMethodClass
    {
        public override void OptionMethod(PlayerManager player, NpcManager npc)
        {
            //���̵괰��:
            player.uiManager.EnableStoreWindow(npc);
        }
    }
}