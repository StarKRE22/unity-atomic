using AIModule;
using Atomic;
using Game.Engine;
using UnityEngine;

namespace GOAPModule.Samples
{
    [CreateAssetMenu(
        fileName = "MoveToTargetAction",
        menuName = "GOAP/Samples/New MoveToTargetAction"
    )]
    public sealed class MoveToTargetAction : GoapAction
    {
        [SerializeField, BlackboardKey, Space]
        private ushort character;

        [SerializeField, BlackboardKey]
        private ushort target;

        [SerializeField, BlackboardKey]
        private ushort stoppingDistance;

        [SerializeField, Space]
        private float distanceCoef = 1;

        protected internal override bool IsValid(IBlackboard blackboard)
        {
            return blackboard.HasKey(this.character) &&
                   blackboard.HasKey(this.target) && 
                   blackboard.HasKey(this.stoppingDistance);
        }

        protected internal override int GetCost(IBlackboard blackboard)
        {
            IAtomicObject character = blackboard.GetObject<IAtomicObject>(this.character);
            IAtomicObject target = blackboard.GetObject<IAtomicObject>(this.target);

            Vector3 characterPosition = character.Get<Transform>(MasterAPI.Transform).position;
            Vector3 targetPosition = target.Get<Transform>(MasterAPI.Transform).position;
            Vector3 distanceVector = targetPosition - characterPosition;

            float distance = distanceVector.magnitude - blackboard.GetFloat(this.stoppingDistance);
            int cost = Mathf.RoundToInt(Mathf.Max(0, distance) * this.distanceCoef);
            return cost;
        }
        

        protected override State OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!blackboard.TryGetObject(this.character, out IAtomicEntity character) ||
                !blackboard.TryGetObject(this.target, out IAtomicEntity target) ||
                !blackboard.TryGetFloat(this.stoppingDistance, out float stoppingDistance))
            {
                return State.FAILURE;
            }

            Vector2 currentPosition = character.Get<Transform>(MasterAPI.Transform).position;
            Vector2 targetPosition = target.Get<Transform>(MasterAPI.Transform).position;
            Vector2 distanceVector = targetPosition - currentPosition;

            if (distanceVector.sqrMagnitude <= stoppingDistance * stoppingDistance)
            {
                return State.SUCCESS;
            }

            float moveDirection = Mathf.Sign(distanceVector.x);
            // character.InvokeAction(MoveAPI.MoveStep, moveDirection);
            return State.RUNNING;
        }
    }
}