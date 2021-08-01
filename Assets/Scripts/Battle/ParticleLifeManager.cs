using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLifeManager : MonoBehaviour
{
    public bool ImmediateDestory { get; set; } = true;
    public Transform transRoot;
    public float lifetime;
    private float lifetimeSeconds;

    void Start()
    {
        lifetimeSeconds = lifetime;
    }

    public void SetLifeTimeSeconds(float time)
    {
        lifetimeSeconds = time;
    }

    void Update()
    {
        if (ImmediateDestory)
        {
            lifetimeSeconds -= Time.deltaTime;
            if (lifetimeSeconds <= 0)
            {
                this.gameObject.SetActive(false);
                Destroy(this.gameObject);
            }
        }
        else
        {
            if (lifetime > 0)
            {
                lifetime -= Time.deltaTime;
                if (lifetime <= 0)
                {
                    lifetime = 0;
                    gameObject.SetVisibility(false);
                    if (transRoot != null)
                    {
                        transform.parent = transRoot;// back2pool
                    }
                }
            }
        }
    }
}
