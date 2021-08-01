using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using FLH.Battle.Ability;
using Ball.Config;

namespace FLH.UI.Views.List
{
    public class UIDebugView : BaseMonoBehaviour<UIListItemSkillCard>
    {
        public Dropdown drpEffect;
        public Dropdown drpFlyEffect;
        public Dropdown drpHitEffect;
        public Dropdown drpPlayerModel;

        Dictionary<int, AbilityData> dicAbility;

        private void Awake()
        {
            dicAbility = new Dictionary<int, AbilityData>();
            GameManager.Instance.abilityManager.asyncLoadAbilityData = () =>
            {
                var data = GameManager.Instance.abilityManager.abilityData;
                var index = 1;
                data.Sort((x, y) => { return x.skillId.CompareTo(y.skillId); });
                foreach (var item in data)
                {
                    AddDropDownOptionsData(item.displayName);
                    dicAbility.Add(index, item);
                    index++;
                }
            };
        }

        void AddDropDownOptionsData(string itemText)
        {
            // 添加一个下拉选项
            var data = new Dropdown.OptionData
            {
                text = itemText
            };
            drpEffect.options.Add(data);
        }

        public void OnEffectValueChange(int value)
        {
            Debug.Log("点击下拉控件的索引是..." + value);
            if (value == 0) return;

            Debug.Assert(dicAbility.ContainsKey(value));
            var data = dicAbility[value];

            if (GameManager.Instance.EffectPondObj != null)
            {
                GameManager.Instance.objectPool.ReleasePool(GameManager.Instance.EffectPondObj.transform);
                Destroy(GameManager.Instance.EffectPondObj);
                GameManager.Instance.EffectPondObj = null;
            }

            foreach (var item in data.graphicReferences)
            {
                var template = ConfigManager.Instance.FindEffectTemplate(item.effectId);
                Debug.Assert(template != null);

                if (item.bindTarget == AbilityData.BindTarget.Master)
                {
                    if (item.hangOverMainBall)
                    {
                        StartCoroutine(LoadEffect(template.AssetPath, (obj) =>
                        {
                            GameManager.Instance.ballCtrl.AppendEffect(item.positionOnNode, obj, item);
                        }));
                    }
                }
                else if (item.bindTarget == AbilityData.BindTarget.Target)
                {
                    StartCoroutine(LoadEffect(template.AssetPath, (obj) =>
                    {
                        GameManager.Instance.EffectHitId = item.effectId;
                        GameManager.Instance.objectPool.AppendEffect2Pool(obj, $"hitEffect-{item.effectId}");
                    }));
                }
            }
        }

        private IEnumerator LoadEffect(string assetPath, Action<GameObject> callback)
        {
            var asyncLoad = AssetManager.LoadAssetAsync<GameObject>(assetPath);
            yield return asyncLoad;

            if (asyncLoad.IsDone && asyncLoad.IsValid())
            {
                callback?.Invoke(asyncLoad.Result);
            }
        }

        public void OnFlyEffectValueChange(int value)
        {
            Debug.Log("点击下拉控件的索引是..." + value);
        }

        public void OnHitEffectValueChange(int value)
        {
            Debug.Log("点击下拉控件的索引是..." + value);
        }

        public void OnPlayerModelValueChange(int value)
        {
            Debug.Log("点击下拉控件的索引是..." + value);
        }
    }
}
