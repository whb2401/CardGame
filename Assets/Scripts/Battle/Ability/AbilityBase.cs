using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLH.Battle.Ability
{
    public class AbilityBase
    {
        protected Core.Enums.BallSkillEnum SkillType;
        protected BallController Ctrl;

        public AbilityBase(BallController controller)
        {
            Ctrl = controller;
        }

        protected virtual void Init()
        { }

        public virtual void Use()
        { }

        public virtual void OnUpdate()
        { }

        public virtual void OnFixedUpdate()
        { }

        public virtual void OnEndTurn()
        { }

        public void Destory() { }
    }
}
