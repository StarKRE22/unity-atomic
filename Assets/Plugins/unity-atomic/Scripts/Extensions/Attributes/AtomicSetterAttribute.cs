using System;

namespace Atomic
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class AtomicSetterAttribute : Attribute
    {
        internal readonly int index;

        public AtomicSetterAttribute(int index)
        {
            this.index = index;
        }
    }
}