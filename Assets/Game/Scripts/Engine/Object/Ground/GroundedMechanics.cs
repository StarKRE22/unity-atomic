using Atomic;
using UnityEngine;

namespace Game.Engine
{
    public sealed class GroundedMechanics
    {
        private static readonly int GROUND_MASK = LayerMask.GetMask("Ground");
        
        private readonly Transform groundPoint;
        private readonly IAtomicVariable<bool> canJump;
        private readonly IAtomicValue<float> groundDistance;

        public GroundedMechanics(Transform groundPoint, IAtomicVariable<bool> canJump, IAtomicValue<float> groundDistance)
        {
            this.groundPoint = groundPoint;
            this.canJump = canJump;
            this.groundDistance = groundDistance;
        }

        public void FixedUpdate()
        {
            this.canJump.Value = Physics2D.Raycast(this.groundPoint.position, Vector2.down, this.groundDistance.Value, GROUND_MASK);
        }
    }
}