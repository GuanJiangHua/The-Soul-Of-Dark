using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class InputHandler : MonoBehaviour
    {
        public float horizontal;    //-1,0,1
        public float vertical;         //-1,0,1
        public float moveAmount;    //移动量
        public float mouseX;
        public float mouseY;

        public bool a_Input;
        public bool b_Input;
        public bool x_Input;                           //使用道具（血瓶,蓝瓶等）
        public bool y_Input;

        public bool tap_rt_Input;
        public bool tap_lt_Input;

        public bool tap_rb_Input;                        //轻攻击键输入
        public bool hold_rb_Input;                      //按住轻攻击键(按住)

        public bool lb_Input;                             //防御键[F]输入;
        public bool tap_lb_Input;                      //单击LB键[F]输入;

        public bool jump_Input;
        public bool inventory_Input;
        public bool lockOn_Input;
        public bool sneakMove_Input;            //潜行移动;
        public bool right_Stick_Left_Input;     //切换锁定目标[向左]-Q
        public bool right_Stick_Right_Input;  //切换锁定目标[向右]-E

        public bool d_Pad_Up;                       //上方向键
        public bool d_Pad_Down;                  //下方向键
        public bool d_Pad_Right;                   //左方向键
        public bool d_Pad_Left;                     //右方向键

        public bool rollFlag;                //翻滚标记;
        public bool fireFlag;                //开火标记;
        public bool twoHandFlag;      //双手持握标记;
        public bool sprintFlag;            //冲刺标记;
        public bool sneakMoveFlag;   //潜行标记;
        public bool lockOnFlag;          //锁定标记;
        public bool comboFlag;          //连击标记;
        public bool inventoryFlag;      //背包菜单标记;
        public float rollInputTimer;     //翻滚按钮输入计时;

        public Transform criricalAttackRaycastStartPoint;   //背刺或反击检测射线发射点;

        PlayerControls inputActions;
        PlayerCombatManager playerCombatManager;
        CameraHandler cameraHandler;
        PlayerWeaponSlotManger weaponSlotManger;
        PlayerAnimatorManager animatorHandler;
        BlockingCollider blockingCollider;
        PlayerEffectsManager playerEffectsManager;
        PlayerInventoryManager playerInventoryManager;
        PlayerManager playerManger;
        PlayerStatsManager playerStatsManager;
        public UIManager uiManager;

        Vector2 movementInput;
        Vector2 cameraInput;

        public void Awake()
        {
            playerManger = GetComponent<PlayerManager>();
            playerStatsManager = GetComponent<PlayerStatsManager>();
            playerCombatManager = GetComponent<PlayerCombatManager>();
            playerEffectsManager = GetComponentInChildren<PlayerEffectsManager>();
            playerInventoryManager = GetComponent<PlayerInventoryManager>();
            blockingCollider = GetComponentInChildren<BlockingCollider>();
            weaponSlotManger = GetComponent<PlayerWeaponSlotManger>();
            animatorHandler = GetComponent<PlayerAnimatorManager>();

            cameraHandler = FindObjectOfType<CameraHandler>();
            uiManager = FindObjectOfType<UIManager>();
        }

        //当启用:
        public void OnEnable()
        {
            if(inputActions == null)
            {
                inputActions = new PlayerControls();
                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();    //匿名函数: 如->函数(PlayerControls inputActions){ movementInput = inputActions.ReadValue<Vector2>();}
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

                inputActions.PlayerActions.Y.performed += i => y_Input = true;
                inputActions.PlayerActions.RB.performed += i => tap_rb_Input = true;    //轻攻击输入事件检测
                inputActions.PlayerActions.HoldRB.performed += i => hold_rb_Input = true;   //轻攻击键按住
                inputActions.PlayerActions.HoldRB.canceled += i => hold_rb_Input = false;     //轻攻击键抬起
                inputActions.PlayerActions.RT.performed += i => tap_rt_Input = true;     //重攻击输入事件检测
                inputActions.PlayerActions.LT.performed += i => tap_lt_Input = true;      //战技输入事件检测(算入攻击输入)

                inputActions.PlayerActions.LB.performed += i => lb_Input = true;     //LB键[F]输入事件检测(按下时)
                inputActions.PlayerActions.LB.canceled += i => lb_Input = false;       //LB键[F]输入事件检测(抬起时)
                inputActions.PlayerActions.TapLB.performed += i => tap_lb_Input = true;     //LB键[F]单击

                inputActions.PlayerQuickSlots.DPadUp.performed += i => d_Pad_Up = true;          //切换当前法术物品输入
                inputActions.PlayerQuickSlots.DPadDown.performed += i => d_Pad_Down = true; //切换消耗品装备
                inputActions.PlayerQuickSlots.DPadRight.performed += i => d_Pad_Right = true;   //切换右手武器输入事件检测
                inputActions.PlayerQuickSlots.DPadLeft.performed += i => d_Pad_Left = true;        //切换左手武器输入事件检测

                inputActions.PlayerActions.A.performed += i => a_Input = true;                               //交互按键输入检测
                inputActions.PlayerActions.Roll.performed += i => b_Input = true;                           //按下翻滚按钮
                inputActions.PlayerActions.Roll.canceled += i => b_Input = false;                             //松开翻滚按钮 
                inputActions.PlayerActions.X.performed += i => x_Input = true;                               //使用道具按键的输入检测()
                inputActions.PlayerActions.Jump.performed += i => jump_Input = true;                  //跳跃按键输入检测
                inputActions.PlayerActions.Inventory.performed += i => inventory_Input = true;    //打开背包按键输入检测
                inputActions.PlayerActions.LockOn.performed += i => lockOn_Input = true;           //锁定敌人按键输入检测
                inputActions.PlayerMovement.LockOnTargetLeft.performed += i => right_Stick_Left_Input = true;           //向左切换锁定目标;
                inputActions.PlayerMovement.LockOnTargetRight.performed += i => right_Stick_Right_Input = true;      //向右切换锁定目标;

                //修改:
                inputActions.PlayerMovement.SneakMoveButton.performed += i => sneakMove_Input = true;
                inputActions.PlayerMovement.SneakMoveButton.canceled += i => sneakMove_Input = false;
                inputActions.PlayerActions.LB.canceled += i => sneakMove_Input = false;       //举盾输入事件检测(抬起时)
            }

            inputActions.Enable();
        }
        //当销毁:
        private void OnDisable()
        {
            inputActions.Dispose();
        }

        public void TickInput(float delta)
        {
            if (playerManger.isDeath) return;

            HandleRollInput(delta);
            
            HandleSneakMoveInput();
            HandleMoveInput();

            if(inventoryFlag != true && playerManger.isResting != true && playerManger.isExitGameWindow != true && playerManger.isClimbLadder != true)
            {
                HandleTapRBInput();
                HandleHoldRBInput();

                HandleTapLBInput();
                HandleHoldLBInput();

                HandleTapRTInput();
                HandleTapLTInput();
            }

            HandleQuickSlotsInput();
            HandleTwoHandInput();
            HandleInventoryInput();
            HandleLockOnInput();
        }

        //检测移动输入:
        private void HandleMoveInput()
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;

            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

            if(sneakMoveFlag == true || playerManger.isHoldingArrow)
            {
                vertical /= 2.0f;
                moveAmount /= 2.0f;
            }

            mouseX = cameraInput.x;
            mouseY = cameraInput.y;

        }
    
        //检测翻滚输入:
        private void HandleRollInput(float delte)
        {
            if (playerManger.isClimbLadder && b_Input)
            {
                playerManger.isClimbLadder = false;
                playerManger.playerAnimatorManager.anim.SetBool("isClimbLadder", false);
                playerManger.playerAnimatorManager.PlayTargetAnimation("Falling", true);
            }
            //玩家动作的Player Actions的Roll按键，是否为开始阶段(被按下)

            if (b_Input)
            {
                rollInputTimer += delte;
                if(playerStatsManager.currentStamina <= 1)
                {
                    b_Input = false;
                    sprintFlag = false;
                }

                if(moveAmount >= 0.5f && playerStatsManager.currentStamina >= 1)
                {
                    sprintFlag = sprintFlag = true;
                    sneakMove_Input = false;
                }
            }
            else
            {
                sprintFlag = false;
                if(rollInputTimer > 0 && rollInputTimer < 0.5f)
                {
                    rollFlag = true;
                }

                rollInputTimer = 0.0f;
            }

            if(playerManger.isInteracting == true)
            {
                rollFlag = false;
            }
        }
    
        //检测轻攻击输入:
        public void HandleTapRBInput()
        {
            if (tap_rb_Input)
            {
                tap_rb_Input = false;

                WeaponItem rightWeapon = playerInventoryManager.rightWeapon;
                if (rightWeapon.tap_RB_Action != null)
                {
                    playerManger.UpdateWhichHandCharacterIsUsing(false);
                    playerInventoryManager.currentItemBeingUsed = rightWeapon;
                    rightWeapon.tap_RB_Action.PerformAction(playerManger);
                }

            }
        }

        //按住左键输入:(持续按住RB键"左键")
        private void HandleHoldRBInput()
        {
            //当持续按下轻攻击键时:("鼠标左键")
            if (hold_rb_Input)
            {
                WeaponItem rightWeapon = playerInventoryManager.rightWeapon;
                if(rightWeapon != null && rightWeapon.hold_RB_Action != null)
                {
                    playerManger.UpdateWhichHandCharacterIsUsing(false);
                    playerInventoryManager.currentItemBeingUsed = rightWeapon;
                    rightWeapon.hold_RB_Action.PerformAction(playerManger);
                }
            }
        }
        
        //重攻击输入:
        private void HandleTapRTInput()
        {
            if (tap_rt_Input)
            {
                tap_rt_Input = false;
                if (playerManger.isHoldingArrow)
                {
                    if (playerManger.isHoldingArrow)
                    {
                        Destroy(playerEffectsManager.currentRangeFX);

                        Animator bowAnimator = playerManger.playerWeaponSlotManger.rightHandSlot.GetComponentInChildren<Animator>();
                        bowAnimator.SetBool("isDrawn", false);
                        playerManger.isHoldingArrow = false;
                    }
                }
                else
                {
                    //重攻击检测;
                    WeaponItem rightWeapon = playerInventoryManager.rightWeapon;

                    if(rightWeapon!=null && rightWeapon.tap_RT_Action != null)
                    {
                        playerManger.UpdateWhichHandCharacterIsUsing(false);
                        playerInventoryManager.currentItemBeingUsed = playerInventoryManager.rightWeapon;
                        playerInventoryManager.rightWeapon.tap_RT_Action.PerformAction(playerManger);
                    }
                }
            }
        }

        //战技[Shift键单击]输入:
        private void HandleTapLTInput()
        {
            if (tap_lt_Input)
            {
                tap_lt_Input = false;
                WeaponItem rightWeapon = playerInventoryManager.rightWeapon;
                WeaponItem leftWeapon = playerInventoryManager.leftWeapon;
                //如果是双手持握:(则使用右手战技)
                if(twoHandFlag == true)
                {
                    if(rightWeapon!=null && rightWeapon.tap_LT_Action)
                    {
                        playerManger.UpdateWhichHandCharacterIsUsing(false);
                        playerInventoryManager.currentItemBeingUsed = playerInventoryManager.rightWeapon;
                        playerInventoryManager.rightWeapon.tap_LT_Action.PerformAction(playerManger);
                    }
                }
                //左手武器非空,非空手(拳头)
                else if(leftWeapon != null && leftWeapon.weaponType != WeaponType.Unarmed)
                {
                    if (leftWeapon.tap_LT_Action !=null)
                    {
                        playerManger.UpdateWhichHandCharacterIsUsing(true);
                        playerInventoryManager.currentItemBeingUsed = leftWeapon;
                        leftWeapon.tap_LT_Action.PerformAction(playerManger);
                    }
                }
                else
                {
                    if(rightWeapon!=null && rightWeapon.tap_LT_Action!=null)
                    {
                        playerManger.UpdateWhichHandCharacterIsUsing(false);
                        playerInventoryManager.currentItemBeingUsed = rightWeapon;
                        rightWeapon.tap_LT_Action.PerformAction(playerManger);
                    }
                }
            }
        }

        //单击LB键[F]：(单击f为左手施法[仅当非双持且左手武器非空])
        private void HandleTapLBInput()
        {
            if (tap_lb_Input)
            {
                tap_lb_Input = false;

                WeaponItem rightWeapon = playerInventoryManager.rightWeapon;
                WeaponItem leftWeapon = playerInventoryManager.leftWeapon;

                if (playerManger.isTwoHandingWeapon)
                {
                    if(rightWeapon != null && rightWeapon.tap_LB_Action != null)
                    {
                        playerManger.UpdateWhichHandCharacterIsUsing(false);     //单击LB键[F]
                        playerInventoryManager.currentItemBeingUsed = rightWeapon;
                        rightWeapon.tap_LB_Action.PerformAction(playerManger);
                    }
                }
                else
                {
                    if (leftWeapon != null && leftWeapon.tap_LB_Action != null)
                    {
                        playerManger.UpdateWhichHandCharacterIsUsing(true);     //单击LB键[F]
                        playerInventoryManager.currentItemBeingUsed = leftWeapon;
                        leftWeapon.tap_LB_Action.PerformAction(playerManger);
                    }
                } 

            }
        }

        //防御,举盾,瞄准键:(LB [F] 键按住)
        private void HandleHoldLBInput()
        {
            if (playerManger.isInAir || playerManger.isInteracting || playerManger.isFiringSpell)
            {
                return;
            }

            if (lb_Input)
            {
                //举盾状态的布尔值的赋值在此方法中;
                sneakMove_Input = true;
                WeaponItem rightWeapon = playerInventoryManager.rightWeapon;
                WeaponItem leftWeapon = playerInventoryManager.leftWeapon;

                if (playerManger.isTwoHandingWeapon && rightWeapon != null)
                {
                    playerManger.UpdateWhichHandCharacterIsUsing(false);
                    playerInventoryManager.currentItemBeingUsed = rightWeapon;
                    if (rightWeapon.hold_LB_Action != null)
                        rightWeapon.hold_LB_Action.PerformAction(playerManger);
                }
                //左手是空
                else if(leftWeapon!=null && leftWeapon.isUnarmed == false)
                {
                    if(leftWeapon.hold_LB_Action != null)
                    {
                        playerManger.UpdateWhichHandCharacterIsUsing(true);
                        playerInventoryManager.currentItemBeingUsed = leftWeapon;
                        leftWeapon.hold_LB_Action.PerformAction(playerManger);
                    }
                }
                else
                {
                    if (rightWeapon.hold_LB_Action != null)
                    {
                        playerManger.UpdateWhichHandCharacterIsUsing(false);
                        playerInventoryManager.currentItemBeingUsed = rightWeapon;
                        rightWeapon.hold_LB_Action.PerformAction(playerManger);
                    }
                }
            }
            else
            {
                if (playerManger.isAiming)
                {
                    playerManger.isAiming = false;
                    uiManager.crossHair.SetActive(false);
                }

                if (blockingCollider.blockingCollider.enabled)
                {
                    playerManger.isBlocking = false;
                    blockingCollider.DisableBlockingCollider(); //禁用盾牌的碰撞器(阻止飞行道具)
                }
            }
        }
    
        //切换武器输入:
        public void HandleQuickSlotsInput()
        {
            if (d_Pad_Right)
            {
                d_Pad_Right = false;
                playerInventoryManager.ChangeRightWeapon();
            }
            if (d_Pad_Left)
            {
                d_Pad_Left = false;
                playerInventoryManager.ChangeLeftWeapon();
            }
            if (d_Pad_Down)
            {
                d_Pad_Down = false;
                playerInventoryManager.ChangeConsumable();
            }
            if (d_Pad_Up)
            {
                d_Pad_Up = false;
                playerInventoryManager.ChangeSpell();
            }
        }

        //背包菜单输入:
        private void HandleInventoryInput()
        {
            if (inventory_Input)
            {
                //如果升级窗口处于打开状态:
                if(uiManager.playerLeveUpWindow.activeInHierarchy == true)
                {
                    uiManager.playerLeveUpWindow.GetComponent<LevelUpUI>().CloseInputEventMethod();
                    return;
                }
                //如果在退出游戏窗口打开状态:
                if (playerManger.isExitGameWindow)
                {
                    //禁用渲染头像的摄像机:
                    uiManager.exitGameWindowManager.CloseExitGameWindow();
                    return;
                }
                //如果在交易窗口打开状态:
                if(playerManger.isTrading == true)
                {
                    //打开结束交易窗口:
                    uiManager.commodityTradingWindow.EnableEndTransactionWindow();
                    return;
                }

                //如果坐在篝火旁休息,不能打开背包:
                if (playerManger.isResting)
                {
                    playerManger.uiManager.campfireUIWindowManager.CloseTransferFunctionWindow();          //禁用传送窗口;
                    playerManger.uiManager.campfireUIWindowManager.CloseMemorySpellWindow();                //禁用记忆法术窗口;
                    playerManger.uiManager.campfireUIWindowManager.CloseAssignElementBottleWindow();    //禁用分配元素瓶窗口;
                    playerManger.uiManager.campfireUIWindowManager.CloseFortifiedElementBottleWindow();//禁用强化元素瓶窗口;
                    playerManger.uiManager.EnableCampfireUIWindow();                                                               //启用篝火功能窗口;
                    return;
                }

                //背包窗口:
                inventoryFlag = !inventoryFlag;
                if (inventoryFlag)
                {
                    uiManager.OpenSelectWindow();
                    uiManager.UpdateUI();
                    uiManager.hudWindow.SetActive(false);
                }
                else
                {
                    uiManager.CloseSelectWindow();          //禁用菜单窗口
                    uiManager.CloseAllLeftWindow();         //禁用左侧所有窗口
                    uiManager.CloseAllCentralWindow();   //禁用中间所有窗口
                    uiManager.CloseAllRightWindow();      //禁用右侧所有窗口
                    uiManager.hudWindow.SetActive(true);    //启用简略属性

                    tap_rb_Input = false;
                    hold_rb_Input = false;
                    tap_rt_Input = false;
                    tap_lt_Input = false;
                }
            }
        }
        
        //锁定目标输入:
        private void HandleLockOnInput()
        {
            //锁定按钮按下 && 不在锁定敌人状态:
            if(lockOn_Input && lockOnFlag == false)
            {
                lockOn_Input = false;
                cameraHandler.HandleLockOn();
                if(cameraHandler.nearestLockOnTarger != null)
                {
                    lockOnFlag = true;
                    cameraHandler.cameraPivotTransform.localRotation = Quaternion.Euler(2.3f , 0, 0);
                    cameraHandler.currentLockOnTarger = cameraHandler.nearestLockOnTarger;
                }
            }//锁定按钮按下 && 在锁定敌人状态:
            else if(lockOn_Input && lockOnFlag == true)
            {
                lockOnFlag = false;
                lockOn_Input = false;
                cameraHandler.ClearLockOnTargets();
            }

            //按下向左切换锁定目标键:(快速跳过对话:)
            if (right_Stick_Right_Input)
            {
                uiManager.dialogSystemWindowManager.SkipDialogue();
                //没有锁定:
                if(lockOnFlag != true)
                {
                    right_Stick_Right_Input = false;
                }
            }

            //按下向左切换锁定目标键：
            if (lockOnFlag && right_Stick_Left_Input)
            {
                right_Stick_Left_Input = false;
                cameraHandler.HandleLockOn();
                if (cameraHandler.leftLockTarget != null)
                {
                    cameraHandler.currentLockOnTarger = cameraHandler.leftLockTarget;
                }
            }
            //按下向右切换锁定目标键：
            if(lockOnFlag && right_Stick_Right_Input)
            {
                right_Stick_Right_Input = false;
                cameraHandler.HandleLockOn();
                if (cameraHandler.rightLockTarget != null)
                {
                    cameraHandler.currentLockOnTarger = cameraHandler.rightLockTarget;
                }
            }
        }

        //双手持握输入:
        private void HandleTwoHandInput()
        {
            if (y_Input)
            {
                y_Input = false;
                twoHandFlag = !twoHandFlag;
                if (twoHandFlag)
                {
                    playerManger.isTwoHandingWeapon = true;
                    weaponSlotManger.LoadWeaponHolderOfSlot(playerInventoryManager.rightWeapon, false);
                    weaponSlotManger.LoadWeaponHolderOfSlot(playerInventoryManager.unarmedWeapon, true);
                }
                else
                {
                    playerManger.isTwoHandingWeapon = false;
                    weaponSlotManger.LoadWeaponHolderOfSlot(playerInventoryManager.rightWeapon, false);
                    weaponSlotManger.LoadWeaponHolderOfSlot(playerInventoryManager.leftWeapon, true);

                }
            }
        }

        //潜行输入:
        private void HandleSneakMoveInput()
        {
            if(sneakMove_Input == true)
            {
                sneakMoveFlag = true;
            }
            else
            {
                sneakMoveFlag = false;
            }
        }

        //处理使用道具事件:(本人修改，由于和交互按钮同一个键位，故在交互检测中调用)
        public void HandleUseConsumableInput()
        {
            if(x_Input == true)
            {
                x_Input = false;
                //使用当前消耗品：
                if (playerInventoryManager.currentConsumable != null)
                {
                    Debug.Log(playerInventoryManager.currentConsumable.GetType());
                    playerInventoryManager.currentConsumable.AttemptToConsumeItem(animatorHandler, weaponSlotManger, playerEffectsManager);
                }
            }
        }
    }
}

