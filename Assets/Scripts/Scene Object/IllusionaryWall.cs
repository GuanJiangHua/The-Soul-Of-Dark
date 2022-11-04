using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class IllusionaryWall : MonoBehaviour
    {
        public bool wallHasBeenHit;                     //ǽ�ѱ�����:
        public Material illusionaryWallMaterial;   //����ǽ�ڲ���;
        public float alpha;
        public float fadeTimer = 2.5f;                  //���뵭������;
        public BoxCollider wallCollider;               //ǽ��ײ��;

        public AudioSource audioSource;           //��ƵԴ;
        public AudioClip illusionaryWallSound;  //����ǽ������;

        [Header("�Ѿ�����:")]
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
        //���뵭������ǽ��:
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
        //����ǽ�ڳ�ʼ��:
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
