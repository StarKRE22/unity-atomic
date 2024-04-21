using System.Collections.Generic;

namespace Atomic.Objects
{
    internal sealed class AtomicEntityInfo
    {
        internal readonly IEnumerable<string> types;
        internal readonly IEnumerable<ValueInfo> values;

        internal AtomicEntityInfo(IEnumerable<string> types, IEnumerable<ValueInfo> values)
        {
            this.types = types;
            this.values = values;
        }
    }
}