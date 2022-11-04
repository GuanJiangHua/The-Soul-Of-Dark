using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class ChooseGender : MonoBehaviour
    {
        public DodyModelData dodyModelData;
        public PlayerManager player;

        private void Start()
        {
            dodyModelData = player.playerEquipmentManager.bodyModelData;  
        }
        public void UpdateGender(bool isMale)
        {
            dodyModelData.isMale = isMale;
            //卸载装备:(加载人体模型,不加载装备模型:)
            player.playerEquipmentManager.UninstallEquipment();
        }
    }
}