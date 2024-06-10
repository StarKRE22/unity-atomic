using System;

namespace Atomic.Objects
{
    public sealed class UpdateLogic : IUpdate
    {
        private readonly Action<float> action;

        public UpdateLogic(Action<float> action)
        {
            this.action = action;
        }

        public void OnUpdate(float deltaTime)
        {
            this.action?.Invoke(deltaTime);
        }
    }
}