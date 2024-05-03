using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Atomic
{
    internal sealed class DictionaryProvider<K, V> : IMapProvider<K, V>
    {
        [ShowInInspector, ReadOnly]
        private readonly Dictionary<K, V> dictionary = new();

        public V this[K index]
        {
            get => this.dictionary[index];
            set => this.dictionary[index] = value;
        }

        public bool TryGetValue(K index, out V result)
        {
            return this.dictionary.TryGetValue(index, out result);
        }

        public List<KeyValuePair<K, V>> GetPairs()
        {
            return new List<KeyValuePair<K, V>>(this.dictionary);
        }

        public int GetPairsNonAlloc(KeyValuePair<K, V>[] results)
        {
            int i = 0;

            foreach (KeyValuePair<K, V> property in this.dictionary)
            {
                results[i++] = property;
            }

            return i;
        }

        public bool Add(K index, V value)
        {
            return this.dictionary.TryAdd(index, value);
        }

        public bool Remove(K index)
        {
            return this.dictionary.Remove(index);
        }

        public bool Remove(K index, out V removed)
        {
            return this.dictionary.Remove(index, out removed);
        }
    }
}