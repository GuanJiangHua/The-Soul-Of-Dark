using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class EquipmentPropertiesWindow : MonoBehaviour
    {
        [Header("防具名称文本:")]
        public Text equipmentNameText;
        [Header("防具类型文本:")]
        public Text equipmentTypeText;
        [Header("防具介绍文本:")]
        public Text equipmentDescriptionText;
        [Header("防具图图片:")]
        public Image equipmentIcon;
        [Header("当前防具的防御力值文本:")]
        public Text currentPhysicalDefenseText;        //物理伤害抗性;
        public Text currentFireDefenseText;               //火焰伤害抗性;
        public Text currentMagicDefenseText;           //魔力伤害抗性;
        public Text currentLightningDefenseText;     //雷电伤害抗性;
        public Text currentDarkDefenseText;             //黑暗伤害抗性;
        [Header("对比防具的防御力值文本")]
        public Text contrastPhysicalDefenseText;        //物理伤害抗性;
        public Text contrastFireDefenseText;               //火焰伤害抗性;
        public Text contrastMagicDefenseText;           //魔力伤害抗性;
        public Text contrastLightningDefenseText;     //雷电伤害抗性;
        public Text contrastDarkDefenseText;             //黑暗伤害抗性;

        [Header("当前防具的抵抗力值文本:")]
        public Text currentPoisonDefenseText;                 //毒属性抵抗力;
        public Text currentFrostDefenseText;                    //寒冷属性抵抗力;
        public Text currentHemorrhageDefenseText;       //出血属性抵抗力;
        public Text currentCurseDefenseText;                  //出血属性抵抗力;
        [Header("对比防具的抵抗力值文本:")]
        public Text contrastPoisonDefenseText;                 //毒属性抵抗力;
        public Text contrastFrostDefenseText;                    //寒冷属性抵抗力;
        public Text contrastHemorrhageDefenseText;       //出血属性抵抗力;
        public Text contrastCurseDefenseText;                  //出血属性抵抗力;
        private void Awake()
        {

        }
        private void OnEnable()
        {
            UpdateEquipmentDefense(null, null);
            UpdateEquipmentResistance(null, null);
            UpdateEquipmentNameAndDescriptionAndTypeAndIcon(null, null);
        }
        public void UpdateUIWindow(EquipmentItem equipmentItem, PlayerInventoryManager playerInventoryManager)
        {
            if (equipmentItem != null)
            {
                string equipmentType = "装备类型";
                if (equipmentItem.isHelmet)
                {
                    equipmentType = "头盔";
                    UpdateEquipmentDefense(playerInventoryManager.currentHelmetEquipment, equipmentItem);
                    UpdateEquipmentResistance(playerInventoryManager.currentHelmetEquipment, equipmentItem);
                }
                else if (equipmentItem.isTorso)
                {
                    equipmentType = "胸甲";
                    UpdateEquipmentDefense(playerInventoryManager.currentTorsoEquipment, equipmentItem);
                    UpdateEquipmentResistance(playerInventoryManager.currentTorsoEquipment, equipmentItem);
                }
                else if (equipmentItem.isLeg)
                {
                    equipmentType = "腿甲";
                    UpdateEquipmentDefense(playerInventoryManager.currentLegEquipment, equipmentItem);
                    UpdateEquipmentResistance(playerInventoryManager.currentLegEquipment, equipmentItem);
                }
                else if (equipmentItem.isHand)
                {
                    equipmentType = "护手";
                    UpdateEquipmentDefense(playerInventoryManager.currentHandEquipment, equipmentItem);
                    UpdateEquipmentResistance(playerInventoryManager.currentHandEquipment, equipmentItem);
                }

                UpdateEquipmentNameAndDescriptionAndTypeAndIcon(equipmentItem, equipmentType);
            }
            else
            {
                UpdateEquipmentDefense(null, null);
                UpdateEquipmentResistance(null, null);
                UpdateEquipmentNameAndDescriptionAndTypeAndIcon(null, null);
            }
        }

        public void UpdateUIWindow()
        {
            UpdateEquipmentDefense(null, null);
            UpdateEquipmentResistance(null, null);
            UpdateEquipmentNameAndDescriptionAndTypeAndIcon(null, null);
        }

        //更新防御:
        private void UpdateEquipmentDefense(EquipmentItem currentEquipment , EquipmentItem contrastEquipment)
        {
            //当前武器属性:
            int currentPhysicalDefense = 0;        //物理防御加成;
            int currentFireDefense = 0;               //火焰伤害抗性;
            int currentMagicDefense = 0;           //魔力伤害抗性;
            int currentLightningDefense = 0;     //雷电伤害抗性;
            int currentDarkDefense = 0;             //黑暗伤害抗性;
            //对比武器属性:
            int contrastPhysicalDefense = 0;        //物理防御加成;
            int contrastFireDefense = 0;               //火焰伤害抗性;
            int contrastMagicDefense = 0;           //魔力伤害抗性;
            int contrastLightningDefense = 0;     //雷电伤害抗性;
            int contrastDarkDefense = 0;             //黑暗伤害抗性;

            if (currentEquipment != null)
            {
                currentPhysicalDefense = Mathf.RoundToInt(currentEquipment.physicalDefense);
                currentFireDefense = Mathf.RoundToInt(currentEquipment.fireDefense);
                currentMagicDefense = Mathf.RoundToInt(currentEquipment.magicDefense);
                currentLightningDefense = Mathf.RoundToInt(currentEquipment.lightningDefense);
                currentDarkDefense = Mathf.RoundToInt(currentEquipment.darkDefense);
            }

            if(contrastEquipment != null)
            {
                contrastPhysicalDefense = Mathf.RoundToInt(contrastEquipment.physicalDefense);
                contrastFireDefense = Mathf.RoundToInt(contrastEquipment.fireDefense);
                contrastMagicDefense = Mathf.RoundToInt(contrastEquipment.magicDefense);
                contrastLightningDefense = Mathf.RoundToInt(contrastEquipment.lightningDefense);
                contrastDarkDefense = Mathf.RoundToInt(contrastEquipment.darkDefense);
            }

            //颜色:
            if(contrastPhysicalDefense == currentPhysicalDefense)
            {
                contrastPhysicalDefenseText.color = Color.white;
            }
            else if(contrastPhysicalDefense < currentPhysicalDefense)
            {
                contrastPhysicalDefenseText.color = Color.red;
            }
            else if(contrastPhysicalDefense > currentPhysicalDefense)
            {
                contrastPhysicalDefenseText.color = Color.blue;
            }

            if (contrastFireDefense == currentFireDefense)
            {
                contrastFireDefenseText.color = Color.white;
            }
            else if (contrastFireDefense < currentFireDefense)
            {
                contrastFireDefenseText.color = Color.red;
            }
            else if (contrastFireDefense > currentFireDefense)
            {
                contrastFireDefenseText.color = Color.blue;
            }

            if (contrastMagicDefense == currentMagicDefense)
            {
                contrastMagicDefenseText.color = Color.white;
            }
            else if (contrastMagicDefense < currentMagicDefense)
            {
                contrastMagicDefenseText.color = Color.red;
            }
            else if (contrastMagicDefense > currentMagicDefense)
            {
                contrastMagicDefenseText.color = Color.blue;
            }

            if (contrastLightningDefense == currentLightningDefense)
            {
                contrastLightningDefenseText.color = Color.white;
            }
            else if (contrastLightningDefense < currentLightningDefense)
            {
                contrastLightningDefenseText.color = Color.red;
            }
            else if (contrastLightningDefense > currentLightningDefense)
            {
                contrastLightningDefenseText.color = Color.blue;
            }

            if (contrastDarkDefense == currentDarkDefense)
            {
                contrastDarkDefenseText.color = Color.white;
            }
            else if (contrastDarkDefense < currentDarkDefense)
            {
                contrastDarkDefenseText.color = Color.red;
            }
            else if (contrastDarkDefense > currentDarkDefense)
            {
                contrastDarkDefenseText.color = Color.blue;
            }
            //赋值:
            currentPhysicalDefenseText.text = currentPhysicalDefense.ToString("d2");
            currentFireDefenseText.text = currentFireDefense.ToString("d2");
            currentMagicDefenseText.text = currentMagicDefense.ToString("d2");
            currentLightningDefenseText.text = currentLightningDefense.ToString("d2");
            currentDarkDefenseText.text = currentDarkDefense.ToString("d2");

            contrastPhysicalDefenseText.text = contrastPhysicalDefense.ToString("d2");
            contrastFireDefenseText.text = contrastFireDefense.ToString("d2");
            contrastMagicDefenseText.text = contrastMagicDefense.ToString("d2");
            contrastLightningDefenseText.text = contrastLightningDefense.ToString("d2");
            contrastDarkDefenseText.text = contrastDarkDefense.ToString("d2");
        }
        //更新抵抗力:
        private void UpdateEquipmentResistance(EquipmentItem currentEquipment, EquipmentItem contrastEquipment)
        {
            //当前装备抵抗力:
            int currentPoisonDefense = 0;                 //毒属性抵抗力;
            int currentFrostDefense = 0;                    //寒冷属性抵抗力;
            int currentHemorrhageDefense = 0;       //出血属性抵抗力;
            int currentCurseDefense = 0;                  //出血属性抵抗力;
            //对比装备抵抗力:
            int contrastPoisonDefense = 0;                 //毒属性抵抗力;
            int contrastFrostDefense = 0;                    //寒冷属性抵抗力;
            int contrastHemorrhageDefense = 0;       //出血属性抵抗力;
            int contrastCurseDefense = 0;                  //出血属性抵抗力;

            if(currentEquipment != null)
            {
                currentPoisonDefense = (int)(currentEquipment.poisonDefense * 100);                 
                currentFrostDefense = (int)(currentEquipment.frostDefense * 100); ;                    
                currentHemorrhageDefense = (int)(currentEquipment.hemorrhageDefense * 100); ;       
                currentCurseDefense = (int)(currentEquipment.curseDefense * 100); ;                  
            }

            if(contrastEquipment != null)
            {
                contrastPoisonDefense = (int)(contrastEquipment.poisonDefense * 100);
                contrastFrostDefense = (int)(contrastEquipment.frostDefense * 100); ;
                contrastHemorrhageDefense = (int)(contrastEquipment.hemorrhageDefense * 100); ;
                contrastCurseDefense = (int)(contrastEquipment.curseDefense * 100); ;
            }
        
            //文本颜色:
            if(currentPoisonDefense == contrastPoisonDefense)
            {
                contrastPoisonDefenseText.color = Color.white;
            }
            else if(currentPoisonDefense > contrastPoisonDefense)
            {
                contrastPoisonDefenseText.color = Color.red;
            }
            else if (currentPoisonDefense < contrastPoisonDefense)
            {
                contrastPoisonDefenseText.color = Color.blue;
            }

            if (currentFrostDefense == contrastFrostDefense)
            {
                contrastFrostDefenseText.color = Color.white;
            }
            else if (currentFrostDefense > contrastFrostDefense)
            {
                contrastFrostDefenseText.color = Color.red;
            }
            else if (currentFrostDefense < contrastFrostDefense)
            {
                contrastFrostDefenseText.color = Color.blue;
            }

            if (currentHemorrhageDefense == contrastHemorrhageDefense)
            {
                contrastHemorrhageDefenseText.color = Color.white;
            }
            else if (currentHemorrhageDefense > contrastHemorrhageDefense)
            {
                contrastHemorrhageDefenseText.color = Color.red;
            }
            else if (currentHemorrhageDefense < contrastHemorrhageDefense)
            {
                contrastHemorrhageDefenseText.color = Color.blue;
            }

            if (currentCurseDefense == contrastCurseDefense)
            {
                contrastCurseDefenseText.color = Color.white;
            }
            else if (currentCurseDefense > contrastCurseDefense)
            {
                contrastCurseDefenseText.color = Color.red;
            }
            else if (currentCurseDefense < contrastCurseDefense)
            {
                contrastCurseDefenseText.color = Color.blue;
            }

            //赋值:
            currentPoisonDefenseText.text = currentPoisonDefense.ToString("d2");
            currentFrostDefenseText.text = currentFrostDefense.ToString("d2");
            currentHemorrhageDefenseText.text = currentHemorrhageDefense.ToString("d2");
            currentCurseDefenseText.text = currentCurseDefense.ToString("d2");

            contrastPoisonDefenseText.text = contrastPoisonDefense.ToString("d2");
            contrastFrostDefenseText.text = contrastFrostDefense.ToString("d2");
            contrastHemorrhageDefenseText.text = contrastHemorrhageDefense.ToString("d2");
            contrastCurseDefenseText.text = contrastCurseDefense.ToString("d2");
        }
        //更新名字,介绍,类型,图片:
        private void UpdateEquipmentNameAndDescriptionAndTypeAndIcon(EquipmentItem equipment , string equipmentType)
        {
            if (equipment != null)
            {
                equipmentNameText.text = equipment.itemName;
                equipmentTypeText.text = equipmentType;
                equipmentDescriptionText.text = equipment.equipmentIDescription;
                equipmentIcon.sprite = equipment.itemIcon;
            }
            else
            {
                equipmentNameText.text = "装备名称";
                equipmentTypeText.text = "装备类型";
                equipmentDescriptionText.text = "装备介绍--";
                equipmentIcon.sprite = null;
            }

            if(equipmentIcon.sprite == null)
            {
                equipmentIcon.enabled = false;
            }
            else
            {
                equipmentIcon.enabled = true;
            }
        }
    }
}