using System;
using Unity.Mathematics;

namespace Modules.AI
{
    [Serializable]
    public sealed class QuaternionInstaller : ValueInstaller<quaternion>
    {
        public override void Install(IBlackboard blackboard)
        {
            blackboard.SetQuaternion(this.key, this.value);
        }
    }
}