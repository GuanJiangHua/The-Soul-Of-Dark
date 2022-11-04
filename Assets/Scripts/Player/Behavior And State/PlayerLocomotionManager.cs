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

        public Vector3 moveDirection;                               //�ƶ�����;
        [HideInInspector]
        public Transform myTransform;
        [HideInInspector]
        public PlayerAnimatorManager animatorManager;

        public new Rigidbody rigidbody;
        public GameObject normalCamera;
        [Header("Ground or Air Detecion Stats")]              //�������м��:
        [SerializeField]
        float groundDetectionRayStartPoint = 0.5f;           //���������ߵĿ�ʼ��;(��ҵ�transform���ڽŵף�Ҫ����ƽ�Ƶ���Ӧ��;
        [SerializeField]
        float minimunDistanceNeededToBeginFall = 1.0f; //�ж�Ϊ��ʼ׹����Ҫ����С����;
        [SerializeField]
        float groudDirectionRayDistance = 0.2f;                //���淽�����߾���;
        public LayerMask groudMask;                                           //�����;
        public float inAirTimer;                                           //�ڿ��е�ʱ��ļ�ʱ��;

        [Header("�ƶ�����:")]
        [SerializeField]
        float movementSpeed = 5;
        [SerializeField]
        float sprintSpeed = 7;
        [SerializeField]
        float rotationSpeed = 10;
        [SerializeField]
        float climbLadderSpeed = 5;
        [SerializeField]
        float fallingSpeed = 45;                      //��׹�ٶ�;
        [Header("�������:")]
        int rollStaminaCost = 15;               //�������������;
        int backstepStaminaCost = 12;      //�󳷲����������;
        int sprintStaminaCost = 1;             //�������;
        [Header("��������:")]
        public bool isRoll = false;

        [Range(1,2)] public float rollDelay = 1;
        [Header("�󳷲�:")]
        public float stepBackSpeed = 1;

        [Header("ս���е���ײ��:")]
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

            //������ײ:(������������ײ������ײ)
            Physics.IgnoreCollision(characterCollider, characterCollidionBlockerCollider, true);
        }

        #region Movement
        Vector3 normalVector = new Vector2(0,1);
        Vector3 targetPosition;
        //����ʱ������:
        public Vector3 lostPropertyPosition;
        //��ת:
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
        //�ƶ�:
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

            //�ٶȷ�������normalVectorΪ��������ƽ���ϵ�ͶӰ;
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
        //����:
        public void HandleRollingAndSprinting(float delta)
        {
            if (animatorManager.anim.GetBool("isInteracting")) return;  //������ڷ������̣���ʲôҲ����;

            if ((playerState.currentStamina - rollStaminaCost) <= 0) return;

            if (inputHandler.rollFlag)
            {
                inputHandler.rollFlag = false;

                moveDirection = cameraObjcet.forward * inputHandler.vertical;
                moveDirection += cameraObjcet.right * inputHandler.horizontal;

                if(inputHandler.moveAmount > 0)
                {
                    animatorManager.PlayTargetAnimation("Rolling", true);   //���ŷ��������������ƶ�;
                    moveDirection.y = 0;
                    Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
                    myTransform.rotation = rollRotation;
                    playerState.TakeStaminaDamage(rollStaminaCost);       //�������;
                }
                else
                {
                    targetPosition = -myTransform.forward;
                    animatorManager.PlayTargetAnimation("Backstep", true);  //�󳷲�
                    playerState.TakeStaminaDamage(backstepStaminaCost);  //�������;
                }
            }
        }
        //��׹:
        public void HandleFalling(float delta, Vector3 moveDirection)
        {
            if (playerManger.isDeath) return;
            if (playerManger.isClimbLadder) return;

            playerManger.isGrounded = false;
            RaycastHit hit;
            Vector3 origin = myTransform.position;
            origin.y += groundDetectionRayStartPoint;   //��ʼ������;

            if(Physics.Raycast(origin,myTransform.forward,out hit, 0.4f))   //����ӿ�ʼ����б�·�(��ǰ)0.4f�������ж���,���ƶ�����Ϊ������;
            {
                moveDirection = Vector3.zero;
            }

            if (playerManger.isInAir)
            {
                float scale =  transform.localScale.y;
                rigidbody.AddForce(-Vector3.up * fallingSpeed / scale , ForceMode.Force);     //ʩ�ӷ������µ���
                rigidbody.AddForce(moveDirection * fallingSpeed / (4 * scale)); //ʩ���ƶ��������;
            }

            Vector3 dir = moveDirection;
            dir.Normalize();
            origin = origin + dir * groudDirectionRayDistance;

            targetPosition = myTransform.position;

            Debug.DrawRay(origin, -Vector3.up * minimunDistanceNeededToBeginFall, Color.red, 0.1f, false);
            if(Physics.Raycast(origin,-Vector3.up,out hit, minimunDistanceNeededToBeginFall, groudMask))
            {
                //ͶӰ�淨����Ϊ���߼�⵽��ƽ��ķ�����
                normalVector = hit.normal;
                Vector3 tp = hit.point;         //������ײ���ĵ�;
                playerManger.isGrounded = true; //�����ڵ�����;
                targetPosition.y = tp.y;                  //Ŀ���߶�λ�õ�����ײ���λ�õĸ߶�;

                if (playerManger.isInAir && inAirTimer < 2.0f)
                {
                    if(inAirTimer > 0.5f)
                    {
                        //�������ն���:
                        Debug.Log("You were in the air for " + inAirTimer);
                        animatorManager.PlayTargetAnimation("Land", true);      //������½�������������ڽ���;
                    }
                    else
                    {
                        animatorManager.PlayTargetAnimation("Empty", false);    //���ſն��������������ڽ���;
                        animatorManager.UpdateAnimatorValue(0, 0, false);
                    }
                    inAirTimer = 0;

                    playerManger.isInAir = false;
                }
                else if(playerManger.isInAir && inAirTimer >= 2.0f)
                {
                    //�浵:(���������ʧλ��,��ʧ�����,����λ��)
                    playerState.TakeDamageNoAnimation(9999, 0, 0, 0, 0);
                    targetPosition += new Vector3(0, 0.05f, 0);
                    myTransform.position = targetPosition;
                    return;
                }

                lostPropertyPosition = targetPosition;
            }
            else //���鲻������:
            {
                if (playerManger.isGrounded)    //���֮ǰ���ŵ�״̬,�ı�Ϊ���ŵ�:
                {
                    playerManger.isGrounded = false;
                }

                if(playerManger.isInAir == false)   //���֮ǰ��������״̬;
                {
                    if (!playerManger.isInteracting)
                    {
                        //��������״̬
                        animatorManager.PlayTargetAnimation("Falling", true);
                    }

                    Vector3 vel = rigidbody.velocity;
                    vel.Normalize();
                    rigidbody.velocity = vel * (movementSpeed / 2); //�����ƶ��ٶ�(����Ϊ�ƶ��ٶȵ�һ��)
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
                myTransform.position = targetPosition;      //���ƶ��Ļ�����Ϸ�����λ�õ��ڼ��������ײ����λ�ã�
            }
        }
        //��Ծ:
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
                    moveDirection = cameraObjcet.forward * inputHandler.vertical;           //������������ǰ�� * ��ֱ����;
                    moveDirection += cameraObjcet.right * inputHandler.horizontal;        //������������ǰ�� * ˮƽ����;
                    animatorManager.PlayTargetAnimation("Jumping While Running",true);  //������Ծ����;
                    moveDirection.y = 0;
                    Quaternion jumpRotation = Quaternion.LookRotation(moveDirection);
                    myTransform.rotation = jumpRotation;
                }
            }
        }
        //��¥��:
        public void HandleClimbLadder()
        {
            if (playerManger.isClimbLadder == false) return;
            
            animatorManager.UpdateAnimatorValue(inputHandler.vertical, 0, playerManger.isSprinting);
            rigidbody.velocity = Vector3.up  * inputHandler.vertical * climbLadderSpeed * transform.localScale.y;
        }
        #endregion
    }
}
