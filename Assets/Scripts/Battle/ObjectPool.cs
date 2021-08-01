using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Ball.Config;

[System.Serializable]
public class ObjectPoolItem
{
    public int amountToPool;
    public GameObject objectToPool;
}

public class ObjectPool : BaseMonoBehaviour<ObjectPool>
{
    public List<GameObject> pooledObjects;
    public List<ObjectPoolItem> itemsToPool;

    public static Transform ParticleRoot { get; set; }

    GameObject root;
    private Dictionary<int, GameObject> _LoadedModel;
    const string _RootName = "PoolRoot";
    const string _ParticlesRootName = "Particles";
    void Start()
    {
        root = new GameObject(_RootName);
        pooledObjects = new List<GameObject>();
        _LoadedModel = new Dictionary<int, GameObject>();

        // Particles
        var particle = new GameObject(_ParticlesRootName);
        particle.transform.parent = root.transform;
        ParticleRoot = particle.transform;

        // Items
        foreach (var item in itemsToPool)
        {
            if (item.objectToPool.tag.Equals(GameSetting.TAGEXTRABALL))
            {
                // set scale
                // follow main ball
                item.objectToPool.transform.localScale = GameManager.Instance.MainBallScale;
            }

            Spawn2Pool(item.objectToPool, item.amountToPool, "_" + item.objectToPool.name + "s");
        }

        //if (GameSetting.DebugMode)
        //{
        //    GameManager.Instance.PlaceBricks();
        //}
    }

    void Update()
    { }

    void Spawn2Pool(GameObject obj2Pool, int amount2Pool, string customRootName = null)
    {
        if (obj2Pool == null)
        {
            Debug.LogError("obj2Pool is null.");
            return;
        }

        var pond = new GameObject(customRootName ?? obj2Pool.name + "s");
        pond.transform.parent = root.transform;

        for (int i = 0; i < amount2Pool; i++)
        {
            var obj = (GameObject)Instantiate(obj2Pool, pond.transform);
            if (customRootName.Contains("Effect"))
            {
                var plm = obj.AddComponent<ParticleLifeManager>();
                plm.ImmediateDestory = false;
                plm.transRoot = pond.transform;
                GameManager.Instance.EffectPondObj = pond;
            }
            obj.name += " (" + i + ")";
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (pooledObjects[i].activeInHierarchy == false && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }

    public GameObject GetPooledObject(int id)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (pooledObjects[i].activeInHierarchy == false &&
                pooledObjects[i].transform.parent.name.IndexOf(id.ToString()) > -1)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }

    public void ResetPool()
    {
        if (root == null) return;
        foreach (var subRoot in root.transform.GetChildList(true))
        {
            foreach (var item in subRoot.GetChildList())
            {
                item.SetVisibility(false);
            }
        }
    }

    public void ReleasePool(Transform transRoot)
    {
        var listRemove = new List<Transform>();

        foreach (var subRoot in transRoot.GetChildList(true))
        {
            listRemove.Add(subRoot);
        }

        while (listRemove.Count > 0)
        {
            var index = listRemove.Count - 1;
            var obj = listRemove[index].gameObject;
            pooledObjects.Remove(obj);
            Destroy(obj);
            listRemove.RemoveAt(index);
        }

        listRemove.Clear();
    }

    public void ReleasePool()
    {
        var listRemove = new List<Transform>();
        var listCache = new List<Transform>();

        foreach (var subRoot in root.transform.GetChildList(true))
        {
            var createByPool = subRoot.name.StartsWith("_");
            if (subRoot.name.IndexOf(_ParticlesRootName) > -1 ||
                createByPool)
            {
                if (createByPool)
                {
                    listCache.AddRange(subRoot.GetChildList(true));
                }
                continue;
            }

            listRemove.Add(subRoot);
        }

        while (listRemove.Count > 0)
        {
            var index = listRemove.Count - 1;
            Destroy(listRemove[index].gameObject);
            listRemove.RemoveAt(index);
        }

        listRemove.Clear();

        pooledObjects.Clear();
        foreach (var item in listCache)
        {
            pooledObjects.Add(item.gameObject);
        }

        listCache.Clear();
    }

    public void RebuildPool(Dictionary<int, int> items, bool loadBy3D = true, Action act = null)
    {
        ReleasePool();

        var count = 0;
        foreach (var item in items)
        {
            var card = ConfigManager.Instance.GetCard(item.Key);
            Assert.IsNotNull(card, "Build Pool, Card Is Null.");

            GameObject model = null;
            if (_LoadedModel.ContainsKey(item.Key))
            {
                _LoadedModel.TryGetValue(item.Key, out model);
            }
            if (model == null)
            {
                if (loadBy3D)
                {
                    StartCoroutine(LoadCharacter(() =>
                    {
                        count++;
                        if (count >= items.Count)
                        {
                            act?.Invoke();
                        }
                    }, card, item.Key, item.Value));
                    continue;
                }

                // TODO: Get Card Model GameObject
                model = AssetManager.LoadAssetsFromFile<GameObject>(string.Format("{0}/{1}",
                    GameSetting.ABPMODEL,
                    card.Model.Replace(" ", "").ToLower()))[0];
                _LoadedModel.Add(item.Key, model);
            }
            Assert.IsNotNull(card, "Build Pool, Card Load Model Is Null.");
            Spawn2Pool(model, item.Value, string.Format("[{0}].{1}s", card.Id, card.Model));
        }

        if (!loadBy3D) act?.Invoke();
    }

    private IEnumerator LoadCharacter(Action callback, CardTemplateExt card, int key, int value)
    {
        var asyncLoad = AssetManager.LoadAssetsFromFile<GameObject>("Characters", card.Model.Replace(" ", ""));
        yield return asyncLoad;

        if (asyncLoad.IsDone && asyncLoad.IsValid())
        {
            var model = asyncLoad.Result;
            _LoadedModel.Add(key, model);

            Spawn2Pool(model, value, string.Format("[{0}].{1}s", card.Id, card.Model));
            callback?.Invoke();
        }
    }

    public void AppendEffect2Pool(GameObject obj, string effectId)
    {
        Spawn2Pool(obj, 100, effectId.ToString());
    }

    // 改造缓存池

    private const int maxCount = 128;
    private Dictionary<string, List<GameObject>> pool = new Dictionary<string, List<GameObject>>();

    public void RecycleObj(GameObject obj)
    {
        var par = Camera.main;
        obj.transform.SetParentSafe(par.transform);
        obj.SetActive(false);

        if (pool.ContainsKey(obj.name))
        {
            if (pool[obj.name].Count < maxCount)
            {
                pool[obj.name].Add(obj);
            }
        }
        else
        {
            pool.Add(obj.name, new List<GameObject>() { obj });
        }
    }

    public void RecycleAllChildren(GameObject parent)
    {
        for (; parent.transform.childCount > 0;)
        {
            var tar = parent.transform.GetChild(0).gameObject;
            RecycleObj(tar);
        }
    }

    public GameObject GetObj(GameObject prefab)
    {
        // 池子中有
        GameObject result = null;
        if (pool.ContainsKey(prefab.name))
        {
            if (pool[prefab.name].Count > 0)
            {
                result = pool[prefab.name][0];
                result.SetActive(true);
                pool[prefab.name].Remove(result);
                return result;
            }
        }
        // 池子中缺少
        result = UnityEngine.Object.Instantiate(prefab);
        result.name = prefab.name;
        RecycleObj(result);
        GetObj(result);
        return result;
    }

    public GameObject GetObj(GameObject perfab, Transform parent)
    {
        var result = GetObj(perfab);
        result.transform.SetParentSafe(parent);
        return result;
    }

    public void Clear()
    {
        pool.Clear();
    }
}
