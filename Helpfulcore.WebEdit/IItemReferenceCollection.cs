using System;
using System.Collections.Generic;

namespace Helpfulcore.WebEdit
{
    public interface IItemReferenceCollection
    {
        void AddItemReference(Guid itemId);

        void AddItemReferences(IEnumerable<Guid> itemIds);

        IEnumerable<Guid> GetItemReferences(Guid currentItemId);

        void Clear(Guid currentItemId);
    }
}