using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "商品信息", menuName = "游戏物品/新建商品")]
    public class Commodity : ScriptableObject
    {
        [Header("商品名称:")]
        public string commodityName;
        //物品:
        [Header("交易物:")]
        public ItemType itemType;
        public Item item;
        [Header("交易物个数:")]
        public bool isInfinite = false;
        public int commodityAmount; //商品数量
        [Header("商品价值:")]
        public ItemType exchangeItemType;   //交易物类型;
        public Item exchangeItem;
        public int commodityPrice;
    }
}