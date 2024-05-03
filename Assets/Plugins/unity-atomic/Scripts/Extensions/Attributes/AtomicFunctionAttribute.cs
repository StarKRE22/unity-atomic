using System;

namespace Atomic
{
    //Для Fusion
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class AtomicFunctionAttribute : Attribute
    {
        internal readonly string key;
        internal readonly int index;

        public AtomicFunctionAttribute(string key, int index = -1)
        {
            this.key = key;
            this.index = index;
        }
        
        public AtomicFunctionAttribute(int index, string key = null)
        {
            this.key = key;
            this.index = index;
        }
    }
}