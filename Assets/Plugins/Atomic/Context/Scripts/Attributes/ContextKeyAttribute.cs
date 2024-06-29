using System;

namespace Atomic.Contexts
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ContextKeyAttribute : Attribute
    {
    }
}