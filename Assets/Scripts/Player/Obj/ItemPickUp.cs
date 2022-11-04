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
        [Header("是消耗品和箭矢时:")]
        public int amount = 1;
        [Header("是否已经拾取:")]
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
        //交互方法:
        public override void Interact(PlayerManager playerManger)
        {
            base.Interact(playerManger);
            PickUpItemByType(playerManger);
        }

        //捡起物品:
        private void PickUpItemByType(PlayerManager playerManger)
        {
            PlayerInventoryManager playerInventory;
            PlayerLocomotionManager playerLocomotion;
            PlayerAnimatorManager animatorHandler;
            playerInventory = playerManger.GetComponent<PlayerInventoryManager>();
            playerLocomotion = playerManger.GetComponent<PlayerLocomotionManager>();
            animatorHandler = playerManger.GetComponentInChildren<PlayerAnimatorManager>();

            playerLocomotion.rigidbody.velocity = Vector3.zero;                                         //拾取物品时停止移动:
            animatorHandler.PlayTargetAnimation("Pick Up Item", true);                             //播放捡起物品动画，并标注正在交互;

            playerManger.itemInteractableObj.GetComponentInChildren<Text>().text = item.itemName;         //获取到其子物体的Text组件，并给出物品信息
            playerManger.itemInteractableObj.SetActive(true);

            switch (itemType)
            {
                case ItemType.Weapon:
                    playerInventory.weaponInventory.Add((WeaponItem)item);                                                //将武器添加到武器库存中;
                    break;
                case ItemType.Helmet:
                    playerInventory.headEquipmentInventory.Add((HelmetEquipment)item);                                                //将武器添加到武器库存中;
                    break;
                case ItemType.Torso:
                    playerInventory.torsoEquipmentInventory.Add((TorsoEquipment)item);                                                //将武器添加到武器库存中;
                    break;
                case ItemType.Leg:
                    playerInventory.legEquipmentInventory.Add((LegEquipment)item);                                                //将武器添加到武器库存中;
                    break;
                case ItemType.Hand:
                    playerInventory.handEquipmentInventory.Add((HandEquipment)item);                                                //将武器添加到武器库存中;
                    break;
                case ItemType.Spell:
                    playerInventory.spellleInventory.Add((SpellItem)item);                                                //将武器添加到武器库存中;
                    break;
                case ItemType.Consumable:
                    playerInventory.AddConsumableItemToInventory((ConsumableItem)item);              //将消耗品添加到武器库存中;
                    break;
                case ItemType.Ring:
                    playerInventory.ringInventory.Add((RingItem)item);                                                //将武器添加到武器库存中;
                    break;
                case ItemType.Ammo:
                    playerInventory.AddAmmoItemToInventory((RangedAmmoItem)item);
                    break;
            }

            isAlreadyPicked = true;
            gameObject.SetActive(false);
        }

        //拾取物初始化:
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
