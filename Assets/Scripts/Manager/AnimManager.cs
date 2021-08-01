using UnityEngine;

public class AnimManager : BaseMonoBehaviour<AnimManager>
{
    private Animator animator;

    [HideInInspector]
    public bool IsLogicStop { get; set; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
        Debug.Assert(animator != null);
    }

    private void Start()
    {
        base.OnStart();
    }

    private void OnDestroy()
    {
        base.Destroy();
    }

    protected override void GameStatusChange(bool isPause)
    {
        if (IsLogicStop) return;

        animator.enabled = !isPause;
        base.GameStatusChange(isPause);
    }

    public void SetFreeze(bool isFreeze)
    {
        animator.enabled = !isFreeze;
    }
}
