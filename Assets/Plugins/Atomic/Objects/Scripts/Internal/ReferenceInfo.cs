using System;

namespace Atomic.Objects
{
    internal sealed class ReferenceInfo
    {
        public readonly int index;
        public readonly Func<object, object> valueFunc;

        internal ReferenceInfo(int index, Func<object, object> valueFunc)
        {
            this.index = index;
            this.valueFunc = valueFunc;
        }
    }
}