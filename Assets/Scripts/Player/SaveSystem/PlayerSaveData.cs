using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSaveData
{
    public string playerName;   //玩家名称
    public int playerLeve;          //玩家等级

    //人物裸身模型:
    public bool isMale = true;      //性别
    public int headId;                   //头部
    public int hairstyle;                //发型
    public int facialHairId;           //胡须
    public int eyebrow;               //眉毛
    //毛发颜色:
    public float hairColorR;
    public float hairColorG;
    public float hairColorB;
    //皮肤颜色:
    public float skinColorR;
    public float skinColorG;
    public float skinColorB;
    //面部涂鸦颜色:
    public float facialMarkColorR;
    public float facialMarkColorG;
    public float facialMarkColorB;

    //玩家属性等级:
    public int healthLevel;           //健康级别
    public int staminaLevel;        //体力级别
    public int focusLevel;            //法力级别
    public int poiseLevel;            //韧性级别
    public int strengthLevel;       //力量级别
    public int dexterityLevel;      //敏捷级别
    public int intelligenceLevel;  //智力级别
    public int faithLevel;             //信仰级别
    public float soulsRewardLevel;  //灵魂奖励级别

    //灵魂量:
    public int soulCount;         //灵魂量

    //玩家当前基础属性:
    public int currentHealth;               //当前生命值
    public float currentStamina;         //当前耐力值
    public float currentFocusPoints;   //当前法力值

    //玩家元素瓶总数:
    public int totalNumberElementBottle;    //元素瓶总数(含灰瓶一起)
    public int numberElement;                     //元素瓶个数(不含灰瓶)
    //元素瓶生命与专注值回复量级别:
    public int restoreHealthLevel;

    //玩家当前位置:
    public float positionX;
    public float positionY;
    public float positionZ;
    //上一个休息过的篝火的索引:
    public int previousBonfireIndex;

    //当前消耗品,武器,法术,装备槽位:
    public int currentSpellID;                              //法术槽位
    public int currentRightWeaponIndex;          //右手武器槽位
    public int currentLeftWeaponIndex;            //左手武器槽位
    public int currentSpellIndex;                       //当前记忆法术索引位置
    public int currentConsumableIndex;           //当前消耗品位置

    public int currentHelmetEquipmentID;           //当前头盔;
    public int currentTorsoEquipmentID;              //当前胸甲;
    public int currentLegEquipmentID;                 //当前腿甲;
    public int currentHandEquipmentID;              //当前臂甲;

    //当前左,右手武器快速装备槽位装备:
    public int[] weaponLeftHandSlots = new int[3];
    public int[] weaponRightHandSlots = new int[3];
    //当前弹药装备:
    public RangedAmmoItemData currentBowID;              //当前弓箭槽
    public RangedAmmoItemData spareBowID;                 //备用弓箭槽
    public RangedAmmoItemData otherAmmoID;             //其他弹丸槽
    public RangedAmmoItemData spareOtherAmmoID;   //备用其他弹丸槽
    //当前戒指装备槽位装备:
    public int[] currentRingSlots = {-1,-1,-1,-1 };
    //当前记忆的法术:
    public string[] memorySpellNameArray = { "", "", "", "", "", "", "", "" };
    //当前消耗品装备槽位装备:
    public ConsumableItemData[] currentConsumableSlots = new ConsumableItemData[10];

    //武器库存数组:
    public int[] weaponInventoryIDArray;
    public int[] helmetEquipmentInventoryIDArrayArray;
    public int[] torsoEquipmentInventoryIDArrayArray;
    public int[] legEquipmentInventoryIDArrayArray;
    public int[] handEquipmentInventoryIDArrayArray;
    //戒指库存:
    public int[] ringInventoryIDArray;
    //法术库存:
    public string[] spellInventoryNameArray;
    //消耗品库存数组:
    public ConsumableItemData[] consumableInventoryDataArray;
    //弹药库存:
    public RangedAmmoItemData[] ammoInventoryDataArray;


    #region 世界数据:
    public WorldEventData worldEventData;
    #endregion

    #region npc剧情
    public int mainlineProgress;
    public NpcPlotProgress[] npcPlotProgresArray;
    #endregion

    #region 商店与商品
    public ShopData[] allShopData;
    #endregion
}

//消耗品类:
[System.Serializable]
public class ConsumableItemData
{
    public string consumableItemName;            //消耗品id
    public int maxItemAmount;               //最大物品数量
    public int currentItemAmount;          //当前物品个数
}

//箭矢:
[System.Serializable]
public class RangedAmmoItemData
{
    public int rangedAmmoItemID;             //箭矢id
    public int currentAmmoItemAmount;   //当前箭矢个数
}

//世界事件:
[System.Serializable]
public class WorldEventData
{
    public int isLostSoulNotFound;                                //是否丢失的灵魂;(0是false , 1是true)

    public float soulLossPointX;                                        
    public float soulLossPointY;                                        //丢失的灵魂位置;
    public float soulLossPointZ;

    public int soulLossAmount;                                         //掉落灵魂数量;

    public int[] LitBonfire;                                                  //点燃的篝火数组:
    public int[] OpenDoorArray;                                        //已经开启的门的数组;
    public int[] OpenElevatorArray;                                   //已经开启的升降机;(升降机位置)
    public int[] AlreadyPickedItemArray;                          //已经拾取的物品数组;
    public int[] OpenChestArray;                                      //已经开启的宝箱的数组;
    public int[] OpenIllusionaryWallArray;                       //已经开启的隐藏墙壁;
    public int[] IsBossAefeatedArray;                               //击败的boss数组;
}

//游戏剧情:
[System.Serializable]
public class NpcPlotProgress 
{
    //npc名称
    public string npcName;
    //是否已经死亡:
    public bool isDeath;
    //是否已经敌对:
    public bool isHostile;
    //npc当前剧情
    public string currentPlotName;
    //当前剧情进度
    public int currentPlotProgress;
}

//商店:
[System.Serializable]
public class ShopData
{
    public string shopName;     //商店名称
    public CommodityData[] allCommodityData;
}
//商品:
[System.Serializable]
public class CommodityData
{
    public string commodityName;   //商品名称
    public int commodityAmount;     //商品数量
}