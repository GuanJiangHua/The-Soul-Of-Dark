using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class AllItemInventoryWindow : MonoBehaviour
    {
        public PlayerManager player;

        [Header("消耗品库存:")]
        public GameObject consumableInventoryWindon;         //消耗品库存ui窗口引用;
        public GameObject consumableInventorySlotPrefab;     //消耗品库存槽预制体;
        public Transform consumableInventorySlotParent;        //背包库存槽的父物体;
        ConsumableInventorySlot[] consumableInventorySlots; //武器库存槽;
        Vector2 consumableSlopParentOriginalSize;                  //消耗品库存槽父物体的原始大小;
        Vector2 consumableSlopParentOriginalPosition;           //消耗品库存槽父物体的原始位置;
        [Header("武器库存:")]
        public GameObject weaponInventoryWindon;              //武器库存ui窗口引用;
        public GameObject weaponInventorySlotPrefab;          //武器库存槽预制体;
        public Transform weaponInventorySlotParent;             //武器库存槽的父物体;
        WeaponInventorySlot[] weaponInventorySlots;            //武器库存槽;
        Vector2 weaponSlopParentOriginalSize;                       //武器库存槽父物体的原始大小;
        Vector2 weaponSlopParentOriginalPosition;                //武器库存槽父物体的原始位置;
        [Header("头盔库存:")]
        public GameObject helmetInventoryWindon;                                 //头盔库存ui窗口引用;
        public GameObject helmetInventorySlotPrefab;                             //头盔库存槽预制体;
        public Transform helmetInventorySlotParent;                                 //头盔库存槽的父物体;
        HelmetEquipmentInventorySlotUI[] helmetInventorySlots;            //头盔库存槽;
        Vector2 helmetSlopParentOriginalSize;                                          //头盔库存槽父物体的原始大小;
        Vector2 helmetSlopParentOriginalPosition;                                   //头盔库存槽父物体的原始位置;
        [Header("胸甲库存:")]
        public GameObject torsoInventoryWindon;                                 //胸甲库存ui窗口引用;
        public GameObject torsoInventorySlotPrefab;                             //胸甲库存槽预制体;
        public Transform torsoInventorySlotParent;                                 //胸甲库存槽的父物体;
        TorsoEquipmentInventorySlotUI[] torsoInventorySlots;               //胸甲库存槽;
        Vector2 torsoSlopParentOriginalSize;                                          //胸甲库存槽父物体的原始大小;
        Vector2 torsoSlopParentOriginalPosition;                                   //胸甲库存槽父物体的原始位置;
        [Header("腿甲库存:")]
        public GameObject legInventoryWindon;                                 //腿甲库存ui窗口引用;
        public GameObject legInventorySlotPrefab;                             //腿甲库存槽预制体;
        public Transform legInventorySlotParent;                                 //腿甲库存槽的父物体;
        LegEquipmentInventorySlotUI[] legInventorySlots;                  //腿甲库存槽;
        Vector2 legSlopParentOriginalSize;                                          //腿甲库存槽父物体的原始大小;
        Vector2 legSlopParentOriginalPosition;                                   //腿甲库存槽父物体的原始位置;
        [Header("臂甲库存:")]
        public GameObject handInventoryWindon;                                 //臂甲库存ui窗口引用;
        public GameObject handInventorySlotPrefab;                             //臂甲库存槽预制体;
        public Transform handInventorySlotParent;                                 //臂甲库存槽的父物体;
        HandEquipmentInventorySlotUI[] handInventorySlots;               //臂甲库存槽;
        Vector2 handSlopParentOriginalSize;                                          //臂甲库存槽父物体的原始大小;
        Vector2 handSlopParentOriginalPosition;                                   //臂甲库存槽父物体的原始位置;
        [Header("弹丸库存:")]
        public GameObject ammoInventoryWindon;                                 //弹丸库存ui窗口引用;
        public GameObject ammoInventorySlotPrefab;                             //弹丸库存槽预制体;
        public Transform ammoInventorySlotParent;                                 //弹丸库存槽的父物体;
        AmmoInventorySlot[] ammoInventorySlots;                                  //弹丸库存槽;
        Vector2 ammoSlopParentOriginalSize;                                          //弹丸库存槽父物体的原始大小;
        Vector2 ammoSlopParentOriginalPosition;                                   //弹丸库存槽父物体的原始位置;
        [Header("戒指库存:")]
        public GameObject ringInventoryWindon;                                 //戒指库存ui窗口引用;
        public GameObject ringInventorySlotPrefab;                             //戒指库存槽预制体;
        public Transform ringInventorySlotParent;                                 //戒指库存槽的父物体;
        RingInventorySlot[] ringInventorySlots;                                      //戒指库存槽;
        Vector2 ringSlopParentOriginalSize;                                          //戒指库存槽父物体的原始大小;
        Vector2 ringSlopParentOriginalPosition;                                   //戒指库存槽父物体的原始位置;
        [Header("法术库存:")]
        public GameObject spellInventoryWindon;                                 //法术库存ui窗口引用;
        public GameObject spellInventorySlotPrefab;                             //法术库存槽预制体;
        public Transform spellInventorySlotParent;                                 //法术库存槽的父物体;
        SpellInventorySlot[] spellInventorySlots;                                     //法术库存槽;
        Vector2 spellSlopParentOriginalSize;                                          //法术库存槽父物体的原始大小;
        Vector2 spellSlopParentOriginalPosition;                                   //法术库存槽父物体的原始位置;

        bool notUpdatedSizeAndPosition = false;

        //打开背包按钮调用: 物品类型id: 1[消耗品],2[武器],3[头盔],4[胸甲],5[腿甲],6[臂甲],7[弹丸],8[戒指],10[法术]
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
            //实例化槽位:(更新槽位数据)
            for (int i = 0; i < consumableInventorySlots.Length; i++)
            {
                if (i < consumableItems.Count)
                {
                    //如果"库存"大于"槽位个数:"
                    if (consumableInventorySlots.Length < consumableItems.Count)
                    {
                        Instantiate(consumableInventorySlotPrefab, consumableInventorySlotParent);
                        //重新获取槽位数组:
                        consumableInventorySlots = GetInventorySlotsInChildren<ConsumableInventorySlot>(consumableInventorySlotParent);
                    }

                    consumableInventorySlots[i].AddItem(consumableItems[i]);
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
        private void UpdateWeaponInventory()
        {
            weaponInventorySlots = GetInventorySlotsInChildren<WeaponInventorySlot>(weaponInventorySlotParent);

            List<WeaponItem> weapontems = player.playerInventoryManager.weaponInventory;
            //实例化槽位:(更新槽位数据)
            for (int i = 0; i < weaponInventorySlots.Length; i++)
            {
                if (i < weapontems.Count)
                {
                    //如果"库存"大于"槽位个数:"
                    if (weaponInventorySlots.Length < weapontems.Count)
                    {
                        Instantiate(weaponInventorySlotPrefab, weaponInventorySlotParent);
                        //重新获取槽位数组:
                        weaponInventorySlots = GetInventorySlotsInChildren<WeaponInventorySlot>(weaponInventorySlotParent);
                    }

                    weaponInventorySlots[i].AddItem(weapontems[i]);
                }
                //超过"消耗品库存"的部分槽位禁用:
                else
                {
                    weaponInventorySlots[i].ClearItem();
                }
            }

            //同步ui大小:
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
            //实例化槽位:(更新槽位数据)
            for (int i = 0; i < helmetInventorySlots.Length; i++)
            {
                if (i < helmetItems.Count)
                {
                    //如果"库存"大于"槽位个数:"
                    if (helmetInventorySlots.Length < helmetItems.Count)
                    {
                        Instantiate(helmetInventorySlotPrefab, helmetInventorySlotParent);
                        //重新获取槽位数组:
                        helmetInventorySlots = GetInventorySlotsInChildren<HelmetEquipmentInventorySlotUI>(helmetInventorySlotParent);
                    }

                    helmetInventorySlots[i].AddItem(helmetItems[i]);
                }
                //超过"消耗品库存"的部分槽位禁用:
                else
                {
                    helmetInventorySlots[i].ClearItem();
                }
            }

            //同步ui大小:
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
            //实例化槽位:(更新槽位数据)
            for (int i = 0; i < torsoInventorySlots.Length; i++)
            {
                if (i < torso.Count)
                {
                    //如果"库存"大于"槽位个数:"
                    if (torsoInventorySlots.Length < torso.Count)
                    {
                        Instantiate(torsoInventorySlotPrefab, torsoInventorySlotParent);
                        //重新获取槽位数组:
                        torsoInventorySlots = GetInventorySlotsInChildren<TorsoEquipmentInventorySlotUI>(torsoInventorySlotParent);
                    }

                    torsoInventorySlots[i].AddItem(torso[i]);
                }
                //超过"消耗品库存"的部分槽位禁用:
                else
                {
                    torsoInventorySlots[i].ClearItem();
                }
            }

            //同步ui大小:
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
            //实例化槽位:(更新槽位数据)
            for (int i = 0; i < legInventorySlots.Length; i++)
            {
                if (i < leg.Count)
                {
                    //如果"库存"大于"槽位个数:"
                    if (legInventorySlots.Length < leg.Count)
                    {
                        Instantiate(legInventorySlotPrefab, legInventorySlotParent);
                        //重新获取槽位数组:
                        legInventorySlots = GetInventorySlotsInChildren<LegEquipmentInventorySlotUI>(legInventorySlotParent);
                    }

                    legInventorySlots[i].AddItem(leg[i]);
                }
                //超过"消耗品库存"的部分槽位禁用:
                else
                {
                    legInventorySlots[i].ClearItem();
                }
            }

            //同步ui大小:
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
            //实例化槽位:(更新槽位数据)
            for (int i = 0; i < handInventorySlots.Length; i++)
            {
                if (i < hand.Count)
                {
                    //如果"库存"大于"槽位个数:"
                    if (handInventorySlots.Length < hand.Count)
                    {
                        Instantiate(handInventorySlotPrefab, handInventorySlotParent);
                        //重新获取槽位数组:
                        handInventorySlots = GetInventorySlotsInChildren<HandEquipmentInventorySlotUI>(handInventorySlotParent);
                    }

                    handInventorySlots[i].AddItem(hand[i]);
                }
                //超过"消耗品库存"的部分槽位禁用:
                else
                {
                    handInventorySlots[i].ClearItem();
                }
            }

            //同步ui大小:
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
            //实例化槽位:(更新槽位数据)
            for (int i = 0; i < ammoInventorySlots.Length; i++)
            {
                if (i < ammos.Count)
                {
                    //如果"库存"大于"槽位个数:"
                    if (ammoInventorySlots.Length < ammos.Count)
                    {
                        Instantiate(ammoInventorySlotPrefab, ammoInventorySlotParent);
                        //重新获取槽位数组:
                        ammoInventorySlots = ammoInventorySlots = GetInventorySlotsInChildren<AmmoInventorySlot>(ammoInventorySlotParent);
                    }

                    ammoInventorySlots[i].AddItem(ammos[i]);
                }
                //超过"消耗品库存"的部分槽位禁用:
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
        private void UpdateRingInventory()
        {
            ringInventorySlots = GetInventorySlotsInChildren<RingInventorySlot>(ringInventorySlotParent);

            List<RingItem> rings = player.playerInventoryManager.ringInventory;
            //实例化槽位:(更新槽位数据)
            for (int i = 0; i < ringInventorySlots.Length; i++)
            {
                if (i < rings.Count)
                {
                    //如果"库存"大于"槽位个数:"
                    if (ringInventorySlots.Length < rings.Count)
                    {
                        Instantiate(ringInventorySlotPrefab, ringInventorySlotParent);
                        //重新获取槽位数组:
                        ringInventorySlots = ringInventorySlots = GetInventorySlotsInChildren<RingInventorySlot>(ringInventorySlotParent);
                    }

                    ringInventorySlots[i].AddItem(rings[i]);
                }
                //超过"消耗品库存"的部分槽位禁用:
                else
                {
                    ringInventorySlots[i].ClearItem();
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
        }
        private void UpdateSpellInventory()
        {
            spellInventorySlots = GetInventorySlotsInChildren<SpellInventorySlot>(spellInventorySlotParent);

            List<SpellItem> spellList = player.playerInventoryManager.spellleInventory;
            //实例化槽位:(更新槽位数据)
            for (int i = 0; i < spellInventorySlots.Length; i++)
            {
                if (i < spellList.Count)
                {
                    //如果"库存"大于"槽位个数:"
                    if (spellInventorySlots.Length < spellList.Count)
                    {
                        Instantiate(spellInventorySlotPrefab, spellInventorySlotParent);
                        //重新获取槽位数组:
                        spellInventorySlots = GetInventorySlotsInChildren<SpellInventorySlot>(spellInventorySlotParent);
                    }

                    spellInventorySlots[i].AddSpell(spellList[i] , false);
                }
                //超过"消耗品库存"的部分槽位禁用:
                else
                {
                    spellInventorySlots[i].RemoveSpell(false);
                }

                //同步ui大小:
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
        //获取目标的子物体的组件:
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