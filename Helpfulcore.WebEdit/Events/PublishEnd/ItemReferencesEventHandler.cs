using System;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Publishing.Pipelines.PublishItem;

namespace Helpfulcore.WebEdit.Events.PublishEnd
{
    public class ItemReferencesEventHandler
    {
        public void ClearItemReferences(object sender, EventArgs args)
        {
            try
            {
                var itemArgs = args as ItemProcessedEventArgs;

                if (itemArgs != null && itemArgs.Context != null && itemArgs.Context.ItemId != ID.Null && itemArgs.Context.ItemId != ID.Undefined)
                {
                    ItemReferenceCollection.FromConfiguration().Clear(itemArgs.Context.ItemId.Guid);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error while processing publish:itemProcessed event in ItemReferencesPurgeProcessor event handler.", ex, this);
            }
        }
    }
}