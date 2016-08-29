using System;
using Sitecore;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.RenderField;

namespace Helpfulcore.WebEdit.Pipelines.RenderField
{
    public class CollectItemReferences
    {
        public void Process(RenderFieldArgs args)
        {
            try
            {
                if (this.CanCollectReferences(args))
                {
                    ItemReferenceCollection.FromConfiguration().AddItemReference(args.Item.ID.Guid);
                }
            }
            catch(Exception ex)
            {
                Log.Error("Error in CollectItemReferences pipeline processor.", ex, this);
            }
        }

        protected virtual bool CanCollectReferences(RenderFieldArgs args)
        {
            return args != null && args.Item != null && !args.Aborted 
                && !args.DisableWebEdit && !args.DisableWebEditContentEditing
                && (Context.PageMode.IsPageEditor || Context.PageMode.IsPageEditorEditing);
        }
    }
}