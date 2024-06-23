using System;

namespace Atomic.Objects
{
    public sealed class ConstructableLogic : IConstructable
    {
        private readonly Action<IObject> action;

        public ConstructableLogic(Action<IObject> action)
        {
            this.action = action;
        }

        public void Construct(IObject obj)
        {
            this.action?.Invoke(obj);
        }
    }
}