using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlotProgressManager : MonoBehaviour
    {
        [Header("���߽���:")]
        public int mainlineProgress = 0;

        [Header("����npc�ĵ�ǰ����:")]
        public NpcPlotProgress[] allNpcPlotProgressArray;
        [Header("����npc�����о���:")]
        public NpcPlot[] allNPCPlotArray;
        public Dictionary<string, NpcManager> allNPC = new Dictionary<string, NpcManager>();
        [Header("�����̵�:")]
        public Dictionary<string, StoreManager> allStore = new Dictionary<string, StoreManager>();  //�̵�:Key-��������,�̵����
        [HideInInspector] public ShopData[] allShopDataArray;                                                                     //�����̵�Ĵ浵����;
        [Header("������Ʒ:")]
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
            //����
            PlayerSaveManager.LoadLoadDataToPlotProgress(this);
            TryToStartAllNpcPlot(allNPC, allNpcPlotProgressArray);
        }

        #region ����:
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
        //��ȡ��ĳnpc�����о���
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
        //��ȡĳnpc��ǰ����:
        private DialogueandAndPlot GetCurrentPlotOfNPC(string npcName , string plotName)
        {
            NpcPlot npc = new NpcPlot();
            npc.npcName = "Empty NPC";                          //�յ�npc
            for(int i = 0; i < allNPCPlotArray.Length; i++)
            {
                if(allNPCPlotArray[i].npcName == npcName)
                {
                    npc = allNPCPlotArray[i];
                }
            }

            //���npc��Ȼ�ǿ�npc˵��û����λnpc
            if(npc.npcName.Equals("Empty NPC"))
            {
                Debug.Log("û�� " + npcName + " ��λNpc");
                return null;
            }
            else
            {
                //����npc�����о���:
                for(int i = 0; i < npc.allDialogueandAndPlot.Length; i++)
                {
                    if(npc.allDialogueandAndPlot[i].plotName == plotName)
                    {
                        return npc.allDialogueandAndPlot[i];
                    }
                }

                Debug.Log(npcName + " Npcû�� " + plotName + " ��ξ���");
                return null;
            }
        }

        //����npc���䵱ǰ����,�Լ���ǰ�������,����npc�������ĳ��Կ������鷽��:
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
                        //��ȡnpc����;
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
                    //��ȡĳnpc�����о���:
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
            //��ȡĳnpc�����о���:
            DialogueandAndPlot[] npcAllPlot = GetNpcManagerByName(npcName);
            if (npcAllPlot != null)
            {
                return npcAllPlot[0].plotName;
            }

            return null;
        }
        #endregion

        #region �̵�
        //ע���̵꣺
        public void RegisterStore(StoreManager storeManager)
        {
            if (storeManager != null)
                allStore.Add(storeManager.shopownerName, storeManager);
        }
        //����һ���̵�:
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
        //���������̵�:
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

        //�����̵��ʼ��:
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
        //��ȡĳ�̵��������Ʒ:
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