using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.ResourceManagement.AsyncOperations;
using RND = UnityEngine.Random;
using Ball.Config;
using Newtonsoft.Json;
using FLH.Core;
using FLH.Core.Enums;
using FLH.Battle.Ability;
using FCR = FLH.Core.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get
        {
            if (instance != null)
                return instance;

            instance = FindObjectOfType<GameManager>();

            if (instance != null)
                return instance;

            Create();

            return instance;
        }
    }

    protected static GameManager instance;

    public static GameManager Create()
    {
        GameObject gameManagerGameObject = new GameObject("GameManager");
        instance = gameManagerGameObject.AddComponent<GameManager>();

        return instance;
    }

    [HideInInspector]
    public int EffectHitId { get; set; }
    public GameObject EffectPondObj { get; set; }

    public GameObject objHero;

    public Transform[] spawnPoints;
    public int level;
    public GameObject brickDestroyParticle;
    public List<GameObject> brickInScene;
    public List<GameObject> ballsInScene;
    public ObjectPool objectPool;
    public int numberOfExtraBallsInRow = 0;
    public BallController ballCtrl;
    public ExtraBallManager exBallManager;
    public bool enableBallStuckFix = true;

    /// <summary>
    /// 游戏暂停
    /// </summary>
    public bool paused = false;
    /// <summary>
    /// 自动加速
    /// </summary>
    public bool accelerateSkip = false;
    public float CurrentAccelerateSeed { get; set; }

    [SerializeField]
    private Vector2 baseScreenSize;
    [SerializeField]
    private Transform background;
    [SerializeField]
    private Transform spawnPointsWrapper;
    [SerializeField]
    private EdgeCollider2D wall;

    private Rect screenRect;
    public float MultipleCoefficient { get; private set; }
    public float Scale { get; private set; }
    public Vector3 MainBallScale { get; set; }

    public BoardManager boardManager;
    public SoundManager soundManager;

    public Enums.LevelTypeEnum levelModel;
    private int currentRowIndex, loadRowCount;
    public int RowsBricksHealth
    {
        get
        {
            if (levelModel == Enums.LevelTypeEnum.classic)
            {
                return currentRowIndex + RND.Range(0, 9);
            }

            return 1;// minimnm
        }
    }

    private UIManager uiManager;
    public List<string> LevelsTest { get; set; }

    public Enums.GameStatusEnum Status { get; set; }
    public static BallSkillEnum PlayerSkill { get; set; }

    /// <summary>
    /// 随机数
    /// </summary>
    public FCR Random { get; set; }

    [Header("Materials")]
    public Material defaultMaterial;
    public Material enemyMaterial;

    private bool is3D = true;

    void Start()
    {
        objectPool = FindObjectOfType<ObjectPool>();
        ballCtrl = FindObjectOfType<BallController>();
        boardManager = GetComponent<BoardManager>();
        uiManager = FindObjectOfType<UIManager>();
        screenRect = CameraTools.GetScreenRect();
        Debug.Log("screenRect: " + screenRect);
        ballCtrl.ScreenRect = screenRect;
        Random = new FCR();
        InitManagers();

        LevelsTest = new List<string>();
        for (var i = 0; i < 10; i++)
        {
            LevelsTest.Add("No" + (i + 1));
        }

        SetBounds();
        SetMap();
        Status = Enums.GameStatusEnum.MainUI;
        if (GameSetting.DebugMode)
        {
            Status = Enums.GameStatusEnum.Battle;
            //EnterLevel(10001);
            //NextLevel();
        }

        PlayerSkill = BallSkillEnum.Accelerate | BallSkillEnum.PingPong;

        SpawnHero(null);
    }

    void Update()
    {
        CurrentLevelEnd();
    }

    private void FixedUpdate()
    {
        HeroStandBy();
    }

    public AbilityManager abilityManager;
    void InitManagers()
    {
        abilityManager = new AbilityManager();
        abilityManager.Initialize();
        StartCoroutine(abilityManager.SetupAsync());

        AssetManager.LoadAssetsAsync<TextAsset>("Level", null).Completed += (AsyncOperationHandle<IList<TextAsset>> asyncLoad) =>
        {
            if (asyncLoad.IsDone && asyncLoad.IsValid())
            {
                List<TextAsset> levels = (List<TextAsset>)asyncLoad.Result;
                _Levels = levels.ToArray();

                if (GameSetting.DebugMode)
                {
                    EnterLevel(10001);
                }
            }
        };
    }

    public Rect GetScreenRect()
    {
        return screenRect;
    }

    void SetBounds()
    {
        if (wall != null && wall.points.Length > 0)
        {
            List<Vector2> newVerticies = new List<Vector2>();
            newVerticies.Add(new Vector2(screenRect.x, screenRect.y));
            newVerticies.Add(new Vector2(Mathf.Abs(screenRect.x), screenRect.y));
            newVerticies.Add(new Vector2(Mathf.Abs(screenRect.x), Mathf.Abs(screenRect.y)));
            newVerticies.Add(new Vector2(screenRect.x, Mathf.Abs(screenRect.y)));
            newVerticies.Add(new Vector2(screenRect.x, screenRect.y));

            wall.points = newVerticies.ToArray();
        }

        var aspectRatio = baseScreenSize.x / baseScreenSize.y;
        var baseCameraWidth = Camera.main.orthographicSize * 2 * aspectRatio;
        var cameraWidth = Camera.main.orthographicSize * 2 * Camera.main.aspect;
        MultipleCoefficient = cameraWidth / baseCameraWidth - 1;
        Scale = 1f + MultipleCoefficient;
        background.localScale = new Vector3(Scale, Scale, 1f);
        spawnPointsWrapper.localScale = new Vector3(Scale, Scale, 1f);

        rowsCount = Mathf.FloorToInt(baseCameraWidth / Camera.main.aspect);
    }

    private int rowsCount = 0;
    void SetMap()
    {
        if (!boardManager.isActiveAndEnabled)
        {
            return;
        }

        if (rowsCount > 0)
        {
            boardManager.SetRowsCount(rowsCount);
        }
        boardManager.SetupScene(1);
    }

    public void NextLevel()
    {
        currentRowIndex = loadRowCount = 0;
        level++;
        SetLevel();
        Random.SetSeed(Guid.NewGuid().ToString());
    }

    private List<LevelRowsModelInfo> rowsModelInfos;
    private int levelStartIndex = -1;
    static TextAsset[] _Levels;
    bool SetLevel(string map = "L01", int startIndex = -1)
    {
        SwtichBGM(true);
        if (_Levels == null)
        {
            //_Levels = AssetManager.LoadAssetsFromFile<TextAsset>(GameSetting.ABPLEVEL);
            Debug.LogError("levels null.");
            return false;
        }

        TextAsset levelData = null;
        foreach (var level in _Levels)
        {
            if (level.name == map)
            {
                levelData = level;
            }
        }

        if (levelData == null)
        {
            return false;
        }

        var serializeObj = ProtoBuf.Serializer.Deserialize<byte[]>(new MemoryStream(levelData.bytes));
        var decryptText = Tools.DecryptAes(serializeObj, Tools.lKey);
        if (!string.IsNullOrEmpty(decryptText))
        {
            rowsModelInfos = JsonConvert.DeserializeObject<List<LevelRowsModelInfo>>(decryptText);
            levelStartIndex = startIndex;// LevelStartIndex, Load From Config

            var monsters = rowsModelInfos.SelectMany(x => x.Columns.Where(z => z.Type == 0).Select(z => new { cid = z.Id }))
                .GroupBy(o => o.cid)
                .Select(s => new { Id = s.Key, Count = s.Count() }).ToDictionary(r => r.Id, r => r.Count);
            objectPool.RebuildPool(monsters, true, () =>
            {
                PlaceBricks();// init
            });

            Assert.IsNotNull(currentLevelModel, "Level Model Can Not Null.");
            currentLevelModel.TotalHp = rowsModelInfos.Sum(x => x.Columns.Where(cell => cell.Type == 0).Sum(c => c.Hp));

            if (GameSetting.DebugMode)
            {
                currentLevelModel.TotalHp = 9999;
            }
        }
        return true;
    }

    TextAsset LoadLevelFromResources(string map)
    {
        return Resources.Load<TextAsset>(string.Format("Levels/{0}", map));
    }

    void ResetLevel()
    {
        brickInScene.Clear();
        rowsModelInfos = null;
        currentRowIndex = loadRowCount = 0;
        levelStartIndex = -1;

        isReadyStay = true;
        currentLevelRecord = new LevelRecordInfo();
        currentLevelModel = new LevelModelInfo();

        objectPool.ResetPool();
        ballCtrl.HorizontalReturn2Zero();
        ballCtrl.currentBallState = BallController.BallState.aim;
        exBallManager.Reset();
        HeroController.Instance.Move2FirePoint(Vector3.zero, true);

        Random.SetSeed(Guid.NewGuid().ToString());
        AbilityManager.InitCardSkill();
    }

    public void PlaceBricks(bool isRandomMode = false)
    {
        if (isRandomMode)
        {
            levelModel = Enums.LevelTypeEnum.classic;
            RandomSpawnBricks();
            numberOfExtraBallsInRow = 0;
            return;
        }

        if (rowsModelInfos == null)
        {
            Debug.LogError("rowsModelInfos is null.");
            return;
        }

        if (loadRowCount >= rowsModelInfos.Count)
        {
            return;
        }

        do
        {
            List<LevelColumnsModelInfo> columnsModelInfos = null;
            foreach (var row in rowsModelInfos)
            {
                if (row.Rows == currentRowIndex)
                {
                    columnsModelInfos = row.Columns;
                    loadRowCount++;
                    break;
                }
            }

            if (columnsModelInfos == null)
            {
                // this row is empty
                currentRowIndex++;// pass 2 next row
                continue;
            }

            foreach (var col in columnsModelInfos)
            {
                if (col.Index > 0 && col.Index < spawnPoints.Length + 1)
                {
                    var point = spawnPoints[col.Index - 1];
                    if (point != null)
                    {
                        var tag = GetModelTag(col.Id, col.Type);
                        if (col.Type == 0)
                        {
                            SpawnBricks(point, tag, col, false, col.Id);
                        }
                        else if (col.Type == 1 && numberOfExtraBallsInRow == 0)
                        {
                            SpawnBricks(point, tag, col, true);
                            if (isRandomMode)
                            {
                                numberOfExtraBallsInRow++;
                            }
                        }
                    }
                    else
                    {
                        Debug.LogError("spawn points is null.");
                    }
                }
            }

            numberOfExtraBallsInRow = 0;
            currentRowIndex++;
        } while (currentRowIndex < levelStartIndex);

        levelStartIndex = -1;// initialize complele, set -1
    }

    // TODO: 根据配表ID获取相应缓存池里的模型
    string GetModelTag(int id, int type)
    {
        if (type == 0)
        {
            return null;
        }
        else
        {
            // Item
            return GameSetting.TAGEXTRABALLPWUP;
        }
    }

    void RandomSpawnBricks()
    {
        foreach (var item in spawnPoints)
        {
            int brickToCreate = RND.Range(0, 9);
            if (brickToCreate == 0)
            {
                SpawnBricks(item, GameSetting.TAGBRICKSQUARE, null);
            }
            else if (brickToCreate == 1)
            {
                SpawnBricks(item, GameSetting.TAGBRICKTRIANGLE, null);
            }
            else if (brickToCreate == 2 && numberOfExtraBallsInRow == 0)
            {
                SpawnBricks(item, GameSetting.TAGEXTRABALLPWUP, null, true);
                numberOfExtraBallsInRow++;
            }
        }
    }

    void SpawnBricks(Transform root, string tag, LevelColumnsModelInfo data, bool selfScaled = false, int getById = -1)
    {
        GameObject brick;
        if (getById > 0)
        {
            brick = objectPool.GetPooledObject(getById);
        }
        else
        {
            // by Tag
            brick = objectPool.GetPooledObject(tag);
        }
        Assert.IsNotNull(brick, string.Format("Spawn Bricks Object Is Null.[{0}]", getById > 0 ? getById.ToString() : tag));

        brickInScene.Add(brick);
        if (brick != null)
        {
            brick.transform.position = root.position;
            brick.transform.rotation = Quaternion.identity;

            Vector3 scale;
            if (!selfScaled)
            {
                scale = new Vector3(Scale, Scale, 1f);
                if (is3D)
                {
                    scale = new Vector3(0.3f, 0.3f, 0.3f);// temp
                }
            }
            else
            {
                scale = new Vector3(brick.transform.localScale.x * Scale
                    , brick.transform.localScale.y * Scale
                    , 1f);
                if (is3D)
                {
                    scale = new Vector3(0.3f, 0.3f, 0.3f);// temp
                }
            }

            brick.transform.localScale = scale;
            if (is3D)
            {
                brick.transform.localRotation = new Quaternion(0, 180, 0, 0);
                SpawnHero(brick, 0);
            }

            if (data != null)
            {
                var brickHealthManager = brick.GetComponent<BrickHealthManager>();
                if (brickHealthManager != null)
                {
                    brickHealthManager.SetHealth(data.Hp);
                }
            }

            // start index not zero
            if (levelStartIndex > 1)
            {
                var multiple = Mathf.Abs(currentRowIndex - levelStartIndex + 1);
                if (is3D)
                {
                    brick.transform.position = brick.transform.position - new Vector3(0, 0, multiple * GameSetting.GridBounds.z);
                }
                else
                {
                    brick.transform.position = brick.transform.position - new Vector3(0, multiple * GameSetting.GridBounds.y, 0);
                }
            }

            brick.SetActive(true);
        }
    }

    void HeroStandBy()
    {
        // Move Hero To Fire Point
        if (ballCtrl.StopCounter >= 1 && !HeroController.Instance.OnStandBy)
        {
            HeroController.Instance.OnStandBy = true;
            HeroController.Instance.Move2FirePoint(ballCtrl.StopPoint);
        }
    }

    void Initialize()
    {
        ConfigManager.Instance.LoadAllConfigs();
    }

    void InitializePlayerData()
    {
        string name = Singleton<Me>.Instance.Player.UserName;
        Debug.Log("name: " + name);
    }

    void ReadConfig()
    {
        foreach (var item in ConfigManager.Instance.GetItems())
        {
            Debug.Log("item: " + item.Id + ", Name: " + item.Name);
        }
    }

    LevelRecordInfo currentLevelRecord;
    LevelModelInfo currentLevelModel;
    public bool IsNextLevelGridLoaded { get; set; }
    public void EnterLevel(int id)
    {
        if (id <= 0)
        {
            id = Singleton<Me>.Instance.Player.CurrentDupId;
        }
        var current = ConfigManager.Instance.GetLevelInfo(id);
        if (current != null)
        {
            Singleton<Me>.Instance.Player.CurrentDupId = id;

            ResetLevel();
            Assert.IsNotNull(currentLevelRecord, "Level Record Can Not Null.");
            currentLevelRecord.Index = id;

            if (SetLevel(current.MapPath, current.StartIndex))
            {
                Status = Enums.GameStatusEnum.Battle;
                uiManager.EnterBattleView();
                return;
            }

            isReadyStay = false;
            Debug.LogError("load level error.");
            // TODO: alert dialog tips.
        }
    }

    public void Calculate(int damage)
    {
        Assert.IsNotNull(currentLevelModel, "Level Model Can Not Null.");
        currentLevelModel.TotalHp -= damage;

        if (currentLevelModel.TotalHp <= 0)
        {
            // clear
            // TODO: Score Calculate
            currentLevelRecord.Score = 100;
            currentLevelRecord.StarCount = 3;
            Singleton<Me>.Instance.Player.LevelAdd(currentLevelRecord);
            Singleton<Me>.Instance.Save();
        }
    }

    bool isReadyStay;
    void CurrentLevelEnd()
    {
        var ballStop = (ballCtrl.currentBallState == BallController.BallState.wait ||
                        ballCtrl.currentBallState == BallController.BallState.aim);
        if (ballStop &&
            ballsInScene.Count == 1 &&
            isReadyStay &&
            currentLevelModel != null &&
            currentLevelModel.TotalHp <= 0)
        {
            isReadyStay = false;
            Status = Enums.GameStatusEnum.MainUI;
            // ui
            uiManager.Ready2Next();
        }
    }

    public void SwtichBGM(bool inBattle)
    {
        if (soundManager != null)
        {
            soundManager.source.clip = (inBattle ? soundManager.acTown : soundManager.acMain);
            soundManager.source.Play();
        }
    }

    public void UpdateHitCount(bool isMainBall, int count)
    {
        if (isMainBall)
        {
            ballCtrl.HitEmptyCount += count;
            if (count == 0)
            {
                ballCtrl.HitEmptyCount = 0;
            }

            var isSkip = ballCtrl.HitEmptyCount >= GameSetting.BATTLE_MAX_RECORD_HIT_EMPTY;
            if (isSkip != accelerateSkip)
            {
                accelerateSkip = isSkip;
                CurrentAccelerateSeed = accelerateSkip ? GameSetting.BATTLE_ACCELERATE_SEED : GameSetting.BATTLE_ACCELERATE_SEED;
                NotifyBallAccelerateChange();
            }
        }
    }

    // --- Event

    public Action<bool> OnGameStatusChange;
    public Action<bool> OnBallAccelerateChange;

    public void NotifyGameStatusChange()
    {
        if (OnGameStatusChange == null) { return; }
        OnGameStatusChange.Invoke(paused);
    }

    public void NotifyBallAccelerateChange()
    {
        if (OnBallAccelerateChange == null) { return; }
        OnBallAccelerateChange.Invoke(accelerateSkip);
    }

    // --- Hero

    public void SpawnHero(GameObject hero, float fixY = 0.5f)
    {
        if (hero == null)
        {
            hero = objHero;
        }
        if (hero == null) return;

        Debug.Log("SpawnHero Name " + hero.name);
        Physics.Raycast(hero.transform.position, Vector3.down, out RaycastHit hitInfo, 10.0f, 1 << LayerMask.NameToLayer(GameSetting.LAYERFLOOR));
        if (hitInfo.collider != null)
        {
            Debug.Log("hitInfo.collider->" + hitInfo.point);
            var pos = hero.transform.position;
            pos.y = hitInfo.point.y + fixY;
            hero.transform.position = pos;
        }
    }
}
