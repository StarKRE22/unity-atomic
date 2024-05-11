using System;

namespace Atomic.Objects
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ContractAttribute : Attribute
    {
        public ContractAttribute(Type type)
        {
        }
    }
}