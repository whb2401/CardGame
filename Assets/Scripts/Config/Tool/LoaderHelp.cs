using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ball.Config
{
    public class LoaderHelp
    {
        public static void Load()
        {
            GeneratorConfig.Configs = AssetManager.LoadAssetsFromFile<TextAsset>(GameSetting.ABPCONFIG);
            LoadConfigManagers();
            GeneratorConfig.InvokeDelayInitAction();
        }

        public static void Unload() { }

        public static void LoadConfig()
        {
            AssetManager.LoadAssetsAsync<TextAsset>("Config", null).Completed += ConfigsLoaded;
        }

        public static IEnumerator LoadConfig(Action callback)
        {
            var asyncLoad = AssetManager.LoadAssetsAsync<TextAsset>("Config", null);
            yield return asyncLoad;

            if (asyncLoad.IsDone && asyncLoad.IsValid())
            {
                List<TextAsset> configs = (List<TextAsset>)asyncLoad.Result;
                GeneratorConfig.Configs = configs.ToArray();
                LoadConfigManagers();
                GeneratorConfig.InvokeDelayInitAction();

                callback?.Invoke();
            }
        }

        static void ConfigsLoaded(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<IList<TextAsset>> asyncLoad)
        {
            if (asyncLoad.IsDone && asyncLoad.IsValid())
            {
                List<TextAsset> configs = (List<TextAsset>)asyncLoad.Result;
                GeneratorConfig.Configs = configs.ToArray();
                LoadConfigManagers();
                GeneratorConfig.InvokeDelayInitAction();
            }
        }

        public static void Load(string dir)
        {
            GeneratorConfig.ConfigDir = dir;
            LoadConfigManagers();
            GeneratorConfig.InvokeDelayInitAction();
        }

        static void LoadConfigManagers()
        {
            GlobalTemplateManager.Load();
            DuplicateTemplateManager.Load();
            ItemTemplateManager.Load();
            CardTemplateManager.Load();
            SkillTemplateManager.Load();
            EffectTemplateManager.Load();
        }
    }
}
