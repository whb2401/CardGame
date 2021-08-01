using FLH.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using RND = UnityEngine.Random;

namespace FLH.Battle.Ability.Card
{
    /// <summary>
    /// 红胡子
    /// BOMBUR
    /// ---------
    /// [无专武].发射火弹 火属性 百分之三概率触发爆炸造成2x2范围伤害 初始子弹数量与bnb一致
    /// [有专武].发射火弹的同时伴有1-3发炮弹发射
    /// [辅助位].每3回合发射一次导弹
    /// 亲密度满时得到必杀技 爆破：攻击4x4个单位 cd4 卡牌等级每加一级多一颗火弹 满级提高百分比概率
    /// </summary>
    public class Bombur : AbilityCard
    {
        public static AbilityCard Create(CardInfo template) { return new Bombur(template); }

        const float triggerProbability = 5000f;// 触发概率
        const int defaultBoomAttackScope = 2;// 2*2
        const int advanceBoomAttackScope = 4;// 4*4
        readonly int[] rangeForFireBomb;

        int AttackScope
        {
            get
            {
                if (currentCard.IsInfluenceLevelMax)
                {
                    return advanceBoomAttackScope;
                }
                return defaultBoomAttackScope;
            }
        }

        public Bombur(CardInfo card) : base(card)
        {
            rangeForFireBomb = new int[] { 1, 3 };
            Loaded();
        }

        private void Loaded()
        {
            if (IsEquippedAssistSlot) return;

            if (currentCard.ExclusiveWeaponsSlot > 0)
            {
                // 有专武，则火弹上膛
                // 额外的1-3发 都会爆 100%
                var extraBallMgr = GameManager.Instance.exBallManager;
                if (extraBallMgr != null)
                {
                    var name = typeof(Bombur).Name;
                    var num = RND.Range(rangeForFireBomb[0], rangeForFireBomb[1]);
                    Debug.Log(name + "火弹" + num + "颗已上膛");
                    if (!extraBallMgr.specialBalls.ContainsKey(name))
                    {
                        extraBallMgr.specialBalls.Add(name, num);
                    }
                    extraBallMgr.LoadedBullet(name);
                }
            }
        }

        public override void Release()
        {
            base.Release();

            if (IsEquippedAssistSlot)
            {
                if (round % 3 == 0)
                {
                    // 每3回合发射一次导弹
                    Debug.Log("导弹发射");
                }
            }
        }

        public override void PickOnTargets(GameObject attacker, Transform mainTarget, Action action)
        {
            if (IsEquippedAssistSlot)
            {
                if (action != null) action.Invoke();
                return;
            }

            if (GameSetting.CARD_SKILL_ACTION_SCOPE == 0 &&
               !attacker.CompareTag(GameSetting.TAGBALL))
            {
                if (action != null) action.Invoke();
                return;
            }

            if (!CheckProbability(triggerProbability * GameSetting.FP2I))
            {
                if (action != null) action.Invoke();
                return;
            }

            var leftEnemies = Ctrl.GetRangeEnemies(mainTarget, Vector2.up, Vector2.left, AttackScope);
            var rightEnemies = Ctrl.GetRangeEnemies(mainTarget, Vector2.up, Vector2.right, AttackScope);

            var rangeEnemies = new List<Transform>();
            var leftCount = leftEnemies.Count();
            var rightCount = rightEnemies.Count();
            if (leftCount > 0 && rightCount > 0)
            {
                if (leftCount > rightCount)
                {
                    rangeEnemies.AddRange(leftEnemies);
                }
                else
                {
                    rangeEnemies.AddRange(rightEnemies);
                }
            }
            else
            {
                if (leftCount > 0 && rightCount <= 0)
                {
                    rangeEnemies.AddRange(leftEnemies);
                }
                if (leftCount <= 0 && rightCount > 0)
                {
                    rangeEnemies.AddRange(rightEnemies);
                }
            }

            foreach (var enemy in rangeEnemies)
            {
                Debug.Log("rangeEnemies.name: " + enemy.name);
                enemy.GetComponent<BrickHealthManager>().HitTarget();
            }
        }

        public override void OnDamage(Transform target)
        {
            base.OnDamage(target);

            var enemy = target.GetComponent<BrickHealthManager>();

            var damage = 1;// 从配表按规则计算
            var addDamage = CalculatePropretiesDamage(enemy.info);
            var finalDamage = damage + addDamage;

            enemy.TakeDamage(finalDamage);
        }
    }
}
