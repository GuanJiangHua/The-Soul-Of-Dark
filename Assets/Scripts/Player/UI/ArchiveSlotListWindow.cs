using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace SG
{
    public class ArchiveSlotListWindow : MonoBehaviour
    {
        [Header("�浵�б���:")]
        public GameObject archiveSlotPreform;
        public Transform archiveSlotParent;
        ArchiveSlot[] archiveSlots;
        [Header("�浵���ô���:")]
        public GameObject archiveProcessing;
        public ArchiveSlot currentArchiveSlot;

        //���´浵�б�:
        public void UpdateArchiveSlotList()
        {
            string fullPath = Application.persistentDataPath;
            if (Directory.Exists(fullPath))
            {
                DirectoryInfo direction = new DirectoryInfo(fullPath);
                Debug.Log("Path:" + fullPath);
                FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);

                Debug.Log(files.Length);
                if (files != null)
                {
                    archiveSlots = GetChildComponent(archiveSlotParent);

                    for (int i = 0; i < archiveSlots.Length; i++)
                    {

                        if (i < files.Length)
                        {
                            if (files[i].Name.EndsWith(".cundang") != true)
                            {
                                archiveSlots[i].gameObject.SetActive(false);
                                continue;
                            }

                            if (archiveSlots.Length < files.Length)
                            {
                                Instantiate(archiveSlotPreform, archiveSlotParent);
                                archiveSlots = GetChildComponent(archiveSlotParent);
                            }
                            string newName = files[i].Name.Split('.')[0];
                            archiveSlots[i].AddArchiveName(newName);
                        }
                        else
                        {
                            archiveSlots[i].gameObject.SetActive(false);
                        }
                    }

                }
            }
        }

        public void DisableOtherOptions(ArchiveSlot archive)
        {
            archiveSlots = GetChildComponent(archiveSlotParent);
            if (archive != null)
            {
                for (int i = 0; i < archiveSlots.Length; i++)
                {
                    if (archive != archiveSlots[i])
                    {
                        archiveSlots[i].DisableThisSlot();
                    }
                }
            }
            else
            {
                for (int i = 0; i < archiveSlots.Length; i++)
                {
                    archiveSlots[i].EnableThisSlot();
                }
            }
        }
        //����ѡ��ǰ��:
        public void NoLongerSelectCurrentSlot()
        {
            archiveProcessing.SetActive(false);
            DisableOtherOptions(null);
        }
        //ɾ����ǰ�۶�Ӧ�Ĵ浵:
        public void DeleteCurrentArchive()
        {
            if (currentArchiveSlot != null)
            {
                currentArchiveSlot.DeleteThisArchive();
                NoLongerSelectCurrentSlot();
                UpdateArchiveSlotList();
            }
        }
        private ArchiveSlot[] GetChildComponent(Transform parent)
        {
            ArchiveSlot[] child = new ArchiveSlot[parent.childCount];
            for (int i = 0; i < child.Length; i++)
            {
                child[i] = parent.GetChild(i).GetComponent<ArchiveSlot>();
            }
            return child;
        }
    }
}