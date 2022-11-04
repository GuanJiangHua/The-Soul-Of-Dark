using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class UIManager : MonoBehaviour
    {
        public PlayerManager playerManager;                        //玩家库存;
        public EquipmentWindowUI equipmentWindowUI;    //装备窗口ui;

        [Header("十字准星:")]
        public GameObject crossHair;
        
        [Header("UI窗口:")]
        public GameObject hudWindow;                        //人物属性简略窗口;
        public GameObject selectWindow;                     //菜单窗口;
        public GameObject playerLeveUpWindow;        //玩家升级窗口;
        public GameObject weaponInventoryWindow;  //武器背包窗口;
        public GameObject equipmentScreenWindow;  //装备选择窗口;
        public GameObject inventoryWindon;                //库存窗口;
        public GameObject weaponPropertiesWindow;              //武器详情窗口;
        public GameObject equipmentPropertiesWindow;         //装备详情窗口;
        public GameObject playerPropertiesWindow;                 //人物属性详情窗口;
        public GameObject allItemInventoryWindow;                //所有物品库存窗口;
        public GameObject itemPropertiesWindow;                   //所有物品展示窗口;
        public GameObject campfireUIWindow;                         //篝火功能窗口窗口;
        public GameObject dialogSystemWindow;                    //对话窗口
        public GameObject dialogOptionsWindow;                   //对话选项窗口;
        public GameObject exitGameWindow;                           //退出游戏窗口;
        public GameObject textPromptWindow;                        //小提示窗口;
        public GameObject storeWindow;                                  //商店窗口;
        [Header("死亡提示窗口:")]
        public DeathPromptWindow deathPromptWindow;
        [Header("死亡提示窗口:")]
        public GameObject youDeadWindow;
        public Image youDeadWindowImage;
        public Text youDeadWindowText;
        bool isEnable = false;
        float transparentTimer = 0;
        [Header("已经选择的装备槽:")]
        public bool rightHandSlot01Selected;
        public bool rightHandSlot02Selected;
        public bool rightHandSlot03Selected;
        public bool leftHandSlot01Selected;
        public bool leftHandSlot02Selected;
        public bool leftHandSlot03Selected;
        [Header("已经选择的消耗品槽位索引:")]
        public int consumableInventorySlotSelected = -1;         //-1未选择
        [Header("已经选择的戒指槽位索引:")]
        public int ringInventorySlotSelected = -1;                      //-1未选择

        [Header("武器库存:")]
        public GameObject weaponInventorySlotPrefab;     //武器背包库存槽预制体;
        public Transform weaponInventorySlotParent;        //武器背包库存槽的父物体;
        WeaponInventorySlot[] weaponInventorySlots;       //武器背包库存槽位数组;

        [Header("武器,消耗品,法术槽位:")]
        public QuickSlotUI quickSlotUI;

        #region -------------------------------------私有属性:------------------------------------
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
            //每次被启用，都更新一次武器库存槽数组:
            weaponInventorySlots = weaponInventorySlotParent.GetComponentsInChildren<WeaponInventorySlot>();

            //装备窗口更新:
            if (playerManager != null && playerManager.playerInventoryManager!=null)
            {
                //装备窗口: 更新武器(左右手)
                equipmentWindowUI.LoadWeaponsOnEquipmentScreen(playerManager.playerInventoryManager);
                //消耗品:
                equipmentWindowUI.LoadConsumableOnEquipmentScreen(playerManager.playerInventoryManager);
                //更新头盔:
                equipmentWindowUI.LoadHelmetEquipmentOnEquipmentScreen(playerManager.playerInventoryManager);
                //更新胸甲:
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
            #region 武器库存更新
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

            //消耗品库存更新:
            inventoryWindonManager.UpdateInventoryWindowUI();

            if(playerPropertiesWindowManager != null)
            {
                playerPropertiesWindowManager.UpdatePropertiesText(playerManager.playerStateManager);
            }
        }
        //启用菜单窗口:
        public void OpenSelectWindow()
        {
            selectWindow.SetActive(true);
        }
        //禁用菜单窗口:
        public void CloseSelectWindow()
        {
            selectWindow.SetActive(false);
        }
    
        //禁用使有左侧窗口:
        public void CloseAllLeftWindow()
        {
            ResetAllSelectedSlots();

            weaponInventoryWindow.SetActive(false);
            equipmentScreenWindow.SetActive(false);
            inventoryWindon.SetActive(false);
            storeWindow.SetActive(false);
            allItemInventoryWindow.SetActive(false);

            allItemInventoryWindow.SetActive(false);
            //重置已经选择的消耗品槽位:
            consumableInventorySlotSelected = -1;
            //重置以选择的弹药槽位:
            inventoryWindonManager.ammoSlotIndex = -1;
        }
        //禁用所有中间窗口:
        public void CloseAllCentralWindow()
        {
            weaponPropertiesWindow.SetActive(false);
            equipmentPropertiesWindow.SetActive(false);
            itemPropertiesWindow.SetActive(false);
        }
        //禁用所有右侧窗口:
        public void CloseAllRightWindow()
        {
            playerPropertiesWindow.gameObject.SetActive(false);
        }

        //重置已选择的武器槽:
        public void ResetAllSelectedSlots()
        {
            rightHandSlot01Selected = false;
            rightHandSlot02Selected = false;
            rightHandSlot03Selected = false;
            leftHandSlot01Selected = false;
            leftHandSlot02Selected = false;
            leftHandSlot03Selected = false;
        }

        //启用篝火功能窗口:
        public void EnableCampfireUIWindow()
        {
            //禁用基础属性窗口:
            //禁用菜单窗口:
            //禁用背包窗口:
            CloseSelectWindow();          //禁用菜单窗口
            CloseAllLeftWindow();         //禁用左侧所有窗口
            CloseAllCentralWindow();   //禁用中间所有窗口
            CloseAllRightWindow();      //禁用右侧所有窗口
            hudWindow.SetActive(false);    //启用简略属性

            //启用
            campfireUIWindow.SetActive(true);
        }
        //禁用篝火功能窗口:
        public void CloseCampfireUIWindow()
        {
            campfireUIWindow.SetActive(false);
            hudWindow.SetActive(true);    //启用简略属性
        }
        //启用商店窗口:
        public void EnableStoreWindow(NpcManager npc)
        {
            CloseSelectWindow();          //禁用菜单窗口
            CloseAllLeftWindow();         //禁用左侧所有窗口
            CloseAllCentralWindow();   //禁用中间所有窗口
            CloseAllRightWindow();      //禁用右侧所有窗口
            hudWindow.SetActive(false);    //禁用简略属性

            storeWindow.SetActive(true);                    //商店窗口
            itemPropertiesWindow.SetActive(true);    //所有物品展示窗口
            playerPropertiesWindow.SetActive(true); //人物属性等级窗口
            playerPropertiesWindowManager.UpdatePropertiesText(playerManager.playerStateManager);//更新属性;

            playerManager.isTrading = true;
            commodityTradingWindow.UpdateCommodityInventory(playerManager, npc);
        }

        //启用提示窗口:
        public void EnablePromptWindow()
        {
            deathPromptWindow.confirmButtonEvent.RemoveAllListeners();   //删除所有事件
            deathPromptWindow.gameObject.SetActive(true);
        }

        //启用死亡提示窗口:
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

        //禁用死亡提示窗口:
        public void CloseYouDeadWindow()
        {
            isEnable = false;

            //启用重新加载场景:
            SceneLoadingManagement.single.LoadingSceneByName("SampleScene");
        }
    }
}
