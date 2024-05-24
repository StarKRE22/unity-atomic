using System;

namespace Atomic.Objects
{
    internal sealed class LogicInfo
    {
        public readonly Func<object, object> valueFunc;

        public LogicInfo(Func<object, object> valueFunc)
        {
            this.valueFunc = valueFunc;
        }
    }
}