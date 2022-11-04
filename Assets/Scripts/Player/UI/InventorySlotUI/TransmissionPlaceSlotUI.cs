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
        [Header("是基地:")]
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

        //设置改地点槽:
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

        //重置该地点槽:
        public void ResetTheLocation()
        {
            playerManager = null;
            locationNameText.text = "地点名称";
            locationIcon.sprite = null;
            gameObject.SetActive(false);
        }

        //选择该点作为传送点:[点击该按钮]
        public void SelectTheLocation()
        {
            //播放起身动画点燃篝火动作:Transmit Up -- Bonfire_Ignite
            playerManager.playerAnimatorManager.PlayTargetAnimation("Transmit Up", true);

            //设置上一个篝火为熄灭的
           WorldEventManager.single.bonfireLocations[playerManager.previousBonfireIndex].isLgnite = false;
            //保存本槽位对应的篝火点为上一个篝火点
            playerManager.previousBonfireIndex = WorldEventManager.single.GetBonfiresIndex(bonfire);
            //熄灭其他篝火:
            WorldEventManager.single.ExtinguishOtherBonfires(bonfire);
            //设置复活位置为对应篝火位置
            playerManager.rebirthPosition = bonfire.transform.position + bonfire.transform.forward;
            //保存:
            PlayerSaveManager.SaveToFile(playerManager, PlotProgressManager.single, WorldEventManager.single);
            
            //关闭所有传送ui窗口
            FindObjectOfType<TransferFunctionWindow>().gameObject.SetActive(false);

            //切换场景[按钮调用]
        }

        public void MouseEntry()
        {
            locationIcon.sprite = bonfire.locationIcon;
        }
    }
}