using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FLH.Battle.Ability.Ball
{
    public class Devour : AbilityBase
    {
        private float waitTimeSeconds = 0.4f;
        private bool skillStart;
        private bool isDevourTime;
        private Vector2 mainBallStopLocation;
        private int eatExtraBallCount;
        readonly Vector3 vectorScale;

        public Devour(BallController controller) : base(controller)
        {
            SkillType = Core.Enums.BallSkillEnum.Devour;
            vectorScale = new Vector3(.05f, .05f, 0);
        }

        protected override void Init()
        {
            base.Init();
        }

        public override void Use()
        {
            base.Use();

            skillStart = true;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (!skillStart) return;
            if (!isDevourTime) return;
            skillStart = false;

            var index = 0;
            foreach (var item in GameManager.Instance.ballsInScene)
            {
                var extraBall = item.GetComponent<ExtraBallController>();
                if (extraBall == null) continue;
                index++;

                if (!mainBallStopLocation.Equals(Vector2.zero))
                {
                    extraBall.CurrentVelocity = Ctrl.ball.velocity;// record
                    extraBall.rgbSelf.velocity = Vector2.zero;// stop
                    extraBall.transform.DOMove(mainBallStopLocation, .1f * index).OnComplete(() =>
                    {
                        eatExtraBallCount++;
                        extraBall.gameObject.SetVisibility(false);

                        // bigger
                        Ctrl.transform.DOScale(Ctrl.transform.localScale + vectorScale, 0.05f);

                        // end
                        if (eatExtraBallCount >= GameManager.Instance.ballsInScene.Count - 1)
                        {
                            var times = 0;
                            Tools.Delay(.05f, ref times, () =>
                            {
                                DevourComplete();
                            });
                        }
                    });
                }
            }
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if (!skillStart) return;
            if (isDevourTime) return;

            waitTimeSeconds -= Time.fixedDeltaTime;
            if (waitTimeSeconds > 0)
            {
                return;
            }

            isDevourTime = true;
            GameManager.Instance.enableBallStuckFix = false;// 该技能临时关闭掉落修正，避免停止后掉落
            GameManager.Instance.exBallManager.enabled = false;
            Ctrl.CurrentVelocity = Ctrl.ball.velocity;// record
            Ctrl.ball.velocity = Vector2.zero;// stop
            mainBallStopLocation = Ctrl.transform.position;
        }

        private void DevourComplete()
        {
            Ctrl.ball.velocity = Ctrl.CurrentVelocity;// restore

            while (GameManager.Instance.ballsInScene.Count > 1)
            {
                var target = GameManager.Instance.ballsInScene.FirstOrDefault(x => x.GetComponent<ExtraBallController>() != null);
                if (target != null)
                {
                    GameManager.Instance.ballsInScene.Remove(target);
                }
            }

            GameManager.Instance.enableBallStuckFix = true;
        }
    }
}
