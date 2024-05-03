using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIModule
{
    [Serializable]
    public sealed class BehaviourTree : IAIUpdatable, IAIStoppable, IBTNodeParent
    {
        [SerializeReference]
        private BTNode root;
        
        public void OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            this.root.Run(blackboard, deltaTime);
        }

        public void OnStop(IBlackboard blackboard)
        {
            this.root.Abort(blackboard);
        }

        public bool FindChild(string name, out BTNode result)
        {
            if (string.IsNullOrEmpty(name))
            {
                result = default;
                return false;
            }

            if (name.Equals(this.root.Name))
            {
                result = this.root;
                return true;
            }

            if (this.root is not IBTNodeParent parent)
            {
                result = default;
                return false;
            }
            
            return parent.FindChild(name, out result);
        }
    }
}