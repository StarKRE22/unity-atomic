using System;

namespace Atomic.Objects
{
    public sealed class DisableLogic : IDisable
    {
        private readonly Action action;

        public DisableLogic(Action action)
        {
            this.action = action;
        }

        public void Disable()
        {
            this.action?.Invoke();
        }
    }
}