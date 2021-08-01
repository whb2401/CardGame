using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FLH.Core.Enums;

public class CardInfo
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Level { get; set; }
    public int Influence { get; set; }
    public bool IsInfluenceLevelMax
    {
        get
        {
            return Level == 15 && Influence == 100;// Debug
        }
    }
    public CampEnum Camp { get; set; }
    public PropertiesEnum Properties { get; set; }
    public CardPositionEnum Position { get; set; }
    /// <summary>
    /// 专武槽
    /// </summary>
    public int ExclusiveWeaponsSlot { get; set; } = 0;
    public string ExclusiveWeaponName { get; set; }
    public string SkillClassName { get; set; }
    /// <summary>
    /// 装备位置，3为辅助位
    /// </summary>
    public int EquippedSlot { get; set; }
    public string Code { get; set; }
    public string AssetName
    {
        get
        {
            return string.Format("/card_{0}", Code);
        }
    }
}
