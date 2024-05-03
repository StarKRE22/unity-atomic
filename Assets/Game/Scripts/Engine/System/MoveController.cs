using Atomic;
using UnityEngine;

namespace Game.Engine
{
    public sealed class MoveController
    {
        private readonly IAtomicSetter<float> moveDirection;

        public MoveController(IAtomicSetter<float> moveDirection)
        {
            this.moveDirection = moveDirection;
        }

        public void Update()
        {
            float moveDirection = 0;
            
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                moveDirection = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                moveDirection = 1;
            }

            if (this.moveDirection != null)
            {
                this.moveDirection.Value = moveDirection;
            }
        }
    }
}