using System;

namespace Atomic
{
    internal sealed class ValueIndexInfo
    {
        public readonly int index;
        public readonly Func<object, object> valueFunc;

        internal ValueIndexInfo(int index, Func<object, object> valueFunc)
        {
            this.index = index;
            this.valueFunc = valueFunc;
        }
    }
}