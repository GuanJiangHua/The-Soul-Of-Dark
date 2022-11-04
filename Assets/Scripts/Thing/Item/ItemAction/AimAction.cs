using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "��׼����", menuName = "��Ϸ��Ʒ/�½���������/(F����ס)��׼����")]
    public class AimAction : ItemAction
    {
        public override void PerformAction(PlayerManager player)
        {
            if (player.isAiming)
                return;

            player.inputHandler.uiManager.crossHair.SetActive(true);
            player.isAiming = true;
        }
    }
}