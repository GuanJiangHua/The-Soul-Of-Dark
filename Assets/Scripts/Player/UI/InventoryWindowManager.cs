using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    //各个库存窗口管理:(除了武器[懒得改了])
    public class InventoryWindowManager : MonoBehaviour
    {
        public PlayerManager player;

        [Header("消耗品库存:")]
        public GameObject consumableInventoryWindon;         //消耗品库存ui窗口引用
        public GameObject consumableInventorySlotPrefab;     //消耗品库存槽预制体;
        public Transform consumableInventorySlotParent;        //消耗品背包库存槽的父物体;
        ConsumableInventorySlot[] consumableInventorySlots; //消耗品库存槽;
        Vector2 consumableSlopParentOriginalSize;                  //消耗品库存槽父物体的原始大小;
        Vector2 consumableSlopParentOriginalPosition;           //消耗品库存槽父物体的原始位置;

        [Header("头盔库存:")]
        public GameObject helmetEquipmentInventoryWindon;                        //头盔库存ui窗口引用
        public GameObject helmetEquipmentInventorySlotPrefab;                    //头盔库存槽预制体;
        public Transform helmetEquipmentInventorySlotParent;                        //头盔背包库存槽的父物体;
        HelmetEquipmentInventorySlotUI[] helmetEquipmentInventorySlots;   //头盔库存槽
        Vector2 helmetSlopParentOriginalSize;                                                   //头盔库存槽父物体的原始大小;
        Vector2 helmetSlopParentOriginalPosition;                                            //头盔库存槽父物体的原始位置;

        [Header("胸甲库存:")]
        public GameObject torsoEquipmentInventoryWindon;                            //胸甲库存ui窗口引用
        public GameObject torsoEquipmentInventorySlotPrefab;                        //胸甲库存槽预制体
        public Transform torsoEquipmentInventorySlotParent;                            //胸甲库存槽的父物体
        TorsoEquipmentInventorySlotUI[] torsoEquipmentInventorySlotUIs;       //胸甲库存槽
        Vector2 torsoSlopParentOriginalSize;                                                        //胸甲库存槽父物体的原始大小;
        Vector2 torsoSlopParentOriginalPosition;                                                 //胸甲库存槽父物体的原始位置;

        [Header("腿甲库存:")]
        public GameObject legEquipmentInventoryWindon;                                //腿甲库存ui窗口引用
        public GameObject legEquipmentInventorySlotPrefab;                            //腿甲库存槽预制体
        public Transform legEquipmentInventorySlotParent;                               //腿甲库存槽父物体
        LegEquipmentInventorySlotUI[] legEquipmentInventorySlotUIs;              //腿甲库存槽
        Vector2 legSlopParentOriginalSize;                                                           //腿甲库存槽父物体的原始大小;
        Vector2 legSlopParentOriginalPosition;                                                    //腿甲库存槽父物体的原始位置;

        [Header("臂甲库存:")]
        public GameObject handEquipmentInventoryWindon;                             //臂甲库存ui窗口引用
        public GameObject handEquipmentInventorySlotPrefab;                         //臂甲库存槽预制体
        public Transform handEquipmentInventorySlotParent;                            //臂甲库存槽父物体
        HandEquipmentInventorySlotUI[] handEquipmentInventorySlotUIs;        //臂甲库存槽
        Vector2 handSlopParentOriginalSize;                                                        //臂甲库存槽父物体的原始大小;
        Vector2 handSlopParentOriginalPosition;                                                 //臂甲库存槽父物体的原始位置;

        [Header("弹药库存:")]
        public GameObject ammoInventoryWindon;
        public GameObject ammoInventorySlotPrefab;
        public Transform ammoInventorySlotParent;
        [Header("选择的弹药槽:")]
        public int ammoSlotIndex = -1;
        AmmoInventorySlot[] ammoInventorySlots;
        Vector2 ammoSlopParentOriginalSize;                                                        //弹丸库存槽父物体的原始大小;
        Vector2 ammoSlopParentOriginalPosition;                                                 //弹丸库存槽父物体的原始位置;

        [Header("戒指库存:")]
        public GameObject ringInventoryWindon;                                                //臂甲库存ui窗口引用
        public GameObject ringInventorySlotPrefab;                                            //臂甲库存槽预制体
        public Transform ringInventorySlotParent;                                               //臂甲库存槽父物体
        RingInventorySlot[] ringInventorySlotUIs;                                                 //臂甲库存槽
        Vector2 ringSlopParentOriginalSize;                                                         //臂甲库存槽父物体的原始大小;
        Vector2 ringSlopParentOriginalPosition;                                                  //臂甲库存槽父物体的原始位置;

        private void Start()
        {
            consumableSlopParentOriginalSize = consumableInventorySlotParent.GetComponent<RectTransform>().sizeDelta;
            consumableSlopParentOriginalPosition = consumableInventorySlotParent.GetComponent<RectTransform>().anchoredPosition;

            helmetSlopParentOriginalSize = helmetEquipmentInventorySlotParent.GetComponent<RectTransform>().sizeDelta;
            helmetSlopParentOriginalPosition = helmetEquipmentInventorySlotParent.GetComponent<RectTransform>().anchoredPosition;

            torsoSlopParentOriginalSize = torsoEquipmentInventorySlotParent.GetComponent<RectTransform>().sizeDelta;
            torsoSlopParentOriginalPosition = torsoEquipmentInventorySlotParent.GetComponent<RectTransform>().anchoredPosition;

            legSlopParentOriginalSize = legEquipmentInventorySlotParent.GetComponent<RectTransform>().sizeDelta;
            legSlopParentOriginalPosition = legEquipmentInventorySlotParent.GetComponent<RectTransform>().anchoredPosition;
            
            handSlopParentOriginalSize = handEquipmentInventorySlotParent.GetComponent<RectTransform>().sizeDelta;
            handSlopParentOriginalPosition = handEquipmentInventorySlotParent.GetComponent<RectTransform>().anchoredPosition;

            ammoSlopParentOriginalSize = ammoInventorySlotParent.GetComponent<RectTransform>().sizeDelta;
            ammoSlopParentOriginalPosition = ammoInventorySlotParent.GetComponent<RectTransform>().anchoredPosition;

            ringSlopParentOriginalSize = ringInventorySlotParent.GetComponent<RectTransform>().sizeDelta;
            ringSlopParentOriginalPosition = ringInventorySlotParent.GetComponent<RectTransform>().anchoredPosition;
        }
        //当启用时:
        private void OnEnable()
        {

        }

        //更新管理的库存窗口UI:
        public void UpdateInventoryWindowUI()
        {
            UpdateConsumableInventorySlot();
            UpdateHelmetInventorySlot();
            UpdateTorsoEquipmentInventorySlot();
            UpdateLegEquipmentInventorySlotUI();
            UpdateHandEquipmentInventorySlotUI();
            UpdateAmmoInventorySlotUI();
            UpdateRingInventorySlotUI();
        }

        //按钮调用:
        public void SelectAmmoSlot(int index)
        {
            ammoSlotIndex = index;
        }

        //更新消耗品库存窗口的槽位:
        private void UpdateConsumableInventorySlot()
        {
            consumableInventorySlots = GetInventorySlotsInChildren<ConsumableInventorySlot>(consumableInventorySlotParent);

            List<ConsumableItem> consumableItems = player.playerInventoryManager.consumableInventory;
            //实例化槽位:(更新槽位数据)
            for (int i = 1; i < consumableInventorySlots.Length; i++)
            {
                if(i - 1 < consumableItems.Count)
                {
                    //如果"库存"大于"槽位个数:"
                    if(consumableInventorySlots.Length -1 < consumableItems.Count)
                    {
                        Instantiate(consumableInventorySlotPrefab, consumableInventorySlotParent);
                        //重新获取槽位数组:
                        consumableInventorySlots = GetInventorySlotsInChildren<ConsumableInventorySlot>(consumableInventorySlotParent);
                    }

                    consumableInventorySlots[i].AddItem(consumableItems[i - 1]);
                }
                //超过"消耗品库存"的部分槽位禁用:
                else
                {
                    consumableInventorySlots[i].ClearItem();
                }
            }

            //同步ui大小:
            int chilrenNumber = consumableItems.Count;
            Vector2 sizeV2 = consumableSlopParentOriginalSize;
            Vector2 positionV2 = consumableSlopParentOriginalPosition;
            if (chilrenNumber > 16)
            {
                sizeV2.y += ((chilrenNumber - 16) / 4 + 1) * (consumableSlopParentOriginalSize.y / 4.0f);
                positionV2.y -= ((chilrenNumber - 16) / 4 + 1) * (consumableSlopParentOriginalSize.y / 8.0f);
            }

            consumableInventorySlotParent.GetComponent<RectTransform>().sizeDelta = sizeV2;
            consumableInventorySlotParent.GetComponent<RectTransform>().anchoredPosition = positionV2;
        }

        //更新头盔库存窗口的槽位:
        private void UpdateHelmetInventorySlot()
        {
            helmetEquipmentInventorySlots = GetInventorySlotsInChildren<HelmetEquipmentInventorySlotUI>(helmetEquipmentInventorySlotParent);

            List<HelmetEquipment> helmets = player.playerInventoryManager.headEquipmentInventory;

            //遍历"头盔槽"数组:
            for(int i = 1; i < helmetEquipmentInventorySlots.Length; i++)
            {
                //小于库存数部分的槽位:
                if(i-1 < helmets.Count)
                {
                    //如果槽位数小于库存数:
                    if (helmetEquipmentInventorySlots.Length - 1 < helmets.Count)
                    {
                        //实例化新槽位:
                        Instantiate(helmetEquipmentInventorySlotPrefab , helmetEquipmentInventorySlotParent);
                        //重新获取数组:
                        helmetEquipmentInventorySlots = GetInventorySlotsInChildren<HelmetEquipmentInventorySlotUI>(helmetEquipmentInventorySlotParent);
                    }

                    //如果"槽位"够用,启用以前的槽位:
                    helmetEquipmentInventorySlots[i].AddItem(helmets[i - 1]);
                }
                //大于库存数的槽位:(禁用掉)
                else
                {
                    helmetEquipmentInventorySlots[i].ClearItem();
                }
            }

            //同步ui大小:
            int chilrenNumber = helmets.Count;
            Vector2 sizeV2 = helmetSlopParentOriginalSize;
            Vector2 positionV2 = helmetSlopParentOriginalPosition;
            if (chilrenNumber > 16)
            {
                sizeV2.y += ((chilrenNumber - 16) / 4 + 1) * (helmetSlopParentOriginalSize.y / 4.0f);
                positionV2.y -= ((chilrenNumber - 16) / 4 + 1) * (helmetSlopParentOriginalSize.y / 8.0f);
            }

            helmetEquipmentInventorySlotParent.GetComponent<RectTransform>().sizeDelta = sizeV2;
            helmetEquipmentInventorySlotParent.GetComponent<RectTransform>().anchoredPosition = positionV2;
        }

        //更新胸甲库存槽位:
        private void UpdateTorsoEquipmentInventorySlot()
        {
            torsoEquipmentInventorySlotUIs = GetInventorySlotsInChildren<TorsoEquipmentInventorySlotUI>(torsoEquipmentInventorySlotParent);
            List<TorsoEquipment> torsos = player.playerInventoryManager.torsoEquipmentInventory;

            for(int i = 1; i<torsoEquipmentInventorySlotUIs.Length; i++)
            {
                if(i-1 < torsos.Count)
                {
                    //如果槽数组内的槽位小于列表元素,就实例化一个新槽,并且重新加载槽位数组
                    if(torsoEquipmentInventorySlotUIs.Length -1 < torsos.Count)
                    {
                        Instantiate(torsoEquipmentInventorySlotPrefab, torsoEquipmentInventorySlotParent);

                        torsoEquipmentInventorySlotUIs = GetInventorySlotsInChildren<TorsoEquipmentInventorySlotUI>(torsoEquipmentInventorySlotParent);
                    }

                    //更新该位置槽位对应库存中的物品:
                    torsoEquipmentInventorySlotUIs[i].AddItem(torsos[i - 1]);
                }
                else
                {
                    torsoEquipmentInventorySlotUIs[i].ClearItem();
                }
            }

            //同步ui大小:
            int chilrenNumber = torsos.Count;
            Vector2 sizeV2 = torsoSlopParentOriginalSize;
            Vector2 positionV2 = torsoSlopParentOriginalPosition;
            if (chilrenNumber > 16)
            {
                sizeV2.y += ((chilrenNumber - 16) / 4 + 1) * (torsoSlopParentOriginalSize.y / 4.0f);
                positionV2.y -= ((chilrenNumber - 16) / 4 + 1) * (torsoSlopParentOriginalSize.y / 8.0f);
            }

            torsoEquipmentInventorySlotParent.GetComponent<RectTransform>().sizeDelta = sizeV2;
            torsoEquipmentInventorySlotParent.GetComponent<RectTransform>().anchoredPosition = positionV2;
        }

        //更新腿甲库存槽位:
        private void UpdateLegEquipmentInventorySlotUI()
        {
            //获取槽位:
            legEquipmentInventorySlotUIs = GetInventorySlotsInChildren<LegEquipmentInventorySlotUI>(legEquipmentInventorySlotParent);
            List<LegEquipment> legs = player.playerInventoryManager.legEquipmentInventory;
            for(int i = 1; i < legEquipmentInventorySlotUIs.Length; i++)
            {
                //如果i<库存腿甲数;
                if (i -1 < legs.Count)
                {
                    //如果巢位不够:
                    if(legEquipmentInventorySlotUIs.Length -1 < legs.Count)
                    {
                        Instantiate(legEquipmentInventorySlotPrefab, legEquipmentInventorySlotParent);
                        //重新获取槽位:
                        legEquipmentInventorySlotUIs = GetInventorySlotsInChildren<LegEquipmentInventorySlotUI>(legEquipmentInventorySlotParent);
                    }

                    legEquipmentInventorySlotUIs[i].AddItem(legs[i - 1]);
                }
                else
                {
                    legEquipmentInventorySlotUIs[i].ClearItem();
                }
            }

            //同步ui大小:
            int chilrenNumber = legs.Count;
            Vector2 sizeV2 = legSlopParentOriginalSize;
            Vector2 positionV2 = legSlopParentOriginalPosition;
            if (chilrenNumber > 16)
            {
                sizeV2.y += ((chilrenNumber - 16) / 4 + 1) * (legSlopParentOriginalSize.y / 4.0f);
                positionV2.y -= ((chilrenNumber - 16) / 4 + 1) * (legSlopParentOriginalSize.y / 8.0f);
            }

            legEquipmentInventorySlotParent.GetComponent<RectTransform>().sizeDelta = sizeV2;
            legEquipmentInventorySlotParent.GetComponent<RectTransform>().anchoredPosition = positionV2;
        }

        //更新臂甲库存槽位:
        private void UpdateHandEquipmentInventorySlotUI()
        {
            handEquipmentInventorySlotUIs = GetInventorySlotsInChildren<HandEquipmentInventorySlotUI>(handEquipmentInventorySlotParent);
            List<HandEquipment> hands = player.playerInventoryManager.handEquipmentInventory;
            for(int i = 1; i < handEquipmentInventorySlotUIs.Length; i++)
            {
                if (i - 1 < hands.Count)
                {
                    if (handEquipmentInventorySlotUIs.Length - 1 < hands.Count)
                    {
                        Instantiate(handEquipmentInventorySlotPrefab, handEquipmentInventorySlotParent);
                        handEquipmentInventorySlotUIs = GetInventorySlotsInChildren<HandEquipmentInventorySlotUI>(handEquipmentInventorySlotParent);
                    }

                    handEquipmentInventorySlotUIs[i].AddItem(hands[i - 1]);
                }
                else
                {
                    handEquipmentInventorySlotUIs[i].ClearItem();
                }
            }

            //同步ui大小:
            int chilrenNumber = hands.Count;
            Vector2 sizeV2 = handSlopParentOriginalSize;
            Vector2 positionV2 = handSlopParentOriginalPosition;
            if (chilrenNumber > 16)
            {
                sizeV2.y += ((chilrenNumber - 16) / 4 + 1) * (handSlopParentOriginalSize.y / 4.0f);
                positionV2.y -= ((chilrenNumber - 16) / 4 + 1) * (handSlopParentOriginalSize.y / 8.0f);
            }

            handEquipmentInventorySlotParent.GetComponent<RectTransform>().sizeDelta = sizeV2;
            handEquipmentInventorySlotParent.GetComponent<RectTransform>().anchoredPosition = positionV2;
        }

        //更新弹丸库存:
        private void UpdateAmmoInventorySlotUI()
        {
            ammoInventorySlots = GetInventorySlotsInChildren<AmmoInventorySlot>(ammoInventorySlotParent);
            List<RangedAmmoItem> ammos = player.playerInventoryManager.rangedAmmoInventory;
            for (int i = 1; i < ammoInventorySlots.Length; i++)
            {
                if (i - 1 < ammos.Count)
                {
                    if (ammoInventorySlots.Length - 1 < ammos.Count)
                    {
                        Instantiate(ammoInventorySlotPrefab, ammoInventorySlotParent);
                        ammoInventorySlots = GetInventorySlotsInChildren<AmmoInventorySlot>(ammoInventorySlotParent);
                    }

                    ammoInventorySlots[i].AddItem(ammos[i - 1]);
                }
                else
                {
                    ammoInventorySlots[i].ClearItem();
                }
            }

            //同步ui大小:
            int chilrenNumber = ammos.Count;
            Vector2 sizeV2 = ammoSlopParentOriginalSize;
            Vector2 positionV2 = ammoSlopParentOriginalPosition;
            if (chilrenNumber > 16)
            {
                sizeV2.y += ((chilrenNumber - 16) / 4 + 1) * (ammoSlopParentOriginalSize.y / 4.0f);
                positionV2.y -= ((chilrenNumber - 16) / 4 + 1) * (ammoSlopParentOriginalSize.y / 8.0f);
            }

            ammoInventorySlotParent.GetComponent<RectTransform>().sizeDelta = sizeV2;
            ammoInventorySlotParent.GetComponent<RectTransform>().anchoredPosition = positionV2;
        }

        private void UpdateRingInventorySlotUI()
        {
            ringInventorySlotUIs = GetInventorySlotsInChildren<RingInventorySlot>(ringInventorySlotParent);
            List<RingItem> rings = player.playerInventoryManager.ringInventory;
            for (int i = 1; i < ringInventorySlotUIs.Length; i++)
            {
                if (i - 1 < rings.Count)
                {
                    if (ringInventorySlotUIs.Length - 1 < rings.Count)
                    {
                        Instantiate(ringInventorySlotPrefab, ringInventorySlotParent);
                        ringInventorySlotUIs = GetInventorySlotsInChildren<RingInventorySlot>(ringInventorySlotParent);
                    }

                    ringInventorySlotUIs[i].AddItem(rings[i - 1]);
                }
                else
                {
                    ringInventorySlotUIs[i].ClearItem();
                }
            }

            //同步ui大小:
            int chilrenNumber = rings.Count;
            Vector2 sizeV2 = ringSlopParentOriginalSize;
            Vector2 positionV2 = ringSlopParentOriginalPosition;
            if (chilrenNumber > 16)
            {
                sizeV2.y += ((chilrenNumber - 16) / 4 + 1) * (ringSlopParentOriginalSize.y / 4.0f);
                positionV2.y -= ((chilrenNumber - 16) / 4 + 1) * (ringSlopParentOriginalSize.y / 8.0f);
            }

            ringInventorySlotParent.GetComponent<RectTransform>().sizeDelta = sizeV2;
            ringInventorySlotParent.GetComponent<RectTransform>().anchoredPosition = positionV2;
        }

        private T[] GetInventorySlotsInChildren<T>(Transform parent)
        {
            T[] typeArray = new T[parent.childCount];
            for(int i=0;i<parent.childCount;i++)
            {
                typeArray[i] = parent.GetChild(i).gameObject.GetComponent<T>();
            }

            return typeArray;
        }
    }
}