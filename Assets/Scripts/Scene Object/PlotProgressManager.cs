using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlotProgressManager : MonoBehaviour
    {
        [Header("主线进度:")]
        public int mainlineProgress = 0;

        [Header("所有npc的当前剧情:")]
        public NpcPlotProgress[] allNpcPlotProgressArray;
        [Header("所有npc的所有剧情:")]
        public NpcPlot[] allNPCPlotArray;
        public Dictionary<string, NpcManager> allNPC = new Dictionary<string, NpcManager>();
        [Header("所有商店:")]
        public Dictionary<string, StoreManager> allStore = new Dictionary<string, StoreManager>();  //商店:Key-店主名称,商店管理
        [HideInInspector] public ShopData[] allShopDataArray;                                                                     //所有商店的存档数据;
        [Header("所有商品:")]
        public List<Commodity> allCommoditieList = new List<Commodity>();
        public static PlotProgressManager single;
        private void Awake()
        {
            if (single == null)
            {
                single = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            //读档
            PlayerSaveManager.LoadLoadDataToPlotProgress(this);
            TryToStartAllNpcPlot(allNPC, allNpcPlotProgressArray);
        }

        #region 剧情:
        public void AdvanceToMainPlot(int target)
        {
            if(target > mainlineProgress)
            {
                mainlineProgress = target;
            }
        }
        public void NpcRegistrationMethod(NpcManager npc)
        {
            if(npc != null)
            {
                allNPC.Add(npc.npcName ,npc);
            }
        }
        //获取到某npc的所有剧情
        private DialogueandAndPlot[] GetNpcManagerByName(string npcName) 
        {
            for(int i=0;i< allNPCPlotArray.Length; i++)
            {
                if(allNPCPlotArray[i].npcName == npcName)
                {
                    return allNPCPlotArray[i].allDialogueandAndPlot;
                }
            }

            return null;
        }
        //获取某npc当前剧情:
        private DialogueandAndPlot GetCurrentPlotOfNPC(string npcName , string plotName)
        {
            NpcPlot npc = new NpcPlot();
            npc.npcName = "Empty NPC";                          //空的npc
            for(int i = 0; i < allNPCPlotArray.Length; i++)
            {
                if(allNPCPlotArray[i].npcName == npcName)
                {
                    npc = allNPCPlotArray[i];
                }
            }

            //如果npc依然是空npc说明没有这位npc
            if(npc.npcName.Equals("Empty NPC"))
            {
                Debug.Log("没有 " + npcName + " 这位Npc");
                return null;
            }
            else
            {
                //遍历npc的所有剧情:
                for(int i = 0; i < npc.allDialogueandAndPlot.Length; i++)
                {
                    if(npc.allDialogueandAndPlot[i].plotName == plotName)
                    {
                        return npc.allDialogueandAndPlot[i];
                    }
                }

                Debug.Log(npcName + " Npc没有 " + plotName + " 这段剧情");
                return null;
            }
        }

        //遍历npc分配当前剧情,以及当前剧情进度,调用npc管理器的尝试开启剧情方法:
        private void TryToStartAllNpcPlot(Dictionary<string , NpcManager> allNpcDictionary , NpcPlotProgress[] npcPlotProgressArray)
        {
            if(allNpcPlotProgressArray.Length > 0)
            {
                for(int i = 0; i < npcPlotProgressArray.Length; i++)
                {
                    NpcManager npc;
                    allNpcDictionary.TryGetValue(npcPlotProgressArray[i].npcName,out npc);

                    if(npc != null)
                    {
                        //获取npc剧情;
                        npc.currentDialogueAndPlot = GetCurrentPlotOfNPC(npcPlotProgressArray[i].npcName, npcPlotProgressArray[i].currentPlotName);
                        npc.currentDialogueAndPlot.dialogStatus = npcPlotProgressArray[i].currentPlotProgress;
                        npc.currentPlotName = npcPlotProgressArray[i].currentPlotName;

                        npc.InitializeByPlot(npcPlotProgressArray[i].isHostile);

                        if (npcPlotProgressArray[i].isDeath)
                        {
                            npc.enemyManager.isDeath = true;
                            npc.gameObject.SetActive(false);
                        }
                    }
                }
            }
            else
            {
                foreach(NpcManager npc in allNpcDictionary.Values)
                {
                    //获取某npc的所有剧情:
                    DialogueandAndPlot[] npcAllPlot = GetNpcManagerByName(npc.npcName);
                    if (npcAllPlot != null)
                    {
                        npc.currentDialogueAndPlot = npcAllPlot[0];
                        npc.currentDialogueAndPlot.dialogStatus = -1;
                        npc.currentPlotName = npcAllPlot[0].plotName;
                    }
                    else
                    {
                        npc.currentDialogueAndPlot = null;
                    }

                    npc.InitializeByPlot(false);
                }
            }
        }
        public string GetNpcInitialPlotName(string npcName)
        {
            //获取某npc的所有剧情:
            DialogueandAndPlot[] npcAllPlot = GetNpcManagerByName(npcName);
            if (npcAllPlot != null)
            {
                return npcAllPlot[0].plotName;
            }

            return null;
        }
        #endregion

        #region 商店
        //注册商店：
        public void RegisterStore(StoreManager storeManager)
        {
            if (storeManager != null)
                allStore.Add(storeManager.shopownerName, storeManager);
        }
        //返回一个商店:
        private ShopData GetStoreData(StoreManager store)
        {
            ShopData shop = new ShopData();
            List<Commodity> commoditieList = store.commoditieList;
            CommodityData[] commoditiesDataArray = new CommodityData[commoditieList.Count];

            for(int i = 0; i < commoditiesDataArray.Length; i++)
            {
                CommodityData temp = new CommodityData();
                temp.commodityName = commoditieList[i].commodityName;
                temp.commodityAmount = commoditieList[i].commodityAmount;
                commoditiesDataArray[i] = temp;
            }
            shop.shopName = store.shopownerName;
            shop.allCommodityData = commoditiesDataArray;
            return shop;
        }
        //返回所有商店:
        public ShopData[] GetAllStoreData()
        {
            ShopData[] allShopArray = new ShopData[allStore.Count];
            int index = 0;
            foreach(StoreManager store in allStore.Values)
            {
                allShopArray[index] = GetStoreData(store);
                index++;
            }

            return allShopArray;
        }

        //所有商店初始化:
        public void InitializationAllStore(ShopData[] allShopDataArray)
        {
            foreach(ShopData shopData in allShopDataArray)
            {
                List<Commodity> commoditieList = new List<Commodity>();
                foreach(CommodityData commodityData in shopData.allCommodityData)
                {
                    foreach(Commodity commodity in allCommoditieList)
                    {
                        if(commodity.commodityName == commodityData.commodityName)
                        {
                            Commodity temp = Instantiate(commodity);
                            temp.commodityAmount = commodityData.commodityAmount;
                            commoditieList.Add(temp);
                            break;
                        }
                    }
                }
                StoreManager store;
                allStore.TryGetValue(shopData.shopName, out store);
                store.commoditieList = commoditieList;
            }
        }
        //获取某商店的所有商品:
        public List<Commodity> GetStoreCommoditieList(string shopownerName)
        {
            PlayerSaveManager.LoadLoadDataToPlotProgress(this);
            List<Commodity> commodityList = new List<Commodity>();
            foreach(ShopData shop in allShopDataArray)
            {
                if(shop.shopName == shopownerName)
                {
                    foreach (CommodityData commodityData in shop.allCommodityData)
                    {
                        foreach (Commodity commodity in allCommoditieList)
                        {
                            if (commodity.commodityName == commodityData.commodityName)
                            {
                                Commodity temp = Instantiate(commodity);
                                temp.commodityAmount = commodityData.commodityAmount;
                                commodityList.Add(temp);
                                break;
                            }
                        }
                    }
                    break;
                }
            }
            return commodityList;
        }
        #endregion
    }

    [System.Serializable]
    public struct NpcPlot
    {
        public string npcName;
        public DialogueandAndPlot[] allDialogueandAndPlot;

        public DialogueandAndPlot GetDialogueandAndPlotByName(string plotName)
        {
            for(int i = 0; i < allDialogueandAndPlot.Length; i++)
            {
                if(allDialogueandAndPlot[i].plotName == plotName)
                {
                    return allDialogueandAndPlot[i];
                }
            }

            return null;
        }
    }
}