namespace Atomic.Contexts
{
    public interface IDestroySystem : ISystem
    {
        void Destroy(IContext context);
    }
}