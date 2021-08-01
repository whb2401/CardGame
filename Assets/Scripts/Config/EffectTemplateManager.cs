using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ball.Config
{
    public sealed class EffectTemplateExt : EffectTemplate
    {
        public override void OnInit()
        {
            if (Id < 0)
            {
                throw new ArgumentException(string.Format("Id[{0}] is less zero.", Id));
            }
        }
    }

    public sealed class EffectTemplateManager : ConfigSingleExtend<EffectTemplateManager, EffectTemplateExt>
    {
        protected override int GetId(EffectTemplateExt item)
        {
            return item.Id;
        }

        public List<EffectTemplateExt> GetTemplates()
        {
            return Dict.Values.ToList();
        }

        public EffectTemplateExt GetTemplate(int id)
        {
            if (IsExists(id))
            {
                return GetItemTemplate(id);
            }

            return null;
        }
    }
}
