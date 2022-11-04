using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG {
    public class ArchiveSlot : MonoBehaviour
    {
        public string archiveName;
        [Header("存档名称文本:")]
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

        //选择这个槽:[打开文档处置窗口:]
        public void SelectThisSlot()
        {
            PlayerRegenerationData.playerName = archiveName;

            archiveSlotListWindow.archiveProcessing.SetActive(true);
            archiveSlotListWindow.archiveProcessing.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
            archiveSlotListWindow.currentArchiveSlot = this;
            archiveSlotListWindow.DisableOtherOptions(this);
        }
        //禁用这个槽:
        public void DisableThisSlot()
        {
            button.interactable = false;
        }
        public void EnableThisSlot()
        {
            button.interactable = true;
        }
        //删除这个存档:
        public void DeleteThisArchive()
        {
            PlayerSaveManager.DeletePlayerSaveFile(archiveName);
        }
    }
}