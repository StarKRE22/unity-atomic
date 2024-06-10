using System.Collections.Generic;

namespace Atomic.Elements
{
    public interface IAtomicList<T> : IList<T>
    {
        event ChangeItemHandler<T> OnItemChanged;
        event AddItemHandler<T> OnItemInserted;
        event RemoveItemHandler<T> OnItemDeleted;
        event ClearHandler OnCleared;
    }
}