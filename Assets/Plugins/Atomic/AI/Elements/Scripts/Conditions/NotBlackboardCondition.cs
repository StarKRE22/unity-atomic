using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Modules.AI
{
    [MovedFrom(true, null, null, "AIConditionNot")] 
    [Serializable, InlineProperty, LabelWidth(1)]
    public sealed class NotBlackboardCondition : IBlackboardCondition
    {
        [SerializeReference, HideLabel]
        private IBlackboardCondition condition = default;
        
        public bool Invoke(IBlackboard blackboard)
        {
            if (this.condition == null)
            {
                return false;
            }
            
            return !this.condition.Invoke(blackboard);
        }
    }
}