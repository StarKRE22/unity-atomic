using System;
using JetBrains.Annotations;

namespace Atomic.Objects
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class ReferenceAttribute : Attribute
    {
        internal readonly int id;
        
        public ReferenceAttribute(int id)
        {
            this.id = id;
        }
    }
}