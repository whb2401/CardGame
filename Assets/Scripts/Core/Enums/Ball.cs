using System;

namespace FLH.Core.Enums
{
    public enum BallSkillEnum
    {
        None,
        Accelerate,
        Devour,
        Flash,
        Through,
        PingPong,
        BeeBeeBee
    }

    /// <summary>
    /// 属性：火克木 木克雷 雷克兽 兽克血 血克冰 冰克火
    /// </summary>
    public enum PropertiesEnum
    {
        Fire,
        Ice,
        Beast,
        Thunder,
        IceBlood,
        BloodBeast,
        Wood,
        WoodBeast
    }

    /// <summary>
    /// 阵营：太阳、月亮、星星
    /// </summary>
    public enum CampEnum
    {
        Sun,
        Moon,
        Star
    }

    /// <summary>
    /// 主卡、辅助卡
    /// </summary>
    public enum CardPositionEnum
    {
        Main,
        Assist
    }
}
