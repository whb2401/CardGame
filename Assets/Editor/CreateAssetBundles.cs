using System.IO;

using UnityEngine;
using UnityEditor;

public class CreateAssetBundles
{
    [MenuItem("Tools/Assets/Build AssetBundles(Editor)")]
    static void BuildAllAssetBundles()
    {
        // "Windows" Editor
        var abDir = AssetManager.GetBundleSourceFile(GameSetting.ABPROOT, false);
        if (!Directory.Exists(abDir))
        {
            Directory.CreateDirectory(abDir);
        }

        BuildPipeline.BuildAssetBundles(abDir, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows64);
    }
}
