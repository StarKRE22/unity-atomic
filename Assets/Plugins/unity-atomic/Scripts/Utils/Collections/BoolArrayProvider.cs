using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Atomic
{
    internal sealed class BoolArrayProvider : ISetProvider<int>
    {
        [ShowInInspector, ReadOnly]
        private readonly bool[] indexes;

        public BoolArrayProvider(int length)
        {
            this.indexes = new bool[length];
        }

        public bool Contains(int value)
        {
            return this.indexes[value];
        }

        public List<int> GetValues()
        {
            List<int> result = new List<int>();
            for (int i = 0, count = this.indexes.Length; i < count; i++)
            {
                if (this.indexes[i])
                {
                    result.Add(i);
                }
            }

            return result;
        }

        public int GetValuesNonAlloc(int[] results)
        {
            int count = 0;
            
            for (int i = 0, length = this.indexes.Length; i < length; i++)
            {
                if (this.indexes[i])
                {
                    results[count++] = i;
                }
            }

            return count;
        }

        public bool Add(int value)
        {
            if (this.indexes[value])
            {
                return false;
            }

            this.indexes[value] = true;
            return true;
        }

        public bool Remove(int value)
        {
            if (this.indexes[value])
            {
                this.indexes[value] = false;
                return true;
            }
            
            return false;
        }

        public void UnionWith(IEnumerable<int> values)
        {
            foreach (int value in values)
            {
                if (!this.indexes[value])
                {
                    this.indexes[value] = true;
                }
            }
        }
    }
}