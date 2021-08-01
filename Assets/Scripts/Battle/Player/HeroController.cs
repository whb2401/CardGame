using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HeroController : MonoBehaviour
{
    public GameObject objShield;

    public Animator animatorHero;
    private Rigidbody2D rbHero;

    public SpriteRenderer spriteRendererHero;
    [SerializeField]
    private GameObject PlayerGhost;

    private bool isMoving = false;
    private readonly float speed = 2.0f;

    public bool OnStandBy { get; set; } = true;

    private static HeroController instance;
    public static HeroController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<HeroController>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        rbHero = GetComponent<Rigidbody2D>();
        objShield.SetVisibility(false);

        if (rbHero == null)
        {
            StartCoroutine(LoadCharacter((hero) =>
            {
                GameManager.Instance.SpawnHero(hero, 0);
                animatorHero = GetComponentInChildren<Animator>();
            }));
        }
    }

    void Update()
    {
        if (rbHero == null) return;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            print("press right.");
            rbHero.velocity = Vector2.right * speed;
            isMoving = true;
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            rbHero.velocity = Vector2.zero;
            isMoving = false;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            print("press left.");
            rbHero.velocity = Vector2.left * speed;
            isMoving = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            rbHero.velocity = Vector2.zero;
            isMoving = false;
        }

        if (isMoving)
        {
            GameObject ghost = Instantiate(PlayerGhost, transform.position, transform.rotation);
        }
    }

    public void ShootIt()
    {
        if (animatorHero != null)
        {
            animatorHero.SetTrigger("Skill01_Trigger");
        }
    }

    public void Move2FirePoint(Vector3 nextFirePoint, bool isDirect = false)
    {
        if (openShield) return;

        isMoving = true;
        transform.DOMoveX(nextFirePoint.x, isDirect ? 0f : 0.5f)
            .OnComplete(() =>
            {
                isMoving = false;
                OnStandBy = true;
                MoveShield(transform.localPosition, true);
            });
    }

    bool openShield;
    public void SwitchShield(bool isOpen)
    {
        openShield = isOpen;
        objShield.SetVisibility(isOpen);

        if (isOpen) { }
    }

    public void MoveShield(Vector3 position, bool isEnd = false)
    {
        if (objShield == null) return;
        var currentPos = objShield.transform.localPosition;
        objShield.transform.localPosition = new Vector3(position.x, currentPos.y, currentPos.z);
        if (isEnd)
        {
            GameManager.Instance.ballCtrl.currentBallState = BallController.BallState.wait;
        }
    }

    private IEnumerator LoadCharacter(Action<GameObject> callback)
    {
        var asyncLoad = AssetManager.LoadAssetsFromFile<GameObject>("Characters", "Ch_ZhuJue_01");
        yield return asyncLoad;

        if (asyncLoad.IsDone && asyncLoad.IsValid())
        {
            var model = asyncLoad.Result;
            var obj = (GameObject)Instantiate(model, transform);
            obj.SetActive(true);

            callback?.Invoke(obj);
        }
    }
}
