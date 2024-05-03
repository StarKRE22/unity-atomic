using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Atomic
{
    internal sealed class ObjectArrayProvider : IMapProvider<int, object>
    {
        [ShowInInspector, ReadOnly]
        private readonly object[] values;

        public ObjectArrayProvider(int length)
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

        public List<KeyValuePair<int, object>> GetPairs()
        {
            var result = new List<KeyValuePair<int, object>>();

            for (int i = 0, length = this.values.Length; i < length; i++)
            {
                object value = this.values[i];
                if (value != null)
                {
                    result.Add(new KeyValuePair<int, object>(i, value));
                }
            }

            return result;
        }

        public int GetPairsNonAlloc(KeyValuePair<int, object>[] results)
        {
            int count = 0;

            for (int i = 0, length = this.values.Length; i < length; i++)
            {
                object value = this.values[i];
                if (value != null)
                {
                    results[count++] = new KeyValuePair<int, object>(i, value);
                }
            }

            return count;
        }

        public bool Add(int index, object value)
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
    }
}