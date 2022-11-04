using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class EnemyLocomotionManager : MonoBehaviour
    {
        EnemyManager enemyManager;
        EnemyAnimatorManager enemyAnimatorManager;

        Rigidbody enemyRigidbody;

        public float moveSpeed = 3;                   //�ƶ��ٶ�;

        [Header("ս���е���ײ��:")]
        public CapsuleCollider characterCollider;
        public CapsuleCollider characterCollidionBlockerCollider;
        public Collider backStabCollider;
        public Collider blockingCollider;
        private void Awake()
        {
            enemyManager = GetComponent<EnemyManager>();
            enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
            enemyRigidbody = GetComponent<Rigidbody>();

            characterCollider = GetComponent<CapsuleCollider>();
        }

        private void Start()
        {
            Physics.IgnoreCollision(characterCollider, characterCollidionBlockerCollider, true);
        }
    }
}