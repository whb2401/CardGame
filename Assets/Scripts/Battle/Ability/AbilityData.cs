using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using FLH.Battle.Actor;

namespace FLH.Battle.Ability
{
    public class AbilityData : ScriptableObject
    {
        public enum BindType
        {
            Actor,
            Position,
        }

        public enum BindTarget
        {
            None,
            Master = 1,
            Target,
            MasterPosition,
            TargetPosition,
        }

        public enum PlayTime
        {
            None,
            OnPreUseAbility = 1,
            OnUseAbility,
            OnPostUseAbility,
            OnImpact,
            Custom1,
            Custom2,
        }

        public enum AbilityType
        {
            /// <summary>
            /// 远程
            /// </summary>
            ParticleEffect = 0,
            RayCast = 1,

            /// <summary>
            /// 近战
            /// </summary>
            Melee = 2,

            /// <summary>
            /// 远程施法
            /// </summary>
            ReachSpell = 3,
        }

        [Serializable]
        public class EffectReference
        {
            public int effectId;
            public GameObject refVal;

            public PlayTime playTime;
            public BindTarget bindTarget;
            public bool hangOverMainBall;

            public string positionOnNode;
            public Vector3 positionOffset;
            public float forwardOffset;
            public float rightOffset;
            public Vector3 scale;
            public Vector3 scaleOffset;

            public Vector3 rotation;
            public bool setEulerRotation;
        }

        public int skillId;
        public string displayName;
        public string description;
        public AbilityType abilityType;
        public EffectReference[] graphicReferences;
        public AnimationType prepareAnimationType;
        public AnimationType postAnimationType;
    }
}
