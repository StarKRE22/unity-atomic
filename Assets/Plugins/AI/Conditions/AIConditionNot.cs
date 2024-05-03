using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AIModule
{
    [Serializable, InlineProperty, LabelWidth(1)]
    public sealed class AIConditionNot : IAICondition
    {
        [SerializeReference]
        private IAICondition condition;
        
        public bool Check(IBlackboard blackboard)
        {
            return !this.condition.Check(blackboard);
        }
    }
}