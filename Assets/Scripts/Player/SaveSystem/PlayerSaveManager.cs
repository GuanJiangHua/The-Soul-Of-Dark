using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerSaveManager : MonoBehaviour
    {
        //�浵�ļ���׺:
        const string PLAYER_DATA_FILE_NAME_SUFFIX = "_player_save_data.cundang";
        
        //�浵:
        public static void SaveToFile(PlayerManager player, PlotProgressManager plotProgressManager, WorldEventManager worldEvent = null)
        {
            #region �������:
            PlayerSaveData playerData = new PlayerSaveData();
            //�������+��ҵȼ�:
            playerData.playerName = player.playerStateManager.characterName;
            playerData.playerLeve = player.playerStateManager.characterLeve;
            //���λ��:
            playerData.positionX = player.rebirthPosition.x;
            playerData.positionY = player.rebirthPosition.y;
            playerData.positionZ = player.rebirthPosition.z;
            //��һ����Ϣ����������:
            playerData.previousBonfireIndex = player.previousBonfireIndex;

            //�������ģ��:
            DodyModelData dodyModelData = player.playerEquipmentManager.bodyModelData;
            playerData.isMale = dodyModelData.isMale;                   //�Ա�
            playerData.headId = dodyModelData.headId;                 //ͷ��
            playerData.hairstyle = dodyModelData.hairstyle;            //���� 
            playerData.facialHairId = dodyModelData.facialHairId;   //����
            playerData.eyebrow = dodyModelData.eyebrow;            //üë
            //ë����ɫ:
            playerData.hairColorR = dodyModelData.hairColor.r;
            playerData.hairColorG = dodyModelData.hairColor.g;
            playerData.hairColorB = dodyModelData.hairColor.b;
            //Ƥ����ɫ:
            playerData.skinColorR = dodyModelData.skinColor.r;
            playerData.skinColorG = dodyModelData.skinColor.g;
            playerData.skinColorB = dodyModelData.skinColor.b;
            //�沿Ϳѻ��ɫ:
            playerData.facialMarkColorR = dodyModelData.facialMarkColor.r;
            playerData.facialMarkColorG = dodyModelData.facialMarkColor.g;
            playerData.facialMarkColorB = dodyModelData.facialMarkColor.b;

            //������Եȼ�:
            playerData.healthLevel = player.playerStateManager.healthLevel;                       //����
            playerData.staminaLevel = player.playerStateManager.staminaLevel;                  //����
            playerData.focusLevel = player.playerStateManager.focusLevel;                          //����
            playerData.strengthLevel = player.playerStateManager.strengthLevel;                //����
            playerData.dexterityLevel = player.playerStateManager.dexterityLevel;               //����
            playerData.intelligenceLevel = player.playerStateManager.intelligenceLevel;      //����
            playerData.faithLevel = player.playerStateManager.faithLevel;                             //����
            playerData.soulsRewardLevel = player.playerStateManager.soulsRewardLevel;   //��꽱���ȼ�

            //�����������:
            playerData.currentHealth = player.playerStateManager.currentHealth;                     //��ǰ����ֵ
            playerData.currentFocusPoints = player.playerStateManager.currentFocusPoints;   //��ǰ����ֵ
            playerData.currentStamina = player.playerStateManager.currentStamina;               //��ǰ����ֵ

            //�������:
            playerData.soulCount = player.playerStateManager.currentSoulCount;
            //���Ԫ��ƿ����:
            playerData.totalNumberElementBottle = player.totalNumberElementBottle;
            playerData.numberElement = player.numberElement;
            playerData.restoreHealthLevel = player.restoreHealthLevel;
            #endregion

            #region ��������ȡ:
            //��ǰ����λ������:
            playerData.currentRightWeaponIndex = player.playerInventoryManager.currentRightWeaponIndex;
            playerData.currentLeftWeaponIndex = player.playerInventoryManager.currentLeftWeaponIndex;
            //��ǰ������λ������:
            playerData.currentSpellIndex = player.playerInventoryManager.currentSpellIndex;
            //��ǰ����Ʒλ������:
            playerData.currentConsumableIndex = player.playerInventoryManager.currentConsumableIndex;

            //��ǰ������װ������:
            playerData.weaponRightHandSlots = GetCurrentWeaponArray(player.playerInventoryManager, false);
            playerData.weaponLeftHandSlots = GetCurrentWeaponArray(player.playerInventoryManager, true);
            //��ǰ��������ҩ��:
            playerData.currentBowID = GetCurrentAmmoData(player.playerInventoryManager, player.playerInventoryManager.currentBow);
            //��ǰ�������õ����:
            playerData.spareBowID = GetCurrentAmmoData(player.playerInventoryManager, player.playerInventoryManager.spareBow);
            //��ǰ������������ҩ��:
            playerData.otherAmmoID = GetCurrentAmmoData(player.playerInventoryManager, player.playerInventoryManager.otherAmmo);
            //��ǰ�������豸�õ�ҩ��:
            playerData.spareOtherAmmoID = playerData.otherAmmoID = GetCurrentAmmoData(player.playerInventoryManager, player.playerInventoryManager.spareOtherAmmo);

            //�������:
            playerData.weaponInventoryIDArray = GetWeaponArray(player.playerInventoryManager);
            //��ǰͷ��:
            playerData.currentHelmetEquipmentID = player.playerInventoryManager.itemManager.IndexInHelmetEquipmentList(player.playerInventoryManager.currentHelmetEquipment);
            //ͷ�����:
            playerData.helmetEquipmentInventoryIDArrayArray = GetHelmetEquipmentArray(player.playerInventoryManager);
            //��ǰͷ��:
            playerData.currentTorsoEquipmentID = player.playerInventoryManager.itemManager.IndexInTorsoEquipmentList(player.playerInventoryManager.currentTorsoEquipment);
            //ͷ�����:
            playerData.torsoEquipmentInventoryIDArrayArray = GetTorsoEquipmentArray(player.playerInventoryManager);
            //�ȼ�:
            playerData.currentLegEquipmentID = player.playerInventoryManager.itemManager.IndexInLegEquipmentList(player.playerInventoryManager.currentLegEquipment);
            //�ȼ׿��:
            playerData.legEquipmentInventoryIDArrayArray = GetLegEquipmentArray(player.playerInventoryManager);
            //�ۼ�:
            playerData.currentHandEquipmentID = player.playerInventoryManager.itemManager.IndexInHandEquipmentList(player.playerInventoryManager.currentHandEquipment);
            //�ۼ׿��:
            playerData.handEquipmentInventoryIDArrayArray = GetHandEquipmentArray(player.playerInventoryManager);
            //����Ʒ���:
            playerData.consumableInventoryDataArray = GetConsumableInventory(player.playerInventoryManager);

            //��ǰװ���Ľ�ָ:
            playerData.currentRingSlots = GetCurrentRingArray(player.playerInventoryManager);
            //��ָ���:
            playerData.ringInventoryIDArray = GetRingInventoryArray(player.playerInventoryManager);

            //��ǰװ��������Ʒ:
            playerData.currentConsumableSlots = GetCurrentConsumable(player.playerInventoryManager);
            //����Ʒ���:
            playerData.consumableInventoryDataArray = GetConsumableInventory(player.playerInventoryManager);

            //��ҩ���:
            playerData.ammoInventoryDataArray = GetRangedAmmoInventoryDataArray(player.playerInventoryManager);

            //��ǰ���䷨��:
            playerData.memorySpellNameArray = GetMemorySpellSlotArray(player.playerInventoryManager);
            //�������:
            playerData.spellInventoryNameArray = GetSpellInventoryNameArray(player.playerInventoryManager);
            //...
            #endregion

            #region �����¼�

            playerData.worldEventData = new WorldEventData();

            if (worldEvent != null)
            {
                //�Ƿ�������:
                if(worldEvent.playerRegenerationData.isLostSoulNotFound == true)
                {
                    playerData.worldEventData.isLostSoulNotFound = 1;
                }
                else
                {
                    playerData.worldEventData.isLostSoulNotFound = 0;
                }

                //������λ��:
                playerData.worldEventData.soulLossPointX = worldEvent.playerRegenerationData.soulLossPoint.x;
                playerData.worldEventData.soulLossPointY = worldEvent.playerRegenerationData.soulLossPoint.y;
                playerData.worldEventData.soulLossPointZ = worldEvent.playerRegenerationData.soulLossPoint.z;
                //��������:
                playerData.worldEventData.soulLossAmount = worldEvent.playerRegenerationData.soulLossAmount;
                //��ȼ������:
                playerData.worldEventData.LitBonfire = worldEvent.playerRegenerationData.LitBonfire;
                //���ܵ�boss:
                playerData.worldEventData.IsBossAefeatedArray = worldEvent.isBossAefeatedArray;
                //��������:
                playerData.worldEventData.OpenDoorArray = worldEvent.GetOpenDoorAll();
                //�Ѿ�ʰȡ��Ʒ:
                playerData.worldEventData.AlreadyPickedItemArray = worldEvent.GetAlreadyPickupItemArray();
                //�Լ������ı��������:
                playerData.worldEventData.OpenChestArray = worldEvent.GetOpenChestArray();
                //������������:
                playerData.worldEventData.OpenElevatorArray = worldEvent.GetOpenElevatorArray();
                //�Ѿ�����������ǽ��:
                playerData.worldEventData.OpenIllusionaryWallArray = worldEvent.GetAlreadyOpenIllusionaryWallArray();
            }
            #endregion

            #region ��Ϸ�������̵�
            playerData.mainlineProgress = 0;
            playerData.npcPlotProgresArray = null;
            if (plotProgressManager != null)
            {
                playerData.mainlineProgress = plotProgressManager.mainlineProgress;
                playerData.npcPlotProgresArray = new NpcPlotProgress[plotProgressManager.allNPC.Values.Count];

                int i = 0;
                foreach (NpcManager npc in plotProgressManager.allNPC.Values)
                {
                    NpcPlotProgress newNpc = new NpcPlotProgress();
                    newNpc.npcName = npc.npcName;
                    newNpc.isDeath = npc.enemyManager.isDeath;
                    newNpc.isHostile = npc.favorability < 0;
                    newNpc.currentPlotName = npc.currentPlotName;
                    if (newNpc.currentPlotName == null || newNpc.currentPlotName == "") 
                    {
                        newNpc.currentPlotName = plotProgressManager.GetNpcInitialPlotName(npc.npcName);
                    }

                    Debug.Log(npc.npcName + "  " + npc.currentPlotName);
                    newNpc.currentPlotProgress = npc.currentDialogueAndPlot.dialogStatus;
                    //��ǰ�������Ʋ����ڵ�ǰ����:(˵���Ѿ������ƽ�,���ȹ���)
                    if (npc.currentPlotName != npc.currentDialogueAndPlot.plotName)
                    {
                        newNpc.currentPlotProgress = -1;
                    }

                    playerData.npcPlotProgresArray[i] = newNpc;
                    i++;
                }

                //�̵�:
                playerData.allShopData = plotProgressManager.GetAllStoreData();
            }
            #endregion
            //�ļ���:
            string fileName = playerData.playerName + PLAYER_DATA_FILE_NAME_SUFFIX;
            //���浽Json:
            SDSaveSystem.SaveSystem.SaveByJson(fileName, playerData);
        }

        //����:(��ʼ���������:)
        public static void LoadDataToPlayer(PlayerManager player)
        {
            //�浵�ļ���:
            string fileName = player.playerStateManager.characterName + PLAYER_DATA_FILE_NAME_SUFFIX;

            PlayerSaveData playerData = SDSaveSystem.SaveSystem.LoadFromJson<PlayerSaveData>(fileName);

            if (playerData != null)
            {
                #region ���Եȼ�:
                //�������+��ҵȼ�:
                player.playerStateManager.characterLeve = playerData.playerLeve;
                player.playerStateManager.characterName = playerData.playerName;

                //�������ģ��:
                DodyModelData dodyModelD = new DodyModelData();

                dodyModelD.isMale = playerData.isMale;                   //�Ա�
                dodyModelD.headId = playerData.headId;                 //ͷ��
                dodyModelD.hairstyle = playerData.hairstyle;            //����
                dodyModelD.facialHairId = playerData.facialHairId;   //����
                dodyModelD.eyebrow = playerData.eyebrow;           //üë

                dodyModelD.hairColor = new Color(playerData.hairColorR,playerData.hairColorG, playerData.hairColorB);
                dodyModelD.skinColor = new Color(playerData.skinColorR, playerData.skinColorG, playerData.skinColorB);
                dodyModelD.facialMarkColor = new Color(playerData.facialMarkColorR, playerData.facialMarkColorG, playerData.facialMarkColorB);

                player.playerEquipmentManager.bodyModelData = dodyModelD;

                //����:
                player.playerStateManager.healthLevel = playerData.healthLevel;                       //����
                player.playerStateManager.staminaLevel = playerData.staminaLevel;                 //����
                player.playerStateManager.focusLevel =playerData.focusLevel;                          //����
                player.playerStateManager.strengthLevel = playerData.strengthLevel;                 //����
                player.playerStateManager.dexterityLevel = playerData.dexterityLevel;               //����
                player.playerStateManager.intelligenceLevel  = playerData.intelligenceLevel;      //����
                player.playerStateManager.faithLevel = playerData.faithLevel;                              //����
                player.playerStateManager.soulsRewardLevel = playerData.soulsRewardLevel;   //��꽱����
                player.restoreHealthLevel = playerData.restoreHealthLevel;

                //��������:
                player.playerStateManager.currentHealth = playerData.currentHealth;                     //��ǰ����ֵ
                player.playerStateManager.currentFocusPoints = playerData.currentFocusPoints;   //��ǰ����ֵ
                player.playerStateManager.currentStamina = playerData.currentStamina;               //��ǰ����ֵ

                //�������:
                player.playerStateManager.currentSoulCount = playerData.soulCount;
                #endregion

                //��ɫλ��:
                player.transform.position = new Vector3(playerData.positionX, playerData.positionY, playerData.positionZ);
                //��һ����Ϣ�������������:
                player.previousBonfireIndex = playerData.previousBonfireIndex;

                #region ���װ��:
                //��ǰ����λ������:
                player.playerInventoryManager.currentRightWeaponIndex = playerData.currentRightWeaponIndex;
                player.playerInventoryManager.currentLeftWeaponIndex = playerData.currentLeftWeaponIndex;
                player.playerInventoryManager.currentConsumableIndex = playerData.currentConsumableIndex;
                //��ǰ������װ������:
                player.playerInventoryManager.WeaponRightHandSlots = GetWeaponItemsByID(player.playerInventoryManager.itemManager, playerData.weaponRightHandSlots);
                player.playerInventoryManager.WeaponLeftHandSlots = GetWeaponItemsByID(player.playerInventoryManager.itemManager, playerData.weaponLeftHandSlots);
                //��������:
                player.playerInventoryManager.currentBow = player.playerInventoryManager.itemManager.GetRangedAmmoItem(playerData.currentBowID);
                //��������:
                player.playerInventoryManager.spareBow = player.playerInventoryManager.itemManager.GetRangedAmmoItem(playerData.spareBowID);
                //�����������:
                player.playerInventoryManager.otherAmmo = player.playerInventoryManager.itemManager.GetRangedAmmoItem(playerData.otherAmmoID);
                //�����������:
                player.playerInventoryManager.spareOtherAmmo = player.playerInventoryManager.itemManager.GetRangedAmmoItem(playerData.spareOtherAmmoID);

                //�������:
                player.playerInventoryManager.weaponInventory = GetWeaponInventoryByIDArray(player.playerInventoryManager.itemManager, playerData.weaponInventoryIDArray);
                //ͷ��:
                player.playerInventoryManager.currentHelmetEquipment = player.playerInventoryManager.itemManager.GetHelmetEquipmentByID(playerData.currentHelmetEquipmentID);
                //ͷ�����:
                player.playerInventoryManager.headEquipmentInventory = GetHelmetEquipmentsByID(player.playerInventoryManager.itemManager, playerData.helmetEquipmentInventoryIDArrayArray);
                //�ؼ�:
                player.playerInventoryManager.currentTorsoEquipment = player.playerInventoryManager.itemManager.GetTorsoEquipmentByID(playerData.currentTorsoEquipmentID);
                //�ؼ׿��:
                player.playerInventoryManager.torsoEquipmentInventory = GetTorsoEquipmentsByID(player.playerInventoryManager.itemManager, playerData.torsoEquipmentInventoryIDArrayArray);
                //�ȼ�:
                player.playerInventoryManager.currentLegEquipment = player.playerInventoryManager.itemManager.GetLegEquipmentByID(playerData.currentLegEquipmentID);
                //�ȼ׿��:
                player.playerInventoryManager.legEquipmentInventory = GetLegEquipmentsByID(player.playerInventoryManager.itemManager, playerData.legEquipmentInventoryIDArrayArray);
                //�ۼ�:
                player.playerInventoryManager.currentHandEquipment = player.playerInventoryManager.itemManager.GetHandEquipmentByID(playerData.currentHandEquipmentID);
                //�ۼ׿��:
                player.playerInventoryManager.handEquipmentInventory = GetHandEquipmentsByID(player.playerInventoryManager.itemManager, playerData.handEquipmentInventoryIDArrayArray);

                //��ǰ��ָ:
                player.playerInventoryManager.SetRingArray(GetCurrentRingItemsByID(player.playerInventoryManager.itemManager, playerData.currentRingSlots));
                //��ָ���:
                player.playerInventoryManager.ringInventory = GetRingItemByID(player.playerInventoryManager.itemManager, playerData.ringInventoryIDArray);
                //��ǰ���䷨��:
                player.playerInventoryManager.memorySpellArray = GetMemorySpellArrayByNameArray(player.playerInventoryManager.itemManager, playerData.memorySpellNameArray);
                //��ȡ�������:
                player.playerInventoryManager.spellleInventory = GetSpellInventoryByNameArray(player.playerInventoryManager.itemManager, playerData.spellInventoryNameArray);
                //��ǰ����Ʒ:
                player.playerInventoryManager.consumableSlots = GetCurrentConsumableById(player.playerInventoryManager.itemManager, playerData.currentConsumableSlots);
                //����Ʒ���:
                player.playerInventoryManager.consumableInventory = GetConsumableByDataArray(player.playerInventoryManager.itemManager, playerData.consumableInventoryDataArray);
                //��ҩ���:
                player.playerInventoryManager.rangedAmmoInventory = GetRangedAmmoInventoryByDataArray(player.playerInventoryManager.itemManager, playerData.ammoInventoryDataArray);
                #endregion
            }
        }

        //����:(�����¼�)
        public static void LoadDataToWorldEvent(WorldEventManager worldEventManager)
        {
            //�浵�ļ���:
            string fileName = PlayerRegenerationData.playerName + PLAYER_DATA_FILE_NAME_SUFFIX;

            PlayerSaveData playerData = SDSaveSystem.SaveSystem.LoadFromJson<PlayerSaveData>(fileName);
            //�Ƿ�������:
            if(playerData.worldEventData.isLostSoulNotFound == 1)
            {
                worldEventManager.playerRegenerationData.isLostSoulNotFound = true;
            }
            else if(playerData.worldEventData.isLostSoulNotFound == 0)
            {
                worldEventManager.playerRegenerationData.isLostSoulNotFound = false;
            }
            //���������λ��
            worldEventManager.playerRegenerationData.soulLossPoint = new Vector3(playerData.worldEventData.soulLossPointX, playerData.worldEventData.soulLossPointY, playerData.worldEventData.soulLossPointZ);
            //����������:
            worldEventManager.playerRegenerationData.soulLossAmount = playerData.worldEventData.soulLossAmount;

            //��ȼ������:
            worldEventManager.playerRegenerationData.LitBonfire = playerData.worldEventData.LitBonfire;
            //���ܵ�boss:
            worldEventManager.isBossAefeatedArray = playerData.worldEventData.IsBossAefeatedArray;
            //��������:
            worldEventManager.openDoorArray = playerData.worldEventData.OpenDoorArray;
            //������������:
            worldEventManager.openElevatorArray = playerData.worldEventData.OpenElevatorArray;
            //�Ѿ�ʰȡ����Ʒ:
            worldEventManager.alreadyPickupItemArray = playerData.worldEventData.AlreadyPickedItemArray;
            //�Ѿ������ı���:
            worldEventManager.openChestArray = playerData.worldEventData.OpenChestArray;
            //�Ѿ�����������ǽ��:
            worldEventManager.alreadyOpenIllusionaryWallArray = playerData.worldEventData.OpenIllusionaryWallArray;
        }

        //����:(npc�������,�������̵�)
        public static void LoadLoadDataToPlotProgress(PlotProgressManager plotProgressManager)
        {
            //�浵�ļ���:
            string fileName = PlayerRegenerationData.playerName + PLAYER_DATA_FILE_NAME_SUFFIX;

            PlayerSaveData playerData = SDSaveSystem.SaveSystem.LoadFromJson<PlayerSaveData>(fileName);

            plotProgressManager.allNpcPlotProgressArray = playerData.npcPlotProgresArray;
            plotProgressManager.allShopDataArray = playerData.allShopData;
            plotProgressManager.InitializationAllStore(playerData.allShopData);
        }

        //ɾ���浵:
        public static void DeletePlayerSaveFile(string playerFileName)
        {
            string fileName = playerFileName + PLAYER_DATA_FILE_NAME_SUFFIX;
            Debug.Log("Ҫɾ���Ĵ浵������:" + fileName);
            SDSaveSystem.SaveSystem.DeleteSaveFile(fileName);
        }

        #region ��ȡ��ƷID����:
        //��ȡ��װ���е���������������������:
        private static int[] GetCurrentWeaponArray(PlayerInventoryManager playerInventory , bool isLeft)
        {
            if (isLeft)
            {
                int[] leftWeapons = { -1, -1, -1 };
                ItemManager itemManager = playerInventory.itemManager;
                for(int i = 0; i < 3; i++)
                {
                    
                    if (playerInventory.WeaponRightHandSlots[i] != null)
                    {
                        leftWeapons[i] = itemManager.IndexInWeaponList(playerInventory.WeaponLeftHandSlots[i]);
                    }
                    else
                    {
                        leftWeapons[i] = itemManager.IndexInWeaponList(playerInventory.unarmedWeapon);
                    }
                }

                return leftWeapons;
            }
            else
            {
                int[] rightWeapons = { -1, -1, -1 };
                ItemManager itemManager = playerInventory.itemManager;
                for (int i = 0; i < 3; i++)
                {
                    if(playerInventory.WeaponRightHandSlots[i] != null)
                    {
                        Debug.Log(itemManager == null);
                        rightWeapons[i] = itemManager.IndexInWeaponList(playerInventory.WeaponRightHandSlots[i]);
                    }
                    else
                    {
                        rightWeapons[i] = itemManager.IndexInWeaponList(playerInventory.unarmedWeapon);
                    }

                }

                return rightWeapons;
            }
        }      
        //��ȡװ���Ľ�ָ����:
        private static int[] GetCurrentRingArray(PlayerInventoryManager playerInventory)
        {
            int[] rings = { -1, -1, -1, -1 };
            ItemManager itemManager = playerInventory.itemManager;
            for(int i = 0; i < rings.Length; i++)
            {
                rings[i] = itemManager.IndexInRingItemList(playerInventory.GetRingById(i));
            }
            return rings;
        }
        //��ȡ���䷨�����еķ�������:
        private static string[] GetMemorySpellSlotArray(PlayerInventoryManager playerInventory)
        {
            SpellItem[] memorySpellArray = playerInventory.memorySpellArray;
            string[] memorySpellSlotNameArray = new string[memorySpellArray.Length];

            for(int i=0;i< memorySpellSlotNameArray.Length; i++)
            {
                if(memorySpellArray[i] != null)
                {
                    memorySpellSlotNameArray[i] = memorySpellArray[i].itemName;
                }
                else
                {
                    memorySpellSlotNameArray[i] = "";
                }
            }

            return memorySpellSlotNameArray;
        }
        //��ȡ��ǰװ������Ʒ����������:
        private static ConsumableItemData[] GetCurrentConsumable(PlayerInventoryManager playerInventory)
        {
            ConsumableItemData[] consumableItems = new ConsumableItemData[10];
            ConsumableItem[] consumableSlots = playerInventory.consumableSlots;
            ItemManager itemManager = playerInventory.itemManager;

            for (int i = 0; i < consumableItems.Length; i++)
            {
                consumableItems[i] = new ConsumableItemData();
                if (consumableSlots[i] != null)
                {
                    consumableItems[i] = new ConsumableItemData();
                    consumableItems[i].consumableItemName = itemManager.NameInConsumableList(consumableSlots[i]);
                    consumableItems[i].currentItemAmount = consumableSlots[i].currentItemAmount;
                    consumableItems[i].maxItemAmount = consumableSlots[i].maxItemAmount;
                }
                else
                {
                    consumableItems[i].consumableItemName = null;
                }
            }

            return consumableItems;
        }
        //��ȡ��ǰ��ҩ����:
        private static RangedAmmoItemData GetCurrentAmmoData(PlayerInventoryManager playerInventory, RangedAmmoItem ammoItem)
        {
            ItemManager itemManager = playerInventory.itemManager;
            RangedAmmoItemData itemData = new RangedAmmoItemData();
            if (ammoItem != null)
            {
                itemData.rangedAmmoItemID = itemManager.IndexInAmmoItemList(ammoItem);
                itemData.currentAmmoItemAmount = ammoItem.currentAmount;
            }
            else
            {
                itemData.rangedAmmoItemID = -1;
            }
            return itemData;
        }

        //--------------���------------------
        //��ȡ������������������:
        private static int[] GetWeaponArray(PlayerInventoryManager playerInventory)
        {
            int[] weapons = new int[playerInventory.weaponInventory.Count];
            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i] = -1;
            }

            ItemManager itemManager = playerInventory.itemManager;
            for (int i = 0; i < weapons.Length; i++)
            {
                if (playerInventory.weaponInventory[i] != null)
                {
                    weapons[i] = itemManager.IndexInWeaponList(playerInventory.weaponInventory[i]);
                }
            }

            return weapons;
        }
        //��ȡ��װ���������������:
        private static int[] GetHelmetEquipmentArray(PlayerInventoryManager playerInventory)
        {
            int[] helmet = new int[playerInventory.headEquipmentInventory.Count];
            for(int i = 0; i < helmet.Length; i++)
            {
                helmet[i] = -1;
            }

            ItemManager itemManager = playerInventory.itemManager;
            for (int i = 0; i < helmet.Length; i++)
            {
                if (playerInventory.WeaponRightHandSlots[i] != null)
                {
                    helmet[i] = itemManager.IndexInHelmetEquipmentList(playerInventory.headEquipmentInventory[i]);
                }
            }

            return helmet;
        }
        //��ȡ�ؼ�װ������������:
        private static int[] GetTorsoEquipmentArray(PlayerInventoryManager playerInventory)
        {
            int[] torso = new int[playerInventory.torsoEquipmentInventory.Count];
            for (int i = 0; i < torso.Length; i++)
            {
                torso[i] = -1;
            }

            ItemManager itemManager = playerInventory.itemManager;
            for (int i = 0; i < torso.Length; i++)
            {
                if (playerInventory.WeaponRightHandSlots[i] != null)
                {
                    torso[i] = itemManager.IndexInTorsoEquipmentList(playerInventory.torsoEquipmentInventory[i]);
                }
            }

            return torso;
        }
        //��ȡ�ȼ�װ������������:
        private static int[] GetLegEquipmentArray(PlayerInventoryManager playerInventory)
        {
            int[] leg = new int[playerInventory.legEquipmentInventory.Count];
            for (int i = 0; i < leg.Length; i++)
            {
                leg[i] = -1;
            }

            ItemManager itemManager = playerInventory.itemManager;
            for (int i = 0; i < leg.Length; i++)
            {
                if (playerInventory.WeaponRightHandSlots[i] != null)
                {
                    leg[i] = itemManager.IndexInLegEquipmentList(playerInventory.legEquipmentInventory[i]);
                }
            }

            return leg;
        }
        //��ȡ�ۼ�װ������������:
        private static int[] GetHandEquipmentArray(PlayerInventoryManager playerInventory)
        {
            int[] hand = new int[playerInventory.handEquipmentInventory.Count];
            for (int i = 0; i < hand.Length; i++)
            {
                hand[i] = -1;
            }

            ItemManager itemManager = playerInventory.itemManager;
            for (int i = 0; i < hand.Length; i++)
            {
                if (playerInventory.WeaponRightHandSlots[i] != null)
                {
                    hand[i] = itemManager.IndexInHandEquipmentList(playerInventory.handEquipmentInventory[i]);
                }
            }

            return hand;
        }
        //��ȡ��ָ���
        private static int[] GetRingInventoryArray(PlayerInventoryManager playerInventoryManager)
        {
            ItemManager itemManager = playerInventoryManager.itemManager;
            List<RingItem> ringInventory = playerInventoryManager.ringInventory;
            int[] rings = new int[ringInventory.Count];
            for (int i = 0; i < rings.Length; i++)
            {
                rings[i] = -1;
            }
            for (int i = 0; i < rings.Length; i++)
            {
                rings[i] = itemManager.IndexInRingItemList(ringInventory[i]);
            }

            return rings;
        }
        //��ȡ�������:
        private static string[] GetSpellInventoryNameArray(PlayerInventoryManager playerInventory)
        {
            List<SpellItem> spellInventory = playerInventory.spellleInventory;
            string[] spellInventoryNameArray = new string[spellInventory.Count];
            for (int i = 0; i < spellInventory.Count; i++)
            {
                if (spellInventory[i] != null)
                {
                    spellInventoryNameArray[i] = spellInventory[i].itemName;
                }
                else
                {
                    spellInventoryNameArray[i] = "";
                }
            }

            return spellInventoryNameArray;
        }
        //��ȡ����Ʒ������������:
        private static ConsumableItemData[] GetConsumableInventory(PlayerInventoryManager playerInventory)
        {
            ItemManager itemManager = playerInventory.itemManager;
            ConsumableItemData[] consumableItemDatas = new ConsumableItemData[playerInventory.consumableInventory.Count];
            List<ConsumableItem> consumableInventory = playerInventory.consumableInventory;

            for(int i = 0; i < consumableItemDatas.Length; i++)
            {
                consumableItemDatas[i] = new ConsumableItemData();
                consumableItemDatas[i].consumableItemName = itemManager.NameInConsumableList(consumableInventory[i]);
                consumableItemDatas[i].maxItemAmount = consumableInventory[i].maxItemAmount;
                Debug.Log(consumableInventory[i].maxItemAmount +" " + consumableInventory[i].itemName);
                consumableItemDatas[i].currentItemAmount = consumableInventory[i].currentItemAmount;
            }

            return consumableItemDatas;
        }
        //��ȡ��ҩ������������:
        private static RangedAmmoItemData[] GetRangedAmmoInventoryDataArray(PlayerInventoryManager playerInventory)
        {
            ItemManager itemManager = playerInventory.itemManager;
            List<RangedAmmoItem> ammoInventory = playerInventory.rangedAmmoInventory;
            RangedAmmoItemData[] ammoDatas = new RangedAmmoItemData[ammoInventory.Count];
            for(int i = 0; i < ammoDatas.Length; i++)
            {
                RangedAmmoItemData itemData = new RangedAmmoItemData();
                itemData.rangedAmmoItemID = itemManager.IndexInAmmoItemList(ammoInventory[i]);
                itemData.currentAmmoItemAmount = ammoInventory[i].currentAmount;
                ammoDatas[i] = itemData;
            }

            return ammoDatas;
        }
        #endregion

        #region ��ȡ��Ʒ�б�:
        //��ȡ��������:
        private static WeaponItem[] GetWeaponItemsByID(ItemManager itemManager , int[] indexArray)
        {
            if (indexArray == null) 
                return null;

            WeaponItem[] weaponItems = new WeaponItem[indexArray.Length];
            for(int i = 0; i < weaponItems.Length; i++)
            {
                weaponItems[i] = itemManager.GetWeaponItemByID(indexArray[i]);
            }

            return weaponItems;
        }
        //��ȡ��ǰ��ָ����
        private static RingItem[] GetCurrentRingItemsByID(ItemManager itemManager, int[] indexArray)
        {
            RingItem[] currentRing = { null, null, null, null };
            for(int i = 0; i < currentRing.Length; i++)
            {
                currentRing[i] = itemManager.GetRingItemByID(indexArray[i]);
            }
            return currentRing;
        }
        //��ȡ��ǰ����Ʒ����:
        private static ConsumableItem[] GetCurrentConsumableById(ItemManager itemManager, ConsumableItemData[] indexArray)
        {
            ConsumableItem[] consumableSlots = new ConsumableItem[10];
            for (int i = 0; i < consumableSlots.Length; i++)
            {
                consumableSlots[i] = itemManager.GetConsumableItem(indexArray[i]);
            }

            return consumableSlots;
        }

        //----------------���---------------------------

        // ��ȡͷ��������������:

        //��ȡͷ���������:
        private static List<WeaponItem> GetWeaponInventoryByIDArray(ItemManager itemManager, int[] indexArray)
        {
            List<WeaponItem> weaponInventory = new List<WeaponItem>();
            for(int i = 0; i < indexArray.Length; i++)
            {
                WeaponItem weapon = itemManager.GetWeaponItemByID(indexArray[i]);
                weaponInventory.Add(weapon);
            }
            return weaponInventory;
        }
        private static List<HelmetEquipment> GetHelmetEquipmentsByID(ItemManager itemManager, int[] indexArray)
        {
            if (indexArray == null)
            {
                return new List<HelmetEquipment>();
            }


            HelmetEquipment[] helmetEquipments = new HelmetEquipment[indexArray.Length];
            for (int i = 0; i < helmetEquipments.Length; i++)
            {
                helmetEquipments[i] = itemManager.GetHelmetEquipmentByID(indexArray[i]);
            }

            return new List<HelmetEquipment>(helmetEquipments);
        }
        //��ȡͷ������������:

        //��ȡͷ������:
        private static List<TorsoEquipment> GetTorsoEquipmentsByID(ItemManager itemManager, int[] indexArray) 
        {
            if (indexArray == null)
            {
                return new List<TorsoEquipment>();
            }

            TorsoEquipment[] torsoEquipments = new TorsoEquipment[indexArray.Length];
            for (int i = 0; i < torsoEquipments.Length; i++)
            {
                torsoEquipments[i] = itemManager.GetTorsoEquipmentByID(indexArray[i]);
            }

            return new List<TorsoEquipment>(torsoEquipments);
        }
        //��ȡ�ȼ׵���������:

        //��ȡ�ȼ�����:
        private static List<LegEquipment> GetLegEquipmentsByID(ItemManager itemManager, int[] indexArray)
        {
            if (indexArray == null)
            {
                return new List<LegEquipment>();
            }

            LegEquipment[] legEquipments = new LegEquipment[indexArray.Length];
            for (int i = 0; i < legEquipments.Length; i++)
            {
                legEquipments[i] = itemManager.GetLegEquipmentByID(indexArray[i]);
            }

            return new List<LegEquipment>(legEquipments);
        }
        //��ȡ�ۼ׵���������:

        //��ȡ�ۼ׿������:
        private static List<HandEquipment> GetHandEquipmentsByID(ItemManager itemManager, int[] indexArray)
        {
            if (indexArray == null)
            {
                return new List<HandEquipment>();
            }

            HandEquipment[] handEquipments = new HandEquipment[indexArray.Length];
            for (int i = 0; i < handEquipments.Length; i++)
            {
                handEquipments[i] = itemManager.GetHandEquipmentByID(indexArray[i]);
            }

            return new List<HandEquipment>(handEquipments);
        }
        //��ȡ��ָ����������:

        //��ȡ��ָ�������:
        private static List<RingItem> GetRingItemByID(ItemManager itemManager, int[] indexArray)
        {
            if (indexArray == null)
            {
                return new List<RingItem>();
            }

            RingItem[] ringEquipments = new RingItem[indexArray.Length];
            for (int i = 0; i < ringEquipments.Length; i++)
            {
                ringEquipments[i] = itemManager.GetRingItemByID(indexArray[i]);
            }

            return new List<RingItem>(ringEquipments);
        }
        //��ǰ����ķ���:
        private static SpellItem[] GetMemorySpellArrayByNameArray(ItemManager itemManager, string[] nameArray)
        {
            SpellItem[] spellArray = new SpellItem[nameArray.Length];
            for(int i = 0; i < spellArray.Length; i++)
            {
                spellArray[i] = itemManager.GetSpellItemByName(nameArray[i]);
            }

            return spellArray;
        }
        //�������:
        private static List<SpellItem> GetSpellInventoryByNameArray(ItemManager itemManager, string[] nameArray)
        {
            List<SpellItem> spellList = new List<SpellItem>();
            for(int i=0;i< nameArray.Length; i++)
            {
                SpellItem spell = itemManager.GetSpellItemByName(nameArray[i]);
                if (spell != null)
                    spellList.Add(spell);
            }

            return spellList;
        }
        //����Ʒ���:
        private static List<ConsumableItem> GetConsumableByDataArray(ItemManager itemManager , ConsumableItemData[] consumableDatas)
        {
            List<ConsumableItem> consumableInventory = new List<ConsumableItem>();
            for(int i = 0; i < consumableDatas.Length; i++)
            {
                ConsumableItem consumableItem = itemManager.GetConsumableItem(consumableDatas[i]);
                consumableInventory.Add(consumableItem);
            }

            return consumableInventory;
        }
        //������:
        private static List<RangedAmmoItem> GetRangedAmmoInventoryByDataArray(ItemManager itemManager , RangedAmmoItemData[] ammoDataArray)
        {
            List<RangedAmmoItem> ammoInventory = new List<RangedAmmoItem>();
            for(int i = 0; i < ammoDataArray.Length; i++)
            {
                if(ammoDataArray[i].rangedAmmoItemID >= 0)
                {
                    ammoInventory.Add(itemManager.GetRangedAmmoItem(ammoDataArray[i]));
                }
            }

            return ammoInventory;
        }
        #endregion
    }
}