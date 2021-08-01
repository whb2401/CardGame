using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using FLH.Battle.Ability;

public class BrickHealthManager : MonoBehaviour
{
    public int brickHealth;
    private Text brickHealthText;
    private ScoreManager scoreManager;
    private SoundManager soundManager;

    [SerializeField] private SpriteRenderer spriteRenderer;
    private Vector3 startScale;
    private BoxCollider2D modelCollider;
    private CapsuleCollider modelCollider3;

    private Material matActor;
    private bool initialized;
    private int hp;

    public EnemyController ctrl;
    [HideInInspector]
    public CardInfo info;

    private Transform transBodyNode;

    private void Awake()
    {
        if (spriteRenderer != null)
        {
            startScale = spriteRenderer.transform.localScale;
            matActor = spriteRenderer.material;
        }
        scoreManager = FindObjectOfType<ScoreManager>();
        soundManager = FindObjectOfType<SoundManager>();
        brickHealth = GameManager.Instance.RowsBricksHealth;
        brickHealthText = GetComponentInChildren<Text>();
        modelCollider = GetComponent<BoxCollider2D>();
        modelCollider3 = GetComponent<CapsuleCollider>();
        hitEffects = new Dictionary<ParticleSystem, float>();

        // fakeData
        info = new CardInfo()
        {
            Properties = FLH.Core.Enums.PropertiesEnum.Wood
        };

        transBodyNode = transform.DeepFind("Sk_Particle_Body");
    }

    private void OnEnable()
    {
        if (modelCollider != null)
        {
            modelCollider.enabled = true;
        }
        if (modelCollider3 != null)
        {
            modelCollider3.enabled = true;
        }

        if (initialized)
        {
            brickHealth = hp;
            return;
        }

        ctrl.IsDead = false;
        brickHealth = GameManager.Instance.RowsBricksHealth;
    }

    private int curHPKey;
    public int HP
    {
        get
        {
            return hp ^ curHPKey;
        }
        set
        {
            curHPKey = Random.Range(0, 0xffff);
            hp = value ^ curHPKey;
        }
    }

    public void SetHealth(int hp)
    {
        if (GameSetting.DebugMode)
        {
            hp = 99;
        }
        initialized = true;
        this.hp = hp;
        startScale = transform.localScale;
    }

    float hitEffectTime;

    void Update()
    {
        brickHealthText.SetTextValue<int>(brickHealth);

        if (brickHealth <= 0)
        {
            if (modelCollider != null)
            {
                modelCollider.enabled = false;
            }
            if (modelCollider3 != null)
            {
                modelCollider3.enabled = false;
            }

            transform.DOShakePosition(.2f, new Vector3(.02f, .02f, 0));
            StartCoroutine(DestroyBrick());
        }

        if (hitEffects != null && hitEffects.Count > 0)
        {
            foreach (var item in hitEffects)
            {
                if (item.Value > 0)
                {
                    var time = item.Value - Time.deltaTime;
                    hitEffects[item.Key] = time;
                    if (time <= 0)
                    {
                        hitEffects[item.Key] = 0;
                        item.Key.gameObject.SetVisibility(false);
                    }
                }
            }
        }
    }

    IEnumerator DestroyBrick()
    {
        yield return new WaitForSeconds(.2f);

        scoreManager.IncreaseScore();
        var dp = Instantiate(GameManager.Instance.brickDestroyParticle, transform.position, Quaternion.identity);
        dp.transform.parent = transform;
        dp.transform.Reset();
        dp.transform.parent = ObjectPool.ParticleRoot;
        ctrl.IsDead = true;
        gameObject.SetActive(false);
        initialized = false;
        SetMaterialWhite(0f);
    }

    public void TakeDamage(float damageToTake)
    {
        if (brickHealth <= 0)
        {
            brickHealth = 0;
            return;
        }
        ctrl.OnMasterHurted();
        var damage = Mathf.CeilToInt(damageToTake);// temp
        brickHealth -= damage;
        GameManager.Instance.Calculate(damage);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GameSetting.TAGBALL) || collision.gameObject.CompareTag(GameSetting.TAGEXTRABALL))
        {
            AbilityManager.PreDamage(collision.gameObject, transform, () =>
            {
                // 物理
                HitTarget();
            });
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(GameSetting.TAGBALL) || collision.gameObject.CompareTag(GameSetting.TAGEXTRABALL))
        {
            AbilityManager.PreDamage(collision.gameObject, transform, () =>
            {
                // 物理
                HitTarget();
            });
        }
    }

    public void HitTarget()
    {
        // 逻辑
        GameManager.Instance.UpdateHitCount(true, 0);

        soundManager.source.PlayOneShot(soundManager.hitBrick);

        AbilityManager.Damage(transform);

        SetHitEffect();
        StartCoroutine(BeAttackFlashing());
        StartCoroutine(DOPunchScaleCoroutine3(0.4f, 0.1f));
    }

    public IEnumerator DOPunchScaleCoroutine(float amplitude, float time = 1f)
    {
        Vector3 midScale = startScale * (1 - amplitude);

        float count = 0;
        float firstDuration = time / 2;

        while (count < firstDuration)
        {
            count += Time.deltaTime;

            if (spriteRenderer != null)
            {
                spriteRenderer.transform.localScale = Vector3.Lerp(startScale, midScale, count / firstDuration);
            }
            yield return null;
        }

        count = 0;

        while (count < firstDuration)
        {
            count += Time.deltaTime;

            if (spriteRenderer != null)
            {
                spriteRenderer.transform.localScale = Vector3.Lerp(midScale, startScale, count / firstDuration);
            }
            yield return null;
        }

        if (spriteRenderer != null)
        {
            spriteRenderer.transform.localScale = startScale;
        }
    }

    public IEnumerator DOPunchScaleCoroutine3(float amplitude, float time = 1f)
    {
        var midScale = startScale * (1 - amplitude);

        float count = 0;
        float firstDuration = time / 2;

        while (count < firstDuration)
        {
            count += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, midScale, count / firstDuration);
            yield return null;
        }

        count = 0;

        while (count < firstDuration)
        {
            count += Time.deltaTime;
            transform.localScale = Vector3.Lerp(midScale, startScale, count / firstDuration);
            yield return null;
        }

        transform.localScale = startScale;
    }

    public IEnumerator BeAttackFlashing()
    {
        SetMaterialWhite(1f);
        yield return new WaitForSeconds(0.1f);
        SetMaterialWhite(0f);
    }

    void SetMaterialWhite(float value)
    {
        if (matActor != null)
        {
            matActor.SetFloat("_IsWhite", value);
        }
    }

    Dictionary<ParticleSystem, float> hitEffects;

    void SetHitEffect()
    {
        if (GameManager.Instance.EffectHitId == 0) return;

        var effectObj = GameManager.Instance.objectPool.GetPooledObject(GameManager.Instance.EffectHitId);
        if (effectObj == null) return;
        effectObj.GetComponent<ParticleLifeManager>().lifetime = effectObj.GetComponent<ParticleSystem>().main.duration;
        effectObj.transform.parent = transBodyNode ?? transform;
        effectObj.transform.Reset();
        effectObj.SetVisibility(true);
    }
}
