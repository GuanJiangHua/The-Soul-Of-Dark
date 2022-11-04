using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG {
    public class ClassSelector : MonoBehaviour
    {
        PlayerManager player;
        //ÿ��ְҵ��һ��ͳ������
        [Header("ְҵ����:")]
        public ClassStats[] classStats;
        //ÿ��ְҵһ��װ��
        [Header("ְҵװ��:")]
        public ClassGear[] classGears;
        [Header("������ֵ�ı�:")]
        public Text healthLevelText;
        public Text staminaLevelText;
        public Text focusLevelText;
        public Text strengthLevelText;
        public Text dexterityLevelText;
        public Text intelligenceLevelText;
        public Text faithLevelText;
        public Text soulsRewardLevelText;
        [Header("ְҵ�����ı�:")]
        public Text classIntroductionText;

        [Header("����Ʒ:")]
        public Image leftWeaponIcon;
        public Image rightWeaponIcon;
        public Image helmetIcon;
        public Image torsoIcon;
        public Image legIcon;
        public Image handIcon;
        public Image extraGiftItemIcon_One;
        public Image extraGiftItemIcon_Two;
        public Image extraGiftItemIcon_Three;
        //�趨ÿ��ְҵ�����Ժ�װ��
        private void OnEnable()
        {
            player = FindObjectOfType<PlayerManager>();
            leftWeaponIcon.enabled = false;
            rightWeaponIcon.enabled = false;
            helmetIcon.enabled = false;
            torsoIcon.enabled = false;
            legIcon.enabled = false;
            handIcon.enabled = false;
            extraGiftItemIcon_One.enabled = false;
            extraGiftItemIcon_Two.enabled = false;
            extraGiftItemIcon_Three.enabled = false;
        }
        //����ְҵ���ԣ�
        private void AssignClassStats(int index)
        {
            player.playerStateManager.healthLevel = classStats[index].healthLevel;
            player.playerStateManager.staminaLevel = classStats[index].staminaLevel;
            player.playerStateManager.focusLevel = classStats[index].focusLevel;
            player.playerStateManager.strengthLevel = classStats[index].strengthLevel;
            player.playerStateManager.dexterityLevel = classStats[index].dexterityLevel;
            player.playerStateManager.intelligenceLevel = classStats[index].intelligenceLevel;
            player.playerStateManager.faithLevel = classStats[index].faithLevel;
            player.playerStateManager.soulsRewardLevel = classStats[index].soulsRewardLevel;

            player.playerStateManager.maxHealth = classStats[index].healthLevel * 10;
            player.playerStateManager.maxFocusPoints = classStats[index].focusLevel * 10;
            player.playerStateManager.maxStamina = classStats[index].staminaLevel * 10;

            player.playerStateManager.currentHealth = player.playerStateManager.maxHealth;
            player.playerStateManager.currentFocusPoints = player.playerStateManager.maxFocusPoints;
            player.playerStateManager.currentStamina = player.playerStateManager.maxStamina;

            healthLevelText.text = classStats[index].healthLevel.ToString("d2");
            staminaLevelText.text = classStats[index].staminaLevel.ToString("d2");
            focusLevelText.text = classStats[index].focusLevel.ToString("d2");
            strengthLevelText.text = classStats[index].strengthLevel.ToString("d2");
            dexterityLevelText.text = classStats[index].dexterityLevel.ToString("d2");
            intelligenceLevelText.text = classStats[index].intelligenceLevel.ToString("d2");
            faithLevelText.text = classStats[index].faithLevel.ToString("d2");
            soulsRewardLevelText.text = classStats[index].soulsRewardLevel.ToString("d2");

            //����ְҵ����:
            classIntroductionText.text = classStats[index].classDescription;
            //����������Ʒ��ͼ��:
            leftWeaponIcon.enabled = true;
            rightWeaponIcon.enabled = true;
            helmetIcon.enabled = true;
            torsoIcon.enabled = true;
            legIcon.enabled = true;
            handIcon.enabled = true;
            extraGiftItemIcon_One.enabled = true;
            extraGiftItemIcon_Two.enabled = true;
            extraGiftItemIcon_Three.enabled = true;

            leftWeaponIcon.sprite = classGears[index].offHandWeapon.itemIcon;
            rightWeaponIcon.sprite = classGears[index].primaryWeapon.itemIcon;
            if (classGears[index].headEquipment != null)
                helmetIcon.sprite = classGears[index].headEquipment.itemIcon;
            else
                helmetIcon.enabled = false;

            if (classGears[index].torsoEquipment != null)
                torsoIcon.sprite = classGears[index].torsoEquipment.itemIcon;
            else
                torsoIcon.enabled = false;

            if (classGears[index].legEquipment != null)
                legIcon.sprite = classGears[index].legEquipment.itemIcon;
            else
                legIcon.enabled = false;

            if (classGears[index].handEquipment != null)
                handIcon.sprite = classGears[index].handEquipment.itemIcon;
            else
                handIcon.enabled = false;

            if (classGears[index].funeraryItemOne != null)
                extraGiftItemIcon_One.sprite = classGears[index].funeraryItemOne.itemIcon;
            else
                extraGiftItemIcon_One.enabled = false;

            if (classGears[index].funeraryItemTwo != null)
            {
                if (classGears[index].funeraryItemTwoAmount <= 0)
                    extraGiftItemIcon_Two.color = new Color(0.5f, 0.5f, 0.5f, 1);
                else
                    extraGiftItemIcon_Two.color = new Color(1, 1, 1, 1);

                extraGiftItemIcon_Two.sprite = classGears[index].funeraryItemTwo.itemIcon;
            }
            else
                extraGiftItemIcon_Two.enabled = false;

            if (classGears[index].funeraryItemTwo != null)
            {
                if (classGears[index].funeraryItemTwoAmount <= 0)
                    extraGiftItemIcon_Three.color = new Color(0.5f, 0.5f, 0.5f, 1);
                else
                    extraGiftItemIcon_Three.color = new Color(1, 1, 1, 1);

                extraGiftItemIcon_Three.sprite = classGears[index].funeraryItemThree.itemIcon;
            }
            else
                extraGiftItemIcon_Three.enabled = false;
        }
        //����id�л�ְҵ:
        public void AssignClassByID(int id)
        {
            AssignClassStats(id);
            //����װ��:
            player.playerInventoryManager.currentHelmetEquipment = classGears[id].headEquipment;
            player.playerInventoryManager.currentTorsoEquipment = classGears[id].torsoEquipment;
            player.playerInventoryManager.currentLegEquipment = classGears[id].legEquipment;
            player.playerInventoryManager.currentHandEquipment = classGears[id].handEquipment;
            //��������:
            player.playerInventoryManager.WeaponRightHandSlots[0] = classGears[id].primaryWeapon;
            player.playerInventoryManager.WeaponLeftHandSlots[0] = classGears[id].offHandWeapon;
            player.playerInventoryManager.rightWeapon = classGears[id].primaryWeapon;
            player.playerInventoryManager.leftWeapon = classGears[id].offHandWeapon;

            player.playerWeaponSlotManger.LoadWeaponHolderOfSlot(); //��������;
            player.playerEquipmentManager.EquipAllEquipmentModels(); //����װ��;

            //������Ʒ:
            player.playerInventoryManager.consumableInventory.Clear();
            ConsumableItem consumable = classGears[id].funeraryItemTwo;
            consumable.currentItemAmount = classGears[id].funeraryItemTwoAmount;
            player.playerInventoryManager.AddConsumableItemToInventory(consumable);

            consumable = classGears[id].funeraryItemThree;
            consumable.currentItemAmount = classGears[id].funeraryItemThreeAmount;
            player.playerInventoryManager.AddConsumableItemToInventory(consumable);

            switch (classGears[id].itemType)
            {
                case ItemType.Consumable:
                    player.playerInventoryManager.AddConsumableItemToInventory(classGears[id].funeraryItemOne as ConsumableItem);
                    break;
                case ItemType.Ammo:
                    player.playerInventoryManager.rangedAmmoInventory.Clear();
                    player.playerInventoryManager.AddAmmoItemToInventory(classGears[id].funeraryItemOne as RangedAmmoItem);
                    break;
                case ItemType.Spell:
                    player.playerInventoryManager.spellleInventory.Clear();
                    player.playerInventoryManager.spellleInventory.Add(classGears[id].funeraryItemOne as SpellItem);
                    break;
            }
        }

        //ж��װ����
        public void UninstallEquipment()
        {
            if(player == null)
            {
                player = FindObjectOfType<PlayerManager>();
            }
            player.playerInventoryManager.rightWeapon = player.playerInventoryManager.unarmedWeapon;
            player.playerInventoryManager.leftWeapon = player.playerInventoryManager.unarmedWeapon;
            player.playerWeaponSlotManger.LoadWeaponHolderOfSlot();
            player.playerEquipmentManager.UninstallEquipment();
        }
    }
}