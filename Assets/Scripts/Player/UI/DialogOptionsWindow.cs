using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class DialogOptionsWindow : MonoBehaviour
    {
        [Header("选项按钮预制体:")]
        public GameObject optionButtonPreform;
        public Transform optionButtonParent;
        OptionButtonSlot[] optionButtonSlots;
        int originalNumber;
        Vector2 originalSize;
        Vector2 originalPosition;

        private void Awake()
        {
            originalNumber = optionButtonParent.childCount;
            originalSize = optionButtonParent.GetComponent<RectTransform>().sizeDelta;
            originalPosition = optionButtonParent.GetComponent<RectTransform>().anchoredPosition;
        }
        public void UpdateOptionsWindow(PlayerManager player , NpcManager npc , OptionMethodClass[] optionMethods)
        {
            if (optionMethods == null)
            {
                gameObject.SetActive(false);
                return;
            }

            optionButtonSlots = GetoptionButtonSlotFromChild(optionButtonParent);
            for(int i = 0; i < optionButtonSlots.Length; i++)
            {
                if(i < optionMethods.Length)
                {
                    if(optionButtonSlots.Length < optionMethods.Length)
                    {
                        Instantiate(optionButtonPreform, optionButtonParent);
                        optionButtonSlots = GetoptionButtonSlotFromChild(optionButtonParent);
                    }
                    optionButtonSlots[i].AddOptionMethod(player, npc, optionMethods[i]);
                }
                else
                {
                    optionButtonSlots[i].ClearOptionMethod();
                }
            }

            Vector2 newSize = originalSize;
            Vector2 newPos = originalPosition;

            int scaleMultiple = optionMethods.Length;   //缩放倍数
            int direction = 1;
            if (optionMethods.Length < originalNumber) direction = -1;

            newSize.y = originalSize.y / originalNumber * scaleMultiple;
            newPos.y += direction * (originalSize.y / originalNumber * scaleMultiple)/2;
            
            optionButtonParent.GetComponent<RectTransform>().sizeDelta = newSize;
            optionButtonParent.GetComponent<RectTransform>().anchoredPosition = newPos;
        }
        private OptionButtonSlot[] GetoptionButtonSlotFromChild(Transform parent)
        {
            OptionButtonSlot[] childButtonArray =new OptionButtonSlot[parent.childCount];
            for(int i = 0; i < childButtonArray.Length; i++)
            {
                childButtonArray[i] = parent.GetChild(i).GetComponent<OptionButtonSlot>();
            }

            return childButtonArray;
        }
    }
}