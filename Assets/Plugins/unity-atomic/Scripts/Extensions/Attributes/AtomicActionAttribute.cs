using System;

namespace Atomic
{
    //Для Fusion
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class AtomicActionAttribute : Attribute
    {
        internal readonly string key;
        internal readonly int index;

        public AtomicActionAttribute(string key, int index = -1)
        {
            this.key = key;
            this.index = index;
        }
        
        public AtomicActionAttribute(int index, string key = null)
        {
            this.key = key;
            this.index = index;
        }
    }
}