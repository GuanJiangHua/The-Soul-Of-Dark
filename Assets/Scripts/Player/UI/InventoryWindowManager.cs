using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    //������洰�ڹ���:(��������[���ø���])
    public class InventoryWindowManager : MonoBehaviour
    {
        public PlayerManager player;

        [Header("����Ʒ���:")]
        public GameObject consumableInventoryWindon;         //����Ʒ���ui��������
        public GameObject consumableInventorySlotPrefab;     //����Ʒ����Ԥ����;
        public Transform consumableInventorySlotParent;        //����Ʒ�������۵ĸ�����;
        ConsumableInventorySlot[] consumableInventorySlots; //����Ʒ����;
        Vector2 consumableSlopParentOriginalSize;                  //����Ʒ���۸������ԭʼ��С;
        Vector2 consumableSlopParentOriginalPosition;           //����Ʒ���۸������ԭʼλ��;

        [Header("ͷ�����:")]
        public GameObject helmetEquipmentInventoryWindon;                        //ͷ�����ui��������
        public GameObject helmetEquipmentInventorySlotPrefab;                    //ͷ������Ԥ����;
        public Transform helmetEquipmentInventorySlotParent;                        //ͷ���������۵ĸ�����;
        HelmetEquipmentInventorySlotUI[] helmetEquipmentInventorySlots;   //ͷ������
        Vector2 helmetSlopParentOriginalSize;                                                   //ͷ�����۸������ԭʼ��С;
        Vector2 helmetSlopParentOriginalPosition;                                            //ͷ�����۸������ԭʼλ��;

        [Header("�ؼ׿��:")]
        public GameObject torsoEquipmentInventoryWindon;                            //�ؼ׿��ui��������
        public GameObject torsoEquipmentInventorySlotPrefab;                        //�ؼ׿���Ԥ����
        public Transform torsoEquipmentInventorySlotParent;                            //�ؼ׿��۵ĸ�����
        TorsoEquipmentInventorySlotUI[] torsoEquipmentInventorySlotUIs;       //�ؼ׿���
        Vector2 torsoSlopParentOriginalSize;                                                        //�ؼ׿��۸������ԭʼ��С;
        Vector2 torsoSlopParentOriginalPosition;                                                 //�ؼ׿��۸������ԭʼλ��;

        [Header("�ȼ׿��:")]
        public GameObject legEquipmentInventoryWindon;                                //�ȼ׿��ui��������
        public GameObject legEquipmentInventorySlotPrefab;                            //�ȼ׿���Ԥ����
        public Transform legEquipmentInventorySlotParent;                               //�ȼ׿��۸�����
        LegEquipmentInventorySlotUI[] legEquipmentInventorySlotUIs;              //�ȼ׿���
        Vector2 legSlopParentOriginalSize;                                                           //�ȼ׿��۸������ԭʼ��С;
        Vector2 legSlopParentOriginalPosition;                                                    //�ȼ׿��۸������ԭʼλ��;

        [Header("�ۼ׿��:")]
        public GameObject handEquipmentInventoryWindon;                             //�ۼ׿��ui��������
        public GameObject handEquipmentInventorySlotPrefab;                         //�ۼ׿���Ԥ����
        public Transform handEquipmentInventorySlotParent;                            //�ۼ׿��۸�����
        HandEquipmentInventorySlotUI[] handEquipmentInventorySlotUIs;        //�ۼ׿���
        Vector2 handSlopParentOriginalSize;                                                        //�ۼ׿��۸������ԭʼ��С;
        Vector2 handSlopParentOriginalPosition;                                                 //�ۼ׿��۸������ԭʼλ��;

        [Header("��ҩ���:")]
        public GameObject ammoInventoryWindon;
        public GameObject ammoInventorySlotPrefab;
        public Transform ammoInventorySlotParent;
        [Header("ѡ��ĵ�ҩ��:")]
        public int ammoSlotIndex = -1;
        AmmoInventorySlot[] ammoInventorySlots;
        Vector2 ammoSlopParentOriginalSize;                                                        //������۸������ԭʼ��С;
        Vector2 ammoSlopParentOriginalPosition;                                                 //������۸������ԭʼλ��;

        [Header("��ָ���:")]
        public GameObject ringInventoryWindon;                                                //�ۼ׿��ui��������
        public GameObject ringInventorySlotPrefab;                                            //�ۼ׿���Ԥ����
        public Transform ringInventorySlotParent;                                               //�ۼ׿��۸�����
        RingInventorySlot[] ringInventorySlotUIs;                                                 //�ۼ׿���
        Vector2 ringSlopParentOriginalSize;                                                         //�ۼ׿��۸������ԭʼ��С;
        Vector2 ringSlopParentOriginalPosition;                                                  //�ۼ׿��۸������ԭʼλ��;

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
        //������ʱ:
        private void OnEnable()
        {

        }

        //���¹���Ŀ�洰��UI:
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

        //��ť����:
        public void SelectAmmoSlot(int index)
        {
            ammoSlotIndex = index;
        }

        //��������Ʒ��洰�ڵĲ�λ:
        private void UpdateConsumableInventorySlot()
        {
            consumableInventorySlots = GetInventorySlotsInChildren<ConsumableInventorySlot>(consumableInventorySlotParent);

            List<ConsumableItem> consumableItems = player.playerInventoryManager.consumableInventory;
            //ʵ������λ:(���²�λ����)
            for (int i = 1; i < consumableInventorySlots.Length; i++)
            {
                if(i - 1 < consumableItems.Count)
                {
                    //���"���"����"��λ����:"
                    if(consumableInventorySlots.Length -1 < consumableItems.Count)
                    {
                        Instantiate(consumableInventorySlotPrefab, consumableInventorySlotParent);
                        //���»�ȡ��λ����:
                        consumableInventorySlots = GetInventorySlotsInChildren<ConsumableInventorySlot>(consumableInventorySlotParent);
                    }

                    consumableInventorySlots[i].AddItem(consumableItems[i - 1]);
                }
                //����"����Ʒ���"�Ĳ��ֲ�λ����:
                else
                {
                    consumableInventorySlots[i].ClearItem();
                }
            }

            //ͬ��ui��С:
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

        //����ͷ����洰�ڵĲ�λ:
        private void UpdateHelmetInventorySlot()
        {
            helmetEquipmentInventorySlots = GetInventorySlotsInChildren<HelmetEquipmentInventorySlotUI>(helmetEquipmentInventorySlotParent);

            List<HelmetEquipment> helmets = player.playerInventoryManager.headEquipmentInventory;

            //����"ͷ����"����:
            for(int i = 1; i < helmetEquipmentInventorySlots.Length; i++)
            {
                //С�ڿ�������ֵĲ�λ:
                if(i-1 < helmets.Count)
                {
                    //�����λ��С�ڿ����:
                    if (helmetEquipmentInventorySlots.Length - 1 < helmets.Count)
                    {
                        //ʵ�����²�λ:
                        Instantiate(helmetEquipmentInventorySlotPrefab , helmetEquipmentInventorySlotParent);
                        //���»�ȡ����:
                        helmetEquipmentInventorySlots = GetInventorySlotsInChildren<HelmetEquipmentInventorySlotUI>(helmetEquipmentInventorySlotParent);
                    }

                    //���"��λ"����,������ǰ�Ĳ�λ:
                    helmetEquipmentInventorySlots[i].AddItem(helmets[i - 1]);
                }
                //���ڿ�����Ĳ�λ:(���õ�)
                else
                {
                    helmetEquipmentInventorySlots[i].ClearItem();
                }
            }

            //ͬ��ui��С:
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

        //�����ؼ׿���λ:
        private void UpdateTorsoEquipmentInventorySlot()
        {
            torsoEquipmentInventorySlotUIs = GetInventorySlotsInChildren<TorsoEquipmentInventorySlotUI>(torsoEquipmentInventorySlotParent);
            List<TorsoEquipment> torsos = player.playerInventoryManager.torsoEquipmentInventory;

            for(int i = 1; i<torsoEquipmentInventorySlotUIs.Length; i++)
            {
                if(i-1 < torsos.Count)
                {
                    //����������ڵĲ�λС���б�Ԫ��,��ʵ����һ���²�,�������¼��ز�λ����
                    if(torsoEquipmentInventorySlotUIs.Length -1 < torsos.Count)
                    {
                        Instantiate(torsoEquipmentInventorySlotPrefab, torsoEquipmentInventorySlotParent);

                        torsoEquipmentInventorySlotUIs = GetInventorySlotsInChildren<TorsoEquipmentInventorySlotUI>(torsoEquipmentInventorySlotParent);
                    }

                    //���¸�λ�ò�λ��Ӧ����е���Ʒ:
                    torsoEquipmentInventorySlotUIs[i].AddItem(torsos[i - 1]);
                }
                else
                {
                    torsoEquipmentInventorySlotUIs[i].ClearItem();
                }
            }

            //ͬ��ui��С:
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

        //�����ȼ׿���λ:
        private void UpdateLegEquipmentInventorySlotUI()
        {
            //��ȡ��λ:
            legEquipmentInventorySlotUIs = GetInventorySlotsInChildren<LegEquipmentInventorySlotUI>(legEquipmentInventorySlotParent);
            List<LegEquipment> legs = player.playerInventoryManager.legEquipmentInventory;
            for(int i = 1; i < legEquipmentInventorySlotUIs.Length; i++)
            {
                //���i<����ȼ���;
                if (i -1 < legs.Count)
                {
                    //�����λ����:
                    if(legEquipmentInventorySlotUIs.Length -1 < legs.Count)
                    {
                        Instantiate(legEquipmentInventorySlotPrefab, legEquipmentInventorySlotParent);
                        //���»�ȡ��λ:
                        legEquipmentInventorySlotUIs = GetInventorySlotsInChildren<LegEquipmentInventorySlotUI>(legEquipmentInventorySlotParent);
                    }

                    legEquipmentInventorySlotUIs[i].AddItem(legs[i - 1]);
                }
                else
                {
                    legEquipmentInventorySlotUIs[i].ClearItem();
                }
            }

            //ͬ��ui��С:
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

        //���±ۼ׿���λ:
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

            //ͬ��ui��С:
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

        //���µ�����:
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

            //ͬ��ui��С:
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

            //ͬ��ui��С:
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