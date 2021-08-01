using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using FLH.Battle.Ability.Ball;
using FLH.Battle.Ability.Card;

namespace FLH.Battle.Ability
{
    public delegate void OnAbilityLoadReadyDelegate();

    public class AbilityManager
    {
        public static BallController ctrl;
        static AbilityBase abilityAccelerate;
        static AbilityBase abilityDevour;
        static AbilityBase abilityPingPong;

        public delegate AbilityCard CardSkillDelegate(CardInfo template);
        private static Dictionary<string, CardSkillDelegate> cardSkillMap;
        private static List<AbilityCard> cardSkills;

        public AbilityManager()
        {
            cardSkillMap = new Dictionary<string, CardSkillDelegate>();
            cardSkills = new List<AbilityCard>();
        }

        public void Initialize()
        {
            cardSkillMap.Add(typeof(Bombur).Name, Bombur.Create);
            cardSkillMap.Add(typeof(Edward).Name, Edward.Create);
        }

        public static void InitCardSkill()
        {
            cardSkills.Clear();
            // 先写死做测试
            // 正式应该初始化从表里读取所有卡，然后从Me里读取拥有卡中已装备的两张主卡和一张副卡
            // 在加载关卡前做初始化技能操作，战中直接释放相应的卡技能
            var equippedSkill = Singleton<Me>.Instance.EquipCard;
            foreach (var item in equippedSkill)
            {
                if (!cardSkillMap.TryGetValue(item.SkillClassName, out CardSkillDelegate creator))
                {
                    return;
                }

                var ability = creator(item);
                cardSkills.Add(ability);
            }
        }

        public static void PreDamage(GameObject attacker, Transform mainTarget, Action action)
        {
            if (cardSkills == null)
            {
                action.Invoke();
                return;
            }

            if (cardSkills.Count() <= 0)
            {
                action.Invoke();
                return;
            }

            // 选中待击目标等
            foreach (var item in cardSkills)
            {
                item.PickOnTargets(attacker, mainTarget, action);
            }
        }

        public static void Damage(Transform target)
        {
            if (cardSkills == null)
            {
                target.GetComponent<BrickHealthManager>().TakeDamage(1);// 普攻
                return;
            }

            if (cardSkills.Count() <= 0)
            {
                target.GetComponent<BrickHealthManager>().TakeDamage(1);// 普攻
                return;
            }

            foreach (var item in cardSkills)
            {
                item.OnDamage(target);
            }
        }

        public static void Init(BallController controller)
        {
            ctrl = controller;
            // TODO: 按启用的技能卡调用
            abilityAccelerate = new Accelerate(controller);
            abilityDevour = new Devour(controller);
            abilityPingPong = new PingPong(controller);
        }

        public static void Release()
        {
            //if (GameManager.PlayerSkill == Core.Enums.BallSkillEnum.Accelerate)
            //{
            //    abilityAccelerate.Use();
            //}
            //if (GameManager.PlayerSkill == Core.Enums.BallSkillEnum.Devour)
            //{
            //    abilityDevour.Use();
            //}
            //if (GameManager.PlayerSkill == Core.Enums.BallSkillEnum.PingPong)
            //{
            //    abilityPingPong.Use();
            //}

            foreach (var item in cardSkills)
            {
                item.Release();
            }
        }

        public static void Fire()
        { }

        public static void EndShot()
        {
            //if (GameManager.PlayerSkill == Core.Enums.BallSkillEnum.Accelerate)
            //{
            //    abilityAccelerate.OnEndTurn();
            //}
            //if (GameManager.PlayerSkill == Core.Enums.BallSkillEnum.Devour)
            //{
            //    abilityDevour.OnEndTurn();
            //}
            //if (GameManager.PlayerSkill == Core.Enums.BallSkillEnum.PingPong)
            //{
            //    abilityPingPong.OnEndTurn();
            //}

            foreach (var item in cardSkills)
            {
                item.EndShot();
            }
        }

        public static void EndStop()
        { }

        public static void EndGame()
        { }

        public static void Update()
        {
            //if (GameManager.PlayerSkill == Core.Enums.BallSkillEnum.Accelerate)
            //{
            //    abilityAccelerate.OnUpdate();
            //}
            //if (GameManager.PlayerSkill == Core.Enums.BallSkillEnum.Devour)
            //{
            //    abilityDevour.OnUpdate();
            //}
            //if (GameManager.PlayerSkill == Core.Enums.BallSkillEnum.PingPong)
            //{
            //    abilityPingPong.OnUpdate();
            //}
        }

        public static void FixedUpdate()
        {
            //if (GameManager.PlayerSkill == Core.Enums.BallSkillEnum.Accelerate)
            //{
            //    abilityAccelerate.OnFixedUpdate();
            //}
            //if (GameManager.PlayerSkill == Core.Enums.BallSkillEnum.Devour)
            //{
            //    abilityDevour.OnFixedUpdate();
            //}
            //if (GameManager.PlayerSkill == Core.Enums.BallSkillEnum.PingPong)
            //{
            //    abilityPingPong.OnFixedUpdate();
            //}
        }

        #region data

        public List<AbilityData> abilityData;
        public OnAbilityLoadReadyDelegate asyncLoadAbilityData;

        public IEnumerator SetupAsync()
        {
            if (abilityData == null)
            {
                abilityData = new List<AbilityData>();
            }

            var asyncLoad = AssetManager.LoadAssetsAsync<AbilityData>("Ability", null);
            yield return asyncLoad;

            if (asyncLoad.IsDone && asyncLoad.IsValid())
            {
                abilityData = (List<AbilityData>)asyncLoad.Result;
                asyncLoadAbilityData?.Invoke();
            }
        }

        #endregion data
    }
}
