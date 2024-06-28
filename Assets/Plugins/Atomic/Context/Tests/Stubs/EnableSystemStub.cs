namespace Atomic.Contexts
{
    public sealed class EnableSystemStub : IEnableSystem
    {
        public bool enabled;
        
        public void Enable(IContext context)
        {
            this.enabled = true;
        }
    }
}