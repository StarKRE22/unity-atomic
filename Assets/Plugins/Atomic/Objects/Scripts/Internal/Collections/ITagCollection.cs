using System.Collections.Generic;

namespace Atomic.Objects
{
    internal interface ITagCollection : IEnumerable<int>
    {
        bool Contains(int tag);
        bool Add(int tag);
        bool Remove(int tag);
    }
}