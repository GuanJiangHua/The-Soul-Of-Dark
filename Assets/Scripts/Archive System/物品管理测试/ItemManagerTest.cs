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

            Debug.Log("����Ʒ���������б��е�����:" + index);
        }
    }
}