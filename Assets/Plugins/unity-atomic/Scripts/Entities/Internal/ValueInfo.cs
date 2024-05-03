using System;

namespace Atomic
{
    internal sealed class ValueInfo
    {
        public readonly int index;
        public readonly Func<object, object> valueFunc;

        internal ValueInfo(int index, Func<object, object> valueFunc)
        {
            this.index = index;
            this.valueFunc = valueFunc;
        }
    }
}