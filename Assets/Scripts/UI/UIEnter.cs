using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

using DG.Tweening;

public class UIEnter : MonoBehaviour
{
    public Transform transLogoText;
    public Text textEnter;

    float ppTime = 0.5f;
    private void Start()
    {
        Logoflow(transLogoText.localPosition.y, transLogoText.localPosition.y + 10f);
        textEnter.DOFade(0.3f, 0.6f).SetLoops(-1, LoopType.Yoyo);
    }

    void Logoflow(float from, float to)
    {
        transLogoText.DOLocalMoveY(to, ppTime).OnComplete(() => Logoflow(to, from));
    }
}
