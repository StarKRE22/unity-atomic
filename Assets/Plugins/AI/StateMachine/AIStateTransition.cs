using System;
using Sirenix.OdinInspector;
using UnityEngine;

// ReSharper disable UnassignedField.Local

namespace AIModule
{
    //TODO: написать Editor для HFSM
    [Serializable]
    internal sealed class AIStateTransition
    {
        [HorizontalGroup]
        [ValueDropdown(nameof(DrawStateNames))]
        [LabelText("From")]
        [SerializeField]
        internal int sourceState;

        [HorizontalGroup]
        [ValueDropdown(nameof(DrawStateNames))]
        [LabelText("    To")]
        [SerializeField]
        internal int targetState;

        [SerializeReference, Space]
        private IAICondition condition = default;

        [SerializeField, Space]
        private AIAction[] actions;

        internal bool Check(IBlackboard blackboard)
        {
            return this.condition.Check(blackboard);
        }

        internal void Perform(IBlackboard blackboard)
        {
            int count = this.actions.Length;
            if (count == 0)
            {
                return;
            }
            
            for (int i = 0; i < count; i++)
            {
                AIAction action = this.actions[i];
                action.Perform(blackboard);
            }
        }

#if UNITY_EDITOR
        
        internal Func<ValueDropdownList<int>> _drawCallback;
        
        internal ValueDropdownList<int> DrawStateNames()
        {
            return _drawCallback?.Invoke();
        }
#endif
    }
}