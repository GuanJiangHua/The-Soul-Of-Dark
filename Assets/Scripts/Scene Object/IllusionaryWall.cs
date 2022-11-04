using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class IllusionaryWall : MonoBehaviour
    {
        public bool wallHasBeenHit;                     //Ç½ÒÑ±»»÷ÖÐ:
        public Material illusionaryWallMaterial;   //Òþ²ØÇ½±Ú²ÄÖÊ;
        public float alpha;
        public float fadeTimer = 2.5f;                  //µ­Èëµ­³ö¼ÆËã;
        public BoxCollider wallCollider;               //Ç½Åö×²Æ÷;

        public AudioSource audioSource;           //ÒôÆµÔ´;
        public AudioClip illusionaryWallSound;  //Òþ²ØÇ½±ÚÉùÒô;

        [Header("ÒÑ¾­¿ªÆô:")]
        public bool isAlreadyTurnedOn = false;
        private void Awake()
        {
            illusionaryWallMaterial = new Material(illusionaryWallMaterial);
            GetComponent<MeshRenderer>().material = illusionaryWallMaterial;
        }
        private void Update()
        {
            if (wallHasBeenHit && isAlreadyTurnedOn == false)
            {
                FadeIllusionaryWall();
            }
        }
        //µ­Èëµ­³öÒþ²ØÇ½±Ú:
        private void FadeIllusionaryWall()
        {
            alpha = illusionaryWallMaterial.color.a;
            alpha = alpha - Time.deltaTime / fadeTimer;
            Color fadeWallColor = new Color(illusionaryWallMaterial.color.r, illusionaryWallMaterial.color.g, illusionaryWallMaterial.color.b, alpha);
            illusionaryWallMaterial.color = fadeWallColor;

            if (wallCollider.enabled)
            {
                wallCollider.enabled = false;
                audioSource.PlayOneShot(illusionaryWallSound);
            }

            if (alpha <= 0)
            {
                isAlreadyTurnedOn = true;
            }
        }
        //Òþ²ØÇ½±Ú³õÊ¼»¯:
        public void IllusionaryWallInitialization(bool isOpen)
        {
            if (isOpen)
            {
                illusionaryWallMaterial.color = new Color(1, 1, 1, 0);
                wallCollider.enabled = false;

                isAlreadyTurnedOn = true;
            }
            else
            {
                illusionaryWallMaterial.color = new Color(1, 1, 1, 1);
                wallCollider.enabled = true;

                isAlreadyTurnedOn = false;
            }
        }
    }
}
