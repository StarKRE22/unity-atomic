using System;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace Sample
{
    [Serializable]
    public sealed class FlipMechanics : IObject.IFixedUpdate
    {
        private readonly IAtomicValue<float> moveDirection;
        private readonly Transform transform;
        
        public FlipMechanics(IAtomicValue<float> moveDirection, Transform transform)
        {
            this.moveDirection = moveDirection;
            this.transform = transform;
        }

        public void OnFixedUpdate(IObject _, float deltaTime)
        {
            float moveDirection = this.moveDirection.Value;
            
            if (moveDirection != 0)
            {
                this.transform.localScale = new Vector3(moveDirection, 1, 1);
            }
        }
    }
}