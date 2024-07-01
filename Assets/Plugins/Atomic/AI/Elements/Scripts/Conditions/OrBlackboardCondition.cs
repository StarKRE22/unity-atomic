using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Modules.AI
{
    [MovedFrom(true, null, null, "AIConditionOr")] 
    [Serializable, InlineProperty, LabelWidth(1)]
    public sealed class OrBlackboardCondition : IBlackboardCondition
    {
        [SerializeReference, HideLabel]
        private IBlackboardCondition[] conditions;

        public OrBlackboardCondition()
        {
        }

        public OrBlackboardCondition(IBlackboardCondition[] conditions)
        {
            this.conditions = conditions;
        }

        public bool Invoke(IBlackboard blackboard)
        {
            if (this.conditions == null)
            {
                return false;
            }
            
            for (int i = 0, count = this.conditions.Length; i < count; i++)
            {
                IBlackboardCondition condition = this.conditions[i];
                
                if (condition.Invoke(blackboard))
                {
                    return true;
                }
            }

            return false;
        }
    }
}