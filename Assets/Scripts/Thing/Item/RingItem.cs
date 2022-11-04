using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "�½���ָ��Ϣ", menuName = "��Ϸ��Ʒ/�½�װ��/�½���ָ")]
    public class RingItem : Item
    {
        [Header("�����ı�:")]
        [TextArea] public string equipmentIDescription;
        [Header("��������:")]
        [TextArea] public string functionDescription = "װ����ָ���Ի���������Լӳɡ�";

        //��������:
        [Header("�������Լӳ�:")]
        public int maxHealthAddition;                   //�������ֵ
        public float maxStaminaAddition;             //�������ֵ
        public float maxFocusPointsAddition;       //�����ֵ
        //���Եȼ�:
        [Header("���Եȼ��ӳ�:")]
        public int healthLevelAddition = 0;             //��������
        public int staminaLevelAddition = 0;          //��������
        public int focusLevelAddition = 0;              //��������
        public int strengthLevelAddition = 0;         //��������
        public int dexterityLevelAddition = 0;        //���ݼ���
        public int intelligenceLevelAddition = 0;    //��������
        public int faithLevelAddition = 0;               //��������
        public int soulsRewardLevelAddition = 0;  //��꽱������
        [Header("��ʱ����:")]
        public float temporaryPhysicalDamageAbsorption;
        public float temporaryFireDamageAbsorption;
        public float temporaryMagicDamageAbsorption;
        public float temporaryLightningDamageAbsorption;
        public float temporaryDarkDamageAbsorption;
        [Header("��ʱ�ֿ���:")]
        public float temporaryPoisonDefenseAbsorption;
        public float temporaryHemorrhageDefenseAbsorption;
        public float temporaryFrostDamageAbsorption;
        public float temporaryCurseDefenseAbsorption;

        //����Ч������
        //...

        //���Ͻ�ָ:
        public virtual void PutOnTheRing(PlayerManager player)
        {
            PlayerStatsManager playerState = player.playerStateManager;
            playerState.maxHealth += maxHealthAddition;
            playerState.maxStamina += maxStaminaAddition;
            playerState.maxFocusPoints += maxFocusPointsAddition;

            playerState.healthLevel += healthLevelAddition;
            playerState.staminaLevel += staminaLevelAddition;
            playerState.focusLevel += faithLevelAddition;
            playerState.strengthLevel += strengthLevelAddition;
            playerState.dexterityLevel += dexterityLevelAddition;
            playerState.intelligenceLevel += intelligenceLevelAddition;
            playerState.faithLevel += faithLevelAddition;
            playerState.soulsRewardLevel += soulsRewardLevelAddition;

            //���»�������:
            playerState.SetMaxHealthFromHealthLevel();
            playerState.SetMaxStaminaFromLevel();
            playerState.SetMaxFocusPointsFormLevel();

            //������ʱ����:
            playerState.temporaryPhysicalDamageAbsorption += temporaryPhysicalDamageAbsorption;
            playerState.temporaryFireDamageAbsorption += temporaryFireDamageAbsorption;
            playerState.temporaryMagicDamageAbsorption += temporaryMagicDamageAbsorption;
            playerState.temporaryLightningDamageAbsorption += temporaryLightningDamageAbsorption;
            playerState.temporaryDarkDamageAbsorption += temporaryDarkDamageAbsorption;
            //������ʱ�ֿ���:
            playerState.temporaryPoisonDefenseAbsorption += temporaryPoisonDefenseAbsorption;
            playerState.temporaryFrostDamageAbsorption += temporaryFrostDamageAbsorption;
            playerState.temporaryHemorrhageDefenseAbsorption += temporaryHemorrhageDefenseAbsorption;
            playerState.temporaryCurseDefenseAbsorption += temporaryCurseDefenseAbsorption;

            //�������ո�:
            player.playerInventoryManager.CountMemorySpellSlotNumber();
        }

        //ժ�½�ָ:
        public virtual void TakeOffTheRing(PlayerManager player)
        {
            PlayerStatsManager playerState = player.playerStateManager;
            playerState.maxHealth -= maxHealthAddition;
            playerState.maxStamina -= maxStaminaAddition;
            playerState.maxFocusPoints -= maxFocusPointsAddition;

            playerState.healthLevel -= healthLevelAddition;
            playerState.staminaLevel -= staminaLevelAddition;
            playerState.focusLevel -= faithLevelAddition;
            playerState.strengthLevel -= strengthLevelAddition;
            playerState.dexterityLevel -= dexterityLevelAddition;
            playerState.intelligenceLevel -= intelligenceLevelAddition;
            playerState.faithLevel -= faithLevelAddition;
            playerState.soulsRewardLevel -= soulsRewardLevelAddition;

            //���»�������:
            playerState.SetMaxHealthFromHealthLevel();
            playerState.SetMaxStaminaFromLevel();
            playerState.SetMaxFocusPointsFormLevel();

            //������ʱ����:
            playerState.temporaryPhysicalDamageAbsorption -= temporaryPhysicalDamageAbsorption;
            playerState.temporaryFireDamageAbsorption -= temporaryFireDamageAbsorption;
            playerState.temporaryMagicDamageAbsorption -= temporaryMagicDamageAbsorption;
            playerState.temporaryLightningDamageAbsorption -= temporaryLightningDamageAbsorption;
            playerState.temporaryDarkDamageAbsorption -= temporaryDarkDamageAbsorption;
            //������ʱ�ֿ���:
            playerState.temporaryPoisonDefenseAbsorption -= temporaryPoisonDefenseAbsorption;
            playerState.temporaryFrostDamageAbsorption -= temporaryFrostDamageAbsorption;
            playerState.temporaryHemorrhageDefenseAbsorption -= temporaryHemorrhageDefenseAbsorption;
            playerState.temporaryCurseDefenseAbsorption -= temporaryCurseDefenseAbsorption;

            //�������ո�:
            player.playerInventoryManager.CountMemorySpellSlotNumber();
        }
    }
}