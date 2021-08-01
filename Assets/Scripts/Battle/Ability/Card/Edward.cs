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
    /// 外科医生
    /// Edward
    /// ---------
    /// [无专武].发射水波或者蓝色剑气 冰属性 第一击必定暴击 百分之3冰冻单位1-3回合 初始子弹数量：bnb-2
    /// [有专武].随机附带一片浪花攻击 浪花可以将最前排的3-5个单位退至后方的空格中 如果后格没有空格则效果无效
    /// [辅助位].前五发必定暴击
    /// 亲密度满时得到必杀技 暴风雨：让当前前五排的单位被水属性克制 cd5 卡牌等级每加一级多一道剑气 满级提高百分比概率
    /// </summary>
    public class Edward : AbilityCard
    {
        public static AbilityCard Create(CardInfo template) { return new Edward(template); }

        const float triggerProbability = 5000f;// 触发概率 300f
        bool firstAttackForRound;
        readonly int[] rangeForFreeze;

        public Edward(CardInfo card) : base(card)
        {
            rangeForFreeze = new int[] { 3, 3 };
            Loaded();
        }

        private void Loaded()
        {
            if (IsEquippedAssistSlot) return;

            // 初始子弹数量：bnb - 2
            var extraBallMgr = GameManager.Instance.exBallManager;
            if (extraBallMgr != null)
            {
                var name = typeof(Edward).Name;
                var num = -2;
                if (!extraBallMgr.specialBalls.ContainsKey(name))
                {
                    extraBallMgr.specialBalls.Add(name, num);
                }
                extraBallMgr.LoadedBullet(name);
            }
        }

        public override void Release()
        {
            base.Release();

            if (IsEquippedAssistSlot)
            {
                // 前五发必定暴击
            }

            if (currentCard.ExclusiveWeaponsSlot <= 0)
            {
                // 冰属性 第一击必定暴击
                firstAttackForRound = true;
            }
        }

        public override void EndShot()
        {
            base.EndShot();
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

            var enemyCtrl = mainTarget.GetComponent<EnemyController>();
            // 已冰冻
            if (enemyCtrl != null && enemyCtrl.IsBuffExist(Core.Enums.BuffTypeEnum.Freeze))
            {
                if (action != null) action.Invoke();
                return;
            }

            if (currentCard.ExclusiveWeaponsSlot <= 0)
            {
                // 无专武，百分之3冰冻单位1-3回合
                if (!CheckProbability(triggerProbability * GameSetting.FP2I))
                {
                    if (action != null) action.Invoke();
                    return;
                }

                var behindEnemies = Ctrl.GetRangeEnemies(mainTarget, Vector2.up, Vector2.up, 10);
                foreach (var enemy in behindEnemies)
                {
                    if (enemy == mainTarget) continue;
                    Debug.Log("behindEnemies.name: " + enemy.name);
                    enemy.GetComponent<BrickMovementController>().currentState = BrickMovementController.BrickState.wait;
                }

                Debug.Log("触发冰冻buff");
                if (enemyCtrl != null)
                {
                    // +1本轮不算
                    // 多次触发命中则叠加
                    enemyCtrl.SetBuffEffect(Core.Enums.BuffTypeEnum.Freeze, RND.Range(rangeForFreeze[0] + 1, rangeForFreeze[1] + 1));
                }
            }
        }

        public override void OnDamage(Transform target)
        {
            base.OnDamage(target);

            var enemy = target.GetComponent<BrickHealthManager>();

            var damage = 1;// 从配表按规则计算
            var addDamage = CalculatePropretiesDamage(enemy.info);
            var critDamage = 0f;
            if (firstAttackForRound)
            {
                // 暴击计算
                critDamage = CalculateCritDamage();
                firstAttackForRound = false;
                // TODO: 暴击表现
            }
            var finalDamage = damage + addDamage + critDamage;

            enemy.TakeDamage(finalDamage);
        }
    }
}
