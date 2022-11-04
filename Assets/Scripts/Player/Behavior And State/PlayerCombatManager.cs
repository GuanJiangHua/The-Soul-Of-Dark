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

        LayerMask backStabLayer = 1<<13;    //背刺检测图层
        LayerMask riposteLayer = 1 << 14;      //处决检测图层
        [Header("武器攻击动作:")]
        //轻攻击:
        public string OH_Light_Attack_1 = "OH_Light_Attack_01";
        public string OH_Light_Attack_2 = "OH_Light_Attack_02";
        public string OH_Light_Attack_3 = "OH_Light_Attack_03";
        //双持轻攻击:
        public string Th_Light_Attack_1 = "TH_Light_Attack_01";
        public string Th_Light_Attack_2 = "TH_Light_Attack_02";
        //武器重攻击:
        public string OH_Heavy_Attack_1 = "OH_Heavy_Attack_01";
        public string OH_Heavy_Attack_2 = "OH_Heavy_Attack_02";
        //武器冲刺攻击:
        public string OH_Runing_Attack_1 = "OH_Runing_Attack";
        public string TH_Runing_Attack_1 = "TH_Runing_Attack";
        //武器跳跃攻击:
        public string OH_Jump_Attack_1 = "OH_Jump_Attack";
        public string TH_Jump_Attack_1 = "TH_Jump_Attack";

        public string lastAttack;                       //最后一次攻击(名字)
        public RangedAmmoItem currAmmo;                     //射击时的弹丸
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

        //执行武器战技:
        private void PerformLTWeaponAar(bool isTwoHanding)
        {
            //正在执行交互动作:
            if (playerManger.isInteracting == true) return;
            if(isTwoHanding == true)
            {
                //使用右手武器战技
            }
            else
            {
                playerAnimatorManager.PlayTargetAnimation("Parry", true , 0);
            }
        }

        //成功施法:(帧动画调用)
        private void SuccessfullyCastSpell()
        {
            CameraHandler cameraHandler = GameObject.FindObjectOfType<CameraHandler>();
            playerInventoryManager.currentSpell.SucessfullyCastSpell(playerAnimatorManager, playerStateManager, playerWeaponSlotManger , cameraHandler , playerManger.isUsingLeftHand);
            playerAnimatorManager.anim.SetBool("isFiringSpell",true);                                                   //设置施法成功标记
        }

        //尝试背刺或反击:
        public void AttemptBackStabOrRigoste()
        {
            WeaponItem rightWeapon = playerInventoryManager.rightWeapon;
            //如果武器不是近战武器:
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
                    //检查成员 ID（这样你就可以区别敌人还是你自己）
                    //站到背刺位置上
                    playerManger.transform.position = enemyChaeacterManager.backStabCollider.criticalDamagerStandPosition.position;
                    //旋转敌人的方向，让背刺看起来正常
                    Vector3 rotationDir = playerManger.transform.root.eulerAngles;  //玩家管理器根对象的欧拉角
                    rotationDir = hit.transform.position - playerManger.transform.position;
                    rotationDir.y = 0;
                    rotationDir.Normalize();
                    Quaternion tr = Quaternion.LookRotation(rotationDir);
                    Quaternion targetRotation = Quaternion.Slerp(playerManger.transform.rotation, tr, 500 * Time.deltaTime);
                    playerManger.transform.rotation = targetRotation;

                    //播放动画
                    playerAnimatorManager.PlayTargetAnimation("Back_Stab", true);
                    //令敌人播放被背刺动画
                    enemyChaeacterManager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("Back_Stabbed",true,0);
                    //设置打断其连击:
                    enemyChaeacterManager.GetComponentInChildren<AnimatorManager>().anim.SetBool("canDoCombo", false);
                    //伤害计算:（传入人物管理器，然后由帧动画调用，产生伤害)
                    int criticalDamage = playerInventoryManager.rightWeapon.criticalDamageMuiltiplier * rightWeaponDamageCollider.physicalDamage;
                    enemyChaeacterManager.pendingCriticalDamage = criticalDamage;
                }


            }
            else if (Physics.Raycast(inputHandler.criricalAttackRaycastStartPoint.transform.position, transform.TransformDirection(Vector3.forward), out hit, 0.7f, riposteLayer))
            {
                CharacterManager enemyManager = hit.collider.GetComponentInParent<CharacterManager>();
                DamageCollider rightHandWeaponDamageCollider = playerWeaponSlotManger.rightHandDamageCollider;

                //敌人管理器不为空,并且处于可以处决状态
                if (enemyManager != null && enemyManager.canBeRiposted == true)
                {
                    //位置转到指定位置,转向目标:
                    playerManger.transform.position = enemyManager.riposteCollider.criticalDamagerStandPosition.position;
                    Vector3 rotationDir = playerManger.transform.root.eulerAngles;
                    rotationDir = hit.transform.position - playerManger.transform.position;
                    rotationDir.y = 0;
                    rotationDir.Normalize();
                    Quaternion tr = Quaternion.LookRotation(rotationDir);
                    Quaternion targetRotation = Quaternion.Slerp(playerManger.transform.rotation, tr, 500 * Time.deltaTime);
                    playerManger.transform.rotation = targetRotation;

                    //伤害计算:（传入人物管理器，然后由帧动画调用，产生伤害)
                    int criticalDamage = playerInventoryManager.rightWeapon.criticalDamageMuiltiplier * rightHandWeaponDamageCollider.physicalDamage;
                    enemyManager.pendingCriticalDamage = criticalDamage;
                    //播放动画
                    playerAnimatorManager.PlayTargetAnimation("Riposte", true);
                    //令敌人播放被背刺动画
                    enemyManager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("Riposted",true,0);
                }
            }
         }
    }
}
