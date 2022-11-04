using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class BombDamageCollider : DamageCollider
    {
        [Header("��ը�˺���뾶:")]
        public int explosiveRadius = 1;
        public int explosiveDamage;
        public int explosiveSplashDamage;
        [Header("��ը��Ч:")]
        public GameObject impactParticles;
        [Header("��ը��Ƶ:")]
        public AudioClip explosiveAudio;

        [Header("����:")]
        public Rigidbody bombRigidbody;
        bool hasCollided = false;   //�Ѿ���ײ��?

        protected override void Awake()
        {
            damageCollider = GetComponent<Collider>();
            bombRigidbody = GetComponent<Rigidbody>();
        }


        private void OnCollisionEnter(Collision collision)
        {
            if(hasCollided == false)
            {
                hasCollided = true;
                impactParticles = Instantiate(impactParticles, transform.position, Quaternion.identity);
                Explode();

                CharacterStatsManager character = collision.gameObject.GetComponent<CharacterStatsManager>();
                if(character != null)
                {
                    //�Ѿ����˴���:
                    if(character.teamIDNumber != this.teamIDNumber)
                    {
                        character.TakeDamage(0, explosiveDamage , 0 , 0 , 0);
                    }
                }

                if (explosiveAudio != null)
                {
                    AudioSource.PlayClipAtPoint(explosiveAudio, transform.position);
                }

                Destroy(impactParticles, 5);
                Destroy(gameObject, 5);
            }
        }

        private void Explode()
        {
            Collider[] characters = Physics.OverlapSphere(transform.position, explosiveRadius);

            foreach(Collider objectsInExplosion in characters)
            {
                CharacterStatsManager character = objectsInExplosion.GetComponent<CharacterStatsManager>();
                if (character != null)
                {
                    if (character.teamIDNumber != this.teamIDNumber)
                    {
                        character.TakeDamage(0, explosiveSplashDamage , 0 , 0 , 0);
                    }
                }
            }
        }
    }
}
