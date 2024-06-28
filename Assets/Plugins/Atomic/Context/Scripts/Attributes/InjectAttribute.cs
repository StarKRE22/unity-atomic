using System;
using JetBrains.Annotations;

namespace Atomic.Contexts
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
    public sealed class InjectAttribute : Attribute
    {
        public readonly int id;

        public InjectAttribute(int id)
        {
            this.id = id;
        }
    }
}