using System;

namespace Atomic.Objects
{
    public sealed class AtomicFixedUpdate : IAtomicObject.IFixedUpdate
    {
        private Action<float> action;

        public AtomicFixedUpdate()
        {
        }

        public AtomicFixedUpdate(Action<float> action)
        {
            this.action = action;
        }

        public void Compose(Action<float> action)
        {
            this.action = action;
        }

        public void OnFixedUpdate(IAtomicObject obj, float deltaTime)
        {
            this.action?.Invoke(deltaTime);
        }
    }
}