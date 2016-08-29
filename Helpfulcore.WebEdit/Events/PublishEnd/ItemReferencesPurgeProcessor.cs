using System;
using Sitecore.Data;
using Sitecore.Publishing.Pipelines.PublishItem;

namespace Helpfulcore.WebEdit.Events.PublishEnd
{
    public class ItemReferencesPurgeProcessor
    {
        public void ClearItemReferences(object sender, EventArgs args)
        {
            var itemArgs = args as ItemProcessedEventArgs;

            if (itemArgs != null && itemArgs.Context != null && itemArgs.Context.ItemId != ID.Null && itemArgs.Context.ItemId != ID.Undefined)
            {
                ItemReferenceCollection.FromConfiguration().Clear(itemArgs.Context.ItemId.Guid);
            }
        }
    }
}