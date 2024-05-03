using System;
using AIModule;
using Atomic;
using UnityEngine;

// ReSharper disable MergeIntoNegatedPattern

namespace Game.Engine
{

    [Serializable]
    public sealed class BTNode_AttackTarget : BTNode
    {
        public override string Name => "Attack Target";

        [SerializeField, BlackboardKey]
        private ushort character;

        [SerializeField, BlackboardKey]
        private ushort target;

        [SerializeField, BlackboardKey]
        private ushort minDistance;

        protected override BTState OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!blackboard.TryGetObject(this.character, out IAtomicObject character) ||
                !blackboard.TryGetObject(this.target, out IAtomicObject target) ||
                !blackboard.TryGetFloat(this.minDistance, out float attackDistance))
            {
                return BTState.FAILURE;
            }

            Transform characterTransform = character.Get<Transform>(MasterAPI.Transform);
            Transform targetTransform = target.Get<Transform>(MasterAPI.Transform);
            if (characterTransform == null || targetTransform == null)
            {
                return BTState.FAILURE;
            }

            Vector3 distanceVector = targetTransform.position - characterTransform.position;
            if (distanceVector.sqrMagnitude > attackDistance * attackDistance)
            {
                return BTState.FAILURE;
            }

            float targetDirection = Mathf.Sign(distanceVector.x);
            character.SetVariable(CommonAPI.LookDirection, targetDirection);
            character.InvokeAction(AttackAPI.AttackRequest);
            return BTState.RUNNING;
        }
    }
}