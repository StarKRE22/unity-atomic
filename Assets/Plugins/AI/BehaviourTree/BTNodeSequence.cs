using System;

namespace AIModule
{
    [Serializable]
    public sealed class BTNodeSequence : BTNodeComposite
    {
        protected override BTState OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            BTNode currentNode = this.nodes[this.nodeIndex];
            BTState result = currentNode.Run(blackboard, deltaTime);

            if (result != BTState.SUCCESS)
            {
                return result;
            }

            //Success:
            if (this.nodeIndex == this.nodes.Length - 1)
            {
                return BTState.SUCCESS;
            }

            this.nodeIndex++;
            return BTState.RUNNING;
        }
    }
}