using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraBallController : BaseMonoBehaviour<ExtraBallController>
{
    public enum BallState
    {
        wait,
        hold,
        fire
    }
    public BallState currentBallState;

    SpriteRenderer sprBall;
    public Rigidbody2D rgbSelf;
    [SerializeField]
    public Rigidbody rgbSelf3;

    private BallController ballCtrl;
    private bool haveGravity;
    private float interval;
    private float latestRecordHeight;
    private bool ballStuck;
    private Vector3 baseOffsetDirection = Vector3.down * 0.1f;
    private Vector3 baseOffsetDirection3 = Vector3.back * 0.1f;
    public Vector2 CurrentVelocity { get; set; }
    public Vector3 CurrentVelocity3 { get; set; }

    private Vector3 velocityMonitor;

    private void Awake()
    {
        rgbSelf = GetComponent<Rigidbody2D>();
        rgbSelf3 = GetComponent<Rigidbody>();
    }

    void Start()
    {
        base.OnStart();

        interval = 0f;
        latestRecordHeight = 0f;
        ballStuck = false;
        ballCtrl = FindObjectOfType<BallController>();
        sprBall = GetComponent<SpriteRenderer>();
    }

    private void OnDestroy()
    {
        base.Destroy();
    }

    protected override void GameStatusChange(bool isPause)
    {
        if (rgbSelf != null)
        {
            if (isPause)
            {
                CurrentVelocity = rgbSelf.velocity;
                rgbSelf.velocity = Vector2.zero;
            }
            else
            {
                rgbSelf.velocity = CurrentVelocity;
            }
        }

        if (rgbSelf3 != null)
        {
            if (isPause)
            {
                CurrentVelocity3 = rgbSelf3.velocity;
                rgbSelf3.velocity = Vector2.zero;
            }
            else
            {
                rgbSelf3.velocity = CurrentVelocity3;
            }
        }

        base.GameStatusChange(isPause);
    }

    protected override void BallAccelerateChange(bool isAccelerate)
    {
        if (rgbSelf != null)
        {
            if (isAccelerate)
            {
                rgbSelf.velocity *= GameManager.Instance.CurrentAccelerateSeed;
            }
            else
            {
                rgbSelf.velocity /= GameManager.Instance.CurrentAccelerateSeed;
            }
        }

        if (rgbSelf3 != null)
        {
            if (isAccelerate)
            {
                rgbSelf3.velocity *= GameManager.Instance.CurrentAccelerateSeed;
            }
            else
            {
                rgbSelf3.velocity /= GameManager.Instance.CurrentAccelerateSeed;
            }
        }

        base.BallAccelerateChange(isAccelerate);
    }

    private void FixedUpdate()
    {
        if (rgbSelf3 != null)
        {
            velocityMonitor = rgbSelf3.velocity.normalized;
        }

        if (GameManager.Instance.paused)
        {
            return;
        }

        if (!GameManager.Instance.enableBallStuckFix)
        {
            return;
        }

        if (rgbSelf3 != null)
        {
            interval += Time.fixedDeltaTime;
            if (interval >= 1f)
            {
                if (latestRecordHeight == transform.localPosition.z)
                {
                    Debug.Log(string.Format("ball[{0}] stuck.", transform.name));
                    ballStuck = true;
                }
                latestRecordHeight = transform.localPosition.z;
                interval = 0f;
            }

            if (ballStuck)
            {
                transform.position += baseOffsetDirection3 * Time.fixedDeltaTime * ballCtrl.constantSpeed;
            }

            return;
        }

        interval += Time.fixedDeltaTime;
        if (interval >= 1f)
        {
            if (latestRecordHeight == transform.localPosition.y)
            {
                Debug.Log(string.Format("ball[{0}] stuck.", transform.name));
                ballStuck = true;
            }
            latestRecordHeight = transform.localPosition.y;
            interval = 0f;
        }

        if (ballStuck)
        {
            transform.position += baseOffsetDirection * Time.fixedDeltaTime * ballCtrl.constantSpeed;
        }
    }

    void Update()
    {
        if (GameManager.Instance.paused)
        {
            return;
        }

        if (!gameObject.activeInHierarchy)
        {
            return;
        }

        if (currentBallState == BallState.fire || currentBallState == BallState.hold)
        {
            return;
        }

        if (ballCtrl != null && (ballCtrl.currentBallState == BallController.BallState.fire || ballCtrl.currentBallState == BallController.BallState.wait))
        {
            var distance = transform.position - ballCtrl.ballLaunchPosition;
            if (!haveGravity && distance.y >= 0)
            {
                currentBallState = BallState.fire;

                if (ballCtrl.UseShootGravity)
                {
                    ChangeGravity(0.1f);
                }
            }
        }

        if (ballCtrl.UseShootGravity)
        {
            var distance = ballCtrl.ballLaunchPosition - transform.localPosition;
            if (haveGravity && distance.y > 0.01f)
            {
                ChangeGravity(0);
            }
        }
    }

    private void OnDisable()
    {
        ballStuck = false;
        currentBallState = BallState.wait;
    }

    private void ChangeGravity(float gravity)
    {
        haveGravity = gravity > 0;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = gravity;
    }

    public void OnTheGround()
    {
        if (rgbSelf3 != null)
        {
            Physics.Raycast(transform.position, Vector3.back, out RaycastHit hitInfo, 10.0f, 1 << LayerMask.NameToLayer(GameSetting.LAYERGROUND));
            if (hitInfo.collider != null)
            {
                Debug.Log("hitInfo.collider->" + hitInfo.point);
                var y = 0.6f;//height / 2;
                transform.position = new Vector3(hitInfo.point.x, y, hitInfo.point.z);
                //arrow.transform.position = transform.position;
            }
            return;
        }

        var hit = Physics2D.Raycast(transform.position, Vector2.down, 5.0f, 1 << LayerMask.NameToLayer(GameSetting.LAYERGROUND));
        if (hit.collider != null)
        {
            transform.position = hit.point;
        }
    }

    [ContextMenu("Execute")]
    public void DebugFunction()
    {
        OnTheGround();
    }
}
