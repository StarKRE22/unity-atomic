using System;
using Object = UnityEngine.Object;

namespace AIModule
{
    [Serializable]
    public sealed class BlackboardObject : BlackboardValue<Object>
    {
        public override void Apply(IBlackboard blackboard)
        {
            blackboard.SetObject(this.key, this.value);
        }
    }
}