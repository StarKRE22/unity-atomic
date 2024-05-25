using System.Collections;
using System.Collections.Generic;

namespace Atomic.Objects
{
    internal sealed class ValueArray : IValueCollection 
    {
        private readonly object[] values;

        public ValueArray(int length)
        {
            this.values = new object[length];
        }

        public object this[int index]
        {
            get { return this.values[index]; }
            set { this.values[index] = value; }
        }

        public bool TryGetValue(int index, out object result)
        {
            result = this.values[index];
            return result != null;
        }

        public bool TryAdd(int index, object value)
        {
            if (this.values[index] != null)
            {
                return false;
            }

            this.values[index] = value;
            return true;
        }

        public bool Remove(int index)
        {
            if (this.values[index] == null)
            {
                return false;
            }

            this.values[index] = null;
            return true;
        }

        public bool Remove(int index, out object removed)
        {
            removed = this.values[index]; 
            if (removed == null)
            {
                removed = null;
                return false;
            }

            this.values[index] = null;
            return true;
        }

        public bool ContainsKey(int index)
        {
            return this.values[index] != null;
        }

        public IEnumerator<KeyValuePair<int, object>> GetEnumerator()
        {
            for (int i = 0, length = this.values.Length; i < length; i++)
            {
                object value = this.values[i];
                if (value != null)
                {
                    yield return new KeyValuePair<int, object>(i, value); 
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}