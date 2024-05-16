using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace Sample
{
    public sealed class FlipMechanicsRx : IAtomicObject.IComposable
    {
        public void Compose(IAtomicObject obj)
        {
            obj.OnFixedUpdate(deltaTime =>
            {
                float moveDirection = obj.GetMoveComponent()!.CurrentDirection.Value;
                if (moveDirection != 0)
                {
                    obj.GetTransform()!.localScale = new Vector3(moveDirection, 1, 1);
                }
            });
        }
    }
}