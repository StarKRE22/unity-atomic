using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Atomic
{
    internal sealed class HashSetProvider<T> : ISetProvider<T>
    {
        [ShowInInspector, ReadOnly]
        private readonly HashSet<T> hashSet = new();

        public bool Contains(T value)
        {
            return this.hashSet.Contains(value);
        }

        public List<T> GetValues()
        {
            return new List<T>(this.hashSet);
        }

        public int GetValuesNonAlloc(T[] results)
        {
            int i = 0;

            foreach (var type in results)
            {
                results[i++] = type;
            }

            return i;
        }

        public bool Add(T value)
        {
            return this.hashSet.Add(value);
        }

        public bool Remove(T value)
        {
            return this.hashSet.Remove(value);
        }

        public void UnionWith(IEnumerable<T> values)
        {
            this.hashSet.UnionWith(values);
        }
    }
}