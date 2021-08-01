using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

using Ball.Config;

public class UILevelView : MonoBehaviour
{
    public Text texLevel;
    public RectTransform rectLock;
    public RectTransform rectStars;
    public GameObject[] arrStar;

    public DuplicateTemplateExt Data { get; set; }

    private bool isLocked = true;

    void ScrollCellIndex(int idx)
    {
        isLocked = false;

        var dataCount = GameManager.Instance.LevelsTest.Count;
        var reverseIdx = Mathf.Abs(idx - (dataCount - 1));
        print(idx + ", reverse: " + reverseIdx);
        if (reverseIdx < dataCount)
        {
            texLevel.SetTextValue(GameManager.Instance.LevelsTest[reverseIdx]);
        }
        rectLock.SetVisibility(false);
        texLevel.SetVisibility(true);
    }

    void ScrollCellContent(object obj)
    {
        Assert.IsNotNull(obj, "Level Item Model Is Null.");

        Data = (DuplicateTemplateExt)obj;
        Bind();
    }

    void ResetView()
    {
        rectLock.SetVisibility(true);
        texLevel.SetVisibility(false);
        rectStars.SetVisibility(false);
        foreach (var item in arrStar)
        {
            item.SetVisibility(false);
        }
    }

    public void Bind()
    {
        if (Data == null)
        {
            return;
        }

        ResetView();

        var firstDuplicateId = DuplicateTemplateManager.Instance.GetFirst().Id;
        if (Singleton<Me>.Instance.Player != null)
        {
            if (Singleton<Me>.Instance.Player.CurrentDupId <= 0 &&
                Data.Id == firstDuplicateId)
            {
                GameManager.Instance.IsNextLevelGridLoaded = true;
                // new player
                LevelUnlocked();
            }
            else if (Singleton<Me>.Instance.Player.Levels.Select(x => x.Index).Contains(Data.Id))
            {
                LevelUnlocked();
                // bind pass level data
                foreach (var item in Singleton<Me>.Instance.Player.Levels)
                {
                    if (item.Index == Data.Id)
                    {
                        if (item.StarCount > 0)
                        {
                            rectStars.SetVisibility(true);
                            for (int i = 0; i < arrStar.Length; i++)
                            {
                                if (i < item.StarCount)
                                {
                                    arrStar[i].SetVisibility(true);
                                }
                            }
                        }
                        break;
                    }
                }
            }
            else
            {
                if (!GameManager.Instance.IsNextLevelGridLoaded)
                {
                    GameManager.Instance.IsNextLevelGridLoaded = true;
                    LevelUnlocked();
                }
            }
        }
        else
        {
            // default open level 1
            if (Data.Id == firstDuplicateId)
            {
                LevelUnlocked();
            }
        }
    }

    void LevelUnlocked()
    {
        isLocked = false;
        texLevel.SetTextValue(Data.Name);
        rectLock.SetVisibility(false);
        texLevel.SetVisibility(true);
    }

    public void OnLevelClicked()
    {
        if (isLocked)
        {
            return;
        }
        Debug.Log("you clicked level." + texLevel.text);

        Assert.IsNotNull(Data, "Level Data Is Null.");
        GameManager.Instance.EnterLevel(Data.Id);
    }

    [System.Obsolete]
    public void NewLevelData()
    {
        var template = ConfigManager.Instance.GetLevelInfo(Data.Id);
        if (template != null)
        {
            var playerLevels = Singleton<Me>.Instance.Player.Levels;
            var isExist = false;

            foreach (var item in playerLevels)
            {
                if (item.Index == template.Id)
                {
                    isExist = true;
                    break;
                }
            }

            if (!isExist)
            {
                playerLevels.Add(new LevelRecordInfo()
                {
                    Index = template.Id
                });

                Singleton<Me>.Instance.Player.CurrentDupId = template.Id;
                Singleton<Me>.Instance.Save();
            }
        }
    }
}
