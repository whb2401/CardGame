using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FLH.Battle.Ability.Ball
{
    public class Accelerate : AbilityBase
    {
        const float baseAccelerateSpeed = 2.5f;
        readonly List<ExtraBallController> extraBalls;

        public Accelerate(BallController controller) : base(controller)
        {
            SkillType = Core.Enums.BallSkillEnum.Accelerate;
            extraBalls = new List<ExtraBallController>();
        }

        protected override void Init()
        {
            base.Init();
        }

        public override void Use()
        {
            base.Use();

            Ctrl.CurrentVelocity = Ctrl.ball.velocity *= baseAccelerateSpeed;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            AccelerateExtraSpeed();
        }

        public override void OnEndTurn()
        {
            base.OnEndTurn();

            extraBalls.Clear();
        }

        public void AccelerateExtraSpeed()
        {
            foreach (var item in GameManager.Instance.ballsInScene)
            {
                var extraBall = item.GetComponent<ExtraBallController>();
                if (extraBall == null) continue;
                if (extraBalls.Contains(extraBall)) continue;

                extraBall.CurrentVelocity = extraBall.rgbSelf.velocity *= baseAccelerateSpeed;
                extraBalls.Add(extraBall);
            }
        }
    }
}
