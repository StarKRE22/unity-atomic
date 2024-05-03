using System;

namespace Atomic
{
    internal sealed class ValueKeyInfo
    {
        public readonly string key;
        public readonly Func<object, object> valueFunc;

        internal ValueKeyInfo(string key, Func<object, object> valueFunc)
        {
            this.key = key;
            this.valueFunc = valueFunc;
        }
    }
}