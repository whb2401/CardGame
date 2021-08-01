using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class HeroBasicAnimator : CSharpAnimator
{
    [SerializeField]
    public Motion idleMotion;

    [SerializeField]
    public Motion attackMotion;

    [SerializeField]
    public Motion attackIdleMotion;

    [SerializeField]
    public Motion runMotion;

    [SerializeField]
    public Motion deathMotion;

    [SerializeField]
    public Motion failMotion;

    [SerializeField]
    public Motion winMotion;

    [SerializeField]
    public Motion skillMotion;

    [SerializeField]
    public Motion showMotion;

    [SerializeField]
    public Motion showIdleMotion;

    public float speedForRun = 0.01f;

    public override AnimatorData Construct()
    {
        var idleTrigger = TriggerParam("Idle_Trigger");
        var attackSpeed = FloatParam("Attack_Speed", 1.0f);
        var transitions = new List<TransitionContext>()
        {

            Transition().Any()
                .Destination("Idle")[Trigger(idleTrigger)]
                .TransitionTime(0.15f)
                .ExitTime(false, 0.85f)
                .FixedTransitionTime(false)
        };

        var states = new List<StateContext>
        {
            State("Idle", idleMotion, new Vector2(400, 0)),
        };

        if (attackMotion != null)
        {
            var attackTrigger = TriggerParam("Attack_Trigger");
            transitions.Add(Transition().Source("Idle")
                .Destination("Attack")
                .TransitionTime(0.15f)
                .ExitTime(false, 0.85f)
                .FixedTransitionTime(false)[
                    Trigger(attackTrigger)
                ]);
            states.Add(State("Attack", attackMotion, new Vector2(800, -200)).SpeedParam(attackSpeed));
        }

        if (attackIdleMotion == null)
        {
            transitions.Add(Transition().Source("Attack").Destination("Idle").TransitionTime(0.15f).ExitTime(true, 0.85f).FixedTransitionTime(false));
        }
        else
        {
            states.Add(State("AttackIdle", attackIdleMotion, new Vector2(600, -400)));
            transitions.Add(Transition().Source("Attack").Destination("AttackIdle").TransitionTime(0.15f).ExitTime(true, 0.85f).FixedTransitionTime(false));
            transitions.Add(Transition().Source("AttackIdle").Destination("Idle").TransitionTime(0.15f).ExitTime(true, 2.85f).FixedTransitionTime(false));
        }

        if (winMotion != null)
        {
            var winTrigger = TriggerParam("Win_Trigger");

            states.Add(State("Win", winMotion, new Vector2(100, 400)));
            transitions.Add(Transition().Source("Idle").Destination("Win").TransitionTime(0.15f).ExitTime(false, 0.85f).FixedTransitionTime(false)[Trigger(winTrigger)]);
        }

        if (failMotion != null)
        {
            var failTrigger = TriggerParam("Fail_Trigger");

            states.Add(State("Fail", failMotion, new Vector2(100, 600)));
            transitions.Add(Transition().Source("Idle").Destination("Fail").TransitionTime(0.15f).ExitTime(false, 0.85f).FixedTransitionTime(false)[Trigger(failTrigger)]);
        }

        if (deathMotion != null)
        {
            var isDeath = BoolParam("IsDeath", false);

            states.Add(State("Death", deathMotion, new Vector2(100, 200)));
            transitions.Add(Transition().Source("Idle").Destination("Death")[Bool(isDeath, true)]);
            transitions.Add(Transition().Source("Death").Destination("Idle")[Bool(isDeath, false)]);
        }

        if (runMotion != null)
        {
            var moveSpeed = FloatParam("MoveSpeed", 0);

            states.Add(State("Run", runMotion, new Vector2(400, -200)));
            var trans = new TransitionContext[] {
                Transition().SourceMultiple("Idle", "Run"),

                Transition().Source("Idle")
                    .ExitTime(false, 0.85f)
                    .TransitionTime(0.15f)
                    .FixedTransitionTime(false)
                    .Destination("Run")[
                        Float(moveSpeed, speedForRun, ConditionMode.Greater)
                    ],
                Transition().Source("Run")
                    .ExitTime(false, 0.85f)
                    .TransitionTime(0.15f)
                    .FixedTransitionTime(false)
                    .Destination("Idle")[
                        Float(moveSpeed, speedForRun, ConditionMode.Less)
                    ]
            };

            transitions.AddRange(trans);
        }

        if (skillMotion != null)
        {
            var skillTrigger = TriggerParam("Skill01_Trigger");

            states.Add(State("Skill", skillMotion, new Vector2(800, 0)).SpeedParam(attackSpeed));

            transitions.Add(Transition().Source("Idle").Destination("Skill").TransitionTime(0.15f).ExitTime(false, 0.85f).FixedTransitionTime(false)[Trigger(skillTrigger)]);
            transitions.Add(Transition().Source("Skill").Destination("Idle").TransitionTime(0.15f).ExitTime(true, 0.85f).FixedTransitionTime(false));
        }

        if (showMotion != null)
        {
            var showTrigger = TriggerParam("Show_Trigger");

            states.Add(State("Show", showMotion, new Vector2(600, 200)));
            transitions.Add(Transition().Source("Idle").Destination("Show").TransitionTime(0f).ExitTime(false, 1f).FixedTransitionTime(false)[Trigger(showTrigger)]);

            if (showIdleMotion == null)
            {
                transitions.Add(Transition().Source("Show").Destination("Idle").TransitionTime(0.15f).ExitTime(true, 0.85f).FixedTransitionTime(false));
            }
            else
            {
                states.Add(State("Show_Idle", showIdleMotion, new Vector2(200, 200)));

                transitions.Add(Transition().Source("Show").Destination("Show_Idle").TransitionTime(0.15f).ExitTime(true, 0.85f).FixedTransitionTime(false));
            }
        }

        Animator
        (
            Graph()[
                Layer("Base")
                    .DefaultState("Idle")[states.ToArray()]
            ],
            Transitions()[transitions.ToArray()]
        );

        return currentData;
    }
}
