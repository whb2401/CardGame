using DG.Tweening;
using FLH.Core.Enums;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    const string WALK = "ToWalk";
    const string REST = "ToRest";
    const string MOVE = "MoveSpeed";

    public AnimManager animManager;
    public BrickMovementController moveCtrl;
    [HideInInspector]
    public bool IsDead { get; set; }

    Dictionary<BuffTypeEnum, int> dicBuffCountDown;

    public const int HIGHLIGHT_FLASH_TIME = 200;
    public const bool HIGHLIGHT_FLASH_FADE = true;

    protected Material actorMaskMaterial = null;
    private Color m_flashColorMin = new Color(1, 1, 1, 0);
    private Color m_flashColorMax = new Color(1, 1, 1, 0.52f);
    private float m_flashingTime = 0;

    private void Awake()
    {
        IsDead = false;
        dicBuffCountDown = new Dictionary<BuffTypeEnum, int>();

        var actorRenderer = this.GetComponentInChildren<SkinnedMeshRenderer>();
        actorMaskMaterial = AddShader(actorRenderer, "Custom/ActorMask");
    }

    private void Update()
    {
        if (m_flashingTime > 0)
        {
            m_flashingTime -= Time.deltaTime * 1000;
            //Debug.Log($"flashingTime: {m_flashingTime}");
            if (m_flashingTime <= 0)
            {
                m_flashingTime = 0;

                SetHighlight(m_flashColorMin);
            }

            if (HIGHLIGHT_FLASH_FADE)
            {
                var color = Color.Lerp(m_flashColorMin, m_flashColorMax, Mathf.Clamp01((float)m_flashingTime / HIGHLIGHT_FLASH_TIME));
                SetHighlight(color);
            }
        }
    }

    public enum AnimatorType
    {
        idle,
        walk
    }
    public Animator animatorEnemy;

    public void SetEnemyAction(AnimatorType type)
    {
        if (animatorEnemy == null) return;
        if (type == AnimatorType.walk)
        {
            if (animatorEnemy.HasAnimParameter(WALK))
            {
                animatorEnemy.SetTrigger(WALK);
            }
            if (animatorEnemy.HasAnimParameter(MOVE))
            {
                animatorEnemy.SetFloat(MOVE, 1f);
            }
        }
        else if (type == AnimatorType.idle)
        {
            if (animatorEnemy.HasAnimParameter(REST))
            {
                animatorEnemy.SetTrigger(REST);
            }
            if (animatorEnemy.HasAnimParameter(MOVE))
            {
                animatorEnemy.SetFloat(MOVE, 0);
            }
        }
    }

    public void OnMasterHurted()
    {
        m_flashingTime = HIGHLIGHT_FLASH_TIME;
        SetHighlight(m_flashColorMax);
    }

    public void UpdateCountDown()
    {
        if (IsDead)
        {
            // clear buff
            dicBuffCountDown.Clear();
            return;
        }

        List<BuffTypeEnum> removeList = null;
        for (int i = 0; i < dicBuffCountDown.Count; i++)
        {
            var item = dicBuffCountDown.ElementAt(i);
            var cd = item.Value - 1;
            if (cd <= 0)
            {
                if (removeList == null)
                {
                    removeList = new List<BuffTypeEnum>();
                }
                removeList.Add(item.Key);
            }
            dicBuffCountDown[item.Key] = cd;
        }

        if (removeList != null)
        {
            if (removeList.Count > 0)
            {
                foreach (var item in removeList)
                {
                    if (dicBuffCountDown.ContainsKey(item))
                    {
                        dicBuffCountDown.Remove(item);
                        if (item == BuffTypeEnum.Freeze)
                        {
                            Freeze(false);
                        }
                    }
                }
            }

            removeList.Clear();
        }
    }

    public void SetBuffEffect(BuffTypeEnum buffType, int countDown)
    {
        if (dicBuffCountDown.ContainsKey(buffType))
        {
            dicBuffCountDown[buffType] += countDown;
        }
        else
        {
            dicBuffCountDown.Add(buffType, countDown);
        }

        if (buffType == BuffTypeEnum.Freeze)
        {
            Freeze(true);
        }
    }

    public bool IsBuffExist(BuffTypeEnum buffType)
    {
        return dicBuffCountDown.ContainsKey(buffType);
    }

    private void Freeze(bool isFreeze)
    {
        if (animatorEnemy == null) return;
        if (animManager == null) return;
        // 冰冻buff
        animManager.IsLogicStop = isFreeze;
        animManager.SetFreeze(isFreeze);
        moveCtrl.CanbeMove = !isFreeze;
        var sprRender = animatorEnemy.GetComponent<SpriteRenderer>();
        if (sprRender != null)
        {
            sprRender.material = isFreeze ?
                GameManager.Instance.defaultMaterial :
                GameManager.Instance.enemyMaterial;
            if (isFreeze)
            {
                sprRender.material.SetColor("_TintColor", new Color(92, 116, 255));
            }
        }
    }

    protected void SetHighlight(Color color)
    {
        if (actorMaskMaterial == null)
        {
            return;
        }

        actorMaskMaterial.SetColor("_MaskColor", color);
    }

    private Material AddShader(Renderer renderer, string name)
    {
        if (renderer == null)
        {
            return null;
        }

        var shader = Shader.Find(name);
        if (shader == null || renderer.materials == null)
        {
            return null;
        }

        var material = new Material(shader)
        {
            renderQueue = 2451
        };
        if (material != null)
        {
            Material[] newMaterials = new Material[renderer.materials.Length + 1];
            for (int i = 0; i < renderer.materials.Length; i++)
            {
                newMaterials[i] = renderer.materials[i];
            }

            newMaterials[renderer.materials.Length] = material;
            renderer.materials = newMaterials;
        }

        return material;
    }
}
