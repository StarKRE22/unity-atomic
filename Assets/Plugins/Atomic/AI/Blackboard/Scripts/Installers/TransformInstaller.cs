using System;
using UnityEngine;

namespace Modules.AI
{
    [Serializable]
    public sealed class TransformInstaller : ValueInstaller<Transform>
    {
        public override void Install(IBlackboard blackboard)
        {
            blackboard.SetObject(this.key, this.value);
        }
    }
}