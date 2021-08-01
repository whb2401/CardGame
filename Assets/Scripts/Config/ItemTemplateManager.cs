using System;
using System.Collections.Generic;
using System.Linq;

namespace Ball.Config
{
    public sealed class ItemTemplateExt : ItemTemplate
    {
        public override void OnInit()
        {
            if (Id < 0)
            {
                throw new ArgumentException(string.Format("Id[{0}] is less zero.", Id));
            }
        }
    }

    public sealed class ItemTemplateManager : ConfigSingleExtend<ItemTemplateManager, ItemTemplateExt>
    {
        protected override int GetId(ItemTemplateExt item)
        {
            return item.Id;
        }

        public List<ItemTemplateExt> GetTemplates()
        {
            return Dict.Values.ToList();
        }
    }
}
