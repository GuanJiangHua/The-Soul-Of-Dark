using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PoisonSurace : MonoBehaviour
    {
        public float poisonBuildupAmount = 7;

        public List<CharacterEffectsManager> charactersInPoisonSurace;    //¶¾ÒºÖÐµÄ½ÇÉ«

        private void OnTriggerEnter(Collider other)
        {
            CharacterEffectsManager character = other.GetComponent<CharacterEffectsManager>();
            if (character != null)
            {
                charactersInPoisonSurace.Add(character);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            foreach(CharacterEffectsManager character in charactersInPoisonSurace)
            {
                if (character.isPoisoned == false) 
                { 
                    character.poisonBuildup += poisonBuildupAmount * Time.deltaTime;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            CharacterEffectsManager character = other.GetComponent<CharacterEffectsManager>();
            if (character != null)
            {
                charactersInPoisonSurace.Remove(character);
            }
        }
    }
}
