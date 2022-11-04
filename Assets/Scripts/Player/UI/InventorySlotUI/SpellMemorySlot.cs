using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class SpellMemorySlot : MonoBehaviour
    {
        public SpellItem mySpell;
        public Image icon;

        public void EnableMemorySlot(SpellItem spell)
        {
            if (spell.itemIcon != null)
            {
                mySpell = spell;
                icon.sprite = spell.itemIcon;
                icon.enabled = true;
            }
            else
            {
                DisableMemorySlot();
            }
        }

        public void DisableMemorySlot()
        {
            mySpell = null;
            icon.sprite = null;
            icon.enabled = false;
        }
    }
}