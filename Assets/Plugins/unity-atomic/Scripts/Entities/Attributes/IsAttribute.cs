using System;
using JetBrains.Annotations;

namespace Atomic
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class IsAttribute : Attribute
    {
        internal readonly int[] indexes;
        
        public IsAttribute(params int[] indexes)
        {
            this.indexes = indexes;
        }
    }
}