using System;

namespace Atomic.Objects
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter)]
    public sealed class TagIdAttribute : Attribute
    {
    }
}