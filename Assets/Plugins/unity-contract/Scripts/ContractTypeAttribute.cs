using System;

namespace Contracts
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter)]
    public sealed class ContractTypeAttribute : Attribute
    {
    }
}