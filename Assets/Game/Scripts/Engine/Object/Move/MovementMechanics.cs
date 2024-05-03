using Atomic;
using UnityEngine;

namespace Game.Engine
{
    public sealed class MovementMechanics
    {
        private readonly Rigidbody2D rigidbody;
        private readonly IAtomicValue<bool> enabled;
        private readonly IAtomicValue<float> moveDirection;
        private readonly IAtomicValue<float> moveSpeed;

        public MovementMechanics(
            Rigidbody2D rigidbody,
            IAtomicValue<bool> enabled,
            IAtomicValue<float> moveDirection,
            IAtomicValue<float> moveSpeed
        )
        {
            this.rigidbody = rigidbody;
            this.enabled = enabled;
            this.moveDirection = moveDirection;
            this.moveSpeed = moveSpeed;
        }

        public void FixedUpdate()
        {
            float speedX = 0.0f;
            float speedY = this.rigidbody.velocity.y;

            if (this.enabled.Value)
            {
                speedX = this.moveDirection.Value * this.moveSpeed.Value;
            }
            
            this.rigidbody.velocity = new Vector2(speedX, speedY);
        }
    }
}