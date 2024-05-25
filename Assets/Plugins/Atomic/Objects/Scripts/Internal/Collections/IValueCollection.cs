using System.Collections.Generic;

namespace Atomic.Objects
{
    internal interface IValueCollection : IEnumerable<KeyValuePair<int, object>>
    {
        object this[int id] { get; set; }
        bool TryGetValue(int id, out object value);
        bool ContainsKey(int index);

        bool TryAdd(int key, object value);
        bool Remove(int id);
        bool Remove(int id, out object removed);
    }
}