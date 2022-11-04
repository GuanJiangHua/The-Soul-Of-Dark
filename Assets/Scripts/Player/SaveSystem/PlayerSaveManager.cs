using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerSaveManager : MonoBehaviour
    {
        //存档文件后缀:
        const string PLAYER_DATA_FILE_NAME_SUFFIX = "_player_save_data.cundang";
        
        //存档:
        public static void SaveToFile(PlayerManager player, PlotProgressManager plotProgressManager, WorldEventManager worldEvent = null)
        {
            #region 玩家属性:
            PlayerSaveData playerData = new PlayerSaveData();
            //玩家名称+玩家等级:
            playerData.playerName = player.playerStateManager.characterName;
            playerData.playerLeve = player.playerStateManager.characterLeve;
            //玩家位置:
            playerData.positionX = player.rebirthPosition.x;
            playerData.positionY = player.rebirthPosition.y;
            playerData.positionZ = player.rebirthPosition.z;
            //上一次休息的篝火索引:
            playerData.previousBonfireIndex = player.previousBonfireIndex;

            //玩家裸身模型:
            DodyModelData dodyModelData = player.playerEquipmentManager.bodyModelData;
            playerData.isMale = dodyModelData.isMale;                   //性别
            playerData.headId = dodyModelData.headId;                 //头部
            playerData.hairstyle = dodyModelData.hairstyle;            //发型 
            playerData.facialHairId = dodyModelData.facialHairId;   //胡须
            playerData.eyebrow = dodyModelData.eyebrow;            //眉毛
            //毛发颜色:
            playerData.hairColorR = dodyModelData.hairColor.r;
            playerData.hairColorG = dodyModelData.hairColor.g;
            playerData.hairColorB = dodyModelData.hairColor.b;
            //皮肤颜色:
            playerData.skinColorR = dodyModelData.skinColor.r;
            playerData.skinColorG = dodyModelData.skinColor.g;
            playerData.skinColorB = dodyModelData.skinColor.b;
            //面部涂鸦颜色:
            playerData.facialMarkColorR = dodyModelData.facialMarkColor.r;
            playerData.facialMarkColorG = dodyModelData.facialMarkColor.g;
            playerData.facialMarkColorB = dodyModelData.facialMarkColor.b;

            //玩家属性等级:
            playerData.healthLevel = player.playerStateManager.healthLevel;                       //健康
            playerData.staminaLevel = player.playerStateManager.staminaLevel;                  //体力
            playerData.focusLevel = player.playerStateManager.focusLevel;                          //法力
            playerData.strengthLevel = player.playerStateManager.strengthLevel;                //力量
            playerData.dexterityLevel = player.playerStateManager.dexterityLevel;               //敏捷
            playerData.intelligenceLevel = player.playerStateManager.intelligenceLevel;      //智力
            playerData.faithLevel = player.playerStateManager.faithLevel;                             //信仰
            playerData.soulsRewardLevel = player.playerStateManager.soulsRewardLevel;   //灵魂奖励等级

            //保存基础属性:
            playerData.currentHealth = player.playerStateManager.currentHealth;                     //当前生命值
            playerData.currentFocusPoints = player.playerStateManager.currentFocusPoints;   //当前法力值
            playerData.currentStamina = player.playerStateManager.currentStamina;               //当前耐力值

            //灵魂数量:
            playerData.soulCount = player.playerStateManager.currentSoulCount;
            //玩家元素瓶数量:
            playerData.totalNumberElementBottle = player.totalNumberElementBottle;
            playerData.numberElement = player.numberElement;
            playerData.restoreHealthLevel = player.restoreHealthLevel;
            #endregion

            #region 库存数组获取:
            //当前武器位置索引:
            playerData.currentRightWeaponIndex = player.playerInventoryManager.currentRightWeaponIndex;
            playerData.currentLeftWeaponIndex = player.playerInventoryManager.currentLeftWeaponIndex;
            //当前法术槽位置索引:
            playerData.currentSpellIndex = player.playerInventoryManager.currentSpellIndex;
            //当前消耗品位置索引:
            playerData.currentConsumableIndex = player.playerInventoryManager.currentConsumableIndex;

            //当前左右手装备数组:
            playerData.weaponRightHandSlots = GetCurrentWeaponArray(player.playerInventoryManager, false);
            playerData.weaponLeftHandSlots = GetCurrentWeaponArray(player.playerInventoryManager, true);
            //当前弓箭主弹药槽:
            playerData.currentBowID = GetCurrentAmmoData(player.playerInventoryManager, player.playerInventoryManager.currentBow);
            //当前弓箭备用弹丸槽:
            playerData.spareBowID = GetCurrentAmmoData(player.playerInventoryManager, player.playerInventoryManager.spareBow);
            //当前其他弹丸主弹药槽:
            playerData.otherAmmoID = GetCurrentAmmoData(player.playerInventoryManager, player.playerInventoryManager.otherAmmo);
            //当前其他弹丸备用弹药槽:
            playerData.spareOtherAmmoID = playerData.otherAmmoID = GetCurrentAmmoData(player.playerInventoryManager, player.playerInventoryManager.spareOtherAmmo);

            //武器库存:
            playerData.weaponInventoryIDArray = GetWeaponArray(player.playerInventoryManager);
            //当前头盔:
            playerData.currentHelmetEquipmentID = player.playerInventoryManager.itemManager.IndexInHelmetEquipmentList(player.playerInventoryManager.currentHelmetEquipment);
            //头盔库存:
            playerData.helmetEquipmentInventoryIDArrayArray = GetHelmetEquipmentArray(player.playerInventoryManager);
            //当前头盔:
            playerData.currentTorsoEquipmentID = player.playerInventoryManager.itemManager.IndexInTorsoEquipmentList(player.playerInventoryManager.currentTorsoEquipment);
            //头盔库存:
            playerData.torsoEquipmentInventoryIDArrayArray = GetTorsoEquipmentArray(player.playerInventoryManager);
            //腿甲:
            playerData.currentLegEquipmentID = player.playerInventoryManager.itemManager.IndexInLegEquipmentList(player.playerInventoryManager.currentLegEquipment);
            //腿甲库存:
            playerData.legEquipmentInventoryIDArrayArray = GetLegEquipmentArray(player.playerInventoryManager);
            //臂甲:
            playerData.currentHandEquipmentID = player.playerInventoryManager.itemManager.IndexInHandEquipmentList(player.playerInventoryManager.currentHandEquipment);
            //臂甲库存:
            playerData.handEquipmentInventoryIDArrayArray = GetHandEquipmentArray(player.playerInventoryManager);
            //消耗品库存:
            playerData.consumableInventoryDataArray = GetConsumableInventory(player.playerInventoryManager);

            //当前装备的戒指:
            playerData.currentRingSlots = GetCurrentRingArray(player.playerInventoryManager);
            //戒指库存:
            playerData.ringInventoryIDArray = GetRingInventoryArray(player.playerInventoryManager);

            //当前装备的消耗品:
            playerData.currentConsumableSlots = GetCurrentConsumable(player.playerInventoryManager);
            //消耗品库存:
            playerData.consumableInventoryDataArray = GetConsumableInventory(player.playerInventoryManager);

            //弹药库存:
            playerData.ammoInventoryDataArray = GetRangedAmmoInventoryDataArray(player.playerInventoryManager);

            //当前记忆法术:
            playerData.memorySpellNameArray = GetMemorySpellSlotArray(player.playerInventoryManager);
            //法术库存:
            playerData.spellInventoryNameArray = GetSpellInventoryNameArray(player.playerInventoryManager);
            //...
            #endregion

            #region 世界事件

            playerData.worldEventData = new WorldEventData();

            if (worldEvent != null)
            {
                //是否掉落灵魂:
                if(worldEvent.playerRegenerationData.isLostSoulNotFound == true)
                {
                    playerData.worldEventData.isLostSoulNotFound = 1;
                }
                else
                {
                    playerData.worldEventData.isLostSoulNotFound = 0;
                }

                //灵魂掉落位置:
                playerData.worldEventData.soulLossPointX = worldEvent.playerRegenerationData.soulLossPoint.x;
                playerData.worldEventData.soulLossPointY = worldEvent.playerRegenerationData.soulLossPoint.y;
                playerData.worldEventData.soulLossPointZ = worldEvent.playerRegenerationData.soulLossPoint.z;
                //灵魂掉落量:
                playerData.worldEventData.soulLossAmount = worldEvent.playerRegenerationData.soulLossAmount;
                //点燃的篝火:
                playerData.worldEventData.LitBonfire = worldEvent.playerRegenerationData.LitBonfire;
                //击败的boss:
                playerData.worldEventData.IsBossAefeatedArray = worldEvent.isBossAefeatedArray;
                //开启的门:
                playerData.worldEventData.OpenDoorArray = worldEvent.GetOpenDoorAll();
                //已经拾取物品:
                playerData.worldEventData.AlreadyPickedItemArray = worldEvent.GetAlreadyPickupItemArray();
                //以及开启的宝箱的数组:
                playerData.worldEventData.OpenChestArray = worldEvent.GetOpenChestArray();
                //开启的升降机:
                playerData.worldEventData.OpenElevatorArray = worldEvent.GetOpenElevatorArray();
                //已经开启的隐藏墙壁:
                playerData.worldEventData.OpenIllusionaryWallArray = worldEvent.GetAlreadyOpenIllusionaryWallArray();
            }
            #endregion

            #region 游戏剧情与商店
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
                    //当前剧情名称不等于当前剧情:(说明已经剧情推进,进度归零)
                    if (npc.currentPlotName != npc.currentDialogueAndPlot.plotName)
                    {
                        newNpc.currentPlotProgress = -1;
                    }

                    playerData.npcPlotProgresArray[i] = newNpc;
                    i++;
                }

                //商店:
                playerData.allShopData = plotProgressManager.GetAllStoreData();
            }
            #endregion
            //文件名:
            string fileName = playerData.playerName + PLAYER_DATA_FILE_NAME_SUFFIX;
            //保存到Json:
            SDSaveSystem.SaveSystem.SaveByJson(fileName, playerData);
        }

        //读档:(初始化玩家属性:)
        public static void LoadDataToPlayer(PlayerManager player)
        {
            //存档文件名:
            string fileName = player.playerStateManager.characterName + PLAYER_DATA_FILE_NAME_SUFFIX;

            PlayerSaveData playerData = SDSaveSystem.SaveSystem.LoadFromJson<PlayerSaveData>(fileName);

            if (playerData != null)
            {
                #region 属性等级:
                //玩家名称+玩家等级:
                player.playerStateManager.characterLeve = playerData.playerLeve;
                player.playerStateManager.characterName = playerData.playerName;

                //玩家裸身模型:
                DodyModelData dodyModelD = new DodyModelData();

                dodyModelD.isMale = playerData.isMale;                   //性别
                dodyModelD.headId = playerData.headId;                 //头部
                dodyModelD.hairstyle = playerData.hairstyle;            //发型
                dodyModelD.facialHairId = playerData.facialHairId;   //胡须
                dodyModelD.eyebrow = playerData.eyebrow;           //眉毛

                dodyModelD.hairColor = new Color(playerData.hairColorR,playerData.hairColorG, playerData.hairColorB);
                dodyModelD.skinColor = new Color(playerData.skinColorR, playerData.skinColorG, playerData.skinColorB);
                dodyModelD.facialMarkColor = new Color(playerData.facialMarkColorR, playerData.facialMarkColorG, playerData.facialMarkColorB);

                player.playerEquipmentManager.bodyModelData = dodyModelD;

                //属性:
                player.playerStateManager.healthLevel = playerData.healthLevel;                       //健康
                player.playerStateManager.staminaLevel = playerData.staminaLevel;                 //体力
                player.playerStateManager.focusLevel =playerData.focusLevel;                          //法力
                player.playerStateManager.strengthLevel = playerData.strengthLevel;                 //力量
                player.playerStateManager.dexterityLevel = playerData.dexterityLevel;               //敏捷
                player.playerStateManager.intelligenceLevel  = playerData.intelligenceLevel;      //智力
                player.playerStateManager.faithLevel = playerData.faithLevel;                              //信仰
                player.playerStateManager.soulsRewardLevel = playerData.soulsRewardLevel;   //灵魂奖励等
                player.restoreHealthLevel = playerData.restoreHealthLevel;

                //基础属性:
                player.playerStateManager.currentHealth = playerData.currentHealth;                     //当前生命值
                player.playerStateManager.currentFocusPoints = playerData.currentFocusPoints;   //当前法力值
                player.playerStateManager.currentStamina = playerData.currentStamina;               //当前耐力值

                //灵魂数量:
                player.playerStateManager.currentSoulCount = playerData.soulCount;
                #endregion

                //角色位置:
                player.transform.position = new Vector3(playerData.positionX, playerData.positionY, playerData.positionZ);
                //上一个休息过的篝火的索引:
                player.previousBonfireIndex = playerData.previousBonfireIndex;

                #region 玩家装备:
                //当前武器位置索引:
                player.playerInventoryManager.currentRightWeaponIndex = playerData.currentRightWeaponIndex;
                player.playerInventoryManager.currentLeftWeaponIndex = playerData.currentLeftWeaponIndex;
                player.playerInventoryManager.currentConsumableIndex = playerData.currentConsumableIndex;
                //当前左右手装备数组:
                player.playerInventoryManager.WeaponRightHandSlots = GetWeaponItemsByID(player.playerInventoryManager.itemManager, playerData.weaponRightHandSlots);
                player.playerInventoryManager.WeaponLeftHandSlots = GetWeaponItemsByID(player.playerInventoryManager.itemManager, playerData.weaponLeftHandSlots);
                //主弓箭槽:
                player.playerInventoryManager.currentBow = player.playerInventoryManager.itemManager.GetRangedAmmoItem(playerData.currentBowID);
                //副弓箭槽:
                player.playerInventoryManager.spareBow = player.playerInventoryManager.itemManager.GetRangedAmmoItem(playerData.spareBowID);
                //主其他弹丸槽:
                player.playerInventoryManager.otherAmmo = player.playerInventoryManager.itemManager.GetRangedAmmoItem(playerData.otherAmmoID);
                //副其他弹丸槽:
                player.playerInventoryManager.spareOtherAmmo = player.playerInventoryManager.itemManager.GetRangedAmmoItem(playerData.spareOtherAmmoID);

                //武器库存:
                player.playerInventoryManager.weaponInventory = GetWeaponInventoryByIDArray(player.playerInventoryManager.itemManager, playerData.weaponInventoryIDArray);
                //头盔:
                player.playerInventoryManager.currentHelmetEquipment = player.playerInventoryManager.itemManager.GetHelmetEquipmentByID(playerData.currentHelmetEquipmentID);
                //头盔库存:
                player.playerInventoryManager.headEquipmentInventory = GetHelmetEquipmentsByID(player.playerInventoryManager.itemManager, playerData.helmetEquipmentInventoryIDArrayArray);
                //胸甲:
                player.playerInventoryManager.currentTorsoEquipment = player.playerInventoryManager.itemManager.GetTorsoEquipmentByID(playerData.currentTorsoEquipmentID);
                //胸甲库存:
                player.playerInventoryManager.torsoEquipmentInventory = GetTorsoEquipmentsByID(player.playerInventoryManager.itemManager, playerData.torsoEquipmentInventoryIDArrayArray);
                //腿甲:
                player.playerInventoryManager.currentLegEquipment = player.playerInventoryManager.itemManager.GetLegEquipmentByID(playerData.currentLegEquipmentID);
                //腿甲库存:
                player.playerInventoryManager.legEquipmentInventory = GetLegEquipmentsByID(player.playerInventoryManager.itemManager, playerData.legEquipmentInventoryIDArrayArray);
                //臂甲:
                player.playerInventoryManager.currentHandEquipment = player.playerInventoryManager.itemManager.GetHandEquipmentByID(playerData.currentHandEquipmentID);
                //臂甲库存:
                player.playerInventoryManager.handEquipmentInventory = GetHandEquipmentsByID(player.playerInventoryManager.itemManager, playerData.handEquipmentInventoryIDArrayArray);

                //当前戒指:
                player.playerInventoryManager.SetRingArray(GetCurrentRingItemsByID(player.playerInventoryManager.itemManager, playerData.currentRingSlots));
                //戒指库存:
                player.playerInventoryManager.ringInventory = GetRingItemByID(player.playerInventoryManager.itemManager, playerData.ringInventoryIDArray);
                //当前记忆法术:
                player.playerInventoryManager.memorySpellArray = GetMemorySpellArrayByNameArray(player.playerInventoryManager.itemManager, playerData.memorySpellNameArray);
                //获取法术库存:
                player.playerInventoryManager.spellleInventory = GetSpellInventoryByNameArray(player.playerInventoryManager.itemManager, playerData.spellInventoryNameArray);
                //当前消耗品:
                player.playerInventoryManager.consumableSlots = GetCurrentConsumableById(player.playerInventoryManager.itemManager, playerData.currentConsumableSlots);
                //消耗品库存:
                player.playerInventoryManager.consumableInventory = GetConsumableByDataArray(player.playerInventoryManager.itemManager, playerData.consumableInventoryDataArray);
                //弹药库存:
                player.playerInventoryManager.rangedAmmoInventory = GetRangedAmmoInventoryByDataArray(player.playerInventoryManager.itemManager, playerData.ammoInventoryDataArray);
                #endregion
            }
        }

        //读档:(世界事件)
        public static void LoadDataToWorldEvent(WorldEventManager worldEventManager)
        {
            //存档文件名:
            string fileName = PlayerRegenerationData.playerName + PLAYER_DATA_FILE_NAME_SUFFIX;

            PlayerSaveData playerData = SDSaveSystem.SaveSystem.LoadFromJson<PlayerSaveData>(fileName);
            //是否掉落灵魂:
            if(playerData.worldEventData.isLostSoulNotFound == 1)
            {
                worldEventManager.playerRegenerationData.isLostSoulNotFound = true;
            }
            else if(playerData.worldEventData.isLostSoulNotFound == 0)
            {
                worldEventManager.playerRegenerationData.isLostSoulNotFound = false;
            }
            //掉落的灵魂的位置
            worldEventManager.playerRegenerationData.soulLossPoint = new Vector3(playerData.worldEventData.soulLossPointX, playerData.worldEventData.soulLossPointY, playerData.worldEventData.soulLossPointZ);
            //掉落的灵魂量:
            worldEventManager.playerRegenerationData.soulLossAmount = playerData.worldEventData.soulLossAmount;

            //点燃的篝火:
            worldEventManager.playerRegenerationData.LitBonfire = playerData.worldEventData.LitBonfire;
            //击败的boss:
            worldEventManager.isBossAefeatedArray = playerData.worldEventData.IsBossAefeatedArray;
            //开启的门:
            worldEventManager.openDoorArray = playerData.worldEventData.OpenDoorArray;
            //开启的升降机:
            worldEventManager.openElevatorArray = playerData.worldEventData.OpenElevatorArray;
            //已经拾取的物品:
            worldEventManager.alreadyPickupItemArray = playerData.worldEventData.AlreadyPickedItemArray;
            //已经开启的宝箱:
            worldEventManager.openChestArray = playerData.worldEventData.OpenChestArray;
            //已经开启的隐藏墙壁:
            worldEventManager.alreadyOpenIllusionaryWallArray = playerData.worldEventData.OpenIllusionaryWallArray;
        }

        //读档:(npc剧情进度,与所有商店)
        public static void LoadLoadDataToPlotProgress(PlotProgressManager plotProgressManager)
        {
            //存档文件名:
            string fileName = PlayerRegenerationData.playerName + PLAYER_DATA_FILE_NAME_SUFFIX;

            PlayerSaveData playerData = SDSaveSystem.SaveSystem.LoadFromJson<PlayerSaveData>(fileName);

            plotProgressManager.allNpcPlotProgressArray = playerData.npcPlotProgresArray;
            plotProgressManager.allShopDataArray = playerData.allShopData;
            plotProgressManager.InitializationAllStore(playerData.allShopData);
        }

        //删除存档:
        public static void DeletePlayerSaveFile(string playerFileName)
        {
            string fileName = playerFileName + PLAYER_DATA_FILE_NAME_SUFFIX;
            Debug.Log("要删除的存档的名称:" + fileName);
            SDSaveSystem.SaveSystem.DeleteSaveFile(fileName);
        }

        #region 获取物品ID数组:
        //获取到装备中的左右手武器的索引数组:
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
        //获取装备的戒指数组:
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
        //获取记忆法术槽中的法术名称:
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
        //获取当前装备消耗品的数据数组:
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
        //获取当前弹药数据:
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

        //--------------库存------------------
        //获取到武器库存的索引数组:
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
        //获取到装备数组的索引数组:
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
        //获取胸甲装备的索引数组:
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
        //获取腿甲装备的索引数组:
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
        //获取臂甲装备的索引数组:
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
        //获取戒指库存
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
        //获取法术库存:
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
        //获取消耗品库存的数据数组:
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
        //获取弹药库存的数据数组:
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

        #region 获取物品列表:
        //获取武器数组:
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
        //获取当前戒指数组
        private static RingItem[] GetCurrentRingItemsByID(ItemManager itemManager, int[] indexArray)
        {
            RingItem[] currentRing = { null, null, null, null };
            for(int i = 0; i < currentRing.Length; i++)
            {
                currentRing[i] = itemManager.GetRingItemByID(indexArray[i]);
            }
            return currentRing;
        }
        //获取当前消耗品数组:
        private static ConsumableItem[] GetCurrentConsumableById(ItemManager itemManager, ConsumableItemData[] indexArray)
        {
            ConsumableItem[] consumableSlots = new ConsumableItem[10];
            for (int i = 0; i < consumableSlots.Length; i++)
            {
                consumableSlots[i] = itemManager.GetConsumableItem(indexArray[i]);
            }

            return consumableSlots;
        }

        //----------------库存---------------------------

        // 获取头盔库存的索引数组:

        //获取头盔库存数组:
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
        //获取头盔的索引数组:

        //获取头盔数组:
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
        //获取腿甲的索引数组:

        //获取腿甲数组:
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
        //获取臂甲的索引数组:

        //获取臂甲库存数组:
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
        //获取戒指的索引数组:

        //获取戒指库存数组:
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
        //当前记忆的法术:
        private static SpellItem[] GetMemorySpellArrayByNameArray(ItemManager itemManager, string[] nameArray)
        {
            SpellItem[] spellArray = new SpellItem[nameArray.Length];
            for(int i = 0; i < spellArray.Length; i++)
            {
                spellArray[i] = itemManager.GetSpellItemByName(nameArray[i]);
            }

            return spellArray;
        }
        //法术库存:
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
        //消耗品库存:
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
        //弹丸库存:
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