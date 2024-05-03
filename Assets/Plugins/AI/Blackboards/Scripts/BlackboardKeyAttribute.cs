using System;
using UnityEngine;

namespace AIModule
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter)]
    public sealed class BlackboardKeyAttribute : PropertyAttribute
    {
    }
}