using System.Collections.Generic;

namespace Atomic.Objects
{
    internal sealed class ReferenceDictionary : Dictionary<int, object>, IReferenceCollection
    {
        public ReferenceDictionary(int capacity) : base(capacity)
        {
        }
    }
}