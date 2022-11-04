using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG{
    public class PlayerCombatManager : MonoBehaviour
    {
        InputHandler inputHandler;
        PlayerManager playerManger;
        PlayerStatsManager playerStateManager;
        PlayerEffectsManager playerEffectsManager;
        PlayerInventoryManager playerInventoryManager;
        PlayerAnimatorManager playerAnimatorManager;
        PlayerEquipmentManager playerEquipmentManager;
        PlayerWeaponSlotManger playerWeaponSlotManger;
        CameraHandler cameraHandler;

        LayerMask backStabLayer = 1<<13;    //���̼��ͼ��
        LayerMask riposteLayer = 1 << 14;      //�������ͼ��
        [Header("������������:")]
        //�ṥ��:
        public string OH_Light_Attack_1 = "OH_Light_Attack_01";
        public string OH_Light_Attack_2 = "OH_Light_Attack_02";
        public string OH_Light_Attack_3 = "OH_Light_Attack_03";
        //˫���ṥ��:
        public string Th_Light_Attack_1 = "TH_Light_Attack_01";
        public string Th_Light_Attack_2 = "TH_Light_Attack_02";
        //�����ع���:
        public string OH_Heavy_Attack_1 = "OH_Heavy_Attack_01";
        public string OH_Heavy_Attack_2 = "OH_Heavy_Attack_02";
        //������̹���:
        public string OH_Runing_Attack_1 = "OH_Runing_Attack";
        public string TH_Runing_Attack_1 = "TH_Runing_Attack";
        //������Ծ����:
        public string OH_Jump_Attack_1 = "OH_Jump_Attack";
        public string TH_Jump_Attack_1 = "TH_Jump_Attack";

        public string lastAttack;                       //���һ�ι���(����)
        public RangedAmmoItem currAmmo;                     //���ʱ�ĵ���
        private void Awake()
        {
            inputHandler = GetComponent<InputHandler>();
            playerManger = GetComponent<PlayerManager>();
            playerStateManager = GetComponent<PlayerStatsManager>();
            playerInventoryManager = GetComponent<PlayerInventoryManager>();
            playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
            playerWeaponSlotManger = GetComponent<PlayerWeaponSlotManger>();
            playerEquipmentManager = GetComponentInChildren<PlayerEquipmentManager>();
            playerEffectsManager = GetComponent<PlayerEffectsManager>();
            cameraHandler = FindObjectOfType<CameraHandler>();
        }

        //ִ������ս��:
        private void PerformLTWeaponAar(bool isTwoHanding)
        {
            //����ִ�н�������:
            if (playerManger.isInteracting == true) return;
            if(isTwoHanding == true)
            {
                //ʹ����������ս��
            }
            else
            {
                playerAnimatorManager.PlayTargetAnimation("Parry", true , 0);
            }
        }

        //�ɹ�ʩ��:(֡��������)
        private void SuccessfullyCastSpell()
        {
            CameraHandler cameraHandler = GameObject.FindObjectOfType<CameraHandler>();
            playerInventoryManager.currentSpell.SucessfullyCastSpell(playerAnimatorManager, playerStateManager, playerWeaponSlotManger , cameraHandler , playerManger.isUsingLeftHand);
            playerAnimatorManager.anim.SetBool("isFiringSpell",true);                                                   //����ʩ���ɹ����
        }

        //���Ա��̻򷴻�:
        public void AttemptBackStabOrRigoste()
        {
            WeaponItem rightWeapon = playerInventoryManager.rightWeapon;
            //����������ǽ�ս����:
            bool notMeleeWeapon = rightWeapon.weaponType == WeaponType.FaithCaster 
                || rightWeapon.weaponType == WeaponType.PyromancyCaster 
                || rightWeapon.weaponType == WeaponType.SpellCaster 
                || rightWeapon.isUnarmed;

            if (notMeleeWeapon) return;
            if (playerStateManager.currentStamina <= 1) return;

            RaycastHit hit;

            if (Physics.Raycast(inputHandler.criricalAttackRaycastStartPoint.transform.position, transform.TransformDirection(Vector3.forward), out hit, 0.5f, backStabLayer))
            {
                CharacterManager enemyChaeacterManager = hit.collider.GetComponentInParent<CharacterManager>();
                DamageCollider rightWeaponDamageCollider = playerWeaponSlotManger.rightHandDamageCollider;

                if (enemyChaeacterManager != null)
                {
                    //����Ա ID��������Ϳ���������˻������Լ���
                    //վ������λ����
                    playerManger.transform.position = enemyChaeacterManager.backStabCollider.criticalDamagerStandPosition.position;
                    //��ת���˵ķ����ñ��̿���������
                    Vector3 rotationDir = playerManger.transform.root.eulerAngles;  //��ҹ������������ŷ����
                    rotationDir = hit.transform.position - playerManger.transform.position;
                    rotationDir.y = 0;
                    rotationDir.Normalize();
                    Quaternion tr = Quaternion.LookRotation(rotationDir);
                    Quaternion targetRotation = Quaternion.Slerp(playerManger.transform.rotation, tr, 500 * Time.deltaTime);
                    playerManger.transform.rotation = targetRotation;

                    //���Ŷ���
                    playerAnimatorManager.PlayTargetAnimation("Back_Stab", true);
                    //����˲��ű����̶���
                    enemyChaeacterManager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("Back_Stabbed",true,0);
                    //���ô��������:
                    enemyChaeacterManager.GetComponentInChildren<AnimatorManager>().anim.SetBool("canDoCombo", false);
                    //�˺�����:�����������������Ȼ����֡�������ã������˺�)
                    int criticalDamage = playerInventoryManager.rightWeapon.criticalDamageMuiltiplier * rightWeaponDamageCollider.physicalDamage;
                    enemyChaeacterManager.pendingCriticalDamage = criticalDamage;
                }


            }
            else if (Physics.Raycast(inputHandler.criricalAttackRaycastStartPoint.transform.position, transform.TransformDirection(Vector3.forward), out hit, 0.7f, riposteLayer))
            {
                CharacterManager enemyManager = hit.collider.GetComponentInParent<CharacterManager>();
                DamageCollider rightHandWeaponDamageCollider = playerWeaponSlotManger.rightHandDamageCollider;

                //���˹�������Ϊ��,���Ҵ��ڿ��Դ���״̬
                if (enemyManager != null && enemyManager.canBeRiposted == true)
                {
                    //λ��ת��ָ��λ��,ת��Ŀ��:
                    playerManger.transform.position = enemyManager.riposteCollider.criticalDamagerStandPosition.position;
                    Vector3 rotationDir = playerManger.transform.root.eulerAngles;
                    rotationDir = hit.transform.position - playerManger.transform.position;
                    rotationDir.y = 0;
                    rotationDir.Normalize();
                    Quaternion tr = Quaternion.LookRotation(rotationDir);
                    Quaternion targetRotation = Quaternion.Slerp(playerManger.transform.rotation, tr, 500 * Time.deltaTime);
                    playerManger.transform.rotation = targetRotation;

                    //�˺�����:�����������������Ȼ����֡�������ã������˺�)
                    int criticalDamage = playerInventoryManager.rightWeapon.criticalDamageMuiltiplier * rightHandWeaponDamageCollider.physicalDamage;
                    enemyManager.pendingCriticalDamage = criticalDamage;
                    //���Ŷ���
                    playerAnimatorManager.PlayTargetAnimation("Riposte", true);
                    //����˲��ű����̶���
                    enemyManager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("Riposted",true,0);
                }
            }
         }
    }
}
