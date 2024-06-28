namespace Atomic.Contexts
{
    public sealed class InitSystemStub : IInitSystem
    {
        public bool initialized;
        
        public void Init(IContext context)
        {
            this.initialized = true;
        }
    }
}