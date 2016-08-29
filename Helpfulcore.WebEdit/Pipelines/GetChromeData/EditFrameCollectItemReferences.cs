using System;
using Sitecore;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.GetChromeData;

namespace Helpfulcore.WebEdit.Pipelines.GetChromeData
{
    public class EditFrameCollectItemReferences : GetChromeDataProcessor
    {
        public override void Process(GetChromeDataArgs args)
        {
            try
            {
                if (this.CanCollectReferences(args))
                {
                    ItemReferenceCollection.FromConfiguration().AddItemReference(args.Item.ID.Guid);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error in EditFrameCollectItemReferences pipeline processor.", ex, this);
            }
        }

        protected virtual bool CanCollectReferences(GetChromeDataArgs args)
        {
            return args != null && args.Item != null && !args.Aborted
                && args.ChromeType != null && args.ChromeData != null
                && "editFrame".Equals(args.ChromeType, StringComparison.OrdinalIgnoreCase)
                && (Context.PageMode.IsPageEditor || Context.PageMode.IsPageEditorEditing);
        }
    }
}
