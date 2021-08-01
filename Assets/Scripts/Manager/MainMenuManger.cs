using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Ball.Config;

public class MainMenuManger : MonoBehaviour
{
    public bool developMode;

    GameObject[] bgms;

    private void Awake()
    {
        bgms = GameObject.FindGameObjectsWithTag("Music");
    }

    void Start()
    {
        InitializeGame();
    }

    void Update()
    { }

    public void StartGame()
    {
        if (bgms != null && bgms.Length > 0)
        {
            Object.Destroy(bgms[0]);
        }
        SceneManager.LoadScene(GameSetting.SCENEMAINBOARD);
    }

    void InitializeGame(bool loadByAddressable = true)
    {
        if (!loadByAddressable)
        {
            ConfigManager.Instance.LoadAllConfigs();
            Singleton<Me>.Instance.Load();

            return;
        }

        StartCoroutine(ConfigManager.LoadConfig(() =>
        {
            Singleton<Me>.Instance.Load();

            if (!developMode) return;
#if UNITY_EDITOR
            GameSetting.DebugMode = true;
            SceneManager.LoadScene(GameSetting.SCENEMAINBOARD);
#endif
        }));
    }
}
