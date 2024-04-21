using System;
using JetBrains.Annotations;

namespace Atomic.Objects
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public sealed class IsAttribute : Attribute
    {
        internal readonly string[] Types;

        public IsAttribute(params string[] types)
        {
            this.Types = types;
        }
    }
}