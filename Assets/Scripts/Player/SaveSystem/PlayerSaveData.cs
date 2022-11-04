using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSaveData
{
    public string playerName;   //�������
    public int playerLeve;          //��ҵȼ�

    //��������ģ��:
    public bool isMale = true;      //�Ա�
    public int headId;                   //ͷ��
    public int hairstyle;                //����
    public int facialHairId;           //����
    public int eyebrow;               //üë
    //ë����ɫ:
    public float hairColorR;
    public float hairColorG;
    public float hairColorB;
    //Ƥ����ɫ:
    public float skinColorR;
    public float skinColorG;
    public float skinColorB;
    //�沿Ϳѻ��ɫ:
    public float facialMarkColorR;
    public float facialMarkColorG;
    public float facialMarkColorB;

    //������Եȼ�:
    public int healthLevel;           //��������
    public int staminaLevel;        //��������
    public int focusLevel;            //��������
    public int poiseLevel;            //���Լ���
    public int strengthLevel;       //��������
    public int dexterityLevel;      //���ݼ���
    public int intelligenceLevel;  //��������
    public int faithLevel;             //��������
    public float soulsRewardLevel;  //��꽱������

    //�����:
    public int soulCount;         //�����

    //��ҵ�ǰ��������:
    public int currentHealth;               //��ǰ����ֵ
    public float currentStamina;         //��ǰ����ֵ
    public float currentFocusPoints;   //��ǰ����ֵ

    //���Ԫ��ƿ����:
    public int totalNumberElementBottle;    //Ԫ��ƿ����(����ƿһ��)
    public int numberElement;                     //Ԫ��ƿ����(������ƿ)
    //Ԫ��ƿ������רעֵ�ظ�������:
    public int restoreHealthLevel;

    //��ҵ�ǰλ��:
    public float positionX;
    public float positionY;
    public float positionZ;
    //��һ����Ϣ�������������:
    public int previousBonfireIndex;

    //��ǰ����Ʒ,����,����,װ����λ:
    public int currentSpellID;                              //������λ
    public int currentRightWeaponIndex;          //����������λ
    public int currentLeftWeaponIndex;            //����������λ
    public int currentSpellIndex;                       //��ǰ���䷨������λ��
    public int currentConsumableIndex;           //��ǰ����Ʒλ��

    public int currentHelmetEquipmentID;           //��ǰͷ��;
    public int currentTorsoEquipmentID;              //��ǰ�ؼ�;
    public int currentLegEquipmentID;                 //��ǰ�ȼ�;
    public int currentHandEquipmentID;              //��ǰ�ۼ�;

    //��ǰ��,������������װ����λװ��:
    public int[] weaponLeftHandSlots = new int[3];
    public int[] weaponRightHandSlots = new int[3];
    //��ǰ��ҩװ��:
    public RangedAmmoItemData currentBowID;              //��ǰ������
    public RangedAmmoItemData spareBowID;                 //���ù�����
    public RangedAmmoItemData otherAmmoID;             //���������
    public RangedAmmoItemData spareOtherAmmoID;   //�������������
    //��ǰ��ָװ����λװ��:
    public int[] currentRingSlots = {-1,-1,-1,-1 };
    //��ǰ����ķ���:
    public string[] memorySpellNameArray = { "", "", "", "", "", "", "", "" };
    //��ǰ����Ʒװ����λװ��:
    public ConsumableItemData[] currentConsumableSlots = new ConsumableItemData[10];

    //�����������:
    public int[] weaponInventoryIDArray;
    public int[] helmetEquipmentInventoryIDArrayArray;
    public int[] torsoEquipmentInventoryIDArrayArray;
    public int[] legEquipmentInventoryIDArrayArray;
    public int[] handEquipmentInventoryIDArrayArray;
    //��ָ���:
    public int[] ringInventoryIDArray;
    //�������:
    public string[] spellInventoryNameArray;
    //����Ʒ�������:
    public ConsumableItemData[] consumableInventoryDataArray;
    //��ҩ���:
    public RangedAmmoItemData[] ammoInventoryDataArray;


    #region ��������:
    public WorldEventData worldEventData;
    #endregion

    #region npc����
    public int mainlineProgress;
    public NpcPlotProgress[] npcPlotProgresArray;
    #endregion

    #region �̵�����Ʒ
    public ShopData[] allShopData;
    #endregion
}

//����Ʒ��:
[System.Serializable]
public class ConsumableItemData
{
    public string consumableItemName;            //����Ʒid
    public int maxItemAmount;               //�����Ʒ����
    public int currentItemAmount;          //��ǰ��Ʒ����
}

//��ʸ:
[System.Serializable]
public class RangedAmmoItemData
{
    public int rangedAmmoItemID;             //��ʸid
    public int currentAmmoItemAmount;   //��ǰ��ʸ����
}

//�����¼�:
[System.Serializable]
public class WorldEventData
{
    public int isLostSoulNotFound;                                //�Ƿ�ʧ�����;(0��false , 1��true)

    public float soulLossPointX;                                        
    public float soulLossPointY;                                        //��ʧ�����λ��;
    public float soulLossPointZ;

    public int soulLossAmount;                                         //�����������;

    public int[] LitBonfire;                                                  //��ȼ����������:
    public int[] OpenDoorArray;                                        //�Ѿ��������ŵ�����;
    public int[] OpenElevatorArray;                                   //�Ѿ�������������;(������λ��)
    public int[] AlreadyPickedItemArray;                          //�Ѿ�ʰȡ����Ʒ����;
    public int[] OpenChestArray;                                      //�Ѿ������ı��������;
    public int[] OpenIllusionaryWallArray;                       //�Ѿ�����������ǽ��;
    public int[] IsBossAefeatedArray;                               //���ܵ�boss����;
}

//��Ϸ����:
[System.Serializable]
public class NpcPlotProgress 
{
    //npc����
    public string npcName;
    //�Ƿ��Ѿ�����:
    public bool isDeath;
    //�Ƿ��Ѿ��ж�:
    public bool isHostile;
    //npc��ǰ����
    public string currentPlotName;
    //��ǰ�������
    public int currentPlotProgress;
}

//�̵�:
[System.Serializable]
public class ShopData
{
    public string shopName;     //�̵�����
    public CommodityData[] allCommodityData;
}
//��Ʒ:
[System.Serializable]
public class CommodityData
{
    public string commodityName;   //��Ʒ����
    public int commodityAmount;     //��Ʒ����
}