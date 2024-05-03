using System;
using AIModule;
using Atomic;
using UnityEngine;

namespace Game.Engine
{
    [Serializable]
    public sealed class BTNode_MoveToPosition : BTNode
    {
        public override string Name => "Move To Position";

        [SerializeField, BlackboardKey]
        private ushort character;

        [SerializeField, BlackboardKey]
        private ushort movePosition;

        [SerializeField, BlackboardKey]
        private ushort stoppingDistance;

        protected override BTState OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!blackboard.TryGetObject(this.character, out IAtomicObject character) ||
                !blackboard.TryGetVector2(this.movePosition, out Vector2 movePosition) ||
                !blackboard.TryGetFloat(this.stoppingDistance, out float stoppingDistance))
            {
                return BTState.FAILURE;
            }
            
            if (!character.TryGet(MasterAPI.Transform, out Transform transform))
            {
                return BTState.FAILURE;
            }

            Vector2 currentPosition = transform.position;
            Vector2 distanceVector = movePosition - currentPosition;

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