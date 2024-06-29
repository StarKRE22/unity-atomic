namespace Atomic.Contexts
{
    public sealed class CommonSystemStub : 
        IInitSystem,
        IEnableSystem,
        IDisableSystem,
        IUpdateSystem,
        IFixedUpdateSystem,
        ILateUpdateSystem,
        IDestroySystem
    {
        public bool initialized;
        public bool enabled;
        public bool disabled;
        public bool updated;
        public bool fixedUpdated;
        public bool lateUpdated;
        public bool destroyed;
        
        public void Init(IContext context)
        {
            this.initialized = true;
        }

        public void Enable(IContext context)
        {
            this.enabled = true;
        }

        public void Disable(IContext context)
        {
            this.disabled = true;
        }

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

        public void Destroy(IContext context)
        {
            this.destroyed = true;
        }
    }
}