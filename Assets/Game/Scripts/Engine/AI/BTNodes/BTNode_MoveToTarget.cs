using System;
using AIModule;
using Atomic;
using UnityEngine;

namespace Game.Engine
{
    [Serializable]
    public sealed class BTNode_MoveToTarget : BTNode
    {
        public override string Name => "Move To Target";

        [SerializeField, BlackboardKey]
        private ushort character;

        [SerializeField, BlackboardKey]
        private ushort target;

        [SerializeField, BlackboardKey]
        private ushort stoppingDistance;

        protected override BTState OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!blackboard.TryGetObject(this.character, out IAtomicObject character) ||
                !blackboard.TryGetObject(this.target, out IAtomicObject target) ||
                !blackboard.TryGetFloat(this.stoppingDistance, out float stoppingDistance))
            {
                return BTState.FAILURE;
            }
            
            if (!character.TryGet(MasterAPI.Transform, out Transform characterTransform) ||
                !target.TryGet(MasterAPI.Transform, out Transform targetTransform))
            {
                return BTState.FAILURE;
            }

            Vector2 currentPosition = characterTransform.position;
            Vector2 targetPosition = targetTransform.position;
            Vector2 distanceVector = targetPosition - currentPosition;

            if (distanceVector.sqrMagnitude <= stoppingDistance * stoppingDistance)
            {
                return BTState.SUCCESS;
            }

            float moveDirection = Mathf.Sign(distanceVector.x);
            character.GetVariable<float>(MovementAPI.MoveDirection).Value = moveDirection;
            return BTState.RUNNING;
        }

        protected override void OnEnable(IBlackboard blackboard)
        {
            if (blackboard.TryGetObject(this.character, out IAtomicObject character))
            {
                character.SetVariable(MovementAPI.MoveDirection, 0.0f);
            }
        }

        protected override void OnDisable(IBlackboard blackboard)
        {
            if (blackboard.TryGetObject(this.character, out IAtomicObject character))
            {
                character.SetVariable(MovementAPI.MoveDirection, 0.0f);
            }
        }
    }
}