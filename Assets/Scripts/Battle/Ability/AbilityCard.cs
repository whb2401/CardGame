using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FLH.Core.Enums;
using UnityEngine;

namespace FLH.Battle.Ability
{
    public class AbilityCard
    {
        protected bool loaded;
        protected readonly CardInfo currentCard;
        protected BallController Ctrl;
        protected int round;// 回合数

        public AbilityCard(CardInfo card)
        {
            currentCard = card;
            Ctrl = AbilityManager.ctrl;
            round = 1;// 初始化
        }

        /// <summary>
        /// 装备在辅助位
        /// </summary>
        protected bool IsEquippedAssistSlot
        {
            get
            {
                if (currentCard != null)
                {
                    return currentCard.EquippedSlot > 2;
                }
                return false;
            }
        }

        public virtual void Release()
        { }

        public virtual void EndShot()
        {
            round++;
        }

        /// <summary>
        /// 选中攻击目标
        /// </summary>
        public virtual void PickOnTargets(GameObject attacker, Transform mainTarget, Action action)
        {
            // 默认不需要筛选的技能，直接base执行action
            // 特殊需求的按需求处理
            if (action != null)
            {
                action.Invoke();
            }
        }

        /// <summary>
        /// 造成伤害
        /// </summary>
        /// <param name="target">单个目标</param>
        /// <param name="isMainBall">是否为主球</param>
        public virtual void OnDamage(Transform target)
        { }

        protected virtual float CalculateCritDamage(float customDamageValue = -1)
        {
            return customDamageValue > 0 ? customDamageValue : 1f;
        }

        /// <summary>
        /// 计算数属性相克的加成伤害
        /// </summary>
        /// <param name="target">目标</param>
        /// <returns>附加伤害值</returns>
        protected virtual float CalculatePropretiesDamage(object target)
        {
            // 火克木 木克雷 雷克兽 兽克血 血克冰 冰克火
            // 测试期间相克先默认减1即可
            var info = (CardInfo)target;

            var damageBonus = 0f;
            if (currentCard.Properties != info.Properties)
            {
                // 属性相克
                // ---对敌
                if (currentCard.Properties == PropertiesEnum.Fire &&
                    info.Properties == PropertiesEnum.Wood)
                {
                    // 火克木
                    damageBonus += 1;
                }
                else if (currentCard.Properties == PropertiesEnum.Wood &&
                         info.Properties == PropertiesEnum.Thunder)
                {
                    // 木克雷
                    damageBonus += 1;
                }
                else if (currentCard.Properties == PropertiesEnum.Thunder &&
                        (info.Properties == PropertiesEnum.Beast ||
                         info.Properties == PropertiesEnum.BloodBeast))
                {
                    // 雷克兽
                    damageBonus += 1;
                }
                else if ((currentCard.Properties == PropertiesEnum.Beast ||
                          currentCard.Properties == PropertiesEnum.BloodBeast) &&
                          info.Properties == PropertiesEnum.IceBlood)
                {
                    // 兽克血
                    damageBonus += 1;
                }
                else if (currentCard.Properties == PropertiesEnum.IceBlood &&
                         info.Properties == PropertiesEnum.Ice)
                {
                    // 血克冰
                    damageBonus += 1;
                }
                else if (currentCard.Properties == PropertiesEnum.Ice &&
                         info.Properties == PropertiesEnum.Fire)
                {
                    // 冰克火
                    damageBonus += 1;
                }
                // ---对我
                if (info.Properties == PropertiesEnum.Fire &&
                    currentCard.Properties == PropertiesEnum.Wood)
                {
                    // 火克木
                    damageBonus -= 0.5f;
                }
                else if (info.Properties == PropertiesEnum.Wood &&
                         currentCard.Properties == PropertiesEnum.Thunder)
                {
                    // 木克雷
                    damageBonus -= 0.5f;
                }
                else if (info.Properties == PropertiesEnum.Thunder &&
                        (currentCard.Properties == PropertiesEnum.Beast ||
                         currentCard.Properties == PropertiesEnum.BloodBeast))
                {
                    // 雷克兽
                    damageBonus -= 0.5f;
                }
                else if ((info.Properties == PropertiesEnum.Beast ||
                          info.Properties == PropertiesEnum.BloodBeast) &&
                          currentCard.Properties == PropertiesEnum.IceBlood)
                {
                    // 兽克血
                    damageBonus -= 0.5f;
                }
                else if (info.Properties == PropertiesEnum.IceBlood &&
                         currentCard.Properties == PropertiesEnum.Ice)
                {
                    // 血克冰
                    damageBonus -= 0.5f;
                }
                else if (info.Properties == PropertiesEnum.Ice &&
                         currentCard.Properties == PropertiesEnum.Fire)
                {
                    // 冰克火
                    damageBonus -= 0.5f;
                }
            }

            return damageBonus;
        }

        #region 概率

        protected virtual bool CheckProbability(float probability)
        {
            float rand = (float)GetGameRand() / 0x7FFF;
            return probability > rand;
        }

        public int GetGameRand()
        {
            return GameManager.Instance.Random.GetRand();
        }

        #endregion 概率
    }
}
