using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class DamageDetectionCollition : MonoBehaviour
    {
        public bool ishead;
        public Collider myCollider;
        public CharacterStatsManager characterStatsManager;
        private void Awake()
        {
            myCollider = GetComponent<Collider>();
        }
    }
}