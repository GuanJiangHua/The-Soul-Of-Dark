using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SG
{
    public class EnemyManager : CharacterManager
    {
        [Header("索敌距离与角度:")]
        //处理检测
        public float detectionRadius = 20;      //检测半径;
        public float detectionAngle = 100;       //检测角度(与对象前方的夹角);

        public float currentRecoveryTime = 0;                                 //当前攻击时间间隔;

        [Header("闲置状态机:")]
        public State currentState;                                                   //当前状态
        public CharacterStatsManager currentTarget;                                  //目标对象
        [Header("追逐目标状态机:")]
        public NavMeshAgent navMeshAgent;                              //导航网格代理
        public Rigidbody enemyRigidbody;                                    //刚体
        public float rotationSpeed = 15;                                        //旋转速度;
        [Header("最大攻击半径:")]
        public float maximumAggroRadius = 1.0f;                                  //停止距离;
        public float minimumAggroRadius = 0.5f;
        [Header("允许连击与连击几率:")]
        public bool allowAIToPerformCombos;
        [Range(0,1f)] public float comboLikelyHood;

        [Header("动作行为判断:")]
        //public bool isDeath = false;                   
        public bool isPhaseShifting;                   //正在换阶段;
        public bool isPreformingAction;            //正在执行行为;

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
            canDoCombo = enemyAnimatorManager.anim.GetBool("canDoCombo");           //是否可以连击
            isInteracting = enemyAnimatorManager.anim.GetBool("isInteracting");
            isInvulnerab = enemyAnimatorManager.anim.GetBool("isInvulnerab");                //是否刀枪不入
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

        //状态机处理程序:
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

        //切换到下一个状态:
        private void SwitchToNextState(State state)
        {
            currentState = state;
        }

        //攻击间隔计时:
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