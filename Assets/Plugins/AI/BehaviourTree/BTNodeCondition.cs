using System;
using UnityEngine;

namespace AIModule
{
    [Serializable]
    public sealed class BTNodeCondition : BTNode
    {
        public override string Name => this.name;

        [SerializeField]
        private string name;
        
        [SerializeReference]
        private IAICondition condition = default;
        
        protected override BTState OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            return this.condition.Check(blackboard) ? BTState.SUCCESS : BTState.FAILURE;
        }
    }
}