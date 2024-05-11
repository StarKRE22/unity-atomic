using System;
using JetBrains.Annotations;

namespace Atomic.Objects
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class BehaviourAttribute : Attribute
    {
    }
}