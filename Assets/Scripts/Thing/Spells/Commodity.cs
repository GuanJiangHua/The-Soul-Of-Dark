using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "��Ʒ��Ϣ", menuName = "��Ϸ��Ʒ/�½���Ʒ")]
    public class Commodity : ScriptableObject
    {
        [Header("��Ʒ����:")]
        public string commodityName;
        //��Ʒ:
        [Header("������:")]
        public ItemType itemType;
        public Item item;
        [Header("���������:")]
        public bool isInfinite = false;
        public int commodityAmount; //��Ʒ����
        [Header("��Ʒ��ֵ:")]
        public ItemType exchangeItemType;   //����������;
        public Item exchangeItem;
        public int commodityPrice;
    }
}