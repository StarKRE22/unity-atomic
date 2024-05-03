using System;
using UnityEngine;

namespace AIModule
{
    [Serializable]
    public sealed class BTNodeDecorator : BTNode, IBTNodeParent
    {
        public override string Name => this.node != null ? this.node.Name : "Undefined";
        
        [SerializeField, Space]
        private AIAction[] enableActions;

        [SerializeField, Space]
        private AIAction[] disableActions;

        [SerializeReference, Space]
        private BTNode node;

        protected override void OnEnable(IBlackboard blackboard)
        {
            for (int i = 0, count = this.enableActions.Length; i < count; i++)
            {
                AIAction action = this.enableActions[i];
                action.Perform(blackboard);
            }
        }

        protected override void OnAbort(IBlackboard blackboard)
        {
            this.node.Abort(blackboard);
        }

        protected override BTState OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            return this.node.Run(blackboard, deltaTime);
        }

        protected override void OnDisable(IBlackboard blackboard)
        {
            for (int i = 0, count = this.disableActions.Length; i < count; i++)
            {
                AIAction action = this.disableActions[i];
                action.Perform(blackboard);
            }
        }
        
        public bool FindChild(string name, out BTNode result)
        {
            if (name.Equals(this.node.Name))
            {
                result = this.node;
                return true;
            }

            if (this.node is IBTNodeParent parent && parent.FindChild(name, out result))
            {
                return true;
            }

            result = default;
            return false;
        }
    }
}