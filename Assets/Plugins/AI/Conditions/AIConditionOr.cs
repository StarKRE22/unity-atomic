using System;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AIModule
{
    [Serializable, InlineProperty, LabelWidth(1)]
    public sealed class AIConditionOr : IAICondition
    {
        [SerializeReference, HideLabel]
        private IAICondition[] conditions;

        public AIConditionOr()
        {
        }

        public AIConditionOr(IAICondition[] conditions)
        {
            this.conditions = conditions;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Check(IBlackboard blackboard)
        {
            for (int i = 0, count = this.conditions.Length; i < count; i++)
            {
                IAICondition condition = this.conditions[i];
                if (condition.Check(blackboard))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
