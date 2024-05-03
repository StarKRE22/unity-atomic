using Atomic;
using UnityEngine;

namespace Game.Engine
{
    public sealed class FlipMechanics : IAtomicFixedUpdate
    {
        private readonly IAtomicValue<float> flipDirection;
        private readonly Transform transform;
        
        public FlipMechanics(IAtomicValue<float> flipDirection, Transform transform)
        {
            this.flipDirection = flipDirection;
            this.transform = transform;
        }

        public void OnFixedUpdate(float deltaTime)
        {
            float moveDirection = this.flipDirection.Value;
            
            if (moveDirection == 0)
            { 
                return;
            }

            float angle = moveDirection > 0 ? 0 : 180;
            this.transform.eulerAngles = new Vector3(0, angle, 0);
        }
    }
}