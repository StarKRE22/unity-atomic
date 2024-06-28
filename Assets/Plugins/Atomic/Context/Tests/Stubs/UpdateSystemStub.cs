namespace Atomic.Contexts
{
    public sealed class UpdateSystemStub : IUpdateSystem, IFixedUpdateSystem, ILateUpdateSystem
    {
        public bool updated;
        public bool fixedUpdated;
        public bool lateUpdated;

        public void Update(IContext context, float deltaTime)
        {
            this.updated = true;
        }

        public void FixedUpdate(IContext context, float deltaTime)
        {
            this.fixedUpdated = true;
        }

        public void LateUpdate(IContext context, float deltaTime)
        {
            this.lateUpdated = true;
        }
    }
}