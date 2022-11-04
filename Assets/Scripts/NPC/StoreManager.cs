using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class StoreManager : MonoBehaviour
    {
        public string shopownerName;    //��������
        [Header("��Ʒ�б�:")]
        public List<Commodity> commoditieList;
        [Header("���׽����Ի�:")]
        public string[] postPurchaseDialogue = { "�Ҿ�֪�������۹���ã�", "���㽻�׷ǳ����,��ӭ�´ι��١�" };
        public string[] notPurchasedDialogue = { "���������𣿺ðɡ�", "��ӭ�´ι��١�" };
        [Header("���������Ĺǻ�:")]
        public GameObject itemPickUp;
        public AshesItem ashesItem;
        [Header("�ۻ����׽��:")]
        public int cumulativeTransactionAmount;

        private void Awake()
        {
            PlotProgressManager.single.RegisterStore(this);
        }
        //�����Ʒ:
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