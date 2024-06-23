using System;

namespace Atomic.Objects
{
    public sealed class EnableLogic : IEnable
    {
        private readonly Action action;
        
        public EnableLogic(Action action)
        {
            this.action = action;
        }

        public void Enable()
        {
            this.action?.Invoke();
        }
    }
}