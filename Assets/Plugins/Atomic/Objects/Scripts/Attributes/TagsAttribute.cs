using System;
using JetBrains.Annotations;

namespace Atomic.Objects
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public sealed class TagsAttribute : Attribute
    {
        internal readonly int[] typeIds;

        public TagsAttribute(params int[] typeIds)
        {
            this.typeIds = typeIds;
        }
    }
}