using System.Collections;
using System.Collections.Generic;

namespace Atomic.Objects
{
    internal sealed class TagArray : ITagCollection
    {
        private readonly bool[] values;
        
        public TagArray(int length)
        {
            this.values = new bool[length];
        }

        public bool Contains(int tag)
        {
            return this.values[tag];
        }

        public bool Add(int tag)
        {
            if (this.values[tag])
            {
                return false;
            }

            this.values[tag] = true;
            return true;
        }

        public bool Remove(int tag)
        {
            if (this.values[tag])
            {
                this.values[tag] = false;
                return true;
            }
            
            return false;
        }

        public IEnumerator<int> GetEnumerator()
        {
            for (int i = 0, count = this.values.Length; i < count; i++)
            {
                if (this.values[i])
                {
                    yield return i;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}