using System.IO;
using UnityEngine;

namespace SDSaveSystem
{
    //基于Json的存档机制:
    public static class SaveSystem
    {
        //存档方法:
        public static void SaveByJson(string saveFileName , object data)
        {
            var json = JsonUtility.ToJson(data);
            string path = Path.Combine(Application.persistentDataPath, saveFileName);

            try
            {
                File.WriteAllText(path , json);
                Debug.Log($"存档成功:存档文件保存路径:{path}.");
            }
            catch (System.Exception exception)
            {
                Debug.LogError($"无法将数据保存到:{path} \n{exception}");
            }
        }

        //读档方法:
        public static T LoadFromJson<T>(string saveFileName)
        {
            string path = Path.Combine(Application.persistentDataPath, saveFileName);
            try
            {
                var json = File.ReadAllText(path);
                var data = JsonUtility.FromJson<T>(json);
                Debug.Log($"读档成功:存档文件保存路径:{path}.");
                return data;
            }catch (System.Exception exception)
            {
                Debug.LogError($"无法从:{path} , 读取数据. \n{exception}");
                return default;
            }
        }

        //删除存档:
        public static void DeleteSaveFile(string saveFileName)
        {
            string path = Path.Combine(Application.persistentDataPath, saveFileName);
            try
            {
                File.Delete(path);
            }
            catch (System.Exception exception)
            {
                Debug.LogError($"无法从:{path} , 删除存档. \n{exception}");
            }
        }
    }
}