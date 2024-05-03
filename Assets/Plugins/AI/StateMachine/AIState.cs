using System;
using UnityEngine;

namespace AIModule
{
    [Serializable]
    public class AIState : IAIState
    {
        [SerializeField]
        private string name = "StateName";

        [SerializeField]
        private AIAction[] startActions;

        [SerializeField]
        private AIMechanics[] updateMechanics;
        
        [SerializeField]
        private AIAction[] stopActions;

        public virtual void OnStart(IBlackboard blackboard)
        {
            int count = this.startActions.Length;
            if (count == 0)
            {
                return;
            }
            
            for (int i = 0; i < count; i++)
            {
                AIAction enable = this.startActions[i];
                enable.Perform(blackboard);
            }
        }

        public virtual void OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            int count = this.updateMechanics.Length;
            if (count == 0)
            {
                return;
            }
            
            for (int i = 0; i < count; i++)
            {
                AIMechanics mechanics = this.updateMechanics[i];
                mechanics.OnUpdate(blackboard, deltaTime);
            }
        }

        public virtual void OnStop(IBlackboard blackboard)
        {
            int count = this.stopActions.Length;
            if (count == 0)
            {
                return;
            }
            
            for (int i = 0; i < count; i++)
            {
                AIAction disable = this.stopActions[i];
                disable.Perform(blackboard);
            }
        }
        
        public override string ToString()
        {
            return this.name;
        }
    }
}