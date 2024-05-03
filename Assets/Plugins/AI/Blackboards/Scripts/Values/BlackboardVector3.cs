using System;
using UnityEngine;

namespace AIModule
{
    [Serializable]
    public sealed class BlackboardVector3 : BlackboardValue<Vector3>
    {
        public override void Apply(IBlackboard blackboard)
        {
            blackboard.SetVector3(this.key, this.value);
        }
    }
}