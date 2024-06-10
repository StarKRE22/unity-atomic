using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Elements
{
    [Serializable]
    public sealed class AtomicDictionary<K, V> : IAtomicDictionary<K, V>, ISerializationCallbackReceiver
    {
        public event ChangeItemHandler<K, V> OnItemChanged;
        public event AddItemHandler<K, V> OnItemAdded;
        public event RemoveItemHandler<K, V> OnItemRemoved;

        [SerializeField]
        private Pair[] pairs;

        private Dictionary<K, V> dictionary;

        public AtomicDictionary()
        {
        }

        public AtomicDictionary(int capacity = 0)
        {
            this.dictionary = new Dictionary<K, V>(capacity);
        }

        public ICollection<K> Keys => this.dictionary.Keys;
        public ICollection<V> Values => this.dictionary.Values;
        public int Count => this.dictionary.Count;
        public bool IsReadOnly => false;
        
        public V this[K key]
        {
            get => this.dictionary[key];
            set
            {
                this.dictionary[key] = value;
                this.OnItemChanged?.Invoke(key, value);
            }
        }

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            return this.dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Add(KeyValuePair<K, V> item)
        {
            throw new System.NotImplementedException();
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public bool Contains(KeyValuePair<K, V> item)
        {
            throw new System.NotImplementedException();
        }

        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(KeyValuePair<K, V> item)
        {
            throw new System.NotImplementedException();
        }


        public void Add(K key, V value)
        {
            throw new System.NotImplementedException();
        }

        public bool ContainsKey(K key)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(K key)
        {
            throw new System.NotImplementedException();
        }

        public bool TryGetValue(K key, out V value)
        {
            throw new System.NotImplementedException();
        }


        [Serializable]
        private struct Pair
        {
            public K key;
            public V value;
        }

        public void OnAfterDeserialize()
        {
            this.dictionary = new Dictionary<K, V>();

            for (int i = 0, count = this.pairs.Length; i < count; i++)
            {
                Pair pair = this.pairs[i];
                this.dictionary[pair.key] = pair.value;
            }
        }

        public void OnBeforeSerialize()
        {
            this.pairs = new Pair[this.dictionary.Count];

            int i = 0;
            foreach (var (key, value) in this.dictionary)
            {
                this.pairs[i++] = new Pair
                {
                    key = key,
                    value = value
                };
            }
        }
    }
}