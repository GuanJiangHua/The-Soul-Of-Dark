using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class WorldEventManager : MonoBehaviour
    {
        [Header("玩家相关属性:")]
        public PlayerRegenerationData playerRegenerationData = new PlayerRegenerationData();
        [Header("玩家丢失的灵魂的预制体:")]
        public GameObject droppedSoulPrefab;                    //灵魂丢失的预制体;
        [Header("可复生敌人索引列表:")]
        public EnemyResurrectableManager[] enemyResurrectableManagers;

        [Header("boss与雾门:")]
        public EnemyBossManager currentBossManager;
        public UIBossHealthBar uiBossHealthBar;
        public List<FogWall> fogWalls = new List<FogWall>();
        public EnemyBossManager[] allBossManager;                                                                    //所有boss的"boss管理器"数组
        [HideInInspector] public int[] isBossAefeatedArray;                                                           //已经击败的boss数组

        [Header("篝火数组:")]
        public BonfireLocation[] bonfireLocations;
        [Header("所有的门:")]
        public DoorInteractable[] allDoorArray;
        [HideInInspector] public int[] openDoorArray;
        [Header("所有升降台:")]
        public Elevator[] allElevatorArray;
        [HideInInspector] public int[] openElevatorArray;               //开启的升降机数组
        [Header("所有拾取物品:")]
        public ItemPickUp[] allItemPickUpArray;
        [HideInInspector] public int[] alreadyPickupItemArray;      //已经拾取的物品的数组
        [Header("所有宝箱:")]
        public OpenChest[] allChestArray;
        [HideInInspector] public int[] openChestArray;
        [Header("隐藏墙壁:")]
        public IllusionaryWall[] allIllusionaryWallArray;
        [HideInInspector] public int[] alreadyOpenIllusionaryWallArray;
        [Header("游戏区域:")]
        public GameArea[] gameAreas;
        public static WorldEventManager single;

        private void Awake()
        {
            if(single == null)
            {
                single = this;
            }
            else
            {
                Destroy(gameObject);
            }
            //读取存档:
            PlayerSaveManager.LoadDataToWorldEvent(single);

            WhenEnabledBonfiresManager();                                                       //篝火管理

            //获取可复生的敌人的复生管理脚本:
            enemyResurrectableManagers = FindObjectsOfType<EnemyResurrectableManager>();

            uiBossHealthBar = FindObjectOfType<UIBossHealthBar>();
        }

        private void Start()
        {
            WhenEnabledBossManager();                                                             //boss管理
            WhenEnabledOpenDoorAndMechanism();                                        //门与机关管理
            WhenEnabledPickUpAndTreasureChest();                                         //可拾取物和宝箱
            WhenEnabledIllusionaryWall();                                                          //隐藏墙壁

            //实例化 ,上次死亡的掉落物:
            if (playerRegenerationData.isLostSoulNotFound)
            {
                GameObject droppedSoul = Instantiate(droppedSoulPrefab, playerRegenerationData.soulLossPoint, Quaternion.identity);
                DroppedItem droppedItem = droppedSoul.GetComponent<DroppedItem>();
                //设置遗失的灵魂:
                droppedItem.lostSoul = playerRegenerationData.soulLossAmount;
            }

            //开启提示信息:
            PlayerManager player = FindObjectOfType<PlayerManager>();
            if (player.previousBonfireIndex != -1)
            {
                GameArea nowGameArea = gameAreas[bonfireLocations[player.previousBonfireIndex].gameAreaIndex];
                EmergencyPromptInformationWindow.single.EnablePrompt(nowGameArea.areaName);
            }
            else
            {
                GameArea nowGameArea = gameAreas[0];
                EmergencyPromptInformationWindow.single.EnablePrompt(nowGameArea.areaName);
            }
        }

        #region boss战管理:
        //启用时:[boss管理:]
        private void WhenEnabledBossManager()
        {
            //更新已经击败的boss的数组:
            if (isBossAefeatedArray.Length != allBossManager.Length)
            {
                isBossAefeatedArray = new int[allBossManager.Length];
                for (int i = 0; i < isBossAefeatedArray.Length; i++)
                {
                    isBossAefeatedArray[i] = -1;
                }
            }

            //甄别哪些boss已经被击败：
            //被击败的boss会被禁用:
            //[被击败的boss对应的雾气不可进入](注意:雾门与boss的对应关系体系在索引位置上,布置场景时要有所体现)
            for(int i = 0; i < isBossAefeatedArray.Length; i++)
            {
                switch (isBossAefeatedArray[i])
                {
                    case -1:
                        //未开启[不需要禁用],但不设置为当前boss;
                        break;
                    case 0:
                        //未击败[不需要禁用,要开启雾门]
                        currentBossManager = allBossManager[i];
                        break;
                    case 1:
                        //已击败[需要禁用]
                        allBossManager[i].gameObject.SetActive(false);
                        break;
                }
            }

            //启用雾墙:
            if (currentBossManager != null)
            {
                int currentBossIndex = GetBossIndexInAllBossArray(currentBossManager);
                //启用雾墙;
                for (int i = 0; i < fogWalls.Count; i++)
                {
                    if (currentBossIndex == i)
                    {
                        fogWalls[i].ActivateFogWall(true);
                    }
                    else
                    {
                        fogWalls[i].ActivateFogWall(false);
                    }
                }
            }
        }
        //激活boss战:
        public void ActivateBossFight(EnemyBossManager bossManager)
        {
            currentBossManager = bossManager;

            currentBossManager.bossFightIsActive = true;
            currentBossManager.bossHasBeenAwakened = true;

            uiBossHealthBar.SetUIHealthBarToActive();

            int currentBossIndex = GetBossIndexInAllBossArray(currentBossManager);
            //启用雾墙;
            for(int i=0;i<fogWalls.Count;i++)
            {
                if (currentBossIndex == i)
                {
                    fogWalls[i].ActivateFogWall(true);
                }
                else
                {
                    fogWalls[i].ActivateFogWall(false);
                }
            }

            //设置该boss已被开启:
            isBossAefeatedArray[currentBossIndex] = 0;
        }
        //已经击败boss:
        public void BossHasBenDefeated(EnemyBossManager enemyBossManager)
        {
            enemyBossManager.bossHasBeenDefeated = true;
            enemyBossManager.bossFightIsActive = false;

            uiBossHealthBar.SetHealthBarToInactive();
            //停用雾墙
            foreach(var fogWall in fogWalls)
            {
                fogWall.DeactivateFogWall();
            }

            int index = GetBossIndexInAllBossArray(enemyBossManager);
            isBossAefeatedArray[index] = 1;                                                     //该boss已经被击败
        }

        //获取该boss在数组中的索引位置
        private int GetBossIndexInAllBossArray(EnemyBossManager bossManager)
        {
            for (int index = 0; index < allBossManager.Length; index++)
            {
                if (allBossManager[index] == bossManager)
                {
                    return index;
                }
            }

            return -1;
        }
        #endregion

        #region 篝火管理:
        //当启用时:[对篝火的管理:]
        private void WhenEnabledBonfiresManager()
        {
            //篝火数量不同://(篝火数组为空)//篝火数组数量不等于场上篝火数量
            if (single.playerRegenerationData.LitBonfire == null || single.playerRegenerationData.LitBonfire.Length != bonfireLocations.Length)
            {
                playerRegenerationData.LitBonfire = new int[bonfireLocations.Length];

                for (int i = 0; i < playerRegenerationData.LitBonfire.Length; i++)
                {
                    playerRegenerationData.LitBonfire[i] = -1;
                }
            }
            //篝火状态初始化:
            ExtinguishOtherBonfires(single.playerRegenerationData.LitBonfire);
            //游戏区域初始化:
            GameAreaDivision(gameAreas);
            //更新篝火分类按区域划分:
            BonfiresAreClassifiedIntoAreas(bonfireLocations);
        }
        //获取到篝火的索索引位置:
        public int GetBonfiresIndex(BonfireLocation bonfire)
        {
            for(int i = 0; i < bonfireLocations.Length; i++)
            {
                if(bonfireLocations[i] == bonfire)
                {
                    return i;
                }
            }

            return -1;
        }
        //熄灭其他篝火:
        public void ExtinguishOtherBonfires(BonfireLocation bonfire)
        {
            for(int i = 0; i < bonfireLocations.Length; i++)
            {
                if(bonfireLocations[i] == bonfire)
                {
                    bonfireLocations[i].isLgnite = true;
                    bonfireLocations[i].IsLit(true);
                    playerRegenerationData.LitBonfire[i] = 1;

                }
                else
                {
                    bonfireLocations[i].isLgnite = false;
                    bonfireLocations[i].IsLit(false);

                    if(bonfireLocations[i].isOnceMet == false)
                    {
                        playerRegenerationData.LitBonfire[i] = -1;
                    }
                    else if(bonfireLocations[i].isLgnite == false)
                    {
                        playerRegenerationData.LitBonfire[i] = 0;
                    }
                }
            }

            //检查是否发现新区域:
            CheckForNewAreas(bonfire);
        }
        

        //篝火状态初始化:
        private void ExtinguishOtherBonfires(int[] bonfireArray)
        {
            for (int i = 0; i < bonfireLocations.Length; i++)
            {
                if(bonfireArray[i] == 0 || bonfireArray[i] == -1)
                {
                    //篝火未被点燃:
                    bonfireLocations[i].isLgnite = false;
                    //篝火未被发现:[bonfireArray[i] = -1时未被发现]
                    if (bonfireArray[i] == -1)
                    {
                        bonfireLocations[i].isOnceMet = false;
                    }
                    else
                    {
                        bonfireLocations[i].isOnceMet = true;
                    }
                }
                else if (bonfireArray[i] == 1)
                {
                    bonfireLocations[i].isLgnite = true;
                    bonfireLocations[i].isOnceMet = true;
                }
            }
        }

        //更新游戏区域划分:
        private void GameAreaDivision(GameArea[] gameAreas)
        {
            for(int i = 0; i < gameAreas.Length; i++)
            {
                gameAreas[i].bonfireInArea = new List<BonfireLocation>();
            }
        }
        //篝火分到特定区域[可能有新区域被发现]::
        private void BonfiresAreClassifiedIntoAreas(BonfireLocation[] bonfires)
        {
            if (bonfires == null) return;
            foreach (var bonfire in bonfireLocations)
            {
                for(int i = 0; i < gameAreas.Length; i++)
                {
                    if(bonfire.gameAreaIndex == i)
                    {
                        gameAreas[i].bonfireInArea.Add(bonfire);
                        //如果该篝火是被揭示过的,则该地点也是被揭示过的:
                        if(bonfire.isOnceMet == true)
                        {
                            gameAreas[i].isOnceMet = true;
                        }
                    }
                }
            }
        }
        //检查是否发现新区域:
        private void CheckForNewAreas(BonfireLocation bonfire)
        {
            //只要有一个篝火被发现,则该地区被发现:
            if (bonfire.isOnceMet == true)
            {
                gameAreas[bonfire.gameAreaIndex].isOnceMet = true;
            }
        }
        #endregion

        #region 门与机关管理

        private void WhenEnabledOpenDoorAndMechanism()
        {
            DoorInitialization();
            ElevatorInitialization();
        }
        //返回开启的门的数组:
        public int[] GetOpenDoorAll()
        {
            int[] openDoor = new int[allDoorArray.Length];
            for(int i = 0; i < openDoor.Length; i++)
            {
                if (allDoorArray[i].isAlreadyTurnedOn)
                {
                    openDoor[i] = 1;
                }
                else
                {
                    openDoor[i] = 0;
                }
            }

            return openDoor;
        }
        //门的初始化:
        private void DoorInitialization()
        {
            if (openDoorArray == null || openDoorArray.Length == 0)
            {
                foreach(DoorInteractable door in allDoorArray)
                {
                    door.ThisOpenDoorInitialization(false);
                }
            }
            else
            {
                for(int i = 0; i < allDoorArray.Length; i++)
                {
                    if (openDoorArray[i] == 1)
                        allDoorArray[i].ThisOpenDoorInitialization(true);
                    else
                        allDoorArray[i].ThisOpenDoorInitialization(false);
                }
            }
        }

        //返回开启的升降台数组:0是没开启,1是已经开启
        public int[] GetOpenElevatorArray()
        {
            openElevatorArray = new int[allElevatorArray.Length];
            for(int i = 0; i < openElevatorArray.Length; i++)
            {
                if (allElevatorArray[i].isTop)
                {
                    openElevatorArray[i] = 1;
                }
                else
                {
                    openElevatorArray[i] = 0;
                }
            }

            return openElevatorArray;
        }
        //升降机的初始化
        private void ElevatorInitialization()
        {
            if(openElevatorArray!=null && openElevatorArray.Length > 0)
            {
                for(int i = 0; i < allElevatorArray.Length; i++)
                {
                    if (openElevatorArray[i] == 0)
                        allElevatorArray[i].LiftTableInitialization(false);
                    if (openElevatorArray[i] == 1)
                        allElevatorArray[i].LiftTableInitialization(true);
                }
            }
        }
        #endregion

        #region 拾取物与宝箱
        private void WhenEnabledPickUpAndTreasureChest()
        {
            AllPickupInitialization();      //可拾取物的初始化;
            AllChestInitialization();       //宝箱初始化;
        }

        //返回已经拾取的可拾取物的数组:0:[未拾取],1:[已经拾取]
        public int[] GetAlreadyPickupItemArray()
        {
            int[] pickupItemArray = new int[allItemPickUpArray.Length];
            for(int i = 0; i < pickupItemArray.Length; i++)
            {
                if (allItemPickUpArray[i].isAlreadyPicked)
                    pickupItemArray[i] = 1;
                else
                    pickupItemArray[i] = 0;
            }

            return pickupItemArray;
        }
        //可拾取物的初始化:
        private void AllPickupInitialization()
        {
            if(alreadyPickupItemArray != null && alreadyPickupItemArray.Length > 0)
            {
                for(int i = 0; i < alreadyPickupItemArray.Length; i++)
                {
                    if (alreadyPickupItemArray[i] == 0)
                        allItemPickUpArray[i].PickupInitialization(false);
                    else if(alreadyPickupItemArray[i] == 1)
                        allItemPickUpArray[i].PickupInitialization(true);
                }
            }
        }

        //返回开启的宝箱的数组:
        public int[] GetOpenChestArray()
        {
            int[] chestArray = new int[allChestArray.Length];
            for(int i = 0; i < chestArray.Length; i++)
            {
                if (allChestArray[i].isAlreadyTurnedOn)
                    chestArray[i] = 1;
                else
                    chestArray[i] = 0;
            }

            return chestArray;
        }
        //宝箱的初始化:
        private void AllChestInitialization()
        {
            if(openChestArray != null && openChestArray.Length > 0)
            {
                for(int i = 0; i < openChestArray.Length; i++)
                {
                    if (openChestArray[i] == 0)
                        allChestArray[i].ChestInitialization(false);
                    else if(openChestArray[i] == 1)
                        allChestArray[i].ChestInitialization(true);
                }
            }
        }
        #endregion

        #region 隐藏墙壁
        private void WhenEnabledIllusionaryWall()
        {
            AllIllusionaryWallInitialization();
        }

        //返回已经开启的隐藏墙壁:
        public int[] GetAlreadyOpenIllusionaryWallArray()
        {
            int[] illusionaryWallArray = new int[allIllusionaryWallArray.Length];
            for(int i = 0; i < illusionaryWallArray.Length; i++)
            {
                if (allIllusionaryWallArray[i].isAlreadyTurnedOn)
                    illusionaryWallArray[i] = 1;
                else
                    illusionaryWallArray[i] = 0;
            }

            return illusionaryWallArray;
        }
        //隐藏墙壁初始化:
        private void AllIllusionaryWallInitialization()
        {
            if (alreadyOpenIllusionaryWallArray != null && alreadyOpenIllusionaryWallArray.Length > 0)
            {
                for(int i=0;i< alreadyOpenIllusionaryWallArray.Length; i++)
                {
                    if (alreadyOpenIllusionaryWallArray[i] == 0)
                        allIllusionaryWallArray[i].IllusionaryWallInitialization(false);
                    else if(alreadyOpenIllusionaryWallArray[i] == 1)
                        allIllusionaryWallArray[i].IllusionaryWallInitialization(true);
                }
            }
        }
        #endregion

        //敌人恢复初始状态:
        public void AllEnemyReturnToOriginalState()
        {
            foreach(var resurrectableManager in enemyResurrectableManagers)
            {
                resurrectableManager.ReturnToOriginal();
            }
        }
    }

    //玩家复活数据类:(退出游戏时保存,由主场景读档进入时重新赋值:)

    [System.Serializable]
    public struct GameArea
    {
        public bool isOnceMet;                                          //是否被发现
        public string areaName;                                         //区域名称;
        public Sprite areaIcon;                                            //地区图标;
        public List<BonfireLocation> bonfireInArea;         //区域内篝火;
    }
}
public class PlayerRegenerationData
{
    public static string playerName;
    public bool isLostSoulNotFound;                                //是否丢失的灵魂;
    public Vector3 soulLossPoint;                                     //丢失的灵魂位置;
    public int soulLossAmount;                                        //丢失灵魂量;

    //点燃的篝火:
    public int[] LitBonfire;
}