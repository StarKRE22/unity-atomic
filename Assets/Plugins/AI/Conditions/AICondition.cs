using UnityEngine;

namespace AIModule
{
    public abstract class AICondition : ScriptableObject
    {
        public abstract bool Check(IBlackboard blackboard);
    }
}