using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerManager : CharacterManager
    {
        Animator anim;
        public UIManager uiManager;
        public CameraHandler cameraHandler;
        public PlayerStatsManager playerStateManager;
        public PlayerEffectsManager playerEffectsManager;
        public PlayerLocomotionManager playerLocomotion;
        public PlayerCombatManager playerCombatManager;
        public PlayerAnimatorManager playerAnimatorManager;
        public PlayerInventoryManager playerInventoryManager;
        public PlayerEquipmentManager playerEquipmentManager;
        public PlayerWeaponSlotManger playerWeaponSlotManger;
        public InputHandler inputHandler;

        InteractUI interactUI;
        [Header("检测射线发射位置:")]
        public Transform RayCastStartPoint;
        [Header("UI:")]
        public GameObject interactableObj;          //与场景物体交互的提示UI
        public GameObject itemInteractableObj;   //拾取物品信息UI
        [Header("正在创建角色:")]
        public bool isCreatingCharacter = false;
        [Header("正在退出游戏窗口:")]
        public bool isExitGameWindow = false;
        public Transform headPosition;
        [Header("正在篝火旁休息:")]
        public bool isResting = false;
        public int previousBonfireIndex;           //上一个篝火索引位置
        [Header("正在交易:")]
        public bool isTrading = false;
        [Header("重生位置:")]
        public Vector3 rebirthPosition = new Vector3();

        [Header("元素瓶个数:")]
        public int totalNumberElementBottle = 5;    //元素瓶总数(含灰瓶一起)
        public int numberElement = 5;                     //元素瓶个数(不含灰瓶)
        [Header("元素瓶强化:")]
        public int restoreHealthLevel;

        protected override void Awake()
        {
            base.Awake();

            anim =GetComponent<Animator>();
            uiManager = FindObjectOfType<UIManager>();
            cameraHandler = FindObjectOfType<CameraHandler>();

            inputHandler = GetComponent<InputHandler>();
            interactUI = FindObjectOfType<InteractUI>();
            playerStateManager = GetComponent<PlayerStatsManager>();
            playerEffectsManager = GetComponent<PlayerEffectsManager>();
            playerLocomotion = GetComponent<PlayerLocomotionManager>();
            playerCombatManager = GetComponent<PlayerCombatManager>();
            playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
            playerInventoryManager = GetComponent<PlayerInventoryManager>();
            playerWeaponSlotManger = GetComponent<PlayerWeaponSlotManger>();
            playerEquipmentManager = GetComponent<PlayerEquipmentManager>();

            if(isCreatingCharacter == false)
            {
                if(PlayerRegenerationData.playerName.Equals("") != true && PlayerRegenerationData.playerName != null)
                {
                    playerStateManager.characterName = PlayerRegenerationData.playerName;
                }

                //读档:
                PlayerSaveManager.LoadDataToPlayer(this);
            }
            else
            {
                //玩家等级:
                playerStateManager.characterLeve = 0;

                //玩家裸身模型:
                DodyModelData dodyModelD = new DodyModelData();

                dodyModelD.isMale = true;                   //性别
                dodyModelD.headId = 0;                 //头部
                dodyModelD.hairstyle = -1;            //发型
                dodyModelD.facialHairId = -1;   //胡须
                dodyModelD.eyebrow = -1;           //眉毛

                playerEquipmentManager.bodyModelData = dodyModelD;
            }
        }
        private void Start()
        {
            if(interactableObj != null)
            {
                interactableObj.SetActive(false);
            }
            if(itemInteractableObj != null)
            {
                itemInteractableObj.SetActive(false);
            }

            //装备加载:
            //a.戒指:
            //
            //interactUI.gameObject.SetActive(false);
        }

        public void Update()
        {
            float delta = Time.deltaTime;

            isInteracting = anim.GetBool("isInteracting");               //是否正在交互
            canFire = anim.GetBool("canFire");
            canDoCombo = anim.GetBool("canDoCombo");           //是否可以连击
            isFiringSpell = anim.GetBool("isFiringSpell");
            isInvulnerab = anim.GetBool("isInvulnerab");                //是否刀枪不入
            
            anim.SetBool("isInAir", isInAir);
            anim.SetBool("isBlocking", isBlocking);
            anim.SetBool("isHoldingArrow", isHoldingArrow);
            anim.SetBool("isDead", playerStateManager.isDead);
            anim.SetBool("isTwoHandingWeapon", isTwoHandingWeapon);

            if (isDeath)
            {
                inputHandler.mouseX = 0;
                inputHandler.mouseY = 0;
                return;
            }


            #region inputHandler脚本方法调用:
            inputHandler.TickInput(delta);
            #endregion
            #region playerLocomotion脚本方法调用:
            playerLocomotion.HandleRollingAndSprinting(delta);
            playerLocomotion.HandleJumping();
            #endregion
            #region PlayerState脚本方法调用
            playerStateManager.RegenerateStamina();
            #endregion

            CheckForInteractaObject();
        }

        //物理帧更新:
        public void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;
            canRotate = anim.GetBool("canRotate");

            inputHandler.TickInput(delta);

            playerLocomotion.HandleMovement(delta);
            playerLocomotion.HandleRotation(delta);
            playerLocomotion.HandleFalling(delta, playerLocomotion.moveDirection);
            playerLocomotion.HandleClimbLadder();

            playerEffectsManager.HandleAllBuildupEffect();
        }

        //延时更新:
        private void LateUpdate()
        {
            #region 更新输入
            //管江华修改:
            isDeath = playerStateManager.isDead;
            //end管江华修改

            inputHandler.d_Pad_Up = false;
            inputHandler.d_Pad_Down = false;
            inputHandler.d_Pad_Right = false;
            inputHandler.d_Pad_Left = false;
            inputHandler.a_Input = false;
            inputHandler.inventory_Input = false;

            isSprinting = inputHandler.b_Input;

            //滞留空中时间记录
            if (isInAir)
            {
                playerLocomotion.inAirTimer += Time.deltaTime;  //如果是在空中，那么悬空时间开始累加;
            }
            #endregion
            //摄像机跟随:
            #region cameraHandler方法调用:
            float delta = Time.deltaTime;

            if (cameraHandler != null)
            {
                cameraHandler.FollowTarget();
                cameraHandler.HandleCameraRotation();
            }
            else
            {
                cameraHandler = CameraHandler.singleton;
            }
            #endregion
        }

        #region 玩家与游戏场景物品交互:
        //检测附近的可交互对象:(由于和使用消耗品道具是同一键位，故又是使用消耗品处理程序)
        public void CheckForInteractaObject()
        {
            RaycastHit hit;
            if(Physics.SphereCast(transform.position,0.3f,transform.forward,out hit, 0.4f, cameraHandler.ignoreLayer))
            {
                if(hit.collider.tag == "Interactab")
                {
                    Interactable instantiate = hit.collider.GetComponent<Interactable>();
                    if (instantiate != null)
                    {
                        string interactableText = instantiate.InteractableText;
                        interactUI.interactText.text = interactableText;
                        interactableObj.SetActive(true);

                        //关闭之前的物品信息ui
                        itemInteractableObj.SetActive(false);
                        //更新ui信息;
                        if (inputHandler.a_Input)
                        {
                            if (isInteracting) return;
                            hit.collider.GetComponent<Interactable>().Interact(this);   //调用交互对象的交互方法;

                            inputHandler.x_Input = false;
                            inputHandler.a_Input = false;
                        }
                    }
                }
            }
            else
            {
                if (interactableObj != null)
                {
                    interactableObj.SetActive(false);
                }

                if(itemInteractableObj != null && itemInteractableObj.activeSelf == true)
                {
                    Invoke("DisableItemInteractableObj", 3);        //如果物品信息是启用的，两秒后禁用;
                }

                //本人修改，使用道具按钮和交互按钮为同一个按钮，故在此调用使用道具方法的检测;
                //即在附近有交互物品情况下，优先交互，无交互物品则使用道具;
                if (isInteracting == false)
                {
                    inputHandler.HandleUseConsumableInput();
                }
            }
        }

        //开箱交互:
        public void OpenChestInteraction(Transform playerStandsHereWhenOpeningChest)
        {
            playerLocomotion.rigidbody.velocity = Vector3.zero;
            transform.position = playerStandsHereWhenOpeningChest.position;
            playerAnimatorManager.PlayTargetAnimation("Open_Chest", true);
        }

        //进入雾墙交互:
        public void PassThroughFogWallInteraction(Transform fogWallEntance)
        {
            //确保我们面对墙壁
            playerLocomotion.rigidbody.velocity = Vector3.zero;

            Vector3 rotationDir = fogWallEntance.transform.forward;
            Quaternion turnRotation = Quaternion.LookRotation(rotationDir);
            transform.rotation = turnRotation;
            //播放进入雾墙动画:
            playerAnimatorManager.PlayTargetAnimation("Pass_Through_Fog", true);
        }

        //禁用物体(物品UI):
        private void DisableItemInteractableObj()
        {
            itemInteractableObj.SetActive(false);
        }
        #endregion
    }
}