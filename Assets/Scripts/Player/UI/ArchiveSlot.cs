using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG {
    public class ArchiveSlot : MonoBehaviour
    {
        public string archiveName;
        [Header("�浵�����ı�:")]
        public Text archiveNameText;

        Button button;
        ArchiveSlotListWindow archiveSlotListWindow;
        private void Awake()
        {
            button = GetComponent<Button>();
            archiveSlotListWindow = GetComponentInParent<ArchiveSlotListWindow>();
        }
        public void AddArchiveName(string fileName)
        {
            archiveName = fileName.Split('_')[0];
            archiveNameText.text = "Archive: " + archiveName;
            gameObject.SetActive(true);
        }

        //ѡ�������:[���ĵ����ô���:]
        public void SelectThisSlot()
        {
            PlayerRegenerationData.playerName = archiveName;

            archiveSlotListWindow.archiveProcessing.SetActive(true);
            archiveSlotListWindow.archiveProcessing.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
            archiveSlotListWindow.currentArchiveSlot = this;
            archiveSlotListWindow.DisableOtherOptions(this);
        }
        //���������:
        public void DisableThisSlot()
        {
            button.interactable = false;
        }
        public void EnableThisSlot()
        {
            button.interactable = true;
        }
        //ɾ������浵:
        public void DeleteThisArchive()
        {
            PlayerSaveManager.DeletePlayerSaveFile(archiveName);
        }
    }
}