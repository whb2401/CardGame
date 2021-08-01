using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using FLH.Battle.Ability;

public class BallStop : MonoBehaviour
{
    public enum StopMode
    {
        fade,
        focus
    }

    public StopMode currentMode;

    public Rigidbody ball3;
    public Rigidbody2D ball;
    public BallController ballCtrl;

    private Transform transFirstBall;

    // Use this for initialization
    void Start()
    {
        currentMode = StopMode.focus;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(GameSetting.TAGBALL))
        {
            if (ballCtrl.currentBallState != BallController.BallState.fire)
            {
                return;
            }

            // stop the ball
            if (ball3 != null)
            {
                ball3.velocity = Vector3.zero;
            }
            // reset the level
            // set the ball as active
            if (ballCtrl.currentBallState == BallController.BallState.born)
            {
                ballCtrl.currentBallState = BallController.BallState.aim;
            }
            else
            {
                ballCtrl.OnTheGround();
                ballCtrl.currentBallState = BallController.BallState.wait;

                if (ballCtrl.StopCounter == 0)
                {
                    // first ball
                    ballCtrl.StopPoint = other.transform.position;
                }
                else
                {
                    Roll2FirstBall(other.transform, false);
                }
                ballCtrl.StopCounter++;
            }
        }

        if (other.gameObject.CompareTag(GameSetting.TAGEXTRABALL))
        {
            var extraBallCtrl = other.gameObject.GetComponent<ExtraBallController>();
            if (extraBallCtrl == null)
            {
                return;
            }

            if (extraBallCtrl.currentBallState != ExtraBallController.BallState.fire)
            {
                return;
            }

            var rgbExBall = other.gameObject.GetComponent<Rigidbody>();
            if (rgbExBall == null)
            {
                return;
            }
            rgbExBall.velocity = Vector3.zero;

            extraBallCtrl.OnTheGround();
            extraBallCtrl.currentBallState = ExtraBallController.BallState.hold;

            if (ballCtrl.StopCounter == 0)
            {
                // first ball
                ballCtrl.StopPoint = other.transform.position;
                transFirstBall = other.transform;
                other.gameObject.SetVisibility(true);

                if (GameManager.Instance.ballsInScene.Contains(other.gameObject))
                {
                    GameManager.Instance.ballsInScene.Remove(other.gameObject);
                }

                ballCtrl.StopCounter++;
                return;
            }

            if (currentMode != StopMode.focus)
            {
                other.gameObject.transform.position = Vector3.zero;
                other.gameObject.SetVisibility(false);
            }
            else
            {
                Roll2FirstBall(other.transform, true);
            }
            ballCtrl.StopCounter++;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    { }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(GameSetting.TAGBALL))
        {
            if (ballCtrl.currentBallState != BallController.BallState.fire)
            {
                return;
            }

            // stop the ball
            if (ball != null)
            {
                ball.velocity = Vector2.zero;
            }
            // reset the level
            // set the ball as active
            if (ballCtrl.currentBallState == BallController.BallState.born)
            {
                ballCtrl.currentBallState = BallController.BallState.aim;
            }
            else
            {
                ballCtrl.OnTheGround();
                ballCtrl.currentBallState = BallController.BallState.wait;

                if (ballCtrl.StopCounter == 0)
                {
                    // first ball
                    ballCtrl.StopPoint = other.transform.position;
                }
                else
                {
                    Roll2FirstBall(other.transform, false);
                }
                ballCtrl.StopCounter++;
            }
        }

        if (other.gameObject.CompareTag(GameSetting.TAGEXTRABALL))
        {
            var extraBallCtrl = other.gameObject.GetComponent<ExtraBallController>();
            if (extraBallCtrl == null)
            {
                return;
            }

            if (extraBallCtrl.currentBallState != ExtraBallController.BallState.fire)
            {
                return;
            }

            var rgbExBall = other.gameObject.GetComponent<Rigidbody2D>();
            if (rgbExBall == null)
            {
                return;
            }
            rgbExBall.velocity = Vector2.zero;

            extraBallCtrl.OnTheGround();
            extraBallCtrl.currentBallState = ExtraBallController.BallState.hold;

            if (ballCtrl.StopCounter == 0)
            {
                // first ball
                ballCtrl.StopPoint = other.transform.position;
                transFirstBall = other.transform;
                other.gameObject.SetVisibility(true);

                if (GameManager.Instance.ballsInScene.Contains(other.gameObject))
                {
                    GameManager.Instance.ballsInScene.Remove(other.gameObject);
                }

                ballCtrl.StopCounter++;
                return;
            }

            if (currentMode != StopMode.focus)
            {
                other.gameObject.transform.position = Vector3.zero;
                other.gameObject.SetVisibility(false);
            }
            else
            {
                Roll2FirstBall(other.transform, true);
            }
            ballCtrl.StopCounter++;
        }
    }

    private void Roll2FirstBall(Transform ball, bool reachPointInvisible)
    {
        if (ball == null)
        {
            Debug.LogWarning("Roll2FirstBall>ball is null.");
            return;
        }
        if (currentMode == StopMode.focus)
        {
            var stopPoint = ballCtrl.StopPoint;
            ball.DOMove(new Vector3(stopPoint.x, 0.6f, stopPoint.z), 0.3f).OnComplete(() =>
            {
                if (reachPointInvisible)
                {
                    ball.gameObject.SetVisibility(false);
                    ball.position = Vector3.zero;

                    print(ball.name);
                    if (GameManager.Instance.ballsInScene.Contains(ball.gameObject))
                    {
                        print($"{GameManager.Instance.ballsInScene.Count},{ball.name}");
                        GameManager.Instance.ballsInScene.Remove(ball.gameObject);
                    }
                }

                if (GameManager.Instance.ballsInScene.Count <= 1)
                {
                    if (transFirstBall != null)
                    {
                        GameManager.Instance.UpdateHitCount(true, 0);

                        print($"last hide. {transFirstBall.name}");
                        transFirstBall.position = Vector3.zero;
                        transFirstBall.gameObject.SetVisibility(false);
                    }

                    AbilityManager.EndStop();
                }
            });
        }
    }

    public void RecycleBalls()
    {
        if (ballCtrl.StopCounter <= 0)
        {
            return;
        }

        foreach (var item in GameManager.Instance.ballsInScene)
        {
            if (item.CompareTag(GameSetting.TAGBALL))
            {
                if (ballCtrl.currentBallState == BallController.BallState.wait)
                {
                    continue;
                }

                Roll2FirstBall(item.transform, false);
            }
            else
            {
                var extraBallCtrl = item.GetComponent<ExtraBallController>();
                if (extraBallCtrl == null)
                {
                    continue;
                }

                if (extraBallCtrl.currentBallState == ExtraBallController.BallState.hold)
                {
                    continue;
                }

                extraBallCtrl.currentBallState = ExtraBallController.BallState.hold;

                var rgbExBall = item.GetComponent<Rigidbody2D>();
                if (rgbExBall == null)
                {
                    continue;
                }
                rgbExBall.velocity = Vector2.zero;

                Roll2FirstBall(item.transform, true);
            }
        }
    }
}
