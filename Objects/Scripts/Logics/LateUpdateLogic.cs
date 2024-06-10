using System;

namespace Atomic.Objects
{
    public sealed class LateUpdateLogic : ILateUpdate
    {
        private readonly Action<float> action;
        
        public LateUpdateLogic(Action<float> action)
        {
            this.action = action;
        }

        public void OnLateUpdate(float deltaTime)
        {
            this.action?.Invoke(deltaTime);
        }
    }
}