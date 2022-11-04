using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class UIManager : MonoBehaviour
    {
        public PlayerManager playerManager;                        //��ҿ��;
        public EquipmentWindowUI equipmentWindowUI;    //װ������ui;

        [Header("ʮ��׼��:")]
        public GameObject crossHair;
        
        [Header("UI����:")]
        public GameObject hudWindow;                        //�������Լ��Դ���;
        public GameObject selectWindow;                     //�˵�����;
        public GameObject playerLeveUpWindow;        //�����������;
        public GameObject weaponInventoryWindow;  //������������;
        public GameObject equipmentScreenWindow;  //װ��ѡ�񴰿�;
        public GameObject inventoryWindon;                //��洰��;
        public GameObject weaponPropertiesWindow;              //�������鴰��;
        public GameObject equipmentPropertiesWindow;         //װ�����鴰��;
        public GameObject playerPropertiesWindow;                 //�����������鴰��;
        public GameObject allItemInventoryWindow;                //������Ʒ��洰��;
        public GameObject itemPropertiesWindow;                   //������Ʒչʾ����;
        public GameObject campfireUIWindow;                         //�����ܴ��ڴ���;
        public GameObject dialogSystemWindow;                    //�Ի�����
        public GameObject dialogOptionsWindow;                   //�Ի�ѡ���;
        public GameObject exitGameWindow;                           //�˳���Ϸ����;
        public GameObject textPromptWindow;                        //С��ʾ����;
        public GameObject storeWindow;                                  //�̵괰��;
        [Header("������ʾ����:")]
        public DeathPromptWindow deathPromptWindow;
        [Header("������ʾ����:")]
        public GameObject youDeadWindow;
        public Image youDeadWindowImage;
        public Text youDeadWindowText;
        bool isEnable = false;
        float transparentTimer = 0;
        [Header("�Ѿ�ѡ���װ����:")]
        public bool rightHandSlot01Selected;
        public bool rightHandSlot02Selected;
        public bool rightHandSlot03Selected;
        public bool leftHandSlot01Selected;
        public bool leftHandSlot02Selected;
        public bool leftHandSlot03Selected;
        [Header("�Ѿ�ѡ�������Ʒ��λ����:")]
        public int consumableInventorySlotSelected = -1;         //-1δѡ��
        [Header("�Ѿ�ѡ��Ľ�ָ��λ����:")]
        public int ringInventorySlotSelected = -1;                      //-1δѡ��

        [Header("�������:")]
        public GameObject weaponInventorySlotPrefab;     //������������Ԥ����;
        public Transform weaponInventorySlotParent;        //�����������۵ĸ�����;
        WeaponInventorySlot[] weaponInventorySlots;       //������������λ����;

        [Header("����,����Ʒ,������λ:")]
        public QuickSlotUI quickSlotUI;

        #region -------------------------------------˽������:------------------------------------
        public PlayerPropertiesWindow playerPropertiesWindowManager;
        public InventoryWindowManager inventoryWindonManager;
        public WeaponPropertiesWindow weapentPropertiesWindowManager;
        public EquipmentPropertiesWindow equipmentPropertiesWindowManager;
        public AllItemInventoryWindow allItemInventoryWindowManager;
        public ItemPropertiesWindow itemPropertiesWindowManager;
        public DialogOptionsWindow dialogOptionsWindowManager;
        public DialogSystemWindow dialogSystemWindowManager;
        public ExitGameWindow exitGameWindowManager;
        public CampfireUIWindow campfireUIWindowManager;
        public TextPromptWindow textPromptWindowManager;
        public CommodityTradingWindow commodityTradingWindow;
        #endregion

        private void Awake()
        {
            quickSlotUI = GetComponentInChildren<QuickSlotUI>();
            playerPropertiesWindowManager = playerPropertiesWindow.GetComponentInChildren<PlayerPropertiesWindow>();
            inventoryWindonManager = inventoryWindon.GetComponent<InventoryWindowManager>();
            weapentPropertiesWindowManager = weaponPropertiesWindow.GetComponent<WeaponPropertiesWindow>();
            equipmentPropertiesWindowManager = equipmentPropertiesWindow.GetComponent<EquipmentPropertiesWindow>();
            allItemInventoryWindowManager = allItemInventoryWindow.GetComponent<AllItemInventoryWindow>();
            itemPropertiesWindowManager = itemPropertiesWindow.GetComponent<ItemPropertiesWindow>();
            dialogOptionsWindowManager = dialogOptionsWindow.GetComponent<DialogOptionsWindow>();
            dialogSystemWindowManager = dialogSystemWindow.GetComponent<DialogSystemWindow>();
            exitGameWindowManager = exitGameWindow.GetComponent<ExitGameWindow>();
            campfireUIWindowManager = campfireUIWindow.GetComponent<CampfireUIWindow>();
            textPromptWindowManager = textPromptWindow.GetComponent<TextPromptWindow>();
            commodityTradingWindow = storeWindow.GetComponent<CommodityTradingWindow>();
        }
        private void Start()
        {
            weaponInventorySlots = weaponInventorySlotParent.GetComponentsInChildren<WeaponInventorySlot>();

            equipmentWindowUI.LoadWeaponsOnEquipmentScreen(playerManager.playerInventoryManager);
            equipmentWindowUI.LoadConsumableOnEquipmentScreen(playerManager.playerInventoryManager);

            equipmentWindowUI.LoadHelmetEquipmentOnEquipmentScreen(playerManager.playerInventoryManager);
            equipmentWindowUI.LoadTorsoEquipmentOnEquipmentScreen(playerManager.playerInventoryManager);
            equipmentWindowUI.LoadLegEquipmentOnEquipmentScreen(playerManager.playerInventoryManager);
            equipmentWindowUI.LoadHandEquipmentOnEquipmentScreen(playerManager.playerInventoryManager);

            equipmentWindowUI.LoadAmmoEquipmentOnEquipmentScreen(playerManager.playerInventoryManager);
            equipmentWindowUI.LoadRingEquipmentOnEquipmentScreen(playerManager.playerInventoryManager);

            selectWindow.SetActive(false);
        }

        private void OnEnable()
        {
            //ÿ�α����ã�������һ��������������:
            weaponInventorySlots = weaponInventorySlotParent.GetComponentsInChildren<WeaponInventorySlot>();

            //װ�����ڸ���:
            if (playerManager != null && playerManager.playerInventoryManager!=null)
            {
                //װ������: ��������(������)
                equipmentWindowUI.LoadWeaponsOnEquipmentScreen(playerManager.playerInventoryManager);
                //����Ʒ:
                equipmentWindowUI.LoadConsumableOnEquipmentScreen(playerManager.playerInventoryManager);
                //����ͷ��:
                equipmentWindowUI.LoadHelmetEquipmentOnEquipmentScreen(playerManager.playerInventoryManager);
                //�����ؼ�:
                equipmentWindowUI.LoadTorsoEquipmentOnEquipmentScreen(playerManager.playerInventoryManager);
            }
        }

        private void Update()
        {
            if (isEnable)
            {
                Color imageColor = youDeadWindowImage.color;
                Color textColor = youDeadWindowText.color;
                transparentTimer += Time.deltaTime;
                imageColor.a = transparentTimer;
                textColor.a = transparentTimer;
                youDeadWindowImage.color = imageColor;
                youDeadWindowText.color = textColor;

                if (transparentTimer > 5)
                {
                    CloseYouDeadWindow();
                }
            }
        }

        public void UpdateUI()
        {
            #region ����������
            for(int i = 0; i < weaponInventorySlots.Length; i++)
            {
                if (i < playerManager.playerInventoryManager.weaponInventory.Count)
                {
                    if (weaponInventorySlots.Length < playerManager.playerInventoryManager.weaponInventory.Count)
                    {
                        Instantiate(weaponInventorySlotPrefab, weaponInventorySlotParent);
                        weaponInventorySlots = weaponInventorySlotParent.GetComponentsInChildren<WeaponInventorySlot>();
                    }

                    weaponInventorySlots[i].AddItem(playerManager.playerInventoryManager.weaponInventory[i]);

                }
                else
                {
                    weaponInventorySlots[i].ClearItem();
                }
            }
            #endregion

            //����Ʒ������:
            inventoryWindonManager.UpdateInventoryWindowUI();

            if(playerPropertiesWindowManager != null)
            {
                playerPropertiesWindowManager.UpdatePropertiesText(playerManager.playerStateManager);
            }
        }
        //���ò˵�����:
        public void OpenSelectWindow()
        {
            selectWindow.SetActive(true);
        }
        //���ò˵�����:
        public void CloseSelectWindow()
        {
            selectWindow.SetActive(false);
        }
    
        //����ʹ����ര��:
        public void CloseAllLeftWindow()
        {
            ResetAllSelectedSlots();

            weaponInventoryWindow.SetActive(false);
            equipmentScreenWindow.SetActive(false);
            inventoryWindon.SetActive(false);
            storeWindow.SetActive(false);
            allItemInventoryWindow.SetActive(false);

            allItemInventoryWindow.SetActive(false);
            //�����Ѿ�ѡ�������Ʒ��λ:
            consumableInventorySlotSelected = -1;
            //������ѡ��ĵ�ҩ��λ:
            inventoryWindonManager.ammoSlotIndex = -1;
        }
        //���������м䴰��:
        public void CloseAllCentralWindow()
        {
            weaponPropertiesWindow.SetActive(false);
            equipmentPropertiesWindow.SetActive(false);
            itemPropertiesWindow.SetActive(false);
        }
        //���������Ҳര��:
        public void CloseAllRightWindow()
        {
            playerPropertiesWindow.gameObject.SetActive(false);
        }

        //������ѡ���������:
        public void ResetAllSelectedSlots()
        {
            rightHandSlot01Selected = false;
            rightHandSlot02Selected = false;
            rightHandSlot03Selected = false;
            leftHandSlot01Selected = false;
            leftHandSlot02Selected = false;
            leftHandSlot03Selected = false;
        }

        //���������ܴ���:
        public void EnableCampfireUIWindow()
        {
            //���û������Դ���:
            //���ò˵�����:
            //���ñ�������:
            CloseSelectWindow();          //���ò˵�����
            CloseAllLeftWindow();         //����������д���
            CloseAllCentralWindow();   //�����м����д���
            CloseAllRightWindow();      //�����Ҳ����д���
            hudWindow.SetActive(false);    //���ü�������

            //����
            campfireUIWindow.SetActive(true);
        }
        //���������ܴ���:
        public void CloseCampfireUIWindow()
        {
            campfireUIWindow.SetActive(false);
            hudWindow.SetActive(true);    //���ü�������
        }
        //�����̵괰��:
        public void EnableStoreWindow(NpcManager npc)
        {
            CloseSelectWindow();          //���ò˵�����
            CloseAllLeftWindow();         //����������д���
            CloseAllCentralWindow();   //�����м����д���
            CloseAllRightWindow();      //�����Ҳ����д���
            hudWindow.SetActive(false);    //���ü�������

            storeWindow.SetActive(true);                    //�̵괰��
            itemPropertiesWindow.SetActive(true);    //������Ʒչʾ����
            playerPropertiesWindow.SetActive(true); //�������Եȼ�����
            playerPropertiesWindowManager.UpdatePropertiesText(playerManager.playerStateManager);//��������;

            playerManager.isTrading = true;
            commodityTradingWindow.UpdateCommodityInventory(playerManager, npc);
        }

        //������ʾ����:
        public void EnablePromptWindow()
        {
            deathPromptWindow.confirmButtonEvent.RemoveAllListeners();   //ɾ�������¼�
            deathPromptWindow.gameObject.SetActive(true);
        }

        //����������ʾ����:
        public void EnableYouDeadWindow()
        {
            Color imageColor = youDeadWindowImage.color;
            Color textColor = youDeadWindowText.color;
            imageColor.a = 0;
            textColor.a = 0;
            youDeadWindowImage.color = imageColor;
            youDeadWindowText.color = textColor;

            isEnable = true;
            youDeadWindow.SetActive(true);
        }

        //����������ʾ����:
        public void CloseYouDeadWindow()
        {
            isEnable = false;

            //�������¼��س���:
            SceneLoadingManagement.single.LoadingSceneByName("SampleScene");
        }
    }
}
