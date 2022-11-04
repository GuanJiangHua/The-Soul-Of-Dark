using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class SavePosition : MonoBehaviour
    {
        public PlayerManager player;

        public SelectHairColor hairColor;
        public SelectSkinColor skinColor;
        public SelectFacialMarkColor facialMarkColor;
        public void Awake()
        {
            hairColor.GetCurrentColor();
            skinColor.GetCurrentColor();
            facialMarkColor.GetCurrentColor();
        }
        public void Save()
        {
            DodyModelData dodyModelData = player.playerEquipmentManager.bodyModelData;
            dodyModelData.hairColor = new Color(hairColor.redAmount, hairColor.greenAmount, hairColor.blueAmount);
            dodyModelData.skinColor = new Color(skinColor.redAmount, skinColor.greenAmount, skinColor.blueAmount);
            dodyModelData.facialMarkColor = new Color(facialMarkColor.redAmount, facialMarkColor.greenAmount, facialMarkColor.blueAmount);

            PlayerSaveManager.SaveToFile(player,null);
            string characterName = player.playerStateManager.characterName;
            if(characterName == null || characterName == "")
            {
                characterName = "DefaultName";
            }
            PlayerRegenerationData.playerName = characterName;
        }
    }
}