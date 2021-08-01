using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelModelInfo
{
    public int ID { get; set; }
    public int PassStar { get; set; }
    public long Score { get; set; }
    public int TotalHp { get; set; }
}

[System.Serializable]
public class LevelRowsModelInfo
{
    public int Rows { get; set; }
    public int StartIndex { get; set; } = -1;
    public List<LevelColumnsModelInfo> Columns { get; set; }
}

[System.Serializable]
public class LevelColumnsModelInfo
{
    public int Id { get; set; }
    public int Type { get; set; }
    public int Index { get; set; }
    public bool IsBoss { get; set; }
    public int Hp { get; set; }
}

[System.Serializable]
public sealed class LevelRecordInfo
{
    public int Index { get; set; }
    public long Score { get; set; }
    public int StarCount { get; set; }
    public bool IsPassed
    {
        get
        {
            return StarCount > 0 && Score > 0;
        }
    }

    public void PasteData(LevelRecordInfo info)
    {
        if (info == null)
        {
            return;
        }

        Score = info.Score;
        StarCount = info.StarCount;
    }
}
