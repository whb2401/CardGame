using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UILevelSectionItem : BaseMonoBehaviour<UILevelSectionItem>
{
    public RectTransform rtBackground;
    public RectTransform rtPhotoWrapper;
    public Image imgPhoto;

    public Text textLevelNum;
    public Text textLevelName;

    public Transform anchorPhotoSelected, anchorLevelNumSelected;

    public int Index { get; private set; }
    public bool IsSelected { get; private set; }
    Sequence mySequence;
    float duration = .4f;

    UILevelsView objRoot;
    private void Awake()
    {
        objRoot = transform.parent.parent.GetComponentInParent<UILevelsView>();
        Debug.Assert(objRoot != null);
    }

    private void OnDestroy()
    {
        mySequence.Kill();
    }

    public void Bind(int index, string levelNum, string levelName, bool isReverseBackground)
    {
        Index = index;
        textLevelNum.SetTextValue(levelNum);
        textLevelName.SetTextValue(levelName);
        rtBackground.localRotation = new Quaternion(isReverseBackground ? 180 : 0, 0, 0, 0);
        // 绑定星级数据
    }

    private void AnimateReady()
    {
        mySequence = DOTween.Sequence();
        Tweener scaleBg = rtBackground.transform.DOScale(Vector3.one, duration);
        Tweener scalePhoto = rtPhotoWrapper.transform.DOScale(Vector3.one, duration);
        Tweener movePhoto = rtPhotoWrapper.transform.DOLocalMove(anchorPhotoSelected.localPosition, duration);
        Tweener moveTitle = textLevelNum.transform.DOLocalMove(anchorLevelNumSelected.localPosition, duration);
        Tweener fadeOutTitle = textLevelNum.DOFade(0, duration / 2);
        Tweener fadeInTitle = textLevelNum.DOFade(1, duration / 2);
        // Append：之后执行，Join：同时执行，AppendInterval：延迟执行
        mySequence.Append(scaleBg);
        mySequence.Join(scalePhoto);
        mySequence.Join(movePhoto);
        mySequence.Join(moveTitle);
        mySequence.Join(fadeOutTitle);
        mySequence.Append(fadeInTitle);
        mySequence.SetAutoKill(false);
    }

    // --- Event

    [ContextMenu("EffectTest")]
    public void OnSelected()
    {
        if (IsSelected)
        {
            return;
        }

        objRoot.OnSelectedSection(Index);
        IsSelected = true;

        if (mySequence == null)
        {
            AnimateReady();
            return;
        }
        mySequence.PlayForward();
    }

    [ContextMenu("Reset")]
    public void OnReset()
    {
        IsSelected = false;

        mySequence.PlayBackwards();
    }
}
