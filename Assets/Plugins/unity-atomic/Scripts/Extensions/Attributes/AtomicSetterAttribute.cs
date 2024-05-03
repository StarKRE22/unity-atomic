using System;

namespace Atomic
{
    //Для Fusion
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class AtomicSetterAttribute : Attribute
    {
        internal readonly string key;
        internal readonly int index;

        public AtomicSetterAttribute(string key, int index = -1)
        {
            this.key = key;
            this.index = index;
        }
        
        public AtomicSetterAttribute(int index, string key = null)
        {
            this.key = key;
            this.index = index;
        }
    }
}