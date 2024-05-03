using AIModule;
using Atomic;
using UnityEngine;

namespace Game.Engine
{
    [CreateAssetMenu(
        fileName = "AICondition_TargetDistance",
        menuName = "Engine/AI/New AICondition_TargetDistance"
    )]
    public sealed class AICondition_TargetDistance : AICondition
    {
        [SerializeField, BlackboardKey]
        private ushort character;

        [SerializeField, BlackboardKey]
        private ushort target;
        
        [SerializeField, BlackboardKey]
        private ushort minDistance;

        public override bool Check(IBlackboard blackboard)
        {
            if (!blackboard.TryGetObject(this.character, out IAtomicObject character) |
                !blackboard.TryGetObject(this.target, out IAtomicObject target) |
                !blackboard.TryGetFloat(this.minDistance, out float minDistance))
            {
                return false;
            }

            if (!character.TryGet(MasterAPI.Transform, out Transform characterTransform) ||
                !target.TryGet(MasterAPI.Transform, out Transform targetTransform))
            {
                return false;
            }

            Vector2 distanceVector = characterTransform.position - targetTransform.position;
            bool distanceReached = distanceVector.sqrMagnitude <= minDistance * minDistance;
            return distanceReached;
        }
    }
}