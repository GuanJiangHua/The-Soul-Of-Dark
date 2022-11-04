using System.IO;
using UnityEngine;

namespace SDSaveSystem
{
    //����Json�Ĵ浵����:
    public static class SaveSystem
    {
        //�浵����:
        public static void SaveByJson(string saveFileName , object data)
        {
            var json = JsonUtility.ToJson(data);
            string path = Path.Combine(Application.persistentDataPath, saveFileName);

            try
            {
                File.WriteAllText(path , json);
                Debug.Log($"�浵�ɹ�:�浵�ļ�����·��:{path}.");
            }
            catch (System.Exception exception)
            {
                Debug.LogError($"�޷������ݱ��浽:{path} \n{exception}");
            }
        }

        //��������:
        public static T LoadFromJson<T>(string saveFileName)
        {
            string path = Path.Combine(Application.persistentDataPath, saveFileName);
            try
            {
                var json = File.ReadAllText(path);
                var data = JsonUtility.FromJson<T>(json);
                Debug.Log($"�����ɹ�:�浵�ļ�����·��:{path}.");
                return data;
            }catch (System.Exception exception)
            {
                Debug.LogError($"�޷���:{path} , ��ȡ����. \n{exception}");
                return default;
            }
        }

        //ɾ���浵:
        public static void DeleteSaveFile(string saveFileName)
        {
            string path = Path.Combine(Application.persistentDataPath, saveFileName);
            try
            {
                File.Delete(path);
            }
            catch (System.Exception exception)
            {
                Debug.LogError($"�޷���:{path} , ɾ���浵. \n{exception}");
            }
        }
    }
}