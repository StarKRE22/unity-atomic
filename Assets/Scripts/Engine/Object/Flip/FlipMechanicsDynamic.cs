using System;
using Atomic.Elements;
using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace Sample
{
    [Serializable]
    public sealed class FlipMechanicsDynamic : IAtomicObject.IFixedUpdate
    {
        public void OnFixedUpdate(IAtomicObject obj, float deltaTime)
        {
            if (!obj.TryGetTransform(out var transform) || 
                !obj.TryGetMoveComponent(out var moveComponent))
            {
                return;
            }

            IAtomicValue<float> moveDirection = moveComponent.CurrentDirection;
            if (moveDirection.Value != 0)
            {
                transform.localScale = new Vector3(moveDirection.Value, 1, 1);
            }
        }
    }
}