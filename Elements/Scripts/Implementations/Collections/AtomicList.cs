using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Elements
{
    [Serializable]
    public sealed class AtomicList<T> : IAtomicList<T>
    {
        public event ChangeItemHandler<T> OnItemChanged;
        public event AddItemHandler<T> OnItemInserted;
        public event RemoveItemHandler<T> OnItemDeleted;
        public event ClearHandler OnCleared;

        [SerializeField]
        private List<T> list;

        public bool IsReadOnly => false;
        public int Count => this.list.Count;
        
        public AtomicList()
        {
        }

        public AtomicList(int capacity = 0)
        {
            this.list = new List<T>(capacity);
        }

        public T this[int index]
        {
            get { return this.list[index]; }
            set
            {
                this.list[index] = value;
                this.OnItemChanged?.Invoke(index, value);
            }
        }
        
        public void Add(T item)
        {
            this.list.Add(item);
            this.OnItemInserted?.Invoke(this.list.Count - 1, item);
        }

        public void Clear()
        {
            this.list.Clear();
            this.OnCleared?.Invoke();
        }

        public bool Contains(T item)
        {
            return this.list.Contains(item);
        }

        public bool Remove(T item)
        {
            int index = this.list.IndexOf(item);
            if (index < 0)
            {
                return false;
            }

            this.list.RemoveAt(index);
            this.OnItemDeleted?.Invoke(index, item);
            return true;
        }
        
        public void RemoveAt(int index)
        {
            T item = this.list[index];
            this.list.RemoveAt(index);
            this.OnItemDeleted?.Invoke(index, item);
        }

        public void Insert(int index, T item)
        {
            this.list.Insert(index, item);
            this.OnItemInserted?.Invoke(index, item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            for (int i = arrayIndex, count = this.list.Count; i < count; i++)
            {
                array[i] = this.list[i];
            }
        }
        
        public int IndexOf(T item)
        {
            return this.list.IndexOf(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}