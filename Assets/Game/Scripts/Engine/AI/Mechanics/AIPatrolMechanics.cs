using AIModule;
using Atomic;
using UnityEngine;

namespace Game.Engine
{
    [CreateAssetMenu(
        fileName = "AIPatrolMechanics",
        menuName = "Engine/AI/New AIPatrolMechanics"
    )]
    public sealed class AIPatrolMechanics : AIMechanics
    {
        [SerializeField, BlackboardKey]
        private ushort patrolEnabled;

        [SerializeField, BlackboardKey]
        private ushort patrolPoints;

        [SerializeField, BlackboardKey]
        private ushort patrolIndex;

        [SerializeField, BlackboardKey]
        private ushort stoppingDistance;

        [SerializeField, BlackboardKey]
        private ushort character;

        public override void OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!blackboard.TryGetBool(this.patrolEnabled, out bool patrolEnabled) ||
                !blackboard.TryGetObject(this.patrolPoints, out Transform[] patrolPoints) ||
                !blackboard.TryGetInt(this.patrolIndex, out int patrolIndex) ||
                !blackboard.TryGetFloat(this.stoppingDistance, out float stoppingDistance) ||
                !blackboard.TryGetObject(this.character, out IAtomicObject character))
            {
                return;
            }

            if (!patrolEnabled)
            {
                return;
            }

            if (!character.TryGet(MasterAPI.Transform, out Transform transform))
            {
                return;
            }

            Vector2 currentPosition = transform.position;
            Vector2 targetPosition = patrolPoints[patrolIndex].position;
            Vector2 distanceVector = targetPosition - currentPosition;

            if (distanceVector.sqrMagnitude <= stoppingDistance * stoppingDistance)
            {
                patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
                blackboard.SetInt(this.patrolIndex, patrolIndex);
            }
            else
            {
                float moveDirection = Mathf.Sign(distanceVector.x);
                character.GetVariable<float>(MovementAPI.MoveDirection).Value = moveDirection;
            }
        }
    }
}