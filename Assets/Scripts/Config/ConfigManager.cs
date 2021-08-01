using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Ball.Config
{
    public class ConfigManager
    {
        public static ConfigManager Instance
        {
            get
            {
                return Singleton<ConfigManager>.Instance;
            }
        }

        public void LoadAllConfigs()
        {
            LoaderHelp.Load();
        }

        public static IEnumerator LoadConfig(System.Action callback)
        {
            yield return LoaderHelp.LoadConfig(callback);
        }

        public static void LoadConfigs()
        {
            LoaderHelp.LoadConfig();
        }

        public void LoadAllConfigsFromOriginal()
        {
            string dir = Application.streamingAssetsPath + "/Config";
            LoaderHelp.Load(dir);
        }

        // --- Get

        public static GlobalTemplateExt Gloabl
        {
            get
            {
                return GlobalTemplateManager.Template;
            }
        }

        public List<DuplicateTemplateExt> GetLevels()
        {
            return DuplicateTemplateManager.Instance.GetTemplates();
        }

        public DuplicateTemplateExt GetLevelInfo(int id)
        {
            return DuplicateTemplateManager.Instance.GetTemplate(id);
        }

        public DuplicateTemplateExt GetNextLevelInfo(int currentLevelId)
        {
            var currentLevelInfo = GetLevelInfo(currentLevelId);
            Assert.IsNotNull(currentLevelInfo, "Current Level Info Can Not Be Null.");
            var levels = GetLevels();
            var index = levels.IndexOf(currentLevelInfo);
            var nextIndex = index + 1;
            Assert.IsFalse(nextIndex >= levels.Count, "Level Index Over Array Length.");
            return levels[nextIndex];
        }

        public List<ItemTemplateExt> GetItems()
        {
            return ItemTemplateManager.Instance.GetTemplates();
        }

        public List<SkillTemplateExt> GetSkills()
        {
            return SkillTemplateManager.Instance.GetTemplates();
        }

        public CardTemplateExt GetCard(int id)
        {
            var cards = CardTemplateManager.Instance.GetTemplates();
            return CardTemplateManager.Instance.GetTemplate(id);
        }

        public EffectTemplateExt FindEffectTemplate(int id)
        {
            return EffectTemplateManager.Instance.GetTemplate(id);
        }
    }
}
