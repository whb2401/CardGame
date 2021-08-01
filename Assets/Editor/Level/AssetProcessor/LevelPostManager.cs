using System.IO;

using UnityEngine;
using UnityEditor;

public class LevelPostManager : AssetPostprocessor
{
    const string LevelBundleName = "files/level";

    public enum ImportAssetType
    {
        Unknow,
        LevelJson,
        LevelBytes
    }

    static void OnPostprocessAllAssets(
        string[] importedAssets,
        string[] deletedAssets,
        string[] movedAssets,
        string[] movedFromAssetPaths)
    {
        foreach (string str in importedAssets)
        {
            var type = GetAssetType(str);
            if (type == ImportAssetType.LevelJson)
            {
                Debug.Log("Reimported Asset: " + str);
                // Game Level
                var path = Path.Combine(Application.dataPath.Replace("Assets", ""), str);
                Debug.Log("File Path: " + path);
                var assetPath = "";
                using (var streamReader = new StreamReader(path))
                {
                    var encrypted = Tools.EncryptAES(streamReader.ReadToEnd(), Tools.lKey);
                    assetPath = path.Replace("json", "bytes");
                    using (var stream = File.OpenWrite(assetPath))
                    {
                        ProtoBuf.Serializer.Serialize(stream, encrypted);
                    }
                }
                // delete import file
                FileUtil.DeleteFileOrDirectory(path);
                FileUtil.DeleteFileOrDirectory(path + ".meta");
                AssetDatabase.Refresh();
            }
            else if (type == ImportAssetType.LevelBytes)
            {
                SetBundleName(str, LevelBundleName);
            }
        }
        foreach (string str in deletedAssets)
        {
            Debug.Log("Deleted Asset: " + str);
        }

        for (int i = 0; i < movedAssets.Length; i++)
        {
            Debug.Log("Moved Asset: " + movedAssets[i] + " from: " + movedFromAssetPaths[i]);
        }
    }

    static ImportAssetType GetAssetType(string asPath)
    {
        var fileName = asPath.Substring(asPath.LastIndexOf("/") + 1);
        if (fileName.StartsWith("L") && fileName.EndsWith(".json"))
        {
            Debug.Log("FileName: " + fileName);
            return ImportAssetType.LevelJson;
        }
        else if (fileName.StartsWith("L") && fileName.EndsWith(".bytes"))
        {
            Debug.Log("FileName: " + fileName);
            return ImportAssetType.LevelBytes;
        }

        return ImportAssetType.Unknow;
    }

    public static void SetBundleName(string assetPath, string bundleName)
    {
        AssetImporter importer = AssetImporter.GetAtPath(assetPath);
        if (importer)
        {
            if (importer.assetBundleName != bundleName)
            {
                importer.assetBundleName = bundleName;

                Debug.LogFormat("Set BundleName：{0}, {1}", assetPath, bundleName);
                AssetDatabase.Refresh();
            }
            return;
        }

        Debug.LogWarningFormat("Can't Find Object At [{0}]", assetPath);
    }
}
