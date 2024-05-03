using System;
using Sirenix.OdinInspector;

namespace AIModule
{
    [Serializable, InlineProperty]
    public sealed class BlackboardBool : BlackboardValue<bool>
    {
        public override void Apply(IBlackboard blackboard)
        {
            blackboard.SetBool(this.key, this.value);
        }
    }
}