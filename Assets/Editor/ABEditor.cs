using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;


public class ABEditor : MonoBehaviour
{
    //Ҫ������ļ��ĸ�·��:
    public static string rootPath = Application.dataPath + "/AssetBundle";
    //���д����ab������Ϣ: һ��AB��ѹ���ļ�,��Ӧһ��AssetBundleBuild����:
    public static List<AssetBundleBuild> assetBundleBuildList = new List<AssetBundleBuild>();
    //ab���ļ������·��:
    public static string abOutputPath = Application.streamingAssetsPath;
    //asset��Դ������һ��ab��:(��Դ��ѹ�����Ǹ�ab����ȥ��)--[(��Դ�����·��,ab���ļ���)]
    public static Dictionary<string, string> asset2bundle = new Dictionary<string, string>();

    [MenuItem("ABEditor/BuildAssetBundle")]
    public static void BuildAssetBundle()
    {
        Debug.Log("��ʼ���--->��������ģ���ab��");
        if(Directory.Exists(abOutputPath) == true)
        {
            //����Ѿ�����һ��ͬ��������ļ�,��ɾ����һ���ļ�:
            Directory.Delete(abOutputPath, true);
        }

        //��������ļ��µ�����һ���ļ�:
        DirectoryInfo rootDir = new DirectoryInfo(rootPath);
        DirectoryInfo[] Dirs = rootDir.GetDirectories();
        foreach (DirectoryInfo moduleDir in Dirs)
        {
            assetBundleBuildList.Clear();
            asset2bundle.Clear();

            ScanChildDireations(moduleDir);
            AssetDatabase.Refresh();
            string moduleOutputPath = abOutputPath + "/" + moduleDir;
            
            if(Directory.Exists(moduleOutputPath) == true)
            {
                Directory.Delete(moduleOutputPath, true);
            }

            Directory.CreateDirectory(moduleOutputPath);

            BuildPipeline.BuildAssetBundles(moduleOutputPath, assetBundleBuildList.ToArray(), BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);

            //������Դ��������ϵ:

            //ˢ��:
            AssetDatabase.Refresh();
        }

        Debug.Log("������----->��������ģ���ab��");
    }

    public static void ScanChildDireations(DirectoryInfo directoryInfo)
    {
        if (directoryInfo.Name.EndsWith("CSProject~"))
        {
            return;
        }
        //�����ļ����������ļ������ab��:
        ScanCurrDirectory(directoryInfo);

        //������ǰ�ļ����������ļ���:
        DirectoryInfo[] dirs = directoryInfo.GetDirectories();
        foreach(DirectoryInfo dir in dirs)
        {
            //�ݹ飺
            ScanChildDireations(dir);
        }
    }

    //ɨ��һ���ļ����µ������ļ�,�������һ��ab��:
    private static void ScanCurrDirectory(DirectoryInfo directoryInfo)
    {
        List<string> assetNames = new List<string>();

        FileInfo[] fileInfoList = directoryInfo.GetFiles();
        foreach(FileInfo fileInfo in fileInfoList)
        {
            if (fileInfo.Name.EndsWith(".mete"))
            {
                continue;
            }

            //asset��Դ�����·����:"Assets/AssetBundle/��Դ����.��׺"
            string assetName = fileInfo.FullName.Substring(Application.dataPath.Length - "Assets".Length).Replace('\\','/');
            assetNames.Add(assetName);
        }

        if(assetNames.Count > 0)
        {
            string assetbundleName = directoryInfo.FullName.Substring(Application.dataPath.Length + 1).Replace('\\', '_').ToLower();
            AssetBundleBuild build = new AssetBundleBuild();

            build.assetBundleName = assetbundleName;
            build.assetNames = new string[assetNames.Count];    //ab������Դ��������

            for(int i = 0; i < assetNames.Count; i++)
            {
                build.assetNames[i] = assetNames[i];
                //��¼������Դ�����Ǹ�ab���ļ�:
                asset2bundle.Add(assetNames[i], assetbundleName);
            }

            assetBundleBuildList.Add(build);                                    //ab�����浽�б���
        }

    }
}
