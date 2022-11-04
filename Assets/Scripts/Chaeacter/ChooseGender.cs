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
            //ж��װ��:(��������ģ��,������װ��ģ��:)
            player.playerEquipmentManager.UninstallEquipment();
        }
    }
}