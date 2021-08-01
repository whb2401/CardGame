using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Assertions;
using Newtonsoft.Json;
using Ball.Config;
using FLH.Core.Enums;
using System.Linq;

public class Me
{
    public string DeviceId { get; set; }
    [SerializeField]
    public PlayerInfo Player { get; set; }

    const string SessionKey = "frC}VD]eeAG1JdomjFit16#y-x_}w13";
    const string DataName = "sd_{0}.dat";

    string DataPath
    {
        get
        {
            var key = string.IsNullOrEmpty(DeviceId) ? "0" : DeviceId;
            return Path.Combine(Application.persistentDataPath, string.Format(DataName, key));
        }
    }

    string Key
    {
        get
        {
            return SessionKey + DeviceId;
        }
    }

    public void Save()
    {
        var serializeObj = JsonConvert.SerializeObject(Player);
        var encrypted = Tools.EncryptAES(serializeObj, Key);
        using (var stream = File.OpenWrite(DataPath))
        {
            ProtoBuf.Serializer.Serialize(stream, encrypted);
        }
    }

    public void Load()
    {
        if (File.Exists(DataPath))
        {
            using (var stream = File.OpenRead(DataPath))
            {
                var serializeObj = ProtoBuf.Serializer.Deserialize<byte[]>(stream);
                Assert.IsNotNull(serializeObj, "User SaveData Exist, But It Is Null. DataPath[" + DataPath + "]");
                var decryptText = Tools.DecryptAes(serializeObj, Key);
                Assert.IsNotNull(decryptText, "If SaveData Is Not Null, Here Is Null, Decrypt Failure.");
                if (!string.IsNullOrEmpty(decryptText))
                {
                    var deserializeObj = JsonConvert.DeserializeObject<PlayerInfo>(decryptText);
                    Player = deserializeObj;
                }
            }
        }

        if (Player == null)
        {
            Player = new PlayerInfo
            {
                Levels = new List<LevelRecordInfo>(),
                Cards = new List<SkillCardInfo>()
            };
        }
        else
        {
            if (Player.Levels == null)
            {
                Player.Levels = new List<LevelRecordInfo>();
            }
            if (Player.Cards == null)
            {
                Player.Cards = new List<SkillCardInfo>();
            }
        }

        Init();
    }

    public List<CardInfo> SkillCard;
    public List<CardInfo> EquipCard;

    private void Init()
    {
        // 技能卡Load
        var skills = ConfigManager.Instance.GetSkills();
        SkillCard = new List<CardInfo>();
        foreach (var item in skills)
        {
            Debug.Log("Skill: " + item.Id + ", Name: " + item.Name);
            SkillCard.Add(new CardInfo()
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Level = item.Level,
                Influence = item.Influence,
                Camp = (CampEnum)(item.Camp + 1),
                Properties = (PropertiesEnum)(item.Propertiess[0] + 1),
                Position = (CardPositionEnum)(item.Position + 1),
                SkillClassName = item.SkillClassName,
                Code = item.Code,
            });
        }

        EquipCard = new List<CardInfo>();
        foreach (var item in Player.Cards)
        {
            if (item.IsEquipped)
            {
                var equippedCard = SkillCard.First(x => x.Id == item.ID);
                if (equippedCard != null)
                {
                    equippedCard.Level += item.Level;
                    equippedCard.Influence += item.Influence;
                    equippedCard.EquippedSlot = item.EquippedSlot;
                }
                EquipCard.Add(equippedCard);
            }
        }
    }

    public void AddSkillCard(int id)
    {
        if (EquipCard.Count > 3)
        {
            // 总共只能装备两张主卡，一张辅助卡
            return;
        }

        var item = new SkillCardInfo()
        {
            ID = id,
            IsEquipped = true,
            EquippedSlot = EquipCard.Count + 1
        };
        Player.CardAdd(item);
        Save();

        var equippedCard = SkillCard.First(x => x.Id == item.ID);
        if (equippedCard != null)
        {
            equippedCard.Level += item.Level;
            equippedCard.Influence += item.Influence;
        }
        EquipCard.Add(equippedCard);
    }

    public void RemoveSkillCard(int id)
    {
        if (Player.Cards.Count(x => x.ID == id) <= 0) return;
        var target = Player.Cards.FirstOrDefault(x => x.ID == id);
        if (target != null)
        {
            Player.Cards.Remove(target);
        }
        Save();

        var targetEquipped = EquipCard.FirstOrDefault(x => x.Id == id);
        if (targetEquipped != null)
        {
            EquipCard.Remove(targetEquipped);
        }
    }
}
