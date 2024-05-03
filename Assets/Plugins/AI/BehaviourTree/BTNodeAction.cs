using System;
using UnityEngine;

namespace AIModule
{
    [Serializable]
    public sealed class BTNodeAction : BTNode
    {
        public override string Name => this.name;

        [SerializeField]
        private string name;

        [SerializeReference]
        private AIAction action;

        protected override BTState OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            this.action.Perform(blackboard);
            return BTState.SUCCESS;
        }
    }
}