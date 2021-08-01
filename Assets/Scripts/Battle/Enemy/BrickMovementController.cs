using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BrickMovementController : MonoBehaviour
{
    public enum BrickState
    {
        stop,
        move,
        wait,// 前排目标不能移动时
    }

    public BrickState currentState;
    private bool hasMoved;
    private EnemyController enemyCtrl;
    private bool is3D = true;

    [HideInInspector]
    public bool CanbeMove;

    void Start()
    {
        hasMoved = false;
        CanbeMove = true;
        currentState = BrickState.stop;
        enemyCtrl = GetComponent<EnemyController>();
    }

    private void OnEnable()
    {
        hasMoved = false;
        CanbeMove = true;
        currentState = BrickState.stop;
    }

    void Update()
    {
        if (currentState == BrickState.stop)
        {
            hasMoved = false;
        }
        if (currentState == BrickState.move)
        {
            if (!hasMoved && CanbeMove)
            {
                if (enemyCtrl != null)
                {
                    enemyCtrl.SetEnemyAction(EnemyController.AnimatorType.walk);
                }

                var movePosition = new Vector3(transform.position.x, transform.position.y - GameSetting.GridBounds.y);
                if (is3D)
                {
                    movePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - GameSetting.GridBounds.z);
                }

                transform.DOMove(movePosition, 1f).OnComplete(() =>
                {
                    if (enemyCtrl != null)
                    {
                        enemyCtrl.SetEnemyAction(EnemyController.AnimatorType.idle);
                    }

                    print("walked.");
                });

                currentState = BrickState.stop;
                hasMoved = true;
            }
        }
    }

    IEnumerator MoveStop(float time)
    {
        yield return new WaitForSeconds(time);
        enemyCtrl.SetEnemyAction(EnemyController.AnimatorType.idle);
    }

    public bool FrontProbe()
    {
        CanbeMove = true;
        currentState = BrickState.stop;
        var distance = BallController.intervalDistance * 5;
        var add = new Vector3(0, -distance, 0);

        var hitObjects = new RaycastHit2D[5];
        var hitNumber = Physics2D.LinecastNonAlloc(this.transform.position, this.transform.position + add, hitObjects, 1 << LayerMask.NameToLayer(GameSetting.LAYERBATTLEOBJS));
        if (hitNumber > 0)
        {
            foreach (var hitObj in hitObjects)
            {
                if (hitObj.transform.CompareTag(GameSetting.TAGBRICKSQUARE))
                {
                    if (hitObj.transform.GetComponent<EnemyController>().IsBuffExist(FLH.Core.Enums.BuffTypeEnum.Freeze))
                    {
                        print("FrontProbe.name: " + hitObj.transform.name);
                        CanbeMove = false;
                        currentState = BrickState.wait;
                        return false;
                    }
                }
            }
        }

        return true;
    }
}
