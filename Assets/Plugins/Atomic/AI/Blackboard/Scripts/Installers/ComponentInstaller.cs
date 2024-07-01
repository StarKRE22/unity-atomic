using System;
using UnityEngine;

namespace Modules.AI
{
    [Serializable]
    public sealed class ComponentInstaller : ValueInstaller<Component>
    {
        public override void Install(IBlackboard blackboard)
        {
            blackboard.SetObject(this.key, this.value);
        }
    }
}