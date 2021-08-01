using System;
using System.Collections.Generic;
using System.Linq;

namespace Ball.Config
{
    public sealed class DuplicateTemplateExt : DuplicateTemplate
    {
        public string Path { get; set; }

        public override void OnInit()
        {
            if (Id < 0)
            {
                throw new ArgumentException(string.Format("Id[{0}] is less zero.", Id));
            }
        }
    }

    public sealed class DuplicateTemplateManager : ConfigSingleExtend<DuplicateTemplateManager, DuplicateTemplateExt>
    {
        protected override int GetId(DuplicateTemplateExt item)
        {
            return item.Id;
        }

        public DuplicateTemplateExt GetFirst()
        {
            return Dict.Values.ElementAt(0);
        }

        public List<DuplicateTemplateExt> GetTemplates()
        {
            return Dict.Values.ToList();
        }

        public DuplicateTemplateExt GetTemplate(int id)
        {
            if (IsExists(id))
            {
                return GetItemTemplate(id);
            }

            return null;
        }
    }
}
