namespace Atomic.UI
{
    public interface IDestroyHandler : IHandler
    {
        void Destroy(IView view);
    }
}