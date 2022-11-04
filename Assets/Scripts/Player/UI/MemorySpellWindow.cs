using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class MemorySpellWindow : MonoBehaviour
    {
        [Header("����еķ���:")]
        public SpellItem currentSelectSpell;            //�Ѿ�ѡ��ķ���;
        public Transform spellInventorySlotParent;
        SpellInventorySlot[] spellInventorySlotArray;
        [Header("��������ո��:")]
        public Transform spellMemorySlotParent;
        SpellMemorySlot[] spellMemorySlotArray;
        [Header("װ�����ô���:")]
        public RectTransform equipmentDisposalWindow;

        [Header("���鴰��:")]
        public Image spelIcon;
        public Text spellNameText;
        public Text spellTypeText;
        public Text focusPointCostValueText;
        public Text occupiedSpellGridValueText;
        public Text requiredIntelligenceLevelText;
        public Text requiredFaithLevelText;
        public Text spellDescribeText;

        UIManager uiManager;
        //����:----------------------------------------
        private void Awake()
        {
            uiManager = FindObjectOfType<UIManager>();
        }
        //���¿�淨����:
        public void UpdateSpellInventorySlot()
        {
            spellInventorySlotArray = GetChildSlotComponent<SpellInventorySlot>(spellInventorySlotParent);
            PlayerInventoryManager playerInventory = uiManager.playerManager.playerInventoryManager;
            List<SpellItem> spllList = playerInventory.spellleInventory;                       //��������б�
            SpellItem[] memorySpellArray = playerInventory.memorySpellArray;        //���䷨������
            for (int i= 0; i < spellInventorySlotArray.Length; i++)
            {
                if(i< spllList.Count)
                {
                    bool isEquipmentIcon = false;
                    for(int j = 0; j < memorySpellArray.Length; j++)
                    {
                        if(memorySpellArray[j] != null && spllList[i].itemName == memorySpellArray[j].itemName)
                        {
                            isEquipmentIcon = true;
                            break;
                        }
                    }
                    spellInventorySlotArray[i].AddSpell(spllList[i], isEquipmentIcon);
                }
                else
                {
                    spellInventorySlotArray[i].RemoveSpell();
                }
            }

            SpellInventorySlotButtonEnable();

            UpdateSpellMemorySlot(playerInventory);
        }
        //���۽�����ť����:
        private void SpellInventorySlotButtonDisable()
        {
            spellInventorySlotArray = GetChildSlotComponent<SpellInventorySlot>(spellInventorySlotParent);
            foreach(var slot in spellInventorySlotArray)
            {
                slot.DisableSelectionButton();
            }
        }
        //���۽�����ť����:
        private void SpellInventorySlotButtonEnable()
        {
            PlayerInventoryManager playerInventory = uiManager.playerManager.playerInventoryManager;
            int availableMemorySlotNumber = AvailableMemorySlotNumber(playerInventory);

            spellInventorySlotArray = GetChildSlotComponent<SpellInventorySlot>(spellInventorySlotParent);
            foreach (var slot in spellInventorySlotArray)
            {
                if (slot.mySpell != null && slot.mySpell.occupiedSpellGrid <= availableMemorySlotNumber) //������Ϊ��,����ռ�ݵķ�������ո�С�ڵ��ڿ��ÿո�,������
                    slot.EnableSelectionButton();

                if (slot.mySpell != null && slot.isEquipment == true)
                    slot.EnableSelectionButton();
            }
        }

        //��װ�����ô���:
        public void OpenEquipmentDisposalWindow(RectTransform rectTransform, SpellInventorySlot slot)
        {
            equipmentDisposalWindow.gameObject.SetActive(true);
            Vector2 pos = rectTransform.position;

            equipmentDisposalWindow.position = pos;
            SpellInventorySlotButtonDisable();

            currentSelectSpell = slot.mySpell;

            if (slot.isEquipment)
            {
                equipmentDisposalWindow.GetChild(0).gameObject.SetActive(false);
                equipmentDisposalWindow.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                equipmentDisposalWindow.GetChild(0).gameObject.SetActive(true);
                equipmentDisposalWindow.GetChild(1).gameObject.SetActive(false);
            }

            UpdateSpellDetail(slot.mySpell);
        }
        //�ر�װ�����ô���:
        public void CloseEquipmentDisposalWindow()
        {
            equipmentDisposalWindow.gameObject.SetActive(false);
            currentSelectSpell = null;
            UpdateSpellDetail(null);
            SpellInventorySlotButtonEnable();
        }

        //װ����ǰѡ��ķ���:
        public void EquipmentCurrentSelectSpell()
        {
            PlayerInventoryManager playerInventory = uiManager.playerManager.playerInventoryManager;
            int availableMemorySlotNumber = AvailableMemorySlotNumber(playerInventory);

            if(currentSelectSpell!=null && currentSelectSpell.occupiedSpellGrid <= availableMemorySlotNumber)
            {
                for(int i=0;i<playerInventory.memorySpellArray.Length;i++)
                {
                    if(playerInventory.memorySpellArray[i] == null)
                    {
                        playerInventory.AddSpellItemToSlots(currentSelectSpell, i);
                        break;
                    }
                }
            }

            UpdateSpellInventorySlot();
            CloseEquipmentDisposalWindow();

            playerInventory.ChangeSpell();
        }
        //ж�µ�ǰѡ��ķ���:
        public void RemoveCurrentSelectSpell()
        {
            PlayerInventoryManager playerInventory = uiManager.playerManager.playerInventoryManager;
            if (currentSelectSpell != null)
            {
                for (int i = 0; i < playerInventory.memorySpellArray.Length; i++)
                {
                    if (playerInventory.memorySpellArray[i] != null && playerInventory.memorySpellArray[i].itemName == currentSelectSpell.itemName)
                    {
                        playerInventory.AddSpellItemToSlots(null, i);
                        break;
                    }
                }
            }

            UpdateSpellInventorySlot();
            CloseEquipmentDisposalWindow();

            playerInventory.ChangeSpell();
        }

        //���·�������:
        public void UpdateSpellDetail(SpellItem spell)
        {
            if(spell != null)
            {
                spelIcon.sprite = spell.itemIcon;
                spelIcon.enabled = true;

                spellNameText.text = spell.itemName;
                if (spell.isMagicSpell)
                    spellTypeText.text = "ħ��";
                if (spell.isPyroSpell)
                    spellTypeText.text = "����";
                if (spell.isFaithSpell)
                    spellTypeText.text = "�漣";
                focusPointCostValueText.text = spell.focusPointCost.ToString("d2");
                occupiedSpellGridValueText.text = spell.occupiedSpellGrid.ToString();
                requiredIntelligenceLevelText.text = spell.requiredIntelligenceLevel.ToString("d2");
                requiredFaithLevelText.text = spell.requiredFaithLevel.ToString("d2");

                spellDescribeText.text = spell.spellDescription;
            }
            else
            {
                spelIcon.sprite = null;
                spelIcon.enabled = false;

                spellNameText.text = "��������";
                spellTypeText.text = "��������";
                focusPointCostValueText.text = "--";
                occupiedSpellGridValueText.text = "--";
                requiredIntelligenceLevelText.text = "00";
                requiredFaithLevelText.text = "00";

                spellDescribeText.text = "��������--";
            }
        }

        //������ü���ո�
        private int AvailableMemorySlotNumber(PlayerInventoryManager playerInventory)
        {
            int availableMemorySlotNumber = playerInventory.enabledMemorySpellSlotNumber;
            foreach(SpellItem spell in playerInventory.memorySpellArray)
            {
                if (spell != null)
                {
                    availableMemorySlotNumber -= spell.occupiedSpellGrid;
                }
            }

            return availableMemorySlotNumber;
        }
        //���¼���ո��:
        private void UpdateSpellMemorySlot(PlayerInventoryManager playerInventory)
        {
            SpellItem[] spellMemoryArray = playerInventory.memorySpellArray;
            spellMemorySlotArray = GetChildSlotComponent<SpellMemorySlot>(spellMemorySlotParent);

            for (int i = 0; i < spellMemorySlotArray.Length; i++)
            {
                spellMemorySlotArray[i].DisableMemorySlot();
                if (i < playerInventory.enabledMemorySpellSlotNumber)
                    spellMemorySlotArray[i].gameObject.SetActive(true);
                else
                    spellMemorySlotArray[i].gameObject.SetActive(false);
            }

            for (int i= 0;i< spellMemoryArray.Length; i++)
            {
                if(spellMemoryArray[i] != null)
                {
                    for (int j = 0; j < spellMemoryArray[i].occupiedSpellGrid; j++)
                    {
                        var activeSelfSlot = GetEmptyMemorySpaceSlot(spellMemorySlotArray);
                        activeSelfSlot.EnableMemorySlot(spellMemoryArray[i]);

                        if (j != 0)
                            activeSelfSlot.icon.color = new Color(0.5f, 0.5f, 0.5f);
                        else
                            activeSelfSlot.icon.color = new Color(1, 1, 1);
                    }
                }
            }
        }
        //����һ���յļ���ո��:
        private SpellMemorySlot GetEmptyMemorySpaceSlot(SpellMemorySlot[] spellMemorieSlotArray)
        {
            if(spellInventorySlotArray != null && spellInventorySlotArray.Length > 0)
            {
                foreach(SpellMemorySlot slot in spellMemorieSlotArray)
                {
                    if(slot.mySpell == null && slot.gameObject.activeSelf)
                    {
                        return slot;
                    }
                }
            }

            Debug.Log("û�пյĿո��!");
            return null;
        }

        //��ȡ���������Ӷ���Ŀ����������:
        private T[] GetChildSlotComponent<T>(Transform parent)
        {
            T[] array = new T[parent.childCount];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = parent.GetChild(i).GetComponent<T>();
            }

            return array;
        }
    }
}
