using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class StoreManager : MonoBehaviour
    {
        public string shopownerName;    //店主名称
        [Header("商品列表:")]
        public List<Commodity> commoditieList;
        [Header("交易结束对话:")]
        public string[] postPurchaseDialogue = { "我就知道，您眼光真好！", "和你交易非常愉快,欢迎下次光临。" };
        public string[] notPurchasedDialogue = { "都看不上吗？好吧。", "欢迎下次光临。" };
        [Header("死亡后掉落的骨灰:")]
        public GameObject itemPickUp;
        public AshesItem ashesItem;
        [Header("累积交易金额:")]
        public int cumulativeTransactionAmount;

        private void Awake()
        {
            PlotProgressManager.single.RegisterStore(this);
        }
        //添加商品:
        public void AddCommodity(Commodity commodity)
        {
            if (commodity != null)
            {
                foreach(Commodity myCommodity in commoditieList)
                {
                    if(myCommodity.commodityName == commodity.commodityName)
                    {
                        myCommodity.commodityAmount += commodity.commodityAmount;
                        return;
                    }
                }
                commoditieList.Add(commodity);
            }
        }

        public void InstantiationAshesItem()
        {
            GameObject ashes = Instantiate(itemPickUp);
            ashes.transform.position = transform.position + new Vector3(0, 0.25f, 0);
            ItemPickUp pickUp = ashes.GetComponent<ItemPickUp>();
            pickUp.item = ashesItem;
            pickUp.itemType = ItemType.Consumable;
        }
    }
}