using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class MemorySpellWindow : MonoBehaviour
    {
        [Header("库存中的法术:")]
        public SpellItem currentSelectSpell;            //已经选择的法术;
        public Transform spellInventorySlotParent;
        SpellInventorySlot[] spellInventorySlotArray;
        [Header("法术记忆空格槽:")]
        public Transform spellMemorySlotParent;
        SpellMemorySlot[] spellMemorySlotArray;
        [Header("装备处置窗口:")]
        public RectTransform equipmentDisposalWindow;

        [Header("详情窗口:")]
        public Image spelIcon;
        public Text spellNameText;
        public Text spellTypeText;
        public Text focusPointCostValueText;
        public Text occupiedSpellGridValueText;
        public Text requiredIntelligenceLevelText;
        public Text requiredFaithLevelText;
        public Text spellDescribeText;

        UIManager uiManager;
        //方法:----------------------------------------
        private void Awake()
        {
            uiManager = FindObjectOfType<UIManager>();
        }
        //更新库存法术槽:
        public void UpdateSpellInventorySlot()
        {
            spellInventorySlotArray = GetChildSlotComponent<SpellInventorySlot>(spellInventorySlotParent);
            PlayerInventoryManager playerInventory = uiManager.playerManager.playerInventoryManager;
            List<SpellItem> spllList = playerInventory.spellleInventory;                       //法术库存列表
            SpellItem[] memorySpellArray = playerInventory.memorySpellArray;        //记忆法术数组
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
        //库存槽交互按钮禁用:
        private void SpellInventorySlotButtonDisable()
        {
            spellInventorySlotArray = GetChildSlotComponent<SpellInventorySlot>(spellInventorySlotParent);
            foreach(var slot in spellInventorySlotArray)
            {
                slot.DisableSelectionButton();
            }
        }
        //库存槽交互按钮启用:
        private void SpellInventorySlotButtonEnable()
        {
            PlayerInventoryManager playerInventory = uiManager.playerManager.playerInventoryManager;
            int availableMemorySlotNumber = AvailableMemorySlotNumber(playerInventory);

            spellInventorySlotArray = GetChildSlotComponent<SpellInventorySlot>(spellInventorySlotParent);
            foreach (var slot in spellInventorySlotArray)
            {
                if (slot.mySpell != null && slot.mySpell.occupiedSpellGrid <= availableMemorySlotNumber) //法术不为空,并且占据的法术记忆空格小于等于可用空格,才启用
                    slot.EnableSelectionButton();

                if (slot.mySpell != null && slot.isEquipment == true)
                    slot.EnableSelectionButton();
            }
        }

        //打开装备处置窗口:
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
        //关闭装备处置窗口:
        public void CloseEquipmentDisposalWindow()
        {
            equipmentDisposalWindow.gameObject.SetActive(false);
            currentSelectSpell = null;
            UpdateSpellDetail(null);
            SpellInventorySlotButtonEnable();
        }

        //装备当前选择的法术:
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
        //卸下当前选择的法术:
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

        //更新法术详情:
        public void UpdateSpellDetail(SpellItem spell)
        {
            if(spell != null)
            {
                spelIcon.sprite = spell.itemIcon;
                spelIcon.enabled = true;

                spellNameText.text = spell.itemName;
                if (spell.isMagicSpell)
                    spellTypeText.text = "魔法";
                if (spell.isPyroSpell)
                    spellTypeText.text = "咒术";
                if (spell.isFaithSpell)
                    spellTypeText.text = "奇迹";
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

                spellNameText.text = "法术名称";
                spellTypeText.text = "法术类型";
                focusPointCostValueText.text = "--";
                occupiedSpellGridValueText.text = "--";
                requiredIntelligenceLevelText.text = "00";
                requiredFaithLevelText.text = "00";

                spellDescribeText.text = "法术描述--";
            }
        }

        //计算可用记忆空格：
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
        //更新记忆空格槽:
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
        //返回一个空的记忆空格槽:
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

            Debug.Log("没有空的空格槽!");
            return null;
        }

        //获取物体所有子对象的库存槽组件数组:
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
