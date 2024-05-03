using System;
using JetBrains.Annotations;

namespace Atomic
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class GetAttribute : Attribute
    {
        internal readonly int index;

        public GetAttribute(int index)
        {
            this.index = index;
        }
    }
}