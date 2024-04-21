using System;
using JetBrains.Annotations;

namespace Atomic.Objects
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class GetAttribute : Attribute
    {
        internal readonly string Id;

        public GetAttribute(string Id)
        {
            this.Id = Id;
        }
    }
}