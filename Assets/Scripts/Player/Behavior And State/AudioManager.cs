using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [System.Serializable]
    public struct AudioAndName
    {
        public string audioName;
        public AudioClip audio;
    }
    public class AudioManager : MonoBehaviour
    {
        public List<AudioAndName> audioClips = new List<AudioAndName>();

        public void PlayAudioByName(string name)
        {
            AudioClip targetAudio = null;
            for(int i = 0; i < audioClips.Count; i++)
            {
                if (audioClips[i].audioName.Equals(name))
                {
                    targetAudio = audioClips[i].audio;
                }
            }

            if (targetAudio != null)
            {
                AudioSource.PlayClipAtPoint(targetAudio, transform.position);
            }
        }
    }
}