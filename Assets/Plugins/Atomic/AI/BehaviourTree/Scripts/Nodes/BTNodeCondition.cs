using System;
using UnityEngine;

namespace Modules.AI
{
    [Serializable]
    public sealed class BTNodeCondition : BTNode
    {
        public override string Name => this.name;

        [SerializeField]
        private string name;
        
        [SerializeReference]
        private IBlackboardCondition condition = default;
        
        protected override BTState OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            return this.condition.Invoke(blackboard) ? BTState.SUCCESS : BTState.FAILURE;
        }
    }
}