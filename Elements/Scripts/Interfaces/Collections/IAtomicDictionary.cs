using System.Collections.Generic;

namespace Atomic.Elements
{
    public interface IAtomicDictionary<K, V> : IDictionary<K, V>
    {
        event ChangeItemHandler<K, V> OnItemChanged;
        event AddItemHandler<K, V> OnItemAdded;
        event RemoveItemHandler<K, V> OnItemRemoved;
    }
}