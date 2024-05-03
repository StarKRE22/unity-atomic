using Atomic;
using UnityEngine;

namespace Game.Engine
{
    public sealed class FlipMechanicsDynamic : IAtomicFixedUpdate
    {
        private readonly IAtomicObject entity;
        
        private readonly Transform _transform;

        public FlipMechanicsDynamic(IAtomicObject entity)
        {
            this.entity = entity;
            _transform = this.entity.Get<Transform>(MasterAPI.Transform);
        }

        public void OnFixedUpdate(float deltaTime)
        {
            if (!this.entity.TryGet(MovementAPI.MoveDirection, out IAtomicValue<float> moveDirection))
            {
                return;
            }

            if (moveDirection.Value != 0)
            {
                _transform.localScale = new Vector3(moveDirection.Value, 1, 1);
            }
        }
    }
}