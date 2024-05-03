using System;
using UnityEngine;

namespace AIModule
{
    [Serializable]
    public sealed class BlackboardQuaternion : BlackboardValue<Quaternion>
    {
        public override void Apply(IBlackboard blackboard)
        {
            blackboard.SetQuaternion(this.key, this.value);
        }
    }
}