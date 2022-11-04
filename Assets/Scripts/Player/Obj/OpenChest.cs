using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class OpenChest : Interactable
    {
        public Animator animator;
        public Transform playerStandingPosition;                //玩家站立位置;
        public Collider interactableCollider;                         //交互检查的碰撞器;
        public GameObject itemSpawner;                            //可拾取物品预制体;
        [Header("物品与物品类型:")]
        public Item itemInChest;                            //宝箱内物品;
        public ItemType itemType;

        public AudioClip openChestAudio;
        [Header("是否已经开启:")]
        public bool isAlreadyTurnedOn = false;

        AudioSource audioSource;
        OpenChest openChest;                                           //宝箱脚本;
        private void Awake()
        {
            animator = GetComponent<Animator>();
            openChest = GetComponent<OpenChest>();
            audioSource = GetComponent<AudioSource>();
            interactableCollider = GetComponent<Collider>();
        }

        public override void Interact(PlayerManager playerManger)
        {
            //玩家位置锁定在指定的点的位置:
            //打开宝箱，播放玩家的开启宝箱动画:
            playerManger.OpenChestInteraction(playerStandingPosition);
            animator.Play("Chest_Open");

            //将玩家旋转到面向宝箱:
            Vector3 rotationDir = transform.position - playerManger.transform.position;
            rotationDir.y = 0;
            rotationDir.Normalize();

            Quaternion rt = Quaternion.LookRotation(rotationDir);
            Quaternion targetRotation = Quaternion.Slerp(playerManger.transform.rotation, rt, 300 * Time.deltaTime);
            playerManger.transform.rotation = targetRotation;

            //在玩家可以拾取的箱子内生成一个物品:
            StartCoroutine(SpawnInChest());

            ItemPickUp itemPickUp = itemSpawner.GetComponent<ItemPickUp>();
            if(itemPickUp != null)
            {
                itemPickUp.item = itemInChest;
                itemPickUp.itemType = this.itemType;
            }

            isAlreadyTurnedOn = true;
            interactableCollider.enabled = false;
            audioSource.PlayOneShot(openChestAudio);
        }

        //宝箱初始化:
        public void ChestInitialization(bool isOpen)
        {
            if (isOpen)
            {
                isAlreadyTurnedOn = true;
                interactableCollider.enabled = false;

                animator.Play("Chest_Open");
            }
            else
            {
                isAlreadyTurnedOn = false;
                interactableCollider.enabled = true;
            }
        }

        private IEnumerator SpawnInChest()
        {
            yield return new WaitForSeconds(1); //等待一秒后执行下面的代码
            GameObject obj = Instantiate(itemSpawner,transform);
            obj.transform.localPosition +=new Vector3(0, 0.5f, 0);
            gameObject.layer = LayerMask.NameToLayer("Weapon");
        }
    }
}