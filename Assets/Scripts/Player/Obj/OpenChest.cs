using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class OpenChest : Interactable
    {
        public Animator animator;
        public Transform playerStandingPosition;                //���վ��λ��;
        public Collider interactableCollider;                         //����������ײ��;
        public GameObject itemSpawner;                            //��ʰȡ��ƷԤ����;
        [Header("��Ʒ����Ʒ����:")]
        public Item itemInChest;                            //��������Ʒ;
        public ItemType itemType;

        public AudioClip openChestAudio;
        [Header("�Ƿ��Ѿ�����:")]
        public bool isAlreadyTurnedOn = false;

        AudioSource audioSource;
        OpenChest openChest;                                           //����ű�;
        private void Awake()
        {
            animator = GetComponent<Animator>();
            openChest = GetComponent<OpenChest>();
            audioSource = GetComponent<AudioSource>();
            interactableCollider = GetComponent<Collider>();
        }

        public override void Interact(PlayerManager playerManger)
        {
            //���λ��������ָ���ĵ��λ��:
            //�򿪱��䣬������ҵĿ������䶯��:
            playerManger.OpenChestInteraction(playerStandingPosition);
            animator.Play("Chest_Open");

            //�������ת��������:
            Vector3 rotationDir = transform.position - playerManger.transform.position;
            rotationDir.y = 0;
            rotationDir.Normalize();

            Quaternion rt = Quaternion.LookRotation(rotationDir);
            Quaternion targetRotation = Quaternion.Slerp(playerManger.transform.rotation, rt, 300 * Time.deltaTime);
            playerManger.transform.rotation = targetRotation;

            //����ҿ���ʰȡ������������һ����Ʒ:
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

        //�����ʼ��:
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
            yield return new WaitForSeconds(1); //�ȴ�һ���ִ������Ĵ���
            GameObject obj = Instantiate(itemSpawner,transform);
            obj.transform.localPosition +=new Vector3(0, 0.5f, 0);
            gameObject.layer = LayerMask.NameToLayer("Weapon");
        }
    }
}