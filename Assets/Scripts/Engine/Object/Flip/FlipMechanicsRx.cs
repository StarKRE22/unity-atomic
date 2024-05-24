using Atomic.Elements;
using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace Sample
{
    public sealed class FlipMechanicsRx : IObject.IFixedUpdate
    {
        private readonly IAtomicValue<float> currentDirection;
        private readonly Transform transform;
        
        public FlipMechanicsRx(IObject obj)
        {
            this.currentDirection = obj.GetMoveComponent()!.CurrentDirection;
            this.transform = obj.GetTransform();
        }

        public void OnFixedUpdate(IObject obj, float deltaTime)
        {
            float moveDirection = this.currentDirection.Value;
            if (moveDirection != 0)
            {
                transform!.localScale = new Vector3(moveDirection, 1, 1);
            }
        }
    }
}