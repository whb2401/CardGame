using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using FLH.Battle.Ability;
using FLH.Battle.Player;

public class BallController : BaseMonoBehaviour<BallController>
{
    public EmissionRayCtrl emissionRayCtrl;
    public EmissionRayCrossCtrl emissionRayCrossCtrl;

    public GameObject effectHangPoint;

    public GameObject objReferenceCamera;
    public GameObject objReferenceGround;

    public enum BallState
    {
        born,
        aim,
        fire,
        wait,
        endFire,
        endShot,
        endGame,
        gap
    }

    public BallState currentBallState;

    public Camera camMain;
    public Camera camOverlook;

    public Rigidbody ball3D;
    public Rigidbody2D ball;
    private Vector3 mouseStartPosition3;
    private Vector2 mouseStartPosition;
    private Vector2 mouseEndPosition;
    public Vector2 tempVelocity;
    public Vector3 tempVelocity3;
    public Vector3 ballLaunchPosition;
    private float ballVelocityX, ballVelocityY;
    public float constantSpeed;

    public GameObject arrow;
    public HeroController heroCtrl;

    public GameObject crossPoint, cpArrow;
    RaycastHit2D[] rays;
    RaycastHit2D hit;
    int hitNumber = 0;// 检测返回与直线相交的物体数量
    private bool inCancelArea;
    private bool haveGravity;
    private MeshRenderer mrMain;

    // 发射出的子弹是否使用重力
    public bool UseShootGravity { get; set; } = false;

    /// <summary>
    /// 累计击空次数（单个回合）
    /// </summary>
    public int HitEmptyCount { get; set; }

    private LineRenderer lineTrackIndicate;

    // 指向线路
    public Transform transIndicator;
    // 弹射方向导航
    public Transform transBallNavigator;

    // 停止处理
    [SerializeField]
    public int StopCounter { get; set; } = 0;
    [SerializeField]
    public Vector3 StopPoint { get; set; } = Vector3.zero;

    private bool isDragMoved;

    [SerializeField]
    private float offsetRotation = 0f;
    private float threshold = 0.1f;
    public Rect ScreenRect { get; set; }

    [SerializeField]
    private float numberOfDots = 10;
    [SerializeField]
    private GameObject dotPrefab;
    private List<Transform> trajectoryDots;

    // 防卡
    private float interval;
    private float latestRecordHeight;
    private bool ballStuck;
    private Vector3 baseOffsetDirection = Vector3.down * 0.1f;
    private Vector3 baseOffsetDirection3 = Vector3.back * 0.1f;

    // 物理
    public Vector2 CurrentVelocity;
    public Vector3 CurrentVelocity3;

    private int RoundEnd;

    private bool is3D;
    private float height;

    void Start()
    {
        base.OnStart();

        is3D = ball3D != null;

        interval = 0f;
        latestRecordHeight = 0f;
        ballStuck = false;

        var globalScale = GameManager.Instance.Scale;
        var currentScale = transform.localScale;
        var zScale = is3D ? currentScale.z * globalScale : 1f;
        transform.localScale = new Vector3(currentScale.x * globalScale, currentScale.y * globalScale, zScale);
        GameManager.Instance.MainBallScale = transform.localScale;

        lineTrackIndicate = transform.parent.GetComponentInChildren<LineRenderer>();
        lineTrackIndicate.enabled = false;
        currentBallState = BallState.aim;
        GameManager.Instance.ballsInScene.Add(this.gameObject);
        //SetUpTrajectoryDots();

        mrMain = transform.GetComponent<MeshRenderer>();
        if (mrMain != null)
        {
            height = mrMain.bounds.size.y;
        }

        OnTheGround();
        AbilityManager.Init(this);
    }

    private void OnDestroy()
    {
        base.Destroy();
    }

    protected override void GameStatusChange(bool isPause)
    {
        if (is3D) { }
        else
        {
            if (isPause)
            {
                CurrentVelocity = ball.velocity;
                ball.velocity = Vector2.zero;
            }
            else
            {
                ball.velocity = CurrentVelocity;
            }
        }

        base.GameStatusChange(isPause);
    }

    protected override void BallAccelerateChange(bool isAccelerate)
    {
        if (is3D) { }
        else
        {
            if (isAccelerate)
            {
                ball.velocity *= GameManager.Instance.CurrentAccelerateSeed;
            }
            else
            {
                ball.velocity /= GameManager.Instance.CurrentAccelerateSeed;
            }
        }

        base.BallAccelerateChange(isAccelerate);
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.Status != Enums.GameStatusEnum.Battle)
        {
            return;
        }

        if (GameManager.Instance.paused)
        {
            return;
        }

        AbilityManager.FixedUpdate();

        if (!GameManager.Instance.enableBallStuckFix)
        {
            return;
        }

        if (is3D)
        {
            if (currentBallState == BallState.fire)
            {
                interval += Time.fixedDeltaTime;
                if (interval >= 1f)
                {
                    if (latestRecordHeight == transform.localPosition.z)
                    {
                        Debug.Log("ball stuck.");
                        ballStuck = true;
                    }
                    latestRecordHeight = transform.localPosition.z;
                    interval = 0f;
                }

                if (ballStuck)
                {
                    transform.position += baseOffsetDirection3 * Time.fixedDeltaTime * constantSpeed;
                }
            }
            return;
        }

        if (currentBallState == BallState.fire)
        {
            interval += Time.fixedDeltaTime;
            if (interval >= 1f)
            {
                if (latestRecordHeight == transform.localPosition.y)
                {
                    Debug.Log("ball stuck.");
                    ballStuck = true;
                }
                latestRecordHeight = transform.localPosition.y;
                interval = 0f;
            }

            if (ballStuck)
            {
                transform.position += baseOffsetDirection * Time.fixedDeltaTime * constantSpeed;
            }
        }
    }

    void Update()
    {
        if (GameManager.Instance.Status != Enums.GameStatusEnum.Battle)
        {
            return;
        }

        if (GameManager.Instance.paused)
        {
            return;
        }

        AbilityManager.Update();
        // BallCtrl();
    }

    private void LateUpdate()
    {
        if (GameManager.Instance.Status != Enums.GameStatusEnum.Battle)
        {
            return;
        }

        if (GameManager.Instance.paused)
        {
            return;
        }

        BallCtrl();
    }

    void BallCtrl()
    {
        switch (currentBallState)
        {
            case BallState.aim:
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (!Tools.IsTouchedUI())
                        {
                            MouseClicked();
                        }
                    }

                    // if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
                    if (Input.GetMouseButton(0))
                    {
                        // mouse hold
                        if (!Tools.IsTouchedUI())
                        {
                            MouseDragged();
                        }
                    }

                    if (Input.GetMouseButtonUp(0))
                    {
                        if (!Tools.IsTouchedUI())
                        {
                            RoundEnd = 0;
                            ReleaseMouse();
                        }
                    }
                }
                break;
            case BallState.fire:
                {
                    if (UseShootGravity)
                    {
                        var distance = transform.position - ballLaunchPosition;
                        if (Mathf.Abs(distance.y) >= 0.01f)
                        {
                            ChangeGravity(0.1f);
                        }
                    }

                    AbilityManager.Fire();
                }
                break;
            case BallState.wait:
                {
                    if (UseShootGravity && haveGravity)
                    {
                        ChangeGravity(0);
                    }

                    if (GameManager.Instance.ballsInScene.Count == 1)
                    {
                        transIndicator.position = transform.position;
                        currentBallState = BallState.endShot;
                    }
                }
                break;
            case BallState.endFire:
                {
                    // cancel
                    if (Input.GetMouseButtonUp(0))
                    {
                        ReleaseMouse();
                    }
                }
                break;
            case BallState.endShot:
                {
                    for (int i = 0; i < GameManager.Instance.brickInScene.Count; i++)
                    {
                        var brick = GameManager.Instance.brickInScene[i];
                        var brickMovementCtrl = brick.GetComponent<BrickMovementController>();
                        if (brickMovementCtrl != null)
                        {
                            Debug.Log("brickMovementCtrl.Status: " + brickMovementCtrl.currentState);
                            if (brickMovementCtrl.currentState == BrickMovementController.BrickState.wait)
                            {
                                // 探路
                                brickMovementCtrl.FrontProbe();
                            }
                            if (brickMovementCtrl.currentState != BrickMovementController.BrickState.wait)
                            {
                                brickMovementCtrl.currentState = BrickMovementController.BrickState.move;
                            }
                        }
                    }

                    ballStuck = false;
                    StopCounter = 0;
                    heroCtrl.OnStandBy = false;
                    currentBallState = BallState.aim;
                    StartCoroutine(PlaceNextBricks());

                    RoundEnd++;
                    if (RoundEnd > 1)
                    {
                        return;
                    }

                    foreach (var brick in GameManager.Instance.brickInScene)
                    {
                        var enemyCtrl = brick.GetComponent<EnemyController>();
                        if (enemyCtrl != null)
                        {
                            enemyCtrl.UpdateCountDown();
                        }
                    }

                    AbilityManager.EndShot();
                }
                break;
            case BallState.endGame:
                AbilityManager.EndGame();
                break;
            case BallState.gap:
                // nothing to do
                break;
            default:
                break;
        }
    }

    IEnumerator PlaceNextBricks()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.PlaceBricks();
    }

    public void MouseClicked()
    {
        mouseStartPosition3 = camMain.ScreenToWorldPoint(Input.mousePosition);
        mouseStartPosition3 = camOverlook.ScreenToWorldPoint(Input.mousePosition);
        mouseStartPosition = camMain.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log("mouseStartPosition: " + mouseStartPosition3);

        emissionRayCtrl.FireBegin();
        emissionRayCrossCtrl.Begin();

        //SetTrajectoryDotsVisibility(true);
    }

    public void MouseDragged()
    {
        if (is3D)
        {
            var currentMousePosition = camMain.ScreenToWorldPoint(Input.mousePosition);
            var movement3 = mouseStartPosition3 - currentMousePosition;

            //currentMousePosition = camMain.ScreenToWorldPoint(Input.mousePosition);
            objReferenceCamera.transform.position = currentMousePosition;
            var direction = Quaternion.Euler(camMain.transform.eulerAngles.x, 0, 0) * objReferenceCamera.transform.TransformDirection(Vector3.forward) * 100;
            Debug.DrawRay(currentMousePosition, direction, Color.blue);
            Physics.Raycast(currentMousePosition, direction, out RaycastHit pointHitInfo, 100.0f, 1 << LayerMask.NameToLayer(GameSetting.LAYERFLOOR));
            if (pointHitInfo.collider != null)
            {
                Debug.DrawLine(currentMousePosition, pointHitInfo.point, Color.yellow);
                transform.LookAt(new Vector3(pointHitInfo.point.x, 0.6f, pointHitInfo.point.z));
                movement3 = transform.TransformDirection(Vector3.forward) * 100;
                Debug.DrawRay(transform.position, movement3, Color.green);
            }

            if (movement3 == Vector3.zero)
            {
                return;
            }
            arrow.SetVisibility(true);

            isDragMoved = true;
            //print($"movement3: {movement3}");
            if (!IsInShootArea3(movement3))
            {
                inCancelArea = true;
                arrow.SetVisibility(false);
                currentBallState = BallState.endFire;
                emissionRayCtrl.FireShut();
                emissionRayCrossCtrl.ShutDown();
            }

            //SetTrajectoryPointsFixedXZ(BallMainPosition, movement3);

            Physics.Raycast(BallMainPosition, new Vector3(movement3.x, 0, movement3.z), out RaycastHit hitInfo, 100.0f, 1 << LayerMask.NameToLayer(GameSetting.LAYERBOUNDARY));
            if (hitInfo.collider != null)
            {
                mouseUpPosition = hitInfo.point;
                Debug.DrawLine(BallMainPosition, hitInfo.point, Color.red);

                crossPoint.transform.position = new Vector3(hitInfo.point.x, transform.localPosition.y, hitInfo.point.z);
                crossPoint.SetActive(!inCancelArea);

                float theta = Mathf.Rad2Deg * Mathf.Atan(movement3.x / movement3.z);
                // + offset fixed
                if (crossPoint.transform.position.z >= 7.05f)
                {
                    // on the top
                    cpArrow.transform.rotation = Quaternion.Euler(-180f, -theta, -180f);
                }
                else
                {
                    cpArrow.transform.rotation = Quaternion.Euler(0f, -theta, 0f);
                }
            }
            return;
        }

        // move the arrow
        Vector2 tempMousePosition = camMain.ScreenToWorldPoint(Input.mousePosition);
        var movement = mouseStartPosition - tempMousePosition;
        if (movement == Vector2.zero)
        {
            return;
        }
        arrow.SetVisibility(true);

        isDragMoved = true;
        if (!IsInShootArea(movement))
        {
            inCancelArea = true;
            arrow.SetVisibility(false);
            currentBallState = BallState.endFire;
        }

        SetTrajectoryPointsFixed(BallMainPosition, movement);

        hit = Physics2D.Raycast(BallMainPosition, movement, 15.0f, 1 << LayerMask.NameToLayer(GameSetting.LAYERBOUNDARY));
        if (hit.collider != null)
        {
            DrawDebugTrackRayLine(false);

            crossPoint.transform.position = hit.point;
            crossPoint.SetActive(!inCancelArea);

            float theta = Mathf.Rad2Deg * Mathf.Atan(movement.x / movement.y);
            // + offset fixed
            if (crossPoint.transform.position.y >= ScreenRect.height / 2 - 0.001f)
            {
                // on the top
                cpArrow.transform.rotation = Quaternion.Euler(-180f, -180f, theta);
            }
            else
            {
                cpArrow.transform.rotation = Quaternion.Euler(0f, 0f, theta);
            }
        }
    }

    /// <summary>
    /// 是否显示辅助线
    /// </summary>
    /// <param name="draw">true：显示（只在editor中）</param>
    private void DrawDebugTrackRayLine(bool draw)
    {
        if (!draw) { return; }
#if UNITY_EDITOR
        Debug.DrawLine(BallMainPosition, hit.point);

        lineTrackIndicate.SetPosition(0, BallMainPosition);
        lineTrackIndicate.SetPosition(1, hit.point);
        lineTrackIndicate.enabled = !inCancelArea;
#endif
    }

    Vector3 mouseUpPosition;
    public void ReleaseMouse()
    {
        lineTrackIndicate.enabled = false;
        arrow.SetVisibility(false);
        crossPoint.SetVisibility(false);
        //SetTrajectoryDotsVisibility(false);
        emissionRayCtrl.FireShut();
        emissionRayCrossCtrl.ShutDown();

        if (!isDragMoved)
        {
            return;
        }
        isDragMoved = false;

        if (currentBallState == BallState.endFire)
        {
            inCancelArea = false;
            currentBallState = BallState.aim;
            return;
        }

        heroCtrl.ShootIt();
        StopCounter = 0;

        if (is3D)
        {
            ballVelocityX = mouseUpPosition.x - transform.position.x;
            var ballVelocityZ = mouseUpPosition.z - transform.position.z;
            tempVelocity3 = new Vector3(ballVelocityX, 0.6f, ballVelocityZ).normalized;
            CurrentVelocity3 = constantSpeed * tempVelocity3;
            ball3D.velocity = CurrentVelocity3;
            if (ball3D.velocity == Vector3.zero)
            {
                return;
            }
        }
        else
        {
            mouseEndPosition = camMain.ScreenToWorldPoint(Input.mousePosition);
            ballVelocityX = mouseStartPosition.x - mouseEndPosition.x;
            ballVelocityY = mouseStartPosition.y - mouseEndPosition.y;
            tempVelocity = new Vector2(ballVelocityX, ballVelocityY).normalized;
            CurrentVelocity = ball.velocity = constantSpeed * tempVelocity;
            if (ball.velocity == Vector2.zero)
            {
                return;
            }
        }

        ballLaunchPosition = transform.position;
        currentBallState = BallState.fire;

        heroCtrl.OnStandBy = false;

        if (hit.collider != null)
        {
            Debug.Log("hit point: " + hit.point + ", collider name: " + hit.collider.gameObject.name);
        }

        AbilityManager.Release();
    }

    void HitTest(Vector2 targetPos)
    {
        rays = new RaycastHit2D[5];
        hitNumber = Physics2D.LinecastNonAlloc(gameObject.transform.position, targetPos, rays, 1 << LayerMask.NameToLayer(GameSetting.LAYERBOUNDARY));
        Debug.Log("hitNumber: " + hitNumber);
        for (int i = 0; i < hitNumber; i++)
        {
            Debug.DrawLine(gameObject.transform.position, rays[i].transform.position, Color.red);
            print("ray[" + i + "].name " + rays[i].transform.name);
        }
    }

    public void OnTheGround()
    {
        if (is3D)
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
            Debug.Log("hit point: " + hit.point + ", collider name: " + hit.collider.gameObject.name);
            transform.position = hit.point;
        }
    }

    private void ChangeGravity(float gravity)
    {
        haveGravity = gravity > 0;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = gravity;
    }

    private void OnCollisionStay2D(Collision2D other)
    { }

    public Vector3 BallMainPosition
    {
        get
        {
            if (arrow != null)
            {
                return arrow.transform.position;
            }

            // 重心上移到中心点
            return transform.position + new Vector3(0, 0.07f, 0);
        }
    }

    bool IsInShootArea3(Vector3 movement)
    {
        //print($"angle:{Vector3.Angle(movement, Vector3.forward)}");
        if (Vector3.Angle(movement, Vector3.forward) > 75 - offsetRotation)
        {
            return false;
        }

        var leftBottom = new Vector3(ScreenRect.xMin, transform.position.z, 0);

        if (Vector3.Distance(transform.position, leftBottom) < transform.localScale.x + threshold && Vector3.Dot(movement, Vector3.right) < 0)
        {
            return false;
        }

        var rightBottom = new Vector3(ScreenRect.xMax, transform.position.z, 0);

        if (Vector3.Distance(transform.position, rightBottom) < transform.localScale.x + threshold && Vector3.Dot(movement, Vector3.right) > 0)
        {
            return false;
        }

        return true;
    }

    bool IsInShootArea(Vector3 movement)
    {
        if (Vector3.Angle(movement, Vector3.up) > 90 - offsetRotation)
        {
            return false;
        }

        var leftBottom = new Vector3(ScreenRect.xMin, transform.position.y, 0);

        if (Vector3.Distance(transform.position, leftBottom) < transform.localScale.x + threshold && Vector3.Dot(movement, Vector3.right) < 0)
        {
            return false;
        }

        var rightBottom = new Vector3(ScreenRect.xMax, transform.position.y, 0);

        if (Vector3.Distance(transform.position, rightBottom) < transform.localScale.x + threshold && Vector3.Dot(movement, Vector3.right) > 0)
        {
            return false;
        }

        return true;
    }

    public void SetUpTrajectoryDots()
    {
        trajectoryDots = new List<Transform>();
        for (int i = 0; i < numberOfDots; i++)
        {
            var dot = Instantiate(dotPrefab);
            dot.transform.parent = arrow.transform;
            dot.transform.position = Vector3.zero;
            dot.SetActive(false);
            if (is3D)
            {
                dot.transform.position += new Vector3(0, 0, 0);
                dot.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            }
            trajectoryDots.Add(dot.transform);
        }
    }

    void SetTrajectoryDotsVisibility(bool visible)
    {
        foreach (Transform dot in trajectoryDots)
        {
            dot.gameObject.SetActive(visible);
        }
    }

    void SetTrajectoryPointsFixed(Vector3 posStart, Vector2 direction)
    {
        if (direction.x <= 0 && direction.y <= 0)
        {
            return;
        }

        float angle = Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x);
        float spacing = 0;

        spacing += .2f;
        foreach (Transform dot in trajectoryDots)
        {
            float dx = spacing * Mathf.Cos(angle * Mathf.Deg2Rad);
            float dy = spacing * Mathf.Sin(angle * Mathf.Deg2Rad);
            var pos = new Vector3(posStart.x + dx, posStart.y + dy, 0);
            dot.position = pos;
            dot.gameObject.SetActive(true);
            dot.eulerAngles = new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x));
            spacing += .2f;
        }
    }

    void SetTrajectoryPointsFixedXZ(Vector3 posStart, Vector3 direction)
    {
        if (direction.x <= 0 && direction.z <= 0)
        {
            return;
        }

        float angle = Mathf.Rad2Deg * Mathf.Atan2(direction.z, direction.x);
        float spacing = 0;

        spacing += .6f;
        foreach (Transform dot in trajectoryDots)
        {
            float dx = spacing * Mathf.Cos(angle * Mathf.Deg2Rad);
            float dz = spacing * Mathf.Sin(angle * Mathf.Deg2Rad);
            var y = 0.6f;//height / 2;
            dot.position = new Vector3(posStart.x + dx, y, posStart.z + dz);
            dot.gameObject.SetActive(true);
            dot.eulerAngles = new Vector3(0, Mathf.Atan2(direction.z, direction.x), 0);
            spacing += .6f;
        }
    }

    void SetTrajectoryPoints(Vector3 posStart, Vector2 direction)
    {
        float velocity = Mathf.Sqrt((direction.x * direction.x) + (direction.y * direction.y));
        float angle = Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x);
        float fTime = 0;

        fTime += 0.1f;
        foreach (Transform dot in trajectoryDots)
        {
            float dx = velocity * fTime * Mathf.Cos(angle * Mathf.Deg2Rad);
            float dy = velocity * fTime * Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector3 pos = new Vector3(posStart.x + dx, posStart.y + dy, 0);
            dot.position = pos;
            dot.gameObject.SetActive(true);
            dot.eulerAngles = new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x));
            fTime += 0.1f;
        }
    }

    public void HorizontalReturn2Zero()
    {
        var oriPos = transform.position;
        transform.position = new Vector3(0, oriPos.y, oriPos.z);
        //transform.localScale = new Vector3(.2f, .2f, .2f);
        transIndicator.position = transform.position;
    }

    Dictionary<int, List<Transform>> dicAttackEnemies;
    // Debug
    void HitEnemy()
    {
#if UNITY_EDITOR

        dicAttackEnemies = new Dictionary<int, List<Transform>>();
        HitEnemy(2, Vector2.left);
        HitEnemy(2, Vector2.right);

        var rangeEnemies = new List<Transform>();
        if (dicAttackEnemies.ContainsKey(0))
        {
            rangeEnemies.AddRange(dicAttackEnemies[0]);
        }

        var leftCount = 0;
        if (dicAttackEnemies.ContainsKey(1))
        {
            leftCount = dicAttackEnemies[1].Count();
        }
        var rightCount = 0;
        if (dicAttackEnemies.ContainsKey(2))
        {
            rightCount = dicAttackEnemies[2].Count();
        }

        if (leftCount > 0 && rightCount > 0)
        {
            if (leftCount > rightCount)
            {
                rangeEnemies.AddRange(dicAttackEnemies[1]);
            }
            else
            {
                rangeEnemies.AddRange(dicAttackEnemies[2]);
            }
        }
        else
        {
            if (leftCount > 0 && rightCount <= 0)
            {
                rangeEnemies.AddRange(dicAttackEnemies[1]);
            }
            if (leftCount <= 0 && rightCount > 0)
            {
                rangeEnemies.AddRange(dicAttackEnemies[2]);
            }
        }

        foreach (var enemy in rangeEnemies)
        {
            print("rangeEnemies.name: " + enemy.name);
        }

#endif
    }

    void HitEnemy(int range, Vector2 direction, int siblingNum = 1)
    {
#if UNITY_EDITOR

        if (dicAttackEnemies == null)
        {
            dicAttackEnemies = new Dictionary<int, List<Transform>>();
        }

        var hit = Physics2D.Raycast(camMain.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            var attackEnemies = GetRangeEnemies(hit.transform, Vector2.up, direction);

            var key = direction == Vector2.up ? 0 : (direction == Vector2.left ? 1 : 2);
            if (dicAttackEnemies.ContainsKey(key))
            {
                dicAttackEnemies[key].AddRange(attackEnemies);
                return;
            }
            dicAttackEnemies.Add(key, attackEnemies);
        }

#endif
    }

    public const float intervalDistance = 0.5f;
    public List<Transform> GetRangeEnemies(Transform target, Vector2 direction, Vector2 dirPoint, int range = 4)
    {
        Debug.Log("Target: " + target.name);

        var attackEnemies = new List<Transform>();
        var hitObjects = new RaycastHit2D[range];
        var farthestDistance = intervalDistance * range;
        var add = new Vector3(0, farthestDistance, 0);
        if (direction != Vector2.up)
        {
            add = new Vector3(direction == Vector2.left ? -farthestDistance : farthestDistance, 0, 0);
        }

        for (int i = 0; i < range; i++)
        {

            var nextPoint = new Vector3(i * (dirPoint == Vector2.left ? -intervalDistance : intervalDistance), 0, 0);
            if (dirPoint == Vector2.up || dirPoint == Vector2.down)
            {
                nextPoint = Vector2.zero;
            }
            var currentPos = target.position + nextPoint;

            var hitNumber = Physics2D.LinecastNonAlloc(currentPos, currentPos + add, hitObjects, 1 << LayerMask.NameToLayer(GameSetting.LAYERBATTLEOBJS));
            Debug.Log("hitNumber: " + hitNumber);
            if (hitNumber > 0)
            {
                for (int j = 0; j < hitNumber; j++)
                {
                    var hitObj = hitObjects[j];
                    if (hitObj.transform.CompareTag(GameSetting.TAGBRICKSQUARE))
                    {
                        Debug.DrawLine(hit.point, hitObj.transform.position, Color.red);
                        print("hitObjects[" + j + "].name: " + hitObj.transform.name);

                        var distancePos = hitObj.transform.position - currentPos;
                        var distance = Mathf.Abs(distancePos.y);
                        if (direction != Vector2.up)
                        {
                            distance = Mathf.Abs(distancePos.x);
                        }

                        if (distance < farthestDistance)
                        {
                            attackEnemies.Add(hitObj.transform);
                        }
                    }
                }
            }

        }

        return attackEnemies;
    }

    public void AppendEffect(string name, GameObject objEffect, AbilityData.EffectReference data)
    {
        if (effectHangPoint == null) return;

        if (effectHangPoint.name == name)
        {
            if (effectHangPoint.transform.childCount > 0)
            {
                Destroy(effectHangPoint.transform.GetChild(0).gameObject);
            }

            var effectObject = Instantiate(objEffect, effectHangPoint.transform);
            effectObject.transform.Reset();

            if (data.scale != Vector3.zero)
            {
                effectObject.transform.localScale = data.scale;
            }
        }
    }
}
