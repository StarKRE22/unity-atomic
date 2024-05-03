using System;
using Sirenix.OdinInspector;

namespace AIModule
{
    [Serializable]
    public abstract class BTNode
    {
        public virtual string Name => this.GetType().Name;

        [ShowInInspector, ReadOnly]
        private bool enable;

        internal BTState Run(IBlackboard blackboard, float deltaTime)
        {
            if (!this.enable)
            {
                this.enable = true;
                this.OnEnable(blackboard);
            }

            BTState result = this.OnUpdate(blackboard, deltaTime);

            if (result != BTState.RUNNING)
            {
                this.enable = false;
                this.OnDisable(blackboard);
            }

            return result;
        }

        internal void Abort(IBlackboard blackboard)
        {
            if (this.enable)
            {
                this.enable = false;
                this.OnAbort(blackboard);
                this.OnDisable(blackboard);
            }
        }

        protected abstract BTState OnUpdate(IBlackboard blackboard, float deltaTime);
        
        protected virtual void OnEnable(IBlackboard blackboard)
        {
        }

        protected virtual void OnDisable(IBlackboard blackboard)
        {
        }

        protected virtual void OnAbort(IBlackboard blackboard)
        {
        }
    }
}