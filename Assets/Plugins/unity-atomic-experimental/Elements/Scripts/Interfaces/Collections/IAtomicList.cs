using System.Collections.Generic;

namespace Atomic.Elements
{
    public interface IAtomicList<T> : IList<T>
    {
        event StateChangedHandler OnStateChanged;
        event ChangeItemHandler<T> OnItemChanged;
        event InsertItemHandler<T> OnItemInserted;
        event DeleteItemHandler<T> OnItemDeleted;
        event ClearHandler OnCleared;
    }
}