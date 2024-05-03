using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AIModule
{
    [Serializable]
    public sealed class BlackboardTransformArray : BlackboardValue<Transform[]>
    {
        public override void Apply(IBlackboard blackboard)
        {
            blackboard.SetObject(this.key, this.value);
        }
    }
}