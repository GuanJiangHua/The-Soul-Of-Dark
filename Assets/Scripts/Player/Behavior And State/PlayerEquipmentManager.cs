using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    //���װ������:
    public class PlayerEquipmentManager : MonoBehaviour
    {
        [Header("������ײ��:")]
        public BlockingCollider blockingCollider;
        [Header("����ģ������:")]
        public DodyModelData bodyModelData;
        [Header("����ģ�͸���������:")]
        public GameObject maleModel;
        [Header("Ů��ģ�͸���������:")]
        public GameObject femaleModel;

        [Header("ģ��ת����:")]
        [SerializeField] HelmetsModelChanger helmetsModelChanger;
        [SerializeField] TorsoModelChanger torsoModelChanger;      //�ؼ�ģ��ת����
        [SerializeField] HipModelChanger hipModelChanger;             //�ȼ�ģ��ת����
        [SerializeField] ArmsModelChanger armsModelChanger;       //�ۼ�ģ��ת����

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

        //���ص�ǰװ��:
        public void EquipAllEquipmentModels()
        {
            //ͷ��:
            if (playerInventoryManager.currentHelmetEquipment != null)
            {
                //�Ƿ񸲸�ͷ��:
                if(playerInventoryManager.currentHelmetEquipment.isCoverHead == true)
                {
                    helmetsModelChanger.UnequipAllHeadModels();     //ж��ͷ��ģ��
                }
                //����Ǹ��Ƿ���:
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

                    helmetsModelChanger.UnequipHairstyleModels();   //ж�ط���
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

                helmetsModelChanger.UnequipAllHelmetModels();  //ж��ͷ��ģ��

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

                //���¿���:
                playerStatsManager.physicalDamageAbsorptionHead = playerInventoryManager.currentHelmetEquipment.physicalDefense;
                playerStatsManager.fireDamageAbsorptionHead = playerInventoryManager.currentHelmetEquipment.fireDefense;
                playerStatsManager.magicDamageAbsorptionHead = playerInventoryManager.currentHelmetEquipment.magicDefense;
                playerStatsManager.lightningDamageAbsorptionHead = playerInventoryManager.currentHelmetEquipment.lightningDefense;
                playerStatsManager.darkDamageAbsorptionHead = playerInventoryManager.currentHelmetEquipment.darkDefense;
                //�ֿ���:
                playerStatsManager.poisonDefenseAbsorptionHead = playerInventoryManager.currentHelmetEquipment.poisonDefense;
                playerStatsManager.frostDamageAbsorptionHead = playerInventoryManager.currentHelmetEquipment.frostDefense;
                playerStatsManager.hemorrhageDefenseAbsorptionHead = playerInventoryManager.currentHelmetEquipment.hemorrhageDefense;
                playerStatsManager.curseDefenseAbsorptionHead = playerInventoryManager.currentHelmetEquipment.curseDefense;
            }
            else
            {
                //ж��ͷ��ģ��:
                helmetsModelChanger.UnequipAllHelmetModels();
                //ж��ͷ��������ģ��:
                helmetsModelChanger.UnequipAllHeadModels();
                //����ͷ��(ͷ��,üë,����,ͷ):
                helmetsModelChanger.EquipHeadModelById(bodyModelData.headId, bodyModelData.hairstyle, bodyModelData.facialHairId, bodyModelData.eyebrow, bodyModelData.isMale);
                //����:
                playerStatsManager.physicalDamageAbsorptionHead = 0;
                playerStatsManager.fireDamageAbsorptionHead = 0;
                playerStatsManager.magicDamageAbsorptionHead = 0;
                playerStatsManager.lightningDamageAbsorptionHead = 0;
                playerStatsManager.darkDamageAbsorptionHead = 0;
                //�ֿ���:
                playerStatsManager.poisonDefenseAbsorptionHead = 0;
                playerStatsManager.frostDamageAbsorptionHead = 0;
                playerStatsManager.hemorrhageDefenseAbsorptionHead = 0;
                playerStatsManager.curseDefenseAbsorptionHead = 0;
            }

            //�ؼ�:
            if (playerInventoryManager.currentTorsoEquipment != null)
            {
                torsoModelChanger.UnequipAllTorsoModels();
                #region ����
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

                //���¿���:
                playerStatsManager.physicalDamageAbsorptionBody = playerInventoryManager.currentTorsoEquipment.physicalDefense;
                playerStatsManager.fireDamageAbsorptionBody = playerInventoryManager.currentTorsoEquipment.fireDefense;
                playerStatsManager.magicDamageAbsorptionBody = playerInventoryManager.currentTorsoEquipment.magicDefense;
                playerStatsManager.lightningDamageAbsorptionBody = playerInventoryManager.currentTorsoEquipment.lightningDefense;
                playerStatsManager.darkDamageAbsorptionBody = playerInventoryManager.currentTorsoEquipment.darkDefense;
                //�ֿ���:
                playerStatsManager.poisonDefenseAbsorptionBody = playerInventoryManager.currentTorsoEquipment.poisonDefense;
                playerStatsManager.frostDamageAbsorptionBody = playerInventoryManager.currentTorsoEquipment.frostDefense;
                playerStatsManager.hemorrhageDefenseAbsorptionBody = playerInventoryManager.currentTorsoEquipment.hemorrhageDefense;
                playerStatsManager.curseDefenseAbsorptionBody = playerInventoryManager.currentTorsoEquipment.curseDefense;
            }
            else
            {
                torsoModelChanger.UnequipAllTorsoModels();
                torsoModelChanger.LoadNudeModel(bodyModelData.isMale);
                //����:
                playerStatsManager.physicalDamageAbsorptionBody = 0;
                playerStatsManager.fireDamageAbsorptionBody = 0;
                playerStatsManager.magicDamageAbsorptionBody = 0;
                playerStatsManager.lightningDamageAbsorptionBody = 0;
                playerStatsManager.darkDamageAbsorptionBody = 0;
                //�ֿ���:
                playerStatsManager.poisonDefenseAbsorptionBody = 0;
                playerStatsManager.frostDamageAbsorptionBody = 0;
                playerStatsManager.hemorrhageDefenseAbsorptionBody = 0;
                playerStatsManager.curseDefenseAbsorptionBody = 0;
            }

            //�ȼ�:
            if(playerInventoryManager.currentLegEquipment != null)
            {
                hipModelChanger.UnEquipAllHipModels();
                #region ����
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

                //���¿���:
                playerStatsManager.physicalDamageAbsorptionLegs = playerInventoryManager.currentLegEquipment.physicalDefense;
                playerStatsManager.fireDamageAbsorptionLegs = playerInventoryManager.currentLegEquipment.fireDefense;
                playerStatsManager.magicDamageAbsorptionLegs = playerInventoryManager.currentLegEquipment.magicDefense;
                playerStatsManager.lightningDamageAbsorptionLegs = playerInventoryManager.currentLegEquipment.darkDefense;
                //�ֿ���:
                playerStatsManager.poisonDefenseAbsorptionLegs = playerInventoryManager.currentLegEquipment.poisonDefense;
                playerStatsManager.frostDamageAbsorptionLegs = playerInventoryManager.currentLegEquipment.frostDefense;
                playerStatsManager.hemorrhageDefenseAbsorptionLegs = playerInventoryManager.currentLegEquipment.hemorrhageDefense;
                playerStatsManager.curseDefenseAbsorptionLegs = playerInventoryManager.currentLegEquipment.curseDefense;
            }
            else
            {
                hipModelChanger.UnEquipAllHipModels();
                //������ģ��:
                hipModelChanger.LoadNudeModel(bodyModelData.isMale);

                //���¿���:
                playerStatsManager.physicalDamageAbsorptionLegs = 0;
                playerStatsManager.fireDamageAbsorptionLegs = 0;
                playerStatsManager.magicDamageAbsorptionLegs = 0;
                playerStatsManager.lightningDamageAbsorptionLegs = 0;
                //�ֿ���:
                playerStatsManager.poisonDefenseAbsorptionLegs = 0;
                playerStatsManager.frostDamageAbsorptionLegs = 0;
                playerStatsManager.hemorrhageDefenseAbsorptionLegs = 0;
                playerStatsManager.curseDefenseAbsorptionLegs = 0;
            }

            //�ۼ�:
            if(playerInventoryManager.currentHandEquipment != null)
            {
                armsModelChanger.UnEquipAllHandModel();
                #region ����
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

                //���¿���:
                playerStatsManager.physicalDamageAbsorptionHands = playerInventoryManager.currentHandEquipment.physicalDefense;
                playerStatsManager.fireDamageAbsorptionHands = playerInventoryManager.currentHandEquipment.fireDefense;
                playerStatsManager.magicDamageAbsorptionHands = playerInventoryManager.currentHandEquipment.magicDefense;
                playerStatsManager.lightningDamageAbsorptionHands = playerInventoryManager.currentHandEquipment.lightningDefense;
                playerStatsManager.darkDamageAbsorptionHands = playerInventoryManager.currentHandEquipment.darkDefense;
                //�ֿ���:
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
                //�ֿ���:
                playerStatsManager.poisonDefenseAbsorptionHands = 0;
                playerStatsManager.frostDamageAbsorptionHands = 0;
                playerStatsManager.hemorrhageDefenseAbsorptionHands = 0;
                playerStatsManager.curseDefenseAbsorptionHands = 0;
            }
        }

        //���ö�����ײ:
        public void OpenBlockingCollider()
        {
            if (inputHandler.twoHandFlag)
            {
                blockingCollider.SetColliderDamageAbsorption(playerInventoryManager.rightWeapon);
            }
            //���������ǿ�:(��˫����)
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

        //���ö�����ײ:
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

            //ж��ͷ��
            helmetsModelChanger.UnequipAllHelmetModels();
            //ж�ط���:
            helmetsModelChanger.UnequipHairstyleModels();
            //����ͷ��(ͷ��,üë,����,ͷ):
            helmetsModelChanger.EquipHeadModelById(bodyModelData.headId, bodyModelData.hairstyle , bodyModelData.facialHairId , bodyModelData.eyebrow, bodyModelData.isMale);

            playerStatsManager.physicalDamageAbsorptionHead = 0;
            playerStatsManager.fireDamageAbsorptionHead = 0;
            playerStatsManager.magicDamageAbsorptionHead = 0;
            playerStatsManager.lightningDamageAbsorptionHead = 0;
            playerStatsManager.darkDamageAbsorptionHead = 0;

            //ж���ؼ�:
            torsoModelChanger.UnequipAllTorsoModels();
            torsoModelChanger.LoadNudeModel(bodyModelData.isMale);

            playerStatsManager.physicalDamageAbsorptionBody = 0;
            playerStatsManager.fireDamageAbsorptionBody = 0;
            playerStatsManager.magicDamageAbsorptionBody = 0;
            playerStatsManager.lightningDamageAbsorptionBody = 0;
            playerStatsManager.darkDamageAbsorptionBody = 0;

            //ж���ȼ�:
            hipModelChanger.UnEquipAllHipModels();
            hipModelChanger.LoadNudeModel(bodyModelData.isMale);

            playerStatsManager.physicalDamageAbsorptionLegs = 0;
            playerStatsManager.fireDamageAbsorptionLegs = 0;
            playerStatsManager.magicDamageAbsorptionLegs = 0;
            playerStatsManager.lightningDamageAbsorptionLegs = 0;

            //ж�رۼ�:
            armsModelChanger.UnEquipAllHandModel();
            armsModelChanger.LoadNudeModel(bodyModelData.isMale);

            playerStatsManager.physicalDamageAbsorptionHands = 0;
            playerStatsManager.fireDamageAbsorptionHands = 0;
            playerStatsManager.magicDamageAbsorptionHands = 0;
            playerStatsManager.lightningDamageAbsorptionHands = 0;
            playerStatsManager.darkDamageAbsorptionHands = 0;
        }

        //Ƥ����ë�����沿Ϳѻ��ɫ:
        private void SetCharacterColor()
        {
            //ë��
            helmetsModelChanger.SetHairModelColor(bodyModelData.attributeHairColor,bodyModelData.hairColor);
            //Ƥ��
            helmetsModelChanger.SetHeadModelColor(bodyModelData.attributeSkinColor, bodyModelData.skinColor);
            torsoModelChanger.SetTorsoModelColor(bodyModelData.attributeSkinColor, bodyModelData.skinColor);
            hipModelChanger.SetLegModelColor(bodyModelData.attributeSkinColor, bodyModelData.skinColor);
            armsModelChanger.SetHandModelColor(bodyModelData.attributeSkinColor, bodyModelData.skinColor);
            //�沿Ϳѻ��ɫ:
            helmetsModelChanger.SetfacialMarkColorl(bodyModelData.attributeFacialMarkColor, bodyModelData.facialMarkColor);
        }
    }
}