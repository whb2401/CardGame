using System;
using System.Collections.Generic;
using System.Linq;

namespace Ball.Config
{
    public sealed class GlobalTemplateExt : GlobalTemplate { }
    public sealed class GlobalTemplateManager : ConfigSingle<GlobalTemplateManager, GlobalTemplateExt>
    {
        public static GlobalTemplateExt Template { get; private set; }

        protected override void Init(GlobalTemplateExt template)
        {
            Template = template;
        }
    }
}
