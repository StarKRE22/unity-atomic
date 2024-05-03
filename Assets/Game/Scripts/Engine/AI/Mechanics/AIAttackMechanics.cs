using AIModule;
using Atomic;
using UnityEngine;

namespace Game.Engine
{
    [CreateAssetMenu(
        fileName = "AIAttackMechanics",
        menuName = "Engine/AI/New AIAttackMechanics"
    )]
    public sealed class AIAttackMechanics : AIMechanics
    {
        [SerializeField, BlackboardKey]
        private ushort attackEnabled;

        [SerializeField, BlackboardKey]
        private ushort attackDistance;

        [SerializeField, BlackboardKey]
        private ushort character;

        [SerializeField, BlackboardKey]
        private ushort target;

        public override void OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!blackboard.TryGetBool(this.attackEnabled, out bool attackEnabled) ||
                !blackboard.TryGetFloat(this.attackDistance, out float attackDistance) ||
                !blackboard.TryGetObject(this.character, out IAtomicEntity character) ||
                !blackboard.TryGetObject(this.target, out IAtomicEntity target))
            {
                return;
            }

            if (!attackEnabled)
            {
                return;
            }

            if (!target.TryGet(HealthAPI.IsAlive, out IAtomicValue<bool> isAlive) || !isAlive.Value)
            {
                return;
            }

            if (!character.TryGet(MasterAPI.Transform, out Transform characterTransform) ||
                !target.TryGet(MasterAPI.Transform, out Transform targetTransform))
            {
                return;
            }

            Vector2 currentPosition  = characterTransform.position;
            Vector2 targetPosition = targetTransform.position;
            Vector2 distanceVector = targetPosition - currentPosition;

            float targetDirection = Mathf.Sign(distanceVector.x);

            if (distanceVector.sqrMagnitude <= attackDistance * attackDistance)
            {
                character.SetVariable(CommonAPI.LookDirection, targetDirection);
                character.InvokeAction(AttackAPI.AttackRequest);
                targetDirection = 0;
            }
            
            character.SetVariable(MovementAPI.MoveDirection, targetDirection);
        }
    }
}