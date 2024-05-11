using System.Collections.Generic;

namespace Atomic.Objects
{
    internal sealed class TagHashSet : HashSet<int>, ITagCollection
    {
        public TagHashSet(int capacity) : base(capacity)
        {
        }
    }
}