using System;
using UnityEngine;

namespace Modules.AI
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter)]
    public sealed class BlackboardKeyAttribute : PropertyAttribute
    {
    }
}