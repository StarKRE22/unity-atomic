using System.Collections.Generic;

namespace Atomic
{
    internal interface ISetProvider<T>
    {
        bool Contains(T value);
        
        List<T> GetValues();
        int GetValuesNonAlloc(T[] results);

        bool Add(T value);
        bool Remove(T value);

        void UnionWith(IEnumerable<T> values);
    }
}