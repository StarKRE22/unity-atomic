using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Elements
{
    [Serializable]
    public class AtomicList<T> : IAtomicList<T>
    {
        public event StateChangedHandler OnStateChanged;
        public event ChangeItemHandler<T> OnItemChanged;
        public event InsertItemHandler<T> OnItemInserted;
        public event DeleteItemHandler<T> OnItemDeleted;
        public event ClearHandler OnCleared;

        [SerializeField]
        private List<T> list;

        public bool IsReadOnly => false;
        public int Count => this.list.Count;
        
        public AtomicList()
        {
            this.list = new List<T>();
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
                this.OnStateChanged?.Invoke();
            }
        }
        
        public void Add(T item)
        {
            this.list.Add(item);
            this.OnItemInserted?.Invoke(this.list.Count - 1, item);
            this.OnStateChanged?.Invoke();
        }

        public void Clear()
        {
            if (this.list.Count > 0)
            {
                this.list.Clear();
                this.OnCleared?.Invoke();
                this.OnStateChanged?.Invoke();    
            }
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
            this.OnStateChanged?.Invoke();
            return true;
        }
        
        public void RemoveAt(int index)
        {
            if (index >= 0 && index < this.list.Count)
            {
                T item = this.list[index];
                this.list.RemoveAt(index);
                this.OnItemDeleted?.Invoke(index, item);
                this.OnStateChanged?.Invoke();
            }
        }

        public void Insert(int index, T item)
        {
            this.list.Insert(index, item);
            this.OnItemInserted?.Invoke(index, item);
            this.OnStateChanged?.Invoke();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.list.CopyTo(array, arrayIndex);
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