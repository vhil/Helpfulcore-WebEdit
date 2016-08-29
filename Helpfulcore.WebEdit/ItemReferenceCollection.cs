using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Configuration;

namespace Helpfulcore.WebEdit
{
    /// <summary>
    /// An object which stores item related item references. The collection is supposed to be cleared per item bases on publish:itemProcessed event.
    /// </summary>
    /// <seealso cref="Helpfulcore.WebEdit.IItemReferenceCollection" />
    public class ItemReferenceCollection : IItemReferenceCollection
    {
        protected static object SyncRoot = new object();
        protected IDictionary<Guid, IDictionary<Guid, Guid>> ItemReferences = new Dictionary<Guid, IDictionary<Guid, Guid>>();

        /// <summary>
        /// Adds the item reference.
        /// </summary>
        /// <param name="itemId">The item identifier.</param>
        public void AddItemReference(Guid itemId)
        {
            lock (SyncRoot)
            {
                var contextItem = Sitecore.Context.Item;

                if (contextItem == null)
                {
                    return;
                }

                if (this.ItemReferences.ContainsKey(contextItem.ID.Guid))
                {
                    if (!this.ItemReferences[contextItem.ID.Guid].ContainsKey(itemId))
                    {
                        this.ItemReferences[contextItem.ID.Guid].Add(itemId, itemId);
                    }
                }
                else
                {
                    this.ItemReferences.Add(contextItem.ID.Guid, new Dictionary<Guid, Guid> {{itemId, itemId}});
                }
            }
        }

        /// <summary>
        /// Adds the item references.
        /// </summary>
        /// <param name="itemIds">The item ids.</param>
        public void AddItemReferences(IEnumerable<Guid> itemIds)
        {
            foreach (var itemId in itemIds)
            {
                this.AddItemReference(itemId);
            }
        }

        /// <summary>
        /// Gets the item references.
        /// </summary>
        /// <param name="currentItemId">The current item identifier.</param>
        /// <returns></returns>
        public IEnumerable<Guid> GetItemReferences(Guid currentItemId)
        {
            lock (SyncRoot)
            {
                if (this.ItemReferences.ContainsKey(currentItemId))
                {
                    return this.ItemReferences[currentItemId].Values.ToArray();
                }
            }

            return new List<Guid>();
        }

        /// <summary>
        /// Clears the specified item references.
        /// </summary>
        /// <param name="currentItemId">The current item identifier.</param>
        public void Clear(Guid currentItemId)
        {
            lock (SyncRoot)
            {
                if (this.ItemReferences.ContainsKey(currentItemId))
                {
                    this.ItemReferences.Remove(currentItemId);
                }
            }
        }

        /// <summary>
        /// Gets the instance configured in Sitecore config using Sitecore.Configuration.Factory.
        /// </summary>
        /// <returns></returns>
        public static IItemReferenceCollection FromConfiguration()
        {
            return Factory.CreateObject("helpfulcore/webEdit/itemReferences", false) as IItemReferenceCollection;
        }
    }
}