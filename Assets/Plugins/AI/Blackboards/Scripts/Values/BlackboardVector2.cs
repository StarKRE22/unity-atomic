using System;
using UnityEngine;

namespace AIModule
{
    [Serializable]
    public sealed class BlackboardVector2 : BlackboardValue<Vector2>
    {
        public override void Apply(IBlackboard blackboard)
        {
            blackboard.SetVector2(this.key, this.value);
        }
    }
}