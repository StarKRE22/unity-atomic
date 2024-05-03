using System;
using JetBrains.Annotations;

namespace Atomic
{
    //Для Fusion
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class AtomicGetterAttribute : Attribute
    {
        internal readonly string key;
        internal readonly int index;

        public AtomicGetterAttribute(string key, int index = -1)
        {
            this.key = key;
            this.index = index;
        }
        
        public AtomicGetterAttribute(int index, string key = null)
        {
            this.key = key;
            this.index = index;
        }
    }
}