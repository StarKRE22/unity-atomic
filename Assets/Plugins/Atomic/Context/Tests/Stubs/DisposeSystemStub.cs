namespace Atomic.Contexts
{
    public sealed class DisposeSystemStub : IDisposeSystem
    {
        public bool destroyed;
        
        public void Dispose(IContext context)
        {
            destroyed = true;
        }
    }
}