using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ball.Config
{
    public sealed class SkillTemplateExt : SkillTemplate
    {
        public override void OnInit()
        {
            if (Id < 0)
            {
                throw new ArgumentException(string.Format("Id[{0}] is less zero.", Id));
            }
        }
    }

    public sealed class SkillTemplateManager : ConfigSingleExtend<SkillTemplateManager, SkillTemplateExt>
    {
        protected override int GetId(SkillTemplateExt item)
        {
            return item.Id;
        }

        public List<SkillTemplateExt> GetTemplates()
        {
            return Dict.Values.ToList();
        }
    }
}
