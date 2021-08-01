using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BuildProject : ScriptableObject
{
    private const string STREAMING_ASSETS_DIR_NAME = "StreamingAssets";
    static bool isDebugBuild = false;

    [MenuItem("Tools/Build/iOS")]
    static void BuildWithiOS()
    {
        if (EditorUtility.DisplayDialog("Build", "Build iOS Project.", "OK", "Cancel"))
        {
            Build(BuildTarget.iOS);
        }
    }

    [MenuItem("Tools/Build/Android")]
    private static void BuildWithAndroid()
    {
        if (EditorUtility.DisplayDialog("Build", "Build Android Project.", "OK", "Cancel"))
        {
            Build(BuildTarget.Android);
        }
    }

    private static void Build(BuildTarget buildTarget)
    {
        var options = BuildOptions.AcceptExternalModificationsToPlayer | BuildOptions.CompressWithLz4;
        if (isDebugBuild)
        {
            options |= BuildOptions.Development;
            options |= BuildOptions.AllowDebugging;
            options |= BuildOptions.ConnectWithProfiler;

            PlayerSettings.enableInternalProfiler = true;
            PlayerSettings.SetStackTraceLogType(LogType.Log, StackTraceLogType.None);
        }

        var platformName = GetPlatformForAssetBundles(buildTarget);

        // TODO: build bundle first
        var abDir = AssetManager.GetBundleSourceFile($"{GameSetting.ABPROOT}/{platformName}", false, true);
        if (!Directory.Exists(abDir))
        {
            Directory.CreateDirectory(abDir);
        }

        BuildPipeline.BuildAssetBundles(abDir, BuildAssetBundleOptions.ChunkBasedCompression, buildTarget);
        Caching.ClearCache();
        AssetDatabase.Refresh();

        // scenes
        var scenes = EditorBuildSettings.scenes.Where(s => s.enabled).Select(s => s.path).ToArray();

        // copy 2 streamingAssets dir
        if (!CopyAndSavePreloadPacks(platformName))
        {
            Debug.LogError("Build Failure!");
            return;// end
        }

        // build
        var locationPathName = $"Build/{platformName}";
        BuildPipeline.BuildPlayer(scenes, locationPathName, buildTarget, options);
    }

    // ---

    private static bool CreateDirectory(string path)
    {
        try
        {
            Directory.CreateDirectory(path);
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex);
            return false;
        }

        return true;
    }

    private static void DeleteDirFiles(string strPath)
    {
        try
        {
            // 删除这个目录下的所有子目录
            if (Directory.GetDirectories(strPath).Length > 0)
            {
                foreach (var var in Directory.GetDirectories(strPath))
                {
                    Directory.Delete(var, true);
                }
            }

            // 删除这个目录下的所有文件
            if (Directory.GetFiles(strPath).Length > 0)
            {
                foreach (var var in Directory.GetFiles(strPath))
                {
                    File.Delete(var);
                }
            }

            Directory.Delete(strPath);
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex);
        }
    }

    private static bool CopyFile(string from, string to)
    {
        try
        {
            File.Copy(from, to, true);
            Debug.LogFormat("Copy '{0}' To '{1}'", from, to);
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex);
            return false;
        }

        return true;
    }

    private static bool CopyAndSavePreloadPacks(string platformName = "")
    {
        var bundles = new List<string>();

        var manifest =
            AssetManager.LoadAssetsFromFile<AssetBundleManifest>(platformName + "/" + platformName, true, true)[0];
        if (manifest != null)
        {
            bundles.AddRange(manifest.GetAllAssetBundles());
            foreach (var item in bundles)
            {
                Debug.Log("path: " + item);
            }
        }

        var dataPath = Application.dataPath;
        var platDir = $"{GameSetting.ABPROOT}/{platformName}";
        var bundleRoot = Path.Combine(dataPath.Replace("/Assets", ""), platDir);
        foreach (var path in bundles)
        {
            var bundlePath = Path.Combine(bundleRoot, path);
            if (!File.Exists(bundlePath))
            {
                Debug.LogErrorFormat("File Not Exists, [{0}]", bundlePath);
                continue;
            }

            var dest = Path.Combine(dataPath, STREAMING_ASSETS_DIR_NAME, GameSetting.ABPROOT, path);
            var parent = dest.Substring(0, dest.LastIndexOf('/'));
            if (!Directory.Exists(parent))
            {
                CreateDirectory(parent);
            }

            CopyFile(bundlePath, dest);
        }

        if (!string.IsNullOrEmpty(bundleRoot))
        {
            DeleteDirFiles(bundleRoot);
        }

        return bundles.Count > 0;
    }

    private static string GetPlatformForAssetBundles(BuildTarget target)
    {
        switch (target)
        {
            case BuildTarget.Android:
                return "Android";
            case BuildTarget.tvOS:
                return "tvOS";
            case BuildTarget.iOS:
                return "iOS";
            case BuildTarget.WebGL:
                return "WebGL";
            case BuildTarget.StandaloneWindows:
            case BuildTarget.StandaloneWindows64:
                return "Windows";
            case BuildTarget.StandaloneOSX:
                return "OSX";
            default:
                return null;
        }
    }
}