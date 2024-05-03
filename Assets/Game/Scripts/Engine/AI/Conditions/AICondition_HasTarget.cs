using AIModule;
using Atomic;
using UnityEngine;

namespace Game.Engine
{
    [CreateAssetMenu(
        fileName = "AICondition_HasTarget",
        menuName = "Engine/AI/New AICondition_HasTarget"
    )]
    public sealed class AICondition_HasTarget : AICondition
    {
        [SerializeField, BlackboardKey]
        private ushort target;

        public override bool Check(IBlackboard blackboard)
        {
            bool hasTarget = blackboard.TryGetObject(this.target, out IAtomicObject target) &&
                          target.TryGet(HealthAPI.IsAlive, out IAtomicValue<bool> isAlive) && isAlive.Value;

            return hasTarget;
        }
    }
}