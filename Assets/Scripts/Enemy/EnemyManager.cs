using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SG
{
    public class EnemyManager : CharacterManager
    {
        [Header("���о�����Ƕ�:")]
        //������
        public float detectionRadius = 20;      //���뾶;
        public float detectionAngle = 100;       //���Ƕ�(�����ǰ���ļн�);

        public float currentRecoveryTime = 0;                                 //��ǰ����ʱ����;

        [Header("����״̬��:")]
        public State currentState;                                                   //��ǰ״̬
        public CharacterStatsManager currentTarget;                                  //Ŀ�����
        [Header("׷��Ŀ��״̬��:")]
        public NavMeshAgent navMeshAgent;                              //�����������
        public Rigidbody enemyRigidbody;                                    //����
        public float rotationSpeed = 15;                                        //��ת�ٶ�;
        [Header("��󹥻��뾶:")]
        public float maximumAggroRadius = 1.0f;                                  //ֹͣ����;
        public float minimumAggroRadius = 0.5f;
        [Header("������������������:")]
        public bool allowAIToPerformCombos;
        [Range(0,1f)] public float comboLikelyHood;

        [Header("������Ϊ�ж�:")]
        //public bool isDeath = false;                   
        public bool isPhaseShifting;                   //���ڻ��׶�;
        public bool isPreformingAction;            //����ִ����Ϊ;

        public EnemyLocomotionManager enemyLocomotionManager;
        public EnemyWeaponSlotManager enemyWeaponSlotManager;
        public EnemyInventoryManager enemyInventoryManager;
        public EnemyAnimatorManager enemyAnimatorManager;
        public EnemyEffectsManager enemyEffectsManager;
        public EnemyStatsManager enemyStats;
        public NpcManager npcManager;

        protected override void Awake()
        {
            base.Awake();

            navMeshAgent = GetComponentInChildren<NavMeshAgent>();
            enemyRigidbody = GetComponent<Rigidbody>();

            enemyStats = GetComponent<EnemyStatsManager>();
            enemyEffectsManager = GetComponent<EnemyEffectsManager>();
            enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
            enemyInventoryManager = GetComponent<EnemyInventoryManager>();
            enemyWeaponSlotManager = GetComponent<EnemyWeaponSlotManager>();
            enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
            npcManager = GetComponentInParent<NpcManager>();
        }

        private void Start()
        {
            navMeshAgent.enabled = false;
            enemyRigidbody.isKinematic = false;
        }

        private void Update()
        {
            HandleRecoveryTimer();
            HandleStateMachine();
            //isUsingLeftHand = enemyAnimatorManager.anim.GetBool("isUsingLeftHand");
            //isUsingRightHand = enemyAnimatorManager.anim.GetBool("isUsingRightHand");
            isRotatingWithRootMotion = enemyAnimatorManager.anim.GetBool("isRotatingWithRootMotion");
            canRotate = enemyAnimatorManager.anim.GetBool("canRotate");
            canDoCombo = enemyAnimatorManager.anim.GetBool("canDoCombo");           //�Ƿ��������
            isInteracting = enemyAnimatorManager.anim.GetBool("isInteracting");
            isInvulnerab = enemyAnimatorManager.anim.GetBool("isInvulnerab");                //�Ƿ�ǹ����
            isPhaseShifting = enemyAnimatorManager.anim.GetBool("isPhaseShifting");

            enemyAnimatorManager.anim.SetBool("isTwoHandingWeapon", isTwoHandingWeapon);
            enemyAnimatorManager.anim.SetBool("isDead", enemyStats.isDead);
        }

        private void LateUpdate()
        {
            navMeshAgent.transform.localPosition = Vector3.zero;
            navMeshAgent.transform.localRotation = Quaternion.identity;

            if(enemyStats.isDead && isInteracting == false)
            {
                enemyAnimatorManager.PlayTargetAnimation("Death_01");
            }
        }

        private void FixedUpdate()
        {
            enemyEffectsManager.HandleAllBuildupEffect();
        }

        //״̬���������:
        private void HandleStateMachine()
        {
            if (enemyStats.isDead == true) return;

            if (currentState != null)
            {
                State nextState = currentState.Tick(this, enemyStats, enemyAnimatorManager);

                if (nextState != null)
                {
                    SwitchToNextState(nextState);
                }
            }
        }

        //�л�����һ��״̬:
        private void SwitchToNextState(State state)
        {
            currentState = state;
        }

        //���������ʱ:
        private void HandleRecoveryTimer()
        {
            if(currentRecoveryTime > 0)
            {
                currentRecoveryTime -= Time.deltaTime;
            }

            if(isPreformingAction && currentRecoveryTime <= 0)
            {
                isPreformingAction = false;
            }
        }
    }
}