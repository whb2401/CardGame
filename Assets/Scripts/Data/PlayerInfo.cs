using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    [SerializeField]
    public string UserName { get; set; }
    [SerializeField]
    public int CurrentDupId { get; set; }
    [SerializeField]
    public int HeroId { get; set; }

    [SerializeField]
    public List<LevelRecordInfo> Levels { get; set; }

    public void LevelAdd(LevelRecordInfo info)
    {
        if (info == null)
        {
            return;
        }

        if (Levels == null)
        {
            Levels = new List<LevelRecordInfo>();
        }

        var exist = Levels.Count(x => x.Index == info.Index) > 0;
        if (exist)
        {
            // update
            foreach (var item in Levels)
            {
                if (item.Index == info.Index)
                {
                    item.PasteData(info);
                    break;
                }
            }
        }
        else
        {
            Levels.Add(info);
        }
    }

    [SerializeField]
    public List<SkillCardInfo> Cards { get; set; }

    public void CardAdd(SkillCardInfo info)
    {
        if (info == null)
        {
            return;
        }

        if (Cards == null)
        {
            Cards = new List<SkillCardInfo>();
        }

        var exist = Cards.Count(x => x.ID == info.ID) > 0;
        if (exist)
        {
            // update
            foreach (var item in Cards)
            {
                if (item.ID == info.ID)
                {
                    item.PasteData(info);
                    break;
                }
            }
        }
        else
        {
            Cards.Add(info);
        }
    }
}

[SerializeField]
public class SkillCardInfo
{
    public int ID { get; set; }
    public int Level { get; set; }
    public int Influence { get; set; }
    public bool IsEquipped { get; set; }
    public int EquippedSlot { get; set; }

    public void PasteData(SkillCardInfo info)
    {
        if (info == null)
        {
            return;
        }

        Level = info.Level;
        Influence = info.Influence;
        IsEquipped = info.IsEquipped;
        EquippedSlot = info.EquippedSlot;
    }
}
