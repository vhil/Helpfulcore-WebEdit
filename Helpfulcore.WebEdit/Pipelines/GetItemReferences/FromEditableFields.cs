using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Publishing.Pipelines.GetItemReferences;
using Sitecore.Publishing.Pipelines.PublishItem;

namespace Helpfulcore.WebEdit.Pipelines.GetItemReferences
{
    public class FromEditableFields : GetItemReferencesProcessor
    {
        protected override List<Item> GetItemReferences(PublishItemContext context)
        {
            try
            {
                if (context != null && context.VersionToPublish != null)
                {
                    var references = ItemReferenceCollection.FromConfiguration().GetItemReferences(context.VersionToPublish.ID.Guid).ToArray();

                    return references.Select(x => context.VersionToPublish.Database.GetItem(new ID(x))).ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error in FromEditableFields pipeline processor.", ex, this);
            }

            return new List<Item>();
        }
    }
}