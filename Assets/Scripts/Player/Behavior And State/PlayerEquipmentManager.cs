using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    //玩家装备管理:
    public class PlayerEquipmentManager : MonoBehaviour
    {
        [Header("盾牌碰撞器:")]
        public BlockingCollider blockingCollider;
        [Header("身体模型数据:")]
        public DodyModelData bodyModelData;
        [Header("男性模型父物体引用:")]
        public GameObject maleModel;
        [Header("女性模型父物体引用:")]
        public GameObject femaleModel;

        [Header("模型转换器:")]
        [SerializeField] HelmetsModelChanger helmetsModelChanger;
        [SerializeField] TorsoModelChanger torsoModelChanger;      //胸甲模型转换器
        [SerializeField] HipModelChanger hipModelChanger;             //腿甲模型转换器
        [SerializeField] ArmsModelChanger armsModelChanger;       //臂甲模型转换器

        InputHandler inputHandler;
        PlayerManager playerManager;
        PlayerStatsManager playerStatsManager;
        PlayerInventoryManager playerInventoryManager;
        private void Awake()
        {
            inputHandler = GetComponent<InputHandler>();
            playerManager = GetComponent<PlayerManager>();
            playerStatsManager = GetComponent<PlayerStatsManager>();
            playerInventoryManager = GetComponent<PlayerInventoryManager>();

            helmetsModelChanger = GetComponentInChildren<HelmetsModelChanger>();
            torsoModelChanger = GetComponentInChildren<TorsoModelChanger>();
            hipModelChanger = GetComponentInChildren<HipModelChanger>();
            armsModelChanger = GetComponentInChildren<ArmsModelChanger>();
        }

        private void Start()
        {
            if (bodyModelData.isMale)
            {
                maleModel.SetActive(true);
                femaleModel.SetActive(false);
            }
            else
            {
                maleModel.SetActive(false);
                femaleModel.SetActive(true);
            }

            if(playerManager.isCreatingCharacter == false)
            {
                SetCharacterColor();
            }
            
            UninstallEquipment();
            EquipAllEquipmentModels();
        }

        //加载当前装备:
        public void EquipAllEquipmentModels()
        {
            //头盔:
            if (playerInventoryManager.currentHelmetEquipment != null)
            {
                //是否覆盖头部:
                if(playerInventoryManager.currentHelmetEquipment.isCoverHead == true)
                {
                    helmetsModelChanger.UnequipAllHeadModels();     //卸载头部模型
                }
                //如果是覆盖发型:
                else if(playerInventoryManager.currentHelmetEquipment.isCoverHair)
                {
                    if (playerInventoryManager.currentHelmetEquipment.isCoverFaciale)
                    {
                        helmetsModelChanger.EquipHeadModelById(bodyModelData.headId, bodyModelData.hairstyle, -1, bodyModelData.eyebrow, bodyModelData.isMale);
                    }
                    else
                    {
                        helmetsModelChanger.EquipHeadModelById(bodyModelData.headId, bodyModelData.hairstyle, bodyModelData.facialHairId, bodyModelData.eyebrow, bodyModelData.isMale);
                    }

                    helmetsModelChanger.UnequipHairstyleModels();   //卸载发型
                }
                else
                {
                    if (playerInventoryManager.currentHelmetEquipment.isCoverFaciale)
                    {
                        helmetsModelChanger.EquipHeadModelById(bodyModelData.headId, bodyModelData.hairstyle, -1 , bodyModelData.eyebrow, bodyModelData.isMale);
                    }
                    else
                    {
                        helmetsModelChanger.EquipHeadModelById(bodyModelData.headId, bodyModelData.hairstyle, bodyModelData.facialHairId, bodyModelData.eyebrow, bodyModelData.isMale);
                    }
                }

                helmetsModelChanger.UnequipAllHelmetModels();  //卸载头盔模型

                if (bodyModelData.isMale)
                {
                    helmetsModelChanger.EquipHelmetModelByName(playerInventoryManager.currentHelmetEquipment.helmerModelName_male,
                    true,
                    playerInventoryManager.currentHelmetEquipment.accessoriesIDs);
                }
                else
                {
                    helmetsModelChanger.EquipHelmetModelByName(playerInventoryManager.currentHelmetEquipment.helmerModelName, 
                    false, 
                    playerInventoryManager.currentHelmetEquipment.accessoriesIDs);
                }

                //更新抗性:
                playerStatsManager.physicalDamageAbsorptionHead = playerInventoryManager.currentHelmetEquipment.physicalDefense;
                playerStatsManager.fireDamageAbsorptionHead = playerInventoryManager.currentHelmetEquipment.fireDefense;
                playerStatsManager.magicDamageAbsorptionHead = playerInventoryManager.currentHelmetEquipment.magicDefense;
                playerStatsManager.lightningDamageAbsorptionHead = playerInventoryManager.currentHelmetEquipment.lightningDefense;
                playerStatsManager.darkDamageAbsorptionHead = playerInventoryManager.currentHelmetEquipment.darkDefense;
                //抵抗力:
                playerStatsManager.poisonDefenseAbsorptionHead = playerInventoryManager.currentHelmetEquipment.poisonDefense;
                playerStatsManager.frostDamageAbsorptionHead = playerInventoryManager.currentHelmetEquipment.frostDefense;
                playerStatsManager.hemorrhageDefenseAbsorptionHead = playerInventoryManager.currentHelmetEquipment.hemorrhageDefense;
                playerStatsManager.curseDefenseAbsorptionHead = playerInventoryManager.currentHelmetEquipment.curseDefense;
            }
            else
            {
                //卸载头盔模型:
                helmetsModelChanger.UnequipAllHelmetModels();
                //卸载头部的所有模型:
                helmetsModelChanger.UnequipAllHeadModels();
                //加载头部(头发,眉毛,胡须,头):
                helmetsModelChanger.EquipHeadModelById(bodyModelData.headId, bodyModelData.hairstyle, bodyModelData.facialHairId, bodyModelData.eyebrow, bodyModelData.isMale);
                //抗性:
                playerStatsManager.physicalDamageAbsorptionHead = 0;
                playerStatsManager.fireDamageAbsorptionHead = 0;
                playerStatsManager.magicDamageAbsorptionHead = 0;
                playerStatsManager.lightningDamageAbsorptionHead = 0;
                playerStatsManager.darkDamageAbsorptionHead = 0;
                //抵抗力:
                playerStatsManager.poisonDefenseAbsorptionHead = 0;
                playerStatsManager.frostDamageAbsorptionHead = 0;
                playerStatsManager.hemorrhageDefenseAbsorptionHead = 0;
                playerStatsManager.curseDefenseAbsorptionHead = 0;
            }

            //胸甲:
            if (playerInventoryManager.currentTorsoEquipment != null)
            {
                torsoModelChanger.UnequipAllTorsoModels();
                #region 数据
                int behindModelIndex = playerInventoryManager.currentTorsoEquipment.behindOrnamentID;
                string torsoName = playerInventoryManager.currentTorsoEquipment.torsoModelName;
                string rightRearArm = playerInventoryManager.currentTorsoEquipment.rightRearArmModeName;
                string leftRearArm = playerInventoryManager.currentTorsoEquipment.leftRearArmModelName;

                string rightShoulder = playerInventoryManager.currentTorsoEquipment.rightShoulderModelName;
                string leftShoulder = playerInventoryManager.currentTorsoEquipment.leftShoulderModelsName;
                if (bodyModelData.isMale)
                {
                    torsoName = playerInventoryManager.currentTorsoEquipment.torsoModelName_Male;
                    rightRearArm = playerInventoryManager.currentTorsoEquipment.rightRearArmModeName_Male;
                    leftRearArm = playerInventoryManager.currentTorsoEquipment.leftRearArmModelName_Male;
                }
                #endregion
                torsoModelChanger.LoadBehindModel(behindModelIndex);
                torsoModelChanger.EquipTorsoModelByName(torsoName, rightRearArm, leftRearArm ,rightShoulder , leftShoulder);

                //更新抗性:
                playerStatsManager.physicalDamageAbsorptionBody = playerInventoryManager.currentTorsoEquipment.physicalDefense;
                playerStatsManager.fireDamageAbsorptionBody = playerInventoryManager.currentTorsoEquipment.fireDefense;
                playerStatsManager.magicDamageAbsorptionBody = playerInventoryManager.currentTorsoEquipment.magicDefense;
                playerStatsManager.lightningDamageAbsorptionBody = playerInventoryManager.currentTorsoEquipment.lightningDefense;
                playerStatsManager.darkDamageAbsorptionBody = playerInventoryManager.currentTorsoEquipment.darkDefense;
                //抵抗力:
                playerStatsManager.poisonDefenseAbsorptionBody = playerInventoryManager.currentTorsoEquipment.poisonDefense;
                playerStatsManager.frostDamageAbsorptionBody = playerInventoryManager.currentTorsoEquipment.frostDefense;
                playerStatsManager.hemorrhageDefenseAbsorptionBody = playerInventoryManager.currentTorsoEquipment.hemorrhageDefense;
                playerStatsManager.curseDefenseAbsorptionBody = playerInventoryManager.currentTorsoEquipment.curseDefense;
            }
            else
            {
                torsoModelChanger.UnequipAllTorsoModels();
                torsoModelChanger.LoadNudeModel(bodyModelData.isMale);
                //抗性:
                playerStatsManager.physicalDamageAbsorptionBody = 0;
                playerStatsManager.fireDamageAbsorptionBody = 0;
                playerStatsManager.magicDamageAbsorptionBody = 0;
                playerStatsManager.lightningDamageAbsorptionBody = 0;
                playerStatsManager.darkDamageAbsorptionBody = 0;
                //抵抗力:
                playerStatsManager.poisonDefenseAbsorptionBody = 0;
                playerStatsManager.frostDamageAbsorptionBody = 0;
                playerStatsManager.hemorrhageDefenseAbsorptionBody = 0;
                playerStatsManager.curseDefenseAbsorptionBody = 0;
            }

            //腿甲:
            if(playerInventoryManager.currentLegEquipment != null)
            {
                hipModelChanger.UnEquipAllHipModels();
                #region 数据
                string hip = playerInventoryManager.currentLegEquipment.hipModleName;
                string rightLeg = playerInventoryManager.currentLegEquipment.rightLegName;
                string leftLeg = playerInventoryManager.currentLegEquipment.leftLegName;
                string rightKneePad = playerInventoryManager.currentLegEquipment.rightKneePadName;
                string leftKneePad = playerInventoryManager.currentLegEquipment.leftKneePadName;
                if (bodyModelData.isMale)
                {
                    hip = playerInventoryManager.currentLegEquipment.hipModleName_Male;
                    rightLeg = playerInventoryManager.currentLegEquipment.rightLegName_Male;
                    leftLeg = playerInventoryManager.currentLegEquipment.leftLegName_Male;
                }
                #endregion
                hipModelChanger.EquipAccessoriesModelsById(playerInventoryManager.currentLegEquipment.accessorieId);
                hipModelChanger.EquipHipModelByName(hip,rightLeg,leftLeg,rightKneePad,leftKneePad);

                //更新抗性:
                playerStatsManager.physicalDamageAbsorptionLegs = playerInventoryManager.currentLegEquipment.physicalDefense;
                playerStatsManager.fireDamageAbsorptionLegs = playerInventoryManager.currentLegEquipment.fireDefense;
                playerStatsManager.magicDamageAbsorptionLegs = playerInventoryManager.currentLegEquipment.magicDefense;
                playerStatsManager.lightningDamageAbsorptionLegs = playerInventoryManager.currentLegEquipment.darkDefense;
                //抵抗力:
                playerStatsManager.poisonDefenseAbsorptionLegs = playerInventoryManager.currentLegEquipment.poisonDefense;
                playerStatsManager.frostDamageAbsorptionLegs = playerInventoryManager.currentLegEquipment.frostDefense;
                playerStatsManager.hemorrhageDefenseAbsorptionLegs = playerInventoryManager.currentLegEquipment.hemorrhageDefense;
                playerStatsManager.curseDefenseAbsorptionLegs = playerInventoryManager.currentLegEquipment.curseDefense;
            }
            else
            {
                hipModelChanger.UnEquipAllHipModels();
                //加载腿模型:
                hipModelChanger.LoadNudeModel(bodyModelData.isMale);

                //更新抗性:
                playerStatsManager.physicalDamageAbsorptionLegs = 0;
                playerStatsManager.fireDamageAbsorptionLegs = 0;
                playerStatsManager.magicDamageAbsorptionLegs = 0;
                playerStatsManager.lightningDamageAbsorptionLegs = 0;
                //抵抗力:
                playerStatsManager.poisonDefenseAbsorptionLegs = 0;
                playerStatsManager.frostDamageAbsorptionLegs = 0;
                playerStatsManager.hemorrhageDefenseAbsorptionLegs = 0;
                playerStatsManager.curseDefenseAbsorptionLegs = 0;
            }

            //臂甲:
            if(playerInventoryManager.currentHandEquipment != null)
            {
                armsModelChanger.UnEquipAllHandModel();
                #region 数据
                string leftForearmName = playerInventoryManager.currentHandEquipment.leftForearmName;
                string rightForearmName = playerInventoryManager.currentHandEquipment.rightForearmName;
                string leftPalmName = playerInventoryManager.currentHandEquipment.leftPalmName;
                string rightPalmName = playerInventoryManager.currentHandEquipment.rightPalmName;
                string leftElbowName = playerInventoryManager.currentHandEquipment.leftElbowName;
                string rightElbowName = playerInventoryManager.currentHandEquipment.rightElbowName;
                if (bodyModelData.isMale)
                {
                    leftForearmName = playerInventoryManager.currentHandEquipment.leftForearmName_Male;
                    rightForearmName = playerInventoryManager.currentHandEquipment.rightForearmName_Male;
                    leftPalmName = playerInventoryManager.currentHandEquipment.leftPalmName_Male;
                    rightPalmName = playerInventoryManager.currentHandEquipment.rightPalmName_Male;
                }
                #endregion
                armsModelChanger.EquipHandModelByName(leftForearmName, rightForearmName, leftPalmName, rightPalmName, leftElbowName, rightElbowName);

                //更新抗性:
                playerStatsManager.physicalDamageAbsorptionHands = playerInventoryManager.currentHandEquipment.physicalDefense;
                playerStatsManager.fireDamageAbsorptionHands = playerInventoryManager.currentHandEquipment.fireDefense;
                playerStatsManager.magicDamageAbsorptionHands = playerInventoryManager.currentHandEquipment.magicDefense;
                playerStatsManager.lightningDamageAbsorptionHands = playerInventoryManager.currentHandEquipment.lightningDefense;
                playerStatsManager.darkDamageAbsorptionHands = playerInventoryManager.currentHandEquipment.darkDefense;
                //抵抗力:
                playerStatsManager.poisonDefenseAbsorptionHands = playerInventoryManager.currentHandEquipment.poisonDefense;
                playerStatsManager.frostDamageAbsorptionHands = playerInventoryManager.currentHandEquipment.frostDefense;
                playerStatsManager.hemorrhageDefenseAbsorptionHands = playerInventoryManager.currentHandEquipment.hemorrhageDefense;
                playerStatsManager.curseDefenseAbsorptionHands = playerInventoryManager.currentHandEquipment.curseDefense;
            }
            else
            {
                armsModelChanger.UnEquipAllHandModel();

                armsModelChanger.LoadNudeModel(bodyModelData.isMale);

                playerStatsManager.physicalDamageAbsorptionHands = 0;
                playerStatsManager.fireDamageAbsorptionHands = 0;
                playerStatsManager.magicDamageAbsorptionHands = 0;
                playerStatsManager.lightningDamageAbsorptionHands = 0;
                playerStatsManager.darkDamageAbsorptionHands = 0;
                //抵抗力:
                playerStatsManager.poisonDefenseAbsorptionHands = 0;
                playerStatsManager.frostDamageAbsorptionHands = 0;
                playerStatsManager.hemorrhageDefenseAbsorptionHands = 0;
                playerStatsManager.curseDefenseAbsorptionHands = 0;
            }
        }

        //启用盾牌碰撞:
        public void OpenBlockingCollider()
        {
            if (inputHandler.twoHandFlag)
            {
                blockingCollider.SetColliderDamageAbsorption(playerInventoryManager.rightWeapon);
            }
            //右手武器是空:(非双持下)
            else if(inputHandler.twoHandFlag == false && playerInventoryManager.leftWeapon.isUnarmed)
            {
                blockingCollider.SetColliderDamageAbsorption(playerInventoryManager.rightWeapon);
            }
            else
            {
                blockingCollider.SetColliderDamageAbsorption(playerInventoryManager.leftWeapon);
            }

            blockingCollider.EnableBlockingCollider();
        }

        //禁用盾牌碰撞:
        public void ClocseBlockingCollider()
        {

        }

        public void UninstallEquipment()
        {
            if (bodyModelData.isMale)
            {
                maleModel.SetActive(true);
                femaleModel.SetActive(false);
            }
            else
            {
                maleModel.SetActive(false);
                femaleModel.SetActive(true);
            }

            //卸载头盔
            helmetsModelChanger.UnequipAllHelmetModels();
            //卸载发型:
            helmetsModelChanger.UnequipHairstyleModels();
            //加载头部(头发,眉毛,胡须,头):
            helmetsModelChanger.EquipHeadModelById(bodyModelData.headId, bodyModelData.hairstyle , bodyModelData.facialHairId , bodyModelData.eyebrow, bodyModelData.isMale);

            playerStatsManager.physicalDamageAbsorptionHead = 0;
            playerStatsManager.fireDamageAbsorptionHead = 0;
            playerStatsManager.magicDamageAbsorptionHead = 0;
            playerStatsManager.lightningDamageAbsorptionHead = 0;
            playerStatsManager.darkDamageAbsorptionHead = 0;

            //卸载胸甲:
            torsoModelChanger.UnequipAllTorsoModels();
            torsoModelChanger.LoadNudeModel(bodyModelData.isMale);

            playerStatsManager.physicalDamageAbsorptionBody = 0;
            playerStatsManager.fireDamageAbsorptionBody = 0;
            playerStatsManager.magicDamageAbsorptionBody = 0;
            playerStatsManager.lightningDamageAbsorptionBody = 0;
            playerStatsManager.darkDamageAbsorptionBody = 0;

            //卸载腿甲:
            hipModelChanger.UnEquipAllHipModels();
            hipModelChanger.LoadNudeModel(bodyModelData.isMale);

            playerStatsManager.physicalDamageAbsorptionLegs = 0;
            playerStatsManager.fireDamageAbsorptionLegs = 0;
            playerStatsManager.magicDamageAbsorptionLegs = 0;
            playerStatsManager.lightningDamageAbsorptionLegs = 0;

            //卸载臂甲:
            armsModelChanger.UnEquipAllHandModel();
            armsModelChanger.LoadNudeModel(bodyModelData.isMale);

            playerStatsManager.physicalDamageAbsorptionHands = 0;
            playerStatsManager.fireDamageAbsorptionHands = 0;
            playerStatsManager.magicDamageAbsorptionHands = 0;
            playerStatsManager.lightningDamageAbsorptionHands = 0;
            playerStatsManager.darkDamageAbsorptionHands = 0;
        }

        //皮肤、毛发、面部涂鸦颜色:
        private void SetCharacterColor()
        {
            //毛发
            helmetsModelChanger.SetHairModelColor(bodyModelData.attributeHairColor,bodyModelData.hairColor);
            //皮肤
            helmetsModelChanger.SetHeadModelColor(bodyModelData.attributeSkinColor, bodyModelData.skinColor);
            torsoModelChanger.SetTorsoModelColor(bodyModelData.attributeSkinColor, bodyModelData.skinColor);
            hipModelChanger.SetLegModelColor(bodyModelData.attributeSkinColor, bodyModelData.skinColor);
            armsModelChanger.SetHandModelColor(bodyModelData.attributeSkinColor, bodyModelData.skinColor);
            //面部涂鸦颜色:
            helmetsModelChanger.SetfacialMarkColorl(bodyModelData.attributeFacialMarkColor, bodyModelData.facialMarkColor);
        }
    }
}