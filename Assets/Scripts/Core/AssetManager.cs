
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetManager
{
    public delegate void LoadObjectHandler(bool loadSuccess, string strPath, string strObject, UnityEngine.Object objCache);

    public static AsyncOperationHandle<T> LoadAssetsFromFile<T>(string categoryPath, string name)
    {
        return Addressables.LoadAssetAsync<T>($"{categoryPath}/{name}");
    }

    public static AsyncOperationHandle<T> LoadAssetAsync<T>(string path)
    {
        return Addressables.LoadAssetAsync<T>($"{path}");
    }

    public static AsyncOperationHandle<IList<T>> LoadAssetsAsync<T>(string name, System.Action<T> callback)
    {
        return Addressables.LoadAssetsAsync<T>($"{name}", callback);
    }

    public static T[] LoadAssetsFromFile<T>(string path, bool immediateUnload = false, bool isBuild = false) where T : Object
    {
        var filePath = GetBundleSourceFile(Path.Combine(GameSetting.ABPROOT, path), false, isBuild);
        Debug.Log("LoadAssetsFromFile>>filePath: " + filePath);
        Assert.IsTrue(File.Exists(filePath), "Bundle File Not Exist.");

        var bundle = AssetBundle.LoadFromFile(filePath);
        Assert.IsNotNull(bundle, "U Must Execute [Tools-Assets-Build Asset Bundles] First.");// 未生成相应的Bundle

        if (bundle != null)
        {
            T[] result = bundle.LoadAllAssets<T>();
            if (immediateUnload)
            {
                bundle.Unload(false);
            }
            return result;
        }

        return null;
    }

    public static void SetImageTexture(UnityEngine.UI.Image img, string path, string name, System.Action callback = null)
    {
        if (img == null)
        {
            Debug.LogError("UI.Image cannot be null!");
            callback?.Invoke();
            return;
        }

        LoadAsync(path, name,
                 (bool success, string strBundle, string strObject, UnityEngine.Object objCache) =>
                 {
                     if (!success)
                     {
                         callback?.Invoke();
                         return;
                     }

                     var texture = objCache as Sprite;
                     if (texture == null)
                     {
                         Debug.LogError("Loaded object can't convert to texture.");
                         callback?.Invoke();
                         return;
                     }

                     img.sprite = texture;
                     callback?.Invoke();
                 });
    }

    public static void LoadAsync(string fullPath, string objectName, LoadObjectHandler handler = null)
    {
        var objCache = LoadAssetsFromFile<UnityEngine.Object>(fullPath + objectName);
        if (objCache == null)
        {
            handler(false, fullPath, objectName, null);
        }
        else
        {
            handler(true, fullPath, objectName, objCache[1]);
        }
    }

    public static void UnloadFromFileAssets() { }

    public static string GetBundleSourceFile(string path, bool forWWW = true, bool isBuild = false)
    {
        string filePath = null;
        var dataPath = Application.dataPath;

#if UNITY_EDITOR
        dataPath = Application.dataPath.Replace("/Assets", "");// ab放在资源目录外的根目录，避免生成资源meta文件，不进git管理
        if (forWWW)
            filePath = string.Format("file://{0}/{1}", dataPath, path);
        else
        {
            if (!isBuild)
            {
                filePath = Path.Combine(dataPath, path.Replace("AssetBundles", "AssetBundles/Editor"));// "Windows" Editor
            }
            else
            {
                filePath = Path.Combine(dataPath, path);
            }
        }
#elif UNITY_ANDROID
            if (forWWW)
                filePath = string.Format("jar:file://{0}!/assets/{1}", dataPath, path);
            else
                filePath = string.Format("{0}!assets/{1}", dataPath, path);
#elif UNITY_IOS
            if (forWWW)
                filePath = string.Format("file://{0}/Raw/{1}", dataPath, path);
            else
                filePath = string.Format("{0}/Raw/{1}", dataPath, path);
#endif

        return filePath;
    }
}
