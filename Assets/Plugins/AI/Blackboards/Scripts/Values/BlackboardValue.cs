using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AIModule
{
    [Serializable, InlineProperty]
    public abstract class BlackboardValue<T> : IBlackboardValue
    {
        [SerializeField, BlackboardKey]
        protected ushort key;

        [SerializeField]
        protected T value;

        public abstract void Apply(IBlackboard blackboard);
    }
}