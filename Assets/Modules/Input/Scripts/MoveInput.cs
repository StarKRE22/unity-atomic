using UnityEngine;

namespace Modules.Gameplay
{
    public sealed class MoveInput
    {
        public event MoveDelegate OnMove;

        private readonly MoveInputConfig config;

        public MoveInput(MoveInputConfig config)
        {
            this.config = config;
        }
        
        public void Update()
        {
            Vector3 direction = this.ReadDirection();
            if (direction != Vector3.zero)
            {
                this.OnMove?.Invoke(direction, Time.deltaTime);
            }
        }

        private Vector3 ReadDirection()
        {
            Vector3 direction = Vector3.zero;
            
            if (Input.GetKey(config.forward))
            {
                direction.z = 1;
            }
            else if (Input.GetKey(config.back))
            {
                direction.z = -1;
            }

            if (Input.GetKey(config.left))
            {
                direction.x = -1;
            }
            else if (Input.GetKey(config.right))
            {
                direction.x = 1;
            }

            return direction;
        }
    }
}