using System;
using Object = UnityEngine.Object;

namespace Modules.AI
{
    [Serializable]
    public sealed class UnityObjectInstaller : ValueInstaller<Object>
    {
        public override void Install(IBlackboard blackboard)
        {
            blackboard.SetObject(this.key, this.value);
        }
    }
}