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
        [Header("������߷���λ��:")]
        public Transform RayCastStartPoint;
        [Header("UI:")]
        public GameObject interactableObj;          //�볡�����彻������ʾUI
        public GameObject itemInteractableObj;   //ʰȡ��Ʒ��ϢUI
        [Header("���ڴ�����ɫ:")]
        public bool isCreatingCharacter = false;
        [Header("�����˳���Ϸ����:")]
        public bool isExitGameWindow = false;
        public Transform headPosition;
        [Header("������������Ϣ:")]
        public bool isResting = false;
        public int previousBonfireIndex;           //��һ����������λ��
        [Header("���ڽ���:")]
        public bool isTrading = false;
        [Header("����λ��:")]
        public Vector3 rebirthPosition = new Vector3();

        [Header("Ԫ��ƿ����:")]
        public int totalNumberElementBottle = 5;    //Ԫ��ƿ����(����ƿһ��)
        public int numberElement = 5;                     //Ԫ��ƿ����(������ƿ)
        [Header("Ԫ��ƿǿ��:")]
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

                //����:
                PlayerSaveManager.LoadDataToPlayer(this);
            }
            else
            {
                //��ҵȼ�:
                playerStateManager.characterLeve = 0;

                //�������ģ��:
                DodyModelData dodyModelD = new DodyModelData();

                dodyModelD.isMale = true;                   //�Ա�
                dodyModelD.headId = 0;                 //ͷ��
                dodyModelD.hairstyle = -1;            //����
                dodyModelD.facialHairId = -1;   //����
                dodyModelD.eyebrow = -1;           //üë

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

            //װ������:
            //a.��ָ:
            //
            //interactUI.gameObject.SetActive(false);
        }

        public void Update()
        {
            float delta = Time.deltaTime;

            isInteracting = anim.GetBool("isInteracting");               //�Ƿ����ڽ���
            canFire = anim.GetBool("canFire");
            canDoCombo = anim.GetBool("canDoCombo");           //�Ƿ��������
            isFiringSpell = anim.GetBool("isFiringSpell");
            isInvulnerab = anim.GetBool("isInvulnerab");                //�Ƿ�ǹ����
            
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


            #region inputHandler�ű���������:
            inputHandler.TickInput(delta);
            #endregion
            #region playerLocomotion�ű���������:
            playerLocomotion.HandleRollingAndSprinting(delta);
            playerLocomotion.HandleJumping();
            #endregion
            #region PlayerState�ű���������
            playerStateManager.RegenerateStamina();
            #endregion

            CheckForInteractaObject();
        }

        //����֡����:
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

        //��ʱ����:
        private void LateUpdate()
        {
            #region ��������
            //�ܽ����޸�:
            isDeath = playerStateManager.isDead;
            //end�ܽ����޸�

            inputHandler.d_Pad_Up = false;
            inputHandler.d_Pad_Down = false;
            inputHandler.d_Pad_Right = false;
            inputHandler.d_Pad_Left = false;
            inputHandler.a_Input = false;
            inputHandler.inventory_Input = false;

            isSprinting = inputHandler.b_Input;

            //��������ʱ���¼
            if (isInAir)
            {
                playerLocomotion.inAirTimer += Time.deltaTime;  //������ڿ��У���ô����ʱ�俪ʼ�ۼ�;
            }
            #endregion
            //���������:
            #region cameraHandler��������:
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

        #region �������Ϸ������Ʒ����:
        //��⸽���Ŀɽ�������:(���ں�ʹ������Ʒ������ͬһ��λ��������ʹ������Ʒ�������)
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

                        //�ر�֮ǰ����Ʒ��Ϣui
                        itemInteractableObj.SetActive(false);
                        //����ui��Ϣ;
                        if (inputHandler.a_Input)
                        {
                            if (isInteracting) return;
                            hit.collider.GetComponent<Interactable>().Interact(this);   //���ý�������Ľ�������;

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
                    Invoke("DisableItemInteractableObj", 3);        //�����Ʒ��Ϣ�����õģ���������;
                }

                //�����޸ģ�ʹ�õ��߰�ť�ͽ�����ťΪͬһ����ť�����ڴ˵���ʹ�õ��߷����ļ��;
                //���ڸ����н�����Ʒ����£����Ƚ������޽�����Ʒ��ʹ�õ���;
                if (isInteracting == false)
                {
                    inputHandler.HandleUseConsumableInput();
                }
            }
        }

        //���佻��:
        public void OpenChestInteraction(Transform playerStandsHereWhenOpeningChest)
        {
            playerLocomotion.rigidbody.velocity = Vector3.zero;
            transform.position = playerStandsHereWhenOpeningChest.position;
            playerAnimatorManager.PlayTargetAnimation("Open_Chest", true);
        }

        //������ǽ����:
        public void PassThroughFogWallInteraction(Transform fogWallEntance)
        {
            //ȷ���������ǽ��
            playerLocomotion.rigidbody.velocity = Vector3.zero;

            Vector3 rotationDir = fogWallEntance.transform.forward;
            Quaternion turnRotation = Quaternion.LookRotation(rotationDir);
            transform.rotation = turnRotation;
            //���Ž�����ǽ����:
            playerAnimatorManager.PlayTargetAnimation("Pass_Through_Fog", true);
        }

        //��������(��ƷUI):
        private void DisableItemInteractableObj()
        {
            itemInteractableObj.SetActive(false);
        }
        #endregion
    }
}