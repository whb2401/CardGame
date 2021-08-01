using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtraBallManager : MonoBehaviour
{
    private BallController ballController;
    public float ballWaitTime;
    private float ballWaitTimeSeconds, ballWaitFirstTimeSeconds;
    public int numberOfExtraBalls;
    public int numberOfBallsToFire;
    public ObjectPool objectPool;
    public Text numberOfBallsText;

    [HideInInspector]
    public Dictionary<string, int> specialBalls;// 专武特殊弹

    void Start()
    {
        ballController = FindObjectOfType<BallController>();
        ballWaitFirstTimeSeconds = ballWaitTimeSeconds = ballWaitTime;
        numberOfExtraBalls = 5;
        numberOfBallsToFire = 5;
        numberOfBallsText.text = "" + 1;
        specialBalls = new Dictionary<string, int>();
    }

    void FixedUpdate()
    {
        if (ballController.currentBallState == BallController.BallState.fire || ballController.currentBallState == BallController.BallState.wait)
        {
            ballWaitFirstTimeSeconds -= Time.fixedDeltaTime;
            if (ballWaitFirstTimeSeconds > 0)
            {
                Debug.Log("MainBall Interval Time: " + ballWaitFirstTimeSeconds);
                return;
            }

            if (numberOfBallsToFire > 0)
            {
                ballWaitTimeSeconds -= Time.fixedDeltaTime;
                Debug.Log("ExtraBall[" + numberOfBallsToFire + "] Interval Time: " + ballWaitTimeSeconds);
                if (ballWaitTimeSeconds <= 0)
                {
                    var ball = objectPool.GetPooledObject(GameSetting.TAGEXTRABALL);
                    if (ball != null)
                    {
                        ballWaitTimeSeconds = ballWaitTime;
                        numberOfBallsToFire--;

                        ball.transform.position = ballController.ballLaunchPosition;
                        ball.SetActive(true);
                        GameManager.Instance.ballsInScene.Add(ball);
                        var r2d = ball.GetComponent<Rigidbody2D>();
                        if (r2d == null)
                        {
                            var velocity = ballController.constantSpeed * ballController.tempVelocity3;
                            ball.GetComponent<Rigidbody>().velocity = velocity;
                            ball.GetComponent<ExtraBallController>().CurrentVelocity3 = velocity;
                        }
                        else
                        {
                            var velocity = ballController.constantSpeed * ballController.tempVelocity;
                            ball.GetComponent<Rigidbody2D>().velocity = velocity;
                            ball.GetComponent<ExtraBallController>().CurrentVelocity = velocity;
                        }
                    }

                    ballWaitTimeSeconds = ballWaitTime;
                }
            }
        }
        else
        {
            ballWaitFirstTimeSeconds = ballWaitTime / 2;
        }
    }

    void Update()
    {
        numberOfBallsText.text = "" + (numberOfExtraBalls + 1);

        if (ballController.currentBallState == BallController.BallState.endShot)
        {
            numberOfBallsToFire = numberOfExtraBalls;
        }
    }

    public void Reset()
    {
        enabled = true;
        numberOfBallsToFire = numberOfExtraBalls;
    }

    public void LoadedBullet(string bulletName)
    {
        foreach (var bullets in specialBalls)
        {
            if (bullets.Key == bulletName)
            {
                numberOfExtraBalls += bullets.Value;
                numberOfBallsToFire += bullets.Value;
            }
        }
    }
}
