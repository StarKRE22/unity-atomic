using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Modules.AI
{
    [Serializable]
    public sealed class TransformArrayInstaller : ValueInstaller<Transform[]>
    {
        public override void Install(IBlackboard blackboard)
        {
            blackboard.SetObject(this.key, this.value);
        }
    }
}