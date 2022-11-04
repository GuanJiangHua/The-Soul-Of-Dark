using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class WorldEventManager : MonoBehaviour
    {
        [Header("����������:")]
        public PlayerRegenerationData playerRegenerationData = new PlayerRegenerationData();
        [Header("��Ҷ�ʧ������Ԥ����:")]
        public GameObject droppedSoulPrefab;                    //��궪ʧ��Ԥ����;
        [Header("�ɸ������������б�:")]
        public EnemyResurrectableManager[] enemyResurrectableManagers;

        [Header("boss������:")]
        public EnemyBossManager currentBossManager;
        public UIBossHealthBar uiBossHealthBar;
        public List<FogWall> fogWalls = new List<FogWall>();
        public EnemyBossManager[] allBossManager;                                                                    //����boss��"boss������"����
        [HideInInspector] public int[] isBossAefeatedArray;                                                           //�Ѿ����ܵ�boss����

        [Header("��������:")]
        public BonfireLocation[] bonfireLocations;
        [Header("���е���:")]
        public DoorInteractable[] allDoorArray;
        [HideInInspector] public int[] openDoorArray;
        [Header("��������̨:")]
        public Elevator[] allElevatorArray;
        [HideInInspector] public int[] openElevatorArray;               //����������������
        [Header("����ʰȡ��Ʒ:")]
        public ItemPickUp[] allItemPickUpArray;
        [HideInInspector] public int[] alreadyPickupItemArray;      //�Ѿ�ʰȡ����Ʒ������
        [Header("���б���:")]
        public OpenChest[] allChestArray;
        [HideInInspector] public int[] openChestArray;
        [Header("����ǽ��:")]
        public IllusionaryWall[] allIllusionaryWallArray;
        [HideInInspector] public int[] alreadyOpenIllusionaryWallArray;
        [Header("��Ϸ����:")]
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
            //��ȡ�浵:
            PlayerSaveManager.LoadDataToWorldEvent(single);

            WhenEnabledBonfiresManager();                                                       //�������

            //��ȡ�ɸ����ĵ��˵ĸ�������ű�:
            enemyResurrectableManagers = FindObjectsOfType<EnemyResurrectableManager>();

            uiBossHealthBar = FindObjectOfType<UIBossHealthBar>();
        }

        private void Start()
        {
            WhenEnabledBossManager();                                                             //boss����
            WhenEnabledOpenDoorAndMechanism();                                        //������ع���
            WhenEnabledPickUpAndTreasureChest();                                         //��ʰȡ��ͱ���
            WhenEnabledIllusionaryWall();                                                          //����ǽ��

            //ʵ���� ,�ϴ������ĵ�����:
            if (playerRegenerationData.isLostSoulNotFound)
            {
                GameObject droppedSoul = Instantiate(droppedSoulPrefab, playerRegenerationData.soulLossPoint, Quaternion.identity);
                DroppedItem droppedItem = droppedSoul.GetComponent<DroppedItem>();
                //������ʧ�����:
                droppedItem.lostSoul = playerRegenerationData.soulLossAmount;
            }

            //������ʾ��Ϣ:
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

        #region bossս����:
        //����ʱ:[boss����:]
        private void WhenEnabledBossManager()
        {
            //�����Ѿ����ܵ�boss������:
            if (isBossAefeatedArray.Length != allBossManager.Length)
            {
                isBossAefeatedArray = new int[allBossManager.Length];
                for (int i = 0; i < isBossAefeatedArray.Length; i++)
                {
                    isBossAefeatedArray[i] = -1;
                }
            }

            //�����Щboss�Ѿ������ܣ�
            //�����ܵ�boss�ᱻ����:
            //[�����ܵ�boss��Ӧ���������ɽ���](ע��:������boss�Ķ�Ӧ��ϵ��ϵ������λ����,���ó���ʱҪ��������)
            for(int i = 0; i < isBossAefeatedArray.Length; i++)
            {
                switch (isBossAefeatedArray[i])
                {
                    case -1:
                        //δ����[����Ҫ����],��������Ϊ��ǰboss;
                        break;
                    case 0:
                        //δ����[����Ҫ����,Ҫ��������]
                        currentBossManager = allBossManager[i];
                        break;
                    case 1:
                        //�ѻ���[��Ҫ����]
                        allBossManager[i].gameObject.SetActive(false);
                        break;
                }
            }

            //������ǽ:
            if (currentBossManager != null)
            {
                int currentBossIndex = GetBossIndexInAllBossArray(currentBossManager);
                //������ǽ;
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
        //����bossս:
        public void ActivateBossFight(EnemyBossManager bossManager)
        {
            currentBossManager = bossManager;

            currentBossManager.bossFightIsActive = true;
            currentBossManager.bossHasBeenAwakened = true;

            uiBossHealthBar.SetUIHealthBarToActive();

            int currentBossIndex = GetBossIndexInAllBossArray(currentBossManager);
            //������ǽ;
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

            //���ø�boss�ѱ�����:
            isBossAefeatedArray[currentBossIndex] = 0;
        }
        //�Ѿ�����boss:
        public void BossHasBenDefeated(EnemyBossManager enemyBossManager)
        {
            enemyBossManager.bossHasBeenDefeated = true;
            enemyBossManager.bossFightIsActive = false;

            uiBossHealthBar.SetHealthBarToInactive();
            //ͣ����ǽ
            foreach(var fogWall in fogWalls)
            {
                fogWall.DeactivateFogWall();
            }

            int index = GetBossIndexInAllBossArray(enemyBossManager);
            isBossAefeatedArray[index] = 1;                                                     //��boss�Ѿ�������
        }

        //��ȡ��boss�������е�����λ��
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

        #region �������:
        //������ʱ:[������Ĺ���:]
        private void WhenEnabledBonfiresManager()
        {
            //����������ͬ://(��������Ϊ��)//�����������������ڳ�����������
            if (single.playerRegenerationData.LitBonfire == null || single.playerRegenerationData.LitBonfire.Length != bonfireLocations.Length)
            {
                playerRegenerationData.LitBonfire = new int[bonfireLocations.Length];

                for (int i = 0; i < playerRegenerationData.LitBonfire.Length; i++)
                {
                    playerRegenerationData.LitBonfire[i] = -1;
                }
            }
            //����״̬��ʼ��:
            ExtinguishOtherBonfires(single.playerRegenerationData.LitBonfire);
            //��Ϸ�����ʼ��:
            GameAreaDivision(gameAreas);
            //����������ఴ���򻮷�:
            BonfiresAreClassifiedIntoAreas(bonfireLocations);
        }
        //��ȡ�������������λ��:
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
        //Ϩ����������:
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

            //����Ƿ���������:
            CheckForNewAreas(bonfire);
        }
        

        //����״̬��ʼ��:
        private void ExtinguishOtherBonfires(int[] bonfireArray)
        {
            for (int i = 0; i < bonfireLocations.Length; i++)
            {
                if(bonfireArray[i] == 0 || bonfireArray[i] == -1)
                {
                    //����δ����ȼ:
                    bonfireLocations[i].isLgnite = false;
                    //����δ������:[bonfireArray[i] = -1ʱδ������]
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

        //������Ϸ���򻮷�:
        private void GameAreaDivision(GameArea[] gameAreas)
        {
            for(int i = 0; i < gameAreas.Length; i++)
            {
                gameAreas[i].bonfireInArea = new List<BonfireLocation>();
            }
        }
        //����ֵ��ض�����[�����������򱻷���]::
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
                        //����������Ǳ���ʾ����,��õص�Ҳ�Ǳ���ʾ����:
                        if(bonfire.isOnceMet == true)
                        {
                            gameAreas[i].isOnceMet = true;
                        }
                    }
                }
            }
        }
        //����Ƿ���������:
        private void CheckForNewAreas(BonfireLocation bonfire)
        {
            //ֻҪ��һ�����𱻷���,��õ���������:
            if (bonfire.isOnceMet == true)
            {
                gameAreas[bonfire.gameAreaIndex].isOnceMet = true;
            }
        }
        #endregion

        #region ������ع���

        private void WhenEnabledOpenDoorAndMechanism()
        {
            DoorInitialization();
            ElevatorInitialization();
        }
        //���ؿ������ŵ�����:
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
        //�ŵĳ�ʼ��:
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

        //���ؿ���������̨����:0��û����,1���Ѿ�����
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
        //�������ĳ�ʼ��
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

        #region ʰȡ���뱦��
        private void WhenEnabledPickUpAndTreasureChest()
        {
            AllPickupInitialization();      //��ʰȡ��ĳ�ʼ��;
            AllChestInitialization();       //�����ʼ��;
        }

        //�����Ѿ�ʰȡ�Ŀ�ʰȡ�������:0:[δʰȡ],1:[�Ѿ�ʰȡ]
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
        //��ʰȡ��ĳ�ʼ��:
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

        //���ؿ����ı��������:
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
        //����ĳ�ʼ��:
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

        #region ����ǽ��
        private void WhenEnabledIllusionaryWall()
        {
            AllIllusionaryWallInitialization();
        }

        //�����Ѿ�����������ǽ��:
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
        //����ǽ�ڳ�ʼ��:
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

        //���˻ָ���ʼ״̬:
        public void AllEnemyReturnToOriginalState()
        {
            foreach(var resurrectableManager in enemyResurrectableManagers)
            {
                resurrectableManager.ReturnToOriginal();
            }
        }
    }

    //��Ҹ���������:(�˳���Ϸʱ����,����������������ʱ���¸�ֵ:)

    [System.Serializable]
    public struct GameArea
    {
        public bool isOnceMet;                                          //�Ƿ񱻷���
        public string areaName;                                         //��������;
        public Sprite areaIcon;                                            //����ͼ��;
        public List<BonfireLocation> bonfireInArea;         //����������;
    }
}
public class PlayerRegenerationData
{
    public static string playerName;
    public bool isLostSoulNotFound;                                //�Ƿ�ʧ�����;
    public Vector3 soulLossPoint;                                     //��ʧ�����λ��;
    public int soulLossAmount;                                        //��ʧ�����;

    //��ȼ������:
    public int[] LitBonfire;
}