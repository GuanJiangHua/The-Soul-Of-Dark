using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveTestData
{
    public string playerName;
    public int attributeOne;
    public int attributeTwo;
    public int[] weaponIDArray;

    public SaveTestData(string nameString , int one ,int two , int[] array)
    {
        playerName = nameString;
        attributeOne = one;
        attributeTwo = two;
        weaponIDArray = array;
    }
}
