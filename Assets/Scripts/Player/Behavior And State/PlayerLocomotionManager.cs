using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerLocomotionManager : MonoBehaviour
    {
        Transform cameraObjcet;
        InputHandler inputHandler;
        PlayerManager playerManger;
        PlayerStatsManager playerState;
        CameraHandler cameraHandler;

        public Vector3 moveDirection;                               //移动方向;
        [HideInInspector]
        public Transform myTransform;
        [HideInInspector]
        public PlayerAnimatorManager animatorManager;

        public new Rigidbody rigidbody;
        public GameObject normalCamera;
        [Header("Ground or Air Detecion Stats")]              //地面或空中检测:
        [SerializeField]
        float groundDetectionRayStartPoint = 0.5f;           //地面检测射线的开始点;(玩家的transform点在脚底，要向上平移到对应点;
        [SerializeField]
        float minimunDistanceNeededToBeginFall = 1.0f; //判断为开始坠落需要的最小距离;
        [SerializeField]
        float groudDirectionRayDistance = 0.2f;                //地面方向射线距离;
        public LayerMask groudMask;                                           //地面层;
        public float inAirTimer;                                           //在空中的时间的计时器;

        [Header("移动属性:")]
        [SerializeField]
        float movementSpeed = 5;
        [SerializeField]
        float sprintSpeed = 7;
        [SerializeField]
        float rotationSpeed = 10;
        [SerializeField]
        float climbLadderSpeed = 5;
        [SerializeField]
        float fallingSpeed = 45;                      //下坠速度;
        [Header("耐力损耗:")]
        int rollStaminaCost = 15;               //翻滚的耐力损耗;
        int backstepStaminaCost = 12;      //后撤步的耐力损耗;
        int sprintStaminaCost = 1;             //耐力损耗;
        [Header("翻滚属性:")]
        public bool isRoll = false;

        [Range(1,2)] public float rollDelay = 1;
        [Header("后撤步:")]
        public float stepBackSpeed = 1;

        [Header("战斗中的碰撞器:")]
        public CapsuleCollider characterCollider;
        public CapsuleCollider characterCollidionBlockerCollider;
        public Collider backStabCollider;
        public Collider blockingCollider;
        private void Awake()
        {
            cameraHandler = FindObjectOfType<CameraHandler>();
            rigidbody = GetComponent<Rigidbody>();
            inputHandler = GetComponent<InputHandler>();
            animatorManager = GetComponent<PlayerAnimatorManager>();
            playerManger = GetComponent<PlayerManager>();
            playerState = GetComponent<PlayerStatsManager>();
        }
        void Start()
        {
            cameraObjcet = Camera.main.transform;
            myTransform = transform;

            //忽略碰撞:(忽略这两给碰撞器的碰撞)
            Physics.IgnoreCollision(characterCollider, characterCollidionBlockerCollider, true);
        }

        #region Movement
        Vector3 normalVector = new Vector2(0,1);
        Vector3 targetPosition;
        //死亡时被保存:
        public Vector3 lostPropertyPosition;
        //旋转:
        public void HandleRotation(float delte)
        {
            if (playerManger.canRotate == false) return;
            if (playerManger.isClimbLadder == true) return;

            if (playerManger.isAiming)
            {
                Quaternion targetRotation = Quaternion.Euler(0, cameraHandler.cameraTransform.eulerAngles.y, 0);
                Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                transform.rotation = playerRotation;
            }
            else
            {
                if (inputHandler.lockOnFlag)
                {
                    if(inputHandler.sprintFlag || inputHandler.rollFlag)
                    {
                        Vector3 targetDir = Vector3.zero;
                        targetDir = cameraObjcet.transform.forward * inputHandler.vertical;
                        targetDir += cameraObjcet.transform.right * inputHandler.horizontal;
                        targetDir.Normalize();
                        targetDir.y = 0;

                        if(targetDir == Vector3.zero)
                        {
                            targetDir = transform.forward;
                        }

                        Quaternion ts = Quaternion.LookRotation(targetDir);
                        Quaternion targetRotation = Quaternion.SlerpUnclamped(transform.rotation, ts, 5 * rotationSpeed * delte);

                        transform.rotation = targetRotation;
                    }
                    else
                    {
                        Vector3 rotationDir = moveDirection;
                        rotationDir = cameraHandler.currentLockOnTarger.transform.position - transform.position;
                        rotationDir.y = 0;
                        rotationDir.Normalize();
                        Quaternion rs = Quaternion.LookRotation(rotationDir);
                        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, rs, rotationSpeed * delte);
                        transform.rotation = targetRotation;
                    }

                }
                else
                {
                    Vector3 targetDir = Vector3.zero;
                    float moveOverride = inputHandler.moveAmount;

                    targetDir = cameraObjcet.forward * inputHandler.vertical;
                    targetDir += cameraObjcet.right * inputHandler.horizontal;

                    targetDir.Normalize();
                    targetDir.y = 0;

                    if (targetDir == Vector3.zero)
                        targetDir = transform.forward;

                    float rs = rotationSpeed;
                    Quaternion ts = Quaternion.LookRotation(targetDir);
                    Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, ts, rs * delte);

                    myTransform.rotation = targetRotation;
                }
            }

        }
        //移动:
        public void HandleMovement(float delta)
        {
            if (inputHandler.rollFlag) return;

            if (playerManger.isInteracting) return;

            if (playerManger.isInAir) return;

            if (playerManger.isClimbLadder) return;

            moveDirection = cameraObjcet.forward * inputHandler.vertical;
            moveDirection += cameraObjcet.right * inputHandler.horizontal;
            moveDirection.Normalize();

            float speed = movementSpeed;
            if (inputHandler.sprintFlag)
            {
                speed = sprintSpeed;
                moveDirection *= speed;
                playerManger.isSprinting = true;
                playerState.TakeStaminaDamage(sprintStaminaCost);
            }
            else if (inputHandler.sneakMove_Input || playerManger.isHoldingArrow)
            {
                moveDirection *= (speed/3.0f);
            }
            else
            {
                moveDirection *= speed;
            }

            //速度方向在以normalVector为法向量的平面上的投影;
            Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
            rigidbody.velocity = projectedVelocity;


            if (inputHandler.lockOnFlag && inputHandler.sprintFlag == false)
            {
                animatorManager.UpdateAnimatorValue(inputHandler.vertical, inputHandler.horizontal , playerManger.isSprinting);
            }
            else
            {
                animatorManager.UpdateAnimatorValue(inputHandler.moveAmount, 0 , playerManger.isSprinting);
            }
        }
        //翻滚:
        public void HandleRollingAndSprinting(float delta)
        {
            if (animatorManager.anim.GetBool("isInteracting")) return;  //如果正在翻滚或冲刺，就什么也不做;

            if ((playerState.currentStamina - rollStaminaCost) <= 0) return;

            if (inputHandler.rollFlag)
            {
                inputHandler.rollFlag = false;

                moveDirection = cameraObjcet.forward * inputHandler.vertical;
                moveDirection += cameraObjcet.right * inputHandler.horizontal;

                if(inputHandler.moveAmount > 0)
                {
                    animatorManager.PlayTargetAnimation("Rolling", true);   //播放翻滚，并开启根移动;
                    moveDirection.y = 0;
                    Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
                    myTransform.rotation = rollRotation;
                    playerState.TakeStaminaDamage(rollStaminaCost);       //损耗耐力;
                }
                else
                {
                    targetPosition = -myTransform.forward;
                    animatorManager.PlayTargetAnimation("Backstep", true);  //后撤步
                    playerState.TakeStaminaDamage(backstepStaminaCost);  //损耗耐力;
                }
            }
        }
        //下坠:
        public void HandleFalling(float delta, Vector3 moveDirection)
        {
            if (playerManger.isDeath) return;
            if (playerManger.isClimbLadder) return;

            playerManger.isGrounded = false;
            RaycastHit hit;
            Vector3 origin = myTransform.position;
            origin.y += groundDetectionRayStartPoint;   //开始点上移;

            if(Physics.Raycast(origin,myTransform.forward,out hit, 0.4f))   //如果从开始检测点斜下方(朝前)0.4f距离内有对象,则移动方向为零向量;
            {
                moveDirection = Vector3.zero;
            }

            if (playerManger.isInAir)
            {
                float scale =  transform.localScale.y;
                rigidbody.AddForce(-Vector3.up * fallingSpeed / scale , ForceMode.Force);     //施加方向向下的力
                rigidbody.AddForce(moveDirection * fallingSpeed / (4 * scale)); //施加移动方向的力;
            }

            Vector3 dir = moveDirection;
            dir.Normalize();
            origin = origin + dir * groudDirectionRayDistance;

            targetPosition = myTransform.position;

            Debug.DrawRay(origin, -Vector3.up * minimunDistanceNeededToBeginFall, Color.red, 0.1f, false);
            if(Physics.Raycast(origin,-Vector3.up,out hit, minimunDistanceNeededToBeginFall, groudMask))
            {
                //投影面法向量为射线检测到的平面的法向量
                normalVector = hit.normal;
                Vector3 tp = hit.point;         //射线碰撞到的点;
                playerManger.isGrounded = true; //设置在地面上;
                targetPosition.y = tp.y;                  //目标点高度位置等于碰撞点的位置的高度;

                if (playerManger.isInAir && inAirTimer < 2.0f)
                {
                    if(inAirTimer > 0.5f)
                    {
                        //播放悬空动画:
                        Debug.Log("You were in the air for " + inAirTimer);
                        animatorManager.PlayTargetAnimation("Land", true);      //播放着陆动画，开启正在交互;
                    }
                    else
                    {
                        animatorManager.PlayTargetAnimation("Empty", false);    //播放空动画，不开启正在交互;
                        animatorManager.UpdateAnimatorValue(0, 0, false);
                    }
                    inAirTimer = 0;

                    playerManger.isInAir = false;
                }
                else if(playerManger.isInAir && inAirTimer >= 2.0f)
                {
                    //存档:(设置灵魂遗失位置,遗失灵魂量,复活位置)
                    playerState.TakeDamageNoAnimation(9999, 0, 0, 0, 0);
                    targetPosition += new Vector3(0, 0.05f, 0);
                    myTransform.position = targetPosition;
                    return;
                }

                lostPropertyPosition = targetPosition;
            }
            else //检验不到地面:
            {
                if (playerManger.isGrounded)    //如果之前是着地状态,改变为不着地:
                {
                    playerManger.isGrounded = false;
                }

                if(playerManger.isInAir == false)   //如果之前不是悬空状态;
                {
                    if (!playerManger.isInteracting)
                    {
                        //播放悬空状态
                        animatorManager.PlayTargetAnimation("Falling", true);
                    }

                    Vector3 vel = rigidbody.velocity;
                    vel.Normalize();
                    rigidbody.velocity = vel * (movementSpeed / 2); //重置移动速度(减少为移动速度的一半)
                    playerManger.isInAir = true;
                }
            }

            if (playerManger.isInteracting || inputHandler.moveAmount > 0)
            {
                myTransform.position = Vector3.Lerp(myTransform.position, targetPosition, Time.deltaTime/0.1f);
                //myTransform.position = targetPosition;
            }
            else
            {
                myTransform.position = targetPosition;      //不移动的话，游戏对象的位置等于检测射线碰撞到的位置；
            }
        }
        //跳跃:
        public void HandleJumping()
        {
            if (playerManger.isInteracting)
                return;
            if (playerState.currentStamina <= 0) 
                return;

            if (inputHandler.jump_Input)
            {
                inputHandler.jump_Input = false;

                if(inputHandler.moveAmount > 0)
                {
                    moveDirection = cameraObjcet.forward * inputHandler.vertical;           //方向等于摄像机前方 * 垂直输入;
                    moveDirection += cameraObjcet.right * inputHandler.horizontal;        //方向等于摄像机前方 * 水平输入;
                    animatorManager.PlayTargetAnimation("Jumping While Running",true);  //播放跳跃动画;
                    moveDirection.y = 0;
                    Quaternion jumpRotation = Quaternion.LookRotation(moveDirection);
                    myTransform.rotation = jumpRotation;
                }
            }
        }
        //爬楼梯:
        public void HandleClimbLadder()
        {
            if (playerManger.isClimbLadder == false) return;
            
            animatorManager.UpdateAnimatorValue(inputHandler.vertical, 0, playerManger.isSprinting);
            rigidbody.velocity = Vector3.up  * inputHandler.vertical * climbLadderSpeed * transform.localScale.y;
        }
        #endregion
    }
}
