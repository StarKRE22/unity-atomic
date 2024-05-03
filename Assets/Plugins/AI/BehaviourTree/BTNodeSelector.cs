using System;

namespace AIModule
{
    [Serializable]
    public sealed class BTNodeSelector : BTNodeComposite
    {
        protected override BTState OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            BTNode currentNode = this.nodes[this.nodeIndex];
            BTState result = currentNode.Run(blackboard, deltaTime);
            
            if (result != BTState.FAILURE)
            {
                return result;
            }

            //Failure:
            if (this.nodeIndex == this.nodes.Length - 1)
            {
                return BTState.FAILURE;
            }

            this.nodeIndex++;
            return BTState.RUNNING;
        }
    }
}