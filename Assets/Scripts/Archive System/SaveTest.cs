using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveTest : MonoBehaviour
{
    [Header("�浵��ʾ�ı�:")]
    public Text nameText;
    public Text oneText;
    public Text twoText;
    public int[] weaponIDArray;
    [Header("������ʾ�ı�:")]
    public Text nameText_Exhibition;
    public Text oneText_Exhibition;
    public Text twoText_Exhibition;
    public Text weaponIDArray_Exhibition;

    //����:(�ļ������ļ�����:)
    const string PLAYER_DATA_FILE_NAME = "PlayerData.cundang";

    public void SavingData()
    {
        SaveByJson();
    }

    public void Load()
    {
        LoadFromJson();
    }

    private void SaveByJson()
    {
        string playerName = nameText.text;
        int oneValue = int.Parse(oneText.text);
        int twoValue = int.Parse(twoText.text);
        SaveTestData data = new SaveTestData(playerName, oneValue, twoValue, weaponIDArray);

        SDSaveSystem.SaveSystem.SaveByJson(PLAYER_DATA_FILE_NAME, data);
    }

    private void LoadFromJson()
    {
        SaveTestData data = SDSaveSystem.SaveSystem.LoadFromJson<SaveTestData>(PLAYER_DATA_FILE_NAME);

        nameText_Exhibition.text = data.playerName;
        oneText_Exhibition.text = data.attributeOne.ToString("d2");
        twoText_Exhibition.text = data.attributeTwo.ToString("d2");

        for(int i = 0; i < data.weaponIDArray.Length; i++)
        {
            weaponIDArray_Exhibition.text = weaponIDArray_Exhibition.text + " " + data.weaponIDArray[i].ToString("d2");
        }
    }
}
