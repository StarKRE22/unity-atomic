using System.Collections.Generic;

namespace Atomic.Elements
{
    public interface IAtomicSet<T> : ISet<T>
    {
        event StateChangedHandler OnStateChanged;
        event AddItemHandler<T> OnItemAdded;
        event RemoveItemHandler<T> OnItemRemoved;
        event ClearHandler OnCleared;
    }
}