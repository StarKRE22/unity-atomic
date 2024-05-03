using System;

namespace AIModule
{
    [Serializable]
    public sealed class BlackboardInt : BlackboardValue<int>
    {
        public override void Apply(IBlackboard blackboard)
        {
            blackboard.SetInt(this.key, this.value);
        }
    }
}