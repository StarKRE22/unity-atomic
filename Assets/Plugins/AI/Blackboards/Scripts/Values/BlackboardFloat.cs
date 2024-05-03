using System;

namespace AIModule
{
    [Serializable]
    public sealed class BlackboardFloat : BlackboardValue<float>
    {
        public override void Apply(IBlackboard blackboard)
        {
            blackboard.SetFloat(this.key, this.value);
        }
    }
}