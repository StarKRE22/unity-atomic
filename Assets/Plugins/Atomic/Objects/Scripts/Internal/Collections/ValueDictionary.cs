using System.Collections.Generic;

namespace Atomic.Objects
{
    internal sealed class ValueDictionary : Dictionary<int, object>, IValueCollection
    {
        public ValueDictionary(int capacity) : base(capacity)
        {
        }
    }
}