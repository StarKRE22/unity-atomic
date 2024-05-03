using System;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AIModule
{
    [Serializable, InlineProperty, LabelWidth(1)]
    public sealed class AIConditionRef : IAICondition
    {
        [SerializeField, HideLabel]
        private AICondition value;

        public AIConditionRef()
        {
        }

        public AIConditionRef(AICondition value)
        {
            this.value = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Check(IBlackboard blackboard)
        {
            return this.value.Check(blackboard);
        }
    }
}