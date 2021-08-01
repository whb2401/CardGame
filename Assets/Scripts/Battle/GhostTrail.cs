using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTrail : MonoBehaviour
{
    private SpriteRenderer spriteRendererGhost;
    private float timerGhostDisappear = 0.2f;

    private void Awake()
    {
        spriteRendererGhost = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        transform.position = HeroController.Instance.transform.position;
        transform.localScale = HeroController.Instance.transform.localScale;

        spriteRendererGhost.sprite = HeroController.Instance.spriteRendererHero.sprite;
        spriteRendererGhost.color = new Vector4(50, 50, 50, 0.2f);
    }

    void Update()
    {
        timerGhostDisappear -= Time.deltaTime;

        if (timerGhostDisappear <= 0)
        {
            Destroy(gameObject);
        }
    }
}
