using System;

namespace Atomic.Objects
{
    internal sealed class BehaviourInfo
    {
        public readonly Func<object, object> valueFunc;

        public BehaviourInfo(Func<object, object> valueFunc)
        {
            this.valueFunc = valueFunc;
        }
    }
}