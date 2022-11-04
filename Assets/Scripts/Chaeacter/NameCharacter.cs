using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class NameCharacter : MonoBehaviour
    {
        public CharacterStatsManager character;
        public InputField inputField;
        public Text nameButtonText;

        public void NameMyCharacter()
        {
            string myName = inputField.text;
            if (myName.Equals("") || myName == null)
            {
                character.characterName = "DefaultName";
            }
            else
            {
                character.characterName = myName;
            }

            nameButtonText.text = character.characterName;
        }
    }
}
