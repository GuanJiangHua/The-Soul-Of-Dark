using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class Item : ScriptableObject
    {
        [Header("物品信息:")]
        public Sprite itemIcon;
        public string itemName;
    }
}
