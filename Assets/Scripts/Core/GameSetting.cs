using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting
{
    public static bool DebugMode { get; set; } = false;
    public static Vector3 GridBounds { get; private set; } = new Vector3(0.5f, 0.5f, 0.5f);

    #region Define

    public const string TAGBALL = "Ball";
    public const string TAGEXTRABALL = "Extra Ball";
    public const string TAGBRICKSQUARE = "Square Brick";
    public const string TAGBRICKTRIANGLE = "Triangle Brick";
    public const string TAGWALL = "Wall";
    public const string TAGEXTRABALLPWUP = "Extra Ball Powerup";

    public const string SCENEMAINBOARD = "Level0";//"Main";

    public const string LAYERGROUND = "Ground";
    public const string LAYERFLOOR = "Floor";
    public const string LAYERBOUNDARY = "Boundary";// wall
    public const string LAYERBATTLEOBJS = "BattleObjects";

    // asset bundle path name
    public const string ABPROOT = "AssetBundles";
    public const string ABPCONFIG = "files/config";
    public const string ABPLEVEL = "files/level";
    public const string ABPTEXCARD = "ui/textures/card";
    public const string ABPMODEL = "characters";

    // game
    public const float BATTLE_ACCELERATE_SEED = 1.8f;
    public const int BATTLE_MAX_RECORD_HIT_EMPTY = 3;
    /// <summary>
    /// 卡牌技能作用域：0，主球、1，所有球
    /// </summary>
    public const int CARD_SKILL_ACTION_SCOPE = 0;

    // Math
    public const float I2FP = 10000.0f;
    public const float FP2I = 0.0001f;
    public const float I2P = 100.0f;
    public const float P2I = 0.01f;

    #endregion Define
}
