using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

using Ball.Config;

public class UIManager : MonoBehaviour
{
    public Transform root;
    public RectTransform rectBattle;
    public RectTransform rectMainView;
    public RectTransform rectCardView;
    public RectTransform rectLevels;
    public RectTransform rectNextBattle;
    public RectTransform rectEndGame;
    public GameObject objDebug;

    private void Awake()
    { }

    private void Start()
    {
        if (!GameSetting.DebugMode)
        {
            EnterMainView();
        }
        else
        {
            EnterBattleView();
        }

        // fix eventsystem no response
        // https://blog.csdn.net/SerenaHaven/article/details/80845994
        var threshold = (int)Mathf.Ceil(UnityEngine.Screen.dpi / 40.0f);
        UnityEngine.EventSystems.EventSystem.current.pixelDragThreshold = threshold;
    }

    public void EnterMainView()
    {
        ResetView();
        rectMainView.SetVisibility(true);
    }
    
    public void EnterCardView()
    {
        ResetView();
        rectCardView.SetVisibility(true);
    }

    public void EnterSelectLevelView()
    {
        ResetView();
        rectLevels.GetComponent<UILevelsView>().InitView();
        rectLevels.SetVisibility(true);
    }

    public void EnterBattleView()
    {
        ResetView();
        objDebug.SetVisibility(GameSetting.DebugMode);
        rectBattle.SetVisibility(true);
    }

    public void EndGame()
    {
        ResetView();
        rectEndGame.SetVisibility(true);
    }

    public void Ready2Next()
    {
        ResetView();
        rectNextBattle.SetVisibility(true);
    }

    void ResetView()
    {
        foreach (var item in root.GetChildList())
        {
            item.SetVisibility(false);
        }
    }

    public void OnBack2Home()
    {
        EnterMainView();
    }

    public void On2SelectLevelView()
    {
        EnterSelectLevelView();
    }

    public void OnEnterNextLevel()
    {
        var nextLevelInfo = ConfigManager.Instance.GetNextLevelInfo(Singleton<Me>.Instance.Player.CurrentDupId);
        GameManager.Instance.EnterLevel(nextLevelInfo.Id);
    }

    public void On2Home()
    {
        EnterMainView();
        GameManager.Instance.SwtichBGM(false);
        GameManager.Instance.IsNextLevelGridLoaded = false;
        rectLevels.GetComponent<UILevelsView>().Go2Home();
    }
}
