namespace Atomic.Contexts
{
    public sealed class DestroySystemStub : IDestroySystem
    {
        public bool destroyed;
        
        public void Destroy(IContext context)
        {
            destroyed = true;
        }
    }
}