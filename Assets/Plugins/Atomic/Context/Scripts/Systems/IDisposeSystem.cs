namespace Atomic.Contexts
{
    public interface IDisposeSystem : ISystem
    {
        void Dispose(IContext context);
    }
}