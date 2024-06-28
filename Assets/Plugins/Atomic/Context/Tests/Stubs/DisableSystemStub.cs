namespace Atomic.Contexts
{
    public sealed class DisableSystemStub : IDisableSystem
    {
        public bool disabled;
        
        public void Disable(IContext context)
        {
            disabled = true;
        }
    }
}