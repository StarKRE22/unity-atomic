using System;
using UnityEngine;

namespace Modules.AI
{
    [Serializable]
    public sealed class GameObjectInstaller : ValueInstaller<GameObject>
    {
        public override void Install(IBlackboard blackboard)
        {
            blackboard.SetObject(this.key, this.value);
        }
    }
}