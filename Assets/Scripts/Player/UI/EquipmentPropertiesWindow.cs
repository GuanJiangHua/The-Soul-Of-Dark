using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class EquipmentPropertiesWindow : MonoBehaviour
    {
        [Header("���������ı�:")]
        public Text equipmentNameText;
        [Header("���������ı�:")]
        public Text equipmentTypeText;
        [Header("���߽����ı�:")]
        public Text equipmentDescriptionText;
        [Header("����ͼͼƬ:")]
        public Image equipmentIcon;
        [Header("��ǰ���ߵķ�����ֵ�ı�:")]
        public Text currentPhysicalDefenseText;        //�����˺�����;
        public Text currentFireDefenseText;               //�����˺�����;
        public Text currentMagicDefenseText;           //ħ���˺�����;
        public Text currentLightningDefenseText;     //�׵��˺�����;
        public Text currentDarkDefenseText;             //�ڰ��˺�����;
        [Header("�Աȷ��ߵķ�����ֵ�ı�")]
        public Text contrastPhysicalDefenseText;        //�����˺�����;
        public Text contrastFireDefenseText;               //�����˺�����;
        public Text contrastMagicDefenseText;           //ħ���˺�����;
        public Text contrastLightningDefenseText;     //�׵��˺�����;
        public Text contrastDarkDefenseText;             //�ڰ��˺�����;

        [Header("��ǰ���ߵĵֿ���ֵ�ı�:")]
        public Text currentPoisonDefenseText;                 //�����Եֿ���;
        public Text currentFrostDefenseText;                    //�������Եֿ���;
        public Text currentHemorrhageDefenseText;       //��Ѫ���Եֿ���;
        public Text currentCurseDefenseText;                  //��Ѫ���Եֿ���;
        [Header("�Աȷ��ߵĵֿ���ֵ�ı�:")]
        public Text contrastPoisonDefenseText;                 //�����Եֿ���;
        public Text contrastFrostDefenseText;                    //�������Եֿ���;
        public Text contrastHemorrhageDefenseText;       //��Ѫ���Եֿ���;
        public Text contrastCurseDefenseText;                  //��Ѫ���Եֿ���;
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
                string equipmentType = "װ������";
                if (equipmentItem.isHelmet)
                {
                    equipmentType = "ͷ��";
                    UpdateEquipmentDefense(playerInventoryManager.currentHelmetEquipment, equipmentItem);
                    UpdateEquipmentResistance(playerInventoryManager.currentHelmetEquipment, equipmentItem);
                }
                else if (equipmentItem.isTorso)
                {
                    equipmentType = "�ؼ�";
                    UpdateEquipmentDefense(playerInventoryManager.currentTorsoEquipment, equipmentItem);
                    UpdateEquipmentResistance(playerInventoryManager.currentTorsoEquipment, equipmentItem);
                }
                else if (equipmentItem.isLeg)
                {
                    equipmentType = "�ȼ�";
                    UpdateEquipmentDefense(playerInventoryManager.currentLegEquipment, equipmentItem);
                    UpdateEquipmentResistance(playerInventoryManager.currentLegEquipment, equipmentItem);
                }
                else if (equipmentItem.isHand)
                {
                    equipmentType = "����";
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

        //���·���:
        private void UpdateEquipmentDefense(EquipmentItem currentEquipment , EquipmentItem contrastEquipment)
        {
            //��ǰ��������:
            int currentPhysicalDefense = 0;        //��������ӳ�;
            int currentFireDefense = 0;               //�����˺�����;
            int currentMagicDefense = 0;           //ħ���˺�����;
            int currentLightningDefense = 0;     //�׵��˺�����;
            int currentDarkDefense = 0;             //�ڰ��˺�����;
            //�Ա���������:
            int contrastPhysicalDefense = 0;        //��������ӳ�;
            int contrastFireDefense = 0;               //�����˺�����;
            int contrastMagicDefense = 0;           //ħ���˺�����;
            int contrastLightningDefense = 0;     //�׵��˺�����;
            int contrastDarkDefense = 0;             //�ڰ��˺�����;

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

            //��ɫ:
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
            //��ֵ:
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
        //���µֿ���:
        private void UpdateEquipmentResistance(EquipmentItem currentEquipment, EquipmentItem contrastEquipment)
        {
            //��ǰװ���ֿ���:
            int currentPoisonDefense = 0;                 //�����Եֿ���;
            int currentFrostDefense = 0;                    //�������Եֿ���;
            int currentHemorrhageDefense = 0;       //��Ѫ���Եֿ���;
            int currentCurseDefense = 0;                  //��Ѫ���Եֿ���;
            //�Ա�װ���ֿ���:
            int contrastPoisonDefense = 0;                 //�����Եֿ���;
            int contrastFrostDefense = 0;                    //�������Եֿ���;
            int contrastHemorrhageDefense = 0;       //��Ѫ���Եֿ���;
            int contrastCurseDefense = 0;                  //��Ѫ���Եֿ���;

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
        
            //�ı���ɫ:
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

            //��ֵ:
            currentPoisonDefenseText.text = currentPoisonDefense.ToString("d2");
            currentFrostDefenseText.text = currentFrostDefense.ToString("d2");
            currentHemorrhageDefenseText.text = currentHemorrhageDefense.ToString("d2");
            currentCurseDefenseText.text = currentCurseDefense.ToString("d2");

            contrastPoisonDefenseText.text = contrastPoisonDefense.ToString("d2");
            contrastFrostDefenseText.text = contrastFrostDefense.ToString("d2");
            contrastHemorrhageDefenseText.text = contrastHemorrhageDefense.ToString("d2");
            contrastCurseDefenseText.text = contrastCurseDefense.ToString("d2");
        }
        //��������,����,����,ͼƬ:
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
                equipmentNameText.text = "װ������";
                equipmentTypeText.text = "װ������";
                equipmentDescriptionText.text = "װ������--";
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