using Ball.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UILevelsView : BaseMonoBehaviour<UILevelsView>
{
    [Header("LegacyScroll")]
    public GameObject objLevel;
    private GridLayoutGroup gridLevels;

    [Header("LoopScroll")]
    public UILevelSectionItem itemLevelSection;
    public GridLayoutGroup grdLevelSections;
    public LoopVerticalScrollRect lsLevels;

    private List<UILevelSectionItem> listSections;

    private void Awake()
    {
        if (!GameSetting.DebugMode)
        {
            InitializeLevels();
        }
    }

    private void Start()
    {
        gridLevels = GetComponentInChildren<GridLayoutGroup>();
        lsLevels.gameObject.SetParentVisibility(false);
    }

    private void OnDisable()
    {
        grdLevelSections.enabled = true;
        currentSectionIndex = -1;
        foreach (var section in listSections.Where(x => x.IsSelected))
        {
            section.OnReset();
        }
    }

    // Data

    void InitializeLevels()
    {
        if (lsLevels != null)
        {
            var levels = ConfigManager.Instance.GetLevels();
            levels.Reverse();
            lsLevels.objectsToFill = levels.ToArray();

            var initObj = lsLevels.GetComponent<SG.InitOnStart>();
            if (initObj != null)
            {
                initObj.totalCount = levels.Count;
                initObj.offset = Mathf.FloorToInt(Singleton<Me>.Instance.Player.Levels.Count / 5) * 5;// skip
            }
            return;
        }

        // 旧的方式
        if (gridLevels == null)
        {
            return;
        }
        gridLevels.gameObject.SetVisibility(true);
        foreach (var item in ConfigManager.Instance.GetLevels())
        {
            var level = Instantiate(objLevel, gridLevels.transform);
            if (level)
            {
                var view = level.GetComponent<UILevelView>();
                view.Data = item;
                view.Bind();

                level.name += " (" + item.Id + ")";
                level.SetVisibility(true);
            }
        }
    }

    // View

    public void InitView()
    {
        if (listSections != null)
        {
            return;
        }

        // TODO: 数据从配表读取
        CreateLevelSection(0, "I", "Sunset", false);
        CreateLevelSection(1, "II", "Wild", true);
        CreateLevelSection(2, "III", "Zone", false);
        CreateLevelSection(3, "IV", "Scary", true);
    }

    void CreateLevelSection(int index, string levelNum, string levelName, bool isReverseBackground)
    {
        if (listSections == null)
        {
            listSections = new List<UILevelSectionItem>();
        }

        var newLevelSection = Instantiate<UILevelSectionItem>(itemLevelSection, grdLevelSections.transform);
        Debug.Assert(newLevelSection != null);
        newLevelSection.Bind(index, levelNum, levelName, isReverseBackground);
        newLevelSection.transform.Reset();
        newLevelSection.gameObject.SetVisibility(true);
        listSections.Add(newLevelSection);
    }

    public void Go2Home()
    {
        if (lsLevels != null)
        {
            var initObj = lsLevels.GetComponent<SG.InitOnStart>();
            if (initObj != null)
            {
                lsLevels.RefillCells(Mathf.FloorToInt(Singleton<Me>.Instance.Player.Levels.Count / 5) * 5);
            }

            lsLevels.gameObject.SetParentVisibility(false);
        }
    }

    // Event

    public void OnNextChapter()
    {
        grdLevelSections.enabled = true;
    }

    int currentSectionIndex = -1;
    const float sectionInterval = 280f;

    public void OnSelectedSection(int currentIndex)
    {
        grdLevelSections.enabled = false;
        lsLevels.gameObject.SetParentVisibility(true);
        lsLevels.GetComponent<CanvasGroup>().DOFade(0, 0.1f);
        lsLevels.GetComponentInParent<Image>().DOFade(0, 0.1f);

        var downMove = false;
        for (var i = 0; i < listSections.Count; i++)
        {
            var finalPosY = 0f;
            var section = listSections[i];
            if (section.IsSelected)
            {
                section.OnReset();
            }

            if (downMove)
            {
                if (currentSectionIndex > currentIndex)
                {
                    // 反选
                    if (section.Index > currentSectionIndex)
                    {
                        continue;
                    }
                }
                else
                {
                    if (currentSectionIndex > -1 && section.Index > currentIndex)
                    {
                        continue;
                    }
                }

                finalPosY = section.transform.localPosition.y - sectionInterval;
                section.transform.DOLocalMoveY(finalPosY, 0.2f);
            }

            if (currentSectionIndex > -1)
            {
                bool upMove = section.Index > 0;
                var isSibling = Mathf.Abs(currentIndex - currentSectionIndex) == 1;
                if (isSibling)
                {
                    if (section.Index <= currentSectionIndex ||
                        section.Index > currentSectionIndex + 1)
                    {
                        upMove = false;
                    }
                }
                else
                {
                    if (section.Index > currentIndex ||
                        currentSectionIndex == section.Index ||
                        currentSectionIndex == listSections.Count - 1)
                    {
                        upMove = false;
                    }
                }

                if (upMove)
                {
                    finalPosY = section.transform.localPosition.y + sectionInterval;
                    section.transform.DOLocalMoveY(finalPosY, 0.2f);
                }
            }

            if (currentIndex == section.Index)
            {
                downMove = true;
            }

            if (i == currentIndex)
            {
                if (finalPosY == 0)
                {
                    finalPosY = section.transform.localPosition.y;
                }

                lsLevels.transform.parent.DOLocalMoveY(finalPosY - 134f, 0.2f).OnComplete(() =>
                {
                    lsLevels.gameObject.SetVisibility(true);
                    lsLevels.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
                    lsLevels.GetComponentInParent<Image>().DOFade(1, 0.5f);
                });
            }
        }

        currentSectionIndex = currentIndex;
    }
}
