using System;

namespace Atomic
{
    //Для Fusion
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class AtomicFunctionAttribute : Attribute
    {
        internal readonly int index;

        public AtomicFunctionAttribute(int index)
        {
            this.index = index;
        }
    }
}