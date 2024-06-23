using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Elements
{
    [Serializable]
    public class AtomicSortedList<K, V> : IAtomicDictionary<K, V>, ISerializationCallbackReceiver where K : IComparable<K>
    {
        public event StateChangedHandler OnStateChanged;
        public event SetItemHandler<K, V> OnItemChanged;
        public event AddItemHandler<K, V> OnItemAdded;
        public event RemoveItemHandler<K, V> OnItemRemoved;
        public event ClearHandler OnCleared;

        [Serializable]
        private struct Pair
        {
            public K key;
            public V value;
        }
        
        [SerializeField]
        private Pair[] pairs;

        private SortedList<K, V> dictionary;
        
        public ICollection<K> Keys => this.dictionary.Keys;
        public ICollection<V> Values => this.dictionary.Values;
        public int Count => this.dictionary.Count;
        public bool IsReadOnly => false;

        public AtomicSortedList()
        {
            this.dictionary = new SortedList<K, V>();
        }

        public AtomicSortedList(int capacity = 0)
        {
            this.dictionary = new SortedList<K, V>(capacity);
        }
        
        public V this[K key]
        {
            get => this.dictionary[key];
            set
            {
                this.dictionary[key] = value;
                this.OnItemChanged?.Invoke(key, value);
                this.OnStateChanged?.Invoke();
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
        
        public void Add(K key, V value)
        {
            if (this.dictionary.TryAdd(key, value))
            {
                this.OnItemAdded?.Invoke(key, value);
                this.OnStateChanged?.Invoke();
            }
            else
            {
                throw new Exception($"Item with key {key} is already exists!");
            }
        }

        public bool Remove(K key)
        {
            if (!this.dictionary.Remove(key, out V value))
            {
                return false;
            }

            this.OnItemRemoved?.Invoke(key, value);
            this.OnStateChanged?.Invoke();
            return true;

        }

        public bool ContainsKey(K key)
        {
            return this.dictionary.ContainsKey(key);
        }

        public bool TryGetValue(K key, out V value)
        {
            return this.dictionary.TryGetValue(key, out value);
        }

        public void Clear()
        {
            if (this.dictionary.Count > 0)
            {
                this.dictionary.Clear();
                this.OnCleared?.Invoke();
                this.OnStateChanged?.Invoke();
            }
        }
        
        void ICollection<KeyValuePair<K, V>>.Add(KeyValuePair<K, V> item)
        {
            (K key, V value) = item;
            this.Add(key, value);
        }

        bool ICollection<KeyValuePair<K, V>>.Contains(KeyValuePair<K, V> item)
        {
            return ((ICollection<KeyValuePair<K, V>>) this.dictionary).Contains(item);
        }

        void ICollection<KeyValuePair<K, V>>.CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<K, V>>) this.dictionary).CopyTo(array, arrayIndex);
        }

        bool ICollection<KeyValuePair<K, V>>.Remove(KeyValuePair<K, V> item)
        {
            if (((ICollection<KeyValuePair<K, V>>) this.dictionary).Remove(item))
            {
                this.OnItemRemoved?.Invoke(item.Key, item.Value);
                return true;
            }

            return false;
        }
        
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            this.dictionary = new SortedList<K, V>();

            for (int i = 0, count = this.pairs.Length; i < count; i++)
            {
                Pair pair = this.pairs[i];
                this.dictionary[pair.key] = pair.value;
            }
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
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