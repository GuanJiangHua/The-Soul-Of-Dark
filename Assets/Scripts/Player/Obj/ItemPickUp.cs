using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class ItemPickUp : Interactable
    {
        public ItemType itemType = ItemType.Weapon;
        public Item item;
        [Header("������Ʒ�ͼ�ʸʱ:")]
        public int amount = 1;
        [Header("�Ƿ��Ѿ�ʰȡ:")]
        public bool isAlreadyPicked = false;

        private void Awake()
        {
            switch (itemType)
            {
                case ItemType.Ammo:
                    item = Instantiate(item);
                    RangedAmmoItem ammoItem = item as RangedAmmoItem;
                    ammoItem.currentAmount = amount;
                    break;
                case ItemType.Consumable:
                    item = Instantiate(item);
                    ConsumableItem consumableItem = item as ConsumableItem;
                    consumableItem.currentItemAmount = amount;
                    break;
            }
        }
        //��������:
        public override void Interact(PlayerManager playerManger)
        {
            base.Interact(playerManger);
            PickUpItemByType(playerManger);
        }

        //������Ʒ:
        private void PickUpItemByType(PlayerManager playerManger)
        {
            PlayerInventoryManager playerInventory;
            PlayerLocomotionManager playerLocomotion;
            PlayerAnimatorManager animatorHandler;
            playerInventory = playerManger.GetComponent<PlayerInventoryManager>();
            playerLocomotion = playerManger.GetComponent<PlayerLocomotionManager>();
            animatorHandler = playerManger.GetComponentInChildren<PlayerAnimatorManager>();

            playerLocomotion.rigidbody.velocity = Vector3.zero;                                         //ʰȡ��Ʒʱֹͣ�ƶ�:
            animatorHandler.PlayTargetAnimation("Pick Up Item", true);                             //���ż�����Ʒ����������ע���ڽ���;

            playerManger.itemInteractableObj.GetComponentInChildren<Text>().text = item.itemName;         //��ȡ�����������Text�������������Ʒ��Ϣ
            playerManger.itemInteractableObj.SetActive(true);

            switch (itemType)
            {
                case ItemType.Weapon:
                    playerInventory.weaponInventory.Add((WeaponItem)item);                                                //��������ӵ����������;
                    break;
                case ItemType.Helmet:
                    playerInventory.headEquipmentInventory.Add((HelmetEquipment)item);                                                //��������ӵ����������;
                    break;
                case ItemType.Torso:
                    playerInventory.torsoEquipmentInventory.Add((TorsoEquipment)item);                                                //��������ӵ����������;
                    break;
                case ItemType.Leg:
                    playerInventory.legEquipmentInventory.Add((LegEquipment)item);                                                //��������ӵ����������;
                    break;
                case ItemType.Hand:
                    playerInventory.handEquipmentInventory.Add((HandEquipment)item);                                                //��������ӵ����������;
                    break;
                case ItemType.Spell:
                    playerInventory.spellleInventory.Add((SpellItem)item);                                                //��������ӵ����������;
                    break;
                case ItemType.Consumable:
                    playerInventory.AddConsumableItemToInventory((ConsumableItem)item);              //������Ʒ��ӵ����������;
                    break;
                case ItemType.Ring:
                    playerInventory.ringInventory.Add((RingItem)item);                                                //��������ӵ����������;
                    break;
                case ItemType.Ammo:
                    playerInventory.AddAmmoItemToInventory((RangedAmmoItem)item);
                    break;
            }

            isAlreadyPicked = true;
            gameObject.SetActive(false);
        }

        //ʰȡ���ʼ��:
        public void PickupInitialization(bool alreadyPicked)
        {
            if (alreadyPicked)
            {
                isAlreadyPicked = true;
                gameObject.SetActive(false);
            }
            else
            {
                isAlreadyPicked = false;
            }
        }
    }
}
