using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class InputHandler : MonoBehaviour
    {
        public float horizontal;    //-1,0,1
        public float vertical;         //-1,0,1
        public float moveAmount;    //�ƶ���
        public float mouseX;
        public float mouseY;

        public bool a_Input;
        public bool b_Input;
        public bool x_Input;                           //ʹ�õ��ߣ�Ѫƿ,��ƿ�ȣ�
        public bool y_Input;

        public bool tap_rt_Input;
        public bool tap_lt_Input;

        public bool tap_rb_Input;                        //�ṥ��������
        public bool hold_rb_Input;                      //��ס�ṥ����(��ס)

        public bool lb_Input;                             //������[F]����;
        public bool tap_lb_Input;                      //����LB��[F]����;

        public bool jump_Input;
        public bool inventory_Input;
        public bool lockOn_Input;
        public bool sneakMove_Input;            //Ǳ���ƶ�;
        public bool right_Stick_Left_Input;     //�л�����Ŀ��[����]-Q
        public bool right_Stick_Right_Input;  //�л�����Ŀ��[����]-E

        public bool d_Pad_Up;                       //�Ϸ����
        public bool d_Pad_Down;                  //�·����
        public bool d_Pad_Right;                   //�����
        public bool d_Pad_Left;                     //�ҷ����

        public bool rollFlag;                //�������;
        public bool fireFlag;                //������;
        public bool twoHandFlag;      //˫�ֳ��ձ��;
        public bool sprintFlag;            //��̱��;
        public bool sneakMoveFlag;   //Ǳ�б��;
        public bool lockOnFlag;          //�������;
        public bool comboFlag;          //�������;
        public bool inventoryFlag;      //�����˵����;
        public float rollInputTimer;     //������ť�����ʱ;

        public Transform criricalAttackRaycastStartPoint;   //���̻򷴻�������߷����;

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

        //������:
        public void OnEnable()
        {
            if(inputActions == null)
            {
                inputActions = new PlayerControls();
                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();    //��������: ��->����(PlayerControls inputActions){ movementInput = inputActions.ReadValue<Vector2>();}
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

                inputActions.PlayerActions.Y.performed += i => y_Input = true;
                inputActions.PlayerActions.RB.performed += i => tap_rb_Input = true;    //�ṥ�������¼����
                inputActions.PlayerActions.HoldRB.performed += i => hold_rb_Input = true;   //�ṥ������ס
                inputActions.PlayerActions.HoldRB.canceled += i => hold_rb_Input = false;     //�ṥ����̧��
                inputActions.PlayerActions.RT.performed += i => tap_rt_Input = true;     //�ع��������¼����
                inputActions.PlayerActions.LT.performed += i => tap_lt_Input = true;      //ս�������¼����(���빥������)

                inputActions.PlayerActions.LB.performed += i => lb_Input = true;     //LB��[F]�����¼����(����ʱ)
                inputActions.PlayerActions.LB.canceled += i => lb_Input = false;       //LB��[F]�����¼����(̧��ʱ)
                inputActions.PlayerActions.TapLB.performed += i => tap_lb_Input = true;     //LB��[F]����

                inputActions.PlayerQuickSlots.DPadUp.performed += i => d_Pad_Up = true;          //�л���ǰ������Ʒ����
                inputActions.PlayerQuickSlots.DPadDown.performed += i => d_Pad_Down = true; //�л�����Ʒװ��
                inputActions.PlayerQuickSlots.DPadRight.performed += i => d_Pad_Right = true;   //�л��������������¼����
                inputActions.PlayerQuickSlots.DPadLeft.performed += i => d_Pad_Left = true;        //�л��������������¼����

                inputActions.PlayerActions.A.performed += i => a_Input = true;                               //��������������
                inputActions.PlayerActions.Roll.performed += i => b_Input = true;                           //���·�����ť
                inputActions.PlayerActions.Roll.canceled += i => b_Input = false;                             //�ɿ�������ť 
                inputActions.PlayerActions.X.performed += i => x_Input = true;                               //ʹ�õ��߰�����������()
                inputActions.PlayerActions.Jump.performed += i => jump_Input = true;                  //��Ծ����������
                inputActions.PlayerActions.Inventory.performed += i => inventory_Input = true;    //�򿪱�������������
                inputActions.PlayerActions.LockOn.performed += i => lockOn_Input = true;           //�������˰���������
                inputActions.PlayerMovement.LockOnTargetLeft.performed += i => right_Stick_Left_Input = true;           //�����л�����Ŀ��;
                inputActions.PlayerMovement.LockOnTargetRight.performed += i => right_Stick_Right_Input = true;      //�����л�����Ŀ��;

                //�޸�:
                inputActions.PlayerMovement.SneakMoveButton.performed += i => sneakMove_Input = true;
                inputActions.PlayerMovement.SneakMoveButton.canceled += i => sneakMove_Input = false;
                inputActions.PlayerActions.LB.canceled += i => sneakMove_Input = false;       //�ٶ������¼����(̧��ʱ)
            }

            inputActions.Enable();
        }
        //������:
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

        //����ƶ�����:
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
    
        //��ⷭ������:
        private void HandleRollInput(float delte)
        {
            if (playerManger.isClimbLadder && b_Input)
            {
                playerManger.isClimbLadder = false;
                playerManger.playerAnimatorManager.anim.SetBool("isClimbLadder", false);
                playerManger.playerAnimatorManager.PlayTargetAnimation("Falling", true);
            }
            //��Ҷ�����Player Actions��Roll�������Ƿ�Ϊ��ʼ�׶�(������)

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
    
        //����ṥ������:
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

        //��ס�������:(������סRB��"���")
        private void HandleHoldRBInput()
        {
            //�����������ṥ����ʱ:("������")
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
        
        //�ع�������:
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
                    //�ع������;
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

        //ս��[Shift������]����:
        private void HandleTapLTInput()
        {
            if (tap_lt_Input)
            {
                tap_lt_Input = false;
                WeaponItem rightWeapon = playerInventoryManager.rightWeapon;
                WeaponItem leftWeapon = playerInventoryManager.leftWeapon;
                //�����˫�ֳ���:(��ʹ������ս��)
                if(twoHandFlag == true)
                {
                    if(rightWeapon!=null && rightWeapon.tap_LT_Action)
                    {
                        playerManger.UpdateWhichHandCharacterIsUsing(false);
                        playerInventoryManager.currentItemBeingUsed = playerInventoryManager.rightWeapon;
                        playerInventoryManager.rightWeapon.tap_LT_Action.PerformAction(playerManger);
                    }
                }
                //���������ǿ�,�ǿ���(ȭͷ)
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

        //����LB��[F]��(����fΪ����ʩ��[������˫�������������ǿ�])
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
                        playerManger.UpdateWhichHandCharacterIsUsing(false);     //����LB��[F]
                        playerInventoryManager.currentItemBeingUsed = rightWeapon;
                        rightWeapon.tap_LB_Action.PerformAction(playerManger);
                    }
                }
                else
                {
                    if (leftWeapon != null && leftWeapon.tap_LB_Action != null)
                    {
                        playerManger.UpdateWhichHandCharacterIsUsing(true);     //����LB��[F]
                        playerInventoryManager.currentItemBeingUsed = leftWeapon;
                        leftWeapon.tap_LB_Action.PerformAction(playerManger);
                    }
                } 

            }
        }

        //����,�ٶ�,��׼��:(LB [F] ����ס)
        private void HandleHoldLBInput()
        {
            if (playerManger.isInAir || playerManger.isInteracting || playerManger.isFiringSpell)
            {
                return;
            }

            if (lb_Input)
            {
                //�ٶ�״̬�Ĳ���ֵ�ĸ�ֵ�ڴ˷�����;
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
                //�����ǿ�
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
                    blockingCollider.DisableBlockingCollider(); //���ö��Ƶ���ײ��(��ֹ���е���)
                }
            }
        }
    
        //�л���������:
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

        //�����˵�����:
        private void HandleInventoryInput()
        {
            if (inventory_Input)
            {
                //����������ڴ��ڴ�״̬:
                if(uiManager.playerLeveUpWindow.activeInHierarchy == true)
                {
                    uiManager.playerLeveUpWindow.GetComponent<LevelUpUI>().CloseInputEventMethod();
                    return;
                }
                //������˳���Ϸ���ڴ�״̬:
                if (playerManger.isExitGameWindow)
                {
                    //������Ⱦͷ��������:
                    uiManager.exitGameWindowManager.CloseExitGameWindow();
                    return;
                }
                //����ڽ��״��ڴ�״̬:
                if(playerManger.isTrading == true)
                {
                    //�򿪽������״���:
                    uiManager.commodityTradingWindow.EnableEndTransactionWindow();
                    return;
                }

                //���������������Ϣ,���ܴ򿪱���:
                if (playerManger.isResting)
                {
                    playerManger.uiManager.campfireUIWindowManager.CloseTransferFunctionWindow();          //���ô��ʹ���;
                    playerManger.uiManager.campfireUIWindowManager.CloseMemorySpellWindow();                //���ü��䷨������;
                    playerManger.uiManager.campfireUIWindowManager.CloseAssignElementBottleWindow();    //���÷���Ԫ��ƿ����;
                    playerManger.uiManager.campfireUIWindowManager.CloseFortifiedElementBottleWindow();//����ǿ��Ԫ��ƿ����;
                    playerManger.uiManager.EnableCampfireUIWindow();                                                               //���������ܴ���;
                    return;
                }

                //��������:
                inventoryFlag = !inventoryFlag;
                if (inventoryFlag)
                {
                    uiManager.OpenSelectWindow();
                    uiManager.UpdateUI();
                    uiManager.hudWindow.SetActive(false);
                }
                else
                {
                    uiManager.CloseSelectWindow();          //���ò˵�����
                    uiManager.CloseAllLeftWindow();         //����������д���
                    uiManager.CloseAllCentralWindow();   //�����м����д���
                    uiManager.CloseAllRightWindow();      //�����Ҳ����д���
                    uiManager.hudWindow.SetActive(true);    //���ü�������

                    tap_rb_Input = false;
                    hold_rb_Input = false;
                    tap_rt_Input = false;
                    tap_lt_Input = false;
                }
            }
        }
        
        //����Ŀ������:
        private void HandleLockOnInput()
        {
            //������ť���� && ������������״̬:
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
            }//������ť���� && ����������״̬:
            else if(lockOn_Input && lockOnFlag == true)
            {
                lockOnFlag = false;
                lockOn_Input = false;
                cameraHandler.ClearLockOnTargets();
            }

            //���������л�����Ŀ���:(���������Ի�:)
            if (right_Stick_Right_Input)
            {
                uiManager.dialogSystemWindowManager.SkipDialogue();
                //û������:
                if(lockOnFlag != true)
                {
                    right_Stick_Right_Input = false;
                }
            }

            //���������л�����Ŀ�����
            if (lockOnFlag && right_Stick_Left_Input)
            {
                right_Stick_Left_Input = false;
                cameraHandler.HandleLockOn();
                if (cameraHandler.leftLockTarget != null)
                {
                    cameraHandler.currentLockOnTarger = cameraHandler.leftLockTarget;
                }
            }
            //���������л�����Ŀ�����
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

        //˫�ֳ�������:
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

        //Ǳ������:
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

        //����ʹ�õ����¼�:(�����޸ģ����ںͽ�����ťͬһ����λ�����ڽ�������е���)
        public void HandleUseConsumableInput()
        {
            if(x_Input == true)
            {
                x_Input = false;
                //ʹ�õ�ǰ����Ʒ��
                if (playerInventoryManager.currentConsumable != null)
                {
                    Debug.Log(playerInventoryManager.currentConsumable.GetType());
                    playerInventoryManager.currentConsumable.AttemptToConsumeItem(animatorHandler, weaponSlotManger, playerEffectsManager);
                }
            }
        }
    }
}

