using UnityEngine;

namespace AIModule
{
    public abstract class AIAction : ScriptableObject
    {
        public abstract void Perform(IBlackboard blackboard);
    }
}