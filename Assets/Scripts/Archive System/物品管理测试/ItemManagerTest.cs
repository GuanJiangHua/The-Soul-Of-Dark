using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class ItemManagerTest : MonoBehaviour
    {
        public Item urrentTestItem;

        public ItemManager itemManager;

        public void TestWeapon()
        {
            int index = itemManager.IndexInWeaponList((WeaponItem)urrentTestItem);

            Debug.Log("该物品在其类型列表中的索引:" + index);
        }
    }
}