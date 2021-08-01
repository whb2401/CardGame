using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FLH.Battle.Ability.Ball
{
    public class PingPong : AbilityBase
    {
        private bool skillStart;
        private bool canMove;

        public PingPong(BallController controller) : base(controller)
        {
            SkillType = Core.Enums.BallSkillEnum.PingPong;
        }

        protected override void Init()
        {
            base.Init();
        }

        public override void Use()
        {
            base.Use();

            skillStart = true;
            var times = 0;
            Tools.Delay(.5f, ref times, () =>
            {
                Ctrl.heroCtrl.SwitchShield(true);
            });
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (!skillStart && !canMove) return;

            if (Input.GetMouseButtonDown(0))
            {
                if (!Tools.IsTouchedUI())
                {
                    canMove = true;
                }
            }

            if (Input.GetMouseButton(0))
            {
                if (!Tools.IsTouchedUI())
                {
                    if (!canMove) return;
                    Vector2 tempMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    var position = new Vector3(tempMousePosition.x, 0, 0);
                    Ctrl.heroCtrl.transform.localPosition = position;
                    Ctrl.heroCtrl.MoveShield(position);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (!Tools.IsTouchedUI())
                {
                    if (!skillStart)
                    {
                        Ctrl.heroCtrl.SwitchShield(false);
                    }
                    canMove = false;
                }
            }
        }

        private void SwitchPingPongMode()
        { }

        public override void OnEndTurn()
        {
            base.OnEndTurn();
            if (!skillStart) return;

            skillStart = false;
            canMove = false;
            Ctrl.heroCtrl.SwitchShield(false);

            Ctrl.currentBallState = BallController.BallState.gap;
            Ctrl.heroCtrl.Move2FirePoint(Ctrl.StopPoint);
        }
    }
}
