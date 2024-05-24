using System;
using JetBrains.Annotations;

namespace Atomic.Objects
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class ValueAttribute : Attribute
    {
        internal readonly int id;
        
        public ValueAttribute(int id)
        {
            this.id = id;
        }
    }
}