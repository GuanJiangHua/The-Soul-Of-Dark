using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class WeaponPropertiesWindow : MonoBehaviour
    {
        public UIManager uiManager;
        //������
        [Header("����ͼ������:")]
        public Image weaponIcon;
        [Header("���������ı�����:")]
        public Text weaponNameText;
        //��������:
        [Header("������������:")]
        public Text weaponDescription;
        //��������:
        [Header("�˺�ֵ����:")]
        public Text physicsValueText;
        public Text magicValueText;
        public Text fierValueText;
        public Text lightningValueText;
        public Text darkValueText;
        public Text criticalDamageMuiltiplierText;  //�����˺�����
        //��������:
        [Header("����ֵ����:")]
        public Text physicsBlockValueText;
        public Text magicBlockValueText;
        public Text fierBlockValueText;
        public Text lightningBlockValueText;
        public Text darkBlockValueText;
        public Text blockingForceValueText;
        //����Ч��:
        [Header("�쳣Ч������:")]
        public Text poisonText;                                     //�������ۻ�;
        public Text frostText;                                        //���������ۻ�;
        public Text hemorrhageText;                            //��ѪЧ���ۻ�;
        [Header("���Լӳ�����:")]
        public Text strengthAdditionText;
        public Text dexterityAdditionText;
        public Text intelligenceAdditionText;
        public Text faithAdditionText;

        private void Awake()
        {
            uiManager = FindObjectOfType<UIManager>();
        }

        private void OnEnable()
        {
            UpdateAllDamageValue(null, null);
            UpdateAllBlockValue(null, null);
            UpdateAllAbnormalEffect(null, null);
            UpdateAllAttributeRankAddition(null, null);
            UpdateWeaponDescriptionAndName(null);
        }
        //����ui����:[����¼�����:]
        public void UpdateUIWindow(WeaponItem weaponItem , PlayerInventoryManager playerInventoryManager)
        {
            if(uiManager.leftHandSlot01Selected || uiManager.leftHandSlot02Selected || uiManager.leftHandSlot03Selected)
            {
                UpdateAllDamageValue(playerInventoryManager.leftWeapon, weaponItem);
                UpdateAllBlockValue(playerInventoryManager.leftWeapon, weaponItem);
                UpdateAllAbnormalEffect(playerInventoryManager.leftWeapon, weaponItem);
                UpdateAllAttributeRankAddition(playerInventoryManager.leftWeapon, weaponItem);
            }
            else if(uiManager.rightHandSlot01Selected || uiManager.rightHandSlot02Selected || uiManager.rightHandSlot03Selected)
            {
                UpdateAllDamageValue(playerInventoryManager.rightWeapon, weaponItem);
                UpdateAllBlockValue(playerInventoryManager.rightWeapon, weaponItem);
                UpdateAllAbnormalEffect(playerInventoryManager.rightWeapon, weaponItem);
                UpdateAllAttributeRankAddition(playerInventoryManager.rightWeapon, weaponItem);
            }

            UpdateItemIcon(weaponItem);
            UpdateWeaponDescriptionAndName(weaponItem);
        }
        public void UpdateUIWindow(RangedAmmoItem ammo, PlayerInventoryManager playerInventoryManager, bool isAmmo)
        {
            Debug.Log("�������Է�������:");
            if (uiManager.inventoryWindonManager.ammoSlotIndex == 1)
            {
                UpdateAllDamageValue(playerInventoryManager.currentBow, ammo, true);
                UpdateAllBlockValue(playerInventoryManager.currentBow, ammo, true);
                UpdateAllAbnormalEffect(playerInventoryManager.currentBow, ammo, true);
                UpdateAllAttributeRankAddition(playerInventoryManager.currentBow, ammo, true);
            }
            else if (uiManager.inventoryWindonManager.ammoSlotIndex == 2)
            {
                UpdateAllDamageValue(playerInventoryManager.spareBow, ammo, true);
                UpdateAllBlockValue(playerInventoryManager.spareBow, ammo, true);
                UpdateAllAbnormalEffect(playerInventoryManager.spareBow, ammo, true);
                UpdateAllAttributeRankAddition(playerInventoryManager.spareBow, ammo, true);
            }
            else if (uiManager.inventoryWindonManager.ammoSlotIndex == 3)
            {
                UpdateAllDamageValue(playerInventoryManager.otherAmmo, ammo, true);
                UpdateAllBlockValue(playerInventoryManager.otherAmmo, ammo, true);
                UpdateAllAbnormalEffect(playerInventoryManager.otherAmmo, ammo, true);
                UpdateAllAttributeRankAddition(playerInventoryManager.otherAmmo, ammo, true);
            }
            else if (uiManager.inventoryWindonManager.ammoSlotIndex == 4)
            {
                UpdateAllDamageValue(playerInventoryManager.spareOtherAmmo, ammo, true);
                UpdateAllBlockValue(playerInventoryManager.spareOtherAmmo, ammo, true);
                UpdateAllAbnormalEffect(playerInventoryManager.spareOtherAmmo, ammo, true);
                UpdateAllAttributeRankAddition(playerInventoryManager.spareOtherAmmo, ammo, true);
            }

            UpdateItemIcon(ammo);
            UpdateWeaponDescriptionAndName(ammo , true);
        }
        public void UpdateUIWindow()
        {
            UpdateAllDamageValue(null, null);
            UpdateAllBlockValue(null, null);
            UpdateAllAbnormalEffect(null, null);
            UpdateAllAttributeRankAddition(null , null);

            UpdateItemIcon(null);
            UpdateWeaponDescriptionAndName(null);
        }

        //������ƷͼƬ:
        private void UpdateItemIcon(Item item)
        {
            if (item != null)
            {
                weaponIcon.enabled = true;
                weaponIcon.sprite = item.itemIcon;
            }
            else
            {
                weaponIcon.enabled = false;
                weaponIcon.sprite = null;
            }
        }

        //---------------------------------------------Զ�̵�ҩ------------------------------------
        private void UpdateAllDamageValue(RangedAmmoItem currentAmmo, RangedAmmoItem contrastAmmo , bool isAmmo)
        {
            //��ǰ��������:
            string currentPhysics = "000";
            string currentMagic = "000";
            string currentFier = "000";
            string currentLightning = "000";
            string currentDark = "000";
            string currentCriticalDamageMuiltiplier = "--";
            //����������������:
            string contrastPhysics = "000";
            string contrastMagic = "000";
            string contrastFier = "000";
            string contrastLightning = "000";
            string contrastDark = "000";
            string contrastCriticalDamageMuiltiplier = "--";

            if (currentAmmo != null)
            {
                currentPhysics = currentAmmo.physicalDamage.ToString("d3");
                currentMagic = currentAmmo.magicDamage.ToString("d3");
                currentFier = currentAmmo.fierDamage.ToString("d3");
                currentLightning = currentAmmo.lightningDamage.ToString("d3");
                currentDark = currentAmmo.darkDamage.ToString("d3");
            }

            if (contrastAmmo != null)
            {
                contrastPhysics = contrastAmmo.physicalDamage.ToString("d3");
                contrastMagic = contrastAmmo.magicDamage.ToString("d3");
                contrastFier = contrastAmmo.fierDamage.ToString("d3");
                contrastLightning = contrastAmmo.lightningDamage.ToString("d3");
                contrastDark = contrastAmmo.darkDamage.ToString("d3");

                //������ɫ:
                if (currentAmmo != null)
                {
                    //�����˺���ɫ:
                    if (currentAmmo.physicalDamage == contrastAmmo.physicalDamage)
                    {
                        physicsValueText.color = Color.white;
                    }
                    else if (currentAmmo.physicalDamage > contrastAmmo.physicalDamage)
                    {
                        physicsValueText.color = Color.red;
                    }
                    else
                    {
                        physicsValueText.color = Color.blue;
                    }

                    //ħ���˺���ɫ
                    if (currentAmmo.magicDamage == contrastAmmo.magicDamage)
                    {
                        magicValueText.color = Color.white;
                    }
                    else if (currentAmmo.magicDamage > contrastAmmo.magicDamage)
                    {
                        magicValueText.color = Color.red;
                    }
                    else
                    {
                        magicValueText.color = Color.blue;
                    }

                    //�����˺���ɫ
                    if (currentAmmo.fierDamage == contrastAmmo.fierDamage)
                    {
                        fierValueText.color = Color.white;
                    }
                    else if (currentAmmo.fierDamage > contrastAmmo.fierDamage)
                    {
                        fierValueText.color = Color.red;
                    }
                    else
                    {
                        fierValueText.color = Color.blue;
                    }

                    //�׵��˺���ɫ
                    if (currentAmmo.lightningDamage == contrastAmmo.lightningDamage)
                    {
                        lightningValueText.color = Color.white;
                    }
                    else if (currentAmmo.lightningDamage > contrastAmmo.lightningDamage)
                    {
                        lightningValueText.color = Color.red;
                    }
                    else
                    {
                        lightningValueText.color = Color.blue;
                    }

                    //���˺���ɫ:
                    if (currentAmmo.darkDamage == contrastAmmo.darkDamage)
                    {
                        darkValueText.color = Color.white;
                    }
                    else if (currentAmmo.darkDamage > contrastAmmo.darkDamage)
                    {
                        darkValueText.color = Color.red;
                    }
                    else
                    {
                        darkValueText.color = Color.blue;
                    }
                }
                else
                {
                    physicsValueText.color = Color.blue;
                    magicValueText.color = Color.blue;
                    fierValueText.color = Color.blue;
                    lightningValueText.color = Color.blue;
                    darkValueText.color = Color.blue;
                    criticalDamageMuiltiplierText.color = Color.blue;
                }
            }
            else
            {
                physicsValueText.color = Color.white;
                magicValueText.color = Color.white;
                fierValueText.color = Color.white;
                lightningValueText.color = Color.white;
                darkValueText.color = Color.white;
                criticalDamageMuiltiplierText.color = Color.white;
            }

            physicsValueText.text = currentPhysics + "-" + contrastPhysics;
            magicValueText.text = currentMagic + "-" + contrastMagic;
            fierValueText.text = currentFier + "-" + contrastFier;
            lightningValueText.text = currentLightning + "-" + contrastLightning;
            darkValueText.text = currentDark + "-" + contrastDark;
            criticalDamageMuiltiplierText.text = currentCriticalDamageMuiltiplier + "-" + contrastCriticalDamageMuiltiplier;
        }
        private void UpdateAllBlockValue(RangedAmmoItem currentAmmo, RangedAmmoItem contrastAmmo, bool isAmmo)
        {
            physicsBlockValueText.text = "--";
            magicBlockValueText.text = "--";
            fierBlockValueText.text = "--";
            lightningBlockValueText.text = "--";
            darkBlockValueText.text = "--";
            blockingForceValueText.text = "--";
        }
        private void UpdateAllAbnormalEffect(RangedAmmoItem currentAmmo, RangedAmmoItem contrastAmmo ,bool isAmmo)
        {
            //��ǰ��������Ч���˺�����:
            float currentPoisonDamageValue = 0;
            float currentFrostDamageValue = 0;
            float currentHemorrhageDamageValue = 0;
            //��������������Ч������:
            float contrastPoisonDamageValue = 0;
            float contrastFrostDamageValue = 0;
            float contrastHemorrhageDamageValue = 0;
            if (currentAmmo != null)
            {
                currentPoisonDamageValue = currentAmmo.poisonDamage;
                currentFrostDamageValue = currentAmmo.frostDamage;
                currentHemorrhageDamageValue = currentAmmo.hemorrhageDamage;
            }
            if (contrastAmmo != null)
            {
                contrastPoisonDamageValue = contrastAmmo.poisonDamage;
                contrastFrostDamageValue = contrastAmmo.frostDamage;
                contrastHemorrhageDamageValue = contrastAmmo.hemorrhageDamage;

                //�ı���ɫ:
                if (currentPoisonDamageValue == contrastPoisonDamageValue)
                {
                    poisonText.color = Color.white;
                }
                else if (currentPoisonDamageValue < contrastPoisonDamageValue)
                {
                    poisonText.color = Color.blue;
                }
                else if (currentPoisonDamageValue > contrastPoisonDamageValue)
                {
                    poisonText.color = Color.red;
                }

                if (currentFrostDamageValue == contrastFrostDamageValue)
                {
                    frostText.color = Color.white;
                }
                else if (currentFrostDamageValue < contrastFrostDamageValue)
                {
                    frostText.color = Color.blue;
                }
                else if (currentFrostDamageValue > contrastFrostDamageValue)
                {
                    frostText.color = Color.red;
                }

                if (currentHemorrhageDamageValue == contrastHemorrhageDamageValue)
                {
                    hemorrhageText.color = Color.white;
                }
                else if (currentHemorrhageDamageValue < contrastHemorrhageDamageValue)
                {
                    hemorrhageText.color = Color.blue;
                }
                else if (currentHemorrhageDamageValue > contrastHemorrhageDamageValue)
                {
                    hemorrhageText.color = Color.red;
                }
            }
            else
            {
                poisonText.color = Color.white;
                frostText.color = Color.white;
                hemorrhageText.color = Color.white;
            }

            if (contrastPoisonDamageValue <= 0)
            {
                //d
                poisonText.text = "D";
            }
            else if (contrastPoisonDamageValue < 5)
            {
                //c
                poisonText.text = "C";
            }
            else if (5 <= contrastPoisonDamageValue && contrastPoisonDamageValue < 10)
            {
                //b
                poisonText.text = "B";
            }
            else if (10 <= contrastPoisonDamageValue && contrastPoisonDamageValue < 15)
            {
                //a
                poisonText.text = "A";
            }
            else if (15 <= contrastPoisonDamageValue)
            {
                //s
                poisonText.text = "S";
            }

            if (contrastFrostDamageValue <= 0)
            {
                //d
                frostText.text = "D";
            }
            else if (contrastFrostDamageValue < 25)
            {
                //c
                frostText.text = "C";
            }
            else if (25 <= contrastFrostDamageValue && contrastFrostDamageValue < 35)
            {
                //b
                frostText.text = "B";
            }
            else if (35 <= contrastFrostDamageValue && contrastFrostDamageValue < 45)
            {
                //a
                frostText.text = "A";
            }
            else if (45 <= contrastFrostDamageValue)
            {
                //s
                frostText.text = "S";
            }

            if (contrastHemorrhageDamageValue <= 0)
            {
                //d
                hemorrhageText.text = "D";
            }
            else if (contrastHemorrhageDamageValue < 25)
            {
                //c
                hemorrhageText.text = "C";
            }
            else if (25 <= contrastHemorrhageDamageValue && contrastHemorrhageDamageValue < 35)
            {
                //b
                hemorrhageText.text = "B";
            }
            else if (35 <= contrastHemorrhageDamageValue && contrastHemorrhageDamageValue < 45)
            {
                //a
                hemorrhageText.text = "A";
            }
            else if (45 <= contrastHemorrhageDamageValue)
            {
                //s
                hemorrhageText.text = "S";
            }
        }
        private void UpdateAllAttributeRankAddition(RangedAmmoItem currentAmmo, RangedAmmoItem contrastAmmo, bool isAmmo)
        {
            strengthAdditionText.text = "-";
            dexterityAdditionText.text = "-";
            intelligenceAdditionText.text = "-";
            faithAdditionText.text = "-";

            strengthAdditionText.color = Color.white;
            dexterityAdditionText.color = Color.white;
            intelligenceAdditionText.color = Color.white;
            faithAdditionText.color = Color.white;
        }
        private void UpdateWeaponDescriptionAndName(RangedAmmoItem ammo , bool isAmmo)
        {
            if (ammo != null)
            {
                weaponNameText.text = ammo.itemName;
                weaponDescription.text = ammo.weaponDescription;
            }
            else
            {
                weaponNameText.text = "��������";
                weaponDescription.text = "��������";
            }
        }

        //---------------------------------------------��ս����------------------------------------
        //�����˺�ֵ:
        private void UpdateAllDamageValue(WeaponItem currentWeapon , WeaponItem contrastWeapon)
        {
            //��ǰ��������:
            string currentPhysics = "000";
            string currentMagic = "000";
            string currentFier = "000";
            string currentLightning = "000";
            string currentDark = "000";
            string currentCriticalDamageMuiltiplier = "00";
            //����������������:
            string contrastPhysics = "000";
            string contrastMagic = "000";
            string contrastFier = "000";
            string contrastLightning = "000";
            string contrastDark = "000";
            string contrastCriticalDamageMuiltiplier = "00";

            if (currentWeapon != null)
            {
                currentPhysics = currentWeapon.physicalDamage.ToString("d3");
                currentMagic = currentWeapon.magicDamage.ToString("d3");
                currentFier = currentWeapon.fierDamage.ToString("d3");
                currentLightning = currentWeapon.lightningDamage.ToString("d3");
                currentDark = currentWeapon.darkDamage.ToString("d3");
                currentCriticalDamageMuiltiplier = currentWeapon.criticalDamageMuiltiplier.ToString("d2");
            }
            
            if (contrastWeapon != null)
            {
                contrastPhysics = contrastWeapon.physicalDamage.ToString("d3");
                contrastMagic = contrastWeapon.magicDamage.ToString("d3");
                contrastFier = contrastWeapon.fierDamage.ToString("d3");
                contrastLightning = contrastWeapon.lightningDamage.ToString("d3");
                contrastDark = contrastWeapon.darkDamage.ToString("d3");
                contrastCriticalDamageMuiltiplier = contrastWeapon.criticalDamageMuiltiplier.ToString("d2");

                //������ɫ:
                if (currentWeapon != null)
                {
                    //�����˺���ɫ:
                    if (currentWeapon.physicalDamage == contrastWeapon.physicalDamage)
                    {
                        physicsValueText.color = Color.white;
                    }
                    else if(currentWeapon.physicalDamage > contrastWeapon.physicalDamage)
                    {
                        physicsValueText.color = Color.red;
                    }
                    else
                    {
                        physicsValueText.color = Color.blue;
                    }

                    //ħ���˺���ɫ
                    if(currentWeapon.magicDamage == contrastWeapon.magicDamage)
                    {
                        magicValueText.color = Color.white;
                    }
                    else if (currentWeapon.magicDamage > contrastWeapon.magicDamage)
                    {
                        magicValueText.color = Color.red;
                    }
                    else
                    {
                        magicValueText.color = Color.blue;
                    }

                    //�����˺���ɫ
                    if (currentWeapon.fierDamage == contrastWeapon.fierDamage)
                    {
                        fierValueText.color = Color.white;
                    }
                    else if (currentWeapon.fierDamage > contrastWeapon.fierDamage)
                    {
                        fierValueText.color = Color.red;
                    }
                    else
                    {
                        fierValueText.color = Color.blue;
                    }

                    //�׵��˺���ɫ
                    if (currentWeapon.lightningDamage == contrastWeapon.lightningDamage)
                    {
                        lightningValueText.color = Color.white;
                    }
                    else if (currentWeapon.lightningDamage > contrastWeapon.lightningDamage)
                    {
                        lightningValueText.color = Color.red;
                    }
                    else
                    {
                        lightningValueText.color = Color.blue;
                    }

                    //���˺���ɫ:
                    if (currentWeapon.darkDamage == contrastWeapon.darkDamage)
                    {
                        darkValueText.color = Color.white;
                    }
                    else if (currentWeapon.darkDamage > contrastWeapon.darkDamage)
                    {
                        darkValueText.color = Color.red;
                    }
                    else
                    {
                        darkValueText.color = Color.blue;
                    }

                    //�����˺���ɫ:
                    if (currentWeapon.criticalDamageMuiltiplier == contrastWeapon.criticalDamageMuiltiplier)
                    {
                        criticalDamageMuiltiplierText.color = Color.white;
                    }
                    else if (currentWeapon.criticalDamageMuiltiplier > contrastWeapon.criticalDamageMuiltiplier)
                    {
                        criticalDamageMuiltiplierText.color = Color.red;
                    }
                    else
                    {
                        criticalDamageMuiltiplierText.color = Color.blue;
                    }
                }
                else
                {
                    physicsValueText.color = Color.blue;
                    magicValueText.color = Color.blue;
                    fierValueText.color = Color.blue;
                    lightningValueText.color = Color.blue;
                    darkValueText.color = Color.blue;
                    criticalDamageMuiltiplierText.color = Color.blue;
                }
            }
            else
            {
                physicsValueText.color = Color.white;
                magicValueText.color = Color.white;
                fierValueText.color = Color.white;
                lightningValueText.color = Color.white;
                darkValueText.color = Color.white;
                criticalDamageMuiltiplierText.color = Color.white;
            }

            physicsValueText.text = currentPhysics + "-" + contrastPhysics;
            magicValueText.text = currentMagic + "-" + contrastMagic;
            fierValueText.text = currentFier + "-" + contrastFier;
            lightningValueText.text = currentLightning + "-" + contrastLightning;
            darkValueText.text = currentDark + "-" + contrastDark;
            criticalDamageMuiltiplierText.text = currentCriticalDamageMuiltiplier + "-" + contrastCriticalDamageMuiltiplier;
        }
        //���¿���ֵ:
        private void UpdateAllBlockValue(WeaponItem currentWeapon, WeaponItem contrastWeapon)
        {
            //��ǰ��������:
            string currentPhysics = "000";
            string currentMagic = "000";
            string currentFier = "000";
            string currentLightning = "000";
            string currentDark = "000";
            string currentBlockingForce = "000";
            //����������������:
            string contrastPhysics = "000";
            string contrastMagic = "000";
            string contrastFier = "000";
            string contrastLightning = "000";
            string contrastDark = "000";
            string contrastBlockingForce = "000";

            if (currentWeapon != null)
            {
                currentPhysics = Mathf.RoundToInt(currentWeapon.physicalDamageAbsorption * 100).ToString("d3");
                currentMagic = Mathf.RoundToInt(currentWeapon.magicDamageAbsorption * 100).ToString("d3");
                currentFier = Mathf.RoundToInt(currentWeapon.fierDamageAbsorption * 100).ToString("d3");
                currentLightning = Mathf.RoundToInt(currentWeapon.lightningDamageAbsorption * 100).ToString("d3");
                currentDark = Mathf.RoundToInt(currentWeapon.darkDamageAbsorption * 100).ToString("d3");
                currentBlockingForce = currentWeapon.blockingForce.ToString("d3");
            }

            if (contrastWeapon != null)
            {
                contrastPhysics = Mathf.RoundToInt(contrastWeapon.physicalDamageAbsorption * 100).ToString("d3");
                contrastMagic = Mathf.RoundToInt(contrastWeapon.magicDamageAbsorption * 100).ToString("d3");
                contrastFier = Mathf.RoundToInt(contrastWeapon.fierDamageAbsorption * 100).ToString("d3");
                contrastLightning = Mathf.RoundToInt(contrastWeapon.lightningDamageAbsorption * 100).ToString("d3");
                contrastDark = Mathf.RoundToInt(contrastWeapon.darkDamageAbsorption * 100).ToString("d3");
                contrastBlockingForce = contrastWeapon.blockingForce.ToString("d3");

                //������ɫ:
                if (currentWeapon != null)
                {
                    //�����˺�������ɫ:
                    if (currentWeapon.physicalDamageAbsorption == contrastWeapon.physicalDamageAbsorption)
                    {
                        physicsBlockValueText.color = Color.white;
                    }
                    else if (currentWeapon.physicalDamageAbsorption > contrastWeapon.physicalDamageAbsorption)
                    {
                        physicsBlockValueText.color = Color.red;
                    }
                    else
                    {
                        physicsBlockValueText.color = Color.blue;
                    }

                    //ħ���˺�������ɫ
                    if (currentWeapon.magicDamageAbsorption == contrastWeapon.magicDamageAbsorption)
                    {
                        magicBlockValueText.color = Color.white;
                    }
                    else if (currentWeapon.magicDamageAbsorption > contrastWeapon.magicDamageAbsorption)
                    {
                        magicBlockValueText.color = Color.red;
                    }
                    else
                    {
                        magicBlockValueText.color = Color.blue;
                    }

                    //�����˺�������ɫ
                    if (currentWeapon.fierDamageAbsorption == contrastWeapon.fierDamageAbsorption)
                    {
                        fierBlockValueText.color = Color.white;
                    }
                    else if (currentWeapon.fierDamageAbsorption > contrastWeapon.fierDamageAbsorption)
                    {
                        fierBlockValueText.color = Color.red;
                    }
                    else
                    {
                        fierBlockValueText.color = Color.blue;
                    }

                    //�׵��˺�������ɫ
                    if (currentWeapon.lightningDamageAbsorption == contrastWeapon.lightningDamageAbsorption)
                    {
                        lightningBlockValueText.color = Color.white;
                    }
                    else if (currentWeapon.lightningDamageAbsorption > contrastWeapon.lightningDamageAbsorption)
                    {
                        lightningBlockValueText.color = Color.red;
                    }
                    else
                    {
                        lightningBlockValueText.color = Color.blue;
                    }

                    //���˺�������ɫ:
                    if (currentWeapon.darkDamageAbsorption == contrastWeapon.darkDamageAbsorption)
                    {
                        darkBlockValueText.color = Color.white;
                    }
                    else if (currentWeapon.darkDamageAbsorption > contrastWeapon.darkDamageAbsorption)
                    {
                        darkBlockValueText.color = Color.red;
                    }
                    else
                    {
                        darkBlockValueText.color = Color.blue;
                    }

                    //������ɫ:
                    if (currentWeapon.blockingForce == contrastWeapon.blockingForce)
                    {
                        blockingForceValueText.color = Color.white;
                    }
                    else if (currentWeapon.blockingForce > contrastWeapon.blockingForce)
                    {
                        blockingForceValueText.color = Color.red;
                    }
                    else
                    {
                        blockingForceValueText.color = Color.blue;
                    }
                }
                else
                {
                    physicsBlockValueText.color = Color.blue;
                    magicBlockValueText.color = Color.blue;
                    fierBlockValueText.color = Color.blue;
                    lightningBlockValueText.color = Color.blue;
                    darkBlockValueText.color = Color.blue;
                    blockingForceValueText.color = Color.blue;
                }
            }
            else
            {
                physicsBlockValueText.color = Color.white;
                magicBlockValueText.color = Color.white;
                fierBlockValueText.color = Color.white;
                lightningBlockValueText.color = Color.white;
                darkBlockValueText.color = Color.white;
                blockingForceValueText.color = Color.white;
            }

            physicsBlockValueText.text = currentPhysics + "-" + contrastPhysics;
            magicBlockValueText.text = currentMagic + "-" + contrastMagic;
            fierBlockValueText.text = currentFier + "-" + contrastFier;
            lightningBlockValueText.text = currentLightning + "-" + contrastLightning;
            darkBlockValueText.text = currentDark + "-" + contrastDark;
            blockingForceValueText.text = currentBlockingForce + "-" + contrastBlockingForce;
        }
        //������������Ч��ֵ:
        private void UpdateAllAbnormalEffect(WeaponItem currentWeapon, WeaponItem contrastWeapon)
        {
            //��ǰ��������Ч���˺�����:
            float currentPoisonDamageValue = 0;
            float currentFrostDamageValue = 0;
            float currentHemorrhageDamageValue = 0;
            //��������������Ч������:
            float contrastPoisonDamageValue = 0;
            float contrastFrostDamageValue = 0;
            float contrastHemorrhageDamageValue = 0;
            if (currentWeapon != null)
            {
                currentPoisonDamageValue = currentWeapon.poisonDamage;
                currentFrostDamageValue = currentWeapon.frostDamage;
                currentHemorrhageDamageValue = currentWeapon.hemorrhageDamage;
            }
            if(contrastWeapon != null)
            {
                contrastPoisonDamageValue = contrastWeapon.poisonDamage;
                contrastFrostDamageValue = contrastWeapon.frostDamage;
                contrastHemorrhageDamageValue = contrastWeapon.hemorrhageDamage;

                //�ı���ɫ:
                if (currentPoisonDamageValue == contrastPoisonDamageValue)
                {
                    poisonText.color = Color.white;
                }
                else if (currentPoisonDamageValue < contrastPoisonDamageValue)
                {
                    poisonText.color = Color.blue;
                }
                else if (currentPoisonDamageValue > contrastPoisonDamageValue)
                {
                    poisonText.color = Color.red;
                }

                if (currentFrostDamageValue == contrastFrostDamageValue)
                {
                    frostText.color = Color.white;
                }
                else if (currentFrostDamageValue < contrastFrostDamageValue)
                {
                    frostText.color = Color.blue;
                }
                else if (currentFrostDamageValue > contrastFrostDamageValue)
                {
                    frostText.color = Color.red;
                }

                if (currentHemorrhageDamageValue == contrastHemorrhageDamageValue)
                {
                    hemorrhageText.color = Color.white;
                }
                else if (currentHemorrhageDamageValue < contrastHemorrhageDamageValue)
                {
                    hemorrhageText.color = Color.blue;
                }
                else if (currentHemorrhageDamageValue > contrastHemorrhageDamageValue)
                {
                    hemorrhageText.color = Color.red;
                }
            }
            else
            {
                poisonText.color = Color.white;
                frostText.color = Color.white;
                hemorrhageText.color = Color.white;
            }

            if(contrastPoisonDamageValue <= 0)
            {
                //d
                poisonText.text = "D";
            }
            else if(contrastPoisonDamageValue < 5)
            {
                //c
                poisonText.text = "C";
            }
            else if(5 <= contrastPoisonDamageValue && contrastPoisonDamageValue < 10)
            {
                //b
                poisonText.text = "B";
            }
            else if (10 <= contrastPoisonDamageValue && contrastPoisonDamageValue < 15)
            {
                //a
                poisonText.text = "A";
            }
            else if(15 <= contrastPoisonDamageValue)
            {
                //s
                poisonText.text = "S";
            }

            if(contrastFrostDamageValue <= 0)
            {
                //d
                frostText.text = "D";
            }
            else if (contrastFrostDamageValue < 25)
            {
                //c
                frostText.text = "C";
            }
            else if (25 <= contrastFrostDamageValue && contrastFrostDamageValue < 35)
            {
                //b
                frostText.text = "B";
            }
            else if (35 <= contrastFrostDamageValue && contrastFrostDamageValue < 45)
            {
                //a
                frostText.text = "A";
            }
            else if (45 <= contrastFrostDamageValue)
            {
                //s
                frostText.text = "S";
            }

            if (contrastHemorrhageDamageValue <= 0)
            {
                //d
                hemorrhageText.text = "D";
            }
            else if (contrastHemorrhageDamageValue < 25)
            {
                //c
                hemorrhageText.text = "C";
            }
            else if (25 <= contrastHemorrhageDamageValue && contrastHemorrhageDamageValue < 35)
            {
                //b
                hemorrhageText.text = "B";
            }
            else if (35 <= contrastHemorrhageDamageValue && contrastHemorrhageDamageValue < 45)
            {
                //a
                hemorrhageText.text = "A";
            }
            else if (45 <= contrastHemorrhageDamageValue)
            {
                //s
                hemorrhageText.text = "S";
            }
        }
        //�����������Լӳ�ֵ:
        private void UpdateAllAttributeRankAddition(WeaponItem currentWeapon, WeaponItem contrastWeapon)
        {
            //��ǰ�������Լӳ�ֵ:
            float currentStrengthAddition = 0;
            float currentDexterityAddition = 0;
            float currentIntelligenceAddition = 0;
            float currentFaithAddition = 0;
            //�������������Լӳ�ֵ:
            float contrastStrengthAddition = 0;
            float contrastDexterityAddition = 0;
            float contrastIntelligenceAddition = 0;
            float contrastFaithAddition = 0;
            if (currentWeapon != null)
            {
                //��ǰ�������Լӳ�ֵ:
                currentStrengthAddition = currentWeapon.strengthAddition;
                currentDexterityAddition = currentWeapon.dexterityAddition;
                currentIntelligenceAddition = currentWeapon.intelligenceAddition;
                currentFaithAddition = currentWeapon.faithAddition;
            }

            if(contrastWeapon != null)
            {
                //�������������Լӳ�ֵ:
                contrastStrengthAddition = contrastWeapon.strengthAddition;
                contrastDexterityAddition = contrastWeapon.dexterityAddition;
                contrastIntelligenceAddition = contrastWeapon.intelligenceAddition;
                contrastFaithAddition = contrastWeapon.faithAddition;

                //�ı���ɫ:
                if (currentStrengthAddition == contrastStrengthAddition)
                {
                    strengthAdditionText.color = Color.white;
                }
                else if (currentStrengthAddition < contrastStrengthAddition)
                {
                    strengthAdditionText.color = Color.blue;
                }
                else if (currentStrengthAddition > contrastStrengthAddition)
                {
                    strengthAdditionText.color = Color.red;
                }

                if (currentDexterityAddition == contrastDexterityAddition)
                {
                    dexterityAdditionText.color = Color.white;
                }
                else if (currentDexterityAddition < contrastDexterityAddition)
                {
                    dexterityAdditionText.color = Color.blue;
                }
                else if (currentDexterityAddition > contrastDexterityAddition)
                {
                    dexterityAdditionText.color = Color.red;
                }

                if (currentIntelligenceAddition == contrastIntelligenceAddition)
                {
                    intelligenceAdditionText.color = Color.white;
                }
                else if (currentIntelligenceAddition < contrastIntelligenceAddition)
                {
                    intelligenceAdditionText.color = Color.blue;
                }
                else if (currentIntelligenceAddition > contrastIntelligenceAddition)
                {
                    intelligenceAdditionText.color = Color.red;
                }

                if (currentFaithAddition == contrastFaithAddition)
                {
                    faithAdditionText.color = Color.white;
                }
                else if (currentFaithAddition < contrastFaithAddition)
                {
                    faithAdditionText.color = Color.blue;
                }
                else if (currentFaithAddition > contrastFaithAddition)
                {
                    faithAdditionText.color = Color.red;
                }
            }
            else
            {
                strengthAdditionText.color = Color.white;
                dexterityAdditionText.color = Color.white;
                intelligenceAdditionText.color = Color.white;
                faithAdditionText.color = Color.white;
            }

            #region ui�ı���ֵ:
            if (contrastStrengthAddition <= 0)
            {
                strengthAdditionText.text = "-";
            }
            else if(0<contrastStrengthAddition && contrastStrengthAddition <= 0.35f)
            {
                strengthAdditionText.text = "D";
            }
            else if (0.35f < contrastStrengthAddition && contrastStrengthAddition <= 0.5f)
            {
                strengthAdditionText.text = "C";
            }
            else if (0.5f < contrastStrengthAddition && contrastStrengthAddition <= 0.8f)
            {
                strengthAdditionText.text = "A";
            }
            else if (0.8f < contrastStrengthAddition)
            {
                strengthAdditionText.text = "S";
            }

            if (contrastDexterityAddition <= 0)
            {
                dexterityAdditionText.text = "-";
            }
            else if (0 < contrastDexterityAddition && contrastDexterityAddition <= 0.35f)
            {
                dexterityAdditionText.text = "D";
            }
            else if (0.35f < contrastDexterityAddition && contrastDexterityAddition <= 0.5f)
            {
                dexterityAdditionText.text = "C";
            }
            else if (0.5f < contrastDexterityAddition && contrastDexterityAddition <= 0.8f)
            {
                dexterityAdditionText.text = "A";
            }
            else if (0.8f < contrastDexterityAddition)
            {
                dexterityAdditionText.text = "S";
            }

            if (contrastIntelligenceAddition <= 0)
            {
                intelligenceAdditionText.text = "-";
            }
            else if (0 < contrastIntelligenceAddition && contrastIntelligenceAddition <= 0.35f)
            {
                intelligenceAdditionText.text = "D";
            }
            else if (0.35f < contrastIntelligenceAddition && contrastIntelligenceAddition <= 0.5f)
            {
                intelligenceAdditionText.text = "C";
            }
            else if (0.5f < contrastIntelligenceAddition && contrastIntelligenceAddition <= 0.8f)
            {
                intelligenceAdditionText.text = "A";
            }
            else if (0.8f < contrastIntelligenceAddition)
            {
                intelligenceAdditionText.text = "S";
            }

            if (contrastFaithAddition <= 0)
            {
                faithAdditionText.text = "-";
            }
            else if (0 < contrastFaithAddition && contrastFaithAddition <= 0.35f)
            {
                faithAdditionText.text = "D";
            }
            else if (0.35f < contrastFaithAddition && contrastFaithAddition <= 0.5f)
            {
                faithAdditionText.text = "C";
            }
            else if (0.5f < contrastFaithAddition && contrastFaithAddition <= 0.8f)
            {
                faithAdditionText.text = "A";
            }
            else if (0.8f < contrastFaithAddition)
            {
                faithAdditionText.text = "S";
            }
            #endregion
        }
        //�����������ܺ�����:
        private void UpdateWeaponDescriptionAndName(WeaponItem weapon)
        {
            if(weapon != null)
            {
                weaponNameText.text = weapon.itemName;
                weaponDescription.text = weapon.weaponDescription;
            }
            else
            {
                weaponNameText.text = "��������";
                weaponDescription.text = "��������";
            }
        }
    }
}