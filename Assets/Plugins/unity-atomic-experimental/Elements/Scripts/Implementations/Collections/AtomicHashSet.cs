using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Elements
{
    [Serializable]
    public class AtomicHashSet<T> : IAtomicSet<T>, ISerializationCallbackReceiver
    {
        public event StateChangedHandler OnStateChanged;
        public event AddItemHandler<T> OnItemAdded;
        public event RemoveItemHandler<T> OnItemRemoved;
        public event ClearHandler OnCleared;

        public int Count => this.set.Count;
        public bool IsReadOnly => ((ICollection<T>) this.set).IsReadOnly;

        private HashSet<T> set;

        [SerializeField]
        private List<T> items;

        public AtomicHashSet()
        {
            this.set = new HashSet<T>();
        }

        public AtomicHashSet(IEnumerable<T> elements)
        {
            this.set = new HashSet<T>(elements);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.set.GetEnumerator();
        }
        
        public bool Add(T item)
        {
            if (this.set.Add(item))
            {
                this.OnItemAdded?.Invoke(item);
                return true;
            }
            
            return false;
        }
        
        public bool Remove(T item)
        {
            if (this.set.Remove(item))
            {
                this.OnItemRemoved?.Invoke(item);
                return true;
            }

            return false;
        }
        
        public bool Contains(T item)
        {
            return this.set.Contains(item);
        }
        
        public void ExceptWith(IEnumerable<T> other)
        {
            if (this.set.Count > 0)
            {
                this.set.ExceptWith(other);
                this.OnStateChanged?.Invoke();
            }
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            if (this.set.Count > 0)
            {
                this.set.IntersectWith(other);
                this.OnStateChanged?.Invoke();
            }
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            return this.set.IsProperSubsetOf(other);
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            return this.set.IsProperSupersetOf(other);
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            return this.set.IsSubsetOf(other);
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            return this.set.IsSupersetOf(other);
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            return this.set.Overlaps(other);
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            return this.set.SetEquals(other);
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            this.set.SymmetricExceptWith(other);
            this.OnStateChanged?.Invoke();
        }

        public void UnionWith(IEnumerable<T> other)
        {
            this.set.UnionWith(other);
            this.OnStateChanged?.Invoke();
        }

        public void Clear()
        {
            if (this.set.Count > 0)
            {
                this.set.Clear();
                this.OnCleared?.Invoke();
            }
        }
        
        public void CopyTo(T[] array, int arrayIndex)
        {
            this.set.CopyTo(array, arrayIndex);
        }

        void ICollection<T>.Add(T item)
        {
            if (this.set.Add(item))
            {
                this.OnItemAdded?.Invoke(item);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            this.set = new HashSet<T>(this.items);
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            this.items = new List<T>(this.set);
        }
    }
}