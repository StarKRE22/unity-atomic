using System;
using UnityEngine;

namespace AIModule
{
    [Serializable]
    public sealed class BlackboardTransform : BlackboardValue<Transform>
    {
        public override void Apply(IBlackboard blackboard)
        {
            blackboard.SetObject(this.key, this.value);
        }
    }
}