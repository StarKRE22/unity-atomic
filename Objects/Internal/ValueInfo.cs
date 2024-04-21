using System;

namespace Atomic.Objects
{
    internal sealed class ValueInfo
    {
        internal readonly string id;
        internal readonly Func<object, object> value;

        internal ValueInfo(string id, Func<object, object> value)
        {
            this.id = id;
            this.value = value;
        }
    }
}