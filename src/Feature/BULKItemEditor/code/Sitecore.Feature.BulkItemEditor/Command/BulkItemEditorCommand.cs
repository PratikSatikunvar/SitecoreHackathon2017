using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Web.UI.Sheer;

namespace Sitecore.Feature.BulkItemEditor.Command
{
    public class BulkItemEditorCommand : Sitecore.Shell.Framework.Commands.Command
    {
        public override void Execute(CommandContext context)
        {
            SheerResponse.ShowModalDialog("/tools/bulkitemeditor.aspx", "1015px", "600px");
        }
    }
}