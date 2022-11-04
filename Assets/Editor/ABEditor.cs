using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;


public class ABEditor : MonoBehaviour
{
    //要打包的文件的根路径:
    public static string rootPath = Application.dataPath + "/AssetBundle";
    //所有打包的ab包的信息: 一个AB包压缩文件,对应一个AssetBundleBuild对象:
    public static List<AssetBundleBuild> assetBundleBuildList = new List<AssetBundleBuild>();
    //ab包文件的输出路径:
    public static string abOutputPath = Application.streamingAssetsPath;
    //asset资源属于哪一个ab包:(资源被压缩道那个ab包中去了)--[(资源的相对路径,ab包文件名)]
    public static Dictionary<string, string> asset2bundle = new Dictionary<string, string>();

    [MenuItem("ABEditor/BuildAssetBundle")]
    public static void BuildAssetBundle()
    {
        Debug.Log("开始打包--->生成所有模块的ab包");
        if(Directory.Exists(abOutputPath) == true)
        {
            //如果已经存在一个同名的输出文件,则删除上一个文件:
            Directory.Delete(abOutputPath, true);
        }

        //遍历打包文件下的所有一级文件:
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

            //计算资源的依赖关系:

            //刷新:
            AssetDatabase.Refresh();
        }

        Debug.Log("打包完成----->生成所有模块的ab包");
    }

    public static void ScanChildDireations(DirectoryInfo directoryInfo)
    {
        if (directoryInfo.Name.EndsWith("CSProject~"))
        {
            return;
        }
        //将此文件夹下所有文件打包成ab包:
        ScanCurrDirectory(directoryInfo);

        //遍历当前文件夹下所有文件夹:
        DirectoryInfo[] dirs = directoryInfo.GetDirectories();
        foreach(DirectoryInfo dir in dirs)
        {
            //递归：
            ScanChildDireations(dir);
        }
    }

    //扫描一个文件夹下的所有文件,并打包成一个ab包:
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

            //asset资源的相对路径名:"Assets/AssetBundle/资源名称.后缀"
            string assetName = fileInfo.FullName.Substring(Application.dataPath.Length - "Assets".Length).Replace('\\','/');
            assetNames.Add(assetName);
        }

        if(assetNames.Count > 0)
        {
            string assetbundleName = directoryInfo.FullName.Substring(Application.dataPath.Length + 1).Replace('\\', '_').ToLower();
            AssetBundleBuild build = new AssetBundleBuild();

            build.assetBundleName = assetbundleName;
            build.assetNames = new string[assetNames.Count];    //ab包内资源名称数组

            for(int i = 0; i < assetNames.Count; i++)
            {
                build.assetNames[i] = assetNames[i];
                //记录单个资源属于那个ab包文件:
                asset2bundle.Add(assetNames[i], assetbundleName);
            }

            assetBundleBuildList.Add(build);                                    //ab包保存到列表中
        }

    }
}
