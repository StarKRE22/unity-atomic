using System.Collections.Generic;

namespace Atomic
{
    internal interface IMapProvider<K, V>
    {
        V this[K index] { get; set; }
        bool TryGetValue(K index, out V result);

        List<KeyValuePair<K, V>> GetPairs();
        int GetPairsNonAlloc(KeyValuePair<K, V>[] results);
        
        bool Add(K index, V value);
        bool Remove(K index);
        bool Remove(K index, out V removed);
    }
}