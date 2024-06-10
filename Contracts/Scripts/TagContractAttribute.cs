using System;

namespace Atomic.Contracts
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter)]
    public sealed class TagContractAttribute : Attribute
    {
    }
}