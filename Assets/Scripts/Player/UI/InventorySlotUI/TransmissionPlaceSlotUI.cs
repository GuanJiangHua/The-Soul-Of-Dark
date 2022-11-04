using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class TransmissionPlaceSlotUI : MonoBehaviour
    {
        public Text locationNameText;
        public Image locationIcon;
        public BonfireLocation bonfire;
        [Header("�ǻ���:")]
        public bool isHome;
        public Button button;
        PlayerManager playerManager;

        private void Awake()
        {
            button = GetComponent<Button>();
            if (isHome)
            {
                playerManager = FindObjectOfType<PlayerManager>();
                locationNameText.text = bonfire.locationName;
                locationIcon.sprite = bonfire.locationIcon;
            }
        }

        //���øĵص��:
        public void SetThisLocation(BonfireLocation bonfire , PlayerManager player)
        {
            playerManager = player;
            locationNameText.text = bonfire.locationName;
            locationIcon.sprite = bonfire.locationIcon;
            this.bonfire = bonfire;

            if(player.previousBonfireIndex == WorldEventManager.single.GetBonfiresIndex(bonfire))
            {
                locationNameText.color = new Color(0.5f, 0.5f, 0.5f, 1);
                button.interactable = false;
            }
            else
            {
                locationNameText.color = new Color(1f, 1f, 1f, 1);
                button.interactable = true;
            }

            gameObject.SetActive(true);
        }

        //���øõص��:
        public void ResetTheLocation()
        {
            playerManager = null;
            locationNameText.text = "�ص�����";
            locationIcon.sprite = null;
            gameObject.SetActive(false);
        }

        //ѡ��õ���Ϊ���͵�:[����ð�ť]
        public void SelectTheLocation()
        {
            //������������ȼ������:Transmit Up -- Bonfire_Ignite
            playerManager.playerAnimatorManager.PlayTargetAnimation("Transmit Up", true);

            //������һ������ΪϨ���
           WorldEventManager.single.bonfireLocations[playerManager.previousBonfireIndex].isLgnite = false;
            //���汾��λ��Ӧ�������Ϊ��һ�������
            playerManager.previousBonfireIndex = WorldEventManager.single.GetBonfiresIndex(bonfire);
            //Ϩ����������:
            WorldEventManager.single.ExtinguishOtherBonfires(bonfire);
            //���ø���λ��Ϊ��Ӧ����λ��
            playerManager.rebirthPosition = bonfire.transform.position + bonfire.transform.forward;
            //����:
            PlayerSaveManager.SaveToFile(playerManager, PlotProgressManager.single, WorldEventManager.single);
            
            //�ر����д���ui����
            FindObjectOfType<TransferFunctionWindow>().gameObject.SetActive(false);

            //�л�����[��ť����]
        }

        public void MouseEntry()
        {
            locationIcon.sprite = bonfire.locationIcon;
        }
    }
}