using System;
using System.Collections.Generic;
using System.Linq;

namespace Ball.Config
{
    public sealed class CardTemplateExt : CardTemplate
    {
        public override void OnInit()
        {
            if (Id < 0)
            {
                throw new ArgumentException(string.Format("Id[{0}] is less zero.", Id));
            }
        }
    }

    public sealed class CardTemplateManager : ConfigSingleExtend<CardTemplateManager, CardTemplateExt>
    {
        protected override int GetId(CardTemplateExt item)
        {
            return item.Id;
        }

        public CardTemplateExt GetFirst()
        {
            return Dict.Values.ElementAt(0);
        }

        public List<CardTemplateExt> GetTemplates()
        {
            return Dict.Values.ToList();
        }

        public CardTemplateExt GetTemplate(int id)
        {
            if (IsExists(id))
            {
                return GetItemTemplate(id);
            }

            return null;
        }
    }
}
