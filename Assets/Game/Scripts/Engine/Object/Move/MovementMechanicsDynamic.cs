using Atomic;
using UnityEngine;

namespace Game.Engine
{
    public sealed class MovementMechanicsDynamic : IAtomicFixedUpdate
    {
        private readonly IAtomicObject entity;

        public MovementMechanicsDynamic(IAtomicObject entity)
        {
            this.entity = entity;
        }

        public void OnFixedUpdate(float deltaTime)
        {
            if (!this.entity.TryGet(MasterAPI.Rigidbody, out Rigidbody2D rigidbody) ||
                !this.entity.TryGet(MovementAPI.MoveEnabled, out IAtomicValue<bool> moveEnabled) ||
                !this.entity.TryGet(MovementAPI.MoveDirection, out IAtomicValue<float> moveDirection) ||
                !this.entity.TryGet(MovementAPI.BaseMoveSpeed, out IAtomicValue<float> moveSpeed))
            {
                return;
            }
            
            float speedX = 0.0f;
            float speedY = rigidbody.velocity.y;

            if (moveEnabled.Value)
            {
                speedX = moveDirection.Value * moveSpeed.Value;
            }

            rigidbody.velocity = new Vector2(speedX, speedY);
        }
    }
}