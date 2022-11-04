using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class AllItemInventoryWindow : MonoBehaviour
    {
        public PlayerManager player;

        [Header("����Ʒ���:")]
        public GameObject consumableInventoryWindon;         //����Ʒ���ui��������;
        public GameObject consumableInventorySlotPrefab;     //����Ʒ����Ԥ����;
        public Transform consumableInventorySlotParent;        //�������۵ĸ�����;
        ConsumableInventorySlot[] consumableInventorySlots; //��������;
        Vector2 consumableSlopParentOriginalSize;                  //����Ʒ���۸������ԭʼ��С;
        Vector2 consumableSlopParentOriginalPosition;           //����Ʒ���۸������ԭʼλ��;
        [Header("�������:")]
        public GameObject weaponInventoryWindon;              //�������ui��������;
        public GameObject weaponInventorySlotPrefab;          //��������Ԥ����;
        public Transform weaponInventorySlotParent;             //�������۵ĸ�����;
        WeaponInventorySlot[] weaponInventorySlots;            //��������;
        Vector2 weaponSlopParentOriginalSize;                       //�������۸������ԭʼ��С;
        Vector2 weaponSlopParentOriginalPosition;                //�������۸������ԭʼλ��;
        [Header("ͷ�����:")]
        public GameObject helmetInventoryWindon;                                 //ͷ�����ui��������;
        public GameObject helmetInventorySlotPrefab;                             //ͷ������Ԥ����;
        public Transform helmetInventorySlotParent;                                 //ͷ�����۵ĸ�����;
        HelmetEquipmentInventorySlotUI[] helmetInventorySlots;            //ͷ������;
        Vector2 helmetSlopParentOriginalSize;                                          //ͷ�����۸������ԭʼ��С;
        Vector2 helmetSlopParentOriginalPosition;                                   //ͷ�����۸������ԭʼλ��;
        [Header("�ؼ׿��:")]
        public GameObject torsoInventoryWindon;                                 //�ؼ׿��ui��������;
        public GameObject torsoInventorySlotPrefab;                             //�ؼ׿���Ԥ����;
        public Transform torsoInventorySlotParent;                                 //�ؼ׿��۵ĸ�����;
        TorsoEquipmentInventorySlotUI[] torsoInventorySlots;               //�ؼ׿���;
        Vector2 torsoSlopParentOriginalSize;                                          //�ؼ׿��۸������ԭʼ��С;
        Vector2 torsoSlopParentOriginalPosition;                                   //�ؼ׿��۸������ԭʼλ��;
        [Header("�ȼ׿��:")]
        public GameObject legInventoryWindon;                                 //�ȼ׿��ui��������;
        public GameObject legInventorySlotPrefab;                             //�ȼ׿���Ԥ����;
        public Transform legInventorySlotParent;                                 //�ȼ׿��۵ĸ�����;
        LegEquipmentInventorySlotUI[] legInventorySlots;                  //�ȼ׿���;
        Vector2 legSlopParentOriginalSize;                                          //�ȼ׿��۸������ԭʼ��С;
        Vector2 legSlopParentOriginalPosition;                                   //�ȼ׿��۸������ԭʼλ��;
        [Header("�ۼ׿��:")]
        public GameObject handInventoryWindon;                                 //�ۼ׿��ui��������;
        public GameObject handInventorySlotPrefab;                             //�ۼ׿���Ԥ����;
        public Transform handInventorySlotParent;                                 //�ۼ׿��۵ĸ�����;
        HandEquipmentInventorySlotUI[] handInventorySlots;               //�ۼ׿���;
        Vector2 handSlopParentOriginalSize;                                          //�ۼ׿��۸������ԭʼ��С;
        Vector2 handSlopParentOriginalPosition;                                   //�ۼ׿��۸������ԭʼλ��;
        [Header("������:")]
        public GameObject ammoInventoryWindon;                                 //������ui��������;
        public GameObject ammoInventorySlotPrefab;                             //�������Ԥ����;
        public Transform ammoInventorySlotParent;                                 //������۵ĸ�����;
        AmmoInventorySlot[] ammoInventorySlots;                                  //�������;
        Vector2 ammoSlopParentOriginalSize;                                          //������۸������ԭʼ��С;
        Vector2 ammoSlopParentOriginalPosition;                                   //������۸������ԭʼλ��;
        [Header("��ָ���:")]
        public GameObject ringInventoryWindon;                                 //��ָ���ui��������;
        public GameObject ringInventorySlotPrefab;                             //��ָ����Ԥ����;
        public Transform ringInventorySlotParent;                                 //��ָ���۵ĸ�����;
        RingInventorySlot[] ringInventorySlots;                                      //��ָ����;
        Vector2 ringSlopParentOriginalSize;                                          //��ָ���۸������ԭʼ��С;
        Vector2 ringSlopParentOriginalPosition;                                   //��ָ���۸������ԭʼλ��;
        [Header("�������:")]
        public GameObject spellInventoryWindon;                                 //�������ui��������;
        public GameObject spellInventorySlotPrefab;                             //��������Ԥ����;
        public Transform spellInventorySlotParent;                                 //�������۵ĸ�����;
        SpellInventorySlot[] spellInventorySlots;                                     //��������;
        Vector2 spellSlopParentOriginalSize;                                          //�������۸������ԭʼ��С;
        Vector2 spellSlopParentOriginalPosition;                                   //�������۸������ԭʼλ��;

        bool notUpdatedSizeAndPosition = false;

        //�򿪱�����ť����: ��Ʒ����id: 1[����Ʒ],2[����],3[ͷ��],4[�ؼ�],5[�ȼ�],6[�ۼ�],7[����],8[��ָ],10[����]
        public void UpdateAllInventory(int itemTypeID)
        {
            if(notUpdatedSizeAndPosition == false)
            {
                notUpdatedSizeAndPosition = true;
                GeOriginalSizeAndOriginalPosition();
            }

            if(itemTypeID == 1)
            {
                UpdateConsumableInventory();
                consumableInventoryWindon.SetActive(true);
                weaponInventoryWindon.SetActive(false);
                helmetInventoryWindon.SetActive(false);
                torsoInventoryWindon.SetActive(false);
                legInventoryWindon.SetActive(false);
                handInventoryWindon.SetActive(false);
                ammoInventoryWindon.SetActive(false);
                ringInventoryWindon.SetActive(false);
                spellInventoryWindon.SetActive(false);
            }
            else if(itemTypeID == 2)
            {
                UpdateWeaponInventory();
                consumableInventoryWindon.SetActive(false);
                weaponInventoryWindon.SetActive(true);
                helmetInventoryWindon.SetActive(false);
                torsoInventoryWindon.SetActive(false);
                legInventoryWindon.SetActive(false);
                handInventoryWindon.SetActive(false);
                ammoInventoryWindon.SetActive(false);
                ringInventoryWindon.SetActive(false);
                spellInventoryWindon.SetActive(false);
            }
            else if(itemTypeID == 3)
            {
                UpdateHelmetInventory();
                consumableInventoryWindon.SetActive(false);
                weaponInventoryWindon.SetActive(false);
                helmetInventoryWindon.SetActive(true);
                torsoInventoryWindon.SetActive(false);
                legInventoryWindon.SetActive(false);
                handInventoryWindon.SetActive(false);
                ammoInventoryWindon.SetActive(false);
                ringInventoryWindon.SetActive(false);
                spellInventoryWindon.SetActive(false);
            }
            else if(itemTypeID == 4)
            {
                UpdateTorsoInventory();
                consumableInventoryWindon.SetActive(false);
                weaponInventoryWindon.SetActive(false);
                helmetInventoryWindon.SetActive(false);
                torsoInventoryWindon.SetActive(true);
                legInventoryWindon.SetActive(false);
                handInventoryWindon.SetActive(false);
                ammoInventoryWindon.SetActive(false);
                ringInventoryWindon.SetActive(false);
                spellInventoryWindon.SetActive(false);
            }
            else if(itemTypeID == 5)
            {
                UpdateLegInventory();
                consumableInventoryWindon.SetActive(false);
                weaponInventoryWindon.SetActive(false);
                helmetInventoryWindon.SetActive(false);
                torsoInventoryWindon.SetActive(false);
                legInventoryWindon.SetActive(true);
                handInventoryWindon.SetActive(false);
                ammoInventoryWindon.SetActive(false);
                ringInventoryWindon.SetActive(false);
                spellInventoryWindon.SetActive(false);
            }
            else if(itemTypeID == 6)
            {
                UpdateHandInventory();
                consumableInventoryWindon.SetActive(false);
                weaponInventoryWindon.SetActive(false);
                helmetInventoryWindon.SetActive(false);
                torsoInventoryWindon.SetActive(false);
                legInventoryWindon.SetActive(false);
                handInventoryWindon.SetActive(true);
                ammoInventoryWindon.SetActive(false);
                ringInventoryWindon.SetActive(false);
                spellInventoryWindon.SetActive(false);
            }
            else if(itemTypeID == 7)
            {
                UpdateAmmoInventory();
                consumableInventoryWindon.SetActive(false);
                weaponInventoryWindon.SetActive(false);
                helmetInventoryWindon.SetActive(false);
                torsoInventoryWindon.SetActive(false);
                legInventoryWindon.SetActive(false);
                handInventoryWindon.SetActive(false);
                ammoInventoryWindon.SetActive(true);
                ringInventoryWindon.SetActive(false);
                spellInventoryWindon.SetActive(false);
            }
            else if (itemTypeID == 8)
            {
                UpdateRingInventory();
                consumableInventoryWindon.SetActive(false);
                weaponInventoryWindon.SetActive(false);
                helmetInventoryWindon.SetActive(false);
                torsoInventoryWindon.SetActive(false);
                legInventoryWindon.SetActive(false);
                handInventoryWindon.SetActive(false);
                ammoInventoryWindon.SetActive(false);
                ringInventoryWindon.SetActive(true);
                spellInventoryWindon.SetActive(false);
            }
            else if(itemTypeID == 9)
            {
                UpdateSpellInventory();
                consumableInventoryWindon.SetActive(false);
                weaponInventoryWindon.SetActive(false);
                helmetInventoryWindon.SetActive(false);
                torsoInventoryWindon.SetActive(false);
                legInventoryWindon.SetActive(false);
                handInventoryWindon.SetActive(false);
                ammoInventoryWindon.SetActive(true);
                ringInventoryWindon.SetActive(false);
                spellInventoryWindon.SetActive(true);
            }
        }

        private void UpdateConsumableInventory()
        {
            consumableInventorySlots = GetInventorySlotsInChildren<ConsumableInventorySlot>(consumableInventorySlotParent);

            List<ConsumableItem> consumableItems = player.playerInventoryManager.consumableInventory;
            //ʵ������λ:(���²�λ����)
            for (int i = 0; i < consumableInventorySlots.Length; i++)
            {
                if (i < consumableItems.Count)
                {
                    //���"���"����"��λ����:"
                    if (consumableInventorySlots.Length < consumableItems.Count)
                    {
                        Instantiate(consumableInventorySlotPrefab, consumableInventorySlotParent);
                        //���»�ȡ��λ����:
                        consumableInventorySlots = GetInventorySlotsInChildren<ConsumableInventorySlot>(consumableInventorySlotParent);
                    }

                    consumableInventorySlots[i].AddItem(consumableItems[i]);
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
        private void UpdateWeaponInventory()
        {
            weaponInventorySlots = GetInventorySlotsInChildren<WeaponInventorySlot>(weaponInventorySlotParent);

            List<WeaponItem> weapontems = player.playerInventoryManager.weaponInventory;
            //ʵ������λ:(���²�λ����)
            for (int i = 0; i < weaponInventorySlots.Length; i++)
            {
                if (i < weapontems.Count)
                {
                    //���"���"����"��λ����:"
                    if (weaponInventorySlots.Length < weapontems.Count)
                    {
                        Instantiate(weaponInventorySlotPrefab, weaponInventorySlotParent);
                        //���»�ȡ��λ����:
                        weaponInventorySlots = GetInventorySlotsInChildren<WeaponInventorySlot>(weaponInventorySlotParent);
                    }

                    weaponInventorySlots[i].AddItem(weapontems[i]);
                }
                //����"����Ʒ���"�Ĳ��ֲ�λ����:
                else
                {
                    weaponInventorySlots[i].ClearItem();
                }
            }

            //ͬ��ui��С:
            int chilrenNumber = weapontems.Count;
            Vector2 sizeV2 = weaponSlopParentOriginalSize;
            Vector2 positionV2 = weaponSlopParentOriginalPosition;
            if (chilrenNumber > 16)
            {
                sizeV2.y += ((chilrenNumber - 16) / 4 + 1) * (weaponSlopParentOriginalSize.y / 4.0f);
                positionV2.y -= ((chilrenNumber - 16) / 4 + 1) * (weaponSlopParentOriginalSize.y / 8.0f);
            }

            weaponInventorySlotParent.GetComponent<RectTransform>().sizeDelta = sizeV2;
            weaponInventorySlotParent.GetComponent<RectTransform>().anchoredPosition = positionV2;
        }
        private void UpdateHelmetInventory()
        {
           helmetInventorySlots = GetInventorySlotsInChildren<HelmetEquipmentInventorySlotUI>(helmetInventorySlotParent);

            List<HelmetEquipment> helmetItems = player.playerInventoryManager.headEquipmentInventory;
            //ʵ������λ:(���²�λ����)
            for (int i = 0; i < helmetInventorySlots.Length; i++)
            {
                if (i < helmetItems.Count)
                {
                    //���"���"����"��λ����:"
                    if (helmetInventorySlots.Length < helmetItems.Count)
                    {
                        Instantiate(helmetInventorySlotPrefab, helmetInventorySlotParent);
                        //���»�ȡ��λ����:
                        helmetInventorySlots = GetInventorySlotsInChildren<HelmetEquipmentInventorySlotUI>(helmetInventorySlotParent);
                    }

                    helmetInventorySlots[i].AddItem(helmetItems[i]);
                }
                //����"����Ʒ���"�Ĳ��ֲ�λ����:
                else
                {
                    helmetInventorySlots[i].ClearItem();
                }
            }

            //ͬ��ui��С:
            int chilrenNumber = helmetItems.Count;
            Vector2 sizeV2 = helmetSlopParentOriginalSize;
            Vector2 positionV2 = helmetSlopParentOriginalPosition;
            if (chilrenNumber > 16)
            {
                sizeV2.y += ((chilrenNumber - 16) / 4 + 1) * (helmetSlopParentOriginalSize.y / 4.0f);
                positionV2.y -= ((chilrenNumber - 16) / 4 + 1) * (helmetSlopParentOriginalSize.y / 8.0f);
            }

            helmetInventorySlotParent.GetComponent<RectTransform>().sizeDelta = sizeV2;
            helmetInventorySlotParent.GetComponent<RectTransform>().anchoredPosition = positionV2;
        }
        private void UpdateTorsoInventory()
        {
            torsoInventorySlots = GetInventorySlotsInChildren<TorsoEquipmentInventorySlotUI>(torsoInventorySlotParent);

            List<TorsoEquipment> torso = player.playerInventoryManager.torsoEquipmentInventory;
            //ʵ������λ:(���²�λ����)
            for (int i = 0; i < torsoInventorySlots.Length; i++)
            {
                if (i < torso.Count)
                {
                    //���"���"����"��λ����:"
                    if (torsoInventorySlots.Length < torso.Count)
                    {
                        Instantiate(torsoInventorySlotPrefab, torsoInventorySlotParent);
                        //���»�ȡ��λ����:
                        torsoInventorySlots = GetInventorySlotsInChildren<TorsoEquipmentInventorySlotUI>(torsoInventorySlotParent);
                    }

                    torsoInventorySlots[i].AddItem(torso[i]);
                }
                //����"����Ʒ���"�Ĳ��ֲ�λ����:
                else
                {
                    torsoInventorySlots[i].ClearItem();
                }
            }

            //ͬ��ui��С:
            int chilrenNumber = torso.Count;
            Vector2 sizeV2 = torsoSlopParentOriginalSize;
            Vector2 positionV2 = torsoSlopParentOriginalPosition;
            if (chilrenNumber > 16)
            {
                sizeV2.y += ((chilrenNumber - 16) / 4 + 1) * (torsoSlopParentOriginalSize.y / 4.0f);
                positionV2.y -= ((chilrenNumber - 16) / 4 + 1) * (torsoSlopParentOriginalSize.y / 8.0f);
            }

            torsoInventorySlotParent.GetComponent<RectTransform>().sizeDelta = sizeV2;
            torsoInventorySlotParent.GetComponent<RectTransform>().anchoredPosition = positionV2;
        }
        private void UpdateLegInventory()
        {
            legInventorySlots = GetInventorySlotsInChildren<LegEquipmentInventorySlotUI>(legInventorySlotParent);

            List<LegEquipment> leg = player.playerInventoryManager.legEquipmentInventory;
            //ʵ������λ:(���²�λ����)
            for (int i = 0; i < legInventorySlots.Length; i++)
            {
                if (i < leg.Count)
                {
                    //���"���"����"��λ����:"
                    if (legInventorySlots.Length < leg.Count)
                    {
                        Instantiate(legInventorySlotPrefab, legInventorySlotParent);
                        //���»�ȡ��λ����:
                        legInventorySlots = GetInventorySlotsInChildren<LegEquipmentInventorySlotUI>(legInventorySlotParent);
                    }

                    legInventorySlots[i].AddItem(leg[i]);
                }
                //����"����Ʒ���"�Ĳ��ֲ�λ����:
                else
                {
                    legInventorySlots[i].ClearItem();
                }
            }

            //ͬ��ui��С:
            int chilrenNumber = leg.Count;
            Vector2 sizeV2 = legSlopParentOriginalSize;
            Vector2 positionV2 = legSlopParentOriginalPosition;
            if (chilrenNumber > 16)
            {
                sizeV2.y += ((chilrenNumber - 16) / 4 + 1) * (legSlopParentOriginalSize.y / 4.0f);
                positionV2.y -= ((chilrenNumber - 16) / 4 + 1) * (legSlopParentOriginalSize.y / 8.0f);
            }

            legInventorySlotParent.GetComponent<RectTransform>().sizeDelta = sizeV2;
            legInventorySlotParent.GetComponent<RectTransform>().anchoredPosition = positionV2;
        }
        private void UpdateHandInventory()
        {
            handInventorySlots = GetInventorySlotsInChildren<HandEquipmentInventorySlotUI>(handInventorySlotParent);

            List<HandEquipment> hand = player.playerInventoryManager.handEquipmentInventory;
            //ʵ������λ:(���²�λ����)
            for (int i = 0; i < handInventorySlots.Length; i++)
            {
                if (i < hand.Count)
                {
                    //���"���"����"��λ����:"
                    if (handInventorySlots.Length < hand.Count)
                    {
                        Instantiate(handInventorySlotPrefab, handInventorySlotParent);
                        //���»�ȡ��λ����:
                        handInventorySlots = GetInventorySlotsInChildren<HandEquipmentInventorySlotUI>(handInventorySlotParent);
                    }

                    handInventorySlots[i].AddItem(hand[i]);
                }
                //����"����Ʒ���"�Ĳ��ֲ�λ����:
                else
                {
                    handInventorySlots[i].ClearItem();
                }
            }

            //ͬ��ui��С:
            int chilrenNumber = hand.Count;
            Vector2 sizeV2 = handSlopParentOriginalSize;
            Vector2 positionV2 = handSlopParentOriginalPosition;
            if (chilrenNumber > 16)
            {
                sizeV2.y += ((chilrenNumber - 16) / 4 + 1) * (handSlopParentOriginalSize.y / 4.0f);
                positionV2.y -= ((chilrenNumber - 16) / 4 + 1) * (handSlopParentOriginalSize.y / 8.0f);
            }

            handInventorySlotParent.GetComponent<RectTransform>().sizeDelta = sizeV2;
            handInventorySlotParent.GetComponent<RectTransform>().anchoredPosition = positionV2;
        }
        private void UpdateAmmoInventory()
        {
            ammoInventorySlots = GetInventorySlotsInChildren<AmmoInventorySlot>(ammoInventorySlotParent);

            List<RangedAmmoItem> ammos = player.playerInventoryManager.rangedAmmoInventory;
            //ʵ������λ:(���²�λ����)
            for (int i = 0; i < ammoInventorySlots.Length; i++)
            {
                if (i < ammos.Count)
                {
                    //���"���"����"��λ����:"
                    if (ammoInventorySlots.Length < ammos.Count)
                    {
                        Instantiate(ammoInventorySlotPrefab, ammoInventorySlotParent);
                        //���»�ȡ��λ����:
                        ammoInventorySlots = ammoInventorySlots = GetInventorySlotsInChildren<AmmoInventorySlot>(ammoInventorySlotParent);
                    }

                    ammoInventorySlots[i].AddItem(ammos[i]);
                }
                //����"����Ʒ���"�Ĳ��ֲ�λ����:
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
        private void UpdateRingInventory()
        {
            ringInventorySlots = GetInventorySlotsInChildren<RingInventorySlot>(ringInventorySlotParent);

            List<RingItem> rings = player.playerInventoryManager.ringInventory;
            //ʵ������λ:(���²�λ����)
            for (int i = 0; i < ringInventorySlots.Length; i++)
            {
                if (i < rings.Count)
                {
                    //���"���"����"��λ����:"
                    if (ringInventorySlots.Length < rings.Count)
                    {
                        Instantiate(ringInventorySlotPrefab, ringInventorySlotParent);
                        //���»�ȡ��λ����:
                        ringInventorySlots = ringInventorySlots = GetInventorySlotsInChildren<RingInventorySlot>(ringInventorySlotParent);
                    }

                    ringInventorySlots[i].AddItem(rings[i]);
                }
                //����"����Ʒ���"�Ĳ��ֲ�λ����:
                else
                {
                    ringInventorySlots[i].ClearItem();
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
        }
        private void UpdateSpellInventory()
        {
            spellInventorySlots = GetInventorySlotsInChildren<SpellInventorySlot>(spellInventorySlotParent);

            List<SpellItem> spellList = player.playerInventoryManager.spellleInventory;
            //ʵ������λ:(���²�λ����)
            for (int i = 0; i < spellInventorySlots.Length; i++)
            {
                if (i < spellList.Count)
                {
                    //���"���"����"��λ����:"
                    if (spellInventorySlots.Length < spellList.Count)
                    {
                        Instantiate(spellInventorySlotPrefab, spellInventorySlotParent);
                        //���»�ȡ��λ����:
                        spellInventorySlots = GetInventorySlotsInChildren<SpellInventorySlot>(spellInventorySlotParent);
                    }

                    spellInventorySlots[i].AddSpell(spellList[i] , false);
                }
                //����"����Ʒ���"�Ĳ��ֲ�λ����:
                else
                {
                    spellInventorySlots[i].RemoveSpell(false);
                }

                //ͬ��ui��С:
                int chilrenNumber = spellList.Count;
                Vector2 sizeV2 = spellSlopParentOriginalSize;
                Vector2 positionV2 = spellSlopParentOriginalPosition;
                if (chilrenNumber > 16)
                {
                    sizeV2.y += ((chilrenNumber - 16) / 4 + 1) * (spellSlopParentOriginalSize.y / 4.0f);
                    positionV2.y -= ((chilrenNumber - 16) / 4 + 1) * (spellSlopParentOriginalSize.y / 8.0f);
                }

                spellInventorySlotParent.GetComponent<RectTransform>().sizeDelta = sizeV2;
                spellInventorySlotParent.GetComponent<RectTransform>().anchoredPosition = positionV2;
            }
        }
        private void GeOriginalSizeAndOriginalPosition()
        {
            consumableSlopParentOriginalSize = consumableInventorySlotParent.GetComponent<RectTransform>().sizeDelta;
            consumableSlopParentOriginalPosition = consumableInventorySlotParent.GetComponent<RectTransform>().anchoredPosition;

            weaponSlopParentOriginalSize = weaponInventorySlotParent.GetComponent<RectTransform>().sizeDelta;
            weaponSlopParentOriginalPosition = weaponInventorySlotParent.GetComponent<RectTransform>().anchoredPosition;

            helmetSlopParentOriginalSize = helmetInventorySlotParent.GetComponent<RectTransform>().sizeDelta;
            helmetSlopParentOriginalPosition = helmetInventorySlotParent.GetComponent<RectTransform>().anchoredPosition;

            torsoSlopParentOriginalSize = torsoInventorySlotParent.GetComponent<RectTransform>().sizeDelta;
            torsoSlopParentOriginalPosition = torsoInventorySlotParent.GetComponent<RectTransform>().anchoredPosition;

            legSlopParentOriginalSize = legInventorySlotParent.GetComponent<RectTransform>().sizeDelta;
            legSlopParentOriginalPosition = legInventorySlotParent.GetComponent<RectTransform>().anchoredPosition;

            handSlopParentOriginalSize = handInventorySlotParent.GetComponent<RectTransform>().sizeDelta;
            handSlopParentOriginalPosition = handInventorySlotParent.GetComponent<RectTransform>().anchoredPosition;

            ammoSlopParentOriginalSize = ammoInventorySlotParent.GetComponent<RectTransform>().sizeDelta;
            ammoSlopParentOriginalPosition = ammoInventorySlotParent.GetComponent<RectTransform>().anchoredPosition;

            ringSlopParentOriginalSize = ringInventorySlotParent.GetComponent<RectTransform>().sizeDelta;
            ringSlopParentOriginalPosition = ringInventorySlotParent.GetComponent<RectTransform>().anchoredPosition;

            spellSlopParentOriginalSize = spellInventorySlotParent.GetComponent<RectTransform>().sizeDelta;
            spellSlopParentOriginalPosition = spellInventorySlotParent.GetComponent<RectTransform>().anchoredPosition;
        }
        //��ȡĿ�������������:
        private T[] GetInventorySlotsInChildren<T>(Transform parent)
        {
            T[] typeArray = new T[parent.childCount];
            for (int i = 0; i < parent.childCount; i++)
            {
                typeArray[i] = parent.GetChild(i).gameObject.GetComponent<T>();
            }

            return typeArray;
        }
    }
}