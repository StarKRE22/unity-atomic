using System;

namespace Atomic.Objects
{
    public sealed class FixedUpdateLogic : IFixedUpdate
    {
        private readonly Action<float> action;

        public FixedUpdateLogic(Action<float> action)
        {
            this.action = action;
        }

        public void OnFixedUpdate(float deltaTime)
        {
            this.action?.Invoke(deltaTime);
        }
    }
}