using System;
using Atomic;
using UnityEngine;

namespace Game.Engine
{
    [Serializable]
    public sealed class GroundedComponent : IAtomicFixedUpdate
    {
        [Get(CommonAPI.IsGrounded)]
        public IAtomicValue<bool> IsGrounded => this.isGrounded;

        [SerializeField]
        private Transform feetTransform;
        
        [SerializeField]
        private AtomicVariable<bool> isGrounded;
        
        [SerializeField]
        private AtomicValue<float> groundDistance = new(0.1f);
        
        private GroundedMechanics groundedMechanics;
        
        public GroundedComponent(Transform feetTransform)
        {
            this.feetTransform = feetTransform;
            this.isGrounded = new AtomicVariable<bool>();
            this.Compose();
        }

        public void Compose()
        {
            this.groundedMechanics = new GroundedMechanics(
                this.feetTransform,
                this.isGrounded,
                this.groundDistance
            );
        }

        public void OnFixedUpdate(float deltaTime)
        {
            this.groundedMechanics.FixedUpdate();
        }

        public void OnDrawGizmos()
        {
            Gizmos.DrawLine(this.feetTransform.position,
                this.feetTransform.position + Vector3.down * this.groundDistance.Value);
        }
    }
}